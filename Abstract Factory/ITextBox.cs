using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlatform.Interfaces
{
	public interface ITextBox
	{
		string Content { set; }
		void DrawContent();
	}

    //--------------- Concrete Products Implementation --------------

    public class iOSTextBox : ITextBox
    {
        private string content;

        public string Content { set { content = value; } }

        public void DrawContent()
        {
            Console.WriteLine($"{content}");
        }

        public iOSTextBox() { Console.WriteLine($"iOS TextBox created"); }
    }


    public class WindowsTextBox : ITextBox
    {
        private string content;

        public string Content
        {
            set
            {
                string tmp = value;
                int n;
                if (value.Length % 2 == 0) n = value.Length / 2;
                else n = (value.Length / 2) + 1;
                tmp = value.Substring((int)(value.Length / 2), n);
                tmp += " by .Net Core";
                content = tmp;
            }
        }

        public void DrawContent()
        {
            Console.WriteLine($"{content}");
        }

        public WindowsTextBox() { Console.WriteLine($"Windows TextBox created"); }
    }


    public class AndroidTextBox : ITextBox
    {
        private string content;

        public string Content
        {
            set
            {
                char[] arr = value.ToCharArray();
                Array.Reverse(arr);
                content = new string(arr);
            }
        }

        public void DrawContent()
        {
            Console.WriteLine($"{content}");
        }

        public AndroidTextBox() { Console.WriteLine($"Android TextBox created"); }
    }

}
