using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PaintProgram
{
    public partial class PaintForm : Form
    {
        //class variables
        private bool canPaint = false;
        Color color;
        int thickness;
        Bitmap image;
        int x0;
        int y0;
       

        public PaintForm()
        {
            InitializeComponent();
            //initialize event handlers
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);
            pictureBox1.MouseUp += new MouseEventHandler(pictureBox1_MouseUp);
            pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_MouseMove);
            
            //set default color & thickness
            thickness = 5;
            color = Color.Red;

            //set default text
            colorLabel.Text = color.ToString();
            brushLabel.Text = "Brush Size: " + thickness;

            //create new bitmap to hold picture data this will allow us to save data 
            image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //if mouse is pushed down paint
            if (canPaint)
            {
                if (x0 != 0 && y0 != 0)
                {
                
                    //create graphics object from bitmap
                    Graphics g = Graphics.FromImage(image);

                    //create a pen 
                    Brush brush = new SolidBrush(color);
                    Pen pen = new Pen(brush, thickness);
                    pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                   
                    //set start/finish points of line
                    Point startPoint = new Point(x0, y0);
                    Point endPoint = new Point(e.X, e.Y);
                    g.DrawLine(pen, startPoint, endPoint);
                    

                    //make picture box image our bitmap
                    pictureBox1.Image = image;
                    g.Dispose();
                }

                //set previous mouse location from current location
                x0 = e.X;
                y0 = e.Y;

            }
        }

       


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //set to true so we can paint
            canPaint = true;

            //set initial mouse draw location 
            x0 = e.X;
            x0 = e.Y;
            pictureBox1_MouseMove(sender, e);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //set to false to we cannot paint
            canPaint = false;

            //set to zero
            x0 = 0;
            y0 = 0;
        }

        private void groupBox1_CheckedChanged(object sender, EventArgs e)
        {
            //determine which color radion button is selected & set color
            if (sender == radioButton1)
            {
                color = Color.Red;
            }
            else if (sender == radioButton2)
            {
                color = Color.Blue;
            }
            else if (sender == radioButton3)
            {
                color = Color.Green;
            }
            else if (sender == radioButton4)
            {
                color = Color.Black;
            }
            //set color text label
            colorLabel.Text = color.ToString(); 

        }

        private void groupBox2_CheckedChanged(object sender, EventArgs e)
        {
            //determine which size radio button is selected and set pen thickness
            if (sender == radioButton5)
            {
                thickness = 5;
            }
            else if (sender == radioButton6)
            {
                thickness = 10;
            }
            else if (sender == radioButton7)
            {
                thickness = 15;
            }
            //set brush size label text
            brushLabel.Text = "Brush Size: " + thickness;

        }

     
    }
}
