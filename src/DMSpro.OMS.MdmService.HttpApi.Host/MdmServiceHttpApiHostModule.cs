using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DMSpro.OMS.MdmService.DbMigrations;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using DMSpro.OMS.Shared.Hosting.Microservices;
using DMSpro.OMS.Shared.Hosting.AspNetCore;
using Prometheus;
using Volo.Abp;
using Volo.Abp.Modularity;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.Vendors;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.VATs;
using DMSpro.OMS.MdmService.Items;
using DMSpro.OMS.MdmService.UOMs;

namespace DMSpro.OMS.MdmService;

[DependsOn(
    typeof(OMSSharedHostingMicroservicesModule),
    typeof(MdmServiceApplicationModule),
    typeof(MdmServiceHttpApiModule),
    typeof(MdmServiceEntityFrameworkCoreModule)
    )]
public class MdmServiceHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        //You can disable this setting in production to avoid any potential security risks.
        Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
        
        // Enable if you need these
        // var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();
        
        JwtBearerConfigurationHelper.Configure(context, "MdmService");
        SwaggerConfigurationHelper.ConfigureWithAuth(
            context: context,
            authority: configuration["AuthServer:Authority"],
            scopes: new
                Dictionary<string, string> /* Requested scopes for authorization code request and descriptions for swagger UI only */
                {
                    {"MdmService", "MdmService API"}
                },
            apiTitle: "MdmService API"
        );
        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.Trim().RemovePostFix("/"))
                            .ToArray()
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
        
        context.Services.AddGrpc().AddJsonTranscoding();

        // DISABLE ALL AUTHORIZATIONS 
        //context.Services.AddAlwaysAllowAuthorization();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCorrelationId();
        app.UseAbpRequestLocalization();
        app.UseAbpSecurityHeaders();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseAbpClaimsMap();
        app.UseMultiTenancy();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "MdmService API");
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
        });
        app.UseAbpSerilogEnrichers();
        app.UseAuditing();
        app.UseUnitOfWork();
        //app.UseConfiguredEndpoints(endpoints => endpoints.MapMetrics());
        app.UseConfiguredEndpoints(endpoints =>
        {
            endpoints.MapMetrics();
            endpoints.MapGrpcService<CompaniesGRPCAppService>();
            endpoints.MapGrpcService<CustomersGRPCAppService>();
            endpoints.MapGrpcService<ItemsGRPCAppService>();
            endpoints.MapGrpcService<SalesOrgHierarchiesGRPCAppService>();
            endpoints.MapGrpcService<UOMsGRPCAppService>();
            endpoints.MapGrpcService<VATsGRPCAppService>();
            endpoints.MapGrpcService<VendorsGRPCAppService>();
        });
    }

    public async override Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        using (var scope = context.ServiceProvider.CreateScope())
        {
            await scope.ServiceProvider
                .GetRequiredService<MdmServiceDatabaseMigrationChecker>()
                .CheckAndApplyDatabaseMigrationsAsync();
        }
    }
}