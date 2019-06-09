using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelZEngine.Clases.DataClases
{
    /// <summary>
    /// Пиксели одного кадра, считанные из изображения
    /// </summary>
    public class framePixel
    {
        /// <summary>
        /// Цвет пикселя
        /// </summary>
        public Color color { get; set; }
        /// <summary>
        /// Координаты пикселя
        /// </summary>
        public Point position { get; set; }
        /// <summary>
        /// Координаты пикселя внутри спрайта
        /// </summary>
        public Point spritePosition { get; set; }


        /// <summary>
        /// Конструктор класса
        /// </summary>
        public framePixel()
        {
            //Проставляем дефолтные значения

        }
    }
}
