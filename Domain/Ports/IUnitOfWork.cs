using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DkHoc.Ports
{
    public interface IUnitOfWork
    {
        void SaveChange(CancellationToken ct = default);
    }
}
