using BibliotecaApi.Models;
using BibliotecaApi.Models.Dto.Livro;
using BibliotecaApi.Repositories.Livro;
using BibliotecaApi.Repositories.Autor;

namespace BibliotecaApi.Services.Livro
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IAutorRepository _autorRepository;

        public LivroService(ILivroRepository livroRepository, IAutorRepository autorRepository)
        {
            _livroRepository = livroRepository;
            _autorRepository = autorRepository;
        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livros = await _livroRepository.ListarLivros() ?? new List<LivroModel>();

                if (!livros.Any())
                {
                    resposta.Mensagem = "Nenhum livro foi encontrado.";
                }
                else
                {
                    resposta.Mensagem = "Todos os livros foram coletados.";
                }

                resposta.Dados = livros;
            }
            catch (Exception e)
            {
                resposta.Status = false;
                resposta.Mensagem = e.Message;
            }
            return resposta;
        }

        public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
        {
            ResponseModel<LivroModel> resposta = new ResponseModel<LivroModel>();
            try
            {
                var livro = await _livroRepository.BuscarLivroPorId(idLivro);
                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro foi encontrado.";
                    return resposta;
                }

                resposta.Dados = livro;
                resposta.Mensagem = "Livro localizado.";
            }
            catch (Exception e)
            {
                resposta.Status = false;
                resposta.Mensagem = e.Message;
            }
            return resposta;
        }

        public async Task<ResponseModel<List<LivroModel>>> BuscarLivrosPorIdAutor(int idAutor)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livros = await _livroRepository.BuscarLivrosPorIdAutor(idAutor) ?? new List<LivroModel>();
                
                if (!livros.Any())
                {
                    resposta.Mensagem = "Nenhum livro foi encontrado.";
                }
                else
                {
                    resposta.Mensagem = "Todos os livros foram coletados.";
                }

                resposta.Dados = livros;
            }
            catch (Exception e)
            {
                resposta.Status = false;
                resposta.Mensagem = e.Message;
            }
            return resposta;
        }

        public async Task<ResponseModel<LivroModel>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            ResponseModel<LivroModel> resposta = new ResponseModel<LivroModel>();
            try
            {
                if (livroCriacaoDto.Autor == null)
                {
                    resposta.Mensagem = "Autor não informado.";
                    return resposta;
                }

                var autor = await _autorRepository.BuscarAutorPorId(livroCriacaoDto.Autor.Id);
                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum registro de autor localizado.";
                    return resposta;
                }

                var livro = new LivroModel
                {
                    Titulo = livroCriacaoDto.Titulo,
                    Autor = autor
                };

                await _livroRepository.CriarLivro(livro);

                resposta.Dados = livro;
                resposta.Mensagem = "Livro criado com sucesso.";
            }
            catch (Exception e)
            {
                resposta.Status = false;
                resposta.Mensagem = e.Message;
            }
            return resposta;
        }

        public async Task<ResponseModel<LivroModel>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            ResponseModel<LivroModel> resposta = new ResponseModel<LivroModel>();
            try
            {
                var livro = await _livroRepository.BuscarLivroPorId(livroEdicaoDto.Id);
                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro foi encontrado para ser editado.";
                    return resposta;
                }

                if (livroEdicaoDto.Autor != null)
                {
                    var autor = await _autorRepository.BuscarAutorPorId(livroEdicaoDto.Autor.Id);
                    if (autor == null)
                    {
                        resposta.Mensagem = "Autor informado não existe.";
                        return resposta;
                    }
                    livro.Autor = autor;
                }

                livro.Titulo = livroEdicaoDto.Titulo;

                await _livroRepository.EditarLivro(livro);

                resposta.Dados = livro;
                resposta.Mensagem = "Dados do livro alterados com sucesso.";
            }
            catch (Exception e)
            {
                resposta.Status = false;
                resposta.Mensagem = e.Message;
            }
            return resposta;
        }

        public async Task<ResponseModel<LivroModel>> ExcluirLivro(int idLivro)
        {
            ResponseModel<LivroModel> resposta = new ResponseModel<LivroModel>();
            try
            {
                var livro = await _livroRepository.BuscarLivroPorId(idLivro);
                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro foi encontrado para exclusão.";
                    return resposta;
                }

                await _livroRepository.ExcluirLivro(idLivro);

                resposta.Dados = livro;
                resposta.Mensagem = "Livro excluído com sucesso.";
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