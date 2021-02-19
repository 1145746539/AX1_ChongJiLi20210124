using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using csLTDMC;
using System.Threading;
using DevComponents.DotNetBar;
using System.IO;
using InpactForceToSFC;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;


namespace AX1_ChongJiLi20201221
{
    public partial class FrMain : Form
    {
        PublicClass myPublicClass;
        public FrMain()
        {
            InitializeComponent();
            myPublicClass = new PublicClass(this);
            myloadThread = new Thread(new ThreadStart(load));
            myloadThread.Start();

        }
        public FrAxisManual myFrManualAxis;
        public FrManualCylinderIO myFrManualCylinder;
        public FrAxisPara myFrAxisPara;
        public FrAxisStatusIO myFrAxisStatusIO;
        public FrAlarm myFrAlarm;
        public Scan myScan;
        public InpactForceToSFC.FrToSFC myFrToSFC;
        Thread myloadThread;
        Thread myCycleThread;


        public int SelectIndex_FileListBox = 0;
        Label[] Labels;
        Button[] BT_Position;
        GroupBox[] myGroupBox;
        ComboBox[] failure_mode_cb;

        Label[] Label_Pos;
        int start_machine = 0;
        // int HomeStep = 0;
        int tim0 = 0;
        int tim1 = 0;
        //  PublicClass myPublicClass = new PublicClass(this);

        public FrLoading myFormLoading = new FrLoading();
        private void Form1_Load(object sender, EventArgs e)
        {
            myFormLoading.Invoke((MethodInvoker)delegate
            {
                myFormLoading.progressBar1.Value = 50;

            });

            PublicClass.AxisInitialize();
            Scan.Client_initial1(VarClass.IP, VarClass.port);

            Thread.Sleep(2000);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = Screen.AllScreens[0].Bounds.Size;
            this.Location = new System.Drawing.Point(0, 0);
            PublicClass.ReadIniParameter();

            myCycleThread = new Thread(new ThreadStart(Cycle));
            myCycleThread.Start();

            VarClass.Autoing = false;
            VarClass.Homeing = false;
            VarClass.HomeFinish = false;
            VarClass.Manual = true;  //手动模式
            VarClass.HandsStart = false;

            VarClass.Pause_Flag = false;
            myFormLoading.Invoke((MethodInvoker)delegate
            {
                myFormLoading.progressBar1.Value = 100;
                myFormLoading.Close();
            });




            //  fileListBox2.Items.Clear();

            // Labels = new Label[] { Label_Pos1, Label_Pos2, Label_Pos3, Label_Pos4, Label_Pos5, Label_Pos6, Label_Pos7, Label_Pos8, Label_Pos9 };
            BT_Position = new Button[] { bt_Position1, bt_Position2, bt_Position3, bt_Position4, bt_Position5, bt_Position6, bt_Position7, bt_Position8, bt_Position9 };
            myGroupBox = new GroupBox[] { gB1, gB2, gB3, gB4, gB5, gB6, gB7, gB8, gB9 }; //groupBox数组
            failure_mode_cb = new ComboBox[] { failure_mode_cb1, failure_mode_cb2, failure_mode_cb3, failure_mode_cb4, failure_mode_cb5, failure_mode_cb6, failure_mode_cb7, failure_mode_cb8, failure_mode_cb9 }; //ComboBox数组
            Label_Pos = new Label[] { Label_Pos1, Label_Pos2, Label_Pos3, Label_Pos4, Label_Pos5, Label_Pos6, Label_Pos7, Label_Pos8, Label_Pos9 };
            for (int i = 0; i < BT_Position.Length; i++) //清空按钮text属性 && 隐藏gB
            {
                BT_Position[i].Text = "开始测试";
                BT_Position[i].BackColor = Color.Gainsboro;
                myGroupBox[i].Visible = false;
                failure_mode_cb[i].Items.Clear();
            }
            ManualUp_Button.Visible = false;
            //  BT = new Button[] { bt1, bt2, bt3, bt4, bt5, bt6, bt7, bt8, bt9 }; //button数组



            fileListBox1.Path = "D:\\ModeData\\CJModeData";

        }


        void load()
        {
            myFormLoading.ShowDialog();
        }



