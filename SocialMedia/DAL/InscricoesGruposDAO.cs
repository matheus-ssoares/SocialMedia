using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia.DAL
{
    public class InscricoesGruposDAO
    {
         private readonly Context _context;
        public InscricoesGruposDAO(Context context) => _context = context;

       


        public bool verifyGroupSubscriptions(int userAccountId, int groupId)
        {
            List<InscricoesGrupos> inscricoesGrupos;
            inscricoesGrupos = _context.inscricoesGrupos.Where(g => g.grupo.Id == groupId).Where(x => x.user.Id == userAccountId).ToList();

            

            User user = _context.Users.FirstOrDefault( x => x.Id == userAccountId);
            List<Grupo> grupo = _context.Grupos.Include(x => x.user).ToList();

            if (userAccountId == grupo[0].user.Id)
            {
                return true;
            }

            if (inscricoesGrupos.Count < 1)
            {
                InscricoesGrupos inscricoesGrupo = new InscricoesGrupos
                {
                    status = "pending",
                    user = user,
                    grupo = grupo[0]
                };
                _context.Add(inscricoesGrupo);
                _context.SaveChanges();
                return false;
            }
            else
            {
                InscricoesGrupos incricoesGrupo = inscricoesGrupos.Find(g => g.status == "accept");
                if(incricoesGrupo != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public InscricoesGrupos findById(int Id)
        {
           return _context.inscricoesGrupos.Find(Id);
        }
        public List<InscricoesGrupos> getAllSubscriptions(int userId)
        {
            List<InscricoesGrupos> inscricoesGrupos;



            inscricoesGrupos = _context.inscricoesGrupos.Include(x => x.grupo).ThenInclude(x => x.user).Where( x => x.grupo.user.Id == userId).Include(x => x.user).ToList();

            return inscricoesGrupos;

        }
        public bool updateSubscription(InscricoesGrupos inscricoesGrupos)
        {
            try
            {
                _context.Update(inscricoesGrupos);
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
