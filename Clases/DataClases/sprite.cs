using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelZEngine.Clases.DataClases
{
    /// <summary>
    /// Класс, содержащий в себе информацию о спрайте
    /// </summary>
    public class sprite
    {
        //TODO: добавить проверку коллизий спрайтов

        /// <summary>
        /// Уникальный идентификатор спрайта
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// Массив кадров со спрайтами
        /// </summary>
        private pixel[][] frames;
        /// <summary>
        /// Размер одного пикселя спрайта
        /// </summary>
        private int size;
        /// <summary>
        /// Id активного кадра
        /// </summary>
        private int frameId;
        /// <summary>
        /// Параметры анимации
        /// </summary>
        private animationParams animation;
        /// <summary>
        /// Метка времени, при которой была прошлая смена кадра
        /// </summary>
        private double oldFrameTime;
        /// <summary>
        /// Количество кадров в спрайте
        /// </summary>
        public int framesCount { get; private set; }
        /// <summary>
        /// Флаг отрисовки спрайта
        /// </summary>
        public bool visible { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="animation">Параметры анимации кадра</param>
        /// <param name="size">Размер пикселя спрайта</param>
        /// <param name="id">Уникальынй идентификатор спрайта</param>
        public sprite(int size, animationParams animation, long id)
        {
            //Запоминаем id
            this.id = id;
            //Запоминаем размер пикселя
            this.size = size;
            //Запоминаем параметры анимации
            this.animation = animation;
            //Инициализируем список кадров
            frames = new pixel[animation.countFrames][];
            //Активных кадров по дефолту нету
            frameId = -1;
            //Прошлая метка времени 
            oldFrameTime = 0;
            //Ставим дефолтное количество кадров
            framesCount = animation.countFrames;
            //Разрешаем отрисовку спрайта
            visible = true;
        }

        /// <summary>
        /// Добавляем новый кадр
        /// </summary>
        /// <param name="count">Количество пикселов в кадре</param>
        public void addFrame(int count)
        {
            //Переходим к новому кадру
            frameId++;
            //Добавляем новый кадр
            frames[frameId] = new pixel[count];
        }

        /// <summary>
        /// Добавляем пиксеоль в спрайт
        /// </summary>
        /// <param name="fPixel">ИНформация о считанном пикселе</param>
        /// <param name="pixelId">Id пикселя в массиве</param>
        public void setPixel(framePixel fPixel, int pixelId)
        {
            //Добавляем пиксель в массив активного кадра
            frames[frameId][pixelId] = new pixel(
                    fPixel.color, 
                    fPixel.position, 
                    fPixel.spritePosition, 
                    size
                );
        }

        /// <summary>
        /// Завершение загрузки кадров и переход к первому
        /// </summary>
        public void completeLoad()
        {
            //Переходим к первому загруженному кадру
            frameId = 0;
        }

        /// <summary>
        /// Сдвигаем все пиксели спрайта, на указанные значения
        /// </summary>
        /// <param name="x">Значение сдвига по оси X</param>
        /// <param name="y">Значение сдвига по оси Y</param>
        public void move(int x, int y)
        {
            //Проходимся по списку кадров
            for(int i = 0; i < framesCount; i++)
                //Проходимся по всем пикселям кадра
                for (int j = 0; j < frames[i].Length; j++)
                    //Сдвигаем каждый
                    frames[i][j].movePixel(x, y, size);
        }

        /// <summary>
        /// Устанавливаем новую скорость смены кадров
        /// </summary>
        /// <param name="animationDelay">Новая скорость смены кадров</param>
        public void setNewAnimationDelay(double animationDelay)
        {
            //Запоминаем скорость смены кадров
            animation.animationDelay = animationDelay;
        }

        /*        
            Ресайз на ходу, из-за возможностий движка крайне сложен.
            А всё из-за того, что пикселы у нас отображаются не 
            матрицей, а списком. Т.е. мы в душе не ебём, какая конкретно
            координата по иксу и игрику, относительно ноля у конкретного пикселя.
            Нет, посчитать то можно, но это будет затратно.
         */

        //TODO: добавить ресайз на ходу (теперь можно)

        /// <summary>
        /// Получаем спрайт кадра
        /// </summary>
        /// <returns>Массив пикселов кадра</returns>
        public pixel[] getSprite() =>
            //Возвращаем активный спрайт, если нужно его отрисовывать
            //в противном случае - возвращаем пустой спрайт
            (visible) ? frames[frameId] : new pixel[0];


        /// <summary>
        /// Переходим к следующему кадру
        /// </summary>
        /// <param name="time">Метка времени с милисекундами</param>
        public void goToNextFrame(double time)
        {
            //Если прошло уже достаточно времени, для смены кадра
            //И смена кадра тут вообще нужна и данный спрайт 
            //вообще должен отрисовываться
            if (visible && (animation.animationDelay != -1 ) && 
                (time - oldFrameTime > animation.animationDelay))
            {
                //Запоминаем время перехода к новому кадру
                oldFrameTime = time;
                //Циклически переходим к следующему кадру
                frameId++;
                if (frameId >= animation.countFrames)
                    frameId = 0;
            }
        }
    }
}
