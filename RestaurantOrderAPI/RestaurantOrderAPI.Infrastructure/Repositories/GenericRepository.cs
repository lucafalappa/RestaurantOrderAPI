using Microsoft.EntityFrameworkCore;
using RestaurantOrderAPI.Infrastructure.Contexts;
using System.Linq.Expressions;

namespace RestaurantOrderAPI.Infrastructure.Repositories
{
    /// <summary>
    /// Provides generic persistence operations for 
    /// <typeparamref name="T"/> type entities
    /// </summary>
    /// <typeparam name="T">The entity
    /// </typeparam>
    public abstract class GenericRepository<T>
        where T : class
    {
        /// <summary>
        /// The database context for the 
        /// RestaurantOrderAPI platform
        /// </summary>
        protected RestaurantOrderAPIDbContext _context;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="GenericRepository{T}"/> class
        /// </summary>
        /// <param name="context">The database context
        /// to associate to the repository
        /// </param>
        public GenericRepository
            (RestaurantOrderAPIDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Asynchronously adds an entity to the repository,
        /// if it is not already present
        /// </summary>
        /// <param name="entity">The entity to add
        /// </param>
        /// <param name="predicate">The predicate to check 
        /// for existing entities
        /// </param>
        /// <returns>true if the entity was added,
        /// false otherwise
        /// </returns>
        public async Task<bool> AddEntityAsync
            (T entity, Predicate<T> predicate)
        {
            try
            {
                var list = await _context.Set<T>().ToListAsync();
                var exists = list.Any(t => predicate(t));
                if (!exists)
                {
                    await _context.Set<T>().AddAsync(entity);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Asynchronously retrieves an entity 
        /// by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the entity
        /// </param>
        /// <returns>The entity if is found, 
        /// null otherwise
        /// </returns>
        public async Task<T?> GetEntityByIdAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        /// <summary>
        /// Asynchronously retrieves a list of entities 
        /// based on optional query builder and search conditions
        /// </summary>
        /// <param name="queryBuilder">Optional query builder
        /// </param>
        /// <param name="predicate">
        /// Optional predicate to filter entities
        /// </param>
        /// <returns>A list of entities
        /// </returns>
        public async Task<List<T>> GetEntitiesAsync
            (Func<IQueryable<T>, IQueryable<T>>? queryBuilder,
            Expression<Func<T, bool>>? predicate)
        {
            var query = _context.Set<T>().AsQueryable();
            if (queryBuilder != null) { query = queryBuilder(query); }
            if (predicate != null) { query = query.Where(predicate); }
            return await query.ToListAsync();
        }
        /// <summary>
        /// Asynchronously modifies an entity, if a matching one 
        /// exists based on the provided conditions
        /// </summary>
        /// <param name="entity">The entity to modify
        /// </param>
        /// <param name="predicate">The predicate to find 
        /// the existing entity
        /// </param>
        /// <returns>true if the entity was modified, 
        /// false otherwise
        /// </returns>
        public async Task<bool> ModifyEntityAsync
            (T entity, Predicate<T> predicate)
        {
            try
            {
                var list = await _context.Set<T>().ToListAsync();
                var existingEntity = list.Where(t => predicate(t))
                                         .FirstOrDefault();
                if (existingEntity != null)
                {
                    _context.Entry(existingEntity).CurrentValues
                            .SetValues(entity);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Asynchronously deletes an entity
        /// by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier 
        /// of the entity to delete
        /// </param>
        /// <returns>true if the entity was deleted, 
        /// false otherwise
        /// </returns>
        public async Task<bool> DeleteEntityAsync
            (object id)
        {
            try
            {
                var entity = await GetEntityByIdAsync(id);
                if (entity != null)
                {
                    _context.Entry(entity).State = EntityState.Deleted;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Asynchronously checks if any entities in the repository 
        /// satisfy the given condition
        /// </summary>
        /// <param name="predicate">The predicate to check
        /// </param>
        /// <returns>true if any entities satisfy the predicate, 
        /// false otherwise
        /// </returns>
        public async Task<bool> AnyEntitiesAsync
            (Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync
                (predicate);
        }
        /// <summary>
        /// Asynchronously saves changes made 
        /// to the database context
        /// </summary>
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}