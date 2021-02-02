using System;

namespace YandexAPI
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.White;
            if (args.Length > 0)
            {
                WebManeger webManeger = new WebManeger();
                webManeger.DownloadFiles(args[0], args[1]);
                Console.Read();
            }
            else
            {
                Console.WriteLine("Введите два параметра:\n1.Директорию(путь к папке)\n2.Адрес Яндекс диска");
            }
           
        }
    }
}
