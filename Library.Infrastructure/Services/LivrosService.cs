using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.Interfaces;
using Library.Domain.DTOs.Livros;

namespace Library.Infrastructure.Services {
    public class LivrosService : ServiceBase<Livros, PostRequest, PutRequest>, ILivrosService {
        public LivrosService(ILivrosRepository repository) : base(repository) { }

        protected override Livros MapearParaCriacao(PostRequest request) {
            return new Livros {
                descricao = request.descricao,
                subtitulo = request.subtitulo,
                autorId = request.autorId,
                editoraId = request.editoraId,
                categoriaId = request.categoriaId,
                isbn = request.isbn,
                idioma = request.idioma,
                edicao = request.edicao,
                paginas = request.paginas,
                publicacao = request.publicacao,
                sinopse = request.sinopse,
                imagemCapa = request.imagemCapa,
                observacoes = request.observacoes,
                ativo = request.ativo,
                cadastro = DateTime.Now
            };
        }

        protected override Livros MapearParaAtualizacao(int id, PutRequest request, Livros existente) {
            existente.descricao = request.descricao;
            existente.subtitulo = request.subtitulo;
            existente.autorId = request.autorId;
            existente.editoraId = request.editoraId;
            existente.categoriaId = request.categoriaId;
            existente.isbn = request.isbn;
            existente.idioma = request.idioma;
            existente.edicao = request.edicao;
            existente.paginas = request.paginas;
            existente.publicacao = request.publicacao;
            existente.sinopse = request.sinopse;
            existente.imagemCapa = request.imagemCapa;
            existente.categoriaId = request.categoriaId;
            existente.observacoes = request.observacoes;
            existente.ativo = request.ativo;
            existente.atualizacao = DateTime.Now;

            return existente;
        }
    }
}