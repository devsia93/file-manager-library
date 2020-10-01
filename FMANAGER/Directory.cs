using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDirectory = System.IO.Directory;
using SFile = System.IO.File;
using System.IO;
using Ionic.Zip;
using System.Security.AccessControl;

namespace Fmanager
{
    /// <summary>
    /// Класс для работы с папкой.
    /// </summary>
    public class Directory : IManager, IPersonalManager
    {
        #region Определения полей и событий.
        /// <summary>
        /// Полное имя директории.
        /// </summary>
        private string dname;


        #endregion

        /// <summary>
        /// Запись ошибок в лог.
        /// </summary>
        /// <param name="_NameOperations">Имя операции.</param>
        /// <param name="_NameError">Имя ошибки.</param>
        public void LogForOperations(string _NameOperations, string _NameError)
        {
            try
            {
                if (!SDirectory.Exists(@"C:\\FileManagerLog"))
                {
                    SDirectory.CreateDirectory(@"C:\\FileManagerLog");//создаем каталог для лога
                    SFile.CreateText(@"c:\FileManagerLog\log.txt");
                }
                System.IO.StreamWriter writer = new System.IO.StreamWriter(@"c:\FileManagerLog\log.txt");//создаем поток
                writer.WriteLine("Время операции:" + System.DateTime.Now + ". " + _NameOperations + "'" + dname + "'. Ошибка: " + _NameError + "");//запись в лог
                writer.Close();//закрываем поток
            }
            catch (Exception e)//обработка исключений для лога
            {
                throw e;
            }
        }

        /// <summary>
        /// Операции с папкой. 
        /// </summary>
        /// <param name="d">Путь к дирректории.</param>
        /// <returns>Статус папки (существует/не существует).</returns>
        public Directory(string d)
        {

            dname = d;//определяем рабочий каталог
        }

        /// <summary>
        /// Удаляет заданный каталог.
        /// </summary>
        public void delete()
        {
            if (SDirectory.Exists(dname) & dname.Length <= 260)//проверка на существование директории и корректности имени
            {
                try
                {
                    SDirectory.Delete(dname, true);//удаление дирректории
                    dname = null;//переопределение рабочего каталога
                }
                catch (Exception e)//обработка исключений для удаления
                {
                    LogForOperations("Удаление папки", e.Message);//запись ошибки в лог(если есть)
                    throw e;
                }
            }
            else
            {
                LogForOperations("Удаление папки", "папки не существует либо имя папки превышает 260 символов");//запись ошибки в лог(если не выполнилась проверка)
            }
        }
        /// <summary>
        /// Удаляет заданный каталог, и при наличии соответствующей инструкции, все подкаталоги в нем.  
        /// </summary>
        /// <param name="recursive">Значение true позволяет удалить каталоги, подкаталоги и файлы по заданному пути dname, в противном случае — значение false.</param>
        public void delete(bool Copy)
        {
            if (SDirectory.Exists(dname) & dname.Length <= 260)//проверка на существование директории и корректности имени
            {
                try
                {
                    if (!Copy)
                    {
                        SDirectory.Delete(dname);//удаление дирректории
                        dname = null;//переопределение рабочего каталога
                    }
                    else
                    {
                        copyto(@"C:\\FileManagerLog\Temp");
                    }
                }
                catch (Exception e)//обработка исключений для удаления
                {
                    LogForOperations("Удаление папки", e.Message);//запись ошибки в лог(если есть)
                    throw e;
                }
            }
            else
            {
                LogForOperations("Удаление папки", "папки не существует либо имя папки превышает 260 символов");//запись ошибки в лог(если не выполнилась проверка)
            }
        }

