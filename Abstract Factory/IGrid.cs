using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlatform.Interfaces
{
	public interface IGrid
	{
		void AddButton(IButton button);

		void AddTextBox(ITextBox textBox);

		IEnumerable<IButton> GetButtons();

		IEnumerable<ITextBox> GetTextBoxes();
	}


    //--------------- Concrete Products Implementation --------------

    public class iOSGrid : IGrid
    {
        private List<IButton> buttons = new List<IButton>();
        private List<ITextBox> textBoxes = new List<ITextBox>();

        public iOSGrid() { Console.WriteLine($"iOS Grid created"); }

        public void AddButton(IButton button) { buttons.Add(button); }

        public void AddTextBox(ITextBox textBox) { textBoxes.Add(textBox); }

        public IEnumerable<IButton> GetButtons() => buttons;

        public IEnumerable<ITextBox> GetTextBoxes() => textBoxes;
    
    }


    public class WindowsGrid : IGrid
    {
        private List<IButton> buttons = new List<IButton>();
        private List<ITextBox> textBoxes = new List<ITextBox>();

        public WindowsGrid() { Console.WriteLine($"Windows Grid created"); }

        public void AddButton(IButton button) { buttons.Add(button); }

        public void AddTextBox(ITextBox textBox) { textBoxes.Add(textBox); }

        public IEnumerable<IButton> GetButtons()
        {
            List<IButton> result = new List<IButton>();
            result = buttons;
            result.Reverse();
            return result;
        }

        public IEnumerable<ITextBox> GetTextBoxes()
        {
            List<ITextBox> result = new List<ITextBox>();
            result = textBoxes;
            result.Reverse(1, result.Count() - 1);
            return result;
        }
    }


    public class AndroidGrid : IGrid
    {
        private List<IButton> buttons = new List<IButton>();
        private List<ITextBox> textBoxes = new List<ITextBox>();

        public AndroidGrid() { Console.WriteLine($"Android Grid created"); }

        public void AddButton(IButton button) { buttons.Add(button); }

        public void AddTextBox(ITextBox textBox) { textBoxes.Add(textBox); }

        public IEnumerable<IButton> GetButtons() => buttons;

        public IEnumerable<ITextBox> GetTextBoxes()
        {
            List<ITextBox> result = new List<ITextBox>();
            return result;
        }
    }


}
