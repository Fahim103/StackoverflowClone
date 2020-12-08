using System;

namespace StackOverflow.Core.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string EntityName { get; private set; }

        public EntityNotFoundException(string message, string entityName)
            : base(message)
        {
            EntityName = entityName;
        }
    }
}
