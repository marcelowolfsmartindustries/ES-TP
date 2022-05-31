using System;
using System.ComponentModel.DataAnnotations;

namespace ES_TP.Data.Entities
{
        public class BaseEntity
        {
            public BaseEntity()
            {
                IsActive = true;
                IsSystem = false;
                IsRemoved = false;
            }

            public Guid Id { get; set; }
            public virtual DateTime CreatedAt { get; set; }
            public virtual string CreatedBy { get; set; }
            public virtual DateTime? UpdatedAt { get; set; }
            public virtual string UpdatedBy { get; set; }
            [Timestamp]
            public byte[] RowVersion { get; set; }
            public virtual bool IsActive { get; set; }
            public virtual bool IsSystem { get; set; }
            public virtual bool IsRemoved { get; set; }
        }
    }

