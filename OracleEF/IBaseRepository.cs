using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LZ.Data.OracleEF
{
    /// <summary>
    /// 接口基类
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public partial interface IBaseRepository<T> where T :class ,new ()
    {
        bool Add(T t);
        void Delete(T t);
        void Update(T t);
        IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> GetModelsByPage<type>(int pageSize, int pageIndex, bool isAsc, Expression<Func<T, type>> OrderByLambda, Expression<Func<T, bool>> WhereLambda);
        IQueryable<T> GetModelsByPage<type>(int pageSize, int pageIndex, bool isAsc, Expression<Func<T, type>> OrderByLambda, Expression<Func<T, bool>> WhereLambda,out int total);//total:总记录条数
        /// <summary>
        /// 一个业务中有可能涉及到对多张表的操作,那么可以将操作的数据,打上相应的标记,最后调用该方法,将数据一次性提交到数据库中,避免了多次链接数据库。
        /// </summary>
        bool SaveChanges();

        IQueryable<T> GetModelsByPage<type>(IQueryable<T> dbs, bool isAsc, Expression<Func<T, type>> OrderByLambda, int pageSize, int pageIndex);

    }
}
