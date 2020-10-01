using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SFile = System.IO.File;
using SDirectory = System.IO.Directory;
using Ionic.Zip;

namespace Fmanager
{
    /// <summary>
    /// Класс для операции с файлом.
    /// </summary>
    public class File : IManager, IPersonalManager
    {
        #region Определение полей и событий.
        /// <summary>
        /// Полное имя файла.
        /// </summary>
        private string fname;

        /// <summary>
        /// Файловый поток.
        /// </summary>
        private System.IO.FileStream stream;
        /// <summary>
        /// Переопределение потока, очистка буферов.
        /// </summary>
        /// <param name="fname">Имя рабочего файла.</param>

        #endregion

        #region Дополнительные методы.
        //public void CleanStream(string fname)
        //{
        //    try
        //    {
        //        stream.Flush();//очищаем буфер потока
        //        stream = SFile.Open(fname, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);//связываем файл с потоком
        //    }

        //    catch (Exception exc)//обработка исключений для потока
        //    {
        //        throw exc;
        //    }
        //}


        /// <summary>
        /// Лог для операций.
        /// </summary>
        /// <param name="_NameOperations">Имя операции.</param>
        /// <param name="_NameError">Имя ошибки.</param>
        public void LogForOperations(string _NameOperations, string _NameError)
        {
            try
            {
                SDirectory.CreateDirectory(SDirectory.GetLogicalDrives().GetValue(0) + @"\FileManagerLog");//создаем каталог для лога
                SFile.WriteAllText(SDirectory.GetLogicalDrives().GetValue(0) + @"FileManagerLog\log.txt", ("\nВремя операции:" + System.DateTime.Now + ". " + _NameOperations + "'" + fname + "'. Ошибка: " + _NameError + "."));//запись в лог
            }
            catch (Exception e) { throw e; }//обработка исключений для лога
        }
        #endregion

        #region Операции с файлом.
        /// <summary>
        /// Файловый поток.
        /// </summary>
        /// <param name="f">Имя файла.</param>
        public File(string f)
        {
            fname = f;//передаем имя
            if (SFile.Exists(fname) & fname.Length <= 260)//проверка на существование файла
                try
                {
                    stream = SFile.Open(fname, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);//связываем наш поток с файлом
                }

                catch (UnauthorizedAccessException exc)//обработка исключений для потока
                {
                    throw exc;
                }
        }
        /// <summary>
        /// Удаление файла.
        /// </summary>
        public void delete()
        {
            if (SFile.Exists(fname) & fname.Length <= 260)//проверка на существование файла
            {
                try
                {
                    stream.Close();//закрываем поток
                    SFile.Delete(fname);//удаляем файл
                    fname = null;//обнуляем путь
                    stream.Flush();//очищаем буфер потока
                }

                catch (Exception e) //обработка исключений для удаления
                {
                    throw e;
                }
            }
            else
            {
                LogForOperations("Удаление файла", "файла не существует");//запись в лог ошибки (если есть)

            }
        }

