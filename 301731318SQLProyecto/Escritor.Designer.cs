using _201731318SQLProyecto.Backend.Parser_y_Scanner;
using System.Collections.Generic;
using System.Drawing;

namespace _201731318SQLProyecto
{
    partial class Escritor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        List<Token> tokenstexto = new List<Token>();
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }



        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.LineNumberTextBox = new System.Windows.Forms.RichTextBox();
            this.consola = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(48, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(911, 321);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // LineNumberTextBox
            // 
            this.LineNumberTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LineNumberTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LineNumberTextBox.Location = new System.Drawing.Point(13, 12);
            this.LineNumberTextBox.Name = "LineNumberTextBox";
            this.LineNumberTextBox.Size = new System.Drawing.Size(29, 321);
            this.LineNumberTextBox.TabIndex = 1;
            this.LineNumberTextBox.Text = "";
            // 
            // consola
            // 
            this.consola.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.consola.BackColor = System.Drawing.SystemColors.InfoText;
            this.consola.EnableAutoDragDrop = true;
            this.consola.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consola.ForeColor = System.Drawing.SystemColors.Info;
            this.consola.Location = new System.Drawing.Point(12, 339);
            this.consola.Name = "consola";
            this.consola.ReadOnly = true;
            this.consola.Size = new System.Drawing.Size(947, 138);
            this.consola.TabIndex = 2;
            this.consola.Text = "";
            // 
            // Escritor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(971, 489);
            this.Controls.Add(this.consola);
            this.Controls.Add(this.LineNumberTextBox);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Escritor";
            this.Text = "|";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox LineNumberTextBox;

        List<Token> TokensTexto
        {
            get { return tokenstexto; }
            set
            {
                tokenstexto = value;
            }
        }

        public void InsertarTexto(List<Token> tokenstexto)
        {
            richTextBox1.ResetText();
            for (int i = 0; i < tokenstexto.Count; i++)
            {
                Token t = tokenstexto[i];
                if (t.Color != null && t.Tipo != " Error")
                {
                    richTextBox1.SelectionColor = t.Color;
                    richTextBox1.SelectionBackColor = System.Drawing.Color.White;
                    richTextBox1.AppendText(t.Lexema);
                }
                else if (t.Color != null && t.Tipo == " Error")
                {
                    richTextBox1.SelectionColor = t.Color;
                    richTextBox1.SelectionBackColor = System.Drawing.Color.Pink;
                    richTextBox1.AppendText(t.Lexema);

                }
                else
                {
                    richTextBox1.AppendText(t.Lexema);
                }
            }
        }

        public void InsertarErrores(List<ErrorSintactico> errores)
        {
            consola.ResetText();
            for (int i = 0; i < errores.Count; i++)
            {
                ErrorSintactico t = errores[i];
                Token token = t.Token2;
                consola.SelectionColor = Color.White;
                consola.AppendText("ERROR SINTACTICO EN ");
                consola.SelectionColor = Color.Black;
                consola.SelectionBackColor = System.Drawing.Color.LightGray;
                consola.AppendText(token.Lexema);
                consola.SelectionBackColor = System.Drawing.Color.Black;
                consola.SelectionColor = Color.White;
                consola.AppendText(" EN LA LINEA " + token.Fila + "  COLUMNA " + token.Columna); ;
                consola.SelectionColor = Color.Red;
                consola.SelectionBackColor = System.Drawing.Color.Pink;
                consola.AppendText(t.Mensaje+"  \n");
                consola.SelectionColor = Color.White;
                consola.AppendText("Se han descartado los siguientes tokens  \n");
                for(int j=0; j < t.Descartados.Count; j++)
                {
                    Token err = t.Descartados[j];
                    consola.AppendText("\t"+err.Lexema+" Fila: "+err.Fila+" Columna"+ err.Columna+"\n");
                }

            }
        }

        private System.Windows.Forms.RichTextBox consola;
    }
}