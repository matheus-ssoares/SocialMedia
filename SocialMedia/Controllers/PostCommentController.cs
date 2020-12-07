using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.DAL;
using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class PostCommentController : Controller
    {
        private readonly PostCommentDAO _postCommentDAO;
        private readonly PostDAO _postDAO;
        private readonly UserDAO _userDAO;


        public PostCommentController(PostCommentDAO postCommentDAO, PostDAO postDAO, UserDAO userDAO)
        {

            _postCommentDAO = postCommentDAO;
            _postDAO = postDAO;
            _userDAO = userDAO;



        }
        public IActionResult createPostComment(int postId, string description)
        {
            Post post =_postDAO.findPostById(postId);
           
            User user = _userDAO.findById(Convert.ToInt32(HttpContext.Session.GetString("id")));

            PostComments postComments = new PostComments { 
                description = description,
                user = user,
                post = post
            };
            _postCommentDAO.createPostComment(postComments);


           
                return RedirectToAction("HomePage", "HomePage");
           

        }
    }
}
