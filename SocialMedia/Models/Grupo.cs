using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models
{
    [Table("Grupos")]
    public class Grupo : BaseModel
    {


        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string imagem { get; set; }

        public User user { get; set; }

        public List<InscricoesGrupos> inscricoesGrupos { get; set; }


    }
}
