using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlatform.Interfaces
{
    public interface IPlatform 
    {
        ITextBox CreateTextBox();
        IGrid CreateGrid();
        IButton CreateButton();
    }

    //--------------- Concrete Factories Implementation --------------

    public class iOS : IPlatform
    { 
        public ITextBox CreateTextBox() { return new iOSTextBox(); }

        public IGrid CreateGrid() { return new iOSGrid(); }

        public IButton CreateButton() { return new iOSButton(); }
    }


    public class Windows : IPlatform
    {
        public ITextBox CreateTextBox() { return new WindowsTextBox(); }

        public IGrid CreateGrid() { return new WindowsGrid(); }

        public IButton CreateButton() { return new WindowsButton(); }
    }


    public class Android : IPlatform
    {
        public ITextBox CreateTextBox() { return new AndroidTextBox(); }

        public IGrid CreateGrid() { return new AndroidGrid(); }

        public IButton CreateButton() { return new AndroidButton(); }
    }


}
