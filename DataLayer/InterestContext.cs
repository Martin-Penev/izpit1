using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class InterestContext : IDB<Interest, int>
    {
        private HobbyDbContext _context;

        public InterestContext(HobbyDbContext context)
        {
            _context = context;
        }
        public void Create(Interest item)
        {
            try
            {
                Area AreaFromDB = _context.Areas.Find(item.AreaID);

                if (AreaFromDB != null)
                {
                    item.Area = AreaFromDB;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int key)
        {
            try
            {
                _context.Interests.Remove(Read(key));
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Interest Read(int key, bool noTracking = false, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Interest> query = _context.Interests;

                if (noTracking)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                if (useNavigationProperties)
                {
                    query = query.Include(i => i.Area).Include(i => i.Users);
                }

                return query.SingleOrDefault(i => i.ID == key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Interest> Read(int skip, int take, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Interest> query = _context.Interests.AsNoTrackingWithIdentityResolution();

                if (useNavigationProperties)
                {
                    query = query.Include(i => i.Area).Include(i => i.Users);
                }

                return query.Skip(skip).Take(take).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Interest> ReadAll(bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Interest> query = _context.Interests.AsNoTracking();

                if (useNavigationProperties)
                {
                    query = query.Include(i => i.Area).Include(i => i.Users);
                }

                return query.ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Interest item, bool useNavigationProperties = false)
        {
            try
            {
                Interest interestFromDB = Read(item.ID, useNavigationProperties);

                if (useNavigationProperties)
                {
                    interestFromDB.Area = item.Area;

                    List<User> users = new List<User>();

                    foreach (User user in item.Users)
                    {
                        User userFromDB = _context.Users.Find(user.ID);

                        if (userFromDB != null)
                        {
                            users.Add(userFromDB);
                        }
                        else
                        {
                            users.Add(user);
                        }
                    }

                    interestFromDB.Users = users;
                }

                _context.Entry(interestFromDB).CurrentValues.SetValues(item);
                _context.SaveChanges();

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
