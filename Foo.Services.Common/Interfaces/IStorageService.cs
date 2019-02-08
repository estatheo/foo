using System;
using System.Threading.Tasks;
using Foo.Services.Common.Models;

namespace Foo.Services.Common.Interfaces
{
    /// <summary>
    /// The Storage Service Interface.
    /// </summary>
    public interface IStorageService<T>
    {
        /// <summary>
        /// Add Entity.
        /// </summary>
        /// <param name="entity">the new entity</param>
        Task AddEntityAsync(T entity);

        /// <summary>
        /// Get Entity By Id.
        /// </summary>
        /// <param name="id">The entity Id</param>
        /// <returns>The entity</returns>
        Task<T> GetEntityAsync(Guid id);
        
    }
}
