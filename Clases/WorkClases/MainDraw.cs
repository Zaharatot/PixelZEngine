using PixelZEngine.RazorGDIPainter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using PixelZEngine.Clases.DataClases;

namespace PixelZEngine.Clases.WorkClases
{
    /// <summary>
    /// Основной класс отрисовки
    /// </summary>
    internal class MainDraw : IDisposable
    {
        //TODO: Короче говоря, у нас будет 5 потоков.
        // *1. Поток рендеринга. Просто отрисовывает всё, что у нас есть.
        // *2. Поток счётчика fps. Просто обновляет счётчик.
        // *3. Поток анимации. Будет выполнять обновление кадров у всех спрайтов.
        // 4. Поток перемещения спрайтов. Но он будет скорее всего не тут.
        // 5. Поток эффектов. Будет выполнять всяческие эффекты со спрайтами.
        

        /// <summary>
        /// Флаг отображения счётчика fps
        /// </summary>
        private const bool viewFps = true;

        /// <summary>
        /// Контролл отрисовки примитивов
        /// </summary>
        private RazorPainterWFCtl razor;

        /// <summary>
        /// Счётчик fps
        /// </summary>
        private int fps;
        /// <summary>
        /// Строка счётчика fps
        /// </summary>
        private string fpsString;
        /// <summary>
        /// Поток, обновляющий счётчик FPS
        /// </summary>
        private Thread fpsThread;
        /// <summary>
        /// Поток отрисовки
        /// </summary>
        private Thread renderThread;
        /// <summary>
        /// Поток анимации
        /// </summary>
        private Thread animationThread;

        /// <summary>
        /// Класс работы со сценами
        /// </summary>
        private SceneWorker sw;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="razor">Ссылка на контролл отрисовки примитивов</param>
        public MainDraw(RazorPainterWFCtl razor)
        {
            //Созраняем ссылку
            this.razor = razor;
            //Инициализируем класс
            init();
        }

        /// <summary>
        /// Инициализатор класса
        /// </summary>
        private void init()
        {
            //Инициализируем счётчик fps
            fps = 0;
            fpsString = "0";
            //Инициализируем класс работы со сценами
            sw = new SceneWorker();
            //Инициализируем поток обновления строки fps
            fpsThread = new Thread(fpsWork);
            //Инициализируем поток перерисовки
            renderThread = new Thread(render);
            //Инициализируем поток анимаций
            animationThread = new Thread(animation);
        }

        /// <summary>
        /// Запускаем рисование
        /// </summary>
        public void startDraw()
        {
            //Если нужно рисовать счётчик FPS
            if (viewFps)
                //Запускае поток обновления fps
                fpsThread.Start();
            //Запускаем поток перерисовки
            renderThread.Start();
            //ЗАпускаем поток анимации
            animationThread.Start();
        }

        /// <summary>
        /// Выполняем отрисовку
        /// </summary>
        private void render()
        {
            //Массив пикселей для отрисовки
            pixel[] pixels;

            do
            {
                //Очищаем фон
                razor.RazorGFX.Clear(sw.getActiveSceneBg());

                //Получаем список пикселов, которые нужно отрисовать
                pixels = sw.drawPixels;
                
                //Проходимся по всем отрисовываемым пикселям
                for (int i = 0; i < pixels.Length; i++)
                    //Отрисовываем их
                    razor.RazorGFX.FillRectangle(pixels[i].color, pixels[i].position);

                //Отрисовываем счётчик fps, поверх всего
                drawFpsCounter();
                //Выполняем перерисовку объейта
                razor.RazorPaint();
                //Увеличиваем значение счётчика fps
                fps++;
            } while (true);
        }

        /// <summary>
        /// Отрисовываем счётчик fps
        /// </summary>
        private void drawFpsCounter()
        {
            //Если нужно рисовать счётчик FPS
            if (viewFps)
                //Выводим его
                razor.RazorGFX.DrawString(fpsString, SystemFonts.CaptionFont, Brushes.Red, new PointF(5, 5));
        }

        /// <summary>
        /// Работа со счётчиком fps
        /// </summary>
        private void fpsWork()
        {
            do
            {
                //Обновляем строку FPS
                fpsString = fps.ToString();
                //Сбрасываем счётчик FPS
                fps = 0;

                //Спим секунду
                Thread.Sleep(1000);
            } while (true);
        }

        /// <summary>
        /// Работа с анимацией спрайтов
        /// </summary>
        private void animation()
        {
            do
            {
                //ВЫполняем все анимации, для активной сцены
                sw.animateActiveScene();
                //Спим секунду
                Thread.Sleep(15);
            } while (true);
        }


        /// <summary>
        /// Очищаем ресурсы класса
        /// </summary>
        public void Dispose()
        {
            //Грохаем поток счётчика FPS
            if((fpsThread != null) && (fpsThread.IsAlive))
                fpsThread.Abort();
            //Грохаем поток перерисовки
            if ((renderThread != null) && (renderThread.IsAlive))
                renderThread.Abort();
            //Грохаем поток анимаций
            if ((animationThread != null) && (animationThread.IsAlive))
                animationThread.Abort();
        }
    }
}
