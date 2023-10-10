using GithubSlackTrigger.Service.Interface;
using GithubSlackTrigger.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GithubSlackTrigger.DAL.Interface;
using GithubSlackTrigger.DAL;

var host = new HostBuilder()
    .ConfigureAppConfiguration(configurationBuilder =>
    {
    })
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        //services.AddSingleton<IConfiguration>(provider => configuration);
        services.AddTransient<IRequestValidator,   RequestValidator>();
        services.AddTransient<ILogErrorService,    LogErrorService>();
        services.AddTransient<ILogService,         LogService>();
        services.AddTransient<ILogErrorRepository, LogErrorRepository>();
        services.AddTransient<ILogRepository,      LogRepository>();
        services.AddTransient<ISendSlackMessage,   SendSlackMessage>();
    })
    .Build();

host.Run();
