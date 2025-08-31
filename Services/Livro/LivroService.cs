using Microsoft.EntityFrameworkCore;
using BibliotecaApi.Data;
using BibliotecaApi.Models;
using BibliotecaApi.Dto.Livro;

namespace BibliotecaApi.Services.Livro
{
    public class LivroService : ILivro
    {
        private readonly AppDbContext _context;

        public LivroService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livros = await _context.Livros
                .Include(a => a.Autor)
                .ToListAsync();

                if (!livros.Any())
                {
                    resposta.Mensagem = "Nenhum livro foi encontrado.";
                    return resposta;
                }

                resposta.Dados = livros;
                resposta.Mensagem = "Todos os livros foram coletados.";
                return resposta;
            }
            catch (Exception e)
            {
                resposta.Mensagem = e.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
        {
            ResponseModel<LivroModel> resposta = new ResponseModel<LivroModel>();
            try
            {
                var livro = await _context.Livros
                .Include(a => a.Autor)
                .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro foi encontrado";
                    return resposta;
                }

                resposta.Dados = livro;
                resposta.Mensagem = "O livro foi coletado.";
                return resposta;
            }
            catch (Exception e)
            {
                resposta.Mensagem = e.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> BuscarLivrosPorIdAutor(int idAutor)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livros = await _context.Livros
                .Include(a => a.Autor)
                .Where(livroBanco => livroBanco.Autor != null && livroBanco.Autor.Id == idAutor)
                .ToListAsync();

                if (!livros.Any())
                {
                    resposta.Mensagem = "Nenhum livro deste autor foi encontrado.";
                    return resposta;
                }

                resposta.Dados = livros;
                resposta.Mensagem = "Os livros deste autor foram coletados.";
                return resposta;
            }
            catch (Exception e)
            {
                resposta.Mensagem = e.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                if (livroCriacaoDto.Autor == null)
                {
                    resposta.Mensagem = "Autor não informado.";
                    return resposta;
                }

                var autor = await _context.Autores
                .FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroCriacaoDto.Autor.Id);

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

                await _context.AddAsync(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros
                .Include(a => a.Autor)
                .ToListAsync();

                resposta.Mensagem = "O livro foi adicionado com sucesso.";
                return resposta;
            }
            catch (Exception e)
            {
                resposta.Mensagem = e.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros
                .Include(a => a.Autor)
                .FirstOrDefaultAsync(livroBanco => livroBanco.Id == livroEdicaoDto.Id);

                if (livro == null)
                {
                    resposta.Mensagem = "O livro não foi encontrado.";
                    return resposta;
                }

                var autor = await _context.Autores
                .FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroEdicaoDto.Autor.Id);

                if (autor == null)
                {
                    resposta.Mensagem = "O autor não foi encontrado.";
                    return resposta; 
                }

                livro.Titulo = livroEdicaoDto.Titulo;
                livro.Autor = autor;

                _context.Update(livro);
                await _context.SaveChangesAsync();

                resposta.Mensagem = "O livro foi alterado com sucesso.";
                resposta.Dados = await _context.Livros.ToListAsync();
                return resposta;
            }
            catch (Exception e)
            {
                resposta.Mensagem = e.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = _context.Livros
                .FirstOrDefault(livroBanco => livroBanco.Id == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "O livro não foi encontrado.";
                    return resposta;
                }

                _context.Remove(livro);
                await _context.SaveChangesAsync();

                resposta.Mensagem = "O livro foi removido com sucesso.";
                resposta.Dados = await _context.Livros.ToListAsync();
                return resposta;
            }
            catch (Exception e)
            {
                resposta.Mensagem = e.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}