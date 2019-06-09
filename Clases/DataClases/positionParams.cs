using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelZEngine.Clases.DataClases
{
    /// <summary>
    /// Параметры расположения и размера объекта
    /// </summary>
    public class positionParams
    {
        /// <summary>
        /// Координата x
        /// </summary>
        public int x { get; set; }
        /// <summary>
        /// Координата Y
        /// </summary>
        public int y { get; set; }
        /// <summary>
        /// Размер
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public positionParams()
        {
            //Проставляем дефолтные параметры
            x = y = 0;
            size = 5;
        }

        /// <summary>
        /// Получаем координату, с учётом предыдущих пискелов
        /// </summary>
        /// <param name="x">Количество пикселов по оси X</param>
        /// <param name="y">Количество пикселов по оси Y</param>
        /// <returns>Точка левого верхнего угла текущего пикселя</returns>
        public Point getPoint(int x, int y) =>
            //Формируем точку левого верхнего угла нового пикселя
            new Point(x * size + this.x, y * size + this.y);
    }
}
