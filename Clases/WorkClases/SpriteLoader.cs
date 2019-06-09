using PixelZEngine.Clases.DataClases;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelZEngine.Clases.WorkClases
{
    /// <summary>
    /// Класс загрузки спрайтов
    /// </summary>
    public class SpriteLoader
    {
        /// <summary>
        /// Рандомайзер
        /// </summary>
        private Random r;
        /// <summary>
        /// Случайный цвет
        /// </summary>
        private Color rand;
        /// <summary>
        /// Заменяемый цвет
        /// </summary>
        private Color? replacement;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public SpriteLoader()
        {
            //Инициализируем рандом
            r = new Random();

        }

        /// <summary>
        /// Проверка цвета, для замены на рандомный
        /// </summary>
        /// <param name="col">Проверяемый цвет</param>
        /// <returns>Заменённый цвет</returns>
        private Color checkColor(Color col)
        {
            //Если нужно менять цвет
            if (replacement.HasValue && (
                    (col.A == replacement.Value.A) &&
                    (col.R == replacement.Value.R) && 
                    (col.G == replacement.Value.G) && 
                    (col.B == replacement.Value.B))
                )
                    //Заменяем
                    col = rand;

            return col;
        }

        /// <summary>
        /// Загружаем спрайт из изображения
        /// </summary>
        /// <param name="pic">Исходное изображение</param>
        /// <param name="position">Параметры положения и размера спрайта</param>
        /// <param name="replacement">Заменяемый цвет</param>
        /// <param name="animationDelay">Cкорость смены кадров</param>
        /// <param name="animation">Параметры анимации</param>
        /// <param name="id">Уникальный идентификатор спрайта</param>
        /// <returns>ЗАгруженный спрайт</returns>
        public sprite load(long id, Bitmap pic, positionParams position, Color? replacement = null, animationParams animation = null)
        {
            //Итоговый спрайт
            sprite ex = null;
            //Ширина одного кадра
            int frameWidth;
            //Координата начала нового кадра
            int x0;
            //Кадр, считанный из картинки
            List<framePixel> frame;

            try
            {
                //Если параметры анимации не указаны
                if (animation == null)
                    //Проставляем дефолтные
                    animation = new animationParams();

                //Инициализируем новый спрайт
                ex = new sprite(position.size, animation, id);

                //Если нужно менять цвет
                if (replacement.HasValue)
                    //Генерируем рандомный цвет
                    rand = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));

                //Запоминаем инфу о замене цвета
                this.replacement = replacement;

                //Получаем ширину одного кадра
                frameWidth = pic.Width / animation.countFrames;
                //Начало первого кадра всегда в ноле
                x0 = 0;

                //Проходимся по всем кадрам
                for (int i = 0; i < animation.countFrames; i++)
                {
                    //Считываем все пиксели кадра
                    frame = loadFrame(x0, frameWidth, pic, position);
                    //Переходим к следующему кадру
                    x0 += frameWidth;

                    //Добавляем новый кадр в спрайт
                    ex.addFrame(frame.Count);
                    //Проходимся по всем считанным пикселям
                    for(int j = 0; j < frame.Count; j++)
                        //Добавляем пиксель в кадр
                        ex.setPixel(frame[i], i);
                }
                //Завершаем загрузку кадров
                ex.completeLoad();
            }
            catch { ex = null; }

            //Возвращаем спрайт
            return ex;
        }

        /// <summary>
        /// Загружаем кадр из общей картинки
        /// </summary>
        /// <param name="x0">Координата начала кадра, по оси X</param>
        /// <param name="width">Ширина кадра</param>
        /// <param name="image">Изображение с кадрами</param>
        /// <param name="position">Параметры положения и размера спрайта</param>
        /// <returns>Список пикселей для загрузки в спрайт</returns>
        private List<framePixel> loadFrame(int x0, int width, Bitmap image, positionParams position)
        {
            //Выходной массив пикселов
            List<framePixel> ex = new List<framePixel>();
            //Буфер, куда читаем цвет из картинки
            Color buff;
            //Координата правого края кадра
            int x1;
            //Координата x пикселя, внутри спрайта
            int x_sprite = 0;

            try
            {
                //Получаем координату правого края кадра
                x1 = x0 + width;

                //Проходимся по высоте картинки
                for (int y = 0; y < image.Height; y++)
                    //Проходимся по ширине текущего кадра
                    for (int x = x0; x < x1; x++)
                    {
                        //Получаем цвет пиксела
                        buff = image.GetPixel(x, y);
                        //Если пиксель не полностью прозрачный
                        if (buff.A > 0)
                            //Добавляем новый пиксель в список
                            ex.Add(new framePixel()
                            {
                                color = checkColor(buff),
                                position = position.getPoint(x, y),
                                spritePosition = new Point(x_sprite, y)
                            });

                        //Меняем координату пикселя внутри спрайта
                        x_sprite++;
                    }

            }
            catch { ex = new List<framePixel>(); }

            return ex;
        }
    }
}
