using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FastDownload
{
    public class Renewal
    {
        #region
        public bool stop = false;//暂停下载开关
        public string fileNameAndPath;//下载文件的路径及文件名
        public string filePath;//下载文件的路径
        public System.Collections.Generic.List<bool> lbo = new List<bool>();//判断所有线程是否全部下载完成
        public int thread;//下载文件所使用线程的数量
        public List<Locations> lli;//记录续传信息的可序列化类的集合
        public bool complete = false;//文件下载是否完成
        public string fileName;//下载文件的名称
        public DateTime dtBegin;//文件开始下载的时间
        public long fileSize;//下载文件的大小
        public bool stop2 = false;//停止下载，并删除下载文件 开关
        public bool state = false;//续传的状态，是否为已经执行
        private AutoResetEvent are = new AutoResetEvent(true);
        public List<Thread> G_thread_Collection = new List<Thread>();
        //是否支持多线程下载
        private bool b_thread = false;
        #endregion

        #region
        public void Begin(Stream sm, string fileNames)
        {
            BinaryFormatter bf = new BinaryFormatter();     //实例化二进制格式对象
            lli = (List<Locations>)bf.Deserialize(sm);
            dtBegin = DateTime.Now;      //设置开始续传的时间
            if (lli.Count > 0)
            {
                fileSize = lli[lli.Count - 1].Filesize;
            }
            thread = lli.Count;        //判断续传时需要多少线程
            string s = fileNames;
            //  得到续传完成后，下载本地文件的文件路径及名称
            fileNameAndPath = s.Substring(0, s.Length - 4);     //-4为何？？
            fileName = fileNameAndPath.Substring(fileNameAndPath.LastIndexOf(@"\") + 1,
                fileNameAndPath.Length - (fileNameAndPath.LastIndexOf(@"\") + 1));    //得到文件名称
            new Set().GetConfig();
            filePath = Set.Path;      //得到文件路径
            for (int i = 0; i < lli.Count; i++)
            {
                lbo.Add(false);       //续传的文件未完成
                Thread th = new Thread(GetData);  //建立线程，处理每条续传
                th.Name = i.ToString();  //  设置线程的名称
                th.IsBackground = true;
                th.Start(lli[i]);
            }
            b_thread = GetBool(lli[0].Url);
            hebinfile();
            sm.Close();
        }
        #endregion
        #region
        public void GetData(object l)
        {
            FileStream fs = null;
            Stream ss = null;
            try
            {
                //得到续传信息对象(也就是文件下载或续传的开始点与结束点)
                Locations ll = (Locations)l;
                //根据是否支持多线程来判断是否使用线程事件控制线程下载顺序
                if (!b_thread) are.WaitOne(); else are.Set();
                //根据下载地址，创建HttpWebRequest对象
                HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create(ll.Url);
                //设置下载请求超时为200秒
                hwr.Timeout = 6000;
                if (ll.Start > ll.End || ll.Start == ll.End)
                {
                    lbo[Convert.ToInt32(System.Threading.Thread.CurrentThread.Name)] = true;
                    are.Set();
                    return;
                }
                //设置当前线程续传或下载任务的开始点与结束点
                hwr.AddRange(ll.Start, ll.End);
                //得到HttpWebResponse对象
                HttpWebResponse hwp = (HttpWebResponse)hwr.GetResponse();
                //根据HttpWebResponse对象的GetResponseStream()方法得到用于下载数据的网络流对象
                ss = hwp.GetResponseStream();
                //设置文件下载的缓冲区
                byte[] buffer = new byte[Convert.ToInt32(Set.NetValue) * 8];
                //新建文件流对象，用于存放当前每个线程下载的文件
                if (filePath.EndsWith("\\"))
                    fs = new FileStream(
                    string.Format(filePath + fileName + Thread.CurrentThread.Name),
                    FileMode.Append);
                else
                    fs = new FileStream(
                        string.Format(filePath + @"\" + fileName + Thread.CurrentThread.Name),
                        FileMode.Append);
                //用于计数，每次下载有效字节数
                int i;
                //当前线程的索引
                int nns = Convert.ToInt32(Thread.CurrentThread.Name);
                //开始将下载的数据放入缓冲中
                while ((i = ss.Read(buffer, 0, buffer.Length)) > 0)
                {
                    //将缓冲中的数据写到本地文件中
                    fs.Write(buffer, 0, i);
                    //计算现在下载位置，用于续传
                    lli[nns].Start += i;
                    //点击暂停按钮后，使线程暂时挂起
                    while (stop)
                    {
                        //线程挂起
                        System.Threading.Thread.Sleep(100);
                    }
                    //点击删除按钮后，使下载过程强型停止
                    if (stop2)
                    {
                        break;
                    }
                    Thread.Sleep(10);
                }
                //关闭文件流对象
                fs.Close();
                //关闭网络流对象
                ss.Close();
                //开始记录当前线程的下载状态为已经完成
                lbo[Convert.ToInt32(Thread.CurrentThread.Name)] = true;
            }
            catch (Exception ex)
            {
                //如果出现异常，将异常信息写入错误日志
                writelog(ex.Message);
                //保存断点续传状态
                SaveState();
            }
            finally
            {
                if (!b_thread)
                    are.Set();
                else
                    are.Set();
            }
        }
        #endregion
        #region    hebinfile
        public void hebinfile()
        {
            Thread th2 = new Thread(
                delegate ()
                {
                    while (true)
                    {
                        if (!lbo.Contains(false))
                        {
                            GetFile();
                            break;
                        }
                        else
                        {
                            if (this.stop2)
                            {
                                DeleteFile();
                            }
                        }
                        Thread.Sleep(1000);
                    }
                });
            th2.IsBackground = true;
            th2.Start();
        }
        #endregion
        #region  writelog
        private void writelog(string s)
        {
                StreamWriter fs = new StreamWriter(@"c:\Download.log", true);
                fs.Write(string.Format(s + DateTime.Now.ToString("yy-MM-dd hh:mm:ss")));
                fs.Flush();
                fs.Close();
        }
        #endregion
        #region  SaveState方法
        public void SaveState()
        {
            BinaryFormatter bf = new BinaryFormatter();    //实例化二进制格式对象
            MemoryStream ms = new MemoryStream();   //新建内存流对象
            bf.Serialize(ms, lli);        //将续传信息序列化到内存流中
            ms.Seek(0, SeekOrigin.Begin);      //将内存流中指针位置归零
            byte[] bt = ms.GetBuffer();     //从内存流中得到字节数组
            FileStream fs = new FileStream(
                fileNameAndPath + ".cfg", FileMode.Create);       //创建文件流对象
            fs.Write(bt, 0, bt.Length);     //向文件流中写入数据
            fs.Close();        //关闭流对象
        }
        #endregion
        #region  GetFile方法
        private void GetFile()
        {
            new Set().GetConfig();
            if (Set.Path.EndsWith("\\"))
                fileNameAndPath = Set.Path + fileName;
            else
                fileNameAndPath = Set.Path + "\\" + fileName;
            if (stop2)
            {
                DeleteFile();
            }
            else
            {
                FileStream fs = new FileStream(fileNameAndPath, FileMode.Create);
                byte[] buffer = new byte[2000];    //创建缓冲区域
                for (int i = 0; i < thread; i++)
                {
                    FileStream fs2 = new FileStream(string.Format(filePath + @"\" + fileName + i.ToString()), FileMode.Open);
                    int i2;
                    while ((i2 = fs2.Read(buffer, 0, buffer.Length)) > 0)
                        fs.Write(buffer, 0, i2);
                    fs2.Close();
                }
                fs.Close();
                DeleteFile();
            }
        }
        #endregion
        #region   DeleteFile
        private void DeleteFile()
        {
            foreach (var item in G_thread_Collection)
            {
                if (item.Name != Thread.CurrentThread.Name)
                    item.Abort();
            }
            if (stop2)
            {
                for (int i = 0; i < thread; i++)
                {
                    File.Delete(string.Format(filePath + @"\" + fileName + i.ToString()));
                }
                if (File.Exists(fileNameAndPath + ".cfg"))
                {
                    string ssname = string.Format(fileNameAndPath + ".cfg");
                    File.Delete(ssname);
                }
                clear();
            }
            else
            {
                for (int i = 0; i < thread; i++)
                {
                    File.Delete(string.Format(filePath + @"\" + fileName + i.ToString()));
                }
                if (File.Exists(fileNameAndPath + ".cfg"))
                {
                    string ssname = string.Format(fileNameAndPath + ".cfg");
                    File.Delete(ssname);
                }
                clear();
            }
        }
        #endregion
        #region clear
        private void clear()
        {
            stop = false;//标记下载状态为未暂停
            fileNameAndPath = string.Empty;//重置文件路径及名称
            filePath = string.Empty;//重置文件路径
            lbo = new List<bool>();//重置每条线程下载状态
            thread = 0;//重置线程数量
            lli = new List<Locations>();//重置续传信息
            complete = true;//标记当前对象，下载状态为“已完成”
        }
        #endregion
        #region   GetBool方法
        bool GetBool(string url)
        {
            List<Thread> lth = new List<Thread>();
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                Thread th = new Thread(
                    delegate ()
                    {
                        try
                        {
                            HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create(url);
                            hwr.Timeout = 3000;   //超时时间为3秒
                            HttpWebResponse hwp = (HttpWebResponse)hwr.GetResponse();
                            hwr.Abort();
                        }
                        catch
                        {
                            count++;
                        }
                    });
                th.Name = i.ToString();
                th.IsBackground = true;
                th.Start();
                lth.Add(th);
            }
            foreach (var item in lth)    //遍历线程集合
            {
                item.Join();    //顺序执行线程
            }
            return count == 0;
        }
        #endregion
    }
}
