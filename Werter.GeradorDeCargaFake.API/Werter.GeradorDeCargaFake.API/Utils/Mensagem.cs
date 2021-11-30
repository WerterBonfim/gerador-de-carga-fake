namespace Werter.GeradorDeCargaFake.API.Utils
{
    public class Mensagem
    {
        public bool Sucesso { get; set; }
        public string Texto { get; set; }

        public Mensagem(bool sucesso, string texto)
        {
            Sucesso = sucesso;
            Texto = texto;
        }

    }
}
