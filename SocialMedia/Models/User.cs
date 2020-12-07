using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models
{
    [Table("User")] 
    public class User : BaseModel
    {
        public string name { get; set; }

        public string email { get; set; }

        public string bornDate { get; set; }

        public string cpf { get; set; }

        public string phone { get; set; }

        public string Genre { get; set; }

        public string state { get; set; }

        public string city { get; set; }

        public string pass { get; set; }

        public string image { get; set; }
    }
}
