using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelZEngine.Clases.DataClases.LoaderInfo
{
    /// <summary>
    /// Класс, со списком сцен
    /// </summary>
    [Serializable]
    public class scenesL
    {
        /// <summary>
        /// Список сцен
        /// </summary>
        public List<sceneL> scenes { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public scenesL()
        {
            //Проставляем дефолтные значения
            scenes = new List<sceneL>();
        }
    }
}
