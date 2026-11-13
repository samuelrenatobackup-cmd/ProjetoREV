using Plot.WebApi.Models;

namespace Plot.WebApi.Interfaces;

public interface IUsuarioRepository
{
    Task Cadastrar(Usuario usuario);
    Task Atualizar(int id, Usuario usuario);
    Task Excluir(int id);

    Task<Usuario?> BuscarPorId(int id);
    Task<List<Usuario>> Listar();
    Task<Usuario?> BuscarPorEmailESenha(string email, string senha);
}