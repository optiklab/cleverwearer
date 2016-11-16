/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.Repository
{
    using System;
    using System.Linq;

    /// <summary>
    /// Основной интерфейс репозитория.
    /// </summary>
    /// <typeparam name="T">Тип данных.</typeparam>
    public interface IRepository<T>
    {
        IQueryable<T> Table { get; }

        T GetById(params Object[] ids);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
