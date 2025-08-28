using BibliotecaApi.Models;

namespace BibliotecaApi.Services.Autor
{
    public interface IAutor
    {
        Task<ResponseModel<List<AutorModel>>> ListarAutores();
        Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor);
        Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro);
    }
}