using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using laboratorio.Infraestructura;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using laboratorio.Dominio.repositorios;
using laboratorio.Infraestructura.persistencia.implementaciones;
using laboratorio.modelos;
using System.Reflection;

namespace laboratorio.IOC
{
    public static class Dependencia
    {

        public static void InyectarDependencia(this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddDbContext<LaboratorioContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("conexionBD")));
            services.AddControllers().AddJsonOptions(
                opt => { opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; }
                );

            services.AddScoped<IProveedorRepositorio, ProveedorRepositorio>();
            // Registro de RepositorioGenerico<Producto>
            //services.AddScoped<IRepositorioGenerico<Producto>, RepositorioGenerico<Producto>>();

            // Registro de RepositorioGenerico<Proveedor>
            //services.AddScoped<IRepositorioGenerico<Proveedor>, RepositorioGenerico<Proveedor>>();


            // Escaneo de los ensamblajes en busca de implementaciones de IRepositorioGenerico<T>
            var types = Assembly.GetAssembly(typeof(RepositorioGenerico<>))
                               .GetTypes()
                               .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepositorioGenerico<>)));

            // Registro de las implementaciones encontradas
            foreach (var type in types)
            {
                var genericInterface = type.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepositorioGenerico<>));
                services.AddScoped(genericInterface, type);
            }

        }
    }
}
