using ReadingList.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.Infrastructure.Interfaces
{
    public interface IExportStrategyFactory
    {
        public IExportStrategy Create(ExportType exportType);
    }
}
