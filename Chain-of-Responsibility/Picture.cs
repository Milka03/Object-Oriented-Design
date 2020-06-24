using System;

namespace PictureProduction
{
    interface IPicture
    {
        string LeftFrame { get; }
        string RightFrame { get; }
        string Color { get; }
        string Text { get; }
        
        void Print();
    }

    public class Picture : IPicture
    {
        public string LeftFrame { get; set; }
        public string RightFrame { get; set; }
        public string Color { get; set; }
        public string Text { get; set; }
        public int Thinckness { get; set; }

        public Picture(string left, string right, string color, string text)
        {
            LeftFrame = left;
            RightFrame = right;
            Color = color;
            Text = text;
            Thinckness = 2;
        }

        public void Print()
        {
            string l = LeftFrame;
            string r = RightFrame;
            for(int i = 0; i < Thinckness - 1; i++)
            {
                LeftFrame += l;
                RightFrame += r;
            }
            Console.WriteLine($"{LeftFrame}{Color}{RightFrame} {Text} {LeftFrame}{Color}{RightFrame}");
        }
    }
}
