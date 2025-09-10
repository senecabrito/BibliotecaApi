using BibliotecaApi.Models;
using BibliotecaApi.Models.Dto.Autor;
using BibliotecaApi.Repositories.Autor;

namespace BibliotecaApi.Services.Autor
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;

        public AutorService(IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
            try
            {
                var autores = await _autorRepository.ListarAutores() ?? new List<AutorModel>();

                if (!autores.Any())
                {
                    resposta.Mensagem = "Nenhum autor foi encontrado.";
                }
                else
                {
                    resposta.Mensagem = "Todos os autores foram coletados.";
                }

                resposta.Dados = autores;
            }
            catch (Exception e)
            {
                resposta.Status = false;
                resposta.Mensagem = e.Message;
            }
            return resposta;
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
            try
            {
                var autor = await _autorRepository.BuscarAutorPorId(idAutor);
                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum autor foi encontrado.";
                    return resposta;
                }

                resposta.Dados = autor;
                resposta.Mensagem = "Autor localizado.";
            }
            catch (Exception e)
            {
                resposta.Status = false;
                resposta.Mensagem = e.Message;
            }
            return resposta;
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
            try
            {
                var autor = await _autorRepository.BuscarAutorPorIdLivro(idLivro);
                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum autor encontrado.";
                    return resposta;
                }

                resposta.Dados = autor;
                resposta.Mensagem = "Autor localizado.";
            }
            catch (Exception e)
            {
                resposta.Status = false;
                resposta.Mensagem = e.Message;
            }
            return resposta;
        }

        public async Task<ResponseModel<AutorModel>> CriarAutor(AutorCriacaoDto autorCriacaoDto)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
            try
            {
                var autor = new AutorModel
                {
                    Nome = autorCriacaoDto.Nome,
                    Sobrenome = autorCriacaoDto.Sobrenome
                };

                await _autorRepository.CriarAutor(autor);

                resposta.Dados = autor;
                resposta.Mensagem = "Autor criado com sucesso.";
            }
            catch (Exception e)
            {
                resposta.Status = false;
                resposta.Mensagem = e.Message;
            }
            return resposta;
        }

        public async Task<ResponseModel<AutorModel>> EditarAutor(AutorEdicaoDto autorEdicaoDto)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
            try
            {
                var autor = await _autorRepository.BuscarAutorPorId(autorEdicaoDto.Id);
                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum autor foi encontrado para ser editado.";
                    return resposta;
                }

                autor.Nome = autorEdicaoDto.Nome;
                autor.Sobrenome = autorEdicaoDto.Sobrenome;

                await _autorRepository.EditarAutor(autor);

                resposta.Dados = autor;
                resposta.Mensagem = "Dados do autor alterados com sucesso.";
            }
            catch (Exception e)
            {
                resposta.Status = false;
                resposta.Mensagem = e.Message;
            }
            return resposta;
        }

        public async Task<ResponseModel<AutorModel>> ExcluirAutor(int idAutor)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
            try
            {
                var autor = await _autorRepository.BuscarAutorPorId(idAutor);
                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum autor foi encontrado para exclusão.";
                    return resposta;
                }

                await _autorRepository.ExcluirAutor(idAutor);

                resposta.Dados = autor;
                resposta.Mensagem = "Autor excluído com sucesso.";
            }
            catch (Exception e)
            {
                resposta.Status = false;
                resposta.Mensagem = e.Message;
            }
            return resposta;
        }
    }
}