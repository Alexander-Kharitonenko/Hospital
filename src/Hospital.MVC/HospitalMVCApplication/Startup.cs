using Hospital.DataAccess;
using Hospital.DataAccess.Entity;
using Hospital.DataAccess.Interfaces;
using Hospital.DataAccess.RepositoryAdo;
using Hospital.DataAccess.RepositoryEntityFramework;
using Hospital.Services.ImplementServicesEntityFramework;
using Hospital.Services.InterfaceServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            //services.AddScoped<BaseRepositoryAdo<Doctor>>(option => new DoctorRepositoryAdo(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddScoped<BaseRepositoryAdo<Patient>>(option => new PatientRepositoryAdo(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddScoped<BaseRepositoryAdo<MedicalHistory>>(option => new MedicalHistoryRepositoryAdo(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddScoped<BaseRepositoryAdo<RegistrationCard>>(option => new RegistrationCardRepositoryAdo(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddScoped<IDoctorRepository>(option => new DoctorRepositoryAdo(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddScoped<IPatientRepository>(option => new PatientRepositoryAdo(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddScoped<IMedicalHistoryRepository>(option => new MedicalHistoryRepositoryAdo(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddScoped<IRegistrationCardRepository>(option => new RegistrationCardRepositoryAdo(Configuration.GetConnectionString("DefaultConnection")));

            //Services
            services.AddScoped<IDoctorServices, DoctorServices>();
            services.AddScoped<IMedicalHistoryServices, MedicalHistoryServices>();
            services.AddScoped<IPatientServices, PatientServices>();
            services.AddScoped<IRegistrationCardServices, RegistrationCardServices>();

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
                    pattern: "{controller=Home}/{action=GetAllCard}/{id?}");
            });
        }
    }
}
