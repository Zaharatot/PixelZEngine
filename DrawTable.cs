using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PixelZEngine.RazorGDIPainter;
using PixelZEngine.Clases.WorkClases;

namespace PixelZEngine
{
    /// <summary>
    /// Контролл, ответственный за отрисовку всех объектов
    /// </summary>
    public partial class DrawTable : UserControl
    {
        /// <summary>
        /// Контролл отрисовки примитивов
        /// </summary>
        private RazorPainterWFCtl razor;
        /// <summary>
        /// Класс отрисовки спрайтов на движке
        /// </summary>
        private MainDraw draw; 

        /// <summary>
        /// Конструктор контролла
        /// </summary>
        public DrawTable()
        {
            //Инициализация компонентов
            InitializeComponent();
            //Инициализация класса
            init();
        }

        /// <summary>
        /// Инициализация класса
        /// </summary>
        private void init()
        {
            //Инициализируем контролл отрисовки примитивов
            initRazor();
            //Инициализируем класс отрисовки
            draw = new MainDraw(razor);


            //Добавляем обработчик события удаления компонента
            this.Disposed += DrawTable_Disposed;
        }

        /// <summary>
        /// Обработчик события удаления компонента
        /// </summary>
        private void DrawTable_Disposed(object sender, EventArgs e)
        {
            //Завершаем работу с рисованием
            draw.Dispose();
        }

        /// <summary>
        /// Инициализируем контролл отрисовки примитивов
        /// </summary>
        private void initRazor()
        {
            //Инициализируем
            razor = new RazorPainterWFCtl();
            //Проставляем заполнение
            razor.Dock = DockStyle.Fill;
            //Добавляем его на наш основной контролл
            this.Controls.Add(razor);
        }



        /// <summary>
        /// Запускаем рисование
        /// </summary>
        public void startDraw()
        {
            //Запускаем отрисовку
            draw.startDraw();
        }



        /// <summary>
        /// Полностью завершаем рисование
        /// </summary>
        public void closeDraw()
        {
            //Завершаем работу с рисованием
            draw.Dispose();
        }
    }
}
