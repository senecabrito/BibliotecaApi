namespace BibliotecaApi.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public Autor? Autor { get; set; }
    }
}