using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class Form2 : Form
{
    Point pt = Screen.PrimaryScreen.WorkingArea.Location;
    private TextBox textBox1;
    private int _secondClose = 0;
    internal int SecondClose
    {
        get { return _secondClose; }
        set { _secondClose = value; }
    }
    public Form2(int secondToClose, string text)
    {
        InitializeComponent();
        SecondClose = secondToClose;
        textBox1.Text = text;
        this.StartPosition = FormStartPosition.Manual;
        pt.Offset(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
        pt.Offset(-this.Width, -this.Height);
        this.Location = pt;
        this.Location = new Point(this.Location.X, this.Location.Y + this.Height);
        Task.Factory.StartNew(() =>
        {
            for (int i = 0; i < this.Height; i++)
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke((Action)(() =>
                    {
                        this.Location = new Point(this.Location.X, this.Location.Y - 1);
                    }));
                }
                if (this.Location == pt) break;
                Thread.Sleep(2);
            }
        });
        Task.Factory.StartNew(() =>
        {
            Thread.Sleep(_secondClose * 1000);
            this.Invoke((Action)(() =>
            {
                ((IDisposable)this).Dispose();
            }));
        });
    }

    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBox1.Font = new System.Drawing.Font("Mistral", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(258, 51);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form2
            // 
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(291, 71);
            this.Controls.Add(this.textBox1);
            this.Font = new System.Drawing.Font("Mistral", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    private void Form2_Load(object sender, EventArgs e)
    {

    }
}