using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelZEngine.Clases.DataClases.LoaderInfo
{
    /// <summary>
    /// Класс сцены, используемый для загрузки из файла
    /// </summary>
    [Serializable]
    public class sceneL
    {
        /// <summary>
        /// Уникальный идентификатор сцены
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// Цвет заднего плана
        /// </summary>
        public Color bgColor { get; set; } 
        /// <summary>
        /// Список спрайтов для сцены
        /// </summary>
        public List<spriteL> sprites { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public sceneL()
        {
            //Инициализируем дефолтные значения
            sprites = new List<spriteL>();
            bgColor = Color.Coral;
            id = -1;
        }
    }
}
