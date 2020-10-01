using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fmanager;


namespace Test
{
    class Program
    {
        //public static void Help()
        //{
        //    Console.WriteLine("/");
        //}
        //public static IManager _meneger;
        static void Main(string[] args)
        {

            Directory _d = new Directory(@"C:\1");
            _d.copyto(@"C:\2");
            Console.WriteLine("Метод копирования папки выполнен.");
            Directory _d1 = new Directory(@"C:\2\3");
            _d1.create();
            _d.moveto(@"C:\2");
            Console.WriteLine("Метод перемещения выполнен.");
            //_d1.arhiv(@"C:\2");
            //Console.WriteLine("Метод архивирования выполнен.");
            Console.WriteLine(_d.view());
            Console.WriteLine("Метод просмотра содержимого выполнен.");
            foreach (string str in _d.sort())
            {
                Console.WriteLine(str);

            }
            Console.WriteLine("Метод сортировки выполнен.");
            foreach (string str in _d.info())
            {
                Console.WriteLine(str);

            }
            Console.WriteLine("Метод информации выполнен.");
            #region TestFile
            //try
            //{
            //    File _f = new File(@"C:\1\2.txt");

            //    _f.copyto(@"C:\2\2.txt");
            //    Console.WriteLine("Метод копирования выполнен.");
            //    Console.WriteLine(_f.view());
            //    Console.WriteLine("Метод просмотра содержимого выполнен.");
            //    foreach (string str in _f.sort())
            //    {
            //        Console.WriteLine(str);
                   
            //    }
            //    Console.WriteLine("Метод сортировки выполнен.");
            //    foreach (string str in _f.info())
            //    {
            //        Console.WriteLine(str);
                  
            //    }
            //    Console.WriteLine("Метод информации выполнен.");
            //    _f.arhiv(@"C:\1\2.rar");
            //    Console.WriteLine("Метод архивирования выполнен.");
            //    File _ar = new File(@"C:\1\2.rar");
            //    _ar.desarhiv(@"L:\");
            //    Console.WriteLine("Метод разархивирования выполнен.");
            //    _f.coding();
            //    Console.WriteLine("Метод смены кодировки выполнен.");
            //    _f.encoder();
            //    Console.WriteLine("Метод шифрования выполнен.");
            //    _f.uncoder();
            //    Console.WriteLine("Метод дешифрования выполнен.");
            //}
            //catch (Exception e) { Console.WriteLine(e.Message); }
            #endregion
            Console.ReadKey();

            //int _setCount = 0;
            //foreach (string _arg in args)
            //{
            //    if (_arg.ToLower().StartsWith("/set="))
            //        _setCount++;
            //}

            //switch (_setCount)
            //{
            //    case 0: Help(); return;
            //    case 1:
            //    case 2:
            //        string _fileName = string.Empty;
            //        foreach (string _arg in args)
            //        {
            //            if (_arg.StartsWith("/set="))
            //            {
            //                _fileName = _arg.Remove(0, 5).Trim('"');

            //                if (System.IO.File.Exists(_fileName))
            //                {
            //                    _meneger = new File(_fileName);
            //                }
            //                else
            //                {
            //                    _meneger = new Directory(_fileName);
            //                }
            //            }
            //        }
            //        break;

            //    default:
            //        List<string> NameCatalog = new List<string>();//хранит имена каталогов
            //        List<string> NameFile = new List<string>();//хранит имена файлов
            //        foreach (string _arg in args)
            //        {
            //            if (System.IO.File.Exists(_arg.Remove(0, 5).Trim('"')))
            //            {
            //                NameFile.Add(_arg.Remove(0, 5).Trim('"'));
            //            }
            //            else if (System.IO.Directory.Exists(_arg.Remove(0, 5).Trim('"')))
            //            {
            //                NameCatalog.Add(_arg.Remove(0, 5).Trim('"'));
            //            }
            //        }
            //        if (NameFile.Count != 0)
            //        {
            //            _meneger = new Files(NameFile);
            //        }
            //        else
            //        {
            //            Console.WriteLine("Текущий путь отсутсвует."); return;
            //        }
            //        break;

            //}
            //foreach (string _arg in args)
            //{
            //    switch (_arg.ToLower())
            //    {
            //        //вызов методов без параметров
            //        case "/delete": _meneger.delete(); break;
            //        case "/create": _meneger.create(); break;
            //        case "/uncod": _meneger.uncoder(); break;
            //        case "/encod": _meneger.encoder(); break;
            //        case "/coding": _meneger.coding(); break;
            //        case "/edit": _meneger.editing(); break;
            //        //методы с выводом на консоль
            //        case "/info": Console.WriteLine(_meneger.info()); break;
            //        case "/view": Console.WriteLine(_meneger.view()); break;
            //        case "/compareto": Console.WriteLine(_meneger.sort()); break;

            //        default:

            //            //вызов методов с параметрами
            //            if (_arg.StartsWith("/moveto="))
            //                _meneger.moveto(_arg.Replace("/moveto=", ""));
            //            else if (_arg.StartsWith("/copyto="))
            //                _meneger.copyto(_arg.Replace("/copy=", ""));
            //            else if (_arg.StartsWith("/rename="))
            //                _meneger.rename(_arg.Replace("/rename=", ""));
            //            else if (_arg.StartsWith("/arhiving="))
            //                _meneger.arhiv(_arg.Replace("/arhiving=", ""));
            //            else if (_arg.StartsWith("/desarhiving="))
            //                _meneger.desarhiv(_arg.Replace("/desarhiving=", ""));
            //            else if (_arg.StartsWith("/set="))
            //            {

            //            }
            //            else
            //            {
            //                Console.Write("Команда отсутствует.");
            //            }
            //            break;
            //    }
            //    Console.WriteLine("The end. Press any key...");
            //    Console.ReadKey();
            //}

        }
    }
}
