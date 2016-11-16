/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.Repository
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;

    using Phi.Models;

    /// <summary>
    /// Репозиторий объектов.
    /// </summary>
    /// <typeparam name="T">Тип доменного объекта.</typeparam>
    public class EfRepository<T> : IRepository<T> where T : class
    {
        #region Private fields

        /// <summary>
        /// Контекст данных.
        /// </summary>
        private readonly IDbContext _context;

        /// <summary>
        /// Набор данных.
        /// </summary>
        private IDbSet<T> _entities;

        #endregion

        #region Ctor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Object context</param>
        public EfRepository(IDbContext context)
        {
            this._context = context;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Возвращает таблицу данных.
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        #endregion

        #region Private properties

        /// <summary>
        /// Возвращает коллекцию данных указанного по T типа.
        /// </summary>
        private IDbSet<T> Entities
        {
            get
            {
                return this._entities ?? (this._entities = this._context.Set<T>());
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Возвращает объект по его Id.
        /// </summary>
        public T GetById(params Object[] ids)
        {
            return this.Entities.Find(ids);
        }

        /// <summary>
        /// Вставляет новый объект.
        /// </summary>
        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.Entities.Add(entity);

                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                // Чтение ошибок валидации делаем как описано в MSDN.
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format(
                            "Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage) + Environment.NewLine;
                    }
                }

                var fail = new Exception(msg, dbEx);
                Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }

        /// <summary>
        /// Обновляет данные объекта. По сути - сохраняет объект.
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format(
                            "Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage) + Environment.NewLine;
                    }
                }

                var fail = new Exception(msg, dbEx);
                Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Удаляет объект.
        /// </summary>
        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.Entities.Remove(entity);

                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format(
                            "Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage) + Environment.NewLine;
                    }
                }

                var fail = new Exception(msg, dbEx);
                Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }

        #endregion
    }
}
