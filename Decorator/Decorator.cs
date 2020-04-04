using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomNumber
{
    class Decorator : IRandomNumberGenerator
    {
        public IRandomNumberGenerator RandNum;

        public Decorator(IRandomNumberGenerator rand)
        {
            RandNum = rand;
        }

        public virtual double Generate()
        {
            return RandNum.Generate();
        }

        public virtual void Reset()
        {
            RandNum.Reset();
        }
    }


    class DecoratorAddition : Decorator
    {
        public double addition;

        public DecoratorAddition(IRandomNumberGenerator rand, double add) : base(rand) {
            addition = add;
        }

        public override double Generate()
        {
            return base.Generate() + addition;
        }

    }

    class DecoratorMultiply : Decorator
    {
        public double multiply;

        public DecoratorMultiply(IRandomNumberGenerator rand, double p) : base(rand)
        {
            multiply = p;
        }

        public override double Generate()
        {
            return base.Generate() * multiply;
        }

    }

    class DecoratorRoundDown : Decorator
    {
        public DecoratorRoundDown(IRandomNumberGenerator rand) : base(rand) { }

        public override double Generate()
        {
            return Math.Floor(base.Generate());
        }
    }
        

    class DecoratorRoundUp : Decorator
    {
        public DecoratorRoundUp(IRandomNumberGenerator rand) : base(rand) { }

        public override double Generate()
        {
            return Math.Ceiling(base.Generate());
        }
    }

    class DecoratorMerge : Decorator
    {
        IRandomNumberGenerator r1;
        public DecoratorMerge(IRandomNumberGenerator rand, IRandomNumberGenerator r) : base(rand) {
            r1 = r;
        }

        public override double Generate()
        {
            double tmp1 = r1.Generate();
            double tmp2 = base.Generate();
            if (tmp1 > tmp2)
                return tmp1 ;
            else return tmp2;
        }
    }


    class DecoratorMergeAlternate : Decorator
    {
        IRandomNumberGenerator r1;
        static int counter = 0;
        public DecoratorMergeAlternate(IRandomNumberGenerator rand, IRandomNumberGenerator r) : base(rand)
        {
            r1 = r;
        }

        public override double Generate()
        {
            counter++;
            double tmp1 = r1.Generate();
            double tmp2 = base.Generate();
            if (counter%2 == 0)
                return tmp2;
            else return tmp1;
        }
    }


    class DecoratorOperations : Decorator
    {
        public DecoratorOperations(IRandomNumberGenerator rand) : base(rand) { }

        public override double Generate()
        {
            return base.Generate();
        }
    }



    class DecoratorClampingFilter : Decorator
    {
        public double MinValue;
        public double MaxValue;

        public RangeFilter(IRandomNumberGenerator rand, double min, double max) : base(rand)
        {
            MinValue = min;
            MaxValue = max;
        }

        public override double Generate()
        {
            double tmp = base.Generate();
            if (tmp <= MinValue) return MinValue;
            if (tmp >= MaxValue) return MaxValue;
            return tmp;
        }
    }


    class DecoratorIncreasingFilter : Decorator
    {
        public static double lastValue = 0;
        public DecoratorIncreasing(IRandomNumberGenerator r) : base(r) { /*lastValue = base.Generate();*/ }

        public override double Generate()
        {
            double test = base.Generate();
            while (test < lastValue)
            {
                test = base.Generate();
            }
            lastValue = test;
            return test;
        }
    }


    class DecoratorDecreasingFilter : Decorator
    {
        public static double LastValue = 100;
        public DecoratorDecreasing(IRandomNumberGenerator r) : base(r) { /*LastValue = base.Generate();*/ }

        public override double Generate()
        {
            double test = base.Generate();
            while (test > LastValue)
            {
                test = base.Generate();
            }
            LastValue = test;
            return test;
        }
    }



    class DecoratorNegation : Decorator
    {
        static int counter = 0;
        public DecoratorNegation(IRandomNumberGenerator rand) : base(rand) { }

        public override double Generate()
        {
            counter++;
            double tmp = base.Generate();
            if(counter%2 == 0)
            {
                return -tmp;
            }
            return tmp;
        }

    }


}