        void Cycle()
        {
            while (true)
            {

                for (int i = 0; i < 32; i++)   //读输入
                {
                    VarClass.ReadIN[i] = LTDMC.dmc_read_inbit(0, (ushort)(i + 8)) == 0 ? true : false;
                }

                for (int j = 0; j < 16; j++)    //读输出
                {

                    VarClass.ReadOUT[j] = LTDMC.dmc_read_outbit(0, (ushort)(j + 8)) == 0 ? true : false;
                }

                for (int i = 0; i < 5; i++)   //读轴当前位置和读取轴状态
                {
                    LTDMC.dmc_get_position_unit(0, (ushort)i, ref VarClass.Pulse_Current[i]);
                }
                PublicClass.AxisReadStatus(0, 0, ref VarClass.BigZ_AxisStatus);  //读取轴状态
                PublicClass.AxisReadStatus(0, 1, ref VarClass.SmallZ_AxisStatus);  //读取轴状态
                PublicClass.AxisReadStatus(0, 2, ref VarClass.ScanZ_AxisStatus);  //读取轴状态
                PublicClass.AxisReadStatus(0, 3, ref VarClass.XAxis_AxisStatus);  //读取轴状态
                PublicClass.AxisReadStatus(0, 4, ref VarClass.YAxis_AxisStatus);  //读取轴状态
                PublicClass.AxisAlarmChange();   //轴状态转化
                PublicClass.DoorAlarmChange();//menbaojing
                PublicClass.RasterChange();  //guangshan

                if (start_machine >= 10)   //开机不报警
                {
                    if (VarClass.BigZ_AxisAlarm[0] || VarClass.SmallZ_AxisAlarm[0] || VarClass.ScanZ_AxisAlarm[0] || VarClass.XAxis_AxisAlarm[0] || VarClass.YAxis_AxisAlarm[0] ||
                        VarClass.BigZ_AxisAlarm[1] || VarClass.SmallZ_AxisAlarm[1] || VarClass.ScanZ_AxisAlarm[1] || VarClass.XAxis_AxisAlarm[1] || VarClass.YAxis_AxisAlarm[1] ||
                          VarClass.BigZ_AxisAlarm[2] || VarClass.SmallZ_AxisAlarm[2] || VarClass.ScanZ_AxisAlarm[2] || VarClass.XAxis_AxisAlarm[2] || VarClass.YAxis_AxisAlarm[2] ||
                        VarClass.Alarm_LeftCylinder || VarClass.Alarm_RightCylinder || !VarClass.ReadIN[4] || VarClass.AlarmDoor[0] || VarClass.AlarmDoor[1] || VarClass.AlarmDoor[2] || VarClass.AlarmRaster)
                    {
                        VarClass.Alarm_Total = true;    //总报警
                        if ((VarClass.Homeing) || VarClass.Autoing)
                        {
                            VarClass.Pause_Flag = true;   //暂停
                        }
                    }
                }


                this.Invoke((MethodInvoker)delegate    //状态显示
                {
                    if (!VarClass.ReadIN[4])
                    {
                        LB_Status.Text = "急停中";
                        LB_Status.BackColor = Color.Red;
                    }
                    else if (VarClass.Alarm_Total)
                    {
                        LB_Status.Text = "报警";
                        LB_Status.BackColor = Color.Red;
                    }

                    else if (VarClass.Pause_Flag)
                    {
                        LB_Status.Text = "暂停中";
                        LB_Status.BackColor = Color.Red;
                    }
                    else if (VarClass.Autoing)
                    {
                        LB_Status.Text = "自动运行中";
                        LB_Status.BackColor = Color.Blue;
                    }
                    else if (VarClass.Homeing)
                    {
                        LB_Status.Text = "归零中";
                        LB_Status.BackColor = Color.Blue;
                    }
                    else if (VarClass.HomeFinish)
                    {
                        LB_Status.Text = "归零完";
                        LB_Status.BackColor = Color.Blue;
                    }

                    else if (VarClass.Manual)
                    {
                        LB_Status.Text = "手动模式";
                        LB_Status.BackColor = Color.Blue;
                    }



                    if (VarClass.HomeFinish)    //原点
                    {
                        pictureHome.Image = new Bitmap(VarClass.greenBmp);
                        groupBox1.Text = "原点";
                    }
                    else
                    {
                        pictureHome.Image = new Bitmap(VarClass.redBmp);
                        groupBox1.Text = "不在原点";
                    }


                });


                if (VarClass.ReadIN[2])  //暂停按钮
                {
                    if ((VarClass.Homeing) || VarClass.Autoing)
                    {
                        VarClass.Pause_Flag = true;
                    }
                }
                if (!VarClass.ReadIN[4])   //急停
                {
                    LTDMC.nmc_set_axis_disable(0, 0);
                    LTDMC.nmc_set_axis_disable(0, 1);
                    LTDMC.nmc_set_axis_disable(0, 2);
                    LTDMC.nmc_set_axis_disable(0, 3);
                    LTDMC.nmc_set_axis_disable(0, 4);
                    LTDMC.dmc_emg_stop(0);  //停轴
                    if (VarClass.Homeing)   //归零中
                    {
                        myPublicClass.myHomeThread.Abort();
                    }
                    if (VarClass.Autoing)   //自动中
                    {
                        myPublicClass.myAutoThread.Abort();
                    }


                    button2.Text = "自动";
                    button2.BackColor = Color.Gainsboro;
                    VarClass.HandsStart = false;
                    VarClass.StopState = "";
                    //  VarClass.Emergency = true;
                    VarClass.Pause_Flag = false;
                    VarClass.Autoing = false;
                    VarClass.Homeing = false;
                    VarClass.HomeFinish = false;

                    VarClass.Manual = true;  //手动模式


                }

                if (VarClass.ReadIN[0])
                {
                    PublicClass.WriteOUT(0, 9, 0);  //左手启动亮

                }
                else
                {
                    PublicClass.WriteOUT(0, 9, 1);  //左手启动灭
                }
                if (VarClass.ReadIN[1])
                {
                    PublicClass.WriteOUT(0, 10, 0);  //右手启动亮

                }
                else
                {
                    PublicClass.WriteOUT(0, 10, 1);  //左手启动灭
                }
                if ((VarClass.Homeing) || VarClass.Autoing)
                {
                    if (VarClass.Pause_Flag)
                        PublicClass.WriteOUT(0, 8, 0);//0打开
                    else
                        PublicClass.WriteOUT(0, 8, 1);//
                }
                Thread.Sleep(10);

            }



        }
        private void ToolStripMenuItemNPointCalibrate_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)   //暂停按钮
        {
            if ( VarClass.Autoing)
            {
                if ( (VarClass.StopState == ""))
                {
              VarClass.StopState= "outing";
                }
                else
                { VarClass.StopState = ""; }
            }
            else
            {
                MessageBox.Show("不在自动中，不能退出");
            }
        }


