using System;
using System.Collections.Generic;
using System.Linq;

namespace PictureProduction
{
    class Program
    {
        private readonly static Order[] orders =
        {
            new Order("circle", "red", "Hello", "spacing"),
            new Order("square", "green", "HelloWorld", "spacing"),
            new Order("triangle", "blue", "ChainIsBeauty", "spacing"),

            new Order("circle", "red", "Hello", "uppercase"),
            new Order("square", "green", "HelloWorld", "uppercase"),
            new Order("triangle", "blue", "ChainIsBeauty", "uppercase"),

            new Order("circle", "red", "Hello", "lowercase"),
            new Order("square", "yellow", "HelloWorld", "lowercase"),
            new Order("hash", "red", "ChainIsBeauty", "uppercase"),

            new Order("", "green", "ChainIsBeauty", "uppercase"), //invalid order
            new Order("star", "1234", "ChainIsBeauty", "uppercase"), //invalid order
            new Order("star", "green", null, "uppercase"), //invalid order
        };
        
        static void ProducePictures(IEnumerable<Order> orders, IMachine machine)
        {
            foreach(Order o in orders)
            {
                if (!ValidOrder(o))
                {
                    Console.WriteLine($"Error: Invalid order!");
                    continue;
                }
                Picture picture = new Picture(null, null, null, null);
                machine.Handle(o, picture);
            }
        }

        static bool ValidOrder(Order o)
        {
            if (String.IsNullOrEmpty(o.Color) || String.IsNullOrEmpty(o.Shape) || String.IsNullOrEmpty(o.Text) || String.IsNullOrEmpty(o.Operation))
                return false;
            if (!o.Color.All(Char.IsLetter) || !o.Shape.All(Char.IsLetter) || !o.Text.All(Char.IsLetter) || !o.Operation.All(Char.IsLetter))
                return false;
            return true;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("--- Simple Production Line ---");
            AbstractMachine simple1 = new RedColorMachine();
            AbstractMachine simple2 = new NoColorMachine();
            AbstractMachine simple3 = new SimpleTextMachine();
            AbstractMachine simple4 = new CircleFrameMachine();
            AbstractMachine simple5 = new SquareFrameMachine();
            AbstractMachine simple6 = new LastMachine(true);

            simple1.setNextMachine(simple2);
            simple2.setNextMachine(simple3);
            simple3.setNextMachine(simple4);
            simple4.setNextMachine(simple5);
            simple5.setNextMachine(simple6);

            ProducePictures(orders, simple1);

            Console.WriteLine();

            Console.WriteLine("--- Complex Production Line ---");
            AbstractMachine complex1 = new RedColorMachine();
            AbstractMachine complex2 = new GreenColorMachine();
            AbstractMachine complex3 = new BlueColorMachine();
            AbstractMachine complex4 = new NoColorMachine();
            AbstractMachine complex5 = new SpacingTextMachine();
            AbstractMachine complex6 = new UppercaseTextMachine();
            AbstractMachine complex7 = new SimpleTextMachine();
            AbstractMachine complex8 = new CircleFrameMachine();
            AbstractMachine complex9 = new TriangleFrameMachine();
            AbstractMachine complex10 = new SquareFrameMachine();
            AbstractMachine complex11 = new LastMachine(false);

            complex1.setNextMachine(complex2);
            complex2.setNextMachine(complex3);
            complex3.setNextMachine(complex4);
            complex4.setNextMachine(complex5);
            complex5.setNextMachine(complex6);
            complex6.setNextMachine(complex7);
            complex7.setNextMachine(complex8);
            complex8.setNextMachine(complex9);
            complex9.setNextMachine(complex10);
            complex10.setNextMachine(complex11);

            ProducePictures(orders, complex1);
        }
    }
}
