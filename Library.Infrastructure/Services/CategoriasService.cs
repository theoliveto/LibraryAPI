using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.Interfaces;
using Library.Domain.DTOs.Categorias;

namespace Library.Infrastructure.Services {
    public class CategoriasService : ICategoriasService {
        private readonly ICategoriasRepository _repository;

        public CategoriasService(ICategoriasRepository repository) {
            _repository = repository;
        }

        public async Task<IEnumerable<Categorias>> ObterTodosAsync() {
            return await _repository.ObterTodosAsync();
        }

        public async Task<Categorias?> ObterPorIdAsync(int id) {
            if (id <= 0) {
                return null;
            }

            return await _repository.ObterPorIdAsync(id);
        }

        public async Task<int> CriarAsync(PostRequest request) {
            if (string.IsNullOrWhiteSpace(request.DS_CATEGORIA)) {
                throw new ArgumentException("O nome da categoria é obrigatório.");
            }

            var categorias = new Categorias() {
                DS_CATEGORIA = request.DS_CATEGORIA.Trim(),
                DS_OBSERVACOES = request.DS_OBSERVACOES,
                X_ATIVO = request.X_ATIVO,
                DT_CADASTRO = DateTime.Now,
                DT_ATUALIZACAO = DateTime.Now
            };

            return await _repository.CriarAsync(categorias);
        }

        public async Task<bool> AtualizarAsync(int id, PutRequest request) {
            if (id <= 0) {
                throw new ArgumentException("ID inválido.");
            }

            if (string.IsNullOrWhiteSpace(request.DS_CATEGORIA)) {
                throw new ArgumentException("O nome da categoria é obrigatório.");
            }

            var categorias = new Categorias() {
                CD_CATEGORIA = id,
                DS_CATEGORIA = request.DS_CATEGORIA.Trim(),
                DS_OBSERVACOES = request.DS_OBSERVACOES,
                X_ATIVO = request.X_ATIVO,
                DT_ATUALIZACAO = DateTime.Now
            };

            return await _repository.AtualizarAsync(categorias);
        }

        public async Task<bool> ExcluirAsync(int id) {
            if (id <= 0) {
                return false;
            }

            return await _repository.ExcluirAsync(id);
        }
    }
}