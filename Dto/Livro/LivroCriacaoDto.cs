using BibliotecaApi.Models;

namespace BibliotecaApi.Dto.Livro
{
    public class LivroCriacaoDto
    {
        public string? Titulo { get; set; }
        public AutorModel? Autor { get; set; }
    }
}