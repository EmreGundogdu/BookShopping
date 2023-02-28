﻿namespace gNdgd.UI
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Book>> DisplayBooks(string sTerm="",int genreId=0);
        Task<IEnumerable<Genre>> Genres();
    }
}