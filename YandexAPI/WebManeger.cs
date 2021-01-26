using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace YandexAPI
{
   /// <summary>
   /// Класс загрузки файлов с яндекс диска
   /// </summary>
    class WebManeger
    {
        YDir yDir;
        /// <summary>
        /// Логика загрузки файлов с яндекс диска
        /// </summary>
        /// <param name="path"></param>
        /// <param name="address"></param>
        public void DownloadFiles(string path, string address)
        {
            try
            {
                getDirectory(path, address);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            loadFileAsync();
        }
        /// <summary>
        /// Получение списка файлов с яндекс диска
        /// </summary>
        /// <param name="path"></param>
        /// <param name="address"></param>
        void getDirectory(string path,string address)
        {
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString("https://cloud-api.yandex.net/v1/disk/public/resources?public_key=" + address + "&path=" + path);
                    yDir = JsonSerializer.Deserialize<YDir>(json);
                }
        }
        /// <summary>
        /// Загрузка файлов асинхронно
        /// </summary>
        void loadFileAsync()
        {
            DirectoryInfo directory = new DirectoryInfo("files");
            try
            {
                if (!directory.Exists)
                    directory.Create();
            }
            catch
            {
                Console.WriteLine("Ошибка создания папки files");           
            }
            //количество поток не более количество процессоров
            int proc = Environment.ProcessorCount;
            int items = yDir._embedded.items.Length;
            int countTask =  items> proc ? proc :items;
            //количество изображений для одного потока
            int iCount = items / countTask;
            int[] fileCount = new int[countTask];
            int delta = items- iCount * countTask;
            //остатки раскидываем по потокам
            for (int i = 0; i < countTask; i++)
            {
                fileCount[i] = iCount;
                if (delta > 0)
                {
                    fileCount[i]++;
                    delta--;
                }

            }
            Task[] tasks = new Task[countTask];
            int start = 0;
            //Создаем задачи
            for (int i = 0; i < countTask; i++)
            {
                int z = i;
                int startT = start;
                int fileC = fileCount[z];
                tasks[i] = new Task(() => loadFile(startT, startT + fileC));

                start += fileCount[z];
            }
            ///запускаем их
            for (int i = 0; i < countTask; i++)
            {
                tasks[i].Start();
            }
            //ждем завершения
            Task.WaitAll(tasks);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Все файлы загружены!");
        }
        /// <summary>
        /// Загрузка файлов синхронно
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        void loadFile(int start,int end)
        {
            
            for (int i = start; i < end; i++)
            {
                Console.WriteLine(yDir._embedded.items[i].name+"\tЗагрузка...");
                //если файл не создан
                using (var webClient = new WebClient())
                {
                    webClient.DownloadFile(yDir._embedded.items[i].file, "files\\" + yDir._embedded.items[i].name);
                }
                Console.WriteLine(yDir._embedded.items[i].name + "\tЗагружено");
            }
        }
    }
}
