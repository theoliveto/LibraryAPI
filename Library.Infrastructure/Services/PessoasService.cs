using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.Interfaces;
using Library.Domain.DTOs.Pessoas;

namespace Library.Infrastructure.Services {
    public class PessoasService : ServiceBase<Pessoas, PostRequest, PutRequest>, IPessoasService {
        private readonly IPessoasRepository _repository;

        public PessoasService(IPessoasRepository repository) : base(repository) {
            _repository = repository;
        }

        protected override Pessoas MapearParaCriacao(PostRequest request) {
            return new Pessoas {
                descricao = request.descricao,
                razaoSocial = request.razaoSocial,
                usuarioId = request.usuarioId,
                cpfCnpj = request.cpfCnpj,
                email = request.email,
                celular = request.celular,
                telefone = request.telefone,
                nascimento = request.nascimento,
                genero = request.genero,
                cep = request.cep,
                endereco = request.endereco,
                cidade = request.cidade,
                estado = request.estado,
                bairro = request.bairro,
                numero = request.numero,
                complemento = request.complemento,
                observacoes = request.observacoes,
                ativo = request.ativo,
                cadastro = DateTime.Now
            };
        }

        protected override Pessoas MapearParaAtualizacao(int id, PutRequest request, Pessoas existente) {
            existente.descricao = request.descricao;
            existente.razaoSocial = request.razaoSocial;
            existente.usuarioId = request.usuarioId;
            existente.cpfCnpj = request.cpfCnpj;
            existente.email = request.email;
            existente.celular = request.celular;
            existente.telefone = request.telefone;
            existente.nascimento = request.nascimento;
            existente.genero = request.genero;
            existente.cep = request.cep;
            existente.endereco = request.endereco;
            existente.cidade = request.cidade;
            existente.estado = request.estado;
            existente.bairro = request.bairro;
            existente.numero = request.numero;
            existente.complemento = request.complemento;
            existente.observacoes = request.observacoes;
            existente.ativo = request.ativo;
            existente.atualizacao = DateTime.Now;

            return existente;
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