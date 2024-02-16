
using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess;

    public interface IAsyncRepository
        <TEntity, TEntityId> : IQuery<TEntity>
    where TEntity : BaseEntity<TEntityId>

    {
        Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
       


    }


