using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace FastDownload
{
    public partial class Main_form : Form
    {
        public List<Download> dl = new List<Download>();
        public List<Renewal> rn = new List<Renewal>();
        Set set = new Set();

        public Main_form()
        {
            InitializeComponent();
        }

        private void Main_form_Load(object sender, EventArgs e)
        {
            set.GetConfig();      //获取配置信息
            Thread th = new Thread(new ThreadStart(BeginDisplay));
            th.IsBackground = true;   //设置后台线程
            th.Start();    //线程开始
            setToolTip();
            InitialistViewMenu();    //初始化LisyView空间菜单
            Thread th2 = new Thread(new ThreadStart(DisplayListView));    //重绘ListView控件
            th2.IsBackground = true;
            th2.Start();
            if (Set.ShowFlow == "1")
            {
                pictureBox1.Visible = pictureBox2.Visible = label1.Visible = label2.Visible = true;
            }
            if (Set.Auto == "1")
            {
                DirectoryInfo dir = new DirectoryInfo(Set.Path);
                if (dir.Exists)
                {
                    FileInfo[] files = dir.GetFiles();     //获取所有文件列表
                    foreach (FileInfo file in files)
                    {
                        if (file.Extension == ".cfg")     //判断是否有未下载完成的文件
                        {
                            Stream sm = file.Open(FileMode.Open, FileAccess.ReadWrite);
                            string s = file.Name;    //得到续传文件的文件名
                            Renewal rnn = new Renewal();    //示例化处理续传文件下载的类的实例
                            rnn.Begin(sm, s);
                            rn.Add(rnn);
                        }
                    }
                }
            }
        }
    }
}
