using Library.Domain.Services;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.DTOs.Usuarios;

namespace Library.Infrastructure.Services; 
public class UsuariosService : IUsuariosService {
    private readonly IUsuariosRepository _repository;
    private readonly IPasswordHasher _passwordHasher;

    public UsuariosService(IUsuariosRepository repository, IPasswordHasher passwordHasher) {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }

    public async Task<IEnumerable<UsuarioResponse>> ObterTodosAsync() {
        var usuarios = await _repository.ObterTodosAsync();

        return usuarios.Select(MapearParaResponse);
    }

    public async Task<UsuarioResponse?> ObterPorIdAsync(int id) {
        if (id <= 0) {
            return null;
        }

        var usuario = await _repository.ObterPorIdAsync(id);

        if (usuario is null) {
            return null;
        }

        return MapearParaResponse(usuario);
    }

    public async Task<int> CriarAsync(PostRequest request) {
        if (string.IsNullOrWhiteSpace(request.DS_USUARIO)) {
            throw new ArgumentException("O nome do usuário é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(request.DS_SENHA)) {
            throw new ArgumentException("A senha é obrigatória.");
        }

        var senhaHash = _passwordHasher.Hash(request.DS_SENHA);

        var usuario = new Usuarios() {
            DS_USUARIO = request.DS_USUARIO.Trim(),
            DS_LOGIN = request.DS_LOGIN,
            DS_EMAIL = request.DS_EMAIL,
            DS_SENHA = senhaHash,
            DS_OBSERVACOES = request.DS_OBSERVACOES,
            X_ADMINISTRADOR = request.X_ADMINISTRADOR,
            X_ATIVO = request.X_ATIVO,
            DT_CADASTRO = DateTime.Now,
            DT_ATUALIZACAO = DateTime.Now
        };

        return await _repository.CriarAsync(usuario);
    }

    public async Task<bool> AtualizarAsync(int id, PutRequest request) {
        if (id <= 0) {
            throw new ArgumentException("ID inválido.");
        }

        if (string.IsNullOrWhiteSpace(request.DS_USUARIO)) {
            throw new ArgumentException("O nome do usuário é obrigatório.");
        }

        var usuario = new Usuarios() {
            CD_USUARIO = id,
            DS_USUARIO = request.DS_USUARIO.Trim(),
            DS_LOGIN = request.DS_LOGIN,
            DS_EMAIL = request.DS_EMAIL,
            DS_SENHA = request.DS_SENHA,
            DS_OBSERVACOES = request.DS_OBSERVACOES,
            X_ADMINISTRADOR = request.X_ADMINISTRADOR,
            X_ATIVO = request.X_ATIVO,
            DT_ATUALIZACAO = DateTime.Now
        };

        return await _repository.AtualizarAsync(usuario);
    }

    public async Task<bool> ExcluirAsync(int id) {
        if (id <= 0) {
            return false;
        }

        return await _repository.ExcluirAsync(id);
    }

    private static UsuarioResponse MapearParaResponse(Usuarios usuario) {
        return new UsuarioResponse {
            CD_USUARIO = usuario.CD_USUARIO,
            DS_USUARIO = usuario.DS_USUARIO,
            DS_LOGIN = usuario.DS_LOGIN,
            DS_EMAIL = usuario.DS_EMAIL,
            DS_OBSERVACOES = usuario.DS_OBSERVACOES,
            X_ATIVO = usuario.X_ATIVO,
            X_ADMINISTRADOR = usuario.X_ADMINISTRADOR,
            DT_CADASTRO = usuario.DT_CADASTRO,
            DT_ATUALIZACAO = usuario.DT_ATUALIZACAO
        };
    }
}