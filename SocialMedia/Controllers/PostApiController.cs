using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.DAL;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
    [Route("api/Post")]
    [ApiController]
    public class PostApiController : ControllerBase
    {

        private readonly PostDAO _postDAO;


        public PostApiController(PostDAO postDAO)
        {

            _postDAO = postDAO;

        }

        //GET: /api/Post/Listar
        [HttpGet]
        [Route("Listar")]
        public IActionResult Listar()
        {
            List<Post> posts = _postDAO.ListPosts();
            if(posts.Count > 0)
            {
                return Ok(posts);
            }
            return BadRequest(new {msg = "Não existem registros de posts!" });
        }

        //GET: /api/Post/Buscar
        [HttpGet]
        [Route("Buscar/{id}")]
        public IActionResult Buscar(int id)
        {
            Post post = _postDAO.findPostById(id);
            if (post != null)
            {
                return Ok(post);
            }
            return NotFound(new { msg = "Post não encontrado!" });
        }

        //POST: /api/Post/Cadastrar
        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult CreatePost(Post post)
        {
            if(ModelState.IsValid)
            {
                if(_postDAO.CreatePost(post))
                {
                    return Created("", post);
                }
               
            }
            return BadRequest(ModelState);
        }
    }
}
