using System;

namespace YandexAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            string path, address;
            #region Ввод данных
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {

                Console.WriteLine("Введите путь к файлам на яндекс диске. Например, '/загрузка/фото'");
                path = Console.ReadLine();
                if (path != "")
                {
                    break;
                }
            }
            while (true)
            {
                Console.WriteLine("Введите сетевой адрес. Например, 'https://yadi.sk/d/olHnWUtH93RHtA'");
                address = Console.ReadLine();
                if (path != "")
                {
                    break;
                }


            }
            #endregion
            WebManeger webManeger = new WebManeger();
            webManeger.DownloadFiles(path,address);
            Console.Read();
        }
    }
}
