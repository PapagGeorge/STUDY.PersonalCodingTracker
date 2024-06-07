﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces
{
    public interface INewsApiClient
    {
        Task<NewsApiResponse> GetNewsAsync(string keyword)
    }
}
