using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ProcMon.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		private IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			//if (env.IsDevelopment()) {
			app.UseDeveloperExceptionPage();
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProcMon.Api v1"));
			//}

			app.UseRouting();
			//调用 UseCors 扩展方法并指定 _myAllowSpecificOrigins CORS 策略。 UseCors 添加 CORS 中间件。 必须将对的调用 UseCors 置于之后 UseRouting 但在之前 UseAuthorization 。 有关详细信息，请参阅 中间件顺序。
			app.UseCors(x => {
				x.AllowAnyHeader();
				x.AllowAnyMethod();
				x.AllowAnyOrigin();
			});

			app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers().AddNewtonsoftJson();
			services.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProcMon.Api", Version = "v1" });
			});
		}
	}
}