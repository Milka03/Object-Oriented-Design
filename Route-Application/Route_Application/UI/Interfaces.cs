//This file Can be modified
// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Emilia Wróblewska

using BigTask2.Api;
using System.Collections.Generic;

namespace BigTask2.Ui
{
    interface IForm
    {
        void Insert(string command);
        bool GetBoolValue(string name);
        string GetTextValue(string name);
        int GetNumericValue(string name);
        void Reset();  //clean up
    }

    interface IDisplay
    {
        void Print(IEnumerable<Route> routes);
    }

    interface ISystem
    {
        IForm Form { get; }
        IDisplay Display { get; }

    }

    //------------------------------------------
    interface IFactory
    {
        IForm CreateForm();
        IDisplay CreateDisplay();
        ISystem CreateSystem(IForm form, IDisplay display);
    }

    
}
