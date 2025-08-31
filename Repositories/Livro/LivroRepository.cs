using BibliotecaApi.Data;
using BibliotecaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Repositories.Livro
{
    public class LivroRepository : ILivroRepository
    {
        private readonly AppDbContext _context;

        public LivroRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LivroModel>> ListarLivros()
        {
            return await _context.Livros.ToListAsync();
        }

        public async Task<LivroModel?> BuscarLivroPorId(int idLivro)
        {
            return await _context.Livros
                .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);
        }

        public async Task<List<LivroModel>> BuscarLivrosPorIdAutor(int idAutor)
        {
            return await _context.Livros
                .Include(a => a.Autor)
                .Where(livroBanco => livroBanco.Autor != null && livroBanco.Autor.Id == idAutor)
                .ToListAsync();
        }

        public async Task<LivroModel> CriarLivro(LivroModel livro)
        {
            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();
            return livro;
        }

        public async Task<LivroModel> EditarLivro(LivroModel livro)
        {
            _context.Livros.Update(livro);
            await _context.SaveChangesAsync();
            return livro;
        }

        public async Task<bool> ExcluirLivro(int idLivro)
        {
            var livro = await _context.Livros.FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);
            if (livro == null)
                return false;

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
