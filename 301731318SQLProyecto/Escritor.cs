using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;


namespace _301731318SQLProyecto
{
    public partial class Escritor : Form
    {
        public Escritor(String texto)
        {
            InitializeComponent();
            richTextBox1.Text = texto;
            AddLineNumbers();
        }

        public Escritor()
        {
            InitializeComponent();
            AddLineNumbers();
        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            AddLineNumbers();
        }

        public void AddLineNumbers()
        {
            Point pt = new Point(0, 0);  
            int First_Index = richTextBox1.GetCharIndexFromPosition(pt);
            int First_Line = richTextBox1.GetLineFromCharIndex(First_Index);  
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;   
            int Last_Index = richTextBox1.GetCharIndexFromPosition(pt);
            int Last_Line = richTextBox1.GetLineFromCharIndex(Last_Index);
            LineNumberTextBox.SelectionAlignment = HorizontalAlignment.Center;   
            LineNumberTextBox.Text = "";
            LineNumberTextBox.Width = getWidth();
            for (int i = First_Line; i <= Last_Line; i++)
            {
                LineNumberTextBox.Text += i + 1 + "\n";
            }
        }

        public String TEXTO()
        {
            Console.Write(richTextBox1.Text+"\n");
            return richTextBox1.Text;
        }

        public int getWidth()
        {
            int w = 25;   
            int line = richTextBox1.Lines.Length;

            if(line <= 99)
            {
                w = 20 + (int)richTextBox1.Font.Size;
            }
            else if(line <= 999)     
            {
                w = 30 + (int)richTextBox1.Font.Size;
            }
            else
            {
                w = 50 + (int)richTextBox1.Font.Size;
            }
            return  w;
        }
    }
}
