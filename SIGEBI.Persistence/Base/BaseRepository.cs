using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Repository;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Persistence.Base
{
    public  abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly SIGEBIContext _context;
        protected DbSet<TEntity> _entities;
        public BaseRepository(SIGEBIContext context) { 
            this._context = context;
            _entities = _context.Set<TEntity>();
        }

        public async virtual Task<bool> Exists(Expression<Func<TEntity, bool>> filter)
        {
            return await _entities.AnyAsync(filter);
        }

        public async virtual Task<OperationResult> GetAll()
        {
            var result = new OperationResult();
            try
            {
                result.Data = await _entities.ToListAsync();
            }
            catch (Exception ex) 
            { 
            result.Success = false;
                result.Message = "Error: " + ex.Message ;
            }

            return result;
        }

        public async virtual Task<OperationResult> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            var result = new OperationResult();
            try { 
                result.Data = await _entities.Where(filter).ToListAsync();
            }
            catch (Exception ex) 
            { 
                result.Success = false;
                result.Message = "Error: " + ex.Message ;
            }
            return result;
        }

        public async virtual Task<OperationResult> GetEntityBy(int Id)
        {
            var result = new OperationResult();
            try
            {
                result.Data = await _entities.FindAsync(Id);
            }
            catch (Exception ex) 
            { 
                result.Success = false;
                result.Message = "Error: " + ex.Message ;
            }
            return result;
        }

        public async virtual Task<OperationResult> Remove(TEntity entity)
        {
            var result = new OperationResult();
            try
            {
                _entities.Remove(entity);
                await _context.SaveChangesAsync();
                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error: " + ex.Message;
            }
            return result;
        }

        public async virtual Task<OperationResult> Save(TEntity entity)
        {
            OperationResult result = new OperationResult();

            _entities.Add(entity);
            await _context.SaveChangesAsync();

            return result;
        }

        public async virtual Task<OperationResult> Update(TEntity entity)
        {
            OperationResult result = new OperationResult();

            _entities.Update(entity);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
