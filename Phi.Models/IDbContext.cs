/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/
using System.Data.Entity;

namespace Phi.Models
{
 //: DbContext, IDbContext
 //   {
 //       static phiContext()
 //       {
 //           Database.SetInitializer<phiContext>(null);
 //       }

 //       public phiContext()
 //           : base("Name=phiContext")
 //       {
 //       }

 //       public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
 //       {
 //           return base.Set<TEntity>();
 //       }

    /// <summary>
    /// Интерфейс для имплементации в классе контекста.
    /// </summary>
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();
    }
}
