using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomNumber
{
    class RNG : IRandomNumberGenerator
    {
        private const int MAX = 100;

        private readonly int seed;
        
        private Random random;

        public RNG(int seed)
        {
            this.seed = seed;
            this.random = new Random(seed);
        }

        public double Generate()
        {
            return Math.Round(random.NextDouble() * MAX, 2);
        }
        
        public void Reset()
        {
            this.random = new Random(this.seed);
        }
    }
}
