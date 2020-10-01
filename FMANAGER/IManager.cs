using System;

namespace Fmanager
{
    /// <summary>
    /// Интерфейс для всех классов
    /// </summary>
    public interface IManager
    {
        /// <summary>
        /// Удаляет файловый объект.
        /// </summary>
        void delete();

        /// <summary>
        /// Создает файловый объект. 
        /// </summary>
        void create();


        /// <summary>
        /// Перемещает по указанному пути файловый объект.
        /// </summary>
        /// <param name="newName">Новая папка.</param>
        void moveto(string NewName);

        /// <summary>
        ///  Меняет кодировку файла/файлов.
        /// </summary>
        void coding();

        /// <summary>
        /// Шифрует файл/файлы.
        /// </summary>
        void encoder();

        /// <summary>
        /// Дешефрует файл/файлы.
        /// </summary>
        void uncoder();

        /// <summary>
        /// Архивирует и сохраняет в заданное место файловый объект.
        /// </summary>
        /// <param name="newName">Новое имя пути.</param>
        void arhiv(string NewName);

        /// <summary>
        /// Дезархивирует и сохраняет в заданное место файловый объект.
        /// </summary>
        /// <param name="newName">Новое имя пути.</param>
        void desarhiv(string NewName);

        /// <summary>
        /// Копирует в заданное место файловый объект.
        /// </summary>
        /// <param name="newName">Место для сохранения.</param>
        void copyto(string NewName);

        /// <summary>
        /// Просматривает содержимое файлового объекта.
        /// </summary>
        string view();

        /// <summary>
        /// Сортирует элементы по умолчанию файлового объекта.
        /// </summary>
        string[] sort();

        /// <summary>
        /// Информация о файле.
        /// </summary>
        /// <returns>Информация о файле.</returns>
        string[] info();

        /// <summary>
        /// Переименовывает на указанное имя.
        /// </summary>
        /// <param name="newName">Новое имя.</param>
        void rename(string NewName);

        /// <summary>
        /// Редактирует файловый объект.
        /// </summary>
        void editing();
    }
}
