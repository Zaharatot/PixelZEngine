using PixelZEngine.Clases.DataClases;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PixelZEngine.Clases.WorkClases
{
    /// <summary>
    /// Класс работы со сценами
    /// </summary>
    internal class SceneWorker : IDisposable
    {
        /// <summary>
        /// Класс загрузки сцен
        /// </summary>
        private SceneLoader sl;
        /// <summary>
        /// Список сцен проекта
        /// </summary>
        private List<scene> scenes;
        /// <summary>
        /// Поток обновления отрисовки пикселов
        /// </summary>
        private Thread redrawThread;

        /// <summary>
        /// Id активной сцены
        /// </summary>
        public int activeSceneId { get; set; }

        /// <summary>
        /// Массив пикселов для отрисовки
        /// </summary>
        public pixel[] drawPixels { get; private set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public SceneWorker()
        {
            //Инициализируем всё
            init();
        }

        /// <summary>
        /// Инициализатор класса
        /// </summary>
        private void init()
        {
            //Инициализируем класс загрузки сцен
            sl = new SceneLoader();
            //Инициализируем массив пикселов для отрисовки
            drawPixels = new pixel[0];
            //Id активной сцены ставим в "не определено"
            activeSceneId = -1;
            //Инициализируем список сцен
            initScenes();
            //Инициализируем поток перерисовки
            redrawThread = new Thread(redraw);
        }

        /// <summary>
        /// Инициализируем список сцен
        /// </summary>
        private void initScenes()
        {
            //Грузим все сцены из файлов
            scenes = sl.loadScenes();
        }

        /// <summary>
        /// Запускаем выдачу пикселей для отрисовки
        /// </summary>
        public void startWork()
        {
            //Запускаем поток перерисовки
            redrawThread.Start();
        }

        /// <summary>
        /// Загружаем все пиксели активной сцены, для рендеринга
        /// и обновляем массив пикселов для отрисовки
        /// </summary>
        private void redraw()
        {
            //Инициализируем выходной список
            List<pixel> ex = new List<pixel>();

            do
            {
                //Очищаем список
                ex.Clear();

                //Если есть активная сцена
                if (activeSceneId != -1)
                    //Возвращаем все пиксели сцены
                    ex = scenes[activeSceneId].getScenePixels();

                //Обновляем массив перерисовки
                drawPixels = ex.ToArray();
            } while (true);
        }

        /// <summary>
        /// Выполняем анимации, для активной сцены
        /// </summary>
        public void animateActiveScene()
        {
            //Если есть активная сцена
            if (activeSceneId != -1)
                //Выполняем анимации для всех её спрайтов
                scenes[activeSceneId].animateSprites();
        }

        /// <summary>
        /// ПОлучаем цвет заднего плана активной сцены
        /// </summary>
        /// <returns>Цвет заднего платна</returns>
        public Color getActiveSceneBg()
        {
            Color ex = Color.Coral;

            //Если есть активная сцена
            if (activeSceneId != -1)
                //Возвращаем цвет заднего плана
                ex = scenes[activeSceneId].bgColor;

            return ex;
        }


        /// <summary>
        /// Закрываем все используемые ресурсы
        /// </summary>
        public void Dispose()
        {
            //Грохаем поток перерисовки
            if ((redrawThread != null) && (redrawThread.IsAlive))
                redrawThread.Abort();
        }
    }
}
