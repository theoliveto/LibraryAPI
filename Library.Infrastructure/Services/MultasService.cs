using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.Interfaces;
using Library.Domain.DTOs.Multas;

namespace Library.Infrastructure.Services {
    public class MultasService : ServiceBase<Multas, PostRequest, PutRequest>, IMultasService {
        public MultasService(IMultasRepository repository) : base(repository) { }

        protected override Multas MapearParaCriacao(PostRequest request) {
            return new Multas {
                pessoaId = request.pessoaId,
                emprestimoId = request.emprestimoId,
                motivo = request.motivo,
                valor = request.valor,
                data = request.data,
                pagamento = request.pagamento,
                status = request.status,
                observacoes = request.observacoes,
                ativo = request.ativo,
                cadastro = DateTime.Now
            };
        }

        protected override Multas MapearParaAtualizacao(int id, PutRequest request, Multas existente) {
            existente.pessoaId = request.pessoaId;
            existente.emprestimoId = request.emprestimoId;
            existente.motivo = request.motivo;
            existente.valor = request.valor;
            existente.data = request.data;
            existente.pagamento = request.pagamento;
            existente.status = request.status;
            existente.observacoes = request.observacoes;
            existente.ativo = request.ativo;
            existente.atualizacao = DateTime.Now;

            return existente;
        }
    }
}