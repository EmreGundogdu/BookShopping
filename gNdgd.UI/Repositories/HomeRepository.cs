using Microsoft.EntityFrameworkCore;

namespace gNdgd.UI.Repositories;
public class HomeRepository:IHomeRepository
{
    readonly ApplicationDbContext context;
    public HomeRepository(ApplicationDbContext context)
    {
        this.context = context;
    }
    public async Task<IEnumerable<Book>> DisplayBooks(string sTerm = "", int genreId = 0)
    {
        sTerm = sTerm.ToLower();
        IEnumerable<Book> books = await (from book in context.Books
                     join genre in context.Genres on book.GenreId equals genre.Id
                     where string.IsNullOrWhiteSpace(sTerm) || (book != null && book.BookName.ToLower().StartsWith(sTerm))
                     select new Book
                     {
                         Id = book.Id,
                         Image = book.Image,
                         AuthorName = book.AuthorName,
                         BookName = book.BookName,
                         GenreId = book.GenreId,
                         Price = book.Price,
                         GenreName = book.GenreName,
                     }).ToListAsync();
        if (genreId > 0)
        {
            books = books.Where(x => x.GenreId == genreId).ToList();
        }
        return books;
    }
    public async Task<IEnumerable<Genre>> Genres()
    {
        return await context.Genres.ToListAsync();
    }
}
