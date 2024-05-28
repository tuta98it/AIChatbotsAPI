using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ConfigureServices).Assembly;

        //services.AddAutoMapper(assembly);

        //services.AddValidatorsFromAssembly(assembly);

        //services.AddMediatR(configuration =>
        //{
        //    configuration.RegisterServicesFromAssembly(assembly);
        //    configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        //    // configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        //});

        //services.AddScoped<IProductService, ProductService>();
        //services.AddScoped<IGeneralService, GeneralService>();
        //services.AddScoped<IUserService, UserService>();
        //services.AddScoped<ILogService, LogService>();
        //services.AddScoped<IInitUserPermissionService, InitUserPermissionService>();
        //services.AddScoped<INotificationService, NotificationService>();
        //services.AddScoped<IVariantService, VariantService>();
        //services.AddScoped<ICheckTerritoryAccountService, CheckTerritoryAccountService>();
        return services;
    }
}

