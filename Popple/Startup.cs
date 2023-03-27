// using HelloWorld.Data;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.EntityFrameworkCore;
// using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
// using Pomelo.EntityFrameworkCore.MySql.Storage;
// using Microsoft.AspNetCore.Authentication.Cookies;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;

// namespace HelloWorld
// {
//     public class Startup
//     {
//         public Startup(IConfiguration configuration)
//         {
//             Configuration = configuration;
//         }

//         public IConfiguration Configuration { get; }

//         #region snippet
//         #region snippet_ConfigureServices
//         public void ConfigureServices(IServiceCollection services)
//         {
//             services.AddRazorPages();

//             services.AddDbContext<TimecardContext>(
//                 dbContextOptions => dbContextOptions
//                     .UseMySql(Configuration.GetConnectionString("Default"), ServerVersion.AutoDetect(Configuration.GetConnectionString("Default")))
//                     .LogTo(Console.WriteLine, LogLevel.Information)
//                     .EnableSensitiveDataLogging()
//                     .EnableDetailedErrors()
//             );

//             services.AddSession(options =>
//             {
//                 options.IdleTimeout = TimeSpan.FromMinutes(30);
//                 options.Cookie.Name = ".Timecard.Session";
//                 options.Cookie.IsEssential = true;
//             });
//         }
//         #endregion

//         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//         {
//             if (env.IsDevelopment())
//             {
//                 app.UseDeveloperExceptionPage();
//             }
//             else
//             {
//                 app.UseExceptionHandler("/Error");
//                 app.UseHsts();
//             }

//             app.UseHttpsRedirection();
//             app.UseStaticFiles();

//             app.UseRouting();

//             app.UseAuthorization();

//             app.UseSession();

//             app.UseEndpoints(endpoints =>
//             {
//                 endpoints.MapRazorPages();
//             });
//         }
//         #endregion
//     }
// }
