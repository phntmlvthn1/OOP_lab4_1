using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Dynamic;

 namespace OOP_lab4_1
{
   


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }





        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            Storage.Add(X,Y);
            int vlX = Storage.GetValX(0);
            int vlY = Storage.GetValY(0);
            label1.Text = vlX.ToString() + ", " + vlY.ToString();
            label2.Text = Storage.GetSize().ToString();
        }
    }

    public  class CCircle
    {
        public static int x,y;
       public CCircle(int x_, int y_)
        {
            x = x_;
            y = y_;
        }
        public int GetValueX()
        {
            return(x);
        }
        public int GetValueY()
        {
            return (y);
        }
        ~CCircle()
        {

        }

    }

     public class Storage 
    {
        static public int size;
        static public  CCircle[]  objects;
     
        public Storage()
        {
            objects = new CCircle [0];
            size = 0;
        }

        ~Storage()
        {

        }
        static public int GetSize()
        {
            return (size);
        }
        static public int GetValX(int index)
        {
            return (objects[index].GetValueX());
        }
        static public int GetValY(int index)
        {
            return (objects[index].GetValueY());
        }
        static public void Add(int x_, int y_)
        {
            CCircle[] objects2 = new CCircle[size + 1];
            if (size != 0)
            {
                for (int j = 0; j < size; j++)
                {
                    objects2[j] = (objects[j]);
                }
            }
            objects = null;
            objects = objects2;
            objects[size] = new CCircle(x_,y_);
            size = size + 1;
            


        }

    }

}
