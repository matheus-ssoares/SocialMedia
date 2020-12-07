using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.DAL;
using SocialMedia.Models;
using System;
using System.IO;
using System.Linq;



namespace SocialMedia.Controllers
{
    public class UserController : Controller
    {

        private readonly UserDAO _userDAO;
        private readonly IWebHostEnvironment _hosting;

        public UserController(UserDAO userDAO, IWebHostEnvironment hosting)
        {
          
            _userDAO = userDAO;
            _hosting = hosting;

        }
        public IActionResult HomePage(string email,string pass)
        {
            User user = _userDAO.findByEmail(email);
            if (user.pass == pass)
            {
                HttpContext.Session.SetString("image", user.image);
                HttpContext.Session.SetString("name", user.name);
                HttpContext.Session.SetString("id", user.Id.ToString());
                HttpContext.Session.SetString("email", user.email);
                HttpContext.Session.SetString("phone", user.phone); 
                HttpContext.Session.SetString("cpf", user.cpf);
                HttpContext.Session.SetString("Genre", user?.Genre); 
                HttpContext.Session.SetString("state", user.state); 
                HttpContext.Session.SetString("city", user.city);
                HttpContext.Session.SetString("bornDate", user.bornDate);
                HttpContext.Session.SetString("pass", user.pass);

                return RedirectToAction("HomePage", "HomePage");
            }
            else
            {
                return RedirectToAction("Index", "User");
            }


          
        } 
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserProfile()
        {
            ViewBag.image = HttpContext.Session.GetString("image");
            ViewBag.name = HttpContext.Session.GetString("name");
            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.phone = HttpContext.Session.GetString("phone");
            ViewBag.cpf = HttpContext.Session.GetString("cpf");
            ViewBag.genre = HttpContext.Session.GetString("Genre");
            ViewBag.state = HttpContext.Session.GetString("state");
            ViewBag.city = HttpContext.Session.GetString("city");
            ViewBag.bornDate = HttpContext.Session.GetString("bornDate");
            ViewBag.pass = HttpContext.Session.GetString("pass");

            return View();
        }

        public IActionResult UserRegister() => View();

         

        [HttpPost]
        public IActionResult UserRegister(string
            txtName, string txtEmail,
            string txtBornDate, string txtCpf, string txtPhone,
            string txtGenre, string txtState, string txtCity, string txtPassword)
        {
            User user = new User
            {
                pass = txtPassword,
                image= "no-user-image.png",
                name = txtName,
                email = txtEmail,
                bornDate = txtBornDate,
                cpf = txtCpf,
                phone = txtPhone,
                Genre = txtGenre,
                state = txtState,
                city = txtCity
            };
            _userDAO.UserRegister(user);


            return RedirectToAction("Index", "User");
        }
        public IActionResult SaveUserImage(IFormFile file)
        {
            string id = HttpContext.Session.GetString("id");

           
            User user = _userDAO.findById(Convert.ToInt32(id));

           
            string archive = $"{ Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string path = Path.Combine(_hosting.WebRootPath, "UserImages", archive);
            file.CopyTo(new FileStream(path, FileMode.Create));

            ViewBag.image = archive;

            user.image = archive;

            _userDAO.UpdateUserAccount(user);

            HttpContext.Session.SetString("image", user.image);


            return RedirectToAction("UserProfile", "User");
        }
        public IActionResult UpdateUser(User user)
        {
            string id = HttpContext.Session.GetString("id");
            user.image = HttpContext.Session.GetString("image");
            user.Genre = HttpContext.Session.GetString("image");

            user.Id = Convert.ToInt32(id);
            _userDAO.UpdateUserAccount(user);

            HttpContext.Session.SetString("image", user.image);
            HttpContext.Session.SetString("name", user.name);
            HttpContext.Session.SetString("id", user.Id.ToString());
            HttpContext.Session.SetString("email", user.email);
            HttpContext.Session.SetString("phone", user.phone);
            HttpContext.Session.SetString("cpf", user.cpf);
           
            HttpContext.Session.SetString("state", user.state);
            HttpContext.Session.SetString("city", user.city);
            HttpContext.Session.SetString("bornDate", user.bornDate);
            HttpContext.Session.SetString("pass", user.pass);

            return RedirectToAction("UserProfile", "User");
        }

        public IActionResult DeleteUser(User user)
        {
            string id = HttpContext.Session.GetString("id");
            user.image = HttpContext.Session.GetString("image");

            user.Id = Convert.ToInt32(id);
            _userDAO.UpdateUserAccount(user);

            HttpContext.Session.SetString("id", user.Id.ToString());

            return RedirectToAction("Index");
        }
    }
}
