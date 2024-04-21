using Microsoft.EntityFrameworkCore;
using RestaurantOrderAPI.Data.Context;
using RestaurantOrderAPI.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderAPI.Data.Repositories
{
    public abstract class GenericRepository<T> 
        where T : class
    {
        protected RestaurantOrderAPIDbContext _context;
        public GenericRepository(RestaurantOrderAPIDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddEntityAsync
            (T entity, Predicate<T> predicate)
        {
            try
            {
                //TODO : CODE OPTIMIZATION DONE
                //var list = await _context.Set<T>().ToListAsync();
                //if (list.Find(predicate) == null)
                //{
                //    await _context.Set<T>().AddAsync(entity);
                //    return true;
                //} else
                //{
                //    return false;
                //}
                var exists = await _context.Set<T>().AnyAsync
                    (t => predicate(t));
                if (!exists)
                {
                    await _context.Set<T>().AddAsync(entity);
                    return true;
                }
                return false;
            } catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<T?> GetEntityAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<List<T>> GetAllEntityAsync
            (Predicate<T> predicate)
        {
            //TODO : CODE OPTIMIZATION DONE
            //var result = new List<T>();
            //foreach (var entity in await _context.Set<T>().ToListAsync())
            //{
            //    if (predicate(entity))
            //    {
            //        result.Add(entity);
            //    }
            //}
            //return result;
            return await _context.Set<T>().Where(t => predicate(t))
                                          .ToListAsync();
        }
        public async Task<bool> ModifyEntityAsync
            (T entity, Predicate<T> predicate)
        {
            try
            {
                //TODO : CODE OPTIMIZATION DONE
                //var list = await _context.Set<T>().ToListAsync();
                //if (list.Find(predicate) == null)
                //{
                //    _context.Entry(entity).State = EntityState.Modified;
                //    return true;
                //} else
                //{
                //    return false;
                //}
                var exists = await _context.Set<T>().AnyAsync
                    (t => predicate(t));
                if (exists)
                {
                    _context.Entry(entity).State = EntityState.Modified;
                    return true;
                }
                return false;
            } catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteEntityAsync
            (object id)
        {
            try
            {
                //TODO : CODE OPTIMIZATION DONE
                //var entity = await GetEntityAsync(id);
                //var list = await _context.Set<T>().ToListAsync();
                //if (list.Find(predicate) != null)
                //{
                //    _context.Entry(entity).State = EntityState.Deleted;
                //    return true;
                //}
                var entity = await GetEntityAsync(id);
                if (entity != null)
                {
                    _context.Entry(entity).State = EntityState.Deleted;
                    return true;
                }
                return false;
            } catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteEntityAsync
            (T entity, Predicate<T> predicate)
        {
            try
            {
                //TODO : CODE OPTIMIZATION DONE
                //var list = await _context.Set<T>().ToListAsync();
                //if (list.Find(predicate) != null)
                //{
                //    _context.Entry(entity).State = EntityState.Deleted;
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
                var exists = await _context.Set<T>().AnyAsync
                    (t => predicate(t));
                if (exists)
                {
                    _context.Entry(entity).State = EntityState.Deleted;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> AnyEntitiesAsync
            (Predicate<T> predicate)
        {
            return await _context.Set<T>().AnyAsync
                (t => predicate(t));
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
