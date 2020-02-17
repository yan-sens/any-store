using System;

namespace DAL.Models
{
    public class ProductImage : BaseEntity
    {
        public Guid PatientId { get; set; }
        public byte[] Image { get; set; }
    }
}
