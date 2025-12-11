using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCore.Domain
{
    public class Team : BaseDomainModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
