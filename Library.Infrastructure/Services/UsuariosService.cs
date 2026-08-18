using Library.Domain.Services;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.DTOs.Usuarios;

namespace Library.Infrastructure.Services; 
public class UsuariosService : IUsuariosService {
    private readonly IUsuariosRepository _repository;

    public UsuariosService(IUsuariosRepository repository) {
        _repository = repository;
    }

    public async Task<IEnumerable<Usuarios>> ObterTodosAsync() {
        return await _repository.ObterTodosAsync();
    }

    public async Task<Usuarios?> ObterPorIdAsync(int id) {
        if (id <= 0) {
            return null;
        }

        return await _repository.ObterPorIdAsync(id);
    }

    public async Task<int> CriarAsync(PostRequest request) {
        if (string.IsNullOrWhiteSpace(request.DS_USUARIO)) {
            throw new ArgumentException("O nome do usuário é obrigatório.");
        }

        var usuario = new Usuarios() {
            DS_USUARIO = request.DS_USUARIO.Trim(),
            DS_LOGIN = request.DS_LOGIN,
            DS_EMAIL = request.DS_EMAIL,
            DS_SENHA = request.DS_SENHA,
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
}