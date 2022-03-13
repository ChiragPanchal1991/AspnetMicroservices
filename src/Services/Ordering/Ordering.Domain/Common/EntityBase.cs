using System;

namespace Ordering.Domain.Common
{
    /// <summary>
    /// This class will use for provide common field for entities 
    /// </summary>
    public abstract class EntityBase
    {
        public int Id { get; protected set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
