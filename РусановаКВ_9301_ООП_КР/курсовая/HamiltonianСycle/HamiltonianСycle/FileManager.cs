using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HamiltonianСycle
{
    static class FileManager
    {
        public static int[,] ReadMatrix(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("Файл не существует");
            StreamReader file = new StreamReader(fileName);  //открываем поток для чтения
            try
            {
                int size = Convert.ToInt32(file.ReadLine());  //размер матрицы
                if (size <= 0)
                {
                    throw new ArgumentOutOfRangeException("Не корректный размер матрицы");
                }
                int[,] matrix = new int[size, size];
                for (int i = 0; i < size; i++)  //считываем элементы матрицы
                {
                    string line = file.ReadLine();
                    line = line.Trim();
                    string[] bits = line.Split(' ');
                    for (int j = 0; j < size; j++)
                        matrix[i, j] = Convert.ToInt32(bits[j]);
                }
                file.Close();  //закрытие файла
                return matrix;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }

        public static void WriteString(string fileName, string line)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("Файл не существует");
            StreamWriter file = new StreamWriter(fileName);  //открытие потока для записи 

            file.WriteLine(line);  //запись
            file.Close();  //закрытие файла
        }
    }
}
