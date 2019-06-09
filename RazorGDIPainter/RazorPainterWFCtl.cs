// Test control fronend for WindowsForms for RazorGDIPainter library
//   (c) Mokrov Ivan
// special for habrahabr.ru
// under MIT license

using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace PixelZEngine.RazorGDIPainter
{
    /// <summary>
    /// Контролл, выполняющий функционал 
    /// сверхбыстрой отрисовки изображений
    /// </summary>
    internal partial class RazorPainterWFCtl : UserControl
    {

        /// <summary>
        /// Ссылка на объект Graphics текущего контролла, в памяти
        /// </summary>
        private readonly HandleRef hDCRef;
        /// <summary>
        /// Свойство Graphics текущего контролла
        /// </summary>
        private readonly Graphics hDCGraphics;
        /// <summary>
        /// Класс сверхбыстрой отрисовки изображения в контролл
        /// </summary>
        private readonly RazorPainter RP;


        /// <summary>
        /// root Bitmap
        /// </summary>
        public Bitmap RazorBMP { get; private set; }

        /// <summary>
        /// Graphics object to paint on RazorBMP
        /// </summary>
        public Graphics RazorGFX { get; private set; }

        /// <summary>
        /// Lock it to avoid resize/repaint race
        /// </summary>
        public readonly object RazorLock = new object();

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public RazorPainterWFCtl()
        {
            //Инициализируем компоненты
            InitializeComponent();


            //Убираем излишние автоматические действия
            //Отключаем двойную буферизацию
            SetStyle(ControlStyles.DoubleBuffer, false);
            //Событие Paint не вызывается системой
            SetStyle(ControlStyles.UserPaint, true);
            //Не вызывается событие очистки окна
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //Элемент управления по умолчанию непрозрачный и его фон не закрашивается
            SetStyle(ControlStyles.Opaque, true);

            //Получаем объект Graphics, от текущего контролла
            hDCGraphics = CreateGraphics();

            //Получаем ссылку на объект Graphics в памяти
            hDCRef = new HandleRef(hDCGraphics, hDCGraphics.GetHdc());

            //Инициализируем класс сверхюыстрой отрисовки изображений
            RP = new RazorPainter();
            //Инициализируем отрисовываемое изображение
            RazorBMP = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            //Инициализируем объект GRaphics, для отрисовки в контролл
            RazorGFX = Graphics.FromImage(RazorBMP);

            //Событие ресайза контролла
            this.Resize += (sender, args) =>
            {
                lock (RazorLock)
                {
                    //Очищаем изображение и объект Graphics, если они были проинициализированы
                    if (RazorGFX != null) RazorGFX.Dispose();
                    if (RazorBMP != null) RazorBMP.Dispose();
                    //Инициализируем отрисовываемое изображение
                    RazorBMP = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
                    //Инициализируем объект GRaphics, для отрисовки в контролл
                    RazorGFX = Graphics.FromImage(RazorBMP);
                }
            };
        }


        /// <summary>
        /// After all in-memory paint on RazorGFX, call it to display it on control
        /// </summary>
        public void RazorPaint()
        {
            //Выполняем отрисовку содержимого изображения на контролл
            RP.Paint(hDCRef, RazorBMP);
        }
    }
}
