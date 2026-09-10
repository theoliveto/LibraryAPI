using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.Interfaces;
using Library.Domain.DTOs.Categorias;

namespace Library.Infrastructure.Services {
    public class CategoriasService : ServiceBase<Categorias, PostRequest, PutRequest>, ICategoriasService {
        public CategoriasService(ICategoriasRepository repository) : base(repository) { }

        protected override Categorias MapearParaCriacao(PostRequest request) {
            return new Categorias {
                descricao = request.descricao,
                observacoes = request.observacoes,
                ativo = request.ativo,
                cadastro = DateTime.Now
            };
        }

        protected override Categorias MapearParaAtualizacao(int id, PutRequest request, Categorias existente) {
            existente.descricao = request.descricao;
            existente.observacoes = request.observacoes;
            existente.ativo = request.ativo;
            existente.atualizacao = DateTime.Now;

            return existente;
        }
    }
}