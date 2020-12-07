using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models
{
    [Table("InscricoesGrupos")]
    public class InscricoesGrupos : BaseModel
    {

        public string status { get; set; }

        [ForeignKey("grupoId")]
        public Grupo grupo { get; set; }
        [ForeignKey("userAccountId")]
        public User user { get; set; }
    }
}
