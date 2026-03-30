using System.Drawing.Drawing2D;

namespace trajectory2
{
    public partial class Form1 : Form
    {
        private Color trajectoryColor = Color.Blue;

        public Form1()
        {
            InitializeComponent();
            // колір кнопки
            btnSelectColor.BackColor = trajectoryColor;

            // дані за замовчуванням
            txtV0.Text = "40";
            txtAngle.Text = "45";
            txtX0.Text = "0";
            txtY0.Text = "0";
            txtDt.Text = "0,1";
        }

        // подія для кнопки вибору кольору
        private void btnSelectColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                trajectoryColor = colorDialog1.Color;
                // оновлюємо колір самої кнопки
                btnSelectColor.BackColor = trajectoryColor;
            }
        }

        // асинхронний метод обробки події кнопки побудови траєкторії
        private async void btnDraw_Click(object sender, EventArgs e)
        {
            try
            {
                // зчитування даних
                double v0 = double.Parse(txtV0.Text);
                double thetaDeg = double.Parse(txtAngle.Text);
                double x0 = double.Parse(txtX0.Text);
                double y0 = double.Parse(txtY0.Text);
                double dt = double.Parse(txtDt.Text);
                double g = 9.81;

                // математична ініціалізація
                double thetaRad = thetaDeg * Math.PI / 180.0;
                double v0x = v0 * Math.Cos(thetaRad);
                double v0y = v0 * Math.Sin(thetaRad);

                double t = 0;
                double x = x0;
                double y = y0;
                double maxY = y0; // для пошуку максимальної висоти

                // об'єкт графіки
                Graphics gph = pbGraph.CreateGraphics();
                gph.SmoothingMode = SmoothingMode.AntiAlias;

                // об'єкт лінії лінії
                Pen trajPen = new Pen(trajectoryColor, 2);
                // об'єкт крапки
                Brush dotBrush = new SolidBrush(trajectoryColor);
                // лінії до точок траєкторії
                Pen dashPen = new Pen(Color.LightGray, 1) { DashStyle = DashStyle.Dash };

                float scale = 5.0f; // 1 метр = 5 пікселів
                int offsetX = 50;
                int offsetY = pbGraph.Height - 50;

                // оформлення осей координат
                gph.DrawLine(Pens.Black, offsetX - 10, offsetY, pbGraph.Width - 10, offsetY);
                gph.DrawLine(Pens.Black, offsetX, 10, offsetX, offsetY + 10);
                                                                             
                Font labelFont = new Font("Arial", 10, FontStyle.Bold);
                gph.DrawString("Y (м)", labelFont, Brushes.Black, offsetX - 35, 10);
                gph.DrawString("X (м)", labelFont, Brushes.Black, pbGraph.Width - 45, offsetY + 5);

                float prevScreenX = (float)(offsetX + x0 * scale);
                float prevScreenY = (float)(offsetY - y0 * scale);

                btnDraw.Enabled = false;

                while (y >= 0)
                {
                    t += dt;

                    // фіз. коорд. попереднього кроку
                    double lastPhysX = x;
                    double lastPhysY = y;

                    // нові фіз. коорд.
                    x = x0 + v0x * t;
                    y = y0 + v0y * t - (g * t * t) / 2.0;

                    if (y > maxY) maxY = y;

                    if (y < 0)
                    {
                        double ratio = lastPhysY / (lastPhysY - y);
                        x = lastPhysX + (x - lastPhysX) * ratio;
                        y = 0;
                    }

                    // трансформація координат в екранні
                    float screenX = (float)(offsetX + x * scale);
                    float screenY = (float)(offsetY - y * scale);

                    // малювання точок та ліній траєкторії
                    gph.DrawLine(trajPen, prevScreenX, prevScreenY, screenX, screenY);
                    gph.FillEllipse(dotBrush, screenX - 3, screenY - 3, 6, 6);

                    // лінії до точок
                    gph.DrawLine(dashPen, screenX, screenY, screenX, offsetY); 
                    gph.DrawLine(dashPen, screenX, screenY, offsetX, screenY);

                    // оновлення координат
                    prevScreenX = screenX;
                    prevScreenY = screenY;

                    if (y == 0) break;

                    // анімація
                    await Task.Delay((int)(dt * 1000));

                    // видід з циклу
                    if (y == 0) break;
                }

                // макс. висота та дист.
                lblDistance.Text = $"Дальність: {Math.Round(x, 2)} м";
                lblMaxHeight.Text = $"Max висота: {Math.Round(maxY, 2)} м";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка введення даних: " + ex.Message);
            }
            finally
            {
                btnDraw.Enabled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            pbGraph.Refresh(); // очищає малюнок
            lblDistance.Text = "Дальність польоту: ";
            lblMaxHeight.Text = "Максимальна висота: ";
        }
    }
}