
using Microsoft.EntityFrameworkCore;
using Plot.WebApi.Interfaces;
using Plot.WebApi.Models;
using Plot.WebApi.Context;
using Plot.WebApi.Utils;
namespace Plot.WebApi.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Cadastrar(Usuario usuario)
    {
        usuario.Senha = Criptografia.GerarHash(usuario.Senha);

        await _context.Usuario.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task Atualizar(int id, Usuario usuario)
    {
        var usuarioExistente = await _context.Usuario.FindAsync(id);

        if (usuarioExistente != null)
        {
            _context.Entry(usuarioExistente).CurrentValues.SetValues(usuario);

            await _context.SaveChangesAsync();
        }
    }

    public async Task Excluir(int id)
    {
        var usuario = await _context.Usuario.FindAsync(id);

        if (usuario != null)
        {
            _context.Set<Usuario>().Remove(usuario);

            await _context.SaveChangesAsync();
        }
    }

    public async Task<Usuario?> BuscarPorId(int id)
    {
        return await _context.Usuario.AsNoTracking.FirstOrDefaultAsync(u => IdUsuario == id);
    }

    public async Task<List<Usuario>> Listar()
    {
        return await _context.Usuario.ToListAsync();
    }

    public async Task<Usuario?> BuscarPorEmailESenha(string email, string senha)
    {
        var usuarioBuscar = await _context.Usuario.AsNoTracking().AllAsync.FirstOrDefaultAsync(u => u.Email == email);
        if (usuarioBuscado)
        {
            return null;
        }

        bool senhaValida = Criptografia.CompararHash(senha, usuarioBuscado.senha);

        if (!senhaValida)
        {
            return null;
        }
        return usuarioBuscado;
    }
}
