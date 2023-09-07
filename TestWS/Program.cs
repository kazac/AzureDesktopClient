using Eraz51;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        options.UseSqlServer(cs, providerOptions => { providerOptions.EnableRetryOnFailure(); })
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

var scopeRequiredByApi = app.Configuration["AzureAd:Scopes"] ?? "";

app.MapGet("/products", (HttpContext httpContext, TestWSContext ctx) =>
{
 //   httpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
    var cs = app.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
    using var conn = new SqlConnection(cs);
    var ps = new List<ProductDTO>();
    try
    {
        //conn.Open();
        //var cmd = new SqlCommand();
        //cmd.Connection = conn;
        //cmd.CommandText = "SELECT ProductID, Name, ProductNumber, Color, ListPrice FROM[SalesLT].[Product]";
        //using var rdr = cmd.ExecuteReader();
        //while(rdr.Read())
        foreach (var p in ctx.Products)
        {
            var dto = new ProductDTO
            (
                p.ProductId,
                p.Name,
                p.ProductNumber,
                p.Color,
                p.ListPrice
            //rdr.GetInt32(0),
            //rdr.GetString(1),
            //rdr.GetString(2),
            //rdr.IsDBNull(3) ? null : rdr.GetString(3),
            //rdr.GetDecimal(4)
            );
            ps.Add(dto);
        }
    }
    catch (Exception ex)
    {
        ps.Add(new ProductDTO(0, ex.Message, "", "", 0M));

    }
    return ps.ToArray();
})
.RequireAuthorization();

app.Run();

internal record ProductDTO(int ProductID, string Name, string ProductNumber, string? Color, decimal ListCost);