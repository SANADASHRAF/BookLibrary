namespace BookLibraryAPI.DTO
{
    public class ResultAutorWithBooksDto
    {
        public List <AutorWithBooksDto> autorWithBooksDtos { get; set; }
    }

    public class AutorWithBooksDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<BookDto> bookDtos { get; set; }
    }

    public class BookDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
    }
}
