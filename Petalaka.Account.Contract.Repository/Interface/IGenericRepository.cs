﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Petalaka.Account.Contract.Repository.Base;

namespace Petalaka.Account.Contract.Repository.Interface;

public interface IGenericRepository<T> where T : class
{
    T Find(Expression<Func<T, bool>> predicate);
    T FindUndeleted(Expression<Func<T, bool>> predicate);
    Task<T> FindUndeletedAsync(Expression<Func<T, bool>> predicate);
    IQueryable<T> AsQueryable();
    IQueryable<T> AsQueryablePredicate(Expression<Func<T, bool>> predicate);
    IQueryable<T> AsQueryableUndeletedPredicate(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> FindAllUndeleted(Expression<Func<T, bool>> predicate);
    void Insert(T entity);
    Task InsertRangeAsync(IEnumerable<T> entities);
    Task InsertAsync(T entity);
    void SaveChanges();
    Task SaveChangesAsync();
    void Update(T entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
    void DeletePermanent(T entity);
    Task<PaginationResponse<T>> GetPagination(IQueryable<T> queryable, int pageIndex, int pageSize);
}