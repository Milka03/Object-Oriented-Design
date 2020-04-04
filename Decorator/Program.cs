using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomNumber
{
    //Note: for the program to work correctly each part should be run separately
    //i.e. all other parts should be then commented to not re-generate testing sequence
  
    class Program
    {
        static void Main(string[] args)
        {
            IRandomNumberGenerator generator = new RNG(1337);
            IRandomNumberGenerator another = new RNG(210692);

            PrintSequence(generator, 10);
            PrintSequence(another, 10);
            Console.WriteLine();

            //---------------- PART 1 ----------------------
            Console.WriteLine($"Addition");
            generator.Reset();
            generator = new DecoratorAddition(generator, 100);
            PrintSequence(generator, 10);
            Console.WriteLine();

            //---------------- PART 2 ----------------------
            Console.WriteLine($"Multiplication");
            generator.Reset();
            generator = new DecoratorMultiply(generator, 2);
            PrintSequence(generator, 10);
            Console.WriteLine();

            //---------------- PART 3 ----------------------
            Console.WriteLine($"Floor");
            generator.Reset();
            generator = new DecoratorRoundDown(generator);
            PrintSequence(generator, 10);
            Console.WriteLine();

            //---------------- PART 4 ----------------------
            Console.WriteLine($"Ceiling");
            generator.Reset();
            generator = new DecoratorRoundUp(generator);
            PrintSequence(generator, 10);
            Console.WriteLine();

            //---------------- PART 5 ----------------------
            Console.WriteLine($"Merge");
            generator.Reset();
            another.Reset();
            generator = new DecoratorMerge(generator, another);
            PrintSequence(generator, 10);
            Console.WriteLine();

            //---------------- PART 6 ----------------------
            Console.WriteLine($"Merge alternatively");
            generator.Reset();
            another.Reset();
            generator = new DecoratorMergeAlternate(generator, another);
            PrintSequence(generator, 10);
            Console.WriteLine();
            Console.WriteLine();

            //---------------- PART 7 ----------------------
            IRandomNumberGenerator mygenerator = new RNG(1397);
            IRandomNumberGenerator raw2 = new RNG(298742);
            IRandomNumberGenerator result = new RNG(9485);
            Console.WriteLine($"Initial generators");
            PrintSequence(mygenerator, 10);
            PrintSequence(raw2, 10);
            Console.WriteLine();

            mygenerator.Reset();
            mygenerator = new DecoratorAddition(mygenerator, 2);
            mygenerator = new DecoratorMultiply(mygenerator, 3);
            mygenerator = new DecoratorAddition(mygenerator, -2);

            raw2.Reset();
            raw2 = new DecoratorMultiply(raw2, 3);
            raw2 = new DecoratorAddition(raw2, 5);
           
            mygenerator = new DecoratorMerge(mygenerator, raw2);
            mygenerator = new DecoratorRoundDown(mygenerator);

            Console.WriteLine($"Third generator");
            PrintSequence(result, 10);
            result.Reset();
            result = new DecoratorRoundUp(result);
            result = new DecoratorMergeAlternate(mygenerator, result);
            Console.WriteLine();
            Console.WriteLine($"Result rounded up and merged alternatively");
            PrintSequence(result, 10);
            Console.WriteLine();

            //Generator for parts 8-11
            IRandomNumberGenerator test = new RNG(1953);
            Console.WriteLine($"Initial generators");
            PrintSequence(test, 10);
            Console.WriteLine();

            //---------------- PART 8 ----------------------
            Console.WriteLine($"Clamping Filter");
            test.Reset();
            test = new DecoratorClampingFilter(test, 50, 70);
            PrintSequence(test, 10);
            Console.WriteLine();

            //---------------- PART 9 ----------------------
            Console.WriteLine($"Increasing Filter");
            test.Reset();
            test = new DecoratorIncreasingFilter(test);
            PrintSequence(test, 10);
            Console.WriteLine();

            //---------------- PART 10 ----------------------
            Console.WriteLine($"Decreasing Filter");
            test.Reset();
            test = new DecoratorDecreasingFilter(test);
            PrintSequence(test, 10);
            Console.WriteLine();

            //---------------- PART 11 ----------------------
            Console.WriteLine($"Negation");
            test.Reset();
            test = new DecoratorNegation(test);
            PrintSequence(test, 10);
            Console.WriteLine();

        }

        /**
         * This is just a helper method used for printing generator's numbers.
         */
        private static void PrintSequence(IRandomNumberGenerator generator, int count) {
            Console.Write("[");
            for(int i = 0; i < count - 1; ++i) {
                Console.Write("{0}; ", generator.Generate());
            }
            Console.WriteLine("{0}]", generator.Generate());
        }

        
    }
}
