using Microsoft.AspNetCore.Mvc;
using BibliotecaApi.Services.Livro;
using BibliotecaApi.Models;
using BibliotecaApi.Models.Dto.Livro;

namespace BibliotecaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroInterface;

        public LivroController(ILivroService livroInterface)
        {
            _livroInterface = livroInterface;
        }

        [HttpGet("ListarLivros")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> ListarLivros()
        {
            var autores = await _livroInterface.ListarLivros();
            return Ok(autores);
        }

        [HttpGet("BuscarLivroPorId/{idLivro}")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarLivroPorId(int idLivro)
        {
            var autor = await _livroInterface.BuscarLivroPorId(idLivro);
            return Ok(autor);
        }

        [HttpGet("BuscarLivrosPorIdAutor/{idAutor}")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> BuscarLivrosPorIdAutor(int idAutor)
        {
            var livros = await _livroInterface.BuscarLivrosPorIdAutor(idAutor);
            return Ok(livros);
        }

        [HttpPost("CriarLivro")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            var livros = await _livroInterface.CriarLivro(livroCriacaoDto);
            return Ok(livros);
        }

        [HttpPut("EditarLivro")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            var livros = await _livroInterface.EditarLivro(livroEdicaoDto);
            return Ok(livros);
        }

        [HttpDelete("ExcluirLivro/{idLivro}")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> ExcluirLivro(int idLivro)
        {
            var livros = await _livroInterface.ExcluirLivro(idLivro);
            return Ok(livros);
        }
    }
}