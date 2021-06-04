using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Hotel_Booking
{
    class Hotel : IBank
    {
        public readonly List<Room> roomList;  //список комнат
        
        public Hotel(uint[] countRoom)
        {
            roomList = new List<Room>();
            for (int i = 0; i < countRoom[0]; i++) //0 - одиночные, 1 - двойные, 2 - полулюкс, 3 - люкс
                roomList.Add(new SingleRoom());
            for (int i = 0; i < countRoom[1]; i++)
                roomList.Add(new DoubleRoom());
            for (int i = 0; i < countRoom[2]; i++)
                roomList.Add(new HalfLuxuryRoom());
            for (int i = 0; i < countRoom[3]; i++)
                roomList.Add(new LuxuryRoom());
        }
        public Room BookingRequests(Requests req) 
        {
            for (int i = 0; i < roomList.Count; i++)  //проверка всех комнат
            {
                if (req.NumberPersons <= roomList[i].NumberPersons) //проверка вместимости
                {
                    if (roomList[i].CheckList.Count == 0)  //заселений ещё нет
                    {
                        ChechAndBook toAdd = new ChechAndBook(false,req.CheckInTime,req.CheckOutTime);  //добавляем новое заселение
                        roomList[i].CheckList.Add(toAdd);
                        if (req.NumberPersons == roomList[i].NumberPersons) //если число людей совпадает с размером комнаты
                        {
                            IBank.AddToBank(roomList[i].Cost);  //полная стоимость
                        }
                        else
                        {
                            IBank.AddToBank(Convert.ToUInt32(roomList[i].Cost*0.7));  //70% стоимости
                        }
                        return roomList[i];
                    }
                    else
                    {
                        int j;
                        uint Ts = req.CheckInTime, Tf = req.CheckOutTime;
                        bool ifCan = true;
                        for (j = 0; (j < roomList[i].CheckList.Count) && !(Tf < roomList[i].CheckList[j].CheckInTime); j++)
                            if (((Ts < roomList[i].CheckList[j].CheckInTime) && (Tf > roomList[i].CheckList[j].CheckOutTime)) ||
                                ((roomList[i].CheckList[j].CheckInTime < Ts) && (Ts < roomList[i].CheckList[j].CheckOutTime)) ||
                                ((roomList[i].CheckList[j].CheckInTime < Tf) && (Tf < roomList[i].CheckList[j].CheckOutTime)))
                            {
                                ifCan = false;  //временные отрезки пересекаются
                                break;
                            }
                        if (!ifCan) //если пересекаются
                            continue;
                        if ((j == roomList[i].CheckList.Count) && (Ts > roomList[i].CheckList[j-1].CheckInTime))
                        {
                            ChechAndBook toAdd = new ChechAndBook(false, req.CheckInTime, req.CheckOutTime);
                            roomList[i].CheckList.Add(toAdd);    //добавляем новое заселение
                            if (req.NumberPersons == roomList[i].NumberPersons)   //если число людей совпадает с размером комнаты
                            {
                                IBank.AddToBank(roomList[i].Cost);  //полная стоимость
                            }
                            else
                            {
                                IBank.AddToBank(Convert.ToUInt32(roomList[i].Cost * 0.7));  //70% стоимости
                            }
                        }
                        else
                        {
                            ChechAndBook toAdd = new ChechAndBook(false, req.CheckInTime, req.CheckOutTime);
                            roomList[i].CheckList.Insert(j, toAdd);    //добавляем новое заселение на место j
                            if (req.NumberPersons == roomList[i].NumberPersons)   //если число людей совпадает с размером комнаты
                            {
                                IBank.AddToBank(roomList[i].Cost);  //полная стоимость
                            }
                            else
                            {
                                IBank.AddToBank(Convert.ToUInt32(roomList[i].Cost * 0.7));  //70% стоимости
                            }
                            
                        }
                        return roomList[i];
                    }
                }
            }
            return null;  //не получилось заселить
        }

        public Room CheckInRequests(Requests req)
        {
            for (int i = 0; i < roomList.Count; i++)  //проверка всех комнат
            {
                if (req.NumberPersons <= roomList[i].NumberPersons)  //проверка вместимости
                {
                    if (roomList[i].CheckList.Count == 0)  //заселений ещё нет
                    {
                        ChechAndBook toAdd = new ChechAndBook(true, req.CheckInTime, req.CheckOutTime);
                        roomList[i].CheckList.Add(toAdd);  //добавляем новое заселение
                        if (req.NumberPersons == roomList[i].NumberPersons)  //если число людей совпадает с размером комнаты
                        {
                            IBank.AddToBank(roomList[i].Cost);  //полная стоимость
                        }
                        else
                        {
                            IBank.AddToBank(Convert.ToUInt32(roomList[i].Cost * 0.7));  //70% стоимости
                        }
                        return roomList[i];
                    }
                    else
                    {
                        int j;
                        uint Ts = req.CheckInTime, Tf = req.CheckOutTime;
                        bool ifCan = true;

                        for (j = 0; (j < roomList[i].CheckList.Count) && !(Tf < roomList[i].CheckList[j].CheckInTime); j++)
                            if (((Ts < roomList[i].CheckList[j].CheckInTime) && (Tf > roomList[i].CheckList[j].CheckOutTime)) ||
                                ((roomList[i].CheckList[j].CheckInTime < Ts) && (Ts < roomList[i].CheckList[j].CheckOutTime)) ||
                                ((roomList[i].CheckList[j].CheckInTime < Tf) && (Tf < roomList[i].CheckList[j].CheckOutTime)))
                            {
                                ifCan = false;  //временные отрезки пересекаются
                                break;
                            }
                        if (!ifCan)
                            continue;
                        if ((j == roomList[i].CheckList.Count) && (Ts > roomList[i].CheckList[j-1].CheckInTime))
                        {
                            ChechAndBook toAdd = new ChechAndBook(true, req.CheckInTime, req.CheckOutTime);
                            roomList[i].CheckList.Add(toAdd);  //добавляем новое заселение
                            if (req.NumberPersons == roomList[i].NumberPersons)  //если число людей совпадает с размером комнаты
                            {
                                IBank.AddToBank(roomList[i].Cost);  //полная стоимость
                            }
                            else
                            {
                                IBank.AddToBank(Convert.ToUInt32(roomList[i].Cost * 0.7));  //70% стоимости
                            }
                        }
                        else
                        {
                            ChechAndBook toAdd = new ChechAndBook(true, req.CheckInTime, req.CheckOutTime);
                            roomList[i].CheckList.Insert(j, toAdd);  //добавляем новое заселение на место j
                            if (req.NumberPersons == roomList[i].NumberPersons)  //если число людей совпадает с размером комнаты
                            {
                                IBank.AddToBank(roomList[i].Cost);  //полная стоимость
                            }
                            else
                            {
                                IBank.AddToBank(Convert.ToUInt32(roomList[i].Cost * 0.7));  //70% стоимости
                            }

                        }
                        return roomList[i];
                    }
                }
            }
            return null;  //не получилось заселить
        }

    }
}
