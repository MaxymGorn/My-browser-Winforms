using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp10;

namespace WindowsFormsApp9
{

    public partial class Form1 : Form
    {
        ListCategory ListCategory = new ListCategory();
        DatManage datManage = new DatManage();
        string pat = "ListCategory.xml";
        int tmp;
        public Form1()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;

            LoadCategory();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
         
        }
        private void LoadCategory()
        {
            comboBox1.Text = "";
            comboBox1.Items.Clear();

      

            ListCategory = datManage.DeserializeXML<ListCategory>(pat);
            foreach(var el in ListCategory.SiteAndCategories)
            {
                comboBox1.Items.Add(el.Name_Category);
            }
            if(comboBox1.Items.Count>0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            webBrowser1.Navigate("google.com");

        }
 
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
      
        }

        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                webBrowser1.Navigate(textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }


    private void button2_Click(object sender, EventArgs e)
    {
        webBrowser1.GoBack();
    }

        private void button4_Click(object sender, EventArgs e)
        {
            if(CheckInternetConnection())
            webBrowser1.Refresh();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void OpenWebSite(string text, bool not = true)
        {
            if(CheckInternetConnection())
            {
               
                  

                if (RemoteFileExists(text))
                {
                    if(not==true)
                    {
                        NotificShow(5, "На сайте есть опасный контент!!!");
                       
                    }
                    webBrowser1.Navigate(text);
                }
                else
                {
                    MessageBox.Show("Eror Site!", "Notifications", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
         
        }
        void NotificShow(int secondClose, string text) 
        {
            Form2 notific = new Form2(secondClose, text);
            notific.Visible = true;
        }
        private bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }
        private void textBox1_KeyDown_2(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                OpenWebSite(textBox1.Text);
       
            }
          
          

        }
     
        private bool CheckInternetConnection()
        {
            try
            {
                
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://www.google.ru/");
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
                request.Timeout = 10000;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream ReceiveStream1 = response.GetResponseStream();
                StreamReader sr = new StreamReader(ReceiveStream1, true);
                string responseFromServer = sr.ReadToEnd();

                response.Close();
               
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нет подключения к интернету!\nПроверьте ваш фаервол или настройки сетевого подключения...", "Notifications", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
          

            if (textBox1.Text.Length == 0)
            { MessageBox.Show("Eror Text!", "Notifications", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            OpenWebSite(textBox1.Text);
        }

        private void forwardToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void backwardToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
          

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
             

            ListCategory= datManage.DeserializeXML<ListCategory>(pat);
            ListCategory.SiteAndCategories.Add(new SiteAndCategory());
            datManage.SerializeXML(ListCategory, pat);
            LoadCategory();
        }

        private void delToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedIndex < 0)
                {
                    throw new Exception("Eror Selected!");
                }
              

                ListCategory = datManage.DeserializeXML<ListCategory>(pat);
                ListCategory.SiteAndCategories.RemoveAt(comboBox1.SelectedIndex);
                datManage.SerializeXML(ListCategory, pat);
                LoadCategory();
                
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Notifications", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
            }
        }
 
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBox1.DropDownStyle == ComboBoxStyle.DropDownList)
            {
                tmp = comboBox1.SelectedIndex;
                comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
                editToolStripMenuItem.Text = "Save";


            }
            else
            {
                ListCategory.SiteAndCategories[tmp].Name_Category = comboBox1.Text;
                listBox1.Items.Clear();
                datManage.SerializeXML(ListCategory, pat);
                LoadCategory();
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                editToolStripMenuItem.Text = "Edit";
            }

      
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedIndex < 0)
                {
                    throw new Exception("Eror Selected!");
                }
          

                ListCategory = datManage.DeserializeXML<ListCategory>(pat);
                ListCategory.SiteAndCategories[comboBox1.SelectedIndex].Sites.Add(new Site() { Name_Site= "https://www.google.com/search?q=%D0%B3%D1%83%D0%B3%D0%B4&oq=%D0%B3%D1%83%D0%B3%D0%B4&aqs=chrome..69i57j0l7.6873j0j7&sourceid=chrome&ie=UTF-8", Notifications=false });
                datManage.SerializeXML(ListCategory, pat);
                LoadCategory();
                //
                listBox1.Items.Clear();
                listBox2.Items.Clear();

                ListCategory = datManage.DeserializeXML<ListCategory>(pat);
                foreach (var el in ListCategory.SiteAndCategories[comboBox1.SelectedIndex].Sites)
                {
                    var item = listBox1.Items.Add(el.Name_Site);
                    if (el.Notifications == false)
                    {
                        var item2 = listBox2.Items.Add("✗");
                    }
                    else
                    {
                        var item2 = listBox2.Items.Add("✓");
                    }
                }
                //

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Notifications", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();

            ListCategory = datManage.DeserializeXML<ListCategory>(pat);
            foreach(var el in ListCategory.SiteAndCategories[comboBox1.SelectedIndex].Sites)
            {
                var item = listBox1.Items.Add(el.Name_Site);
                if(el.Notifications==false)
                {
                    var item2 = listBox2.Items.Add("✗");
                }
                else
                {
                    var item2 = listBox2.Items.Add("✓");
                }
            }
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedIndex < 0)
                {
                    throw new Exception("Eror Selected!");
                }


                ListCategory = datManage.DeserializeXML<ListCategory>(pat);
                ListCategory.SiteAndCategories[comboBox1.SelectedIndex].Sites[listBox1.SelectedIndex].Name_Site=(textBox1.Text.Length>0) ? textBox1.Text:"google.com";
                datManage.SerializeXML(ListCategory, pat);
                LoadCategory();
                listBox1.Items.Clear();


            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Notifications", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ListCategory.SiteAndCategories[comboBox1.SelectedIndex].Sites[listBox1.SelectedIndex].Notifications==true)
            {
               var res= MessageBox.Show("Do you want open this site?", "Notifications", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if(res==DialogResult.OK)
                {
                    OpenWebSite(ListCategory.SiteAndCategories[comboBox1.SelectedIndex].Sites[listBox1.SelectedIndex].Name_Site);

                }

            }
            else
            {
                OpenWebSite(ListCategory.SiteAndCategories[comboBox1.SelectedIndex].Sites[listBox1.SelectedIndex].Name_Site, false);

            }

        }

        private void delToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListCategory.SiteAndCategories[comboBox1.SelectedIndex].Sites.RemoveAt(listBox1.SelectedIndex);
            listBox1.Items.Clear();
            datManage.SerializeXML(ListCategory, pat);
            LoadCategory();
        }

        private void addNotificationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedIndex < 0)
                {
                    throw new Exception("Eror Selected!");
                }


                ListCategory = datManage.DeserializeXML<ListCategory>(pat);
                if(ListCategory.SiteAndCategories[comboBox1.SelectedIndex].Sites[listBox1.SelectedIndex].Notifications==false)
                ListCategory.SiteAndCategories[comboBox1.SelectedIndex].Sites[listBox1.SelectedIndex].Notifications = true;
                else
                {
                    ListCategory.SiteAndCategories[comboBox1.SelectedIndex].Sites[listBox1.SelectedIndex].Notifications = false;

                }

              datManage.SerializeXML(ListCategory, pat);
                //
                listBox1.Items.Clear();
                listBox2.Items.Clear();

                ListCategory = datManage.DeserializeXML<ListCategory>(pat);
                foreach (var el in ListCategory.SiteAndCategories[comboBox1.SelectedIndex].Sites)
                {
                    var item = listBox1.Items.Add(el.Name_Site);
                    if (el.Notifications == false)
                    {
                        var item2 = listBox2.Items.Add("✗");
                    }
                    else
                    {
                        var item2 = listBox2.Items.Add("✓");
                    }
                }
                //

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Notifications", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
            }
        }

        private void listBox1_RightToLeftChanged(object sender, EventArgs e)
        {

        }
    }
}
