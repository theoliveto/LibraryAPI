using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.Interfaces;
using Library.Domain.DTOs.Emprestimos;

namespace Library.Infrastructure.Services {
    public class EmprestimosService : ServiceBase<Emprestimos, PostRequest, PutRequest>, IEmprestimosService {
        public EmprestimosService(IEmprestimosRepository repository) : base(repository) { }

        protected override Emprestimos MapearParaCriacao(PostRequest request) {
            return new Emprestimos {
                pessoaId = request.pessoaId,
                exemplarId = request.exemplarId,
                emprestimo = request.emprestimo,
                previsaoDevolucao = request.previsaoDevolucao,
                devolucao = request.devolucao,
                status = request.status,
                observacoes = request.observacoes,
                ativo = request.ativo,
                cadastro = DateTime.Now
            };
        }

        protected override Emprestimos MapearParaAtualizacao(int id, PutRequest request, Emprestimos existente) {
            existente.pessoaId = request.pessoaId;
            existente.exemplarId = request.exemplarId;
            existente.emprestimo = request.emprestimo;
            existente.previsaoDevolucao = request.previsaoDevolucao;
            existente.devolucao = request.devolucao;
            existente.status = request.status;
            existente.observacoes = request.observacoes;
            existente.ativo = request.ativo;
            existente.atualizacao = DateTime.Now;

            return existente;
        }
    }
}