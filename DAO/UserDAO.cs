using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZealEducationManager.Model;

namespace ZealEducationManager.DAO
{
    //Data access object
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
        public User GetUserName(string username)
        {
            return db.Users.SingleOrDefault(x => x.Username == username);
        }
        public IEnumerable<User> ListAllPaging(string searchString, int page, int pagesize)
        {
            IQueryable<User> model = db.Users;
            model = model.Where(x => x.Username != "admin");
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Username.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x=>x.CreateData).ToPagedList(page, pagesize);
        }
        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }
        public int Login(string username, string password, bool isLoginAdmin = false)
        {
            var result = db.Users.SingleOrDefault(x=>x.Username == username);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (result.Status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.Password == password)
                        {
                            return 1;
                        }
                        else
                        {
                            return -2;
                        }
                    }
                }
                else
                {
                    if (result.Status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.Password == password)
                        {
                            return 1;
                        }
                        else
                        {
                            return -2;
                        }
                    }
                }
            }
        }
    }
}