        /// <summary>
        /// Создает все каталоги по заданному пути.
        /// </summary>
        public void create()
        {
            if (!SDirectory.Exists(dname) & dname.Length <= 260)//проверка на существование директории и корректности имени
            {
                try
                {
                    SDirectory.CreateDirectory(dname);//создание директории
                }
                catch (Exception e)//обработка ислючения для создания
                {
                    LogForOperations("Создание папки", e.Message);//запись ошибки (если есть) в лог
                    throw e;
                }
            }
            else
            {
                LogForOperations("Создание папки", "папка уже существует либо имя папки превышает 260 символов");//запись ошибки в лог(если не выполнилась проверка)
            }
        }
        /// <summary>
        /// Создает все каталоги по заданному пути с применением заданных параметров безопасности Windows.
        /// </summary>
        /// <param name="directorySecurity">Элемент управления доступом, который необходимо применить к каталогу.</param>
        public void create(bool nonCreateExist)
        {
            if (nonCreateExist)
            {
                if (!SDirectory.Exists(dname) & dname.Length <= 260)//корректности имени
                {
                    try
                    {
                        SDirectory.CreateDirectory(dname);//создание директории
                    }
                    catch (Exception e)//обработка ислючения для создания
                    {
                        LogForOperations("Создание папки", e.Message);//запись ошибки (если есть) в лог
                        throw e;
                    }
                }
                else
                {
                    LogForOperations("Создание папки", "папка уже существует либо имя папки превышает 260 символов");//запись ошибки в лог(если не выполнилась проверка)
                }
            }

        }
        /// <summary>
        /// Переименование каталога.
        /// </summary>
        /// <param name="NewName">Новое имя.</param>
        public void rename(string NewName)
        {
            try
            {
                if (SDirectory.Exists(dname) & dname.Length <= 260 & !SDirectory.Exists(NewName) & NewName.Length <= 260)//проверка на корректность имен каталогов, существование переименуемой дироектории и несуществовании директории с таким именем
                {
                    SDirectory.Move(dname, NewName);//переименование директории
                    dname = NewName;//переопределение рабочей директории
                }
                else
                {
                    LogForOperations("Переименование папки", "операция с папкой невозможна, т.к. папка не существует либо в имени больше 260 символов, либо папка с новым именем уже существует");//запись в лог ошибки, если не выполнились условия проверки
                }

            }
            catch (Exception e)//обработка исключений для переименования
            {
                LogForOperations("Переименование папки", e.Message);//запись в лог ошибки (если есть)
                throw e;
            }
        }


        /// <summary>
        /// Перемещает весь каталог со всем его содержимым в новый каталог.
        /// </summary>
        /// <param name="NewName">Путь, куда нужно переместить.</param>
        public void moveto(string NewName)
        {
            if (!SDirectory.Exists(dname) & dname.Length <= 260 & NewName.Length <= 260)//проверка на существование директории, корректности имени перемещаемой папки и принимающей перемещение
                try
                {
                    SDirectory.Move(dname, NewName);//создание директории
                    dname = NewName;//переопределение рабочей папки
                }
                catch (Exception e)//обработка исключений для перемещения
                {
                    LogForOperations("Перемещение папки", e.Message);//запись ошибки (если есть) в лог
                    throw e;
                }
            else
            {
                LogForOperations("Перемещение папки", "операция с папкой невозможна, т.к. перемещаемая папка не существует либо в имени больше 260 символов, либо папка с новым именем уже существует");//запись ошибки в лог, если не выполнилась проверка
            }
        }
        /// <summary>
        /// Перемещает весь каталог со всем его содержимым в новый каталог либо только файлы из текущего каталога.
        /// </summary>
        /// <param name="NewName">Путь, куда нужно переместить.</param>
        /// <param name="onlyFiles">При значении true — перемещает только файлы текущего каталога.</param>
        public void moveto(string NewName, bool onlyFiles)
        {
            if (!SDirectory.Exists(dname) & dname.Length <= 260 & NewName.Length <= 260)//проверка на существование директории, корректности имени перемещаемой папки и принимающей перемещение
                try
                {
                    if (!onlyFiles)
                    {
                        SDirectory.Move(dname, NewName);//создание директории
                        dname = NewName;//переопределение рабочей папки
                    }
                    else
                    {
                        DirectoryInfo _d = new DirectoryInfo(dname);
                        foreach (string f in SDirectory.GetFiles(dname))
                        {
                            SFile.Move(f, NewName);
                        }
                    }
                }
                catch (Exception e)//обработка исключений для перемещения
                {
                    LogForOperations("Перемещение папки", e.Message);//запись ошибки (если есть) в лог
                    throw e;
                }
            else
            {
                LogForOperations("Перемещение папки", "операция с папкой невозможна, т.к. перемещаемая папка не существует либо в имени больше 260 символов, либо папка с новым именем уже существует");//запись ошибки в лог, если не выполнилась проверка
            }
        }
        /// <summary>
        /// Просмотр содержимого папки.
        /// </summary>
        /// <returns>Файлы и папки текущего каталога.</returns>
        public string view()
        {
            if (SDirectory.Exists(dname) & dname.Length <= 260)//проверка на существование директории и корректности имени
            {
                try
                {
                    string[] str = SDirectory.GetDirectories(dname);
                    string[] str1 = SDirectory.GetFiles(dname);
                    string _directoryShow = string.Empty;
                    string _filesShow = string.Empty;
                    foreach (string s in str)
                    {
                        _directoryShow += Path.GetFileName(s) + "\n";
                    }

                    foreach (string s in str1)
                    {
                        _filesShow += Path.GetFileName(s) + "\n";
                    }
                    return ("\nПапка просмотра: " + dname + "\nПапки: \n" + _directoryShow + " \nФайлы: \n" + _filesShow);//передача имен директорий и файлов
                }
                catch (Exception e)//обработка исключений для потока
                {
                    LogForOperations("Просмотр содержимого папки", e.Message);//запись ошибки (если есть) в лог
                    throw e;
                }
            }
            LogForOperations("Просмотр содержимого папки", "папка не существует либо содержит в названии более 260 символов");//запись ошибки в лог, если не выполнилось условие
            return "\nПапка не существует либо содержит в названии более 260 символов.";//передача ошибки

        }

