using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.DAL
{
    public class UserDAO
    {
        private readonly Context _context;

        public UserDAO(Context context)
        {
            _context = context;
        }
        public bool UserRegister(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch {

                return false;
            }
        }
        public User findById(int id)
        {
            User user;    
                 user = _context.Users.Find(id);      
                    return user;           
        }

        public User findByEmail(string email)
        {
            User user;
            user = _context.Users.FirstOrDefault(x => x.email == email);
            return user;
        }
        public bool UpdateUserAccount(User user)
        {
            try
            {
                _context.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Remove(int id)
        {
            _context.Users.Remove(findById(id));
            _context.SaveChanges();
        }


    }
}
