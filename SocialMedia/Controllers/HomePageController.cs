using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SocialMedia.DAL;
using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class HomePageController : Controller
    {

        private readonly PostDAO _postDAO;
        private readonly PostCommentDAO _postCommentDAO;
        private readonly UserDAO _userDAO;
        private readonly IWebHostEnvironment _hosting;

        public HomePageController(PostDAO postDAO,UserDAO userDAO, PostCommentDAO postCommentDAO, IWebHostEnvironment hosting)
        {
            _postDAO = postDAO;
            _postCommentDAO = postCommentDAO;
            _userDAO = userDAO;
            _hosting = hosting;
        }
        public IActionResult HomePage()
        {
            ViewBag.name = HttpContext.Session.GetString("name");
            ViewBag.image = HttpContext.Session.GetString("image");
            ViewBag.posts = _postDAO.ListPosts().OrderByDescending(x => x.Id).ToList();
         
            return View();
        }
        public IActionResult Register(string description, string image, IFormFile file)
        {
            string id = HttpContext.Session.GetString("id");
            User user = _userDAO.findById(Convert.ToInt32(id));
            if (file != null)
            {
                string archive = $"{ Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                string path = Path.Combine(_hosting.WebRootPath, "images", archive);
                file.CopyTo(new FileStream(path, FileMode.Create));
                image = archive;
            }
            else
            {
                image = "no-image.png";

            }
            Post post = new Post
            {
                title = "123",
                description = description,
                image = image,
                user = user
            };

            _postDAO.CreatePost(post);
            return RedirectToAction("HomePage", "HomePage");
        }
        
    }
}
