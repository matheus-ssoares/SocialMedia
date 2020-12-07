using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { } 
       
        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Grupo> Grupos { get; set; }

        public DbSet<PostComments> postComments { get; set; }
        public DbSet<InscricoesGrupos> inscricoesGrupos { get; set; }


    }
}
