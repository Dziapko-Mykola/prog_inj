using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace backpackapp
{
    public partial class Form1 : Form
    {
        // counters for visualization
        private int bruteForceSteps = 0;
        private int recursiveSteps = 0;
        private int greedySteps = 0;
        private int branchSteps = 0;
        private int prunedBranches = 0;

        public Form1()
        {
            InitializeComponent();

            // set methods to combobox
            cmbMethod.Items.Add("1. Повний перебір");
            cmbMethod.Items.Add("2. Рекурсивний метод");
            cmbMethod.Items.Add("3. Жадібний алгоритм");
            cmbMethod.Items.Add("4. Динамічне програмування");
            cmbMethod.Items.Add("5. Метод гілок і меж");
            cmbMethod.SelectedIndex = 3; // dynamic programming by default
        }

        private async void btnCalculate_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out int N, out int W, out int delay, out int[] weights, out int[] values))
                return;

            try
            {
                ClearInterface();
                btnCalculate.Enabled = false;

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                List<int> selectedItems = new List<int>();
                int maxValue = 0;

                switch (cmbMethod.SelectedIndex)
                {
                    case 0:
                        (maxValue, selectedItems) = SolveBruteForce(weights, values, W);
                        break;

                    case 1:
                        (maxValue, selectedItems) = SolveRecursive(weights, values, W);
                        break;

                    case 2:
                        (maxValue, selectedItems) = SolveGreedy(weights, values, W);
                        break;

                    case 3:
                        (maxValue, selectedItems) = await SolveDynamicProgramming(weights, values, W, delay);
                        break;

                    case 4:
                        (maxValue, selectedItems) = SolveBranchAndBound(weights, values, W);
                        break;
                }

                stopwatch.Stop();

                lblMaxValue.Text = $"Максимальна цінність: {maxValue}";
                lblExecutionTime.Text = $"Час: {stopwatch.ElapsedMilliseconds} мс";

                selectedItems.Reverse();

                foreach (int item in selectedItems)
                {
                    lstSelectedItems.Items.Add(
                        $"Предмет {item + 1} (Вага: {weights[item]}, Цінність: {values[item]})");
                }

                btnCalculate.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася помилка: " + ex.Message, "Помилка");
                btnCalculate.Enabled = true;
            }
        }

        // validate input
        private bool ValidateInputs(out int N, out int W, out int delay, out int[] weights, out int[] values)
        {
            N = 0;
            W = 0;
            delay = 0;
            weights = null;
            values = null;

            // check if every parameter is set
            if (string.IsNullOrWhiteSpace(txtN.Text) ||
                string.IsNullOrWhiteSpace(txtW.Text) ||
                string.IsNullOrWhiteSpace(txtWeights.Text) ||
                string.IsNullOrWhiteSpace(txtValues.Text) ||
                string.IsNullOrWhiteSpace(txtDelay.Text))
            {
                MessageBox.Show("Усі поля повинні бути заповнені.", "Помилка");
                return false;
            }

            // if number is negative
            if (!int.TryParse(txtN.Text, out N) || N <= 0)
            {
                MessageBox.Show("Некоректна кількість предметів.", "Помилка");
                return false;
            }

            if (!int.TryParse(txtW.Text, out W) || W <= 0)
            {
                MessageBox.Show("Некоректна місткість рюкзака.", "Помилка");
                return false;
            }

            if (!int.TryParse(txtDelay.Text, out delay) || delay < 0)
            {
                MessageBox.Show("Некоректна затримка.", "Помилка");
                return false;
            }

            // validate input
            try
            {
                weights = txtWeights.Text.Split(',').Select(x => int.Parse(x.Trim())).ToArray();
                values = txtValues.Text.Split(',').Select(x => int.Parse(x.Trim())).ToArray();
            }
            catch
            {
                MessageBox.Show("Вводьте ваги та цінності через кому.", "Помилка");
                return false;
            }

            // check if number of weights and worths are the same
            if (weights.Length != N || values.Length != N)
            {
                MessageBox.Show("Кількість ваг/цінностей не відповідає N.", "Помилка");
                return false;
            }

            // if numbers are negative
            if (weights.Any(x => x <= 0) || values.Any(x => x < 0))
            {
                MessageBox.Show("Некоректні значення ваг або цінностей.", "Помилка");
                return false;
            }

            return true;
        }

        // clearing the interface
        private void ClearInterface()
        {
            dgvDP.Columns.Clear();
            dgvDP.Rows.Clear();
            lstSelectedItems.Items.Clear();
            lblMaxValue.Text = "Максимальна цінність: 0";
            lblExecutionTime.Text = "Час: 0 мс";

            lblSteps.Text = "Додаткова інформація";

            bruteForceSteps = 0;
            recursiveSteps = 0;
            greedySteps = 0;
            branchSteps = 0;
            prunedBranches = 0;
        }

        // метод 1 "метод перебору"
        private (int, List<int>) SolveBruteForce(int[] weights, int[] values, int capacity)
        {
            int n = weights.Length;
            int bestValue = 0;
            List<int> bestItems = new List<int>();

            for (int mask = 0; mask < (1 << n); mask++)
            {
                bruteForceSteps++;

                int totalWeight = 0;
                int totalValue = 0;
                List<int> currentItems = new List<int>();

                for (int i = 0; i < n; i++)
                {
                    if ((mask & (1 << i)) != 0)
                    {
                        totalWeight += weights[i];
                        totalValue += values[i];
                        currentItems.Add(i);
                    }
                }

                if (totalWeight <= capacity && totalValue > bestValue)
                {
                    bestValue = totalValue;
                    bestItems = new List<int>(currentItems);
                }
            }

            lblSteps.Text = $"Перевірено комбінацій - {bruteForceSteps}";

            return (bestValue, bestItems);
        }

        // метод 2 "рекусрія"
        private (int, List<int>) SolveRecursive(int[] weights, int[] values, int capacity)
        {
            List<int> selected = new List<int>();

            int maxValue = RecursiveKnapsack(
                weights,
                values,
                weights.Length,
                capacity,
                selected,
                out List<int> result);

            lblSteps.Text = $"Рекурсивних викликів - {recursiveSteps}";

            return (maxValue, result);
        }

        private int RecursiveKnapsack(int[] weights, int[] values, int n, int capacity, List<int> current, out List<int> best)
        {
            recursiveSteps++;

            if (n == 0 || capacity == 0)
            {
                best = new List<int>(current);
                return 0;
            }

            if (weights[n - 1] > capacity)
                return RecursiveKnapsack(weights, values, n - 1, capacity, current, out best);

            int exclude = RecursiveKnapsack(weights, values, n - 1, capacity, current, out List<int> excludeList);

            current.Add(n - 1);
            int include = values[n - 1] + RecursiveKnapsack(
                weights,
                values,
                n - 1,
                capacity - weights[n - 1],
                current,
                out List<int> includeList);

            current.Remove(n - 1);

            if (include > exclude)
            {
                best = includeList;
                return include;
            }

            best = excludeList;
            return exclude;
        }

        // метод 3 "жадібний алгоритм"
        private (int, List<int>) SolveGreedy(int[] weights, int[] values, int capacity)
        {
            var items = weights.Select((w, i) => new
            {
                Index = i,
                Weight = w,
                Value = values[i],
                Ratio = (double)values[i] / w
            })
            .OrderByDescending(x => x.Ratio);

            int totalValue = 0;
            List<int> selected = new List<int>();

            foreach (var item in items)
            {
                greedySteps++;

                if (item.Weight <= capacity)
                {
                    capacity -= item.Weight;
                    totalValue += item.Value;
                    selected.Add(item.Index);
                }
            }

            lblSteps.Text = $"Перевірено предметів - {greedySteps}";

            return (totalValue, selected);
        }

        // метод 4 "динамічне програмування"
        private async Task<(int, List<int>)> SolveDynamicProgramming(int[] weights, int[] values, int W, int delay)
        {
            int N = weights.Length;
            int[,] DP = new int[N + 1, W + 1];

            dgvDP.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

            for (int j = 0; j <= W; j++)
            {
                dgvDP.Columns.Add($"col{j}", $"{j}");
                dgvDP.Columns[j].Width = 35;
            }

            dgvDP.Rows.Add(N + 1);

            for (int i = 0; i <= N; i++)
                dgvDP.Rows[i].HeaderCell.Value = $"i={i}";

            for (int i = 1; i <= N; i++)
            {
                for (int j = 1; j <= W; j++)
                {
                    if (weights[i - 1] <= j)
                        DP[i, j] = Math.Max(DP[i - 1, j], values[i - 1] + DP[i - 1, j - weights[i - 1]]);
                    else
                        DP[i, j] = DP[i - 1, j];

                    dgvDP.Rows[i].Cells[j].Value = DP[i, j];
                    dgvDP.Rows[i].Cells[j].Style.BackColor = Color.LightCyan;

                    if (delay > 0)
                        await Task.Delay(delay);

                    dgvDP.Rows[i].Cells[j].Style.BackColor = Color.White;
                }
            }

            List<int> selected = new List<int>();
            int currW = W;

            for (int i = N; i > 0 && currW > 0; i--)
            {
                if (DP[i, currW] != DP[i - 1, currW])
                {
                    selected.Add(i - 1);
                    dgvDP.Rows[i].Cells[currW].Style.BackColor = Color.LightGreen;
                    currW -= weights[i - 1];
                }
            }

            return (DP[N, W], selected);
        }

        // метод 5 "метод гілок"
        private (int, List<int>) SolveBranchAndBound(int[] weights, int[] values, int capacity)
        {
            int n = weights.Length;

            var items = weights.Select((w, i) => new
            {
                OriginalIndex = i,
                Weight = w,
                Value = values[i],
                Ratio = (double)values[i] / w
            })
            .OrderByDescending(x => x.Ratio)
            .ToArray();

            int bestValue = 0;
            List<int> bestItems = new List<int>();

            void Branch(int level, int currentWeight, int currentValue, List<int> currentItems)
            {
                branchSteps++;

                if (currentWeight > capacity)
                    return;

                if (level == n)
                {
                    if (currentValue > bestValue)
                    {
                        bestValue = currentValue;
                        bestItems = new List<int>(currentItems);
                    }
                    return;
                }

                // upper bound
                double bound = currentValue;
                int remainingCapacity = capacity - currentWeight;

                for (int i = level; i < n; i++)
                {
                    if (items[i].Weight <= remainingCapacity)
                    {
                        remainingCapacity -= items[i].Weight;
                        bound += items[i].Value;
                    }
                    else
                    {
                        bound += items[i].Ratio * remainingCapacity;
                        break;
                    }
                }

                // prune
                if (bound <= bestValue)
                {
                    prunedBranches++;
                    return;
                }

                // include item
                currentItems.Add(items[level].OriginalIndex);

                Branch(
                    level + 1,
                    currentWeight + items[level].Weight,
                    currentValue + items[level].Value,
                    currentItems);

                currentItems.RemoveAt(currentItems.Count - 1);

                // exclude item
                Branch(level + 1, currentWeight, currentValue, currentItems);
            }

            Branch(0, 0, 0, new List<int>());

            lblSteps.Text =
                $"Вузлів - {branchSteps}, Відсічено гілок - {prunedBranches}";

            return (bestValue, bestItems);
        }
    }
}