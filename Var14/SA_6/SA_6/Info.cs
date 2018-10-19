using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA_6
{
    class Info
    {
        public Info(double price, double mark)
        {
            Price = price;
            Mark = mark;
        }

        public double Price { get; set; }
        public double Mark { get; set; }
    }
}
