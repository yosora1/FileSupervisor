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

namespace TxtSupervisor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object path, EventArgs e)
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //加载路径下的内容
            String path = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
            LoadData(path, treeView1.Nodes);
        }

        private void LoadData(string path, TreeNodeCollection treeNodeCollection)
        {
            //获取path下的所有文件夹
            try
                {
                    String[] dirs = Directory.GetDirectories(path);
                    foreach (var item in dirs)
                    {
                        TreeNode tnode = treeNodeCollection.Add(item);
                        LoadData(item, tnode.Nodes);
                    }
                    //获取path下的所有文本文件
                    String[] files = Directory.GetFiles(path, "*.txt");
                    foreach (var item in files)//iteam:完整路径
                    {
                       TreeNode node = treeNodeCollection.Add(Path.GetFileName(item));
                       node.Tag = item;
                    }
                }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
           
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String path = textBox2.Text;
            treeView1.Nodes.Clear();
            LoadData(path, treeView1.Nodes);
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //在节点的双击事件中
            if (e.Node.Tag != null)
            {
                textBox1.Text=File.ReadAllText(e.Node.Tag.ToString(),Encoding.Default);
            }
        }
    }
}
