using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SzkolenieTechniczne2.AutoSerwis.Common.Repositories;
using SzkolenieTechniczne2.AutoSerwis.Domain.Command.Client.Create;
using SzkolenieTechniczne2.AutoSerwis.Domain.Command.Client.Delete;
using SzkolenieTechniczne2.AutoSerwis.Domain.Command.Client.Update;
using SzkolenieTechniczne2.AutoSerwis.Domain.Command.RepairOrders.Register;
using SzkolenieTechniczne2.AutoSerwis.Domain.Query.Client.GetAllClientsQuery;
using SzkolenieTechniczne2.AutoSerwis.Domain.Query.Client.GetClientCategories;
using SzkolenieTechniczne2.AutoSerwis.Domain.Query.Client.GetClientQuery;
using SzkolenieTechniczne2.AutoSerwis.Domain.Repositories;
using SzkolenieTechniczne2.AutoSerwis.Infrastructure;
using SzkolenieTechniczne2.AutoSerwis.Infrastructure.Repository;

namespace AutoSerwis.Mvc.UI
{
    public static class AutoSerwisDependencyInjection
    {
        public static IServiceCollection AutoSerwisAddApplication(this IServiceCollection services)
        {
            services.AddScoped<IClientsRepository, ClientsRepository>();
            services.AddDbContext<AutoSerwisDbContext, AutoSerwisDbContext>();
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateClientCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteClientCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateClientCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(RegisterRepairOrderCommand).Assembly);
                // cfg.RegisterServicesFromAssembly(typeof(AddRepairHistoryCommand).Assembly);

                cfg.RegisterServicesFromAssembly(typeof(GetAllClientsQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetClientQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetClientCategoriesQuery).Assembly);
            });
            return services;
        }
    }
}