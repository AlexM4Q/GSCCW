namespace App
{
    partial class Form1
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
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.Clear = new System.Windows.Forms.Button();
            this.Move = new System.Windows.Forms.Button();
            this.ColorSelector = new System.Windows.Forms.ComboBox();
            this.Rotate = new System.Windows.Forms.Button();
            this.Scale = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.TmoSelector = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.FlipVertically = new System.Windows.Forms.Button();
            this.FlipHorizontally = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.FigureSelector = new System.Windows.Forms.ComboBox();
            this.DeleteFigure = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox
            // 
            this.PictureBox.Location = new System.Drawing.Point(12, 100);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(569, 470);
            this.PictureBox.TabIndex = 0;
            this.PictureBox.TabStop = false;
            this.PictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
            this.PictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseMove);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(506, 43);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(75, 21);
            this.Clear.TabIndex = 1;
            this.Clear.Text = "Очистить";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // Move
            // 
            this.Move.Location = new System.Drawing.Point(12, 12);
            this.Move.Name = "Move";
            this.Move.Size = new System.Drawing.Size(83, 23);
            this.Move.TabIndex = 2;
            this.Move.Text = "Перемещать";
            this.Move.UseVisualStyleBackColor = true;
            this.Move.Click += new System.EventHandler(this.Move_Click);
            // 
            // ColorSelector
            // 
            this.ColorSelector.FormattingEnabled = true;
            this.ColorSelector.Items.AddRange(new object[] {
            "Черный",
            "Красный",
            "Зеленый",
            "Синий",
            "Белый"});
            this.ColorSelector.Location = new System.Drawing.Point(506, 12);
            this.ColorSelector.Name = "ColorSelector";
            this.ColorSelector.Size = new System.Drawing.Size(75, 21);
            this.ColorSelector.TabIndex = 3;
            this.ColorSelector.Text = "Цвет";
            this.ColorSelector.SelectedIndexChanged += new System.EventHandler(this.ColorSelector_SelectedIndexChanged);
            // 
            // Rotate
            // 
            this.Rotate.Location = new System.Drawing.Point(101, 12);
            this.Rotate.Name = "Rotate";
            this.Rotate.Size = new System.Drawing.Size(75, 23);
            this.Rotate.TabIndex = 4;
            this.Rotate.Text = "Вращать";
            this.Rotate.UseVisualStyleBackColor = true;
            this.Rotate.Click += new System.EventHandler(this.Rotate_Click);
            // 
            // Scale
            // 
            this.Scale.Location = new System.Drawing.Point(182, 12);
            this.Scale.Name = "Scale";
            this.Scale.Size = new System.Drawing.Size(102, 23);
            this.Scale.TabIndex = 5;
            this.Scale.Text = "Масштабировать";
            this.Scale.UseVisualStyleBackColor = true;
            this.Scale.Click += new System.EventHandler(this.Scale_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(290, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Нарисовать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Draw_Click);
            // 
            // TmoSelector
            // 
            this.TmoSelector.FormattingEnabled = true;
            this.TmoSelector.Items.AddRange(new object[] {
            "Объединение",
            "Пересечение",
            "Симметрическая разность",
            "Разность А - В",
            "Разность В - А"});
            this.TmoSelector.Location = new System.Drawing.Point(379, 43);
            this.TmoSelector.Name = "TmoSelector";
            this.TmoSelector.Size = new System.Drawing.Size(121, 21);
            this.TmoSelector.TabIndex = 7;
            this.TmoSelector.Text = "ТМО";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(290, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Выполнить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.TMO_Click);
            // 
            // FlipVertically
            // 
            this.FlipVertically.Location = new System.Drawing.Point(101, 41);
            this.FlipVertically.Name = "FlipVertically";
            this.FlipVertically.Size = new System.Drawing.Size(75, 23);
            this.FlipVertically.TabIndex = 9;
            this.FlipVertically.Text = "Вертикали";
            this.FlipVertically.UseVisualStyleBackColor = true;
            this.FlipVertically.Click += new System.EventHandler(this.FlipVertically_Click);
            // 
            // FlipHorizontally
            // 
            this.FlipHorizontally.Location = new System.Drawing.Point(182, 41);
            this.FlipHorizontally.Name = "FlipHorizontally";
            this.FlipHorizontally.Size = new System.Drawing.Size(102, 23);
            this.FlipHorizontally.TabIndex = 10;
            this.FlipHorizontally.Text = "Горизонтали";
            this.FlipHorizontally.UseVisualStyleBackColor = true;
            this.FlipHorizontally.Click += new System.EventHandler(this.FlipHorizontally_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Отразить по";
            // 
            // FigureSelector
            // 
            this.FigureSelector.FormattingEnabled = true;
            this.FigureSelector.Items.AddRange(new object[] {
            "Кривая Безье",
            "Полигон",
            "Правильный полигон"});
            this.FigureSelector.Location = new System.Drawing.Point(379, 12);
            this.FigureSelector.Name = "FigureSelector";
            this.FigureSelector.Size = new System.Drawing.Size(121, 21);
            this.FigureSelector.TabIndex = 12;
            this.FigureSelector.Text = "Фигура";
            // 
            // DeleteFigure
            // 
            this.DeleteFigure.Location = new System.Drawing.Point(505, 71);
            this.DeleteFigure.Name = "DeleteFigure";
            this.DeleteFigure.Size = new System.Drawing.Size(75, 23);
            this.DeleteFigure.TabIndex = 13;
            this.DeleteFigure.Text = "Удалить";
            this.DeleteFigure.UseVisualStyleBackColor = true;
            this.DeleteFigure.Click += new System.EventHandler(this.DeleteFigure_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 582);
            this.Controls.Add(this.DeleteFigure);
            this.Controls.Add(this.FigureSelector);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FlipHorizontally);
            this.Controls.Add(this.FlipVertically);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.TmoSelector);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Scale);
            this.Controls.Add(this.Rotate);
            this.Controls.Add(this.ColorSelector);
            this.Controls.Add(this.Move);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.PictureBox);
            this.Name = "Form1";
            this.Text = "ГСК - Курсовая работа. III-АИТ-9, Середенко Д. Д.";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button Move;
        private System.Windows.Forms.ComboBox ColorSelector;
        private System.Windows.Forms.Button Rotate;
        private System.Windows.Forms.Button Scale;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox TmoSelector;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button FlipVertically;
        private System.Windows.Forms.Button FlipHorizontally;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox FigureSelector;
        private System.Windows.Forms.Button DeleteFigure;
    }
}

