using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models
{
    [Table("PostComments")]
    public class PostComments : BaseModel
    {
        public string description { get; set; }

        [ForeignKey("userAccountId")]
        public User user { get; set; }

        [ForeignKey("postId")]
        public Post post { get; set; }
    }
}
