using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort
{
    class Methods
    {
        public void Swap(ref int i, ref int j)
        {
            int z = i;
            i = j;
            j = z;
        }
    }
}
