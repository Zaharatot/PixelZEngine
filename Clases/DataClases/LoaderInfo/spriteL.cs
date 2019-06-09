using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelZEngine.Clases.DataClases.LoaderInfo
{
    /// <summary>
    /// Класс спрайта, используемый для загрузки из файла
    /// </summary>
    [Serializable]
    public class spriteL
    {
        /// <summary>
        /// Уникальный идентификатор спрайта
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// Имя файла, из которого грузим спрайт
        /// </summary>
        public string filename { get; set; }
        /// <summary>
        /// Параметры анимации спрайта
        /// </summary>
        public animationParams animation { get; set; }
        /// <summary>
        /// Начальное расположение спрайта и его размер
        /// </summary>
        public positionParams position { get; set; }
        /// <summary>
        /// Заменяемый цвет
        /// </summary>
        public Color? replace { get; set; }
        /// <summary>
        /// Плотность объекта. Является множителем 
        /// для скорости прохождения сквозь объект.
        /// </summary>
        public double density { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public spriteL()
        {
            //Инициализируем дефолтные значения
            filename = null;
            replace = null;
            position = new positionParams();
            animation = new animationParams();
            id = -1;
            density = 1;
        }
    }
}
