﻿namespace Domain.Models
{
    public class NewsResponse
    {
        public string Status { get; set; }
        public int TotalResults { get; set; }
        public List<Article> Articles { get; set; }      
    }
}
