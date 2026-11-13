
using Microsoft.EntityFrameworkCore;
using Plot.WebApi.Context;
using Plot.WebApi.Interfaces;
using Plot.WebApi.Models;

namespace Plot.WebApi.Repositories;

public class FilmeRepository : IFilmeRepository
{
    private readonly AppDbContext _context;

    public FilmeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Cadastrar(Filme filme)
    {
        await _context.Filme.AddAsync(filme);
        await _context.SaveChangesAsync();
    }

    public async Task Atualizar(int id, Filme filme)
{
    var filmeExistente = await _context.Filme
        .Include(f => f.IdGenero)
        .FirstOrDefaultAsync(f => f.IdFilme == id);

    if (filmeExistente != null)
    {
        filmeExistente.Titulo = filme.Titulo;
        filmeExistente.IdGenero = filme.IdGenero;

        await _context.SaveChangesAsync();
    }
}
    public async Task Excluir(int id)
    {
        var filmeBuscado = await _context.Filme.FindAsync(id);

        if (filmeBuscado != null)
        {
            _context.Filme.Remove(filmeBuscado);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Filme?> BuscarPorId(int id)
    {
        return await _context.Filme
            .AsNoTracking().Include(f => f.IdGenero)
            .FirstOrDefaultAsync(f => f.IdFilme == id);
    }

    public async Task<(List<Filme> filmes, int total)> ListarPaginado(
        int pagina = 1,
        int tamanhoPagina = 10,
        string? busca = null)
    {
        var query = _context.Filme.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(busca))
        {
            query = query.Where(f => f.Titulo.Contains(busca));
        }

        var total = await query.CountAsync();

        var filmes = await query
            .OrderBy(f => f.Titulo)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();

        return (filmes, total);
    }
}
