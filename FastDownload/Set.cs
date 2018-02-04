using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace FastDownload
{
    class Set
    {
        public static string Start;  //开机启动
        public static string Auto;  //自动开始未完成任务
        public static string Path;  //默认下载路径
        public static string Net;  //限制下载速度
        public static string NetValue;  //网速限制值
        public static string DClose;  //下载完毕自动关机
        public static string TClose;  //定时关机
        public static string TCloseValue;  //定时关机时间
        public static string SNotify;  //现在完成显示提示
        public static string Play;  //播放下载完成提示音
        public static string Continue;  //是否在有未完成的下载时显示继续提示
        public static string ShowFlow;  //是否显示流量监控
        public static string strNode = "SET";  // ini文件中要读取的节点
        public static string strPath = Application.StartupPath + "\\Set.ini";  //ini配置文件路径

        private const int EWX_SHUTDOWN = 0x00000001;    //关闭参数
        private const int SE_SHUTDOWN_PEIVILAGE = 0x13;   // 关机权限

        [DllImport("kernel32")]
        public static extern int GetPrivatePeofileString(string secrion, string key, string def,
            StringBuilder retval, int size, string filePath);    //读取ini文件
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string mpAppName, string mpKeyName,
            string mpDefault, string mpFileName);    //向ini文件中写入数据
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]  //定时关机
        public static extern bool ExitWindowsEx(int uFlages, int dwReversed);
        //  关闭、重启系统
        [DllImport("ntdll.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool RtlAdjustPrivilege(int htol, bool disall, bool newst, ref int len);

        ///<summary>
        ///从ini文件中读取指定节点的内容
        ///</summary>
        ///<param name = "section">ini节点
        ///</param>
        ///<param name = "key">节点下的项
        ///</param>
        ///<param name = "def">未找到内容时返回的默认值
        ///</param>
        ///<param name = "filePath">要读取的ini文件
        ///</param>
        ///<returns>读取的节点内容
        ///</returns>
        public static string GetIniFileString(string section, string key, string def, string filePath)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivatePeofileString(section, key, def, temp, 1024, filePath);
            return temp.ToString();
        }

        ///<summary>开机自动运行程序
        ///</summary>
        ///<param name = "auto">是否自动运行
        ///</param>
        ///
        public void AutoRun(string auto)
        {
            string strName = Application.ExecutablePath;  //记录可执行文件路径
            if (!System.IO.File.Exists(strName))
                return;
            string strnewName = strName.Substring(strName.LastIndexOf("\\") + 1);   //获取文件名
            RegistryKey RKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\" +
                "Run", true);   //打开开机自动运行的注册表项
            if (RKey == null)
                RKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");  // 不运行
            if (auto == "0")
                RKey.DeleteValue(strName, false);
            else
                RKey.SetValue(strnewName, strName);
        }

        public void ShutDown()
        {
            int i = 0;
            RtlAdjustPrivilege(SE_SHUTDOWN_PEIVILAGE, true, false, ref i);    //获取关机权限
            ExitWindowsEx(EWX_SHUTDOWN, 0);    //关闭计算机
        }

        public void GetConfig()
        {
            Start = GetIniFileString(strNode, "Start", "", strPath);
            Auto = GetIniFileString(strNode, "Auto", "", strPath);
            Path = GetIniFileString(strNode, "Path", "", strPath);
            string netTemp = GetIniFileString(strNode, "Net", "", strPath);
            Net = netTemp.Split(' ')[0];
            NetValue = netTemp.Split(' ')[1];
            DClose = GetIniFileString(strNode, "DClose", "", strPath);
            string closeTemp = GetIniFileString(strNode, "TClose", "", strPath);
            TClose = closeTemp.Split(' ')[0];
            TCloseValue = closeTemp.Split(' ')[1];
            SNotify = GetIniFileString(strNode, "SNotify", "", strPath);
            Play = GetIniFileString(strNode, "Play", "", strPath);
            Continue = GetIniFileString(strNode, "Continue", "", strPath);
            ShowFlow = GetIniFileString(strNode, "ShowFlow", "", strPath);
        }

        public string GetSpace(string path)
        {
            System.IO.DriveInfo[] drive = System.IO.DriveInfo.GetDrives();
            int i;
            for (i = 0; i < drive.Length; i++)   //遍历驱动器
            {
                if (path == drive[i].Name)
                {
                    break;
                }
            }
            //avaliable space 
            return (drive[i].TotalFreeSpace / 1024 / 1024 / 1024.0).ToString("0.00") + "G";
        }
    }
}
