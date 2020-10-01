using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDirectory = System.IO.Directory;

namespace Fmanager
{
    /// <summary>
    /// Класс для работы с папками.
    /// </summary>
    public class Directories : IManager, IPersonalManager
    {
        /// <summary>
        /// Список имен папок.
        /// </summary>

        List<string> myList = new List<string>();
        /// <summary>
        /// 
        /// </summary>
        private string[] str = null;

        /// <summary>
        /// Определение рабочей директории.
        /// </summary>
        /// <param name="d">Имя папки.</param>
        public Directories(List<string> d)
        {
            myList = d;//передача имени в список
        }
        /// <summary>
        /// Удаление папок.
        /// </summary>
        public void delete()
        {
            for (int i = 0; i < myList.Count; i++)//перебор всех папок, входящих в список
            {
                Directory d = new Directory(myList[i]);//инициализация папки
                d.delete();//удаление
            }

        }
        /// <summary>
        /// Создание папок.
        /// </summary>
        public void create()
        {
            for (int i = 0; i < myList.Count; i++)//перебор всех папок, входящих в список
            {
                Directory d = new Directory(myList[i]);//инициализация папки
                d.create();//создание папки
            }

        }
        /// <summary>
        /// Перемещение папки.
        /// </summary>
        /// <param name="NewName">Путь, куда нужно переместить.</param>
        public void moveto(string NewName)
        {
            for (int i = 0; i < myList.Count; i++)//перебор всех папок, входящих в список
            {
                Directory d = new Directory(myList[i]);//инициализация папки
                d.moveto(NewName);//перемещение папки
            }
        }
        /// <summary>
        /// Просмотр папок и файлов в нужных папках.
        /// </summary>
        /// <returns>Файлы и папки в рабочем каталоге.</returns>
        public string view()
        {
            string str = null;
            for (int i = 0; i < myList.Count; i++)//перебор всех папок, входящих в список
            {
                Directory d = new Directory(myList[i]);//инициализация папки
                str += d.view();//просмотр файлов и папок
            }
            return str;
        }
        /// <summary>
        /// Получение информации о папках.
        /// </summary>
        /// <returns>Время создания дирректории, последнего изменения дирректории, последнего обращения к дирректории, уровень доступа к дирректории, размер дирректории, список файлов в дирректории.</returns>
        public string[] info()
        {
            string[] str = null;
            for (int i = 0; i < myList.Count; i++)//перебор всех папок, входящих в список
            {
                Directory d = new Directory(myList[i]);//инициализация папки
                str[i] = d.info().ToString();//получени информации
            }
            return str;
        }
        /// <summary>
        /// Переименование папок.
        /// </summary>
        /// <param name="NewName">Новое имя.</param>
        public void rename(string NewName)
        {
            for (int i = 0; i < myList.Count; i++)//перебор всех папок, входящих в список
            {
                Directory d = new Directory(myList[i]);//инициализация папки
                d.rename(NewName);
            }
        }


        public void coding()
        {
            throw new NotImplementedException();
        }

        public void encoder()
        {
            throw new NotImplementedException();
        }

        public void uncoder()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NewName"></param>
        public void arhiv(string NewName)
        {
            for (int i = 0; i < myList.Count; i++)//перебор всех папок, входящих в список
            {
                Directory d = new Directory(myList[i]);//инициализация папки
                d.arhiv(NewName);
            }
        }

        public void desarhiv(string NewName)
        {
            for (int i = 0; i < myList.Count; i++)//перебор всех папок, входящих в список
            {
                Directory d = new Directory(myList[i]);//инициализация папки
                d.desarhiv(NewName);
            }
        }

        public void copyto(string NewName)
        {
            for (int i = 0; i < myList.Count; i++)//перебор всех папок, входящих в список
            {
                Directory d = new Directory(myList[i]);//инициализация папки
                d.copyto(NewName);
            }
        }

        public string[] sort()
        {
            for (int i = 0; i < myList.Count; i++)//перебор всех папок, входящих в список
            {
                Directory d = new Directory(myList[i]);//инициализация папки
                d.sort();
            }
            return str;
        }

        public void editing()
        {
            throw new NotImplementedException();
        }

        public void delete(bool Copy)
        {
            for (int i = 0; i < myList.Count; i++)//перебор всех папок, входящих в список
            {
                Directory d = new Directory(myList[i]);//инициализация папки
                if (Copy)
                {
                    d.copyto(@"C:\\FileManagerLog\Temp");
                }
                d.delete();//удаление
            }
        }

        public void create(bool nonCreateExist)
        {
            for (int i = 0; i < myList.Count; i++)//перебор всех папок, входящих в список
            {
                Directory d = new Directory(myList[i]);//инициализация папки
                d.create(nonCreateExist);//создание папки
            }
        }

        public void moveto(string NewName, bool onlyFiles)
        {
            for (int i = 0; i < myList.Count; i++)//перебор всех папок, входящих в список
            {
                Directory d = new Directory(myList[i]);//инициализация папки
                d.moveto(NewName, onlyFiles);//перемещение папки
            }
        }

        public string view(bool onlyDirectory)
        {
            string str = null;
            for (int i = 0; i < myList.Count; i++)//перебор всех папок, входящих в список
            {
                Directory d = new Directory(myList[i]);//инициализация папки
                str += d.view(onlyDirectory);//просмотр файлов и папок
            }
            return str;
        }

        public string[] info(bool onlyDirectory)
        {
            string[] str = null;
            for (int i = 0; i < myList.Count; i++)//перебор всех папок, входящих в список
            {
                Directory d = new Directory(myList[i]);//инициализация папки
                str[0] += d.info(onlyDirectory);//получение информации
            }
            return str;
        }

        public string[] sort(bool strReverse)
        {
            for (int i = 0; i < myList.Count; i++)//перебор всех папок, входящих в список
            {
                Directory d = new Directory(myList[i]);//инициализация папки
                d.sort(strReverse);
            }
            return str;
        }
    }
}