        public void delete(bool Copy)
        {
            if (SFile.Exists(fname) & fname.Length <= 260)//проверка на существование файла и корректность имени
            {
                try
                {
                    if (!Copy)
                    {
                        stream.Close();//закрываем поток
                        SFile.Delete(fname);//удаляем файл
                        fname = null;//обнуляем путь
                        stream.Flush();//очищаем буфер потока
                    }
                    else
                    {
                        stream.Close();
                        SFile.Copy(fname, @"C:\\FileManagerLog\Temp");
                        SFile.Delete(fname);//удаляем файл
                        fname = null;//обнуляем путь
                        stream.Flush();//очищаем буфер потока
                    }
                }

                catch (Exception e) //обработка исключений для удаления
                {
                    throw e;
                }
            }
            else
            {
                LogForOperations("Удаление файла", "файла не существует");//запись в лог ошибки (если есть)

            }
        }
        /// <summary>
        /// Создание или перезаписывание файла.
        /// </summary>
        public void create()
        {
            stream.Close();//закрываем старый поток
            if (fname.Length <= 260)//проверка на корректность имени
            {
                try
                {
                    SFile.Create(fname);//создание файла
                }
                catch (Exception ex)//обработка исключений для создания
                {
                    LogForOperations("Создание файла", ex.Message);//запись в лог ошибки (если есть)
                    throw ex;
                }
            }
            else
            {
                LogForOperations("Создание или перезаписывание файла", "имя файла превышает 260 символов");
            }
        }
        /// <summary>
        /// Создание файла или перезаписывание.
        /// </summary>
        /// <param name="nonCreateExists">Перезаписывание, при значении true.</param>
        public void create(bool nonCreateExists)
        {
            stream.Close();//закрываем старый поток
            if (!nonCreateExists)//проверяем файл на существование до его создания
            {
                if (SFile.Exists(fname) & fname.Length <= 260)//проверка на существование файла и корректность имени
                {
                    LogForOperations("Создание файла", "файл уже существует либо содержит в своем имени более 260 символов");//запись в лог ошибки (если есть)

                }

                else
                {
                    try
                    {
                        SFile.Create(fname);//создание файла
                    }
                    catch (Exception ex)//обработка исключений для создания
                    {
                        LogForOperations("Создание файла", ex.Message);//запись в лог ошибки (если есть)
                        throw ex;

                    }
                }
            }
            else
                try
                {
                    {
                        SFile.Create(fname);//создание файла
                    }
                }
                catch (Exception ex)//обработка исключений для создания
                {
                    if (!SFile.Exists(fname))//проверяем файл на существование после его создания
                    {
                        LogForOperations("Создание файла", ex.Message);//запись в лог ошибки (если есть)

                    }
                    throw ex;
                }
        }
        /// <summary>
        /// Перемещение файла.
        /// </summary>
        /// <param name="NewName">Место (полное имя), куда нужно переместить.</param>
        public void moveto(string NewName)
        {
            stream.Close();
            if (!SFile.Exists(fname) && fname.Length >= 260 && NewName.Length >= 260)//проверка на существование файла и корректность имени перемещаемого файла, папки
            {
                LogForOperations("Перемещение файла", "перемещаемый файл не найден либо содержит в своем имени более 260 символов, либо папка, в которую идет перемещение, содержит в имени более 260 символов");//запись ошибки в лог (если есть)

            }
            else
            {
                try
                {
                    if (!SDirectory.Exists(NewName))//проверка на существование дирректории в которую производится перемещение
                    {
                        try
                        {
                            SDirectory.CreateDirectory(NewName);//создание дирректории, в которую происходит перемещение
                        }
                        catch (Exception e)//обработка исключения для создания директории
                        {
                            LogForOperations("Перемещение файла", e.Message + "(при попытке создании директории, в которую идет перемещение)");//запись ошибки (если есть) в лог
                            throw e;
                        }

                    }
                    else
                    {
                        SFile.Move(fname, NewName);//перемещение файла
                        fname = NewName;//переопределение рабочего пути
                    }

                }

                catch (Exception ex) //обработка исключений для перемещения
                {
                    LogForOperations("Перемещение файла", ex.Message); //запись ошибки в лог (если есть)
                    throw ex;
                }
            }

        }
        /// <summary>
        /// Смена кодировки.
        /// </summary>
        public void coding()
        {
            byte[] buffer = new byte[4096];//буфер 1
            byte[] newBuffer = new byte[4096];//буфер 2
           System.IO.FileStream stream1 = SFile.Open(fname, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);//связываем наш поток с файлом
            while (stream1.Position < stream1.Length)//пока позиция меньше длины
            {
                try
                {
                    int count = stream1.Read(buffer, 0, buffer.Length);//запись байтов в 1 буфер

                    for (int i = 0; i < count - 1; i++)
                    {

                        newBuffer = Encoding.ASCII.GetBytes(buffer[i] + " ");//меняем кодировку, записывая в буфер 2

                    }
                    stream1.Write(newBuffer, 0, newBuffer.Length);//запись ф файловый поток
                    stream1.Flush();//очищаем фуферы
                }
                catch (Exception ex)//обработка исключений для смены кодировки
                {
                    LogForOperations("Смена кодировки файла", ex.Message);//запись в лог ишибки (если есть)
                    throw ex;

                }
            }
        }
        /// <summary>
        /// Шифрование файла.
        /// </summary>
        public void encoder()
        {
            stream.Close();//закрываем поток
            if (SFile.Exists(fname) & fname.Length <= 260)//проверка на существование файла и корректность имени
            {
                try
                {
                    SFile.Encrypt(fname);//шифруем файл
                }

                catch (Exception ex)//обработка исключений для шифрования
                {
                    LogForOperations("Шифрование файла", ex.Message);//запись в лог ошибки (если есть)
                    throw ex;

                }
            }
            else
            {
                LogForOperations("Шифрование файла", "шифруемый файл не найден либо содержит в своем имени более 260 символов");//запись ошибки в лог(если есть)
            }
        }
        /// <summary>
        /// Дешифровка файла.
        /// </summary>
        public void uncoder()
        {
            stream.Close();//закрываем поток
            if (SFile.Exists(fname) & fname.Length <= 260)//проверка на существование файла и корректность имени
            {
                try
                {
                    SFile.Decrypt(fname);//дешифруем файл
                }

                catch (Exception ex)//обработка исключений для дешифрования
                {
                    LogForOperations("Дешифрование файла", ex.Message);//запись в лог ошибки (если есть)
                    throw ex;

                }
            }

        }
        /// <summary>
        /// Архивирование файла.
        /// </summary>
        /// <param name="NewName">Путь, куда нужно сохранить архив.</param>
        public void arhiv(string NewName)
        {
            if (SFile.Exists(fname) & fname.Length <= 260 & NewName.Length <= 260)//проверка на существование файла и корректность имени файла, пути
            {
                try
                {
                    stream.Close();//закрываем старый поток
                    if (!SFile.Exists(NewName))
                    {
                        ZipFile zip = new ZipFile(NewName);//создаем архив по новому пути
                        zip.AddFile(fname);//добавляем в архив наш файл
                        zip.Save(NewName);//сохраняем архив
                    }
                    else
                    {
                        LogForOperations("Архивирование", "архив уже существует");
                    }

                }

                catch (Exception ex)//обработка исключений для архиватора
                {
                    LogForOperations("Архивирование файла", ex.Message);//запись ошибки в лог (если есть)
                    throw ex;
                }
            }
            else
            {
                LogForOperations("Архивирование файла", "файл не существует либо содержит в своем имени более 260 символов, либо имя архива содержит в своем имени более 260 символов");//запись ошибки в лог, если не выполняется условия проверки
            }

            fname = NewName;//переопределение рабочего пути

        }


