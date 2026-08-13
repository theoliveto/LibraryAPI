using Library.Domain.Services;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.DTOs.Pessoas;

namespace Library.Infrastructure.Services
{
    public class PessoasService : IPessoasService
    {
        private readonly IPessoasRepository _repository;

        public PessoasService(IPessoasRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Pessoas>> ObterTodosAsync()
        {
            return await _repository.ObterTodosAsync();
        }

        public async Task<Pessoas?> ObterPorIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _repository.ObterPorIdAsync(id);
        }

        public async Task<int> CriarAsync(PostRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.nome)) 
            { 
                throw new ArgumentException("O nome da pessoa é obrigatório.");
            }

            var pessoa = new Pessoas() {
                nome = request.nome.Trim(),
                cpfCnpj = request.cpfCnpj,
                nascimento = request.nascimento,
                email = request.email,
                genero = request.genero,
                cadastro = DateTime.Now
            };

            return await _repository.CriarAsync(pessoa);
        }

        public async Task<bool> AtualizarAsync(
            int id,
            PutRequest request)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            if (string.IsNullOrWhiteSpace(request.nome))
            {
                throw new ArgumentException("O nome da pessoa é obrigatório.");
            }
                
            var pessoa = new Pessoas() {
                id = id,
                nome = request.nome.Trim(),
                cpfCnpj = request.cpfCnpj,
                nascimento = request.nascimento,
                email = request.email,
                genero = request.genero,
                atualizacao = DateTime.Now
            };

            return await _repository.AtualizarAsync(pessoa);
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            return await _repository.ExcluirAsync(id);
        }
    }
}