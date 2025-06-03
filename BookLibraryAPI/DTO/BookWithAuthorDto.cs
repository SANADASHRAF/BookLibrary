namespace BookLibraryAPI.DTO
{
    public class BookWithAuthorDto
    {
        public string Id { get; set; }
        public string Name { get; set; }       
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
    }

    public class ResultBookWithAuthorDto
    {
        public int TotalOfBook { get; set; }
        public List<BookWithAuthorDto> bookWithAuthorDtos { get; set; }

    }

}