        public string view(bool onlyDirectory)
        {
            if (SDirectory.Exists(dname) & dname.Length <= 260)//проверка на существование директории и корректности имени
            {
                try
                {

                    DirectoryInfo _d = new DirectoryInfo(dname);//создание потока

                    if (onlyDirectory)
                    {
                        return ("\nПапки: " + _d.GetDirectories());//передача имен директорий
                    }
                    else
                    {
                        return ("\nПапки: " + _d.GetDirectories() + " \nФайлы: " + _d.GetFiles());//передача имен директорий и файлов
                    }

                }
                catch (Exception e)//обработка исключений для потока
                {
                    LogForOperations("Просмотр содержимого папки", e.Message);//запись ошибки (если есть) в лог
                    throw e;
                }
            }
            LogForOperations("Просмотр содержимого папки", "папка не существует либо содержит в названии более 260 символов");//запись ошибки в лог, если не выполнилось условие
            return "\nПапка не существует либо содержит в названии более 260 символов.";//передача ошибки

        }
        /// <summary>
        /// Информация о папке.
        /// </summary>
        /// <returns>Время создания дирректории, последнего изменения дирректории, последнего обращения к дирректории, уровень доступа к дирректории, размер дирректории, список файлов в дирректории.</returns>
        public string[] info()
        {
            if (SDirectory.Exists(dname) & dname.Length <= 260)//проверка на существование директории и корректности имени
            {
                try
                {
                    List<string> _Info = new List<string>();//создание списка, куда будут заноситься сведения
                    _Info.Add(Convert.ToString("\nВремя создания дирректории: " + SDirectory.GetCreationTime(dname)));//время создания дирректории
                    _Info.Add(Convert.ToString("\nВремя последнего изменения дирректории: " + SDirectory.GetLastWriteTime(dname)));//время последнего изменения дирректории
                    _Info.Add(Convert.ToString("\nВремя последнего обращения к дирректории: " + SDirectory.GetLastAccessTime(dname)));//время последнего обращения к дирректории
                    _Info.Add(Convert.ToString("\nУровень доступа к дирректории: " + SDirectory.GetAccessControl(dname)));//уровень доступа к дирректории
                    _Info.Add(Convert.ToString("\nРазмер дирректории: " + SizeDirrecrory(dname)));//размер дирректории
                    _Info.Add("\nСписок файлов в дирректории: ");//список файлов в дирректории
                    string[] file = SDirectory.GetFiles(dname);//создание массива строк для имен файлов   

                    for (int i = 0; i < file.Length; i++)
                    {
                        _Info.Add("\n" + file[i]);//добавление в список имя файла
                    }
                    _Info.Add("\nКонец списка файлов.");//объявление о конце операции
                    return _Info.ToArray();//передача файла
                }
                catch (Exception e)//обработка ислючений для получения информации
                {
                    LogForOperations("Получение информации о папке", e.Message);//запись в лог ошибки (если есть)
                    throw e;
                }
            }
            else
            {
                LogForOperations("Получение информации о файле", "папка не существует либо содержит в названии более 260 символов");//запись ошибки в лог, если условие проверки не выполняется
                return null;
            }
        }
        /// <summary>
        /// Информация о папке.
        /// </summary>
        /// <param name="onlyDirectory">Если истинно, то информация без файлов.</param>
        /// <returns>Инфо о папке и файлах.</returns>
        public string[] info(bool onlyDirectory)
        {
            if (SDirectory.Exists(dname) & dname.Length <= 260)//проверка на существование директории и корректности имени
            {
                try
                {
                    List<string> _Info = new List<string>();//создание списка, куда будут заноситься сведения
                    _Info.Add(Convert.ToString("\nВремя создания дирректории: " + SDirectory.GetCreationTime(dname)));//время создания дирректории
                    _Info.Add(Convert.ToString("\nВремя последнего изменения дирректории: " + SDirectory.GetLastWriteTime(dname)));//время последнего изменения дирректории
                    _Info.Add(Convert.ToString("\nВремя последнего обращения к дирректории: " + SDirectory.GetLastAccessTime(dname)));//время последнего обращения к дирректории
                    _Info.Add(Convert.ToString("\nУровень доступа к дирректории: " + SDirectory.GetAccessControl(dname)));//уровень доступа к дирректории
                    _Info.Add(Convert.ToString("\nРазмер дирректории: " + SizeDirrecrory(dname)));//размер дирректории
                    _Info.Add("\nСписок файлов в дирректории: ");//список файлов в дирректории
                    string[] file = SDirectory.GetFiles(dname);//создание массива строк для имен файлов   

                    if (!onlyDirectory)
                    {
                        for (int i = 0; i < file.Length; i++)
                        {
                            _Info.Add("\n" + file[i]);//добавление в список имя файла
                        }
                        _Info.Add("\nКонец списка файлов.");//объявление о конце операции
                        return _Info.ToArray();//передача информации о папке с файлами
                    }
                    return _Info.ToArray();//передача информации о папке
                }
                catch (Exception e)//обработка ислючений для получения информации
                {
                    LogForOperations("Получение информации о папке", e.Message);//запись в лог ошибки (если есть)
                    throw e;
                }
            }
            else
            {
                LogForOperations("Получение информации о файле", "папка не существует либо содержит в названии более 260 символов");//запись ошибки в лог, если условие проверки не выполняется
                return null;
            }
        }
        #region Вспомогательные методы для нахождения размера.
        /// <summary>
        /// Поиск имен файлов и папок в дирректории.
        /// </summary>
        /// <param name="Link">Путь к дирректории.</param>
        /// <returns>Список имен.</returns>
        public string Dir(string Link)
        {
            try
            {
                DirectoryInfo _d = new DirectoryInfo(Link);
                FileSystemInfo[] _f = _d.GetFileSystemInfos();
                foreach (FileSystemInfo f in _f)
                    return SizeDirrecrory(f.Name);
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }


        }

