using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PixelZEngine.Clases.DataClases
{
    /// <summary>
    /// Класс одного псевдопикселя изображения
    /// </summary>
    public class pixel
    {
        /// <summary>
        /// Кисть, которой закрашиваем пиксель
        /// </summary>
        public SolidBrush color { get; set; }
        /// <summary>
        /// Координаты и размеры пикселя
        /// </summary>
        public Rectangle position { get; set; }
        /// <summary>
        /// Расположение пикселя внутри спрайта
        /// </summary>
        public Point spritePosition { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="col">Цвет пикселя</param>
        /// <param name="size">Размер пиеселя</param>
        /// <param name="coord">Координаты пикселя</param>
        /// <param name="spritePosition">Расположение пикселя внутри спрайта</param>
        public pixel(Color col, Point coord, Point spritePosition, int size)
        {
            //РАсположение пикселя внутри спрайта
            this.spritePosition = spritePosition;
            //Инициализируем кисть отрисовки
            color = new SolidBrush(col);
            //Инициализируем координаты
            position = new Rectangle(coord.X, coord.Y, 5, 5);
        }

        /// <summary>
        /// Сдвигаем пиксель
        /// </summary>
        /// <param name="x">Значение сдвига по оси X</param>
        /// <param name="y">Значение сдвига по оси Y</param>
        /// <param name="size">Новый размер пикселя</param>
        public void movePixel(int x, int y, int size)
        {
            //Обновляем расположение и размер пикселя
            position = new Rectangle(position.X + x, position.Y + y, size, size);
        }
    }
}
