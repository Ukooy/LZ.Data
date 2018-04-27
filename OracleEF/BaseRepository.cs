using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LZ.Data.OracleEF
{
    /// <summary>
    /// 基本的仓储，封装增删该查
    /// </summary>
    /// <typeparam name=""></typeparam>
    public class BaseRepository<T> where T : class, new()
    {
        private DbContext dbContext = DbContextFactory.GetCurrentDbContext();
        public bool Add(T t)
        {
            dbContext.Set<T>().Add(t);
            return SaveChanges();
        }
        public void Delete(T t)
        {
            dbContext.Set<T>().Remove(t);
        }

        public void Update(T t)
        {
            dbContext.Set<T>().AddOrUpdate(t);
        }

        public IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda)
        {
            return dbContext.Set<T>().Where(whereLambda);
        }

        public IQueryable<T> GetModelsByPage<type>(int pageSize, int pageIndex, bool isAsc,
            Expression<Func<T, type>> OrderByLambda, Expression<Func<T, bool>> WhereLambda)
        {
            //是否升序
            if (isAsc)
            {
                return dbContext.Set<T>().Where(WhereLambda).OrderBy(OrderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                return dbContext.Set<T>().Where(WhereLambda).OrderByDescending(OrderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
        }

        public IQueryable<T> GetModelsByPage<type>(int pageSize, int pageIndex, bool isAsc,
             Expression<Func<T, type>> OrderByLambda, Expression<Func<T, bool>> WhereLambda, out int total)
        {
            total = dbContext.Set<T>().Where(WhereLambda).Count();
            //是否升序
            if (isAsc)
            {
                return dbContext.Set<T>().Where(WhereLambda).OrderBy(OrderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                return dbContext.Set<T>().Where(WhereLambda).OrderByDescending(OrderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
        }
        public IQueryable<T> GetModelsByPage<type>(IQueryable<T> dbs, bool isAsc,
            Expression<Func<T, type>> OrderByLambda, int pageSize, int pageIndex)
        {
            //是否升序
            if (isAsc)
            {
                return dbs.OrderBy(OrderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                return dbs.OrderByDescending(OrderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
        }
        public bool SaveChanges()
        {
            try
            {
                int i = dbContext.SaveChanges();
                return (i >= 0) ? true : false;
            }
            catch(Exception e)
            {
                return false;
            }
        }

    }
}
