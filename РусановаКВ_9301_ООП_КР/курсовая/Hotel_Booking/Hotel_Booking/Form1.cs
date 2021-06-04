using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Booking
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        uint time = 24; //время имитации
        uint time_disp = 24; //отображаемое время
        uint delta_disp = 0;
        uint scale;
        bool Moving = false;  //флаг запуска таймера
        uint endTime;  //время конца имитации
        Hotel hotel;
        private SortedList<uint, Requests> pendingRequests; //ожидающие заявки сортированные по порядку поступления
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;  //отключение кнопки старта
            button2.Enabled = true;   //всключение кнопок паузы и сохранения
            button3.Enabled = true;
            Moving = true;  //переключение флага таймера
            endTime = Convert.ToUInt32(comboBox2.SelectedItem)*24; //определение конечного времени
            scale = Convert.ToUInt32(comboBox1.SelectedItem);  //определение масштаба шага
            timer1.Interval = (int)(timer1.Interval/scale);
			// создание массива количества номеров каждого типа
            uint[] arr = { Convert.ToUInt32(numericUpDown1.Value), Convert.ToUInt32(numericUpDown2.Value), Convert.ToUInt32(numericUpDown3.Value), Convert.ToUInt32(numericUpDown4.Value) };
            CreateReq();  //создание списка заявок
            hotel = new Hotel(arr);
            listViewRequests.Items.Clear();
            comboBox1.Enabled = false;  //блокировка элементов управления
            comboBox2.Enabled = false;
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            numericUpDown3.Enabled = false;
            numericUpDown4.Enabled = false;
            labelTime.Text = time / 24 + " " + time % 24 + ":00"; //отображение времени
            timer1.Start();  //таймер для дискретного шага
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Requests newReq;
            string[] row;
            ListViewItem liv;
            while (pendingRequests.TryGetValue(time,out newReq))  //пока есть заявка на настоящее время
            {
                if (newReq.RequestsType == false)  //заявка на бронирование
                    row = new string[] { "Бронирование", time / 24 + " " + time % 24 + ":00", newReq.CheckInTime / 24 + " " + newReq.CheckInTime % 24 + ":00", newReq.CheckOutTime / 24 + " " + newReq.CheckOutTime % 24 + ":00", newReq.NumberPersons.ToString() };
                else   //заявка на заселение
                    row = new string[] { "Заселение", time / 24 + " " + time % 24 + ":00", newReq.CheckInTime / 24 + " " + newReq.CheckInTime % 24 + ":00", newReq.CheckOutTime / 24 + " " + newReq.CheckOutTime % 24 + ":00", newReq.NumberPersons.ToString() };
                liv = new ListViewItem(row);  //обновление информации в списках
                liv.Tag = newReq;
                listViewRequests.Items.Insert(0, liv);
                Room roomForReq = hotel.BookingRequests(newReq);
                if (roomForReq != null)  //удалось заселить
                {
                    //поля для добавления в таблицу
                    if (roomForReq.GetType().ToString() == typeof(LuxuryRoom).ToString()) 
                    {
                        row = new string[] { "Люкс", newReq.CheckInTime / 24 + " " + newReq.CheckInTime % 24 + ":00", newReq.CheckOutTime / 24 + " " + newReq.CheckOutTime % 24 + ":00", newReq.NumberPersons.ToString() };
                    }
                    else if (roomForReq.GetType().ToString() == typeof(HalfLuxuryRoom).ToString())
                    {
                        row = new string[] { "Полулюкс", newReq.CheckInTime / 24 + " " + newReq.CheckInTime % 24 + ":00", newReq.CheckOutTime / 24 + " " + newReq.CheckOutTime % 24 + ":00", newReq.NumberPersons.ToString() };
                    }
                    else if (roomForReq.GetType().ToString() == typeof(DoubleRoom).ToString())
                    {
                        row = new string[] { "Двухместный", newReq.CheckInTime / 24 + " " + newReq.CheckInTime % 24 + ":00", newReq.CheckOutTime / 24 + " " + newReq.CheckOutTime % 24 + ":00", newReq.NumberPersons.ToString() };
                    }
                    else if (roomForReq.GetType().ToString() == typeof(SingleRoom).ToString())
                    {
                        row = new string[] { "Одноместный", newReq.CheckInTime / 24 + " " + newReq.CheckInTime % 24 + ":00", newReq.CheckOutTime / 24 + " " + newReq.CheckOutTime % 24 + ":00", newReq.NumberPersons.ToString() };
                    }
                    liv = new ListViewItem(row);
                    liv.Tag = roomForReq;
                    listViewRoomNow.Items.Insert(0,liv); //добавление в начало таблицы
                    row = new string[] { time / 24 + " " + time % 24 + ":00", IBank.DispBankAccount().ToString() };
                    liv = new ListViewItem(row);
                    liv.Tag = roomForReq;
                    listViewBank.Items.Insert(0, liv);  //добавление в начало таблицы
                }

                pendingRequests.Remove(time);  //удаление заявки из ожидающийх
            }
            time++;  //следующий щаг
            delta_disp++;
            if (delta_disp == scale)
            {
                time_disp += delta_disp;
                labelTime.Text = time_disp / 24 + " " + time_disp % 24 + ":00";
                delta_disp = 0;
            }
            if (time == endTime) //дошли до конца имитации
            {
                timer1.Stop();
            }
        }

        private void CreateReq()
        {
            pendingRequests = new SortedList<uint, Requests>();
            Random randStep = new Random();
            uint _time = 24;
            uint step;
            while (_time < endTime)
            {
                pendingRequests.Add(_time, new Requests(_time));
                step = Convert.ToUInt32(randStep.Next(0, int.MaxValue) % 5) + 1; //шаг до следующей заявки
                _time += step;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FileManipulation.SaveToFile(hotel,"save.txt");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Moving = !Moving;
            if (Moving==false) //смена состояния таймера
            {
                timer1.Stop();
            }
            else
            {
                timer1.Start();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }
    }
}
