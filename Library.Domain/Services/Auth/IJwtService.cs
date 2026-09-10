namespace Library.Domain.Services.Auth; 
public interface IJwtService {
    string GerarToken(
        int cdUsuario,
        string nome,
        string login,
        string email,
        bool administrador
    );
}