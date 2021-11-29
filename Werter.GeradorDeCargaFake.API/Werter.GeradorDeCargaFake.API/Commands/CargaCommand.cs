using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace Werter.GeradorDeCargaFake.API.Parametros
{
    public class CargaCommand
    {
        private ValidationResult _resultadoDaValidacao;
        public string StringDeConexão { get; set; }
        public string NomeDaTabela { get; set; }
        public int QuantidadeDeRegistros { get; set; }

        public bool EstaValido()
        {
            _resultadoDaValidacao = new Validacao().Validate(this);
            return _resultadoDaValidacao.IsValid;
        }

        public IList<string> ListarErros()
        {
            return _resultadoDaValidacao.Errors
                .Select(x => x.ErrorMessage)
                .ToList();
        }


        private class Validacao : AbstractValidator<CargaCommand>
        {
            public Validacao()
            {
                RuleFor(x => x.StringDeConexão)
                    .NotEmpty()
                    .WithMessage($"O campo ${nameof(StringDeConexão)} não pode ser um valor vazio");
                
                RuleFor(x => x.NomeDaTabela)
                    .NotEmpty()
                    .WithMessage($"O campo ${nameof(NomeDaTabela)} não pode ser um valor vazio");
                
                RuleFor(x => x.QuantidadeDeRegistros)
                    .GreaterThan(0)
                    .WithMessage($"O campo ${nameof(QuantidadeDeRegistros)} deve ser maior que 0");
            }
        }
        
    }
}