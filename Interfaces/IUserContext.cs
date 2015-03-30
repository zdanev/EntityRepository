using System;

namespace EntityRepository.Interfaces
{
    public interface IUserContext
    {
        string UserName { get; }

        DateTime CurrentTime { get; }
    }
}