        /// <summary>
        /// Распаковка архива.
        /// </summary>
        /// <param name="NewName">Путь, куда нужно распаковать.</param>
        public void desarhiv(string NewName)
        {
            if (SFile.Exists(fname) & fname.Length <= 260 & NewName.Length <= 260)//проверка на существование файла и корректность имени файла, пути
            {
                try
                {
                    stream.Close();//закрываем старый поток
                    ZipFile zip = new ZipFile(fname);//связываем архиватор с архивом
                    zip.ExtractAll(NewName);//Распаковываем в новый путь 
                }

                catch (Exception ex)//обработка исключений для архиватора
                {
                    LogForOperations("Разархивирование файла", ex.Message);//запись в лог ошибки (если есть)
                    throw ex;
                }
            }
            else
            {
                LogForOperations("Распаковка архива", "файл не существует либо содержит в своем имени более 260 символов, либо имя архива содержит в своем имени более 260 символов");//запись ошибки в лог, если не выполняются условие проверки
            }

            fname = NewName;//переопределяем путь, с которым работаем
        }
        /// <summary>
        /// Копирование файла.
        /// </summary>
        /// <param name="NewName">Путь, куда нужно скопировать.</param>
        public void copyto(string NewName)
        {

            stream.Close();//закрываем поток
            if (SFile.Exists(fname) & fname.Length <= 260 & NewName.Length <= 260)//проверка на существование файла и корректность имени файла, папки
            {
                try
                {
                    if (!SDirectory.Exists(NewName))//проверка на существование папки
                    {
                        if (NewName.Contains(System.IO.Path.GetFileName(fname)))
                        {
                            int i = NewName.IndexOf(@"\" + Path.GetFileName(fname));
                            NewName = NewName.Remove(i);
                            SDirectory.CreateDirectory(NewName);//создаем папку, если она не существует
                            SFile.Copy(fname, NewName + @"\" + Path.GetFileName(fname), true);//копирование файла
                        }
                    }
                    else
                    {
                        if (!SFile.Exists(NewName))
                        {
                            SDirectory.CreateDirectory(NewName);//создаем папку, если она не существует
                            SFile.Copy(fname, NewName);//копирование файла
                        }
                        else
                        {
                            LogForOperations("Копирование файла", "файл с таким именем уже существует.");
                        }
                    }

                }



                catch (Exception ex)//обработка исключений для копирования
                {
                    LogForOperations("копирование файла", ex.Message);//запись в лог ошибки (если есть) 
                    throw ex;
                }
            }
            else
            {
                LogForOperations("Копирование файла", "файл не существует либо содержит в своем имени более 260 символов, либо папка, в которую происходит копирование, содержит в своем имени более 260 символов");//запись ошибки в лог, если не выполняется условие проверки
            }
        }
        /// <summary>
        /// Просмотр файла.
        /// </summary>
        public string view()
        {
            string str = string.Empty;
            if (SFile.Exists(fname) & fname.Length <= 260)//проверка на существование файла и корректность имени
            {
                try
                {
                    System.IO.FileStream stream1 = SFile.Open(fname, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);//связываем наш поток с файлом
                    StreamReader Reader = new StreamReader(stream1);// создаем «потоковый читатель» и связываем его с файловым потоком
                    str = Reader.ReadToEnd(); //считываем все данные с потока и выводим на экран
                    Reader.Close(); //закрываем потоковый читатель
                    stream.Close();//закрываем поток
                }

                catch (Exception e)//обработка исключений для читателя
                {
                    LogForOperations("Просмотр файла", e.Message);//запись в лог ошибки (если есть)
                    throw e;
                }
            }
            else
            {
                LogForOperations("Просмотр файла", "файл не существует либо содержит в своем имени более 260 символов");//запись ошибки в лог, если не выполняется условие проверки
            }
            return str;
        }
        /// <summary>
        /// Сортировка файла.
        /// </summary>
        /// <returns>Отсортированный файл.</returns>
        public string[] sort()
        {
            stream.Close();//закрываем поток    
            if (SFile.Exists(fname) & fname.Length <= 260)//проверка на существование файла и корректность имени
            {
                try
                {
                    string[] str = SFile.ReadAllLines(fname);//считываем строки
                    Array.Sort(str);//сортируем
                    return str;
                }



                catch (Exception e)//обработка исключений для сортирвки
                {

                    LogForOperations("Сортировка файла", e.Message); //запись в лог ошибки (если есть)
                    throw e;

                }
            }
            else
            {
                LogForOperations("Сортировка файла", "файл не существует либо содержит в своем имени более 260 символов");//запись ошибки в лог, если не выполняется условие проверки
                return null;
            }

        }
        /// <summary>
        /// Информация о файле.
        /// </summary>
        /// <returns>Дата создания, последнего изменения, последнего обращения к файлу, атрибуты файла, доступ к файлу.</returns>
        public string[] info()
        {
            if (SFile.Exists(fname) & fname.Length <= 260)//проверка на существование файла и корректность имени
            {
                try
                {

                    string[] s = new string[1];
                    s[0] += "\nДата создания файла: " + SFile.GetCreationTime(fname).ToString();//получение информации даты создания
                    s[0] += "\nДата последнего изменения файла: " + Convert.ToString(SFile.GetLastWriteTime(fname));//получение информации даты последнего изменения
                    s[0] += "\nДата последнего обращения к файлу: " + Convert.ToString(SFile.GetLastAccessTime(fname));//получение информации даты последнего обращения к файлу
                    s[0] += "\nАтрибуты файла: " + Convert.ToString(SFile.GetAttributes(fname));//получение информации атрибутов файла
                    s[0] += "\nДоступ к файлу: " + Convert.ToString(SFile.GetAccessControl(fname));//получение информации доступа к файлу
                    return s;
                }

                catch (Exception e)//обработка исключений для получения информации
                {
                    LogForOperations("Информация о файле", e.Message); //запись в лог ошибки (если есть)
                    throw e;
                }
            }
            else
            {
                LogForOperations("Получение информации о файле", "файл не существует либо содержит в своем имени более 260 символов");//запись ошибки в лог, если не выполняется условие проверки
                return null;
            }
        }
        /// <summary>
        /// Переименовывание файла.
        /// </summary>
        /// <param name="NewName">Новое имя.</param>
        public void rename(string NewName)
        {
            stream.Close();//закрываем поток
            if (SFile.Exists(fname) & fname.Length <= 260 & NewName.Length <= 260)//проверка на существование файла и корректность имени, нового имени
            {
                try
                {
                    if (!SFile.Exists(NewName))//проверка файла на отсутствие по новому пути
                    {
                        SFile.Move(fname, NewName);//перемещение файла
                    }
                    else
                    {
                        LogForOperations("Переименование файла", "файл с таким именем уже существует");//запись в лог ошибки

                    }
                }

                catch (Exception ex) //обработка исключений для переименования
                {
                    LogForOperations("Переименование файла", ex.Message);//запись в лог оибки (если есть)
                    throw ex;
                }
            }
            else
            {
                LogForOperations("Переименование файла", "файл не существует либо содержит в своем имени более 260 символов, либо новое имя содержит более 260 символов");//запись ошибки в лог, если не выполняется условие проверки
            }

        }
        /// <summary>
        /// Редактирование файла.
        /// </summary>
        public void editing()
        {
            if (SFile.Exists(fname) & fname.Length <= 260)//проверка на существование файла и корректность имени
            {
                try
                {
                    System.IO.StreamWriter Writer = new System.IO.StreamWriter(stream);//связываем поток с редактором
                    Writer.Write(Console.ReadLine());//запись строки
                    Writer.Close();//закрываем редактор
                    stream.Close();//закрываем поток
                }
                catch (Exception e)//обработка исключения для редактора
                {
                    LogForOperations("Редактирование файла", e.Message);//запись в лог ошибки (если есть)
                    throw e;
                }
            }
            else
            {
                LogForOperations("Редактирование файла", "файл не существует либо содержит в своем имени более 260 символов");//запись ошибки в лог, если не выполняется условие проверки
            }

        }

        /// <summary>
        /// Перемещение файла.
        /// </summary>
        /// <param name="NewName">Путь, куда нужно переместить.</param>
        /// <param name="onlyFiles">При значении true — перемещает только файлы из папки.</param>
        public void moveto(string NewName, bool onlyFiles)
        {
            stream.Close();
            if (SFile.Exists(fname) & fname.Length <= 260 & NewName.Length <= 260)//проверка на существование файла и корректность имени, пути
            {
                if (onlyFiles)
                {
                    try
                    {
                        if (!SDirectory.Exists(NewName))//проверка папки, в которую надо переместить, на существование
                        {
                            SDirectory.CreateDirectory(NewName);//создание папки, в которую надо переместить
                        }
                        SFile.Move(fname, NewName);//перемещение файла
                        fname = NewName;//переопределение рабочего пути
                    }
                    catch (Exception ex) //обработка исключений для перемещения
                    {
                        LogForOperations("Перемещение файла", ex.Message); //запись ошибки в лог (если есть)
                        throw ex;
                    }
                }
                else
                {
                    LogForOperations("Перемещение файла", "операция отменена, т.к. значение параметра = '0'");//запись ошибки(если есть) в лог
                }
            }
            else
            {
                LogForOperations("Перемещение файла", "файл не существует либо содержит в своем имени более 260 символов, либо папка, в которую происходит перемещение, содержит в имени более 260 символов");//запись ошибки в лог, если не выполняется условие проверки
            }

        }
        /// <summary>
        /// Просмотр файла, либо просмотр списка папок директории(при onlyDirectory = true).
        /// </summary>
        /// <param name="onlyDirectory">Просмотр списка папок директории, при значении — true.</param>
        /// <returns>Содержимое файла/список подкаталогов.</returns>
        public string view(bool onlyDirectory)
        {
            if (SFile.Exists(fname) & fname.Length <= 260)//проверка на существование файла и корректность имени
            {
                if (onlyDirectory)//проверка на перегрузку
                {
                    try
                    {
                        string[] str1 = SDirectory.GetDirectories(Path.GetDirectoryName(fname));//получение имени каталога файла
                        return str1.ToString();
                    }
                    catch (Exception ex)
                    {
                        LogForOperations("Просмотр файла", ex.Message);//запись в лог ошибки (если есть) 
                        throw ex;
                    }
                }
                else
                {
                    return view();//если перегрузка имеет значение 0
                }
            }
            else
            {
                LogForOperations("Просмотр файла", "файл не существует либо содержит в своем имени более 260 символов, либо папка содержит в имени более 260 символов");//запись ошибки в лог, если не выполняется условие проверки
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

        /// <summary>
        /// Информация о файле/папке.
        /// </summary>
        /// <param name="onlyDirectory">Просмотр информации только о папке, при значении true.</param>
        /// <returns>Дата создания, последнего изменения, последнего обращения к файлу, атрибуты файла, доступ к файлу.</returns>
        public string[] info(bool onlyDirectory)
        {
            if (onlyDirectory)//проверка на перегрузку
            {
                if (SDirectory.Exists(Path.GetDirectoryName(fname)) & Path.GetDirectoryName(fname).Length <= 260)//проверка на существование директории и корректности имени
                {
                    try
                    {
                        List<string> _Info = new List<string>();//создание списка, куда будут заноситься сведения
                        _Info.Add(Convert.ToString("\nВремя создания дирректории: " + SDirectory.GetCreationTime(Path.GetDirectoryName(fname))));//время создания дирректории
                        _Info.Add(Convert.ToString("\nВремя последнего изменения дирректории: " + SDirectory.GetLastWriteTime(Path.GetDirectoryName(fname))));//время последнего изменения дирректории
                        _Info.Add(Convert.ToString("\nВремя последнего обращения к дирректории: " + SDirectory.GetLastAccessTime(Path.GetDirectoryName(fname))));//время последнего обращения к дирректории
                        _Info.Add(Convert.ToString("\nУровень доступа к дирректории: " + SDirectory.GetAccessControl(Path.GetDirectoryName(fname))));//уровень доступа к дирректории
                        _Info.Add(Convert.ToString("\nРазмер дирректории: " + SizeDirrecrory(Path.GetDirectoryName(fname))));//размер дирректории
                        _Info.Add("\nСписок файлов в дирректории: ");//список файлов в дирректории
                        string[] file = SDirectory.GetFiles(Path.GetDirectoryName(fname));//создание массива строк для имен файлов   


                        for (int i = 0; i < file.Length; i++)
                        {
                            _Info.Add("\n" + file[i]);//добавление в список имя файла
                        }
                        _Info.Add("\nКонец списка файлов.");//объявление о конце операции
                        return _Info.ToArray();////передача информации о папке с файлами
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
            else
            {
                return info();//если перегрузка имеет значение 0
            }
        }
        /// <summary>
        /// Сортировка содержимого.
        /// </summary>
        /// <param name="strReverse">Обратная сортировка.</param>
        /// <returns>Отсортированный массив.</returns>
        public string[] sort(bool strReverse)
        {
            stream.Close();//закрываем поток
            try
            {
                if (fname.Length != 0 & SFile.Exists(fname))//проверка на существование файла и корректности имени
                {
                    string[] str = SFile.ReadAllLines(fname);//считываем строки
                    if (str.Length != 0)//проверка на существование аргумента
                    {
                        Array.Sort(str);//сортируем
                        if (strReverse)//обратная сортировка при значении true
                        {
                            Array.Reverse(str);
                        }
                    }
                    else
                    {
                        LogForOperations("Сортировка файла", "файл пуст");
                    }
                    return str;

                }
                else
                {
                    LogForOperations("Сортировка файла", "не указано имя файла либо файл не существует");//запись ошибки в лог, если не выполняется условие проверки

                    return null;
                }
            }


            catch (Exception e)//обработка исключений для сортирвки
            {

                LogForOperations("Сортировка файла", e.Message); //запись в лог ошибки (если есть)
                throw e;

            }

        }
    }
}
#endregion