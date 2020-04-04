using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomNumber
{
    interface IRandomNumberGenerator
    {
        double Generate();
        
        void Reset();
    }
}
