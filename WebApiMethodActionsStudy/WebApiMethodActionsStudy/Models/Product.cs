﻿namespace WebApiMethodActionsStudy.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Availability { get; set; }
        public bool IsAvailable { get; set; }
    }
}
