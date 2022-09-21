using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region Services
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));

            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IPropiedadesService, PropiedadesService>();

            services.AddTransient<ITipoPropiedadService, TipoPropiedadesService>();

            services.AddTransient<ITipoVentaService, VentaService>();

            services.AddTransient<ITipoMejoraService, MejorasService>();

            services.AddTransient<IFavoriteService, FavoriteService>();

            #endregion
        }
    }
}
