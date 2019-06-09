using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelZEngine.Clases.DataClases
{
    /// <summary>
    /// Класс сцены, на которой содержатся все спрайты сцены
    /// </summary>
    public class scene
    {
        //TODO: добавить надписи. они должны быть аналогичны спрайтам.

        /// <summary>
        /// Уникальный идентификатор сцены
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// Цвет заднего плана сцены
        /// </summary>
        public Color bgColor { get; set; }
        /// <summary>
        /// Список спрайтов сцены
        /// </summary>
        private sprite[] sprites;

        /// <summary>
        /// Метка времени начала отсчёта времени в формате Unix
        /// </summary>
        private DateTime startDate;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="bgColor">Цвет заднего плана сцены</param>
        /// <param name="count">Количество спрайтов в сцене</param>
        /// <param name="id">Уникальынй идентификатор сцены</param>
        public scene(int count, Color bgColor, long id)
        {
            //Запоминаем id
            this.id = id;
            //Запоминаем цвет заднего фона
            this.bgColor = bgColor;
            //Инициализируем массив спрайтов
            sprites = new sprite[count];
            //Прописываем стартовую дату метки времени
            startDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="sprites">Список спрайтов сцены</param>
        /// <param name="bgColor">Цвет заднего плана сцены</param>
        /// <param name="id">Уникальынй идентификатор сцены</param>
        public scene(sprite[] sprites, Color bgColor, long id)
        {
            //Запоминаем id
            this.id = id;
            //Запоминаем цвет заднего фона
            this.bgColor = bgColor;
            //Получаем массив спрайтов
            this.sprites = sprites;
            //Прописываем стартовую дату метки времени
            startDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        }

        /// <summary>
        /// Загружаем спрайт в сцену
        /// </summary>
        /// <param name="spr">Спрайт</param>
        /// <param name="id">Id спрайта в массиве</param>
        public void loadSprite(sprite spr, int id)
        {
            //Записываем спрайт в массив
            sprites[id] = spr;
        }

        /// <summary>
        /// ЗАгружаем все пиксели сцены, для рендеринга
        /// </summary>
        /// <returns>Список пикселей всех активных спрайтов сцены</returns>
        public List<pixel> getScenePixels()
        {
            //Инициализируем выходной массив
            List<pixel> ex = new List<pixel>();

            //проходимся по массиву спрайтов
            for(int i = 0; i < sprites.Length; i++)
                //Добавляем в список пиксели спрайта
                ex.AddRange(sprites[i].getSprite());            

            return ex;
        }

        /// <summary>
        /// Выполняем анимации для спрайтов
        /// </summary>
        public void animateSprites()
        {
            //ПОлучаем текущую метку времени
            double frameTime = timeMicro();

            //проходимся по массиву спрайтов
            for (int i = 0; i < sprites.Length; i++)
                //Обновляем кадры анимаций
                sprites[i].goToNextFrame(frameTime);
        }


        /// <summary>
        /// Получаем время, со значением микросекунд
        /// </summary>
        /// <returns>Дабловое число секунд</returns>
        private double timeMicro() =>
            (DateTime.UtcNow - startDate).TotalSeconds;


        /// <summary>
        /// Получаем спрайт по id
        /// </summary>
        /// <param name="id">Id спрайта</param>
        /// <returns>Найденный спрайт, либо null</returns>
        public sprite getSpriteById(long id)
        {
            sprite ex = null;

            try
            {
                //Проходимся по списку спрайтов
                for (int i = 0; i < sprites.Length; i++)
                    //Если нашли спрайт с таким id
                    if (sprites[i].id == id)
                    {
                        //Возвращаем результат
                        ex = sprites[i];
                        break;
                    }
            }
            catch { ex = null; }

            return ex;
        }
        
    }
}