        /// <summary>
        /// Нахождение размера директории.
        /// </summary>
        /// <param name="Link">Ссылка на директорию.</param>
        /// <returns>Размер.</returns>
        public string SizeDirrecrory(string Link)
        {
            try
            {
                long size = 0;
                DirectoryInfo _d = new DirectoryInfo(Link);
                FileInfo[] _f = _d.GetFiles();
                foreach (FileInfo f in _f)
                {
                    size += f.Length;
                }
                return size + " byte";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion




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
        /// Архивирование файла или папки.
        /// </summary>
        /// <param name="NewName">Путь, куда нужно сохранить архив.</param>
        public void arhiv(string NewName)
        {
            try
            {
                ZipFile _str = new ZipFile(NewName);
                foreach (string s in SDirectory.GetDirectories(dname))
                {
                    _str.AddDirectory(s, s);
                    foreach (string f in SDirectory.GetFiles(s))
                    {
                        _str.AddFile(f, s);
                    }
                }
                _str.Save(NewName);
            }
            catch (Exception e)
            {
                LogForOperations("Архивирование папки", e.Message);
                throw e;
            }


        }
        /// <summary>
        /// Раза
        /// </summary>
        /// <param name="NewName"></param>
        public void desarhiv(string NewName)
        {
            try
            {
                ZipFile _str = new ZipFile(NewName);
                _str.ExtractAll(NewName);
            }
            catch (Exception e)
            {
                LogForOperations("Распаковка архива", e.Message);
                throw e;
            }


        }
        ////метод, который принимает все входящие файлы и копирует их в новую папку 
        //public void Fil(string f, string NewName)
        //{
        //    SFile.Copy(dname + @"\" + f, NewName + @"\" + f);
        //}

        ////метод, который принимает на вход папки, создает их, содержимое отправляет в предыдущий
        //public void Dir(DirectoryInfo[] _d, string NewName)
        //{
        //    for (int i = 0; i < _d.Length; i++)
        //    {
        //        SDirectory.CreateDirectory(NewName + @"\" + _d[i]);
        //        if (_d[i].GetFiles().Length > 0)
        //        {
        //            FileInfo[] _ds = _d[i].GetFiles();//получение файлов
        //            for (int j = 0; j < _ds.Length; j++)
        //            {
        //                Fil(_ds[i].ToString(), NewName);
        //            }
        //        }

        //    }
        //}

        public void copyto(string NewName)
        {
            //List<string> FileName = new List<string>();
            //if (!SDirectory.Exists(NewName))
            //{
            //    SDirectory.CreateDirectory(NewName);//создаем новую директорию
            //}
            //DirectoryInfo _d = new DirectoryInfo(dname);//связываем поток с текущей
            try
            {
                if (!SDirectory.Exists(NewName) & NewName.Length <= 260 & dname.Length <= 260)
                {
                    SDirectory.CreateDirectory(NewName);
                }
                else
                {
                    LogForOperations("Копирование папки", "операция с папкой невозможна, т.к. папка не существует либо в имени больше 260 символов"); //запись в лог ошибки (если есть)
                }
                if (!SDirectory.Exists(NewName))
                {
                    foreach (string _pathFile in SDirectory.GetFiles(dname))
                    {
                        string _pathFile1 = NewName + @"\" + Path.GetFileName(_pathFile);
                        SFile.Copy(_pathFile, _pathFile1);
                    }
                    foreach (string s in SDirectory.GetDirectories(dname))
                    {
                        copyto(NewName + @"\" + Path.GetDirectoryName(s));
                    }
                }
                else LogForOperations("Копирование директории", "директория с таким именем уже существует");
            }

            catch (Exception e)//обработка исключений для сортирвки
            {

                LogForOperations("Копирование папки", e.Message); //запись в лог ошибки (если есть)
                throw e;
            }

        }

        public string[] sort()
        {
            try
            {

                string[] str = SDirectory.GetDirectories(dname);//считываем строки
                Array.Sort(str);//сортируем
                return str;
            }

            catch (Exception e)//обработка исключений для сортирвки
            {

                LogForOperations("Сортировка папки", e.Message); //запись в лог ошибки (если есть)
                throw e;
            }

        }

        public string[] sort(bool strReverse)
        {
            try
            {

                string[] str = SDirectory.GetDirectories(dname);//считываем строки
                Array.Sort(str);//сортируем
                if (strReverse)
                {
                    Array.Reverse(str);
                    return str;
                }
                else
                {
                    return str;
                }
            }

            catch (Exception e)//обработка исключений для сортирвки
            {

                LogForOperations("Сортировка папки", e.Message); //запись в лог ошибки (если есть)
                throw e;
            }

        }

        public void editing()
        {
            throw new NotImplementedException();
        }
    }
}
