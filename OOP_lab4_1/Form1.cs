using System;
using System.Drawing;
using System.Windows.Forms;
using Rectangle = System.Drawing.Rectangle;



namespace OOP_lab4_1
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        Bitmap bmp = new Bitmap(1800, 800);
        MyStorage str = new MyStorage();

        public Bitmap Image { get; internal set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e) // переменная - счетчик, сколько сейчас есть объектов на форме, при del обнуляется, для раскрашивания круга в зеленый
        {

        }

        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            Graphics g = Graphics.FromImage(bmp);
            Form1 snd = (Form1)sender;
            if (e.KeyData == Keys.Delete)
            {
                str.Del(snd, bmp, g);
                str.DrawAll(snd, bmp, g);
                this.Refresh();
            }
            if (e.KeyData == Keys.Space)
            {
                str.DrawAll2(snd, bmp, g);
                this.Refresh();
            }
        }

        private void Form1_MouseUp_1(object sender, MouseEventArgs e)
        {

            Graphics g = Graphics.FromImage(bmp);
            Form1 pb = (Form1)sender;


            if (Control.ModifierKeys == Keys.Control)
            { 
                if (str.isHit2(e.X, e.Y, pb, bmp, g))
                this.Refresh();
           
            }
            else
            {
                if (str.isHit(e.X, e.Y, pb, bmp, g))
                    this.Refresh();
                else
                {

                    if (e.Button == MouseButtons.Left)
                    {
                        str.Add(new CCircle(e.X, e.Y), pb, bmp, g);
                    }
                    if (e.Button == MouseButtons.Right)
                    {

                    }
                    this.Refresh();
                }
            }
            
        }
    }


    public class CCircle
    {

        public int x, y;
        public bool isSelected = false;
        public int rad = 75;
        public CCircle(int x_, int y_)
        {
            x = x_;
            y = y_;
        }
        ~CCircle()
        {

        }

        public void DrawCircleBlack(Form1 sender, Bitmap bmp, Graphics g)
        {
            Rectangle rect = new Rectangle(x - rad, y - rad, rad * 2, rad * 2);
            Pen pen = new Pen(Color.Black, 5);
            isSelected = false;
            g.DrawEllipse(pen, rect);
            sender.BackgroundImage = bmp;

        }

        public void DrawCircleGreen(Form1 sender, Bitmap bmp, Graphics g)
        {
            Rectangle rect = new Rectangle(x - rad, y - rad, rad * 2, rad * 2);
            Pen pen = new Pen(Color.Green, 5);
            isSelected = true;
            g.DrawEllipse(pen, rect);
            sender.BackgroundImage = bmp;
        }

        public void DelCircle(Form1 sender, Bitmap bmp, Graphics g)
        {
            Rectangle rect = new Rectangle(x - rad, y - rad, rad * 2, rad * 2);
            Pen pen = new Pen(Color.White, 5);
            isSelected = true;
            g.DrawEllipse(pen, rect);
            sender.BackgroundImage = bmp;
        }


        public bool isHit(int x_, int y_)
        {
            if (((x - rad) < x_) && (x + rad > x_) && ((y - rad - rad) < y_) && (y + rad > y_))
            { 
                return true;
            } 
            else
                return false;
        }

    }

    public class MyStorage
    {
        static public int size, asize = 0;
        static public CCircle[] objects;
        static public CCircle[] allobjects;
        public MyStorage()
        {
            objects = new CCircle[100];
            allobjects = new CCircle[100];
        }

        ~MyStorage()
        {

        }

        public void Drawing(int index, Form1 sender, Bitmap bmp, Graphics g)
        {
            if (objects[index] != null)
                objects[index].DrawCircleGreen(sender, bmp, g);
            DrawAll(sender, bmp, g);

        }

        public void DrawAll(Form1 sender, Bitmap bmp, Graphics g)
        {
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                    objects[i].DrawCircleBlack(sender, bmp, g);
            }
        }

        public void DrawAll2(Form1 sender, Bitmap bmp, Graphics g)
        {
            for (int i = 0; i < asize; i++)
            {
                    allobjects[i].DrawCircleBlack(sender, bmp, g);
            }
            for (int i = 0;  i< asize; i++)
            objects[i] = allobjects[i];
            size = asize;
        }

        public void DrawAll3(Form1 sender, Bitmap bmp, Graphics g)
        {
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null) { 
                    if (objects[i].isSelected)
                objects[i].DrawCircleGreen(sender, bmp, g);
                else
                    objects[i].DrawCircleBlack(sender, bmp, g);
                }
            }
        }

        public int GetSize()
        {
            return (size);
        }

        public int GetASize()
        {
            return (asize);
        }

        public void Add(CCircle obj, Form1 sender, Bitmap bmp, Graphics g)
        {
            objects[size] = obj;
            allobjects[asize] = obj;
            Drawing(size, sender, bmp, g);
            size++;
            asize++;
        }

        public bool isHit(int x, int y, Form1 sender, Bitmap bmp, Graphics g)
        {
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                    if (objects[i].isHit(x, y))
                {
                    DrawAll(sender, bmp, g);
                    objects[i].DrawCircleGreen(sender, bmp, g);
                    return true;
                }
            }
            return false;
        }

        public bool isHit2(int x, int y, Form1 sender, Bitmap bmp, Graphics g)
        {
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                if (objects[i].isHit(x, y))
                {
                    objects[i].DrawCircleGreen(sender, bmp, g);
                    DrawAll3(sender, bmp, g);
                    return true;
                }
            }
            return false;
        }

        public void Del(Form1 sender, Bitmap bmp, Graphics g)
        {
         for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                    if (objects[i].isSelected)
                {
                        objects[i].DelCircle(sender, bmp, g);
                        objects[i] = null;
                }
            }
        }
    }

}
