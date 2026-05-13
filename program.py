import tkinter as tk
from tkinter import ttk
from typing import List

class NumericalEngine:
    """Клас для виконання обчислень: СЛАР, МНК та інтерполяція Лагранжа."""

    @staticmethod
    def gaussian_elimination(matrix: List[List[float]], vector: List[float]) -> List[float]:
        """Розв'язання системи лінійних рівнянь методом Гаусса з вибором головного елемента."""
        n = len(vector)
        for i in range(n):
            max_row = i
            for k in range(i + 1, n):
                if abs(matrix[k][i]) > abs(matrix[max_row][i]):
                    max_row = k
            
            matrix[i], matrix[max_row] = matrix[max_row], matrix[i]
            vector[i], vector[max_row] = vector[max_row], vector[i]

            pivot = matrix[i][i]
            if abs(pivot) < 1e-15:
                continue

            for k in range(i + 1, n):
                c = -matrix[k][i] / pivot
                for j in range(i, n):
                    if i == j:
                        matrix[k][j] = 0
                    else:
                        matrix[k][j] += c * matrix[i][j]
                vector[k] += c * vector[i]

        x = [0.0] * n
        for i in range(n - 1, -1, -1):
            if abs(matrix[i][i]) < 1e-15:
                continue
            x[i] = vector[i] / matrix[i][i]
            for k in range(i - 1, -1, -1):
                vector[k] -= matrix[k][i] * x[i]
        return x

class DataAnalysisApp:
    def __init__(self, root: tk.Tk):
        self.root = root
        self.root.title("Data Analysis System: Mathematical Approximation")
        self.root.geometry("1280x850")
        self.root.configure(bg="#f5f5f5")
        print("Крок 2: Надійний алгоритм Гаусса додано до математичного ядра.")

if __name__ == "__main__":
    root = tk.Tk()
    app = DataAnalysisApp(root)
    root.mainloop()