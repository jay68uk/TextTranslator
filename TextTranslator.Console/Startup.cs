using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TextTranslator.Library;

namespace TextTranslator.Console;

internal static class Startup
{
    internal static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices(
                (_, services) => services
                    .AddScoped<ITextServices, TextServices>());
    }
}