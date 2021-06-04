using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel_Booking
{
    class Requests
    {
        public uint NumberPersons { get; protected set; }  // количество человек вв номере
        public uint CheckInTime { get; protected set; }   //дата заселения
        public uint CheckOutTime { get; protected set; }   //дата выселения
        public bool RequestsType { get; protected set; }   //тип заявки false -- бронированиек, true -- pfctktybt

        public Requests(bool _RequestsType,uint _NumberPersons, uint _CheckInTime, uint _CheckOutTime)
        {
            RequestsType = _RequestsType;
            NumberPersons = _NumberPersons;
            CheckInTime = _CheckInTime;
            CheckOutTime = _CheckOutTime;
        }

        public Requests(uint _time) //заявка с случайно заданными полями
        {
            Random randT = new Random();
            Random randB = new Random();
            Random randP = new Random();
            Random randType = new Random();
            uint deltaT = Convert.ToUInt32(randT.Next(0, int.MaxValue) % 100) + 5; //длительность проживания
            uint deltaB = Convert.ToUInt32(randB.Next(0, int.MaxValue) % 100) + 1; //время заселения
            NumberPersons = Convert.ToUInt32(randP.Next(0, int.MaxValue) % 6) + 1;  // количество человек
            uint deltaType = Convert.ToUInt32(randType.Next(0, int.MaxValue) % 2);  // количество человек
            if (deltaType == 0)
            {
                RequestsType = false;
                CheckInTime = _time + deltaB;
                CheckOutTime = _time + deltaT + deltaB;
            }
            else
            {
                RequestsType = true;
                CheckInTime = _time;
                CheckOutTime = _time + deltaT;
            }
        }
    }
}
