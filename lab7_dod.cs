using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab7_dod
{
    // Перелічуваний тип для позначення виду депозиту
    public enum DepositType
    {
        Fixed,
        Saving,
        Premium
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /* Програма демонструє принципи ООП:
        * - є базовий клас Deposit;
        * - від нього успадковані класи FixedDeposit та SavingDeposit;
        * - клас PremiumDeposit є нащадком SavingDeposit;
        * - у кожному класі перевизначено метод CalculateInterest(), що демонструє поліморфізм.
        */

        public static string newline = "\r\n"; // Символ переходу на новий рядок
        public static string resultText = "";   // Рядок для збереження результатів
        public static string planText = "";     // Рядок для плану роботи
        public static int j = 1;                // Лічильник кроків

        // ======= БАЗОВИЙ КЛАС =======
        public class Deposit
        {
            protected double principal; // Основна сума вкладу
            protected double rate;      // Відсоткова ставка
            protected int years;        // Кількість років

            // Конструктор для ініціалізації полів
            public Deposit(double principal, double rate, int years)
            {
                this.principal = principal;
                this.rate = rate;
                this.years = years;
            }

            // Віртуальний метод для розрахунку простих відсотків
            public virtual double CalculateInterest()
            {
                Form1.resultText += $"{Form1.j}. Виконано метод CalculateInterest() класу Deposit{Form1.newline}";
                Form1.j++;
                return principal * (rate / 100) * years;
            }

            // Метод для виводу основної інформації про вклад
            public virtual string GetInfo()
            {
                return $"Основна сума: {principal}, ставка: {rate}%, термін: {years} р.";
            }
        }

        // ======= НАЩАДОК 1 =======
        public class FixedDeposit : Deposit
        {
            // Конструктор передає параметри до базового класу
            public FixedDeposit(double principal, double rate, int years)
                : base(principal, rate, years) { }

            // Перевизначення методу — використовується формула складних відсотків 
            public override double CalculateInterest()
            {
                Form1.resultText += $"{Form1.j}. Виконано метод CalculateInterest() класу FixedDeposit (override){Form1.newline}";
                Form1.j++;
                return principal * Math.Pow(1 + rate / 100, years) - principal;
            }
        }

        // ======= НАЩАДОК 2 =======
        public class SavingDeposit : Deposit
        {
            // Конструктор передає параметри базовому класу
            public SavingDeposit(double principal, double rate, int years)
                : base(principal, rate, years) { }

            // Перевизначення методу — відсотки з щомісячною капіталізацією
            public override double CalculateInterest()
            {
                Form1.resultText += $"{Form1.j}. Виконано метод CalculateInterest() класу SavingDeposit (override){Form1.newline}";
                Form1.j++;
                return principal * (Math.Pow(1 + (rate / 100) / 12, 12 * years)) - principal;
            }

            // Обчислення щомісячного прибутку
            public double CalculateMonthlyProfit()
            {
                Form1.resultText += $"{Form1.j}. Виконано метод CalculateMonthlyProfit() класу SavingDeposit{Form1.newline}";
                Form1.j++;
                return CalculateInterest() / (years * 12);
            }
        }

        // ======= НАЩАДОК 3 =======
        public class PremiumDeposit : SavingDeposit
        {
            // Конструктор передає параметри у базовий клас SavingDeposit
            public PremiumDeposit(double principal, double rate, int years)
                : base(principal, rate, years) { }

            // Перевизначений метод: додає бонус 10% до розрахованих відсотків
            public override double CalculateInterest()
            {
                Form1.resultText += $"{Form1.j}. Виконано метод CalculateInterest() класу PremiumDeposit (override){Form1.newline}";
                Form1.j++;
                return base.CalculateInterest() * 1.1; // +10% бонус
            }

            // Виводить інформацію про бонус
            public void BonusInfo()
            {
                Form1.resultText += $"{Form1.j}. Виконано метод BonusInfo() класу PremiumDeposit — додано бонусні 10%{Form1.newline}";
                Form1.j++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Формуємо план виконання
            planText = "План роботи:" + newline;
            resultText = "Результати виконання:" + newline;
            j = 1;

            planText += "1. Створити екземпляри класів Deposit, FixedDeposit, SavingDeposit і PremiumDeposit." + newline;
            planText += "2. Викликати методи CalculateInterest() для кожного." + newline;
            planText += "3. Продемонструвати перевизначення методів (override)." + newline;
            planText += "4. Викликати додатковий метод BonusInfo() у PremiumDeposit." + newline;

            // Створення об'єктів
            Deposit baseDep = new Deposit(10000, 5, 2);
            FixedDeposit fixDep = new FixedDeposit(10000, 7, 2);
            SavingDeposit savDep = new SavingDeposit(10000, 6, 2);
            PremiumDeposit premDep = new PremiumDeposit(10000, 6, 2);

            // Виклик перевизначених методів
            double baseInt = baseDep.CalculateInterest();
            double fixInt = fixDep.CalculateInterest();
            double savInt = savDep.CalculateInterest();
            double premInt = premDep.CalculateInterest();

            premDep.BonusInfo();
            double monthly = savDep.CalculateMonthlyProfit();

            // Виведення результатів на форму
            resultText += $"{newline}Результати розрахунків:" + newline;
            resultText += $"Звичайний депозит: {baseInt:F2} грн{newline}";
            resultText += $"Фіксований депозит: {fixInt:F2} грн{newline}";
            resultText += $"Накопичувальний депозит: {savInt:F2} грн{newline}";
            resultText += $"Преміум депозит: {premInt:F2} грн{newline}";
            resultText += $"Щомісячний прибуток (SavingDeposit): {monthly:F2} грн{newline}";

            label1.Text = planText;
            label2.Text = resultText;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
