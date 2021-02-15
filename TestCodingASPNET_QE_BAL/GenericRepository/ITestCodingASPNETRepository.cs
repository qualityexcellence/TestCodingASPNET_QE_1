using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TestCodingASPNET_QE_BAL
{
    public interface ITestCodingASPNETRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        bool Exists(Expression<Func<T, bool>> filter);
        T GetById(int? id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int? id);
    }
}