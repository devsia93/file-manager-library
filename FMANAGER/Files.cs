using System.Collections.Generic;
using System;

namespace Fmanager
{
    /// <summary>
    /// Класс для работы с файлами.
    /// </summary>
    public class Files : IManager, IPersonalManager
    {
        /// <summary>
        /// Список файлов.
        /// </summary>

        List<string> myList = new List<string>();

        //переводим имена всех путей в список
        /// <summary>
        /// Конструктор класса файлов.
        /// </summary>
        /// <param name="a"></param>
        public Files(List<string> a)
        {
            myList = a;
        }
        /// <summary>
        /// Удаление файлов
        /// </summary>
        public void delete()
        {
            for (int i = 0; i < myList.Count; i++)
            {
                File f = new File(myList[i]);
                f.delete();
            }
        }
        /// <summary>
        /// Создание файлов
        /// </summary>
        public void create()
        {
            for (int i = 0; i < myList.Count; i++)
            {
                File f = new File(myList[i]);
                f.create();
            }
        }
        /// <summary>
        /// Перемещение файлов
        /// </summary>
        /// <param name="NewName">Имя нового пути</param>
        public void moveto(string NewName)
        {
            for (int i = 0; i < myList.Count; i++)
            {
                File f = new File(myList[i]);
                f.moveto(NewName);
            }
        }
        /// <summary>
        /// Смена кодировки файлов
        /// </summary>
        public void coding()
        {
            for (int i = 0; i < myList.Count; i++)
            {
                File f = new File(myList[i]);
                f.coding();
            }
        }
        /// <summary>
        /// Кодирование файлов
        /// </summary>
        public void encoder()
        {
            for (int i = 0; i < myList.Count; i++)
            {
                File f = new File(myList[i]);
                f.encoder();
            }
        }
        /// <summary>
        /// Декодирование файлов
        /// </summary>
        public void uncoder()
        {
            for (int i = 0; i < myList.Count; i++)
            {
                File f = new File(myList[i]);
                f.uncoder();
            }
        }
        /// <summary>
        /// Архивирование файлов
        /// </summary>
        /// <param name="NewName">Путь для архива</param>
        public void arhiv(string NewName)
        {
            for (int i = 0; i < myList.Count; i++)
            {
                File f = new File(myList[i]);
                f.arhiv(NewName);
            }
        }
        /// <summary>
        /// Разархивирование файлов
        /// </summary>
        /// <param name="NewName">Путь, куда нужно разархивировать</param>
        public void desarhiv(string NewName)
        {
            for (int i = 0; i < myList.Count; i++)
            {
                File f = new File(myList[i]);
                f.desarhiv(NewName);
            }
        }
        /// <summary>
        /// Копирование файлов
        /// </summary>
        /// <param name="NewName">Путь, куда нужно скопировать</param>
        public void copyto(string NewName)
        {
            for (int i = 0; i < myList.Count; i++)
            {
                File f = new File(myList[i]);
                f.copyto(NewName);
            }
        }
        /// <summary>
        /// Просмотр файлов
        /// </summary>
        /// <returns>Имена файлов</returns>
        public string view()
        {
            return myList.ToString();
        }
        /// <summary>
        /// Сортировка
        /// </summary>
        /// <returns>Сисок элементов</returns>
        public string[] sort()
        {
            myList.Sort();
            return myList.ToArray();

        }
        /// <summary>
        /// Информация о файлах
        /// </summary>
        /// <returns>Размер файлов</returns>
        public string[] info()
        {
            int i = 0;
            long sum = 0;
            string[] str = new string[myList.Count];
            for (i = 0; i < str.Length; i++)
            {
                System.IO.FileInfo f = new System.IO.FileInfo(myList[i]);
                sum += f.Length;

            }
            str[0] += sum.ToString();
            str[1] += myList.Count.ToString();
            return str;
        }
        /// <summary>
        /// Переименование файлов
        /// </summary>
        /// <param name="NewName">Новое имя</param>
        public void rename(string NewName)
        {
            for (int i = 0; i < myList.Count; i++)
            {
                File f = new File(myList[i]);
                f.rename(NewName);
            }
        }
        /// <summary>
        /// Запись в файлы
        /// </summary>
        public void editing()
        {
            for (int i = 0; i < myList.Count; i++)
            {
                File f = new File(myList[i]);
                f.editing();
            }
        }
        /// <summary>
        /// Удаление файлов.
        /// </summary>
        /// <param name="Copy">Копирование файла перед удалением, при значении — true.</param>
        public void delete(bool Copy)
        {
            for (int i = 0; i < myList.Count; i++)
            {
                File f = new File(myList[i]);
                f.delete(Copy);
            }
        }
        /// <summary>
        /// Создание или перезаписывание файлов.
        /// </summary>
        /// <param name="nonCreateExist">Отменяет перезаписывание, при значении — true.</param>
        public void create(bool nonCreateExist)
        {
            for (int i = 0; i < myList.Count; i++)
            {
                File f = new File(myList[i]);
                f.create(nonCreateExist);
            }
        }
        /// <summary>
        /// Перемещение файлов.
        /// </summary>
        /// <param name="NewName">Путь, куда нужно переместить.</param>
        /// <param name="onlyFiles">Перемещение из папки только файлов, при значении — true.</param>
        public void moveto(string NewName, bool onlyFiles)
        {
            for (int i = 0; i < myList.Count; i++)
            {
                File f = new File(myList[i]);
                f.moveto(NewName, onlyFiles);
            }
        }
        /// <summary>
        /// Просмотр файлов, либо просмотр списка папок директории(при onlyDirectory = true).
        /// </summary>
        /// <param name="onlyDirectory">Просмотр списка папок директории, при значении — true.</param>
        /// <returns>Содержимое файла/список подкаталогов.</returns>
        public string view(bool onlyDirectory)
        {
            string str = null;
            for (int i = 0; i < myList.Count; i++)
            {
                File f = new File(myList[i]);
                str += f.view(onlyDirectory);
            }
            return str;
        }
        /// <summary>
        /// Информация о файле/папке.
        /// </summary>
        /// <param name="onlyDirectory">Просмотр информации только о папке, при значении true.</param>
        /// <returns>Дата создания, последнего изменения, последнего обращения к файлу, атрибуты файла, доступ к файлу.</returns>
        public string[] info(bool onlyDirectory)
        {
            string[] str = null;
            for (int i = 0; i < myList.Count; i++)
            {
                File f = new File(myList[i]);
                str[0] += f.info(onlyDirectory);
            }
            return str;
        }
        /// <summary>
        /// Сортировка содержимого.
        /// </summary>
        /// <param name="strReverse">Обратная сортировка.</param>
        /// <returns>Отсортированный массив.</returns>
        public string[] sort(bool strReverse)
        {
            myList.Sort();
            if (strReverse)
            {
                myList.Reverse();
            }
            return myList.ToArray();



        }
    }
}
