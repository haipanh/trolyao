using Solar.Core;

namespace Application.Domain
{
    public class Admin : DomainEntity<int>
    {
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
    }
}
