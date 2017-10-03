using System;

namespace Core.Domain.Users
{
    public class User
    {
        public Guid ID { get; }

        public User(Guid id)
        {
            ID = id;
        }
    }
}
