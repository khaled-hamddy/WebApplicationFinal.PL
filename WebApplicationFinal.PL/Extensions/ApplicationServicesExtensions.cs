using WebApplicationFinal.BLL.Interfaces;
using WebApplicationFinal.BLL.Repositories;
using WebApplicationFinal.BLL;

namespace WebApplicationFinal.PL.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        }

    }
}
