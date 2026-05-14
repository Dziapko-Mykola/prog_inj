import tkinter as tk
from typing import List

class NumericalEngine:
    @staticmethod
    def gaussian_elimination(matrix: List[List[float]], vector: List[float]) -> List[float]:
        n = len(vector)
        for i in range(n):
            max_row = i
            for k in range(i + 1, n):
                if abs(matrix[k][i]) > abs(matrix[max_row][i]): max_row = k
            matrix[i], matrix[max_row] = matrix[max_row], matrix[i]
            vector[i], vector[max_row] = vector[max_row], vector[i]
            pivot = matrix[i][i]
            if abs(pivot) < 1e-15: continue
            for k in range(i + 1, n):
                c = -matrix[k][i] / pivot
                for j in range(i, n):
                    if i == j: matrix[k][j] = 0
                    else: matrix[k][j] += c * matrix[i][j]
                vector[k] += c * vector[i]
        x = [0.0] * n
        for i in range(n - 1, -1, -1):
            if abs(matrix[i][i]) < 1e-15: continue
            x[i] = vector[i] / matrix[i][i]
            for k in range(i - 1, -1, -1): vector[k] -= matrix[k][i] * x[i]
        return x

    @staticmethod
    def least_squares_method(x_data: List[float], y_data: List[float], degree: int = 2) -> List[float]:
        """Побудова поліноміальної регресії через матрицю Вандермонда та QR-розклад."""
        n = len(x_data)
        m = degree + 1
        A = [[xi ** j for j in range(m)] for xi in x_data]
        Q = [[0.0] * m for _ in range(n)]
        R = [[0.0] * m for _ in range(m)]
        for j in range(m):
            v = [A[i][j] for i in range(n)]
            for i in range(j):
                R[i][j] = sum(Q[k][i] * A[k][j] for k in range(n))
                for k in range(n): v[k] -= R[i][j] * Q[k][i]
            norm = sum(vk ** 2 for vk in v) ** 0.5
            R[j][j] = norm
            if norm > 1e-15:
                for k in range(n): Q[k][j] = v[k] / norm
        qty = [0.0] * m
        for i in range(m): qty[i] = sum(Q[k][i] * y_data[k] for k in range(n))
        coeffs = [0.0] * m
        for i in range(m - 1, -1, -1):
            if abs(R[i][i]) < 1e-15: continue
            coeffs[i] = qty[i]
            for k in range(i + 1, m): coeffs[i] -= R[i][k] * coeffs[k]
            coeffs[i] /= R[i][i]
        return coeffs[::-1]

    @staticmethod
    def lagrange_polynomial(x_pts: List[float], y_pts: List[float], target_x: float) -> float:
        """Обчислення значення інтерполяційного полінома Лагранжа в точці."""
        n = len(x_pts)
        result = 0.0
        for i in range(n):
            li = 1.0
            for j in range(n):
                if i != j: li *= (target_x - x_pts[j]) / (x_pts[i] - x_pts[j])
            result += y_pts[i] * li
        return result

class DataAnalysisApp:
    def __init__(self, root: tk.Tk):
        self.root = root
        self.root.title("Data Analysis System")
        self.root.geometry("1280x850")

        self.datasets = {
            "Набір даних A (5 точок)": [(-3, -2.1), (-1, -0.8), (1, 0.9), (3, 2.2), (5, 3.1)]
        }

        # Створення контейнера для графіків
        self.main_container = ttk.Frame(self.root, padding="10")
        self.main_container.pack(side=tk.RIGHT, fill=tk.BOTH, expand=True)

        # Створення двох осей (головний графік + графік залишків)
        self.fig, (self.ax_plot, self.ax_res) = plt.subplots(2, 1, figsize=(10, 8), gridspec_kw={'height_ratios': [3, 1]})
        self.fig.tight_layout(pad=5.0)
        
        self.canvas = FigureCanvasTkAgg(self.fig, master=self.main_container)
        self.canvas.get_tk_widget().pack(fill=tk.BOTH, expand=True)

        # Тестове малювання базової сітки
        self.ax_plot.grid(True, linestyle=':')
        self.ax_res.grid(True, linestyle=':')
        self.canvas.draw()


if __name__ == "__main__":
    root = tk.Tk()
    app = DataAnalysisApp(root)
    root.mainloop()