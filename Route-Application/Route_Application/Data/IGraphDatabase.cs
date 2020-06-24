//This file contains fragments that You have to fulfill
// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Emilia Wróblewska

using BigTask2.Api;
using System.Collections.Generic;

namespace BigTask2.Data
{
    public interface IGraphDatabase
    {
        //Fill the return type of the method below
        IEnumerable<Route> GetRoutesFrom(City from);
        City GetByName(string cityName);

        void AddRoute(City from, City to, double cost, double travelTime, VehicleType vehicle);
        //Getting Iterator for database
        IGraphIterator CreateIterator();
    }

    //----------------- Iterator ---------------------
    public interface IGraphIterator
    {
        Route Next();
        Route First();
        Route CurrentItem();
    }


}
