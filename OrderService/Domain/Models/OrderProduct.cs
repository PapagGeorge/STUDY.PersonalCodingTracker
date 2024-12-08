﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class OrderProduct
    {
        [Key]
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}