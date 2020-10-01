using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.AccessControl;

namespace Fmanager
{
    public interface IPersonalManager
    {
        /// <summary>
        /// Удаление файлового объекта.
        /// </summary>
        /// <param name="Copy">Копирование объекта в FileManagerLog\Temp при значении true.</param>
        void delete(bool Copy);

        /// <summary>
        /// Создание или перезапись файлового объекта.
        /// </summary>
        /// <param name="nonCreateExist">Перезапись, при значении true.</param>
        void create(bool nonCreateExist);

        /// <summary>
        /// Перемещение файлового объекта.
        /// </summary>
        /// <param name="NewName">Путь, куда нужно переместить.</param>
        /// <param name="onlyFiles">Перемещает только файлы, при значении true.</param>
        void moveto(string NewName, bool onlyFiles);

        /// <summary>
        /// Позволяет просмотреть информацию об объекте.
        /// </summary>
        /// <param name="onlyDirectory">Просмотр информации только о папке, при значении true.</param>
        /// <returns>Информация.</returns>
        string view(bool onlyDirectory);

        /// <summary>
        /// Просмотр свойств объекта.
        /// </summary>
        /// <param name="onlyDirectory">Просмотр свойств только папки, при значении true.</param>
        /// <returns>Свойства.</returns>
        string[] info(bool onlyDirectory);

        /// <summary>
        /// Сортировка файлового объекта.
        /// </summary>
        /// <param name="strReverse">Сделать сортировку обратной, при значении true.</param>
        /// <returns>Отсортированный список объектов.</returns>
        string[] sort(bool strReverse);


    }
}
