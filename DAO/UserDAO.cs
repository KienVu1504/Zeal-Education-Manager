using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZealEducationManager.Model;

namespace ZealEducationManager.DAO
{
    public class UserDAO
    {
        ZealEducationManagerDbContext db = null;
        public UserDAO()
        {
            db = new ZealEducationManagerDbContext();
        }
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.Id);
                user.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = entity.ModifiedDate;
                user.Status = entity.Status;
                user.Phone = entity.Phone;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(int Id)
        {
            try
            {
                var user = db.Users.Find(Id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }
    }
}