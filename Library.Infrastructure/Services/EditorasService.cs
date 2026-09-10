using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.Interfaces;
using Library.Domain.DTOs.Editoras;

namespace Library.Infrastructure.Services {
    public class EditorasService : ServiceBase<Editoras, PostRequest, PutRequest>, IEditorasService {
        public EditorasService(IEditorasRepository repository) : base(repository) { }

        protected override Editoras MapearParaCriacao(PostRequest request) {
            return new Editoras {
                descricao = request.descricao,
                razaoSocial = request.razaoSocial,
                cpfCnpj = request.cpfCnpj,
                email = request.email,
                celular = request.celular,
                telefone = request.telefone,
                nacionalidade = request.nacionalidade,
                siteOficial = request.siteOficial,
                fundacao = request.fundacao,
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

        protected override Editoras MapearParaAtualizacao(int id, PutRequest request, Editoras existente) {
            existente.descricao = request.descricao;
            existente.razaoSocial = request.razaoSocial;
            existente.cpfCnpj = request.cpfCnpj;
            existente.email = request.email;
            existente.celular = request.celular;
            existente.telefone = request.telefone;
            existente.nacionalidade = request.nacionalidade;
            existente.siteOficial = request.siteOficial;
            existente.fundacao = request.fundacao;
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
    }
}