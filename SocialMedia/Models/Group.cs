using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models
{
    [Table("Groups")]
    public class Group : BaseModel
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }
    }
}
