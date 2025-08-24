using System.Reflection;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;
using Peyghom.Common.Application.Behaviors;
using Peyghom.Common.Application.Caching;
using Peyghom.Common.Infrastructure.Authentication;
using Peyghom.Common.Infrastructure.Authorization;
using Peyghom.Common.Infrastructure.Caching;
using Peyghom.Common.Presentation.Endpoints;
using StackExchange.Redis;

namespace Peyghom.Common;

public static class Extension
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        System.Reflection.Assembly[] moduleAssemblies)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(moduleAssemblies);

            config.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssemblies(moduleAssemblies, includeInternalTypes: true);


        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        
        return services;
    }

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string databaseConnectionString,
        string redisConnectionString)
    {
        services.AddAuthenticationInternal();
        services.AddAuthorizationInternal();

        try
        {
            IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.TryAddSingleton(connectionMultiplexer);

            services.AddStackExchangeRedisCache(options =>
                options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer));
        }
        catch
        {
            services.AddDistributedMemoryCache();
        }

        services.TryAddSingleton<ICacheService, CacheService>();

        var mongoClientSettings = MongoClientSettings.FromConnectionString(databaseConnectionString);
        //mongoClientSettings.AllowInsecureTls = true;
        services.AddSingleton<IMongoClient>(new MongoClient(mongoClientSettings));

        //services.AddScoped<IMongoDatabase>(provider =>
        //   provider.GetRequiredService<IMongoClient>().GetDatabase("peyghom"));

        return services;
    }

    /// <summary>
    /// This method scans the provided assemblies for classes that implement the IEndpoint 
    /// interface and registers them as transient services in the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public static IServiceCollection AddEndpoints(this IServiceCollection services,
        params System.Reflection.Assembly[] assemblies)
    {
        // we'll search for the classes that implemented I Endpoint and
        // register them as transient
        ServiceDescriptor[] serviceDescriptors = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        // Register hub endpoints
        var hubDescriptors = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IHubEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IHubEndpoint), type))
            .ToArray();

        // ensures that duplicate registrations are avoided
        services.TryAddEnumerable(serviceDescriptors);
        services.TryAddEnumerable(hubDescriptors);

        return services;
    }

    /// <summary>
    /// This method retrieves all registered IEndpoint implementations from the DI container 
    /// and maps them to the application's routing system.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="routeGroupBuilder"></param>
    /// <returns></returns>
    public static IApplicationBuilder MapEndpoints(
        this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        // Retrieves all registered implementations of IEndpoint from the DI container
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        IEnumerable<IHubEndpoint> hubEndpoints = app.Services.GetRequiredService<IEnumerable<IHubEndpoint>>();

        foreach (IHubEndpoint hubEndpoint in hubEndpoints)
        {
            hubEndpoint.MapHub(app);
        }
        return app;
    }
}