namespace trajectory
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pbGraph = new PictureBox();
            txtV0 = new TextBox();
            txtA = new TextBox();
            txtDT = new TextBox();
            txtAngle = new TextBox();
            txtTMax = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            btnDraw = new Button();
            btnClear = new Button();
            btnSelectColor = new Button();
            colorDialog1 = new ColorDialog();
            txtY0 = new TextBox();
            txtX0 = new TextBox();
            label6 = new Label();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)pbGraph).BeginInit();
            SuspendLayout();
            // 
            // pbGraph
            // 
            pbGraph.BackColor = Color.White;
            pbGraph.Location = new Point(30, 154);
            pbGraph.Name = "pbGraph";
            pbGraph.Size = new Size(811, 503);
            pbGraph.TabIndex = 0;
            pbGraph.TabStop = false;
            // 
            // txtV0
            // 
            txtV0.Location = new Point(925, 244);
            txtV0.Name = "txtV0";
            txtV0.Size = new Size(105, 27);
            txtV0.TabIndex = 1;
            // 
            // txtA
            // 
            txtA.Location = new Point(925, 301);
            txtA.Name = "txtA";
            txtA.Size = new Size(105, 27);
            txtA.TabIndex = 2;
            // 
            // txtDT
            // 
            txtDT.Location = new Point(925, 414);
            txtDT.Name = "txtDT";
            txtDT.Size = new Size(105, 27);
            txtDT.TabIndex = 4;
            // 
            // txtAngle
            // 
            txtAngle.Location = new Point(925, 357);
            txtAngle.Name = "txtAngle";
            txtAngle.Size = new Size(105, 27);
            txtAngle.TabIndex = 3;
            // 
            // txtTMax
            // 
            txtTMax.Location = new Point(925, 471);
            txtTMax.Name = "txtTMax";
            txtTMax.Size = new Size(105, 27);
            txtTMax.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(857, 247);
            label1.Name = "label1";
            label1.Size = new Size(64, 20);
            label1.TabIndex = 6;
            label1.Text = "Поч. шв";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(857, 304);
            label2.Name = "label2";
            label2.Size = new Size(55, 20);
            label2.TabIndex = 7;
            label2.Text = "Приск.";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(857, 417);
            label3.Name = "label3";
            label3.Size = new Size(58, 20);
            label3.TabIndex = 9;
            label3.Text = "Крок ч.";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(857, 360);
            label4.Name = "label4";
            label4.Size = new Size(63, 20);
            label4.TabIndex = 8;
            label4.Text = "Кут (гр.)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(857, 474);
            label5.Name = "label5";
            label5.Size = new Size(59, 20);
            label5.TabIndex = 10;
            label5.Text = "Макс ч.";
            // 
            // btnDraw
            // 
            btnDraw.Location = new Point(857, 542);
            btnDraw.Name = "btnDraw";
            btnDraw.Size = new Size(100, 29);
            btnDraw.TabIndex = 11;
            btnDraw.Text = "Побудувати";
            btnDraw.UseVisualStyleBackColor = true;
            btnDraw.Click += btnDraw_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(967, 542);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(100, 29);
            btnClear.TabIndex = 12;
            btnClear.Text = "Очистити";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnSelectColor
            // 
            btnSelectColor.Font = new Font("Segoe UI", 8F);
            btnSelectColor.Location = new Point(912, 577);
            btnSelectColor.Name = "btnSelectColor";
            btnSelectColor.Size = new Size(100, 29);
            btnSelectColor.TabIndex = 13;
            btnSelectColor.Text = "Вибрати кол.";
            btnSelectColor.UseVisualStyleBackColor = true;
            btnSelectColor.Click += btnSelectColor_Click;
            // 
            // txtY0
            // 
            txtY0.Location = new Point(925, 198);
            txtY0.Name = "txtY0";
            txtY0.Size = new Size(105, 27);
            txtY0.TabIndex = 14;
            // 
            // txtX0
            // 
            txtX0.Location = new Point(925, 154);
            txtX0.Name = "txtX0";
            txtX0.Size = new Size(105, 27);
            txtX0.TabIndex = 15;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(855, 157);
            label6.Name = "label6";
            label6.Size = new Size(26, 20);
            label6.TabIndex = 16;
            label6.Text = "X0";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(855, 201);
            label7.Name = "label7";
            label7.Size = new Size(25, 20);
            label7.TabIndex = 17;
            label7.Text = "Y0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 853);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(txtX0);
            Controls.Add(txtY0);
            Controls.Add(btnSelectColor);
            Controls.Add(btnClear);
            Controls.Add(btnDraw);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtTMax);
            Controls.Add(txtDT);
            Controls.Add(txtAngle);
            Controls.Add(txtA);
            Controls.Add(txtV0);
            Controls.Add(pbGraph);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pbGraph).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbGraph;
        private TextBox txtV0;
        private TextBox txtA;
        private TextBox txtDT;
        private TextBox txtAngle;
        private TextBox txtTMax;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button btnDraw;
        private Button btnClear;
        private Button btnSelectColor;
        private ColorDialog colorDialog1;
        private TextBox txtY0;
        private TextBox txtX0;
        private Label label6;
        private Label label7;
    }
}
