using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AreaContext : IDB<Area, int>
    {
        private HobbyDbContext _context;

        public AreaContext(HobbyDbContext context)
        {
            _context = context;
        }
        public void Create(Area item)
        {
            try
            {
                _context.Areas.Add(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int key)
        {
            try
            {
                _context.Areas.Remove(Read(key));
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Area Read(int key, bool noTracking = false, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Area> query = _context.Areas;

                if (noTracking)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                if (useNavigationProperties)
                {
                    query = query.Include(i => i.Users);
                }

                return query.SingleOrDefault(i => i.ID == key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Area> Read(int skip, int take, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Area> query = _context.Areas.AsNoTrackingWithIdentityResolution();

                if (useNavigationProperties)
                {
                    query = query.Include(i => i.Users);
                }

                return query.Skip(skip).Take(take).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Area> ReadAll(bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Area> query = _context.Areas.AsNoTracking();

                if (useNavigationProperties)
                {
                    query = query.Include(i => i.Users);
                }

                return query.ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Area item, bool useNavigationProperties = false)
        {
            try
            {
                Area AreaFromDB = Read(item.ID, useNavigationProperties);

                if (useNavigationProperties)
                {

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

                    AreaFromDB.Users = users;
                }

                _context.Entry(AreaFromDB).CurrentValues.SetValues(item);
                _context.SaveChanges();

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
