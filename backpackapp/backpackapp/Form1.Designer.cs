namespace backpackapp
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
            txtN = new TextBox();
            txtW = new TextBox();
            txtWeights = new TextBox();
            txtValues = new TextBox();
            panel1 = new Panel();
            lblSteps = new Label();
            lblExecutionTime = new Label();
            cmbMethod = new ComboBox();
            label10 = new Label();
            txtDelay = new TextBox();
            label8 = new Label();
            label9 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            lstSelectedItems = new ListBox();
            lblMaxValue = new Label();
            btnCalculate = new Button();
            dgvDP = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDP).BeginInit();
            SuspendLayout();
            // 
            // txtN
            // 
            txtN.Anchor = AnchorStyles.Right;
            txtN.Location = new Point(123, 238);
            txtN.Name = "txtN";
            txtN.Size = new Size(81, 27);
            txtN.TabIndex = 0;
            // 
            // txtW
            // 
            txtW.Anchor = AnchorStyles.Right;
            txtW.Location = new Point(123, 297);
            txtW.Name = "txtW";
            txtW.Size = new Size(81, 27);
            txtW.TabIndex = 1;
            // 
            // txtWeights
            // 
            txtWeights.Anchor = AnchorStyles.Right;
            txtWeights.Location = new Point(73, 356);
            txtWeights.Name = "txtWeights";
            txtWeights.Size = new Size(180, 27);
            txtWeights.TabIndex = 2;
            // 
            // txtValues
            // 
            txtValues.Anchor = AnchorStyles.Right;
            txtValues.Location = new Point(73, 415);
            txtValues.Name = "txtValues";
            txtValues.Size = new Size(180, 27);
            txtValues.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblSteps);
            panel1.Controls.Add(lblExecutionTime);
            panel1.Controls.Add(cmbMethod);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(txtDelay);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lstSelectedItems);
            panel1.Controls.Add(lblMaxValue);
            panel1.Controls.Add(btnCalculate);
            panel1.Controls.Add(txtN);
            panel1.Controls.Add(txtValues);
            panel1.Controls.Add(txtW);
            panel1.Controls.Add(txtWeights);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(686, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(296, 653);
            panel1.TabIndex = 4;
            // 
            // lblSteps
            // 
            lblSteps.Anchor = AnchorStyles.Right;
            lblSteps.Location = new Point(0, 103);
            lblSteps.Name = "lblSteps";
            lblSteps.Size = new Size(296, 20);
            lblSteps.TabIndex = 21;
            lblSteps.Text = "Додаткова інформація";
            lblSteps.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblExecutionTime
            // 
            lblExecutionTime.Anchor = AnchorStyles.Right;
            lblExecutionTime.AutoSize = true;
            lblExecutionTime.Location = new Point(185, 76);
            lblExecutionTime.Name = "lblExecutionTime";
            lblExecutionTime.Size = new Size(83, 20);
            lblExecutionTime.TabIndex = 20;
            lblExecutionTime.Text = "Час викон.";
            // 
            // cmbMethod
            // 
            cmbMethod.Anchor = AnchorStyles.Right;
            cmbMethod.FormattingEnabled = true;
            cmbMethod.Location = new Point(28, 72);
            cmbMethod.Name = "cmbMethod";
            cmbMethod.Size = new Size(151, 28);
            cmbMethod.TabIndex = 19;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Right;
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label10.Location = new Point(44, 153);
            label10.Name = "label10";
            label10.Size = new Size(72, 20);
            label10.TabIndex = 18;
            label10.Text = "1---------";
            // 
            // txtDelay
            // 
            txtDelay.Anchor = AnchorStyles.Right;
            txtDelay.Location = new Point(123, 179);
            txtDelay.Name = "txtDelay";
            txtDelay.Size = new Size(81, 27);
            txtDelay.TabIndex = 16;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label8.Location = new Point(44, 389);
            label8.Name = "label8";
            label8.Size = new Size(60, 20);
            label8.TabIndex = 15;
            label8.Text = "5-------";
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Location = new Point(111, 153);
            label9.Name = "label9";
            label9.Size = new Size(104, 20);
            label9.TabIndex = 17;
            label9.Text = "Час затримки";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label7.Location = new Point(44, 330);
            label7.Name = "label7";
            label7.Size = new Size(84, 20);
            label7.TabIndex = 14;
            label7.Text = "4-----------";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label6.Location = new Point(44, 271);
            label6.Name = "label6";
            label6.Size = new Size(66, 20);
            label6.TabIndex = 13;
            label6.Text = "3--------";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.Location = new Point(44, 212);
            label5.Name = "label5";
            label5.Size = new Size(48, 20);
            label5.TabIndex = 12;
            label5.Text = "2-----";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(99, 389);
            label4.Name = "label4";
            label4.Size = new Size(128, 20);
            label4.TabIndex = 11;
            label4.Text = "Масив цінностей";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(123, 330);
            label3.Name = "label3";
            label3.Size = new Size(80, 20);
            label3.TabIndex = 10;
            label3.Text = "Масив ваг";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(108, 271);
            label2.Name = "label2";
            label2.Size = new Size(111, 20);
            label2.TabIndex = 9;
            label2.Text = "Макс. місткість";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(91, 212);
            label1.Name = "label1";
            label1.Size = new Size(145, 20);
            label1.TabIndex = 8;
            label1.Text = "Кількість предметів";
            // 
            // lstSelectedItems
            // 
            lstSelectedItems.Anchor = AnchorStyles.Right;
            lstSelectedItems.FormattingEnabled = true;
            lstSelectedItems.Location = new Point(20, 480);
            lstSelectedItems.Name = "lstSelectedItems";
            lstSelectedItems.Size = new Size(256, 104);
            lstSelectedItems.TabIndex = 7;
            // 
            // lblMaxValue
            // 
            lblMaxValue.Anchor = AnchorStyles.Right;
            lblMaxValue.AutoSize = true;
            lblMaxValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMaxValue.Location = new Point(54, 31);
            lblMaxValue.Name = "lblMaxValue";
            lblMaxValue.Size = new Size(188, 20);
            lblMaxValue.TabIndex = 6;
            lblMaxValue.Text = "Максимальна цінність: 0";
            // 
            // btnCalculate
            // 
            btnCalculate.Anchor = AnchorStyles.Right;
            btnCalculate.AutoSize = true;
            btnCalculate.Location = new Point(96, 592);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(105, 30);
            btnCalculate.TabIndex = 5;
            btnCalculate.Text = "Розрахувати";
            btnCalculate.UseVisualStyleBackColor = true;
            btnCalculate.Click += btnCalculate_Click;
            // 
            // dgvDP
            // 
            dgvDP.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDP.Dock = DockStyle.Fill;
            dgvDP.Location = new Point(0, 0);
            dgvDP.Name = "dgvDP";
            dgvDP.RowHeadersWidth = 51;
            dgvDP.Size = new Size(686, 653);
            dgvDP.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 653);
            Controls.Add(dgvDP);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDP).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtN;
        private TextBox txtW;
        private TextBox txtWeights;
        private TextBox txtValues;
        private Panel panel1;
        private ListBox lstSelectedItems;
        private Label lblMaxValue;
        private Button btnCalculate;
        private DataGridView dgvDP;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private TextBox txtDelay;
        private Label label9;
        private Label label10;
        private Label lblExecutionTime;
        private ComboBox cmbMethod;
        private Label lblSteps;
    }
}
