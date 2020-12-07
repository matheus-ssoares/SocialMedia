using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.DAL
{
    public class PostDAO
    {
        private readonly Context _context;

        public PostDAO(Context context) => _context = context;
       

        public List<Post> ListPosts()
        {
              
                List<Post> posts = _context.Posts?.Include(post => post.user)?.Include(post => post.postComments)?.Include(x => x.grupo).ToList();

            List<Post> posts1 = posts.FindAll(x => x.grupo == null).ToList();

            return posts1;
        }

            public Post findPostById(int id) => _context.Posts.FirstOrDefault(p => p.Id == id);


        public List<Post> findPostsByGroupId(int groupId)
        {
            return _context.Posts.Where(p => p.grupo.Id == groupId).Include(p => p.grupo).Include(p => p.user).Include(p => p.postComments).ThenInclude(pc => pc.user).ToList().OrderByDescending(x => x.Id).ToList();
        }


        public bool CreatePost(Post post)
        {
            try
            {
               
                _context.Add(post);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
           
        }
        
    }
}
