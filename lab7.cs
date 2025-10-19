using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab7
{
    public enum stanMjacha // це перелічуваний тип (нумератор)
    {
        vGriA, // м'яч у грі у команди А
        vGriB, // м'яч у грі у команди В
        pozaGroju, // м'яч поза грою
        vCentri, // м'яч в центрі поля
        vVorotahA, // м'яч у воротах команди А
        vVorotahB // м'яч у воротах команди В
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /* Програма ілюструє такі поняття ООП, як поліморфізм і успадкування.
        * На основі базового класу Sphere створено класи-нащадки mjach i povitrjana_kulja
        * На основі класу mjach створено клас futbol_mjach.
        * Клас Sphere має методи: kotytys, letity, udar.
        * Клас mjach має методи: popav, letity, letityBase.
        * Клас futbol_mjach має методи: popav - з сигнатурою, такою, як у класі mjach,
        * i, тому, з модифікатором override,
        * popav - з сигнатурою, відмінною від сигнатури цього методу у класі mjach, i
        * popavBase - метод, для викликання методу popav базового класу mjach,
        * а також метод letity з сигнатурою такою, як у класу Sphere,
        * і тому з модифікатором override.
        * Крім того, цей клас має конструктор з параметром.
        * Усі методи поміщають у рядок sumText повідомлення про свою роботу.
        * Цей рядок ми виводимо у текст мітки Label2 на формі.
        * У текст мітки Label1 помістимо план нашої роботи - який метод, на нашу думку, буде викликатись.
        * Потім порівняємо з результатами роботи. */

        public static string newline = "\r"; // Змінна для переходу на новий рядок у повідомленнях методів.
        public static string sumText = "";
        public static string PlanText = "";
        public static int j = 1;
        public class Sphere
        {
            public const double Pi = Math.PI;
            double r, l, v, s, m; // поля для зберігання властивостей
            public double r_kuli
            {
                get { return r; }
                set
                {
                    r = value;
                    l = 2 * Pi * r;
                    v = 4 * Pi * r * r * r;
                    s = 4 * Pi * r * r;
                }
            }
            public double l_kola
            {
                get { return l; }
            }
            public double v_kuli
            {
                get { return v; }
            }
            public double s_kuli
            {
                get { return v; }
            }
            public double masa
            {
                get { return m; }
                set { m = value; }
            }
            virtual public double kotytys(double t, double v)
            {
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j)
                + ". Виконано метод kotytys(double, double) класу Sphere"
                + Form1.newline;
                Form1.j++;
                return 2 * Pi * r * t * v;
            }
            virtual public double letity(double t, double v)
            {
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j)
                + ". Виконано метод letity(double,double) класу Sphere"
                + Form1.newline;
                Form1.j++;
                return t * v;
            }
            virtual public double udar(double t, out double v, double f, double t1)
            {
                v = f * t1 / m;
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j)
                + ". Виконано метод udar(double, out double,double,double) класу Sphera"
                + " s = v * t= "
                + Convert.ToString(t * v)
                + Form1.newline;
                Form1.j++;
                return t * v;
            }
        }
        public class mjach : Sphere // клас-нащадок від Sphere
        {
            virtual public void popav(bool je, ref int kilkist)
            {
                if (je) kilkist++;
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j)
                + ". Виконано метод popav(bool, ref int) класу mjach. kilkist="
                + Convert.ToString(kilkist)
                + Form1.newline;
                Form1.j++;
            }
            virtual public double letity(double t, double v, double f_tertja)
            {
                Form1.sumText = Form1.sumText
                + Convert.ToString(Form1.j)
                + ". Виконано метод letity(double, double, double ) класу mjach"
                + Form1.newline;
                Form1.j++;
                return t * v - f_tertja / masa * t * t / 2;
            }
            public double letityBase(double t, double v)
            {
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j)
                + ". Виконано метод letityBase(double, double) класу mjach"
                + Form1.newline;
                Form1.j++;
                return base.letity(t, v);
            }
        }
        public class povitrjana_kulja : Sphere
        {
            double tysk, maxTysk;
            public double tyskGazu
            {
                get { return tysk; }
                set { tysk = value; }
            }
            public double maxTyskGazu
            {
                get { return maxTysk; }
                set { maxTysk = value; }
            }
            public bool lopatys()
            {
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j)
                + ". Виконано метод lopatys(); класу povitrjana_kulja"
                + Form1.newline;
                Form1.j++;
                if (tysk > maxTysk) return true;
                else return false;
            }
            public double letity(double t, double v, double v_Vitru, double kutVitru)
            {
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j)
                + ". Виконано метод lletity(double t, double v, double v_Vitru, double kutVitru) класу povitrjana_kulja"
                + Form1.newline;
                Form1.j++;
                return t * v - v_Vitru * Math.Sin(kutVitru);
            }
            new public double letity(double t, double v)
            {
                double s;
                s = t * v;
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j)
                    + ". Виконано метод letity(double t, double v ) класу povitrjana_kulja. Ми пролетіли "
