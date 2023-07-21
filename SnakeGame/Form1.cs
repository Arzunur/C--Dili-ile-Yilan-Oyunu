using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Panel parca; //ekranda olan yeşil alan=PANEL
        Panel elma = new Panel();
        List<Panel> yilan =new List<Panel>(); //List'i listbox gibi düşünebilirsin.
        
        string yon = "sağ";
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void label3_Click(object sender, EventArgs e)
        {
            label2.Text = "0";
            panelgizle();
            parca =new Panel();
            parca.Location = new Point(200, 200);
            parca.Size = new Size(20, 20);
            parca.BackColor= Color.DeepPink;
            yilan.Add(parca);//parca'yı yilan listesine ekleceğiz.
            panel1.Controls.Add(yilan[0]);
            //oyun karakterin ekranda görünmesi için listenin [0]-->hangi elemanı kastettiğini belirtmek için kullanılır. elemanına gönderme 

            timer1.Start();
            elmaolustur();
        }
        void carpısma()
        {
            for (int i =2; i<yilan.Count; i++) //yılanın baş kısmı 1.Yılan baş kısmıyla çarpışmayacağı için i=2 den başlattık.
                           //yilan.Count= yılanın boyu
            {
                if (yilan[0].Location == yilan[i].Location)
                {
                    label4.Visible = true;
                    label4.Text = "KAYBETTİNİZ";
                    timer1.Stop();

                }
            }               
        }
        void panelgizle()
        {
            panel1.Controls.Clear();
            yilan.Clear();
            label4.Visible = false;
        }
        void puankontrol()
        {
            int puan = int.Parse(label2.Text);
            if(puan>300)
            {
                label4.Text = "KAZANDINIZ";
                label4.Visible= true;
                timer1.Stop(); //timer'a bağlı olan tüm oyun kontrolleri durdurulur.
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int locx = yilan[0].Location.X;
            int locy= yilan[0].Location.Y;
            elmayedimi();
            hareket();
            carpısma();
            puankontrol();

            if (yon=="sağ")
            {
                if (locx < 580) //600panel boyutu karakterin sol üst köşesi 0 olarak kabul edilir. 20 de karakter boyutu 
                    locx += 20; 
                else 
                    locx= 0;
            }
            if (yon == "sol")
            {
                if (locx >20)   //x sol doğru gittikçe + o yüzden değer 0'dan büyük olmalı
                    locx -= 20; 
                else
                    locx = 580;
            }
            if (yon == "aşağı")
            {
                if (locy < 580) //y eks aşağı gittikçe azalır oyüzden max değer 580'den küçük olmalı
                    locy += 20;
                else
                    locy = 0;

            }
            if (yon == "yukarı")
            {
                if (locy > 20)
                    locy -= 20;
                else   
                    locy = 580;
            }
            yilan[0].Location = new Point(locx, locy);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right && yon!="sol") //&& koşulu,karakter ters istikamete gittiği zmn yanmasını engellemek
                yon = "sağ";
            if (e.KeyCode == Keys.Left && yon != "sağ")//sol tuşa bastığımızda ve yön sağ değilse;
                yon = "sol";
            if (e.KeyCode == Keys.Up && yon != "aşağı")
                yon = "yukarı";
            if (e.KeyCode == Keys.Down && yon != "yukarı")
                yon = "aşağı";
        }
        void elmaolustur()
        {
            Random rnd= new Random();
            int elmax, elmay;
            elmax = rnd.Next(560);//random değerler almasını sağlar(.Next) 0-580 arasında oluşmasını sağlanır
            elmay = rnd.Next(560);

            elmax -=elmax % 20;//karakterle aynı yere gelmesi için karakterin boyutunun 20nin katı olması lazım ki aynı nok gelmeli
            elmay -= elmay % 20;
            elma.Size=new Size(20, 20);
            elma.BackColor= Color.White;
            elma.Location= new Point(elmax,elmay); 
            panel1.Controls.Add(elma);

        }
        void elmayedimi()
        {
            int puan = int.Parse(label2.Text);
            if (yilan[0].Location == elma.Location)
            {
                panel1.Controls.Remove(elma);
                puan += 20;
                label2.Text=puan.ToString();
                elmaolustur();
                parcaekle();
            }
        }
        void parcaekle()
        {
            Panel ekparca = new Panel();
            ekparca.Size=new Size(20, 20);
            ekparca.BackColor= Color.DeepPink;
            yilan.Add( ekparca );
            panel1.Controls.Add( ekparca );
        }
        void hareket()
        {
            for (int i = yilan.Count-1; i>0; i--)
                yilan[i].Location = yilan[i-1].Location;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

    }
}