        private void 手动伺服页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myFrManualAxis == null || myFrManualAxis.IsDisposed)
            {
                myFrManualAxis = new FrAxisManual();
                myFrManualAxis.StartPosition = FormStartPosition.CenterParent;//设置子窗体在父窗体中居中
                myFrManualAxis.Show();
            }
            else
            {
                myFrManualAxis.WindowState = FormWindowState.Normal;
                myFrManualAxis.Activate();
            }
        }

        private void FrMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            DialogResult dr = MessageBoxEx.Show("确定要退出吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (dr == DialogResult.OK)
            {
            }
            else
            {
                e.Cancel = true;

            }


        }

       

        private void ToolStripMenuItemToPlc_Click(object sender, EventArgs e)
        {
            if (myFrManualCylinder == null || myFrManualCylinder.IsDisposed)
            {
                myFrManualCylinder = new FrManualCylinderIO();
                myFrManualCylinder.StartPosition = FormStartPosition.CenterParent;//设置子窗体在父窗体中居中
                myFrManualCylinder.Show();
            }
            else
            {
                myFrManualCylinder.WindowState = FormWindowState.Normal;
                myFrManualCylinder.Activate();
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (VarClass.Manual && (!VarClass.Alarm_Total) && (!VarClass.Homeing))
            {
                VarClass.Homeing = true;  //归零标志
                listBoxLog.Items.Clear();
                VarClass.Autoing = false;
                VarClass.IsScanOK = false;
                VarClass.HomeFinish = false;
                VarClass.Manual = false;
                VarClass.Pause_Flag = false;
                VarClass.TestNumber = 0;
                myPublicClass.HomeMove_();


            }
            else
            {
                MessageBox.Show("先手动或請先清除報警！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //一个卡 五个轴
            LTDMC.nmc_set_axis_enable(0, 0);
            LTDMC.nmc_set_axis_enable(0, 1);
            LTDMC.nmc_set_axis_enable(0, 2);
            LTDMC.nmc_set_axis_enable(0, 3);
            LTDMC.nmc_set_axis_enable(0, 4);

            addLog("ooo", Color.Black);
            addLog("liufeng", Color.Red);
            if ((VarClass.Pause_Flag) || VarClass.HomeFinish || VarClass.Manual)
            {
                if (VarClass.Homeing)   //归零中
                {
                    myPublicClass.myHomeThread.Abort();
                }
                if (VarClass.Autoing)   //自动中
                {
                    myPublicClass.myAutoThread.Abort();
                }



                //  VarClass.Emergency = true;

                button2.Text = "自动";
                button2.BackColor = Color.Gainsboro;
                VarClass.HandsStart = false;
                VarClass.IsScanOK = false;
                VarClass.Autoing = false;
                VarClass.Homeing = false;
                VarClass.HomeFinish = false;
                VarClass.StopState = "";
                VarClass.Manual = true;  //手动模式
                VarClass.TestNumber = 0;
                VarClass.Pause_Flag = false;
            }
            else
            { MessageBox.Show("机台运行中，不能切手动"); }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (VarClass.HomeFinish && !VarClass.Alarm_Total && !VarClass.Autoing)
            {
                VarClass.Autoing = true;
                VarClass.HomeFinish = false;
                listBoxLog.Items.Clear();
                myPublicClass.Auto_();
                button2.Text = "自动中";
                button2.BackColor = Color.Red;
            }
            else
            {
                MessageBox.Show("请先归零,清除报警，或在自动中");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            VarClass.Pause_Flag = false;   //暂停
           
        }

        private void button4_Click(object sender, EventArgs e)    //报警复位
        {

            LTDMC.nmc_set_axis_enable(0, 0);
            LTDMC.nmc_set_axis_enable(0, 1);
            LTDMC.nmc_set_axis_enable(0, 2);
            LTDMC.nmc_set_axis_enable(0, 3);
            LTDMC.nmc_set_axis_enable(0, 4);
            for (int i = 0; i < 3; i++)   //报警，正负极限
            {

                VarClass.BigZ_AxisAlarm[i] = false;
                VarClass.SmallZ_AxisAlarm[i] = false;
                VarClass.ScanZ_AxisAlarm[i] = false;
                VarClass.XAxis_AxisAlarm[i] = false;
                VarClass.YAxis_AxisAlarm[i] = false;



            }
            LTDMC.nmc_clear_axis_errcode(0, 0);   //清除报警
            LTDMC.nmc_clear_axis_errcode(0, 1);
            LTDMC.nmc_clear_axis_errcode(0, 2);
            LTDMC.nmc_clear_axis_errcode(0, 3);
            LTDMC.nmc_clear_axis_errcode(0, 4);




            VarClass.AlarmDoor[0] = false;
            VarClass.AlarmDoor[1] = false;
            VarClass.AlarmDoor[2] = false;
            VarClass.AlarmRaster = false;
            VarClass.Alarm_LeftCylinder = false;
            VarClass.Alarm_RightCylinder = false;


            VarClass.Alarm_LeftCylinder = false;
            VarClass.Alarm_RightCylinder = false;


            VarClass.Alarm_Total = false;    //总报警


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if ((VarClass.ReadIN[13] && VarClass.ReadIN[14]) || (!VarClass.ReadIN[13] && !VarClass.ReadIN[14]) ||
            (!VarClass.ReadIN[13] && VarClass.ReadOUT[6]) || (!VarClass.ReadIN[14] && VarClass.ReadOUT[7]))            //气缸一个电磁 左气缸报警
            {

                tim0++;
                if (tim0 >= 30)
                {
                    VarClass.Alarm_LeftCylinder = true;
                }

            }
            else
            {
                tim0 = 0;
            }

            if ((VarClass.ReadIN[16] && VarClass.ReadIN[17]) || (!VarClass.ReadIN[16] && !VarClass.ReadIN[17]) ||
           (!VarClass.ReadIN[16] && VarClass.ReadOUT[6]) || (!VarClass.ReadIN[17] && VarClass.ReadOUT[7]))               //右气缸报警
            {

                tim1++;
                if (tim1 >= 30)
                {
                    VarClass.Alarm_RightCylinder = true;
                }

            }
            else
            {
                tim1 = 0;
            }
            start_machine++;
            if (start_machine == 30)
            { start_machine = 30; }


            if (VarClass.TestNumber > 0)
            {
                LBCurrentImpactNum.Text = VarClass.ImpactCurrentNum[VarClass.TestNumber - 1].ToString();
                Lab_specificationsNum.Text = VarClass.Excel_specificationsNum[VarClass.TestNumber - 1];
                if (VarClass.TestEnd[VarClass.TestNumber - 1])
                {
                    
                    failure_mode_cb[VarClass.TestNumber - 1].Visible = true;
                    BT_Position[VarClass.TestNumber - 1].Text = "测试完成";
                    BT_Position[VarClass.TestNumber - 1].BackColor = Color.Gainsboro;
                  
                }
            }
            lab_barcode.Text = HttpVar.barcode;
            if(VarClass.StopState== "outing")
            {
                BT_Stop.Text = "退出中";
                BT_Stop.BackColor = Color.Red;
            }
            else if (VarClass.StopState == "outed")
            {
                BT_Stop.Text = "进入";
                BT_Stop.BackColor = Color.Green;
            }
            else if (VarClass.StopState == "")
            {
                BT_Stop.Text = "退出";
                BT_Stop.BackColor = Color.Gainsboro;
            }

            ////else
            ////{
            ////    BT_Position[VarClass.TestNum - 1].Text = "开始测试";
            ////    BT_Position[VarClass.TestNum - 1].BackColor = Color.Gainsboro;
            ////}
        }

        private void FrMain_FormClosed(object sender, FormClosedEventArgs e)
        {

            LTDMC.dmc_emg_stop(0);  //停轴


            LTDMC.nmc_set_axis_disable(0, 0);
            LTDMC.nmc_set_axis_disable(0, 1);
            LTDMC.nmc_set_axis_disable(0, 2);
            LTDMC.nmc_set_axis_disable(0, 3);
            LTDMC.nmc_set_axis_disable(0, 4);
            LTDMC.dmc_board_close();
            if (VarClass.Homeing)   //归零中
            {
                myPublicClass.myHomeThread.Abort();
            }
            if (VarClass.Autoing)   //自动中
            {
                myPublicClass.myAutoThread.Abort();
            }
            myCycleThread.Abort();
            try
            { 
            Environment.Exit(0);
            }
            catch (Exception ex)
            { }
        }

        public void addLog(string strLog, Color myColor)
        {
            if (listBoxLog.Items.Count > 500)
            {
                listBoxLog.Items.RemoveAt(0);
            }

            string strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + strLog + "\r\n";
            listBoxLog.Items.Add(strAll);
            //  listBoxLog.ForeColor = myColor;
            listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;



            File.AppendAllText(VarClass.txtPath, strAll, Encoding.UTF8);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void 伺服IO监控ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myFrAxisStatusIO == null || myFrAxisStatusIO.IsDisposed)
            {
                myFrAxisStatusIO = new FrAxisStatusIO();
                myFrAxisStatusIO.StartPosition = FormStartPosition.CenterParent;//设置子窗体在父窗体中居中
                myFrAxisStatusIO.Show();
            }
            else
            {
                myFrAxisStatusIO.WindowState = FormWindowState.Normal;
                myFrAxisStatusIO.Activate();
            }
        }

        private void 报警页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myFrAlarm == null || myFrAlarm.IsDisposed)
            {
                myFrAlarm = new FrAlarm();
                myFrAlarm.StartPosition = FormStartPosition.CenterParent;//设置子窗体在父窗体中居中
                myFrAlarm.Show();
            }
            else
            {
                myFrAlarm.WindowState = FormWindowState.Normal;
                myFrAlarm.Activate();
            }
        }

        private void 伺服参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myFrAxisPara == null || myFrAxisPara.IsDisposed)
            {
                myFrAxisPara = new FrAxisPara();
                myFrAxisPara.StartPosition = FormStartPosition.CenterParent;//设置子窗体在父窗体中居中
                myFrAxisPara.Show();
            }
            else
            {
                myFrAxisPara.WindowState = FormWindowState.Normal;
                myFrAxisPara.Activate();
            }
        }

        private void toolStripMenuItemChangeMode_Click(object sender, EventArgs e)
        {
            if (myScan == null || myScan.IsDisposed)
            {
                myScan = new Scan();
                myScan.StartPosition = FormStartPosition.CenterParent;//设置子窗体在父窗体中居中
                myScan.Show();
            }
            else
            {
                myScan.WindowState = FormWindowState.Normal;
                myScan.Activate();
            }
        }

        private void ToolStripMenuItemAuthority_Click(object sender, EventArgs e)
        {
            if (myFrToSFC == null || myFrToSFC.IsDisposed)
            {
                myFrToSFC = new FrToSFC();
                myFrToSFC.StartPosition = FormStartPosition.CenterParent;//设置子窗体在父窗体中居中
                myFrToSFC.Show();
            }
            else
            {
                myFrToSFC.WindowState = FormWindowState.Normal;
                myFrToSFC.Activate();
            }
        }

        private void bt_Position_Click(object sender, EventArgs e)
        {

            if (VarClass.Autoing)
            {
                if ((config_ComboBox.Text != "") && (type_ComboBox.Text != ""))
                {

                    Button cb = sender as Button;
                    string name = cb.Name;
                    int n = Convert.ToInt32(name.Substring(11, 1)) - 1;
                    if (!VarClass.Testing)
                    {

                        ManualUp_Button.Visible = false;
                        VarClass.Testing = true;
                        VarClass.TestNumber = n + 1;   //测试编号
                        PublicClass.ChoiceAxisPos(n);
                        cb.Text = "测试中";
                        cb.BackColor = Color.Red;

                    }
                    else
                    { MessageBox.Show("测试中"); }
                }
                else
                { MessageBox.Show("请选择config和检测类型"); }

            }
            else
            { MessageBox.Show("不在自动中"); }

        }

        // Button[] BT;
        // ComboBox[] CB; //ComboBox数组
        string[] weight_Excel = new string[9]; //Excel的重量
        string[] height_Excel = new string[9]; //Excel的高度
        string[] specifications_Excel = new string[9]; //Excel的规格值
        string[] strmeasurement_type = new string[9];  //声明检测类型
        string[] ScanPositions = new string[3];
        string strmeasurement_item = "";


        private void fileListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            SelectIndex_FileListBox = fileListBox1.SelectedIndex;
            lab_barcode.Text = "";

            Lab_uploading.Text = "";
            Lab_uploading.BackColor = Color.White;
            lab_result.Text = "";
            lab_result.BackColor = Color.White;
            type_ComboBox.Items.Clear();
            ManualUp_Button.Visible = false;
            VarClass.btShowTimes = 0;

            VarClass.TestNumber = 0;
            Lab_uploading.Text = "";
            VarClass.Testing = false;
            Lab_specificationsNum.Text = "";
            
           // config_ComboBox.Items.Clear();
            for (int i = 0; i < BT_Position.Length; i++) //清空按钮text属性 && 隐藏gB
            {
                VarClass.TestEnd[i] = false;
                VarClass.ImpactCurrentNum[i] = 0;
                BT_Position[i].Text = "开始测试";
                BT_Position[i].BackColor = Color.Gainsboro;
                myGroupBox[i].Visible = false;
                failure_mode_cb[i].Visible = false;
                failure_mode_cb[i].Items.Clear();
            }
            //foreach (Button btn in BT_Position)
            //{
            //    btn.BackColor = Color.Gainsboro;
            //    btn.Text = "开始测试";
            //}
            foreach (Label label in Label_Pos)
            {
                label.Enabled = true;
            }




            #region 读取表格的各项数据
            try
            {
                if (fileListBox1.FileName != "")
                {
                    VarClass.ExcelName = ExcelName_Lab.Text = fileListBox1.FileName.Substring(0, fileListBox1.FileName.Length - 4);

                    using (Stream stream = File.OpenRead(@"D:\ModeData\CJModeData\" + fileListBox1.FileName))//读取流
                    {
                        IWorkbook workbook = new HSSFWorkbook(stream);//创建一个工作簿指向读取的流，读取的流excel内容存在到工作簿中
                        ISheet sheet = workbook.GetSheetAt(0);//获取第0个工作表
                        ISheet sheet1 = workbook.GetSheetAt(1);//获取第1个工作表
                        for (int i = 0; i < weight_Excel.Length; i++)  //给按钮，重量，高度，规格值 赋值
                        {
                            //获取第i行，第j列的单元格
                            IRow er = sheet.GetRow(1);
                            if (sheet.GetRow(1).GetCell(5 * i + 1).ToString() != "PosComp")
                            {
                                HttpVar.measurement_position[i] = sheet.GetRow(1).GetCell(5 * i + 1).ToString();
                                Label_Pos[i].Text = "位置：" + HttpVar.measurement_position[i];

                                HttpVar.weight[i] = weight_Excel[i] = sheet.GetRow(1).GetCell(5 * i + 2).ToString();
                                HttpVar.height[i] = height_Excel[i] = sheet.GetRow(1).GetCell(5 * i + 3).ToString();
                                VarClass.Excel_specificationsNum[i] = specifications_Excel[i] =  sheet.GetRow(1).GetCell(5 * i + 4).ToString();
                                myGroupBox[i].Visible = true;
                                VarClass.btShowTimes += 1;
                            }
                            else break;
                            IRow row1 = sheet.GetRow(10 + 2 * i);    //读取测试位坐标
                            if (row1 != null && row1.GetCell(5) != null && row1.GetCell(6) != null)
                            {
                                VarClass.Excel_ScanPos_XAxis[i] = sheet.GetRow(10 + 2 * i).GetCell(2).ToString();
                                VarClass.Excel_ScanPos_YAxis[i] = sheet.GetRow(10 + 2 * i).GetCell(3).ToString();
                                VarClass.Excel_ScanPos_ZAxis[i] = sheet.GetRow(10 + 2 * i).GetCell(4).ToString();

                                VarClass.Excel_testPos_XAxis[i] = sheet.GetRow(10 + 2 * i).GetCell(5).ToString();
                                VarClass.Excel_testPos_YAxis[i] = sheet.GetRow(10 + 2 * i).GetCell(6).ToString();

                            }
                            else
                            {
                                myGroupBox[i].Visible = false;
                                MessageBox.Show((10 + 2 * i).ToString() + "行没有读到位置");
                            }
                        }

                        HttpVar.site = Lab_site.Text = sheet1.GetRow(1).GetCell(2).ToString();  //厂区
                        HttpVar.process = Lab_process.Text = sheet1.GetRow(2).GetCell(2).ToString();  //制程
                        HttpVar.station = Lab_station.Text = sheet1.GetRow(3).GetCell(2).ToString();  //工站

                        HttpVar.measurement_item = strmeasurement_item = sheet1.GetRow(5).GetCell(2).ToString();  //声明检测项目
                        HttpVar.equipment_id = Lab_equipment_id.Text = sheet1.GetRow(6).GetCell(2).ToString();  //机台编号

                        HttpVar.fixture = sheet1.GetRow(7).GetCell(2).ToString();  //检具码
                        if (sheet1.GetRow(7).GetCell(3).ToString() != null)
                            Lab_fixture.Text = HttpVar.fixture;


                        //测试点位ComboBox显示
                        for (int i = 0; i < 10; i++)
                        {
                            if (sheet1.GetRow(4).GetCell(i + 2).ToString() != "0")   //检测类型赋值
                            {
                                type_ComboBox.Items.Add(sheet1.GetRow(4).GetCell(i + 2).ToString());      // Excel上的measurement_ type
                            }
                            if (sheet1.GetRow(8).GetCell(i + 2).ToString() != "0" && sheet1.GetRow(8).GetCell(i + 2).ToString() != "")
                            {
                                for (int j = 0; j < VarClass.btShowTimes; j++)   //对应的myComboBox数
                                {
                                    failure_mode_cb[j].Items.Add(sheet1.GetRow(8).GetCell(i + 2).ToString());//失效模式赋值(截取)  Excel上的 failure_mode
                                }
                            }
                        }

                        fileListBox1.Enabled = false;
                       // type_ComboBox.SelectedIndex = 0;   //选择type_ComboBox第一行
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            #endregion
            //   SDSC.Enabled = true;

            //  LockComboBox(true);
        }

        private void saoma_Click(object sender, EventArgs e)
        {

        }

        public void TestShowFasle()
        {
            for (int i = 0; i < BT_Position.Length; i++) //清空按钮text属性 && 隐藏gB
            {
                BT_Position[i].Text = "开始测试";
                BT_Position[i].BackColor = Color.Gainsboro;
                myGroupBox[i].Visible = false;
                failure_mode_cb[i].Items.Clear();
            }


        }

        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string name = cb.Name;
            int n = Convert.ToInt32(name.Substring(15, 1)) - 1;
            if (VarClass.TestEnd[n])   //测试结束
            {


              //  ManualUp_Button.Visible = true;
                lab_result.Text = (VarClass.ImpactCurrentNum[n] >= int.Parse(VarClass.Excel_specificationsNum[n]) ? "OK" : "NG");
                if (lab_result.Text == "OK")
                    lab_result.BackColor = Color.Green;
                else
                    lab_result.BackColor = Color.Red;
                if (lab_result.Text == "NG")
                {
                    MessageBox.Show("测试结果NG，请采用手动上传.");
               ManualUp_Button.Visible = true;
                    XZGG.Enabled = true;
                    return;
                }
            }
            else
            { MessageBox.Show("当前测试未结束，不能上传"); }

            if ((n + 1) == VarClass.btShowTimes)
            {
                for (int i = 0; i < VarClass.btShowTimes; i++)
                {
                    if (VarClass.TestEnd[i])
                    { }
                    else
                    {
                        MessageBox.Show(i.ToString() + "测试未结束");
                        return;

                    }
                }








                if (MessageBox.Show("准备上传，请确认config和检测类型是否选择正确！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                     UpSFC(n+1);     //自动上传SFC
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.ToString()); }
                }




            }
            else
            { MessageBox.Show("不是最后测试按钮，不能上传"); }
        }

        public void UpSFC(int num)
        {

            http _http = new http();
            HttpVar.site = Lab_site.Text;   //读excel  site
            HttpVar.request_id = DateTime.Now.ToString("yyyyMMddHHmmssfff").ToString() + "000001";     //写死
            HttpVar.ip = "10.175.42.242";   //能改
            HttpVar.barcode = HttpVar.barcode;  //机台读取
            HttpVar.barcode_type = "SP"; //能改
            HttpVar.process = Lab_process.Text;  //读excel   process

            HttpVar.station = Lab_station.Text;   //读excel  station
            HttpVar.measurement_type = type_ComboBox.Text;  //读excel  measurement_ type  ，然后选择
            HttpVar.measurement_item = strmeasurement_item;  //读excel  measurement_ item
            HttpVar.equipment_id = Lab_equipment_id.Text;    //读excel  equipment_id
            HttpVar.fixture = Lab_fixture.Text;  //读excel   fixture
            HttpVar.fixture_version = HttpVar.fixture.Substring(HttpVar.fixture.Length - 1, 1);       //fixture的最后一位    //检具版本

            HttpVar.s_time = (DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).ToString();  //第一次冲击开始;
            HttpVar.e_time = (DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).ToString();  //最后冲击一次结束;
            HttpVar.inspector = "1/1";  //能改  
            HttpVar.config = config_ComboBox.Text;  //写到画面
            for (int i = 0; i < num; i++)
            {
                HttpVar.measurement_position[i] = HttpVar.measurement_position[i];  //读excel  位置
                HttpVar.result[i] = lab_result.Text;   //机台判断，实际冲击次数与excel规格值(次數)
                HttpVar.start_time[i] = (DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).ToString();  //开始时间;
                HttpVar.end_time[i] = (DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).ToString();  //开始时间;
                HttpVar.failure_mode[i] = failure_mode_cb[i].Text.Substring(0, failure_mode_cb[i].Text.Length - 2);  //读excel failure_mode，然后选择
                HttpVar.failuremode_code[i] = failure_mode_cb[i].Text.Substring(failure_mode_cb[i].Text.Length - 1, 1);     //failure_mode的最后一位
                HttpVar.number[i] = VarClass.ImpactCurrentNum[i].ToString();   //实际冲击次数
                HttpVar.weight[i] = weight_Excel[i];   //读excel 重量
                HttpVar.height[i] = height_Excel[i];  //读excel 高度
            }



            HttpVar.resv1 = "";   //填空
            HttpVar.resv2 = "";   //填空
            HttpVar.resv3 = "";   //填空
            _http.DataReport_Run(num);


            if (HttpVar.DataReport_recivedata_rc == "000")
            {
                VarClass.ImpactCurrentNum[VarClass.TestNumber - 1] = 0;
                VarClass.TestNumber = 0;
                Lab_uploading.Text = "上传成功";
                VarClass.Testing = false;
                lab_barcode.Text = "";
                for (int i = 0; i < BT_Position.Length; i++) //清空按钮text属性 && 隐藏gB
                {
                    BT_Position[i].Text = "开始测试";
                    BT_Position[i].BackColor = Color.Gainsboro;
                    myGroupBox[i].Visible = false;
                    failure_mode_cb[i].Items.Clear();
                }
            }
            else
            {
                Lab_uploading.Text = "上传失败";
                MessageBox.Show("自动上传失败");
            }



        }


        public void bt_PositionChange(Button myButton, int num)
        {




        }

        private void XZGG_Click(object sender, EventArgs e)
        {
            fileListBox1.Enabled = true;
            config_ComboBox.Text = "";
          
            type_ComboBox.Items.Clear();
            for (int i = 0; i < BT_Position.Length; i++) //清空按钮text属性 && 隐藏gB
            {
                VarClass.TestEnd[i] = false;
                VarClass.ImpactCurrentNum[i] = 0;
                BT_Position[i].Text = "开始测试";
                BT_Position[i].BackColor = Color.Gainsboro;
                myGroupBox[i].Visible = false;
                failure_mode_cb[i].Visible = false;
                failure_mode_cb[i].Items.Clear();
            }
        }

        private void gB1_Enter(object sender, EventArgs e)
        {

        }

        private void SDSC_Click(object sender, EventArgs e)
        {
          

                if (MessageBox.Show("准备上传，请确认config和检测类型是否选择正确！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        UpSFC(VarClass.TestNumber);     //自动上传SFCr
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.ToString()); }
                }
           
        }
    }
}
