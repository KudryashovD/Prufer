using System;
using System.Collections.Generic;
using System.IO;

namespace MyApp
{
    struct Tree
    {
        public string line;
    }
    struct Code
    {
        public int code;
    }
    internal class Program
    {
        static void WritingFile(string path, List<Tree> V, int n)
        {
            Tree[,] T = new Tree[2, n];
            string[,] lines = new string[2, n];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if(i == 0)
                    {
                        Console.WriteLine("Введите начало ребра [{0}]: ", j);
                        lines[i, j] = Console.ReadLine();
                        T[i, j].line = lines[i, j];
                    }
                    else
                    {
                        Console.WriteLine("Введите конец ребра [{0}]: ", j);
                        lines[i, j] = Console.ReadLine();
                        T[i, j].line = lines[i, j];
                    }
                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        sw.WriteLine(T[i, j].line);
                    }
                }
            }
        }
        static void Coding(string path, List<Tree> V)
        {
            int k = 0;
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.EndOfStream != true)
                {
                    string[] array = sr.ReadLine().Split();
                    V.Add(new Tree()
                    {
                        line = array[0]

                    });
                    k++;
                }
            }
            int h = 0;
            int[,] arr = new int[2, k / 2];
            int sn = 0;
            int sk = 0;
            foreach (Tree t in V)
            {
                if (h < k / 2)
                {
                    if (sn == k/2)
                    {
                        sn = sn - 1;
                    }
                    arr[0, sn] = Convert.ToInt32(t.line);
                    sn++;
                }
                else if (h >= k / 2)
                {
                    arr[1, sk] = Convert.ToInt32(t.line);
                    if (sk <= sn)
                    {
                        sk++;
                    }
                }
                h++;
            }
            Console.WriteLine("Матрица с рёбрами: ");
            for (int i = 0; i < 2; i++)
            {

                for (int j = 0; j < k / 2; j++)
                {
                    Console.Write(arr[i, j]);
                }
                Console.WriteLine();
            }
            int n = k / 2;
            CodingProcess(arr, n);
        }
        static void CodingProcess(int[,] array, int n)
        {
            List<Code> V = new List<Code>();
            int[] mass1 = new int[n];
            int[] mass2 = new int[n];
            int u = 0;
            bool flag = true;
            int roditel = 0;
            int minid = 0;
            for (int j = 0; j < n; j++)
            {
                mass1[j] = array[0, j];
                mass2[j] = array[1, j];
            }
            while (u < n-1)
            {
                int min = 10000;
              
                    for(int i = 0; i < mass2.Length; i++)
            {
                for(int j = 0; j<mass1.Length; j++)
                {
                    if (mass1[j] == mass2[i])
                    {
                            flag = false;
                    }
                }
                if( flag && mass2[i] < min)
                    {
                        min = mass2[i];
                        minid = i;
                        mass2[i] = 0;
                    }
                    flag = true;
                }
roditel = mass1[minid];
                mass1[minid] = 0;
                V.Add(new Code()
                {
                    code = roditel
                }) ;
                u++;
            }
            Console.WriteLine("Код прюфера: ");
            foreach (Code c in V)
            {
                Console.Write(c.code);
            }
            Console.WriteLine("\n");
        }
        static void Decoding(int[] code, int n, int count)
        {
            string path = "Decoding.csv";
            List<int> V = new List<int>();
            List<int> T = new List<int>();
            Console.WriteLine("Код Прюфера: ");
            for(int i = 0; i < n; i++)
            {
                Console.Write(code[i]);
            }
            Console.WriteLine();
            int k = 1;
            int[] points = new int[count];
            Console.WriteLine("Вершины: ");
            for(int i = 0; i < count; i++)
            {
                points[i] = k;
                k++;
                Console.Write(points[i]);    
            }
            Console.WriteLine();
            bool flag = true;
            int rod=0;
            int rodit = 0;
            int d = 0;
            int l = 0;
            for (int j = 0; j < code.Length; j++)
            {
                if (code[j] != 0)
                {
                    V.Add(code[j]);
                }
            }
            while (d<code.Length+1)
            {
               
                for (int i = 0; i < points.Length; i++)
            {
                    int min = 10000;
                    for (int j = 0; j < code.Length; j++)
                {
                    if (points[i] == code[j])
                    {
                        flag = false;
                    }
                }
                if ((flag) && (points[i] < min) && points[i]!=0)
                {
                    min = points[i];
                    points[i] = 0;
                        Console.Write(min);
                        T.Add(min);
                        i = -1;
                        if (l < code.Length-1)
                        {
                            code[l] = 0;
                            l++;
                        }
                    }
                    flag = true;
                }
                
                d++;
        }
            V.Add(code[code.Length - 1]) ;
            int itemdelete = 0;
            Console.WriteLine("\nРёбра: ");
            T.RemoveAll(x => x == itemdelete);
            foreach (int X in T)
            {
                Console.Write(X);
            }
            Console.WriteLine();
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                foreach(int X in V)
                {
                    sw.Write(X);
                }
                sw.WriteLine();
                foreach (int Y in T)
                {
                    sw.Write(Y);
                }
            }
        }
        
        static void Main(string[] args)
        {
            bool check = true;
            while(check == true) {
                Console.WriteLine("Введите название файла: ");
                string filename = Console.ReadLine();
                string path = filename + ".csv";
                Console.WriteLine("Выберите желаемое действие: \n1 - Создать файл с данными о рёбрах\n2 - Составить код Прюфера (если файл создан и заполнен)\n3 - Ввести код Прюфера и декодировать его");
            int num = Convert.ToInt32(Console.ReadLine());
                List<Tree> tree = new List<Tree>();
            if (num == 1)
            {
                Console.WriteLine("Введите количество рёбер");
                int n = Convert.ToInt32(Console.ReadLine());
                WritingFile(path, tree, n);
            }
            if (num == 2 && path!="")
            {
 Coding(path, tree);
            }
           else if (path == "")
            {
                Console.WriteLine("Ошибка ввода, файл не найден");
            }
            if(num == 3)
            {
                    int count = 0;
                    Console.WriteLine("Введите размер кода Прюфера: ");
                    int n = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите код Прюфера: ");
                    int[] code = new int[n];
                    for(int i = 0; i < n; i++)
                    {
                        code[i] = Convert.ToInt32(Console.ReadLine());
                        count++;
                    }
                    count = count + 2;
                    Decoding(code, n,count);
            }
            Console.WriteLine("Желаете продолжить работу?(Y/N)");
               char contin = Convert.ToChar(Console.ReadLine());
                if(contin == 'Y' || contin == 'y')
                {
                    check = true;
                }
                else if(contin == 'N' || contin == 'n')
                {
                    check = false;
                }
                else
                {
                    Console.WriteLine("Такого действия в программе не предусмотрено");
                    break;
                }
            }
        }
    }
}
