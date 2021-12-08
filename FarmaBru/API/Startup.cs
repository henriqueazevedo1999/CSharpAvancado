using API.Application.Commands;
using BusinessLogicalLayer;
using BusinessLogicalLayer.Interfaces;
using ClienteAPI.Application.Handlers;
using FluentValidation;
using MediatR;
using MetaData.Entities;

namespace ClienteAPI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration; 
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddSingleton<IRepository<Cliente>, ClienteBLL>();
        AddMediatr(services);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();    
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    private static void AddMediatr(IServiceCollection services)
    {
        //const string applicationAssemblyName = "BusinessLogicalLayer";
        //var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);

        //AssemblyScanner
        //    .FindValidatorsInAssembly(assembly)
        //    .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));


        services.AddMediatR(typeof(Startup));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));
        services.AddScoped(typeof(IValidator<CadastraCommand>), typeof(ClienteValidator));
    }
}
