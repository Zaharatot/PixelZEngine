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
        //TODO: добавить эффекты для спрайтов. Они будут управлять расположением пикселей.
        //TODO: добавить вращение спрайтов (это относится к эффектам)
        //TODO: добавить анимационные эффекты (например - шлейф частиц, взрыв).


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
        /// Плотность объекта. Является множителем 
        /// для скорости прохождения сквозь объект.
        /// </summary>
        private double density;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="animation">Параметры анимации кадра</param>
        /// <param name="size">Размер пикселя спрайта</param>
        /// <param name="id">Уникальынй идентификатор спрайта</param>
        /// <param name="density">Плотность объекта</param>
        public sprite(int size, animationParams animation, long id, double density)
        {
            //Запоминаем плотность
            this.density = density;
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

        /// <summary>
        /// Обновляем размер пикселя спрайта
        /// </summary>
        /// <param name="size">Новый размер пикселя спрайта</param>
        public void resize(int size)
        {
            //Получаем разницу между старым и новым размером
            int shift = size - this.size;
            //Запоминаем новый размер
            this.size = size;

            //Проходимся по списку кадров
            for (int i = 0; i < framesCount; i++)
                //Проходимся по всем пикселям кадра
                for (int j = 0; j < frames[i].Length; j++)
                    //Меняем размер и положение каждого
                    frames[i][j].resizePixel(shift, size);
        }      

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

        /// <summary>
        /// Проверка замедления, при прохождении сквозь объект
        /// </summary>
        /// <param name="test">Массив пикселов, который тестируем</param>
        /// <returns>Значение замедления</returns>
        public double checkThrough(pixel[] test)
        {
            double ex = 0;

            //Если плотность данного объекта больше ноля
            if (density > 0)
            {
                //Получаем активный спрайт
                var sprite = getSprite();
                //Проходимся по списку пикселей спрайта
                for (int i = 0; (ex == 0) && (i < sprite.Length); i++)
                    //Проходимся по списку тестируемых пикселей
                    for (int j = 0; (ex == 0) && (j < test.Length); j++)
                        //Если спрайты пересеклись
                        if (sprite[i].checkCollision(test[j]))
                            //Возвращаем плотность объекта
                            ex = density;
            }

            return ex;
        } 
    }
}
