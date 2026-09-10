using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.Interfaces;
using Library.Domain.DTOs.Reservas;

namespace Library.Infrastructure.Services {
    public class ReservasService : ServiceBase<Reservas, PostRequest, PutRequest>, IReservasService {
        public ReservasService(IReservasRepository repository) : base(repository) { }

        protected override Reservas MapearParaCriacao(PostRequest request) {
            return new Reservas {
                pessoaId = request.pessoaId,
                exemplarId = request.exemplarId,
                reserva = request.reserva,
                prazo = request.prazo,
                validade = request.validade,
                status = request.status,
                observacoes = request.observacoes,
                ativo = request.ativo,
                cadastro = DateTime.Now
            };
        }

        protected override Reservas MapearParaAtualizacao(int id, PutRequest request, Reservas existente) {
            existente.pessoaId = request.pessoaId;
            existente.exemplarId = request.exemplarId;
            existente.reserva = request.reserva;
            existente.prazo = request.prazo;
            existente.validade = request.validade;
            existente.status = request.status;
            existente.observacoes = request.observacoes;
            existente.ativo = request.ativo;
            existente.atualizacao = DateTime.Now;

            return existente;
        }
    }
}