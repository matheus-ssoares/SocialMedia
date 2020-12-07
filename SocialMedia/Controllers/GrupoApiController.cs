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
    [Route("api/Grupo")]
    [ApiController]
    public class GrupoApiController : ControllerBase
    {
        
            private readonly GrupoDAO _grupoDAO;

            public GrupoApiController(GrupoDAO grupoDAO)
            {
                _grupoDAO = grupoDAO;
            }

            //GET: /api/Grupo/Listar
            [HttpGet]
            [Route("Listar")]
            public IActionResult Listar()
            {
                List<Grupo> grupos = _grupoDAO.Listar();
                if (grupos.Count > 0)
                {
                    return Ok(grupos);
                }
                return BadRequest(new { msg = "Não existem grupos registrados!" });
            }

            //GET: /api/Grupo/Buscar
            [HttpGet]
            [Route("Buscar{id}")]
            public IActionResult Buscar(int id)
            {
                Grupo grupo = _grupoDAO.BuscarPorId(id);
                if(grupo != null)
                {
                    return Ok(grupo);
                }
                return NotFound(new { msg = "Grupo não encotrado" });
            }

            //POST: /api/Grupo/Cadastrar
            [HttpPost]
            [Route("Cadastrar")]
            public IActionResult Cadastrar(Grupo grupo)
            {
                if(ModelState.IsValid)
            {
                if(_grupoDAO.Cadastrar(grupo))
                {
                    return Created("", grupo);
                }
                return Conflict(new { msg = "Este grupo já existe" });
            }
            return BadRequest(ModelState);
            }

    }
}
