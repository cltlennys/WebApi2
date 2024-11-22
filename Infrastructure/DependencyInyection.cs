﻿using Core.Interfaces.Repositories;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text;

namespace Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddRepositories();
        services.AddDatabase(configuration);
        services.AddMapping();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IInstallmentRepository, InstallmentRepository>();
        services.AddScoped<IApprovedLoan, ApprovedLoanRepository>();
        services.AddScoped<ILoanRequestRepository, LoanRequestRepository>();
        services.AddScoped<IPaymentInstallmentRepository, PaymentInstallmentRepository>();

        return services;
    }
    public static IServiceCollection AddDatabase(this IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Bootcamp");

        services.AddDbContext<AplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return services;
    }
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }

}
