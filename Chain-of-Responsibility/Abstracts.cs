using System;
using System.Linq;

namespace PictureProduction
{
    interface IMachine  //IHandler
    {
        // you can add required methods here
        void Handle(Order order, IPicture picture);
        void setNextMachine(IMachine machine);
    }

    abstract class AbstractMachine : IMachine
    {
        protected IMachine nextMachine;

        public void setNextMachine(IMachine machine)
        {
            nextMachine = machine;
        }

        public abstract void Handle(Order request, IPicture picture);
    }

    // ------------------ Color Machines --------------------
    class RedColorMachine : AbstractMachine
    {
        public override void Handle(Order request, IPicture picture)
        {
            Picture p = new Picture(picture.LeftFrame, picture.RightFrame, picture.Color, picture.Text);
            if (request.Color == "red") p.Color = "red";
            nextMachine.Handle(request, p);
        }
    }

    class GreenColorMachine : AbstractMachine
    {
        public override void Handle(Order request, IPicture picture)
        {
            Picture p = new Picture(picture.LeftFrame, picture.RightFrame, picture.Color, picture.Text);
            if (request.Color == "green") p.Color = "green";
            nextMachine.Handle(request, p);
        }
    }

    class BlueColorMachine : AbstractMachine
    {
        public override void Handle(Order request, IPicture picture)
        {
            Picture p = new Picture(picture.LeftFrame, picture.RightFrame, picture.Color, picture.Text);
            if (request.Color == "blue") p.Color = "blue";
            nextMachine.Handle(request, p);
        }
    }

    class NoColorMachine : AbstractMachine
    {
        public override void Handle(Order request, IPicture picture)
        {
            Picture p = new Picture(picture.LeftFrame, picture.RightFrame, picture.Color, picture.Text);
            if (picture.Color == null) p.Color = "";
            nextMachine.Handle(request, p);
        }
    }

    // ------------------ Text Machines --------------------
    class SimpleTextMachine : AbstractMachine
    {
        public override void Handle(Order request, IPicture picture)
        {
            Picture p = new Picture(picture.LeftFrame, picture.RightFrame, picture.Color, picture.Text);
            if (picture.Text == null) p.Text = request.Text;
            nextMachine.Handle(request, p);
        }
    }

    class UppercaseTextMachine : AbstractMachine
    {
        public override void Handle(Order request, IPicture picture)
        {
            Picture p = new Picture(picture.LeftFrame, picture.RightFrame, picture.Color, picture.Text);
            if (request.Operation == "uppercase")
            {
                p.Text = request.Text.ToUpper();
            }
            nextMachine.Handle(request, p);
        }
    }

    class SpacingTextMachine : AbstractMachine
    {
        public override void Handle(Order request, IPicture picture)
        {
            Picture p = new Picture(picture.LeftFrame, picture.RightFrame, picture.Color, picture.Text);
            if (request.Operation == "spacing")
            {
                string tmp = request.Text.Aggregate(string.Empty, (c, i) => c + i + ' ');
                p.Text = tmp.Trim(' ');
            }
            nextMachine.Handle(request, p);
        }
    }


    // ------------------ Frame Machines --------------------
    class CircleFrameMachine : AbstractMachine
    {
        public override void Handle(Order request, IPicture picture)
        {
            Picture p = new Picture(picture.LeftFrame, picture.RightFrame, picture.Color, picture.Text);
            if (p.Color == null || p.Text == null)
            {
                nextMachine.Handle(request, p);
                return;
            }
            if (request.Shape == "circle")
            {
                p.LeftFrame = "(";
                p.RightFrame = ")";
            }
            nextMachine.Handle(request, p);
        }
    }


    class TriangleFrameMachine : AbstractMachine
    {
        public override void Handle(Order request, IPicture picture)
        {
            Picture p = new Picture(picture.LeftFrame, picture.RightFrame, picture.Color, picture.Text);
            if (p.Color == null || p.Text == null)
            {
                nextMachine.Handle(request, p);
                return;
            }
            if (request.Shape == "triangle")
            {
                p.LeftFrame = "<";
                p.RightFrame = ">";
            }
            nextMachine.Handle(request, p);
        }
    }


    class SquareFrameMachine : AbstractMachine 
    {
        public override void Handle(Order request, IPicture picture)
        {
            Picture p = new Picture(picture.LeftFrame, picture.RightFrame, picture.Color, picture.Text);
            if (p.Color == null || p.Text == null)
            {
                nextMachine.Handle(request, p);
                return;
            }
            if (request.Shape == "square")
            {
                p.LeftFrame = "[";
                p.RightFrame = "]";
            }
            nextMachine.Handle(request, p);
        }
    }

    class LastMachine : AbstractMachine   //Last machine
    {
        private bool isSimple;

        public LastMachine(bool b) { isSimple = b; }

        public override void Handle(Order request, IPicture picture)
        {
            Picture p = new Picture(picture.LeftFrame, picture.RightFrame, picture.Color, picture.Text);
            if (p.Color == null || p.Text == null || p.LeftFrame == null)
            {
                Console.WriteLine($"Error: Cannot create picture!");
                return;
            }

            if (!isSimple)  //complicated production line
            {
                if (picture.Color == String.Empty && request.Operation == "spacing") p.Thinckness = 3;
                else if (picture.Color == String.Empty || request.Operation == "spacing") p.Thinckness = 2;
                else p.Thinckness = 1;
            }
            p.Print();
        }
    }
}
