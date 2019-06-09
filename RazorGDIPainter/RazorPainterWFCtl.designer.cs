using System.Drawing;

namespace PixelZEngine.RazorGDIPainter
{
    partial class RazorPainterWFCtl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            lock (this)
            {
                if (RazorGFX != null) RazorGFX.Dispose();
                if (RazorBMP != null) RazorBMP.Dispose();
                if (hDCGraphics != null) hDCGraphics.Dispose();
                RP.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            //Устанавливаем минимальный размер контролла
            this.MinimumSize = new Size(1, 1);
        }

        #endregion
    }
}
