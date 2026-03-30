namespace trajectory2
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
            txtY0 = new TextBox();
            txtX0 = new TextBox();
            txtAngle = new TextBox();
            txtDt = new TextBox();
            lblMaxHeight = new Label();
            lblDistance = new Label();
            btnDraw = new Button();
            btnClear = new Button();
            btnSelectColor = new Button();
            colorDialog1 = new ColorDialog();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)pbGraph).BeginInit();
            SuspendLayout();
            // 
            // pbGraph
            // 
            pbGraph.BackColor = Color.White;
            pbGraph.Location = new Point(12, 125);
            pbGraph.Name = "pbGraph";
            pbGraph.Size = new Size(1080, 566);
            pbGraph.TabIndex = 0;
            pbGraph.TabStop = false;
            // 
            // txtV0
            // 
            txtV0.Location = new Point(1233, 179);
            txtV0.Name = "txtV0";
            txtV0.Size = new Size(125, 27);
            txtV0.TabIndex = 1;
            // 
            // txtY0
            // 
            txtY0.Location = new Point(1233, 326);
            txtY0.Name = "txtY0";
            txtY0.Size = new Size(125, 27);
            txtY0.TabIndex = 2;
            // 
            // txtX0
            // 
            txtX0.Location = new Point(1233, 275);
            txtX0.Name = "txtX0";
            txtX0.Size = new Size(125, 27);
            txtX0.TabIndex = 3;
            // 
            // txtAngle
            // 
            txtAngle.Location = new Point(1233, 227);
            txtAngle.Name = "txtAngle";
            txtAngle.Size = new Size(125, 27);
            txtAngle.TabIndex = 4;
            // 
            // txtDt
            // 
            txtDt.Location = new Point(1233, 378);
            txtDt.Name = "txtDt";
            txtDt.Size = new Size(125, 27);
            txtDt.TabIndex = 5;
            // 
            // lblMaxHeight
            // 
            lblMaxHeight.AutoSize = true;
            lblMaxHeight.Location = new Point(1119, 432);
            lblMaxHeight.Name = "lblMaxHeight";
            lblMaxHeight.Size = new Size(78, 20);
            lblMaxHeight.TabIndex = 6;
            lblMaxHeight.Text = "Макс. вис.";
            // 
            // lblDistance
            // 
            lblDistance.AutoSize = true;
            lblDistance.Location = new Point(1274, 432);
            lblDistance.Name = "lblDistance";
            lblDistance.Size = new Size(84, 20);
            lblDistance.TabIndex = 7;
            lblDistance.Text = "Макс. дист.";
            // 
            // btnDraw
            // 
            btnDraw.Location = new Point(1264, 480);
            btnDraw.Name = "btnDraw";
            btnDraw.Size = new Size(94, 29);
            btnDraw.TabIndex = 8;
            btnDraw.Text = "Вичислити";
            btnDraw.UseVisualStyleBackColor = true;
            btnDraw.Click += btnDraw_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(1264, 531);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(94, 29);
            btnClear.TabIndex = 9;
            btnClear.Text = "Очистити";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnSelectColor
            // 
            btnSelectColor.Location = new Point(1264, 579);
            btnSelectColor.Name = "btnSelectColor";
            btnSelectColor.Size = new Size(94, 29);
            btnSelectColor.TabIndex = 10;
            btnSelectColor.Text = "Колір";
            btnSelectColor.UseVisualStyleBackColor = true;
            btnSelectColor.Click += btnSelectColor_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1119, 182);
            label1.Name = "label1";
            label1.Size = new Size(95, 20);
            label1.TabIndex = 11;
            label1.Text = "Поч. шв (V0)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1183, 230);
            label2.Name = "label2";
            label2.Size = new Size(31, 20);
            label2.TabIndex = 12;
            label2.Text = "Кут";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1183, 329);
            label3.Name = "label3";
            label3.Size = new Size(25, 20);
            label3.TabIndex = 14;
            label3.Text = "Y0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1183, 278);
            label4.Name = "label4";
            label4.Size = new Size(26, 20);
            label4.TabIndex = 13;
            label4.Text = "X0";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1183, 381);
            label5.Name = "label5";
            label5.Size = new Size(26, 20);
            label5.TabIndex = 15;
            label5.Text = "dT";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1440, 853);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnSelectColor);
            Controls.Add(btnClear);
            Controls.Add(btnDraw);
            Controls.Add(lblDistance);
            Controls.Add(lblMaxHeight);
            Controls.Add(txtDt);
            Controls.Add(txtAngle);
            Controls.Add(txtX0);
            Controls.Add(txtY0);
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
        private TextBox txtY0;
        private TextBox txtX0;
        private TextBox txtAngle;
        private TextBox txtDt;
        private Label lblMaxHeight;
        private Label lblDistance;
        private Button btnDraw;
        private Button btnClear;
        private Button btnSelectColor;
        private ColorDialog colorDialog1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}
