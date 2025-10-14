using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Application.Models
{
    public abstract class SmartDevice
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        
        public bool IsOn { get; set; }

        public string GetStatus {  get; set; }
        public SmartDevice()
        {
            Id = Guid.NewGuid();
        }
    }
}
