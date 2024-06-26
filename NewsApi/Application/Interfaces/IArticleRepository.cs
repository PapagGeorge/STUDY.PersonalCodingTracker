﻿using Domain.Models;

namespace Application.Interfaces
{
    public interface IArticleRepository
    {
        Task <ICollection<Article>> GetLatestArticlesAsync();
        Task InsertArticlesAsync (ICollection<Article> articles);
    }
}
