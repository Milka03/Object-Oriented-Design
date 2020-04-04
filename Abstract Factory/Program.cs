using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlatform.Interfaces
{
	class Program
	{
		private static void BuildUI(IPlatform myplatform)
		{
            IGrid grid = myplatform.CreateGrid();
            IButton button1 = myplatform.CreateButton();
            IButton button2 = myplatform.CreateButton();
            IButton button3 = myplatform.CreateButton();
            button1.Content = "BigPurpleButton";
            button2.Content = "SmallButton";
            button3.Content = "Baton";
            grid.AddButton(button1);
            grid.AddButton(button2);
            grid.AddButton(button3);

            ITextBox text1 = myplatform.CreateTextBox();
            ITextBox text2 = myplatform.CreateTextBox();
            ITextBox text3 = myplatform.CreateTextBox();
            text1.Content = "";
            text2.Content = "EmptyTextBox";
            text3.Content = "xoBtxeT";
            grid.AddTextBox(text1);
            grid.AddTextBox(text2);
            grid.AddTextBox(text3);

            Console.WriteLine($"********");
            foreach(var b in grid.GetButtons())
            {
                b.ButtonPressed();
                b.DrawContent();
            }
            Console.WriteLine($"********");
            foreach (var t in grid.GetTextBoxes())
            {
                t.DrawContent();
            }

        }

        static void Main(string[] args)
		{

			Console.WriteLine("<---------------------iOS--------------------->");
            BuildUI(new iOS());
            Console.WriteLine();

			Console.WriteLine("<---------------------Windows--------------------->");
            BuildUI(new Windows());
            Console.WriteLine();

            Console.WriteLine("<---------------------Android--------------------->");
            BuildUI(new Android());
            Console.WriteLine();
        }
    }
}
