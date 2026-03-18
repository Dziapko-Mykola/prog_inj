using System.Drawing.Drawing2D;

namespace trajectory
{
    public partial class Form1 : Form
    {
        private Color trajectoryColor = Color.Blue;

        public Form1()
        {
            InitializeComponent();
            txtV0.Text = "20";
            txtA.Text = "10";
            txtAngle.Text = "45";
            txtDT.Text = "0,2";
            txtTMax.Text = "3";
            txtX0.Text = "0";
            txtY0.Text = "0";
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            try
            {
                // get data
                double physX0 = double.Parse(txtX0.Text);
                double physY0 = double.Parse(txtY0.Text);
                double v0 = double.Parse(txtV0.Text);
                double a = double.Parse(txtA.Text);
                double angleDeg = double.Parse(txtAngle.Text);
                double dt = double.Parse(txtDT.Text);
                double tMax = double.Parse(txtTMax.Text);

                // get graphics
                Graphics g = pbGraph.CreateGraphics();
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // 1 meter = 5 pixels
                float scale = 5.0f; 
        
                int axisOriginX = 50;
                int axisOriginY = pbGraph.Height - 50;

                // draw axes
                DrawAxes(g, axisOriginX, axisOriginY);

                // calculation of angle and projections
                double angleRad = angleDeg * Math.PI / 180.0;
                double v0x = v0 * Math.Cos(angleRad);
                double v0y = v0 * Math.Sin(angleRad);
                double ax = a * Math.Cos(angleRad);
                double ay = a * Math.Sin(angleRad);

                Pen trajPen = new Pen(trajectoryColor, 2);
                Pen dashPen = new Pen(Color.LightGray, 1) { DashStyle = DashStyle.Dash };

                // origin coordinates
                float prevScreenX = (float)(axisOriginX + physX0 * scale);
                float prevScreenY = (float)(axisOriginY - physY0 * scale);

                // main loop
                for (double t = 0; t <= tMax; t += dt)
                {
                    double currentPhysX = physX0 + v0x * t + (ax * t * t) / 2;
                    double currentPhysY = physY0 + v0y * t + (ay * t * t) / 2;

                    // to pixels    
                    float screenX = (float)(axisOriginX + currentPhysX * scale);
                    float screenY = (float)(axisOriginY - currentPhysY * scale);

                    // draw line from prev to current dot
                    if (t > 0)
                    {
                        g.DrawLine(trajPen, prevScreenX, prevScreenY, screenX, screenY);
                    }

                    // draw dot
                    g.FillEllipse(new SolidBrush(trajectoryColor), screenX - 3, screenY - 3, 6, 6);

                    // lines to axes
                    g.DrawLine(dashPen, screenX, screenY, screenX, axisOriginY);
                    g.DrawLine(dashPen, screenX, screenY, axisOriginX, screenY); 

                    // update prev dot
                    prevScreenX = screenX;
                    prevScreenY = screenY;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: перевірте правильність введення даних.");
            }
        }

        private void DrawAxes(Graphics g, int ox, int oy)
        {
            Pen axisPen = new Pen(Color.Black, 2);

            g.DrawLine(axisPen, ox - 10, oy, pbGraph.Width - 10, oy);
            g.DrawLine(axisPen, ox, 10, ox, oy + 10);

            g.DrawString("X (m)", new Font("Arial", 10), Brushes.Black, pbGraph.Width - 40, oy + 5);
            g.DrawString("Y (m)", new Font("Arial", 10), Brushes.Black, ox - 35, 10);
        }

        private void btnSelectColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                trajectoryColor = colorDialog1.Color;
                btnSelectColor.BackColor = trajectoryColor;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            pbGraph.Refresh();
        }
    }
}