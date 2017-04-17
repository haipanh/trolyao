using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Service.Contract
{
    [Serializable]
    public class DtoBase
    {
        public int Id { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
