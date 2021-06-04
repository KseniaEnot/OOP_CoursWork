using System;
using System.Collections.Generic;
using System.Text;

namespace HamiltonianСycle
{
    public class Graph
    {
        public readonly int[,] GraphMatrix;
        public readonly int GraphSize;
        public Graph(int[,] matrix) //задание матрицей
        {
            if (matrix == null)
            {
                throw new ArgumentNullException("Ошибка передаваемой матрицы");
            }
            if (matrix.GetLength(0)>10)
            {
                throw new ArgumentOutOfRangeException("Задана слишком большая матрица");
            }
            GraphSize = matrix.GetLength(0);
            GraphMatrix = new int[GraphSize, GraphSize];
            for (int i = 0; i < GraphSize; i++)  
                for (int j = 0; j < GraphSize; j++)
                    GraphMatrix[i, j] = matrix[i, j];  //копирование матрицы
        }
        public string GamAlg()  //поиск Гамильтоновых циклов
        {
            string[,] A = new string[GraphSize, GraphSize];  //матрица смежности
            string[,] B = new string[GraphSize, GraphSize];  //модифицированная матрица смежности
            for (int i = 0; i < GraphSize; i++)
            {
                for (int j = 0; j < GraphSize; j++)
                {
                    if (GraphMatrix[i, j] == 0) //преобразование исходной матрицы
                    {
                        A[i, j] = "F";  //пустой символ
                        B[i, j] = "F";
                    }
                    else
                    {
                        A[i, j] = Convert.ToString(GraphMatrix[i, j]);
                        B[i, j] = "+" + Convert.ToString(j);
                    }
                }
            }
            string[,] res = new string[GraphSize, GraphSize];
            for (int i = 0; i < GraphSize; i++)  //первое перемножение матриц
            {
                for (int j = 0; j < GraphSize; j++)
                {
                    res[i, j] = "";
                    for (int k = 0; k < GraphSize; k++)
                    {
                        if (A[k, j] == "1")
                        {
                            if (B[i, k] != "F")  // не пустой символ
                            {
                                res[i, j] += B[i, k];
                            }
                        }
                    }
                    if (res[i, j] == "")  //не произведены операции
                        res[i, j] = "F";
                }
            }
            A = res;
            for (int j = 0; j < GraphSize; j++)  //обнуление главной диагонали
                A[j, j] = "F";
            for (int k = 2; k < GraphSize; k++)
            {
                A = Mupl(B, A);
            }

                    
            for (int i = 0; i < GraphSize; i++)
            {
                if (A[i, i] != "F")  //поиск ячейки с результатом на главной диагонали
                {
                    return Convert.ToString(i) + A[i, i].Substring(1, GraphSize - 1);  //вырезаем путь
                }
            }
            return null;  //не удалось найти гамильтонов цикл
        }

        private string[,] Mupl(string[,] Arr1, string[,] Arr2)  //перемножение матриц вершин
        {
            string[,] res = new string[GraphSize, GraphSize];
            for (int i = 0; i < GraphSize; i++)
            {
                for (int j = 0; j < GraphSize; j++)
                {
                    res[i, j] = "";
                    for (int k = 0; k < GraphSize; k++)
                    {
                        /*ячейки не пустые и не содержат вершин i и j*/
                        if ((Arr1[i, k] != "F") && (Arr2[k, j] != "F") && !Arr1[i, k].Contains(Convert.ToString(i)) && !Arr1[i, k].Contains(Convert.ToString(j)) && !Arr2[k, j].Contains(Convert.ToString(i)) && !Arr2[k, j].Contains(Convert.ToString(j)))
                        {
                            res[i, j] += "+" + Arr2[k, j].Replace("+", Arr1[i, k]);
                            res[i, j]=res[i, j].Replace("++", "+");  //удаление лишнего +
                        }
                    }
                    if (res[i, j] == "")  //не произведены операции
                        res[i, j] = "F";
                }
            }
            return res;
        }
    }
}
