using Microsoft.Extensions.DependencyInjection;
using Werter.GeradorDeCargaFake.API.Servico;

namespace Werter.GeradorDeCargaFake.API.Configuracoes
{
    public static class DIConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<ServicoGeradorDeCarga>();

            return services;
        }
    }
}
