using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.DAL
{
    
    public class PostCommentDAO
    {
        private readonly Context _context;
        public PostCommentDAO(Context context) => _context = context;


        public bool createPostComment(PostComments postComment)
        {
            try
            {
                _context.Add(postComment);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<PostComments> getAllPostComments()
        {
            return _context.postComments.Include(p => p.user).ToList();
        }
    }
}
