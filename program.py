import tkinter as tk
from tkinter import ttk, messagebox, filedialog
import matplotlib.pyplot as plt
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg, NavigationToolbar2Tk
import numpy as np
import os
from typing import List, Tuple, Dict, Any

# =============================================================================
# МАТЕМАТИЧНЕ ЯДРО (NUMERICAL COMPUTATION ENGINE)
# =============================================================================

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

    @staticmethod
    def least_squares_method(x_data: List[float], y_data: List[float], degree: int = 2) -> List[float]:
        """Побудова поліноміальної регресії через прямокутну матрицю Вандермонда та QR-розклад (Грам-Шмідт)."""
        n = len(x_data)
        m = degree + 1
        
        A = [[xi ** j for j in range(m)] for xi in x_data]
        Q = [[0.0] * m for _ in range(n)]
        R = [[0.0] * m for _ in range(m)]
        
        for j in range(m):
            v = [A[i][j] for i in range(n)]
            for i in range(j):
                R[i][j] = sum(Q[k][i] * A[k][j] for k in range(n))
                for k in range(n):
                    v[k] -= R[i][j] * Q[k][i]
            
            norm = sum(vk ** 2 for vk in v) ** 0.5
            R[j][j] = norm
            
            if norm > 1e-15:
                for k in range(n):
                    Q[k][j] = v[k] / norm
                    
        qty = [0.0] * m
        for i in range(m):
            qty[i] = sum(Q[k][i] * y_data[k] for k in range(n))
            
        coeffs = [0.0] * m
        for i in range(m - 1, -1, -1):
            if abs(R[i][i]) < 1e-15:
                continue
            coeffs[i] = qty[i]
            for k in range(i + 1, m):
                coeffs[i] -= R[i][k] * coeffs[k]
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
                if i != j:
                    li *= (target_x - x_pts[j]) / (x_pts[i] - x_pts[j])
            result += y_pts[i] * li
        return result

# =============================================================================
# МОДУЛЬ ВІЗУАЛІЗАЦІЇ ТА GUI
# =============================================================================

