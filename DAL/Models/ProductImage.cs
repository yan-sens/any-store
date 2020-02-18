﻿using System;

namespace DAL.Models
{
    public class ProductImage : BaseEntity
    {
        public Guid ProductId { get; set; }
        public byte[] Image { get; set; }
        public Product Product { get; set; }
    }
}