namespace BibliotecaApi.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public ICollection<Livro>? Livros { get; set; }
    }
}