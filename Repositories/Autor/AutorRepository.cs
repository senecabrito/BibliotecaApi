using BibliotecaApi.Data;
using BibliotecaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Repositories.Autor
{
    public class AutorRepository : IAutorRepository
    {
        private readonly AppDbContext _context;

        public AutorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AutorModel>> ListarAutores()
        {
            return await _context.Autores.ToListAsync();
        }

        public async Task<AutorModel?> BuscarAutorPorId(int idAutor)
        {
            return await _context.Autores
                .FirstOrDefaultAsync(a => a.Id == idAutor);
        }

        public async Task<AutorModel?> BuscarAutorPorIdLivro(int idLivro)
        {
            var livro = await _context.Livros
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(l => l.Id == idLivro);

            return livro?.Autor; // Se o livro não existir, retorna null
        }

        public async Task<AutorModel> CriarAutor(AutorModel autor)
        {
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();
            return autor;
        }

        public async Task<AutorModel> EditarAutor(AutorModel autor)
        {
            _context.Autores.Update(autor);
            await _context.SaveChangesAsync();
            return autor;
        }

        public async Task<bool> ExcluirAutor(int idAutor)
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(a => a.Id == idAutor);
            if (autor == null)
                return false;

            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
