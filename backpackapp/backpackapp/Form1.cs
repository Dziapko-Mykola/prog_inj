namespace backpackapp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnCalculate_Click(object sender, EventArgs e)
        {
            // перевірка чи всі поля заповнені
            if (string.IsNullOrWhiteSpace(txtN.Text)) { MessageBox.Show("Потрібно заповнити поле 'Кількість предметів'.", "Увага"); return; }
            if (string.IsNullOrWhiteSpace(txtW.Text)) { MessageBox.Show("Потрібно заповнити поле 'Місткість рюкзака'.", "Увага"); return; }
            if (string.IsNullOrWhiteSpace(txtWeights.Text)) { MessageBox.Show("Потрібно вказати ваги предметів.", "Увага"); return; }
            if (string.IsNullOrWhiteSpace(txtValues.Text)) { MessageBox.Show("Потрібно вказати цінності предметів.", "Увага"); return; }
            if (string.IsNullOrWhiteSpace(txtDelay.Text)) { MessageBox.Show("Вкажіть час затримки (наприклад, 100).", "Увага"); return; }

            try
            {
                // ініціалізація та очищення інтерфейсу
                dgvDP.Columns.Clear();
                dgvDP.Rows.Clear();
                lstSelectedItems.Items.Clear();
                lblMaxValue.Text = "Максимальна цінність: 0";
                btnCalculate.Enabled = false;

                // зчитування параметрів та перевірка на додатність
                if (!int.TryParse(txtN.Text, out int N) || N <= 0)
                {
                    MessageBox.Show("Кількість предметів має бути цілим числом більшим за 0.", "Помилка");
                    btnCalculate.Enabled = true; return;
                }
                if (!int.TryParse(txtW.Text, out int W) || W <= 0)
                {
                    MessageBox.Show("Місткість рюкзака має бути цілим числом більшим за 0.", "Помилка");
                    btnCalculate.Enabled = true; return;
                }
                if (!int.TryParse(txtDelay.Text, out int delay) || delay < 0)
                {
                    MessageBox.Show("Час затримки не може бути від'ємним числом.", "Помилка");
                    btnCalculate.Enabled = true; return;
                }

                // перевірка на правильність вводу масиву через кому
                int[] weights;
                int[] values;
                try
                {
                    weights = txtWeights.Text.Split(',').Select(s => int.Parse(s.Trim())).ToArray();
                    values = txtValues.Text.Split(',').Select(s => int.Parse(s.Trim())).ToArray();
                }
                catch
                {
                    MessageBox.Show("Ваги та цінності мають вводитися через кому.\nПриклад: 2, 3, 4, 5", "Неправильний формат", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnCalculate.Enabled = true;
                    return;
                }

                // перевірка на від'ємні числа у масивах
                if (weights.Any(w => w <= 0))
                {
                    MessageBox.Show("Вага кожного предмета має бути цілим числом більшим за 0.", "Помилка даних");
                    btnCalculate.Enabled = true; return;
                }
                if (values.Any(v => v < 0))
                {
                    MessageBox.Show("Цінність предмета не може бути від'ємною.", "Помилка даних");
                    btnCalculate.Enabled = true; return;
                }

                // перевірка чи кількість предметів збігається з масивами
                if (weights.Length != N || values.Length != N)
                {
                    MessageBox.Show($"Ви вказали N={N}, але ввели {weights.Length} значень ваги та {values.Length} значень цінності.\nПереконайтеся, що їх кількість збігається.", "Помилка");
                    btnCalculate.Enabled = true;
                    return;
                }

                // ініціалізація матриці DP
                int[,] DP = new int[N + 1, W + 1];

                // налаштування DataGridView
                dgvDP.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
                for (int j = 0; j <= W; j++)
                {
                    dgvDP.Columns.Add($"col{j}", $"{j}");
                    dgvDP.Columns[j].Width = 35;
                }

                dgvDP.Rows.Add(N + 1);
                for (int i = 0; i <= N; i++)
                {
                    dgvDP.Rows[i].HeaderCell.Value = $"i={i}";
                }

                // заповнення 0-го рядка та стовпця
                for (int i = 0; i <= N; i++) dgvDP.Rows[i].Cells[0].Value = 0;
                for (int j = 0; j <= W; j++) dgvDP.Rows[0].Cells[j].Value = 0;

                // головні цикли
                for (int i = 1; i <= N; i++)
                {
                    for (int j = 1; j <= W; j++)
                    {
                        // логіка алгоритму
                        if (weights[i - 1] <= j)
                        {
                            DP[i, j] = Math.Max(DP[i - 1, j], values[i - 1] + DP[i - 1, j - weights[i - 1]]);
                        }
                        else
                        {
                            DP[i, j] = DP[i - 1, j];
                        }

                        // візуалізація
                        dgvDP.Rows[i].Cells[j].Value = DP[i, j];
                        dgvDP.Rows[i].Cells[j].Style.BackColor = Color.LightCyan;

                        if (delay > 0) await Task.Delay(delay);

                        dgvDP.Rows[i].Cells[j].Style.BackColor = Color.White;
                    }
                }

                // зворотний хід
                int currI = N;
                int currJ = W;
                List<int> selectedItems = new List<int>();

                while (currI > 0 && currJ > 0)
                {
                    if (DP[currI, currJ] != DP[currI - 1, currJ])
                    {
                        selectedItems.Add(currI);
                        dgvDP.Rows[currI].Cells[currJ].Style.BackColor = Color.LightGreen;
                        currJ -= weights[currI - 1];
                    }
                    currI--;
                }

                // вивід результатів
                lblMaxValue.Text = $"Максимальна цінність: {DP[N, W]}";
                selectedItems.Reverse();

                foreach (int item in selectedItems)
                {
                    lstSelectedItems.Items.Add($"Предмет {item} (Вага: {weights[item - 1]}, Цінність: {values[item - 1]})");
                }

                btnCalculate.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася непередбачена помилка: " + ex.Message, "Помилка");
                btnCalculate.Enabled = true;
            }
        }
    }
}