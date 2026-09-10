using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.Interfaces;
using Library.Domain.DTOs.Autores;

namespace Library.Infrastructure.Services {
    public class AutoresService : ServiceBase<Autores, PostRequest, PutRequest>, IAutoresService {
        public AutoresService(IAutoresRepository repository) : base(repository) { }

        protected override Autores MapearParaCriacao(PostRequest request) {
            return new Autores {
                descricao = request.descricao,
                nascimento = request.nascimento,
                falecimento = request.falecimento,
                nacionalidade = request.nacionalidade,
                genero = request.genero,
                siteOficial = request.siteOficial,
                observacoes = request.observacoes,
                ativo = request.ativo,
                cadastro = DateTime.Now
            };
        }

        protected override Autores MapearParaAtualizacao(int id, PutRequest request, Autores existente) {
            existente.descricao = request.descricao;
            existente.nascimento = request.nascimento;
            existente.falecimento = request.falecimento;
            existente.nacionalidade = request.nacionalidade;
            existente.genero = request.genero;
            existente.siteOficial = request.siteOficial;
            existente.observacoes = request.observacoes;
            existente.ativo = request.ativo;
            existente.atualizacao = DateTime.Now;

            return existente;
        }
    }
}