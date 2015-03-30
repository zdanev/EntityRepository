using System;

namespace EntityRepository.Models
{
    public abstract class TrackedEntity : Entity
    {
        public bool IsDeleted { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreateTime { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }
    }
}