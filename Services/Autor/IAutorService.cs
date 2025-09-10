using BibliotecaApi.Models;
using BibliotecaApi.Models.Dto.Autor;

namespace BibliotecaApi.Services.Autor
{
    public interface IAutorService
    {
        Task<ResponseModel<List<AutorModel>>> ListarAutores();
        Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor);
        Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro);
        Task<ResponseModel<AutorModel>> CriarAutor(AutorCriacaoDto autorCriacaoDto);
        Task<ResponseModel<AutorModel>> EditarAutor(AutorEdicaoDto autorEdicaoDto);
        Task<ResponseModel<AutorModel>> ExcluirAutor(int idAutor);
    }
}