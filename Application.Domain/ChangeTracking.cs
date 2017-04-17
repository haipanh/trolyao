using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Solar.Core;

namespace Application.Domain
{
    public class ChangeTracking : DomainEntity<int>
    {
        public virtual string NiceName { get; set; }
        public virtual string FieldName { get; set; }
        public virtual string OldValue { get; set; }
        public virtual string NewValue { get; set; }
        public virtual string GetChanges()
        {
            return string.Format("Changed {0} from {1} to {2} on {3}", NiceName, OldValue, NewValue, ModifiedDate);
        }
    }
}
