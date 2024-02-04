using Eraz51;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
if (builder.Environment.IsDevelopment())
    builder.Services.AddAuthentication("BasicAuthentication")
        .AddScheme<AuthenticationSchemeOptions, MyAuthenticationHandler>("BasicAuthentication", null);

//builder.Services.AddSingleton<IAuthorizationHandler, XHandler>();

//builder.Services.AddAuthorizationBuilder()
//  .AddPolicy("X", policy => policy.Requirements.Add(new XRequirement(7)))
//  .AddPolicy("test_role", policy => policy.RequireRole("test_role"));

builder.Services.AddAuthorization();
var conStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING"));
if(builder.Environment.IsDevelopment())
    conStrBuilder.Password = builder.Configuration["DatabasePassword"];

var cs = conStrBuilder.ConnectionString;
//var cs = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<TestWSContext>(options => 
        options.UseInMemoryDatabase("TextWS")
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
}
else
{
    builder.Services.AddDbContext<TestWSContext>(options =>
        options.UseSqlServer(cs, providerOptions => { providerOptions.EnableRetryOnFailure(); })
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
}
var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

//var scopeRequiredByApi = app.Configuration["AzureAd:Scopes"] ?? "";

app.MapGet("/productCategories", (HttpContext httpContext, TestWSContext ctx) =>
{
    var pcs = new List<ProductCategoryDTO>();
    try
    {
        foreach (var pc in ctx.ProductCategories.AsNoTracking().OrderBy(x => x.ParentProductCategoryId))
        {
            var dto = new ProductCategoryDTO
            (
                pc.ProductCategoryId,
                pc.ParentProductCategoryId,
                pc.Name,
                pc.ModifiedDate
            );
            pcs.Add(dto);
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
    return Results.Ok(pcs.ToArray());
}).RequireAuthorization();


app.MapGet("/productModels", (HttpContext httpContext, TestWSContext ctx) =>
{
    var pms = new List<ProductModelDTO>();
    try
    {
        // httpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
        foreach (var pm in ctx.ProductModels.Include(pm => pm.Products).AsNoTracking())
        {
            var ps = new List<ProductDTO>();
            foreach (var p in pm.Products)
            {
                var dtoP = new ProductDTO
                (
                    p.ProductId,
                    p.ProductCategoryId,
                    p.Name,
                    p.ProductNumber,
                    p.Color,
                    p.ListPrice,
                    p.ModifiedDate
                );
                ps.Add(dtoP);
            }
            var dto = new ProductModelDTO
            (
                pm.ProductModelId,
                pm.Name,
                pm.CatalogDescription,
                pm.Rowguid,
                pm.ModifiedDate,
                ps.ToArray()
            );
            pms.Add(dto);
        }
        return TypedResults.Ok(pms.ToArray());
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
})
.RequireAuthorization();
//.AddEndpointFilter<TodoIsValidUcFilter>();

app.MapGet("/products/{id}", Results<Ok<ProductDTO>, NotFound<int>, ProblemHttpResult> (HttpContext httpContext, TestWSContext ctx, int id) =>
{
    try
    {
        var p = ctx.Products.AsNoTracking().Where(x => x.ProductId == id).FirstOrDefault();
        if (p == null) return TypedResults.NotFound(id);
        var dto = new ProductDTO
        (
            p.ProductId,
            p.ProductCategoryId,
            p.Name,
            p.ProductNumber,
            p.Color,
            p.ListPrice,
            p.ModifiedDate
        );
        return
        TypedResults.Ok(dto);
    }
    catch (Exception ex)
    {
        return TypedResults.Problem(ex.Message);
    }
})
.RequireAuthorization();

app.MapPut("/products", async (HttpContext httpContext, TestWSContext ctx, ProductDTO[] dtos) =>
{
    try
    {
        var dt = DateTime.Now;
        foreach (var dto in dtos)
        {
            var product = await ctx.Products.FindAsync(dto.productId);
            if (product is null) return Results.NotFound();
            if (product.ModifiedDate != dto.modifiedDate) return Results.Problem($"concurrency id:{dto.productId}");
            if (product.Name != dto.name) product.Name = dto.name;
            if (product.ProductNumber != dto.productNumber) product.ProductNumber = dto.productNumber;
            if (product.ProductCategoryId != dto.productCategoryId) product.ProductCategoryId = dto.productCategoryId;
            if (product.Color != dto.color) product.Color = dto.color;
            if (product.ListPrice != dto.listPrice) product.ListPrice = dto.listPrice;
            product.ModifiedDate = dt;
        }
        await ctx.SaveChangesAsync();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
    return Results.NoContent();

})
.RequireAuthorization()
// see https://www.nuget.org/packages/MinimalApis.Extensions#readme-body-tab for model validatiion
.AddEndpointFilter(async (context, next) =>
{
    var productDTO = context.GetArgument<ProductDTO>(0);
    var errors = new Dictionary<string, string[]>();
    if(productDTO.name.Length == 0)
        errors.Add(nameof(productDTO.name), new string[] { "Name is required" });

    if(errors.Count > 0)
    {
        var problemDetails = new ValidationProblemDetails(errors);
        return Results.ValidationProblem(errors);
    }
    return await next(context);
});


app.MapPost("/productCategories", async (HttpContext httpContext, TestWSContext ctx, ProductCategoryDTO dto) =>
{
    try
    {
        var dt = DateTime.Now;
        var cat = new ProductCategory();
        var lastId = ctx.ProductCategories.Count();
        var pc = new ProductCategory { ProductCategoryId = lastId + 1, ModifiedDate = DateTime.Now, Name = dto.name, ParentProductCategoryId = dto.parentCategoryID };
        ctx.ProductCategories.Add(pc);
        await ctx.SaveChangesAsync();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
    return Results.NoContent();

})
.RequireAuthorization()
.RequireAuthorization()
.AddEndpointFilter(async (context, next) =>
{
    var productDTO = context.GetArgument<ProductCategoryDTO>(2);
    var errors = new Dictionary<string, string[]>();
    if (productDTO.name.Length == 0)
        errors.Add(nameof(productDTO.name), new string[] { "Name is required" });

    if (errors.Count > 0)
    {
        var problemDetails = new ValidationProblemDetails(errors);
        return Results.ValidationProblem(errors);
    }
    return await next(context);
});


app.Run();

public record ProductCategoryDTO(int productCategoryId, int? parentCategoryID, string name, DateTime modifiedDate);
public record ProductDTO(int productId, int? productCategoryId, string name, string productNumber, string? color, decimal listPrice, DateTime modifiedDate);
public record ProductModelDTO(int productModelId, string name, string? catalogDescription, Guid rowguid, DateTime modifiedDate, ProductDTO[] products);

public class XRequirement : Microsoft.AspNetCore.Authorization.IAuthorizationRequirement
{
    public int X { get; }
    public XRequirement(int x) => X = x;
}

public class  XHandler : AuthorizationHandler<XRequirement>
{
    List<(int, string)> owners = new();
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, XRequirement requirement)
    {
        var httpContext = context.Resource as HttpContext;
        //var ok = httpContext.Request.RouteValues.TryGetValue("id", out object? id);
        //if (ok)
        //{
        //    var user = context.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        //    var owner = owners.Find(x => x.Item1 == (int) id!).Item2;
        //    if(user == owner)
        //        context.Succeed(requirement);
        //}
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}

