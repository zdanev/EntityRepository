using System;

using EntityRepository.Interfaces;

namespace EntityRepository.Components
{
    public class UserContext : IUserContext
    {
        private static IUserContext instance;

        public static IUserContext Current
        {
            get
            {
                instance = instance ?? new UserContext();
                return instance;
            }
        }

        public string UserName
        {
            get { return System.Security.Principal.WindowsIdentity.GetCurrent().Name; }
        }

        public DateTime CurrentTime
        {
            get { return DateTime.UtcNow; }
        }
    }
}