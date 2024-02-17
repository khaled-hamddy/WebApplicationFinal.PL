
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using WebApplicationFinal.BLL.Interfaces;
using WebApplicationFinal.BLL.Repositories;
using WebApplicationFinal.DAL.Data;
using WebApplicationFinal.DAL.Models;
using WebApplicationFinal.PL.Extensions;
using WebApplicationFinal.PL.Helpers;

namespace WebApplicationFinal.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

         
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //  builder.Services.AddScoped<AppDbContext>();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                //options.UseSqlServer("Server = .; Database= MvcWebApp; Trusted_connection = True"); i should database connection
                //string in app.settings because it could be changed by chnging enviroment from dev to testing and anything changed by changing enviroment
                //i put it there because i have app.setting for dev and another class for testing and another one for stagging
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            }/*,ServiceLifetime.Scoped by default*/)/*.AddApplicationServices() ican add this services also here because this func return Services container*/ ;//scoped by default and give us more features than previous line
               
            builder .Services.AddApplicationServices(); //make it as extension method to add to container all services together instead of adding it here and also if it returns services container can add another service class in same line of method S5 B1 36:10
            builder.Services.AddAutoMapper(M=>M.AddProfile(new MappingProfiles()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new UserProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new RoleProfile()));

            //builder.Services.AddScoped<UserManager<ApplicationUser>>();
            //builder.Services.AddScoped<SignInManager<ApplicationUser>>();
            //builder.Services.AddScoped<RoleManager<IdentityRole>>(); instead of all this service line below register all of them
            builder.Services.AddIdentity<ApplicationUser,IdentityRole>(config =>
            {
                config.Password.RequiredUniqueChars = 2;
                config.Password.RequireDigit = true;
                config.Password.RequireNonAlphanumeric = true;
                config.Password.RequiredLength = 5;
                config.Password.RequireUppercase = true;
                config.Password.RequireLowercase = true;
                config.User.RequireUniqueEmail = true;
                config.Lockout.MaxFailedAccessAttempts = 3;
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                config.Lockout.AllowedForNewUsers = true;// for enabling lockout
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();//for registeering createasync update delete
            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn"; //configurations for default token
                config.ExpireTimeSpan= TimeSpan.FromMinutes(10); //time of token for expire
            });
         /*   builder.Services.AddAuthentication("Cookies");*/ //by default schema of tolen can be more than one schema but by default it is Cookies

            //builder.Services.AddAuthentication("Cookies")
            //    .AddCookie("Hamda", config =>
            //    {
            //        config.LoginPath = "/Account/SignIn"; // if controller has authorize over it and browser doesnt contain token it will go to this page to get token and its default value /Account/Login
            //        config.AccessDeniedPath = "/Home/Error"; // if not authorized to open this action
            //    });
            //.AddJwt("Baerer", config =>
            //{

            //})
            //.AddJwt("another", config =>
            //{

            //});

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection(); //if user write http instead of https
            app.UseStaticFiles(); // to serve static files in wwwroot because kestrel cant serve it

            app.UseRouting(); //url=urlbase+urlpath+sigma it takes url path to check it what it matches in route table 

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute( //routes which makes route table
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}