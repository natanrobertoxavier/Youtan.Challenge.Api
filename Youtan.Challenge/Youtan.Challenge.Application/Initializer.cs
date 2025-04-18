using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TokenService.Manager.Controller;
using Youtan.Challenge.Application.Services;
using Youtan.Challenge.Application.UseCases.Client.Delete;
using Youtan.Challenge.Application.UseCases.Client.Login;
using Youtan.Challenge.Application.UseCases.Client.Recover.RecoverAll;
using Youtan.Challenge.Application.UseCases.Client.Register;
using Youtan.Challenge.Application.UseCases.Client.Update;
using Youtan.Challenge.Application.UseCases.User.Login;
using Youtan.Challenge.Application.UseCases.User.Register;

namespace Youtan.Challenge.Application;

public static class Initializer
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddLoggedUsers(services);
        AddUseCases(services);
        AddAdditionalKeyPassword(services, configuration);
        AddJWTToken(services, configuration);
        AddSerilog(services);
    }

    private static void AddLoggedUsers(IServiceCollection services)
    {
        services
            .AddScoped<ILoggedUser, LoggedUser>();
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services
            .AddScoped<IRegisterUserUseCase, RegisterUserUseCase>()
            .AddScoped<IUserLoginUseCase, UserLoginUseCase>()
            .AddScoped<IRegisterClientUseCase, RegisterClientUseCase>()
            .AddScoped<IClientLoginUseCase, ClientLoginUseCase>()
            .AddScoped<IDeleteClientUseCase, DeleteClientUseCase>()
            .AddScoped<IRecoverAllClientUseCase, RecoverAllClientUseCase>()
            .AddScoped<IUpdateClienteUseCase, UpdateClienteUseCase>();
    }

    private static void AddAdditionalKeyPassword(IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetRequiredSection("Settings:Password:AdditionalKeyPassword");
        services.AddScoped(option => new PasswordEncryptor(section.Value));
    }

    private static void AddJWTToken(IServiceCollection services, IConfiguration configuration)
    {
        var sectionLifeTime = configuration.GetRequiredSection("Settings:Jwt:LifeTimeTokenMinutes");
        var sectionKey = configuration.GetRequiredSection("Settings:Jwt:KeyToken");
        services.AddScoped(option => new TokenController(int.Parse(sectionLifeTime.Value), sectionKey.Value));
    }
    private static void AddSerilog(IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        services.AddSingleton(Log.Logger);
    }
}
