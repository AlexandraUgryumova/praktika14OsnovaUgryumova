using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace Угрюмова_практическая_14_коллекции_и_собственные_методы
{
    class Program
    {
        static void StackOne(int N)
        {
            Stack<int> stack = new Stack<int>();

            for (int i = 1; i <= N; i++)
            {
                stack.Push(i);
            }

            Console.WriteLine($"размерность стека {stack.Count()}");
            Console.WriteLine($"верхний элемент стека = {stack.Peek()}");
            Console.WriteLine($"размерность стека {stack.Count()}");
            int[] array = new int[N];
            stack.CopyTo(array, 0);
            Console.Write($"содержимое стека = ");
            for (int i = 0; i < N; i++)
            {
                Console.Write($"{stack.Pop()} ");
            }
            Console.WriteLine($"\nновая размерность стека {stack.Count()}");
        }
        static void StackTwo_a(string message)
        {
            StreamWriter sw = File.CreateText("text.txt");
            sw.Write(message);
            sw.Close();
            Console.WriteLine("выражение успешно записано в файл 'text.txt'");

            string message1 = File.ReadAllText("text.txt");
            Stack<char> stack = new Stack<char>();

            bool balance = true;
            int count = 1;

            while(count < message1.Length && balance == true)
            {
                char skobka = message1[count];
                if (skobka == '(') stack.Push(skobka);
                else if(skobka == ')')
                {
                    if (stack.Count == 0) balance = false;
                    else stack.Pop();
                }
                count++;
            }
            if (balance && stack.Count == 0) Console.WriteLine("скобки сбалансированы");
            else if (stack.Count == 0) Console.WriteLine($"возможно лишняя ) в позиции {count}");
            else Console.WriteLine($"возможно лишняя ( в позиции {count}");
        }
        static void StackTwo_b(string message)
        {
            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < message.Length; i++)
            {
                char skobka = message[i];
                if (skobka == '(') stack.Push(skobka);
                else if (skobka == ')')
                {
                    if (stack.Count > 0 && stack.Peek() == '(') stack.Pop();
                    else stack.Push(skobka);
                }
            }

            while(message.Length > 0 && message[0] == ')')
            {
                message = message.Remove(0, 1);
            }
            while(message.Length > 0 && message[message.Length-1] == '(')
            {
                message = message.Remove(message.Length -1, 1);
            }

            while (stack.Count > 0)
            {
                char skobka = stack.Pop();
                if (skobka == '(') message += ')';
                else if (skobka == ')') message = message.Remove(message.IndexOf(')'), 1);
            }

            File.WriteAllText("text.txt", message);
            Console.WriteLine($"новое выражение: '{message}' записано в файл");
        }
        static void Main(string[] args)
        {
            try
            {
                //задание 1
                Console.WriteLine("введите количество элементов");
                int.TryParse(Console.ReadLine(), out int N);
                StackOne(N);
                //задание 2
                Console.WriteLine("Введите математическое выражение");
                string message = Console.ReadLine();
                StackTwo_a(message);
                message = File.ReadAllText("text.txt");
                StackTwo_b(message);

                Console.ReadKey();
            }
            catch
            {
                Console.WriteLine("ошибка ввода данных");
            }
        }
    }
}
