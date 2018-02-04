using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace FastDownload
{
    public class Download
    {
        public string downloadUrl;
        public string fileName;
        public string filePath;
        public string fileNameAndPath;
        public bool stop = false;
        public bool stop2 = false;
        public bool complete = false;
        private bool b_thread = false;
        public long fileSize;
        public int thread;
        public List<Locations> lli = new List<Locations>();
        public List<bool> lbo = new List<bool>();
        public List<Thread> G_thread_Collection = new List<Thread>();

        private AutoResetEvent are = new AutoResetEvent(true);

        public void StartLoad()
        {
            long fileLong = 0;
            try
            {
                //创建HttpWebResponse对象
                HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create(downloadUrl);
                HttpWebResponse hwp = (HttpWebResponse)hwr.GetResponse();
                fileLong = hwp.ContentLength;
                b_thread = GetBool(downloadUrl);
            }
            catch (WebException we)
            {
                //向上一层  抛出  异常
                throw new WebException("未能找打文件下载服务器或下载文件，请添加正确下载地址！");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            fileSize = fileLong;    //得到文件的长度
            int bytePerThre = (int)fileLong / thread;    //计算每条线程需要下载多少字符
            int byteLeft = (int)fileLong % thread;   //每条线程分配字节后，余出的字符
            Locations ll = new Locations(0, 0);      //新建续传信息对象
            lbo = new List<bool>();   //初始化布尔集合
            for (int i = 0; i < thread; i++)
            {
                ll.Start = i != 0 ? ll.End + 1 : ll.End;   //分配下载区间
                ll.End = i == thread - 1 ?
                    ll.End + bytePerThre + byteLeft : ll.End + bytePerThre;
                System.Threading.Thread th = new System.Threading.Thread(GetData);
                th.Name = i.ToString();
                th.IsBackground = true;          //后台线程
                th.Start(ll);             //线程开始
                lli.Add(new Locations(ll.Start, ll.End, downloadUrl, fileName, fileSize,
                    new Locations(ll.Start, ll.End)));
                ll = new Locations(ll.Start, ll.End);
                G_thread_Collection.Add(th);    //
                lbo.Add(false);     //每条线程的完成状态为false
            }
            hebinfile();     //合并文件线程
        }
        #region GetData方法
        public void GetData(object l)
        {
            Locations ll = (Locations)l;
            if (!b_thread)
                are.WaitOne();
            else
                are.Set();
            HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create(downloadUrl);
            hwr.Timeout = 15000;      //下载请求超时为200秒

            //设置当前线程续传或下载任务的开始点和结束点
            hwr.AddRange(ll.Start, ll.End);
            //得到httpwebresponse对象
            HttpWebResponse hwp = (HttpWebResponse)hwr.GetResponse();
            Stream ss = hwp.GetResponseStream();
            new Set().GetConfig();    //文件下载的缓冲区
            byte[] buffer = new byte[Convert.ToInt32(Set.NetValue) * 8];
            FileStream fs = new FileStream(string.Format(filePath + @"\" + fileName
                + Thread.CurrentThread.Name), FileMode.Create);
            try
            {
                int i;     //每次下载有效字节数
                int nns = Convert.ToInt32(Thread.CurrentThread.Name);
                //  开始将下载的数据放入缓冲中
                while ((i = ss.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fs.Write(buffer, 0, i);      //将缓冲数据写到本地文件
                    lli[nns].Start += i;    //计算现在下载的位置，用于续传
                    while (stop)
                    {
                        Thread.Sleep(100);    //线程挂起
                    }
                    if (stop2)
                    {
                        break;
                    }
                    Thread.Sleep(10);
                }
                fs.Close();    //关闭文件流对象
                ss.Close();    //关闭网络流对象
                //记录当前线程的下载状态为已经完成
                lbo[Convert.ToInt32(Thread.CurrentThread.Name)] = true;

            }
            catch (Exception ex)
            {
                writelog(ex.Message);      //将异常信息写入日志文件
                SaveState();      //保存断点续传状态
            }
            finally
            {
                fs.Close();
                ss.Close();
                if (!b_thread)
                    are.Set();
                else
                    are.Set();
            }

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
        #region  hebinfile方法  使用多线程技术实时监控任务状态
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
        #region  writelog
        private void writelog(string s)
        {
            try
            {
                StreamWriter fs = new StreamWriter(@"c:\Download.log", true);
                fs.Write(string.Format(s + DateTime.Now.ToString("yy-MM-dd hh:mm:ss")));
                fs.Flush();
                fs.Close();
            }
            catch
            {
            }
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
            downloadUrl = string.Empty;//重置下载地址
            stop = false;//标记下载状态为未暂停
            fileName = string.Empty;//重置文件名称
            fileNameAndPath = string.Empty;//重置文件路径及名称
            filePath = string.Empty;//重置文件路径
            lbo = new List<bool>();//重置每条线程下载状态
            thread = 0;//重置线程数量
            lli = new List<Locations>();//重置续传信息
            complete = true;//标记当前对象，下载状态为“已完成”
        }
#endregion

    }
}
