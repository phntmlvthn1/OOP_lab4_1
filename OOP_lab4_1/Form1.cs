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
        int start = 0;
        static int n = 25;
        static int[,] arr = new int[n, n];
        Bitmap bmp = new Bitmap(1800, 800);
        MyStorage str = new MyStorage();

        public Bitmap Image { get; internal set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void Wait(double seconds)
        {
            int ticks = System.Environment.TickCount + (int)Math.Round(seconds * 1000.0);
            while (System.Environment.TickCount < ticks)
            {
                Application.DoEvents();
            }
        }
        private void Form1_MouseUp_1(object sender, MouseEventArgs e)
        {

            Graphics g = Graphics.FromImage(bmp);
            Form1 pb = (Form1)sender;


           
                if (str.isHit(e.X, e.Y, pb, bmp, g))
                    this.Refresh();
                else
                {

                        str.Add(new CCircle(e.X, e.Y), pb, bmp, g);

                    this.Refresh();
                }
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(bmp);
            Form1 pb = sender as Form1;
            str.GetArr(arr);
            bool[] visited = new bool[n];
            void DFS(int st)
            {
                int r;
                label1.Text += (st + 1).ToString();
                label1.Text += ", ";
                str.DLG(st, pb, bmp, g);
                str.ZalivkaGreen(st, pb, bmp, g);
               
                this.Refresh();
                Wait(1.5);
                visited[st] = true;

                for (r = 0; r < n; r++)
                    if ((arr[st, r] != 0) && (!visited[r]))
                        DFS(r);
            }
            for (int i = 0; i < n; i++)
                visited[i] = false;

            DFS(start);
        }


        public class Node
        {
            public int inf;
            public Node next;
        }

        public void push(ref Node st, int dat)
        { // Загрузка числа в стек

            Node el = new Node();
            el.inf = dat;
            el.next = st;
            st = el;
        }

        public int pop(ref Node st)
        { // Извлечение из стека

            int value = st.inf;
            Node temp = st;
            st = st.next;
            temp = null;

            return value;
        }

        public int peek(Node st)
        { // Получение числа без его извлечения
            return st.inf;
        }

        public Node[] graph; // Массив списков смежности

        public void add(ref Node list, int data)
        {
            if (list == null)
            {
                list = new Node();
                list.inf = data;
                list.next = null;
                return;
            }

            Node temp = list;
            while (temp.next != null)
            {
                temp = temp.next;
            }
            Node elem = new Node();
            elem.inf = data;
            elem.next = null;
            temp.next = elem;
        }

        public void del(ref Node l, int key)
        { // Удаление вершины key из списка

            if (l.inf == key)
            {
                Node tmp = l;
                l = l.next;
                tmp = null;
            }
            else
            {
                Node tmp = l;
                while (tmp != null)
                {
                    if (tmp.next != null) // есть следующая вершина
                    {
                        if (tmp.next.inf == key)
                        { // и она искомая
                            Node tmp2 = tmp.next;
                            tmp.next = tmp.next.next;
                            tmp2 = null;
                        }
                    }
                    tmp = tmp.next;
                }
            }
        }

        public int eiler(Node[] gr, int num)
        { // Определение эйлеровости графа

            int count;
            for (int i = 0; i < num; i++)
            { //проходим все вершины

                count = 0;
                Node tmp = gr[i];

                while (tmp != null)
                { // считаем степень
                    count++;
                    tmp = tmp.next;
                }
                if (count % 2 == 1)
                {
                    return 0; // степень нечетная
                }
            }
            return 1; // все степени четные
        }
      

        private void button2_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(bmp);
            Form1 pb = sender as Form1;
            graph = new Node[n];
            for (int i = 0; i < n; i++)
            {
                graph[i] = null;
            }
            str.GetArr(arr);
            for (int i = 0; i < n; i++) // заполняем массив списков
            {
                for (int j = 0; j < n; j++)
                {
                    if (arr[i, j] != 0)
                    {
                        add(ref graph[i], j);
                    }
                }
            }


            int res = eiler(graph, n);
            if (res == 1)
            {
             
                Node S = null; // Стек для  пройденных вершин
                int u;

                push(ref S, start); //сохраняем ее в стек
                while (S != null)
                { //пока стек не пуст
                    start = peek(S); // текущая вершина
                    if (graph[start] == null)
                    { // если нет инцидентных ребер
                        start = pop(ref S);


                        label2.Text += (start + 1).ToString() + "  ";
                        str.DLG(start, pb, bmp, g);
                        str.ZalivkaGreen(start, pb, bmp, g);
                        this.Refresh();
                        Wait(1.5);

                        //Wait(1.5);
                    }
                    else
                    {
                        u = graph[start].inf;
                        push(ref S, u); //проходим в следующую вершину
                        del(ref graph[start], u);
                        del(ref graph[u], start); //удаляем пройденное ребро
                    }
                }
            }
            else
            {
                label2.Text = "Граф не является эйлеровым.";
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            { 
            start = Int32.Parse(textBox1.Text);
            start--;
            }
            else 
            start = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(bmp);
            Form1 pb = sender as Form1;
            str.DrawAll(pb, bmp, g);
            textBox1.Text = "";
            label1.Text = "Путь: ";
            label2.Text = "Путь: ";
            this.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Restart();

        }
    }


    public class CCircle
    {
       
        public int x, y, num;
        public bool isSelected = false;
        public int rad = 30;
        public CCircle(int x_, int y_)
        {
            x = x_;
            y = y_;
        }
        ~CCircle()
        {

        }

        public void DrawCircleBlack(int size, Form1 sender, Bitmap bmp, Graphics g)
        {
            num = size+1;
            Rectangle rect = new Rectangle(x - rad, y - rad, rad * 2, rad * 2);
            Pen pen = new Pen(Color.Black, 5);
            Font font = new Font("Arial", 35, FontStyle.Regular);
            
            isSelected = true;
            g.DrawEllipse(pen, rect);

            g.DrawString((num).ToString(), font, Brushes.Black, x - 20, y - 20);
            sender.BackgroundImage = bmp;
            
        }

        public void Zalivka(Form1 sender, Bitmap bmp, Graphics g)
        {
            Font font = new Font("Arial", 35, FontStyle.Regular);
            SolidBrush brush = new SolidBrush(Color.White);
            g.FillEllipse(brush, x - rad, y - rad, rad * 2, rad * 2);
            g.DrawString((num).ToString(), font, Brushes.Black, x - 20, y - 20);
           
        }

        public void ZalivkaGreen(Form1 sender, Bitmap bmp, Graphics g)
        {
            Font font = new Font("Arial", 35, FontStyle.Regular);
            SolidBrush brush = new SolidBrush(Color.Green);
            g.FillEllipse(brush, x - rad, y - rad, rad * 2, rad * 2);
            g.DrawString((num).ToString(), font, Brushes.Black, x - 20, y - 20);

        }

        public void DrawCircleGreen(int size, Form1 sender, Bitmap bmp, Graphics g)
        {
            Rectangle rect = new Rectangle(x - rad, y - rad, rad * 2, rad * 2);
            Pen pen = new Pen(Color.Green, 5);
            Font font = new Font("Arial", 25, FontStyle.Regular);
            isSelected = true;
            g.DrawEllipse(pen, rect);
            g.DrawString((size + 1).ToString(), font, Brushes.Green, x-20, y-20);
            sender.BackgroundImage = bmp;
        }

        public void DrawLine(int x1, int y1, int x2, int y2, Form1 sender, Bitmap bmp, Graphics g)
        {
            Pen p = new Pen(Color.Black, 7);
            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            g.DrawLine(p, p1, p2);

        }

        public void DrawLineGreen(int x1, int y1, int x2, int y2, Form1 sender, Bitmap bmp, Graphics g)
        {
            Pen p = new Pen(Color.Green, 7);
            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            g.DrawLine(p, p1, p2);

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

        public int GetCoorX()
        {
            return (x);
        }
        public int GetCoorY()
        {
            return (y);
        }
    }

    public class MyStorage
    {
        static int[,] arr2 = new int[25, 25];
         
        static public int size= 0;
        static public int dlc = 0;
        static public int x1, x2, y1, y2;
        static public int  dl1  = -1;
        static public int  dl2 = -1;
        static public CCircle[] objects;

        public MyStorage()
        {
            objects = new CCircle[100];
            for (int i = 0; i < 25; i++)
                for (int j = 0; j < 25; j++)
                    arr2[i, j] = 0;
        }

        ~MyStorage()
        {

        }

        public void Drawing(int index, Form1 sender, Bitmap bmp, Graphics g)
        {
            if (objects[index] != null)
                objects[index].DrawCircleBlack(size,sender, bmp, g);

        }


        public int GetSize()
        {
            return (size);
        }
        public void ZalivkaGreen(int index, Form1 sender, Bitmap bmp, Graphics g)
        {
            objects[index].ZalivkaGreen(sender, bmp, g);
        }
        public void GetArr(int[,] arr)
        {
            for (int i = 0; i < 25; i++)
                for (int j = 0; j < 25; j++)
                    arr[i, j] = arr2[i,j];
        }
        public void Add(CCircle obj, Form1 sender, Bitmap bmp, Graphics g)
        {
            objects[size] = obj;

            Drawing(size, sender, bmp, g);
            size++;

        }
        public void DrawL(Form1 sender, Bitmap bmp, Graphics g)
        {
            x1 = objects[dl1].GetCoorX();
            y1 = objects[dl1].GetCoorY();
            x2 = objects[dl2].GetCoorX();
            y2 = objects[dl2].GetCoorY();
            objects[0].DrawLine(x1, y1, x2, y2, sender, bmp, g);
            objects[dl1].Zalivka(sender, bmp, g);
            objects[dl2].Zalivka(sender, bmp, g);
            arr2[ dl1, dl2] = 1;
            arr2[ dl2, dl1] = 1;
        }
        public void DrawLG(Form1 sender, Bitmap bmp, Graphics g)
        {
            x1 = objects[dl1].GetCoorX();
            y1 = objects[dl1].GetCoorY();
            x2 = objects[dl2].GetCoorX();
            y2 = objects[dl2].GetCoorY();
            objects[0].DrawLineGreen(x1, y1, x2, y2, sender, bmp, g);
            objects[dl1].ZalivkaGreen(sender, bmp, g);
            objects[dl2].ZalivkaGreen(sender, bmp, g);

        }
        public void DrawAll(Form1 sender, Bitmap bmp, Graphics g)
        {
            for (int i =0; i < size; i++ )
            {
                if (objects[i] != null)
                    objects[i].Zalivka(sender, bmp, g);
               for (int j =0; j<size; j++)
                {
                    if (arr2[i,j]==1)
                    {
                        dl1 = i;
                        dl2 = j;
                        DrawL(sender, bmp, g);
                    }
                }
            }
            dl1 = -1;
            dl2 = -1;
            dlc = 0;
        }
        public void DLG(int index, Form1 sender, Bitmap bmp, Graphics g)
        {
            if (dl1 == -1)
            {
                dl1 = index;
                dlc++;
            }
            else
            {
                dl2 = index;
                dlc++;
            }
            if (dlc == 2)
            {
                DrawLG(sender, bmp, g);
                dl1 = dl2;
                dl2 = -1;
                dlc = 1;
            }

        }
        public bool isHit(int x, int y, Form1 sender, Bitmap bmp, Graphics g)
        {
            for (int i = 0; i < size; i++)
            {
                if (objects[i] != null)
                    if (objects[i].isHit(x, y))
                    {
                        if (dl1 == -1)
                        { 
                    dl1 = i;
                    dlc++;
                        }
                        else
                        {
                            dl2 = i;
                            dlc++;
                        }
                    if (dlc==2)
                        {
                            DrawL( sender,  bmp,  g);
                            dl1 = -1;
                            dl2 = -1;
                            dlc = 0;
                        }
                    return true;
                    }
            }
            return false;
        }


    }

}
