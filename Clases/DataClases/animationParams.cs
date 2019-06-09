using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelZEngine.Clases.DataClases
{
    /// <summary>
    /// Параметры анимации
    /// </summary>
    public class animationParams
    {
        /// <summary>
        /// Продолжительность кадра анимации, в секундах
        /// </summary>
        public double animationDelay { get; set; }
        /// <summary>
        /// Количество кадров анимации
        /// </summary>
        public int countFrames { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public animationParams()
        {
            //Проставляем дефолтные значения
            animationDelay = -1;
            countFrames = 1;
        }
    }
}
