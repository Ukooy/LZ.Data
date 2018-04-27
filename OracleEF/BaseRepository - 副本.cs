using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class BaseRepository<T> where T : class
    {
        private DbContext db;
        private readonly DbSet<T> dbSet;
        public BaseRepository()
        {
            this.db = DbContextFactory.GetCurrentDbContext();
            this.dbSet = db.Set<T>();
        }

        public T Add(T entity)
        {
            db.Entry<T>(entity).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return entity;
        }
        public int Count(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().Count(predicate);
        }

        public bool Update(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        public bool Delete(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            return db.SaveChanges() > 0;
        }

        public bool Exist(Expression<Func<T, bool>> anyLambda)
        {
            return db.Set<T>().Any(anyLambda);
        }

        public T Find(Expression<Func<T, bool>> whereLambda)
        {
            T _entity = db.Set<T>().FirstOrDefault<T>(whereLambda);
            return _entity;
        }

        public IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)
        {
            var _list = db.Set<T>().Where<T>(whereLamdba);
            if (isAsc) _list = _list.OrderBy<T, S>(orderLamdba);
            else _list = _list.OrderByDescending<T, S>(orderLamdba);
            return _list;
        }

        public IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)
        {
            var _list = db.Set<T>().Where<T>(whereLamdba);
            totalRecord = _list.Count();
            if (isAsc) _list = _list.OrderBy<T, S>(orderLamdba).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            else _list = _list.OrderByDescending<T, S>(orderLamdba).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            return _list;
        }

       
    }
}