+ Convert.ToString(s) + " метрів!" + Form1.newline;
                Form1.j++;
                return s;
            }
        }
        public class futbol_mjach : mjach
        {
            stanMjacha stanM;
            public stanMjacha tstanMjacha
            {
                get { return stanM; }
                set { stanM = value; }
            }
            public bool standout
            {
                get { if (stanM == stanMjacha.pozaGroju) return true; else return false; }
            }
            public futbol_mjach(stanMjacha sm)
            {
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j)
                + ". Виконано конструктор futbol_mjach(stanMjacha sm) "
                + Form1.newline; Form1.j++;
                stanM = sm;
                masa = 0.5F;
                r_kuli = 0.1F;
            }
            public void popav(bool je, ref int kilkistA, ref int kilkistB)
            {
                if (je)
                {
                    if (stanM == stanMjacha.vGriA) kilkistA++;
                    else
                if (stanM == stanMjacha.vGriB) kilkistB++;
                }
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j) +
                ". Виконано метод popav класу futbol_mjach з сигнатурою (bool, ref int, ref int)" + Form1.newline; Form1.j++;
            }
            public void popavBase(bool b, ref int i)
            {
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j) +
                ". Виконано метод popavBase(bool b, ref int i) класу futbol_mjach, який викликає метод popav(b, ref i) класу mjach )" + Form1.newline;
                Form1.j++;
                base.popav(b, ref i);
            }
            override public void popav(bool je, ref int kilkist)
            {
                if (je) kilkist++;
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j) +
                ". Виконано метод popav(bool je, ref int kilkist) (override) класу futbol_mjach " + Form1.newline;
                Form1.j++;
            }
            override public double letity(double t, double v, double ftertja)
            {
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j) +
                ". Виконано метод letity(double t, double v, double ftertja) ( override) класу futbol_mjach " + Form1.newline;
                Form1.j++;
                return t * v - ftertja / masa * t * t / 2;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PlanText = "Планується зробити:" + newline;
            sumText = "Початок роботи" + newline;
            int i = 1;
            label2.Text = sumText + newline;
            PlanText = PlanText + Convert.ToString(i) + ". Плануємо створите екземпляр класу futbol_mjach, викликавши конструктор з параметром " + newline; i++;
            futbol_mjach fm = new futbol_mjach(stanMjacha.vCentri);
            if (String.IsNullOrEmpty(sumText)) { sumText = ""; }
            int GolA = 0, GolB = 0;
            fm.masa = 0.6F;
            fm.r_kuli = 0.12F;
            fm.tstanMjacha = stanMjacha.vGriA;
            PlanText = PlanText + Convert.ToString(i) + ". Плануємо викликати метод popav(true, ref GolA);, що перевизначений у класі futbol_mjach (override) " +
            newline; i++;
            fm.popav(true, ref GolA);
            PlanText = PlanText + Convert.ToString(i) +
            ". Плануємо викликати метод popav(true, ref GolA, ref GolB) перевизначений у класі futbol_mjach з сигнатурою, інакшою ніж у базовому класі "
            + newline; i++;
            fm.popav(true, ref GolA, ref GolB);
            PlanText = PlanText + Convert.ToString(i) + ", " + Convert.ToString(i + 1) +
            ". Плануємо, за посередністю методу popavBase(true, ref GolA) класу futbol_mjach викликати метод popav класу mjach " +
            newline; i++; i++;
            fm.popavBase(true, ref GolA);
            double s, v;
            PlanText = PlanText + Convert.ToString(i) + ". Плануємо викликати метод udar(2, out v, 200, 0.1)класу Sphere iз класу futbol_mjach " + newline; i++;
            s = fm.udar(2, out v, 200, 0.1);
            PlanText = PlanText + Convert.ToString(i) + ". Плануємо викликати метод kotytys(5, 1) класу Sphere iз класу futbol_mjach " + newline; i++;
            s = fm.kotytys(5, 1);
            PlanText = PlanText + Convert.ToString(i) + ". Плануємо викликати метод letity(20, 30) класу Sphere iз класу futbol_mjach" + newline; i++;
            s = fm.letity(20, 30);
            PlanText = PlanText + Convert.ToString(i) + ". Плануємо викликати перевизначений метод letity(20, 30, 5) класу futbol_mjach" + newline; i++;
            s = fm.letity(20, 30, 5);
            label1.Text = PlanText;
            label2.Text = sumText;
        }
    }
}
