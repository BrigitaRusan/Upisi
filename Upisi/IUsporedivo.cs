using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upisi
{
    internal interface IUsporedivo<T>
    {
        int Usporedi(T other);
    }
}
