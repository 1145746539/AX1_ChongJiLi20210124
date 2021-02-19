using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AX1_ChongJiLi20201221
{
    class VarClass
    {
        public static string myWenjianjia = Application.StartupPath + "Log";      
       // public static string mytxt = myWenjianjia + "\\yangban" + ".txt";
        public static string txtPath = myWenjianjia + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
        public static string iniPath = Application.StartupPath + "\\" + "Parameter.ini";

        public static string GreenBmp = Application.StartupPath + "\\Green.bmp";
        public static string RedBmp = Application.StartupPath + "\\Red.bmp";
        public static string greenBmp = Application.StartupPath + "\\green.bmp";
        public static string redBmp = Application.StartupPath + "\\red.bmp";

       

        public static string[] IP = new string[] { "192.168.0.10", "192.168.0.11", "192.168.0.12" };
        public static int[] port = new int[] { 5000, 5000, 5000 };
        public static string[] ClientString = new string[3] { "","",""};    //为下标


        //  public static int status =0;   //Manual手动模式，Zeroing归零中，ZeroFinish归零完，Auto自动模式，AutoRuning自动中，Emergency急停中,Pause暂停
        public static double[] ManualSpeed = new double[5];  //手动速度：分别为，大Z轴(750W),小Z轴(400),扫码Z轴，横川X轴，横川Y轴
        public static double[] AutoSpeed = new double[5];    //自动速度：分别为，大Z轴(750W),小Z轴(400),扫码Z轴，横川X轴，横川Y轴
        public static double[] Pulse_Current = new double[5];   //目前位置 
        public static double[] High_Wel = new double[5];   //归零高速 
        public static double[] Low_Wel = new double[5] {1000, 1000, 1000, 1000,1000 };   //归零高速 
        public static bool[] ReadIN = new bool[32];
        public static bool[] ReadOUT = new bool[32];
        public static double[] BigZ_Location = new double[1];   //大Z轴高位
        public static double[] SmallZ_Location = new double[2];   //0小Z轴高位，1小Z轴低位，
        public static double[] ScanZ_Location = new double[2];   //0扫码Z轴高位，1扫码Z轴扫码位，
        public static double[] XAxis_Location = new double[3];   //0换料位，1扫码位，2冲击位
        public static double[] YAxis_Location = new double[3];   //0换料位，1扫码位，2冲击位

        public static string[] Excel_specificationsNum = new string[9];   //X轴在Excel上规格次数
        public static string[] Excel_ScanPos_XAxis = new string[9];   //X轴在Excel上的9个扫码位
        public static string[] Excel_ScanPos_YAxis = new string[9];   //X轴在Excel上的9个扫码位
        public static string[] Excel_ScanPos_ZAxis = new string[9];   //X轴在Excel上的9个扫码位
        public static string[] Excel_testPos_XAxis = new string[9];   //X轴在Excel上的9个扫码位
        public static string[] Excel_testPos_YAxis = new string[9];   //X轴在Excel上的9个扫码位
      //  public static int ImpactNum = 5;//冲击次数
        public static int[] ImpactCurrentNum = new int[9];  //目前冲击次数

        public static bool[] AxisStop_Start = new bool[5];  //轴暂停过
        public static bool[] BigZ_AxisStatus = new bool[8];  //轴状态
        public static bool[] SmallZ_AxisStatus = new bool[8];  //轴状态
        public static bool[] ScanZ_AxisStatus = new bool[8];  //轴状态
        public static bool[] XAxis_AxisStatus = new bool[8];  //轴状态
        public static bool[] YAxis_AxisStatus = new bool[8];  //轴状态

        public static bool[] BigZ_AxisAlarm = new bool[8];  //轴状态
        public static bool[] SmallZ_AxisAlarm = new bool[8];  //轴状态
        public static bool[] ScanZ_AxisAlarm = new bool[8];  //轴状态
        public static bool[] XAxis_AxisAlarm = new bool[8];  //轴状态
        public static bool[] YAxis_AxisAlarm = new bool[8];  //轴状态

        public static bool Manual = true;   //Manual手动模式
        public static bool Homeing = false; //Zeroing归零中
        public static bool HomeFinish = false;   //归零完
        public static bool Autoing = false;    //自动中
   
        public static bool Alarm_Total = false;   //总报警
        public static bool Pause_Flag = false;  //暂停
      //  public static bool Emergency = false;   //急停
        public static bool Alarm_LeftCylinder = false;   //左气缸报警
        public static bool Alarm_RightCylinder = false;   //左气缸报警
       

        public static bool IsScanOK = false;    //saomaOK

        public static bool[] AlarmDoor = new bool[3];
        public static bool AlarmRaster = false;   //光栅
        public static bool Testing = false;  //测试中
        public static int TestNumber =0;    //测试编号
        public static bool[] TestEnd = new bool[9];    //测试完成
        public static string ExcelName = "";   //Excel名字
        public static int btShowTimes = 0;
        public static bool HandsStart = false;   //dengdai shuangshpouqidong
        public static string StopState = "";   //outing,outed退出中，退出完停止冲击，品管看料

    }

    //public  enum  Status : int //Manual手动模式，Zeroing归零中，ZeroFinish归零完，Auto自动模式，AutoRuning自动中，Emergency急停中
    //{
    //    Manual=0,
    //    Zeroing=1,
    //    ZeroFinish=2,
    //    Auto=3,
    //    AutoRuning=4,
    //    Emergency=5,
    //        Pause=6
    //}
}
