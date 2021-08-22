using Hospital.DataAccess;
using Hospital.DataAccess.ADO;
using Hospital.DataAccess.Entity;
using Hospital.DataAccess.EntityFramework;
using Hospital.DataAccess.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RepositoryADO.InterfaceForRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.MVCApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HospitalContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //RepositoryEFCore
            services.AddScoped<IRepository<Doctor>, DoctorRepository>();
            services.AddScoped<IRepository<Patient>, PatientRepository>();
            services.AddScoped<IRepository<MedicalHistory>, MedicalHistoryRepository>();
            services.AddScoped<IRepository<RegistrationCard>, RegistrationCardRepository>();


            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IMedicalHistoryRepository, MedicalHistoryRepository>();
            services.AddScoped<IRegistrationCardRepository, RegistrationCardRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //RepositoryADO
            services.AddScoped<BaseRepositoryADO<Doctor>>(option => new DoctorRepositoryADO(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<BaseRepositoryADO<Patient>>(option => new PatientRepositoryADO(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<BaseRepositoryADO<MedicalHistory>>(option => new MedicalHistoryRepositoryADO(Configuration.GetConnectionString("DefaultConnection"))); ;
            services.AddScoped<BaseRepositoryADO<RegistrationCard>>(option => new RegistrationCardRepositoryADO(Configuration.GetConnectionString("DefaultConnection"))); ;
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
