using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            CreatedAt = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
