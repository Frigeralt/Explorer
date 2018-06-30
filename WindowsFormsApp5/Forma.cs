using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Forma : Form
    {
        string currentPath;
        public Forma()
        {
            InitializeComponent();
            currentPath = @"C:\";
            SearchDirectory(currentPath);
            listView1.View = View.List;
        }

        private void SearchDirectory(string dir)
        {
            DirectoryInfo info = new DirectoryInfo(dir);
            FileInfo[] files = info.GetFiles();
            DirectoryInfo[] directories = info.GetDirectories();

            listView1.SmallImageList = imageList1;
            listView1.LargeImageList = imageList1;
            foreach (DirectoryInfo direct in directories)
            {
                ListViewItem item = new ListViewItem(direct.Name);
                item.ImageIndex = 0;
                listView1.Items.Add(item);
            }
            foreach (FileInfo file in files)
            {
                ListViewItem item = new ListViewItem(file.Name);
                item.ImageIndex = 1;
                listView1.Items.Add(item);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(currentPath+ listView1.SelectedItems[0].Text + '\\');
            //MessageBox.Show(currentPath + listView1.SelectedItems[0].Text);
            if (listView1.SelectedItems.Count == 0) return;
                string s = currentPath + listView1.SelectedItems[0].Text + '\\';
                FileAttributes attr = File.GetAttributes(s);
                if ((attr & FileAttributes.Directory) != FileAttributes.Directory) return;
            
                if (Directory.Exists(currentPath + listView1.SelectedItems[0].Text + '\\'))
                {
                    currentPath += listView1.SelectedItems[0].Text + '\\';
                    listView1.Clear();
                    SearchDirectory(currentPath);
                }
        }

        private void return_button_Click(object sender, EventArgs e)
        {
            currentPath = Path.GetFullPath(Path.Combine(currentPath, @"..\"));
            listView1.Clear();
            SearchDirectory(currentPath);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
