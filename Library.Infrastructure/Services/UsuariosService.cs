using Library.Domain.Services;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.DTOs.Usuarios;
using Library.Domain.Interfaces.Auth;

namespace Library.Infrastructure.Services { 
    public class UsuariosService : ServiceBase<Usuarios, PostRequest, PutRequest>, IUsuariosService {
        private readonly IUsuariosRepository _repository;
        private readonly IPasswordHasher _passwordHasher;

        public UsuariosService(IUsuariosRepository repository, IPasswordHasher passwordHasher) : base(repository) {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        protected override Usuarios MapearParaCriacao(PostRequest request) {
            return new Usuarios {
                descricao = request.descricao,
                login = request.login,
                email = request.email,
                senha = _passwordHasher.Hash(request.senha),
                perfil = request.perfil,
                avatar = request.avatar,
                administrador = request.administrador,
                tentativasLogin = request.tentativasLogin,
                bloqueioLogin = request.bloqueioLogin,
                observacoes = request.observacoes,
                ativo = request.ativo,
                cadastro = DateTime.Now
            };
        }

        protected override Usuarios MapearParaAtualizacao(int id, PutRequest request, Usuarios existente) {
            existente.descricao = request.descricao;
            existente.login = request.login;
            existente.email = request.email;
            existente.senha = _passwordHasher.Hash(request.senha);
            existente.perfil = request.perfil;
            existente.avatar = request.avatar;
            existente.administrador = request.administrador;
            existente.tentativasLogin = request.tentativasLogin;
            existente.bloqueioLogin = request.bloqueioLogin;
            existente.observacoes = request.observacoes;
            existente.ativo = request.ativo;
            existente.atualizacao = DateTime.Now;

            return existente;
        }

        private static UsuarioResponse MapearParaResponse(Usuarios usuario) {
            return new UsuarioResponse {
                id = usuario.id,
                descricao = usuario.descricao,
                login = usuario.login,
                email = usuario.email,
                perfil = usuario.perfil,
                observacoes = usuario.observacoes,
                ativo = usuario.ativo,
                administrador = usuario.administrador,
                cadastro = usuario.cadastro,
                atualizacao = usuario.atualizacao
            };
        }
    }
}