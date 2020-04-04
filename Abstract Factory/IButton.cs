using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlatform.Interfaces
{
	public interface IButton
	{
		string Content { set; }
		void DrawContent();
		void ButtonPressed();
	}

    //--------------- Concrete Products Implementation --------------

    public class iOSButton : IButton
    {
        private string content;

        public string Content { set { content = value; } }

        public void DrawContent()
        {
            Console.WriteLine($"{content}");
        }

        public void ButtonPressed()
        {
            Console.WriteLine($"IOS Button pressed, content - {content}");
        }

        public iOSButton() { Console.WriteLine($"iOS Button created"); }
    }


    public class WindowsButton : IButton
    {
        private string content;

        public string Content { set { content = value.ToUpper(); } }

        public void DrawContent()
        {
            Console.WriteLine($"{content}");
        }

        public void ButtonPressed()
        {
            Console.WriteLine($"Windows button pressed");
        }

        public WindowsButton() { Console.WriteLine($"Windows Button created"); }
    }


    public class AndroidButton : IButton
    {
        private string content;

        public string Content
        {
            set
            {
                if (value.Length <= 8) content = value;
                else content = value.Substring(0, 8);
            }
        }

        public void DrawContent()
        {
            Console.WriteLine($"{content}");
        }

        public void ButtonPressed()
        {
            Console.WriteLine($"Sweet {content}!");
        }

        public AndroidButton() { Console.WriteLine($"Android Button created"); }
    }

}

