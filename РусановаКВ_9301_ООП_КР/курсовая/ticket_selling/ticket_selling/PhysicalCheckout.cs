using System;
using System.Collections.Generic;

class PhysicalCheckout : Checkout
{
    public readonly DateTime TimeToClose;  //время закрытия

    public PhysicalCheckout(string[] _playsName, uint _rowSize, uint _placeSize, float[] _cost) : base(_playsName, _rowSize, _placeSize, _cost)
    {
        TimeToClose = new DateTime(2021, 6, 1, 18, 0, 0);
    }
}