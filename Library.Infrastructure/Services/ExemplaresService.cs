using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.Interfaces;
using Library.Domain.DTOs.Exemplares;

namespace Library.Infrastructure.Services {
    public class ExemplaresService : ServiceBase<Exemplares, PostRequest, PutRequest>, IExemplaresService {
        public ExemplaresService(IExemplaresRepository repository) : base(repository) { }

        protected override Exemplares MapearParaCriacao(PostRequest request) {
            return new Exemplares {
                livroId = request.livroId,
                patrimonio = request.patrimonio,
                localizacao = request.localizacao,
                ean = request.ean,
                aquisicao = request.aquisicao,
                valorAquisicao = request.valorAquisicao,
                status = request.status,
                observacoes = request.observacoes,
                ativo = request.ativo,
                cadastro = DateTime.Now
            };
        }

        protected override Exemplares MapearParaAtualizacao(int id, PutRequest request, Exemplares existente) {
            existente.livroId = request.livroId;
            existente.patrimonio = request.patrimonio;
            existente.localizacao = request.localizacao;
            existente.ean = request.ean;
            existente.aquisicao = request.aquisicao;
            existente.valorAquisicao = request.valorAquisicao;
            existente.status = request.status;
            existente.observacoes = request.observacoes;
            existente.ativo = request.ativo;
            existente.atualizacao = DateTime.Now;

            return existente;
        }
    }
}