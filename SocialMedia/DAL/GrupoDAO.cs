using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.DAL
{
    public class GrupoDAO
    {
        private readonly Context _context;
        public GrupoDAO(Context context) => _context = context;

        public List<Grupo> Listar() => _context.Grupos.Include(g => g.inscricoesGrupos).ThenInclude(g => g.user).Include(g => g.user).ToList();

        public Grupo BuscarPorId(int id) => _context.Grupos.Find(id);

        public Grupo BuscarPorNome(string nome) =>
            _context.Grupos.FirstOrDefault(x => x.Nome == nome);
        public bool Cadastrar(Grupo grupo)
        {
            if (BuscarPorNome(grupo.Nome) == null)
            {
                _context.Grupos.Add(grupo);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public void Remover(int id)
        {
            _context.Grupos.Remove(BuscarPorId(id));
            _context.SaveChanges();
        }
        public void Alterar(Grupo group)
        {
            _context.Grupos.Update(group);
            _context.SaveChanges();
        }
    }
}
