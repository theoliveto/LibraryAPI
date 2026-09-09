using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.Interfaces;
using Library.Domain.DTOs.Autor;

namespace Library.Infrastructure.Services {
    public class AutoresService : IAutorService {
        private readonly IAutorRepository _repository;

        public AutoresService(IAutorRepository repository) {
            _repository = repository;
        }

        public async Task<IEnumerable<Autor>> ObterTodosAsync() {
            return await _repository.ObterTodosAsync();
        }

        public async Task<Autor?> ObterPorIdAsync(int id) {
            if (id <= 0) {
                return null;
            }

            return await _repository.ObterPorIdAsync(id);
        }

        public async Task<int> CriarAsync(PostRequest request) {
            if (string.IsNullOrWhiteSpace(request.DS_AUTOR)) {
                throw new ArgumentException("O nome do(a) autor(a) é obrigatório.");
            }

            var autor = new Autor() {
                DS_AUTOR = request.DS_AUTOR.Trim(),
                DT_NASCIMENTO = request.DT_NASCIMENTO,
                DT_FALECIMENTO = request.DT_FALECIMENTO,
                DS_PAIS = request.DS_PAIS,
                DS_GENERO = request.DS_GENERO,
                DS_SITE_OFICIAL = request.DS_SITE_OFICIAL,
                IM_FOTO = request.IM_FOTO,
                DS_OBSERVACOES = request.DS_OBSERVACOES,
                X_ATIVO = request.X_ATIVO,
                DT_CADASTRO = DateTime.Now,
                DT_ATUALIZACAO = DateTime.Now
            };

            return await _repository.CriarAsync(autor);
        }

        public async Task<bool> AtualizarAsync(int id, PutRequest request) {
            if (id <= 0) {
                throw new ArgumentException("ID inválido.");
            }

            if (string.IsNullOrWhiteSpace(request.DS_AUTOR)) {
                throw new ArgumentException("O nome do(a) autor(a) é obrigatório.");
            }

            var autor = new Autor() {
                CD_AUTOR = id,
                DS_AUTOR = request.DS_AUTOR.Trim(),
                DT_NASCIMENTO = request.DT_NASCIMENTO,
                DT_FALECIMENTO = request.DT_FALECIMENTO,
                DS_PAIS = request.DS_PAIS,
                DS_GENERO = request.DS_GENERO,
                DS_SITE_OFICIAL = request.DS_SITE_OFICIAL,
                IM_FOTO = request.IM_FOTO,
                DS_OBSERVACOES = request.DS_OBSERVACOES,
                X_ATIVO = request.X_ATIVO,
                DT_ATUALIZACAO = DateTime.Now
            };

            return await _repository.AtualizarAsync(autor);
        }

        public async Task<bool> ExcluirAsync(int id) {
            if (id <= 0) {
                return false;
            }

            return await _repository.ExcluirAsync(id);
        }
    }
}