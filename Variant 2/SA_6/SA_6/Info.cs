using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA_6
{
    class Info
    {
        public Info(params double[] values)
        {
            Values = values;
        }

        public double[] Values { get; set; }
    }
}
