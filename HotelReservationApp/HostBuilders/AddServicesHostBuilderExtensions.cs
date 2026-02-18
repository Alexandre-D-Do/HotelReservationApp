using HotelReservationApp.Services.ReservationConflictValidators;
using HotelReservationApp.Services.ReservationCreators;
using HotelReservationApp.Services.ReservationDeleters;
using HotelReservationApp.Services.ReservationProviders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationApp.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                /// Register our services
                string connectionString = hostContext.Configuration.GetConnectionString("ReservationsDatabase");
                services.AddSingleton<IReservationProvider, DatabaseReservationProvider>((s) => new DatabaseReservationProvider(connectionString));
                services.AddSingleton<IReservationCreator, DatabaseReservationCreator>((s) => new DatabaseReservationCreator(connectionString));
                services.AddSingleton<IReservationDeleter, DatabaseReservationDeleter>((s) => new DatabaseReservationDeleter(connectionString));
                services.AddSingleton<IReservationConflictValidator, DatabaseReservationConflictValidator>((s) => new DatabaseReservationConflictValidator(connectionString));
            });
            return hostBuilder;
        }
    }
}
