using System;
using System.Collections.Generic;

namespace Hotel_Booking
{
    interface IBank
    {
        private static uint BankAccount = 0;  //сумма находящаяся в банке
        private protected static void AddToBank(uint toAdd)  //пополнение баланса банка
        {
            BankAccount += toAdd;
        }
        public static uint DispBankAccount() //отображение баланса
        {
            return BankAccount;
        }
    }
}
