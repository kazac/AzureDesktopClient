using Eraz51;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using System;
using System.Data.SqlTypes;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddAuthorization();
var cs = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
// add no tracking
builder.Services.AddDbContext<TestWSContext>(options =>
        options.UseSqlServer(cs, providerOptions => { providerOptions.EnableRetryOnFailure(); }));
    //    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

var scopeRequiredByApi = app.Configuration["AzureAd:Scopes"] ?? "";

app.MapGet("/productCategories", (HttpContext httpContext, TestWSContext ctx) =>
{
    //   httpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
    //var cs = app.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
    //using var conn = new SqlConnection(cs);
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
})
.RequireAuthorization();

app.MapGet("/products", (HttpContext httpContext, TestWSContext ctx) =>
{
 //   httpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
    //var cs = app.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
    //using var conn = new SqlConnection(cs);
    var ps = new List<ProductDTO>();
    try
    {
        foreach (var p in ctx.Products.AsNoTracking())
        {
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
            ps.Add(dto);
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
    return Results.Ok(ps.ToArray());
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
.RequireAuthorization();

app.Run();

public record ProductCategoryDTO(int productCategoryId, int? parentCategoryID, string name, DateTime modifiedDate);
public record ProductDTO(int productId, int? productCategoryId, string name, string productNumber, string? color, decimal listPrice, DateTime modifiedDate);