class DataAnalysisApp:
    def __init__(self, root: tk.Tk):
        self.root = root
        self.root.title("Data Analysis System: Mathematical Approximation")
        self.root.geometry("1280x850")
        self.root.configure(bg="#f5f5f5")

        self.datasets = {
            "Набір даних A (5 точок)": [
                (-3, -2.1), (-1, -0.8), (1, 0.9), (3, 2.2), (5, 3.1)
            ],
            "Набір даних B (8 точок)": [
                (-4, -4.0), (-3, -3.1), (-2, -2.2), (-1, -1.3), 
                (0, 0.1), (1, 1.2), (2, 2.3), (3, 3.4)
            ],
            "Набір даних C (20 точок)": [
                (0, 0.0), (0.1, 0.1), (0.2, 0.19), (0.3, 0.28), (0.4, 0.36),
                (0.5, 0.44), (0.6, 0.51), (0.7, 0.57), (0.8, 0.63), (0.9, 0.67),
                (1.0, 0.71), (1.1, 0.74), (1.2, 0.76), (1.3, 0.77), (1.4, 0.78),
                (1.5, 0.78), (1.6, 0.77), (1.7, 0.75), (1.8, 0.73), (1.9, 0.69)
            ]
        }

        self.current_data_name = tk.StringVar(value="Набір даних A (5 точок)")
        self._anim_id = None
        self._is_busy = False

        self._init_styles()
        self._build_interface()
        self.refresh_display()

    def _init_styles(self):
        style = ttk.Style()
        style.theme_use('clam')
        style.configure("Main.TFrame", background="#ffffff")
        style.configure("Header.TLabel", font=("Segoe UI", 14, "bold"), background="#ffffff")
        style.configure("Log.TLabel", font=("Consolas", 10), background="#f8f9fa")
        style.configure("Control.TButton", font=("Segoe UI", 10), padding=5)

    def _build_interface(self):
        self.sidebar = ttk.Frame(self.root, padding="20", style="Main.TFrame")
        self.sidebar.pack(side=tk.LEFT, fill=tk.Y, padx=1, pady=1)

        ttk.Label(self.sidebar, text="ВИБІР ДАНИХ", style="Header.TLabel").pack(anchor="w", pady=(0, 15))
        
        self.combo = ttk.Combobox(self.sidebar, textvariable=self.current_data_name, 
                                 values=list(self.datasets.keys()), state="readonly", width=25)
        self.combo.pack(fill=tk.X, pady=(0, 20))
        self.combo.bind("<<ComboboxSelected>>", lambda e: self.refresh_display())

        ttk.Separator(self.sidebar, orient="horizontal").pack(fill=tk.X, pady=10)

        ttk.Label(self.sidebar, text="ІНСТРУМЕНТИ", style="Header.TLabel").pack(anchor="w", pady=(10, 15))
        
        ttk.Button(self.sidebar, text="Запустити симуляцію", command=self.start_animation, style="Control.TButton").pack(fill=tk.X, pady=5)
        ttk.Button(self.sidebar, text="Скинути графіки", command=self.refresh_display, style="Control.TButton").pack(fill=tk.X, pady=5)
        ttk.Button(self.sidebar, text="Зберегти графік", command=self.export_plot, style="Control.TButton").pack(fill=tk.X, pady=5)

        ttk.Label(self.sidebar, text="РЕЖИМ ГРАФІКА", style="Header.TLabel").pack(anchor="w", pady=(15, 5))
        self.view_mode = tk.StringVar(value="Усі")
        
        for mode in ["Усі", "Тільки МНК", "Тільки Інтерполяція"]:
            ttk.Radiobutton(self.sidebar, text=mode, value=mode, variable=self.view_mode, 
                            command=self.refresh_display).pack(anchor="w", pady=2)

        ttk.Label(self.sidebar, text="ЛОГ ОБЧИСЛЕНЬ", font=("Segoe UI", 10, "bold"), background="#ffffff").pack(anchor="w", pady=(20, 5))
        self.log_widget = tk.Text(self.sidebar, height=12, width=35, font=("Consolas", 9), bg="#f8f9fa", relief="flat", padx=10, pady=10)
        self.log_widget.pack(fill=tk.BOTH, expand=True)

        self.main_container = ttk.Frame(self.root, padding="10")
        self.main_container.pack(side=tk.RIGHT, fill=tk.BOTH, expand=True)

        self.fig, (self.ax_plot, self.ax_res) = plt.subplots(2, 1, figsize=(10, 8), gridspec_kw={'height_ratios': [3, 1]})
        self.fig.tight_layout(pad=5.0)
        
        self.canvas = FigureCanvasTkAgg(self.fig, master=self.main_container)
        self.canvas.get_tk_widget().pack(fill=tk.BOTH, expand=True)

        self.toolbar = NavigationToolbar2Tk(self.canvas, self.main_container, pack_toolbar=False)
        self.toolbar.update()
        self.toolbar.pack(side=tk.BOTTOM, fill=tk.X)

        self.fig.canvas.mpl_connect("motion_notify_event", self.on_hover)

    def _get_coords(self) -> Tuple[List[float], List[float]]:
        pts = self.datasets[self.current_data_name.get()]
        return [p[0] for p in pts], [p[1] for p in pts]

    def _log(self, message: str):
        self.log_widget.insert(tk.END, message + "\n")
        self.log_widget.see(tk.END)

    def refresh_display(self):
        """Статичне оновлення графіків з коректним фокусуванням масштабу осі Y."""
        if self._anim_id:
            self.root.after_cancel(self._anim_id)
        
        self._is_busy = False
        self.ax_plot.clear()
        self.ax_res.clear()
        self.log_widget.delete(1.0, tk.END)

        x_pts, y_pts = self._get_coords()
        coeffs = NumericalEngine.least_squares_method(x_pts, y_pts, 2)
        xr = np.linspace(min(x_pts)-1, max(x_pts)+1, 300)
        
        self.scatter_pts = self.ax_plot.scatter(x_pts, y_pts, color='#2c3e50', s=50, label='Вузли (Nodes)', zorder=5)
        
        self.hover_annotation = self.ax_plot.annotate(
            "", xy=(0,0), xytext=(10,10), textcoords="offset points",
            bbox=dict(boxstyle="round,pad=0.5", fc="#ffffff", ec="#2c3e50", alpha=0.95, lw=1),
            arrowprops=dict(arrowstyle="->", color="#2c3e50", lw=1)
        )
        self.hover_annotation.set_visible(False)

        if self.view_mode.get() in ["Усі", "Тільки МНК"]:
            y_lsm = [coeffs[0]*val**2 + coeffs[1]*val + coeffs[2] for val in xr]
            self.ax_plot.plot(xr, y_lsm, color='#e74c3c', lw=2.5, label='МНК (Поліном 2-го ст.)')
        
        if self.view_mode.get() in ["Усі", "Тільки Інтерполяція"]:
            y_lag = [NumericalEngine.lagrange_polynomial(x_pts, y_pts, val) for val in xr]
            self.ax_plot.plot(xr, y_lag, color='#3498db', linestyle='--', alpha=0.8, label='Інтерполяція Лагранжа')

        # ФІКС МАСШТАБУВАННЯ: Запобігаємо сплющенню точок через ефект Рунге
        y_min, y_max = min(y_pts), max(y_pts)
        y_pad = (y_max - y_min) * 0.15 if y_max != y_min else 1.0
        self.ax_plot.set_ylim(y_min - y_pad, y_max + y_pad)

        residuals = [yi - (coeffs[0]*xi**2 + coeffs[1]*xi + coeffs[2]) for xi, yi in zip(x_pts, y_pts)]
        self.ax_res.bar(x_pts, residuals, color='#27ae60', width=0.08, alpha=0.6, label='Відхилення (Залишки)')
        self.ax_res.axhline(0, color='black', lw=1.2, linestyle='-')
        
        self._update_metadata(coeffs, residuals)
        self._apply_decorations()
        self.canvas.draw_idle()

    def _apply_decorations(self):
        self.ax_plot.set_title("Результати апроксимації та інтерполяції", fontsize=12, fontweight='bold')
        self.ax_plot.grid(True, linestyle=':', alpha=0.6)
        self.ax_plot.legend(loc='best')
        self.ax_res.set_title("Аналіз нев’язок (Residuals)", fontsize=10, fontweight='bold')
        self.ax_res.grid(True, axis='y', linestyle=':', alpha=0.5)

        self.ax_plot.set_xlabel("Значення X (Вузли)", fontsize=9)
        self.ax_plot.set_ylabel("Значення Y", fontsize=9)
        self.ax_res.set_xlabel("Значення X", fontsize=9)
        self.ax_res.set_ylabel("Похибка (r)", fontsize=9)

    def _update_metadata(self, c, res):
        mse = sum(r**2 for r in res) / len(res)
        self._log("--- ТЕХНІЧНИЙ ЗВІТ ---")
        self._log(f"Рівняння регресії:\ny = {c[0]:.4f}x² + {c[1]:.4f}x + {c[2]:.4f}")
        self._log(f"\nСередньоквадр. помилка (MSE):\n{mse:.8f}")
        self._log(f"\nКількість вузлів: {len(res)}")
        self._log("\nСтатус: Готово")

    def on_hover(self, event):
        """Інтерактивні підказки працють ідеально за реальними координатами точок."""
        if hasattr(self, 'scatter_pts') and event.inaxes == self.ax_plot and not self._is_busy:
            cont, ind = self.scatter_pts.contains(event)
            if cont:
                idx = ind["ind"][0]
                x_pts, y_pts = self._get_coords()
                xi, yi = x_pts[idx], y_pts[idx]
                
                coeffs = NumericalEngine.least_squares_method(x_pts, y_pts, 2)
                y_pred = coeffs[0]*xi**2 + coeffs[1]*xi + coeffs[2]
                error = yi - y_pred
                
                self.hover_annotation.xy = (xi, yi)
                text = f"Вузол [{idx + 1}]\nX: {xi:.2f}\nY: {yi:.2f}\nПохибка МНК: {error:.4f}"
                self.hover_annotation.set_text(text)
                self.hover_annotation.set_visible(True)
                self.canvas.draw_idle()
                return
                
        if hasattr(self, 'hover_annotation') and self.hover_annotation.get_visible():
            self.hover_annotation.set_visible(False)
            self.canvas.draw_idle()

    # =========================================================================
    # ЛОГІКА АНІМАЦІЇ (З ФІКСОМ МАСШТАБУ НА КОЖНОМУ КАДРІ)
    # =========================================================================

    def start_animation(self):
        if self._is_busy: return
        self._is_busy = True
        self.ax_plot.clear()
        self.ax_res.clear()
        self.log_widget.delete(1.0, tk.END)
        self._log("Запуск візуальної симуляції за кроками...")
        
        x_all, y_all = self._get_coords()
        self.animation_loop(0, x_all, y_all)

    def animation_loop(self, step: int, x: List[float], y: List[float]):
        n = len(x)
        y_min, y_max = min(y), max(y)
        y_pad = (y_max - y_min) * 0.15 if y_max != y_min else 1.0
        
        # Фаза 1: Покрокова інтерполяція Лагранжа
        if step <= n:
            self.ax_plot.clear()
            self.ax_plot.grid(True, linestyle=':', alpha=0.5)
            self.ax_plot.scatter(x, y, color='gray', alpha=0.2, label='Майбутні вузли')
            
            curr_x, curr_y = x[:step+1], y[:step+1]
            self.ax_plot.scatter(curr_x[:-1], curr_y[:-1], color='#3498db', s=50, edgecolors='black')
            if len(curr_x) > 0:
                self.ax_plot.scatter(curr_x[-1], curr_y[-1], color='#e67e22', s=100, edgecolors='black', label='Активний вузол')
            
            if step > 0:
                xr = np.linspace(min(x)-1, max(x)+1, 200)
                yr = [NumericalEngine.lagrange_polynomial(curr_x, curr_y, val) for val in xr]
                self.ax_plot.plot(xr, yr, color='#3498db', lw=1.8, label=f'Інтерполяція ({step+1} вузлів)')
            
            self.ax_plot.set_title(f"Фаза 1: Динамічне додавання вузлів інтерполяції ({step+1}/{n})")
            self.ax_plot.legend(loc='best')
            self.ax_plot.set_ylim(y_min - y_pad, y_max + y_pad)  # Утримуємо фокус
            self.canvas.draw_idle()
            self._anim_id = self.root.after(350, lambda: self.animation_loop(step + 1, x, y))

        # Фаза 2: Плавна підгонка МНК
        elif step <= n + 15:
            progress = (step - n) / 15.0
            coeffs = NumericalEngine.least_squares_method(x, y, 2)
            xr = np.linspace(min(x)-1, max(x)+1, 200)
            
            y_target = [coeffs[0]*v**2 + coeffs[1]*v + coeffs[2] for v in xr]
            y_mean = [sum(y)/n for _ in xr]
            y_morph = [yt * progress + ym * (1 - progress) for yt, ym in zip(y_target, y_mean)]
            
            self.ax_plot.clear()
            self.ax_plot.grid(True, linestyle=':', alpha=0.5)
            self.ax_plot.scatter(x, y, color='#2c3e50', s=50, zorder=5, label='Експериментальні точки')
            self.ax_plot.plot(xr, y_morph, color='#e74c3c', lw=2.5, label='МНК (Адаптація тренду)')
            
            for xi, yi in zip(x, y):
                y_curr_pred = (coeffs[0]*xi**2 + coeffs[1]*xi + coeffs[2]) * progress + (sum(y)/n) * (1 - progress)
                self.ax_plot.plot([xi, xi], [y_curr_pred, yi], color='#27ae60', linestyle=':', lw=1.5)
            
            self.ax_plot.set_title(f"Фаза 2: Оптимізація МНК та мінімізація відхилень ({int(progress*100)}%)")
            self.ax_plot.legend(loc='best')
            self.ax_plot.set_ylim(y_min - y_pad, y_max + y_pad)  # Утримуємо фокус
            self.canvas.draw_idle()
            self._anim_id = self.root.after(80, lambda: self.animation_loop(step + 1, x, y))
        
        else:
            self.refresh_display()
            messagebox.showinfo("ок", "Ок.")

    def export_plot(self):
        path = filedialog.asksaveasfilename(defaultextension=".png", 
                                          filetypes=[("PNG Image", "*.png"), ("PDF File", "*.pdf")])
        if path:
            self.fig.savefig(path)
            self._log(f"\nФайл збережено:\n{os.path.basename(path)}")

if __name__ == "__main__":
    try:
        from ctypes import windll
        windll.shcore.SetProcessDpiAwareness(1)
    except Exception:
        pass

    root = tk.Tk()
    app = DataAnalysisApp(root)
    root.mainloop()