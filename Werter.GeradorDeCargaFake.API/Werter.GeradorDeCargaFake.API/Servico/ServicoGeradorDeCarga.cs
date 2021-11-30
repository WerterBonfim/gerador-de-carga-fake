using Bogus;
using DapperLike;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Werter.GeradorDeCargaFake.API.Modelos;
using Werter.GeradorDeCargaFake.API.Parametros;

namespace Werter.GeradorDeCargaFake.API.Servico
{
    public sealed class ServicoGeradorDeCarga
    {
        private readonly Random _random = new(8675309);
        private const int QTD_MAXIMA_ITENS = 50_000;

        public void GerarCargaFake(CargaCommand command)
        {
            DeletarERecriar(command);
            EfetuarCarga(command);
        }

        private void EfetuarCarga(CargaCommand command)
        {
            using var conexao = new SqlConnection(command.StringDeConexão);
            

            while (command.QuantidadeDeRegistros > 0)
            {
                if (command.QuantidadeDeRegistros > QTD_MAXIMA_ITENS)
                {
                    var cargaGrande = GerarLista(command.NomeDaTabela, QTD_MAXIMA_ITENS);
                    conexao.BulkInsert(cargaGrande);
                    command.QuantidadeDeRegistros -= QTD_MAXIMA_ITENS;
                    continue;
                }

                var cargaLeve = GerarLista(command.NomeDaTabela, command.QuantidadeDeRegistros);
                conexao.BulkInsert(cargaLeve);
                break;
            }




        }

        private static void DeletarERecriar(CargaCommand command)
        {
            var dbFactory = new OrmLiteConnectionFactory(command.StringDeConexão, SqlServerDialect.Provider);
            using var db = dbFactory.Open();

            if (db.TableExists<Veiculo>())
                db.DeleteAll(typeof(Veiculo));

            db.CreateTable<Veiculo>();
        }

        private List<Veiculo> GerarLista(string nomeDaTabela, int quantidadeDeRegistros)
        {
            var fake = new Faker<Veiculo>()
                .RuleFor(x => x.Id, x => Guid.NewGuid())
                .RuleFor(x => x.DataHoraCadastro, x => x.Date.Between(new DateTime(2020, 1, 1), new DateTime(2021, 11, 1)))
                .RuleFor(x => x.Ano, x => x.Date.Between(new DateTime(1995, 1, 1), new DateTime(2021, 11, 1)).Year)
                .RuleFor(x => x.NumeroDocumento, x => x.Vehicle.Vin())
                .RuleFor(x => x.Nome, x => x.Person.FullName)
                .RuleFor(x => x.Modelo, x => x.Vehicle.Model())
                .RuleFor(x => x.Renavam, x => _random.Next(20000, 99999).ToString())
                .RuleFor(x => x.Valor, x => _random.Next(10000, 16000))
                .RuleFor(x => x.Nome, x => x.Person.FullName)

                ;


            return fake.Generate(quantidadeDeRegistros);
        }


    }
}