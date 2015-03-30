using System;

using EntityRepository.Models;

namespace EntityRepository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : Entity;

        // TODO: DbTransaction BeginTransaction();

        int Save();
    }
}