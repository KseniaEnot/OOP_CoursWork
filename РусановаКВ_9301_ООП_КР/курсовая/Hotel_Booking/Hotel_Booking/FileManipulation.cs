using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Hotel_Booking
{
    class FileManipulation
    {
        public static void SaveToFile(Hotel hotel, string filename)
        {
            StreamWriter file = new StreamWriter(filename);  //открытие файла для записи
            for (int i = 0; i < hotel.roomList.Count; i++)  //идём по всем комнатам
            {
                file.WriteLine($"{i} комната типа {hotel.roomList[i].GetType().ToString()}");
                for (int j = 0; j < hotel.roomList[i].CheckList.Count; j++)  //записываем все забронированные промежутки
                {
                    file.WriteLine($"время заселения {hotel.roomList[i].CheckList[j].CheckInTime}, время выезда {hotel.roomList[i].CheckList[j].CheckOutTime},тип заявки {hotel.roomList[i].CheckList[j].CheckIn}");
                }
            }
            file.Close();  //закрытие файла
        }
    }
}
