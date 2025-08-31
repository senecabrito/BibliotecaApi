using BibliotecaApi.Models;

namespace BibliotecaApi.Repositories.Livro
{
    public interface ILivroRepository
    {
        Task<List<LivroModel>> ListarLivros();
        Task<LivroModel?> BuscarLivroPorId(int idLivro);
        Task<List<LivroModel>> BuscarLivrosPorIdAutor(int idAutor);
        Task<LivroModel> CriarLivro(LivroModel livro);
        Task<LivroModel> EditarLivro(LivroModel livro);
        Task<bool> ExcluirLivro(int idLivro);
    }
}
