using System;
using System.Collections.Generic;

interface IBank
{
    private static float BankAccount = 0;  //сумма находящаяся в банке
    private protected static void AddToBank(float toAdd)  //пополнение баланса банка
    {
        BankAccount += toAdd;
    }
}
