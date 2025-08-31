using BibliotecaApi.Models;

namespace BibliotecaApi.Repositories.Autor
{
    public interface IAutorRepository
    {
        Task<List<AutorModel>> ListarAutores();
        Task<AutorModel?> BuscarAutorPorId(int idAutor);
        Task<AutorModel?> BuscarAutorPorIdLivro(int idLivro);
        Task<AutorModel> CriarAutor(AutorModel autor);
        Task<AutorModel> EditarAutor(AutorModel autor);
        Task<bool> ExcluirAutor(int idAutor);
    }
}
