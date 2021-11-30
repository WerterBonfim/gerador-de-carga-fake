using System;

namespace Werter.GeradorDeCargaFake.API.Modelos
{
    public class Veiculo
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Renavam { get; set; }
        public string NumeroDeSerie { get; set; }
        public string NumeroDocumento { get; set; }
        public string Placa { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataHoraCadastro { get; set; }
    }
}