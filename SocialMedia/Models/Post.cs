using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models
{
    [Table("Post")]
    public class Post:BaseModel
    {
        public string title { get; set; }

        public string description { get; set; }

        public string image { get; set; }

        [ForeignKey("userAccountId")]
        public  User user { get; set; }

        [ForeignKey("groupId")]
        public Grupo? grupo { get; set; }

        public List<PostComments> postComments { get; set; }
    }
}
