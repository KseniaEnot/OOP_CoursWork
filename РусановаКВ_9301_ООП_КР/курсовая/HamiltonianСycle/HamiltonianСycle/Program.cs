using System;
using System.Collections.Generic;

namespace HamiltonianСycle
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Введите название файла для чтения графа: ");
                string fileName = Console.ReadLine();  //чтение имени файла
                int[,] matrix = FileManager.ReadMatrix(fileName);  //получение матрицы графа
                Graph graph = new Graph(matrix);  //задание матрицы
                Console.WriteLine("Гамильтонов путь: " + graph.GamAlg());  //вывод результата
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);  //вывод ошибок
            }
        }
    }
}
