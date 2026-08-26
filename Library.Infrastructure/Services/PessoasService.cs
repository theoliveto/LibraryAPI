using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.Interfaces;
using Library.Domain.DTOs.Pessoas;

namespace Library.Infrastructure.Services {
    public class PessoasService : IPessoasService {
        private readonly IPessoasRepository _repository;

        public PessoasService(IPessoasRepository repository) {
            _repository = repository;
        }

        public async Task<IEnumerable<Pessoas>> ObterTodosAsync() {
            return await _repository.ObterTodosAsync();
        }

        public async Task<Pessoas?> ObterPorIdAsync(int id) {
            if (id <= 0) {
                return null;
            }

            return await _repository.ObterPorIdAsync(id);
        }

        public async Task<int> CriarAsync(PostRequest request) {
            if (string.IsNullOrWhiteSpace(request.DS_PESSOA)) {
                throw new ArgumentException("O nome da pessoa é obrigatório.");
            }

            var pessoa = new Pessoas() {
                DS_PESSOA = request.DS_PESSOA.Trim(),
                NR_CPF = request.NR_CPF,
                DS_EMAIL = request.DS_EMAIL,
                NR_TELEFONE = request.NR_TELEFONE,
                DT_NASCIMENTO = request.DT_NASCIMENTO,
                DS_GENERO = request.DS_GENERO,
                DS_OBSERVACOES = request.DS_OBSERVACOES,
                NR_CEP = request.NR_CEP,
                DS_ENDERECO = request.DS_ENDERECO,
                DS_CIDADE = request.DS_CIDADE,
                DS_ESTADO = request.DS_ESTADO,
                DS_BAIRRO = request.DS_BAIRRO,
                NR_NUMERO = request.NR_NUMERO,
                DS_COMPLEMENTO = request.DS_COMPLEMENTO,
                X_ATIVO = request.X_ATIVO,
                DT_CADASTRO = DateTime.Now,
                DT_ATUALIZACAO = DateTime.Now
            };

            return await _repository.CriarAsync(pessoa);
        }

        public async Task<bool> AtualizarAsync(int id, PutRequest request) {
            if (id <= 0) {
                throw new ArgumentException("ID inválido.");
            }

            if (string.IsNullOrWhiteSpace(request.DS_PESSOA)) {
                throw new ArgumentException("O nome da pessoa é obrigatório.");
            }

            var pessoa = new Pessoas() {
                CD_PESSOA = id,
                DS_PESSOA = request.DS_PESSOA.Trim(),
                NR_CPF = request.NR_CPF,
                DS_EMAIL = request.DS_EMAIL,
                NR_TELEFONE = request.NR_TELEFONE,
                DT_NASCIMENTO = request.DT_NASCIMENTO,
                DS_GENERO = request.DS_GENERO,
                DS_OBSERVACOES = request.DS_OBSERVACOES,
                NR_CEP = request.NR_CEP,
                DS_ENDERECO = request.DS_ENDERECO,
                DS_CIDADE = request.DS_CIDADE,
                DS_ESTADO = request.DS_ESTADO,
                DS_BAIRRO = request.DS_BAIRRO,
                NR_NUMERO = request.NR_NUMERO,
                DS_COMPLEMENTO = request.DS_COMPLEMENTO,
                X_ATIVO = request.X_ATIVO,
                DT_ATUALIZACAO = DateTime.Now
            };

            return await _repository.AtualizarAsync(pessoa);
        }

        public async Task<bool> ExcluirAsync(int id) {
            if (id <= 0) {
                return false;
            }

            return await _repository.ExcluirAsync(id);
        }

        public async Task<bool> VerificarCpfAsync(string cpf, int? id) {
            if (id <= 0) {
                throw new ArgumentException("ID inválido.");
            }

            if (string.IsNullOrWhiteSpace(cpf)) {
                throw new ArgumentException("O CPF da pessoa é obrigatório.");
            }

            return await _repository.VerificarCpfAsync(cpf, id);
        }

        public async Task<bool> VerificarEmailAsync(string email, int? id) {
            if (id <= 0) {
                throw new ArgumentException("ID inválido.");
            }

            if (string.IsNullOrWhiteSpace(email)) {
                throw new ArgumentException("O E-mail da pessoa é obrigatório.");
            }

            return await _repository.VerificarEmailAsync(email, id);
        }
    }
}