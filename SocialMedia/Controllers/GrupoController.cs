using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SocialMedia.DAL;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
    public class GrupoController : Controller
    {
        private readonly GrupoDAO _grupoDAO;
        private readonly PostDAO _postDAO;
        private readonly UserDAO _userDAO;
        private readonly InscricoesGruposDAO _inscricoesGruposDAO;
        private readonly IWebHostEnvironment _hosting;

        public GrupoController(GrupoDAO grupoDAO, IWebHostEnvironment hosting, UserDAO userDAO, InscricoesGruposDAO inscricoesGrupos, PostDAO postDAO)
        {
            _userDAO = userDAO;
            _hosting = hosting;
            _grupoDAO = grupoDAO;
            _postDAO = postDAO;
            _inscricoesGruposDAO = inscricoesGrupos;
        }

        // GET: Grupo
        public IActionResult Index()
        {
            List<Grupo> grupos = _grupoDAO.Listar();
           
            ViewBag.Quantidade = grupos.Count;
            ViewBag.userId = Convert.ToInt32(HttpContext.Session.GetString("id"));
            return View(grupos);
        }
        public IActionResult mygroups()
        {
            ViewBag.subscriptions =  _inscricoesGruposDAO.getAllSubscriptions(Convert.ToInt32(HttpContext.Session.GetString("id")));
            return View();
        }

        public IActionResult registerPostGroup()

        {
            ViewBag.name = HttpContext.Session.GetString("name");
            ViewBag.image = HttpContext.Session.GetString("image");
            ViewBag.subscriptions = _inscricoesGruposDAO.getAllSubscriptions(Convert.ToInt32(HttpContext.Session.GetString("id")));
            return View();
        }

        public IActionResult registerPost(string description, string image, IFormFile file)
        {
            string id = HttpContext.Session.GetString("id");
            string groupId = HttpContext.Session.GetString("groupId");
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
                user = user,
                grupo = _grupoDAO.BuscarPorId(Convert.ToInt32(groupId))
            };

            _postDAO.CreatePost(post);
            return RedirectToAction("Details", "Grupo");
        }

        public IActionResult rejectedSubscription(int Id)
        {
            InscricoesGrupos inscricoesGrupos = _inscricoesGruposDAO.findById(Id);
            inscricoesGrupos.status = "rejected";
            _inscricoesGruposDAO.updateSubscription(inscricoesGrupos);
            return RedirectToAction("mygroups", "Grupo");
        }
        public IActionResult acceptSubscription(int Id)
        {
            InscricoesGrupos inscricoesGrupos = _inscricoesGruposDAO.findById(Id);
            inscricoesGrupos.status = "accept";
            _inscricoesGruposDAO.updateSubscription(inscricoesGrupos);
            return RedirectToAction("mygroups", "Grupo");
        }
        public IActionResult verify(int Id)
        {
            if (_inscricoesGruposDAO.verifyGroupSubscriptions(Convert.ToInt32(HttpContext.Session.GetString("id")), Id))
            {
                ViewBag.name = HttpContext.Session.GetString("name");
                ViewBag.image = HttpContext.Session.GetString("image");
                HttpContext.Session.SetString("groupId", Id.ToString());
                ViewBag.groupId = Id;
                ViewBag.grupo = _postDAO.findPostsByGroupId(Id);
                return RedirectToAction("Details", "Grupo");
            }
            else
            {
                return RedirectToAction("Index", "Grupo");
            }
        }

        // GET: Grupo/Details/5
        public IActionResult Details()
        {
            ViewBag.name = HttpContext.Session.GetString("name");
            ViewBag.image = HttpContext.Session.GetString("image");

            ViewBag.groupId = Convert.ToInt32(HttpContext.Session.GetString("groupId"));
            ViewBag.grupo = _postDAO.findPostsByGroupId(Convert.ToInt32(HttpContext.Session.GetString("groupId")));
            return View();
         
        }

        // GET: Grupo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Grupo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Grupo grupo, IFormFile file)
        {
            if (ModelState.IsValid)

            {
                if (file != null)
                {
                    string arquivo = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    string caminho = Path.Combine(_hosting.WebRootPath, "images", arquivo);
                    file.CopyTo(new FileStream(caminho, FileMode.CreateNew));
                    grupo.imagem = arquivo;
                }
                else
                {
                    grupo.imagem = "semimagem.gif";
                }

                grupo.user = _userDAO.findById(Convert.ToInt32(HttpContext.Session.GetString("id")));

                if (_grupoDAO.Cadastrar(grupo))
                {
                    return RedirectToAction("Index", "Grupo");
                }
            }
                ModelState.AddModelError("", "Não foi possível cadastradar o grupo!");
                return View(grupo);
        }

        // GET: Grupo/Edit/5
        public IActionResult Edit(int id)
        {
            
            ViewBag.grupo = _grupoDAO.BuscarPorId(id);
            return View();
        }

        // POST: Grupo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Grupo grupo, IFormFile file)
        {
            if (file != null)
            {
                string arquivo = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                string caminho = Path.Combine(_hosting.WebRootPath, "images", arquivo);
                file.CopyTo(new FileStream(caminho, FileMode.CreateNew));
                grupo.imagem = arquivo;
            }
            _grupoDAO.Alterar(grupo);
            ViewBag.grupo = grupo;
            return RedirectToAction("Index", "Grupo");
        }

        // GET: Grupo/Delete/5
        public IActionResult Delete(int id)
        {
            return View(_grupoDAO.BuscarPorId(id));
        }

        // POST: Grupo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _grupoDAO.Remover(id);
            return RedirectToAction("Index", "Grupo");
        }

      
    }
}
