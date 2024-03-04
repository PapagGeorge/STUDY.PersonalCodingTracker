using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplication
    {
        void Run();
        void BulkInsertTextRun();
        void Stop();
    }
}
