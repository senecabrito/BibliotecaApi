using BibliotecaApi.Models;
using BibliotecaApi.Models.Dto.Livro;

namespace BibliotecaApi.Services.Livro
{
    public interface ILivroService
    {
        Task<ResponseModel<List<LivroModel>>> ListarLivros();
        Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro);
        Task<ResponseModel<List<LivroModel>>> BuscarLivrosPorIdAutor(int idAutor);
        Task<ResponseModel<LivroModel>> CriarLivro(LivroCriacaoDto livroCriacaoDto);
        Task<ResponseModel<LivroModel>> EditarLivro(LivroEdicaoDto livroEdicaoDto);
        Task<ResponseModel<LivroModel>> ExcluirLivro(int idLivro);
    }
}