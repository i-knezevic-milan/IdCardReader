using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using IdCardReaderApi.EID;

namespace IdCardReaderApi
{
    public class Startup
    {
        private ILogger<Startup> _logger { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddControllers();

            services.AddSingleton<IIdCardReaderWrapper, IdCardReaderWrapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime, ILogger<Startup> logger)
        {
            _logger = logger;

            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyHeader()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            hostApplicationLifetime.ApplicationStarted.Register(OnStarted);
            hostApplicationLifetime.ApplicationStopped.Register(OnStopped);
        }

        private void OnStarted()
        {
            int status = IdCardReader.EidStartup(IdCardReader.EID_N_API_VERSION);
            if (status != IdCardReader.EID_OK)
            {
                IdCardReaderException e = new IdCardReaderException() { Method = "EidStartup", Status = status, StatusMessage = IdCardReader.EidMessage(status) };
                _logger.LogError(e, e.StatusMessage);
            }
        }

        private void OnStopped()
        {
            IdCardReader.EidCleanup();
        }
    }
}
