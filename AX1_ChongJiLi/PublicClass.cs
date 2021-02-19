using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using csLTDMC;
using System.Runtime.InteropServices;
using System.Threading;
using DevComponents.DotNetBar;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Drawing;
using InpactForceToSFC;
namespace AX1_ChongJiLi20201221
{
    class PublicClass
    {
        FrMain myFrMain;
        public PublicClass(FrMain mFrMain)
        {
            myFrMain = mFrMain;
        }
        static object Lock0 = "";
        static object Lock1 = "";
        static object Lock2 = "";
        static object Lock3 = "";
        public Thread myHomeThread;
        public Thread myAutoThread;

        public void HomeMove_()
        {

            //PublicClass.HomeMove();
            myHomeThread = new Thread(new ThreadStart(HomeMove));
            myHomeThread.Start();

        }
        public void Auto_()
        {

            // PublicClass.AutoMove();
            myAutoThread = new Thread(new ThreadStart(AutoMove));
            myAutoThread.Start();

        }

        public static void AxisInitialize()
        {

            short Cardnum = 0;


            Cardnum = LTDMC.dmc_board_init();
            if (Cardnum > 0 && Cardnum < 8)
            {
                //  MessageBox.Show("找到控制卡！" + Cardnum);

            }
            else
            {
                MessageBox.Show("未找到控制卡！");
                return;
            }


            Thread.Sleep(500);
            ushort err = 0;
            short res = LTDMC.dmc_soft_reset(0);
            if (res != 0)
            {
                MessageBox.Show("复位错误" + res.ToString());
            }
            Thread.Sleep(3000);
            LTDMC.nmc_set_axis_enable(0, 0);
            LTDMC.nmc_set_axis_enable(0, 1);
            LTDMC.nmc_set_axis_enable(0, 2);
            LTDMC.nmc_set_axis_enable(0, 3);
            LTDMC.nmc_set_axis_enable(0, 4);

        }
        public static void AxisMove(ushort Axis, double Speed, double location)     //绝对定位
        {
            lock (Lock1)
            {
                LTDMC.dmc_set_profile_unit(0, Axis, 100, Speed, 0.1, 0.1, 0);
                LTDMC.dmc_set_s_profile
                    (0, Axis, 0, 0);
                LTDMC.dmc_pmove_unit(0, Axis, location, 1);
            }

        }

        public static void AxisJogMove(ushort Axis, double Speed, ushort dir)
        {
            LTDMC.dmc_set_profile_unit(0, Axis, 100, Speed, 0.1, 0.1, 0);
            LTDMC.dmc_set_s_profile(0, Axis, 0, 0);
            LTDMC.dmc_vmove(0, Axis, dir);

        }

        public static void AxisStop(ushort CardNo, ushort Axis, ushort Stop_mode)
        {
            lock (Lock2)
            {
                LTDMC.dmc_stop(CardNo, Axis, Stop_mode);
            }
        }
        public static void AxisHome(ushort CardNo, ushort Axis, ushort home_mode, double Low_Vel, double High_Vel)
        {
            lock (Lock3)
            {
                LTDMC.nmc_set_home_profile(CardNo, Axis, home_mode, Low_Vel, High_Vel, 0.1, 0.1, 0);   //轴归零
                LTDMC.nmc_home_move(CardNo, Axis);
            }

        }
        public static void AxisReadStatus(ushort CardNo, ushort Axis, ref bool[] AxisStatusSignal)
        {



            //0:ALM:伺服報警,on报警为1
            //1:EL+:正硬限位，正极限on为1
            //2:EL_:负硬限位，负极限on为1
            //3:EMG:急停信号,急停on为1
            //4:ORG:原點信號，原点 on为1
            //6:SL+:軟正限位
            //7:SL-:軟負限位

            uint AxisStatusResult = LTDMC.dmc_axis_io_status(CardNo, Axis);
            for (int count = 0; count < 8; count++)
            {

                AxisStatusSignal[count] = (((AxisStatusResult >> count) & 1) == 1) ? (true) : (false);

            }

        }

        public static void AxisAlarmChange()
        {
            for (int i = 0; i < 3; i++)   //报警，正负极限
            {
                if ((i == 1) && (VarClass.Homeing))   //归零时候，正极限不报警
                {
                    VarClass.BigZ_AxisAlarm[1] = false;
                }
                else if (VarClass.BigZ_AxisStatus[i])
                {
                    VarClass.BigZ_AxisAlarm[i] = true;
                }
            }
            for (int i = 0; i < 3; i++)   //报警，正负极限
            {
                if ((i == 1) && (VarClass.Homeing))   //归零时候，正极限不报警
                {
                    VarClass.SmallZ_AxisStatus[1] = false;
                }
                else if (VarClass.SmallZ_AxisStatus[i])
                {
                    VarClass.SmallZ_AxisAlarm[i] = true;

                }
            }
            for (int i = 0; i < 3; i++)   //报警，正负极限
            {
                if ((i == 1) && (VarClass.Homeing))   //归零时候，正极限不报警
                {
                    VarClass.ScanZ_AxisStatus[1] = false;
                }
                else if (VarClass.ScanZ_AxisStatus[i])
                {
                    VarClass.ScanZ_AxisAlarm[i] = true;

                }
            }

            for (int i = 0; i < 3; i++)   //报警，正负极限
            {
                if ((i == 2) && (VarClass.Homeing))   //归零时候，正极限不报警
                {
                    VarClass.XAxis_AxisStatus[2] = false;
                }
                else if (VarClass.XAxis_AxisStatus[i])
                {
                    VarClass.XAxis_AxisAlarm[i] = true;

                }
            }
            for (int i = 0; i < 3; i++)   //报警，正负极限
            {
                if ((i == 2) && (VarClass.Homeing))   //归零时候，正极限不报警
                {
                    VarClass.YAxis_AxisStatus[2] = false;
                }
                else if (VarClass.XAxis_AxisStatus[i])
                {
                    VarClass.YAxis_AxisAlarm[i] = true;

                }
            }


        }
        public static void RasterChange()  //光栅
        {
            if (HomeStep != 0)
            {
                if (!VarClass.ReadIN[11] || !VarClass.ReadIN[12])
                { VarClass.AlarmRaster = true; }

            }

            if (AutoStep != 0)
            {
                if (!VarClass.HandsStart)
                {
                    if (!VarClass.ReadIN[11] || !VarClass.ReadIN[12])
                    { VarClass.AlarmRaster = true; }
                }

            }
        }
        public static void DoorAlarmChange()
        {
            if (!VarClass.ReadIN[7])
            {
                VarClass.AlarmDoor[0] = true;
            }
            if (!VarClass.ReadIN[8])
            {
                VarClass.AlarmDoor[1] = true;
            }
            if (!VarClass.ReadIN[9])
            {
                VarClass.AlarmDoor[2] = true;
            }

        }
        public static void WriteOUT(ushort CardNum, ushort Bitno, ushort On_Off)
        {
            lock (Lock0)
            {
                LTDMC.dmc_write_outbit(CardNum, (ushort)(Bitno + 8), On_Off);//0打开，1关闭
            }

        }
        #region    配置函数

        [DllImportAttribute("kernel32")]
        public static extern void GetWindowsDirectory(StringBuilder WinDir, int cont);

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="strAppName">配置节点名称</param>
        /// <param name="strKeyName">配置名</param>
        /// <param name="strString">配置值</param>
        /// <param name="strFileName">配置文件名</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool WritePrivateProfileString(string strAppName,
            string strKeyName,
            string strDefault,
            string strFileName);
        /// <summary>
        /// 读取配置文件值
        /// </summary>
        /// <param name="strAppName">配置节点名称</param>
        /// <param name="strKeyName">配置名</param>
        /// <param name="strDefault">返回的默认值</param>
        /// <param name="sbReturnString">返回StringBuilder Cache对象</param>
        /// <param name="nSize">缓冲区大小</param>
        /// <param name="strFileName">配置文件名</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool GetPrivateProfileString(string strAppName,
            string strKeyName,
            string strDefault,
            StringBuilder sbReturnString,
            int nSize,
            string strFileName);
        /// <summary>
        /// 读取配置文件中的指定配置节点，并返回整型值
        /// </summary>
        /// <param name="strAppName">配置节点名称</param>
        /// <param name="strKeyName">配置名</param>
        /// <param name="nDefault">返回的默认值</param>
        /// <param name="strFileName">配置文件名</param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public static extern int GetPrivateProfileInt(string strAppName,
            string strKeyName,
            int nDefault,
            string strFileName);
        #endregion

        public static void ReadIniParameter()
        {
            string aa = "";
            StringBuilder sb1 = new StringBuilder(256);   //每次從ini中讀取256個字節
            PublicClass.GetPrivateProfileString("ManualSpeed", "BigZ", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.ManualSpeed[0] = Convert.ToDouble(aa = sb1.ToString());
            PublicClass.GetPrivateProfileString("ManualSpeed", "SmallZ", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.ManualSpeed[1] = Convert.ToDouble(aa = sb1.ToString());
            PublicClass.GetPrivateProfileString("ManualSpeed", "ScanZ", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.ManualSpeed[2] = Convert.ToDouble(aa = sb1.ToString());
            PublicClass.GetPrivateProfileString("ManualSpeed", "Xaxis", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.ManualSpeed[3] = Convert.ToDouble(aa = sb1.ToString());
            PublicClass.GetPrivateProfileString("ManualSpeed", "Yaxis", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.ManualSpeed[4] = Convert.ToDouble(aa = sb1.ToString());

            PublicClass.GetPrivateProfileString("AxisPara", "HighLocation_Big", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.BigZ_Location[0] = Convert.ToDouble(aa = sb1.ToString());
            PublicClass.GetPrivateProfileString("AxisPara", "AutoSpeed_Big", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.AutoSpeed[0] = Convert.ToDouble(aa = sb1.ToString());
            PublicClass.GetPrivateProfileString("AxisPara", "HomeSpeed_Big", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.High_Wel[0] = Convert.ToDouble(aa = sb1.ToString());

            PublicClass.GetPrivateProfileString("AxisPara", "HighLocation_Small", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.SmallZ_Location[0] = Convert.ToDouble(aa = sb1.ToString());
            PublicClass.GetPrivateProfileString("AxisPara", "LowLocation_Small", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.SmallZ_Location[1] = Convert.ToDouble(aa = sb1.ToString());
            PublicClass.GetPrivateProfileString("AxisPara", "AutoSpeed_Small", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.AutoSpeed[1] = Convert.ToDouble(aa = sb1.ToString());
            PublicClass.GetPrivateProfileString("AxisPara", "HomeSpeed_Small", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.High_Wel[1] = Convert.ToDouble(aa = sb1.ToString());

            PublicClass.GetPrivateProfileString("AxisPara", "HighLocation_Scan", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.ScanZ_Location[0] = Convert.ToDouble(aa = sb1.ToString());
            PublicClass.GetPrivateProfileString("AxisPara", "ScanLocation_Scan", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.ScanZ_Location[1] = Convert.ToDouble(aa = sb1.ToString());
            PublicClass.GetPrivateProfileString("AxisPara", "AutoSpeed_Scan", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.AutoSpeed[2] = Convert.ToDouble(aa = sb1.ToString());
            PublicClass.GetPrivateProfileString("AxisPara", "HomeSpeed_Scan", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.High_Wel[2] = Convert.ToDouble(aa = sb1.ToString());

            PublicClass.GetPrivateProfileString("AxisPara", "ChangeLocation_XAxis", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.XAxis_Location[0] = Convert.ToDouble(aa = sb1.ToString());
            PublicClass.GetPrivateProfileString("AxisPara", "ScanLocation_XAxis", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.XAxis_Location[1] = Convert.ToDouble(aa = sb1.ToString());
            PublicClass.GetPrivateProfileString("AxisPara", "Impact_Location_XAxis", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.XAxis_Location[2] = Convert.ToDouble(aa = sb1.ToString());
            PublicClass.GetPrivateProfileString("AxisPara", "AutoSpeed_XAxis", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.AutoSpeed[3] = Convert.ToDouble(aa = sb1.ToString());
            PublicClass.GetPrivateProfileString("AxisPara", "HomeSpeed_XAxis", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.High_Wel[3] = Convert.ToDouble(aa = sb1.ToString());

            PublicClass.GetPrivateProfileString("AxisPara", "ChangeLocation_YAxis", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.YAxis_Location[0] = Convert.ToDouble(aa = sb1.ToString());
            PublicClass.GetPrivateProfileString("AxisPara", "ScanLocation_YAxis", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.YAxis_Location[1] = Convert.ToDouble(aa = sb1.ToString());
            PublicClass.GetPrivateProfileString("AxisPara", "Impact_Location_YAxis", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.YAxis_Location[2] = Convert.ToDouble(aa = sb1.ToString());
            PublicClass.GetPrivateProfileString("AxisPara", "AutoSpeed_YAxis", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.AutoSpeed[4] = Convert.ToDouble(aa = sb1.ToString());
            PublicClass.GetPrivateProfileString("AxisPara", "HomeSpeed_YAxis", "", sb1, 256, VarClass.iniPath);//主机手   每次從ini中讀取256個字節
            VarClass.High_Wel[4] = Convert.ToDouble(aa = sb1.ToString());


            //PublicClass.GetPrivateProfileString("Half光源", "Half", "", sb1, 256, VariablesClass1.iniPath);//主机手   每次從ini中讀取256個字節
            //VariablesClass1.HalfLightValue = sb1.ToString();



        }

        public static void ChoiceAxisPos(int num)
        {
            VarClass.XAxis_Location[1] = Convert.ToDouble(VarClass.Excel_ScanPos_XAxis[num]);
            VarClass.YAxis_Location[1] = Convert.ToDouble(VarClass.Excel_ScanPos_YAxis[num]);
            VarClass.ScanZ_Location[1] = Convert.ToDouble(VarClass.Excel_ScanPos_ZAxis[num]);
            VarClass.XAxis_Location[2] = Convert.ToDouble(VarClass.Excel_testPos_XAxis[num]);
            VarClass.YAxis_Location[2] = Convert.ToDouble(VarClass.Excel_testPos_YAxis[num]);

        }
        static int AutoStep = 0;
        static int HomeStep = 0;
        public void HomeMove()
        {

            HomeStep = 1;
            while (HomeStep != 0)
            {

                switch (HomeStep)
                {
                    case 1:
                        {
                            myFrMain.Invoke((MethodInvoker)delegate
                            {
                                myFrMain.addLog("3个Z轴归零中", Color.Black);
                            });
                        Home1:
                            PublicClass.AxisHome(0, 0, 7, VarClass.Low_Wel[0], VarClass.High_Wel[0]); //大Z轴归零
                            PublicClass.AxisHome(0, 1, 7, VarClass.Low_Wel[1], VarClass.High_Wel[1]);   //小Z轴归零
                            PublicClass.AxisHome(0, 2, 7, VarClass.Low_Wel[2], VarClass.High_Wel[2]); //扫码轴归零

                            ushort BigZstate = 99;
                            ushort SmallZstate = 99;
                            ushort ScanZstate = 99;
                            Thread.Sleep(500);
                            while ((LTDMC.dmc_check_done(0, 0) == 0) || (BigZstate != 1) || (LTDMC.dmc_check_done(0, 1) == 0) || (SmallZstate != 1) || (LTDMC.dmc_check_done(0, 2) == 0) || (ScanZstate != 1))    //判断是否回零完
                            {
                                LTDMC.dmc_get_home_result(0, 0, ref BigZstate);
                                LTDMC.dmc_get_home_result(0, 1, ref SmallZstate);
                                LTDMC.dmc_get_home_result(0, 2, ref ScanZstate);


                                while (VarClass.Pause_Flag)
                                {


                                    PublicClass.AxisStop(0, 0, 0);   //大Z轴停止
                                    PublicClass.AxisStop(0, 1, 0);   //小Z轴停止
                                    PublicClass.AxisStop(0, 2, 0);  //大Z轴停止
                                    Thread.Sleep(5);
                                    if (!VarClass.Pause_Flag)
                                    {
                                        goto Home1;
                                    }
                                }



                            }
                            myFrMain.Invoke((MethodInvoker)delegate
                            {
                                myFrMain.addLog("3个Z轴归零完成，下一步小Z轴归零", Color.Black);
                            });
                            HomeStep = 3;
                            break;

                        }






                    case 3:
                        {
                        Home3:
                            PublicClass.AxisMove(1, VarClass.AutoSpeed[1], VarClass.SmallZ_Location[1]);   //小Z轴跑低位
                            while ((LTDMC.dmc_check_done(0, 1) == 0) || (VarClass.SmallZ_Location[1] != VarClass.Pulse_Current[1]))
                            {

                                Thread.Sleep(5);

                                while (VarClass.Pause_Flag)
                                {


                                    PublicClass.AxisStop(0, 1, 0);   //小Z轴停止

                                    Thread.Sleep(5);
                                    if (!VarClass.Pause_Flag)
                                    {
                                        goto Home3;
                                    }

                                }



                            }
                            myFrMain.Invoke((MethodInvoker)delegate
                        {
                            myFrMain.addLog("小Z轴到达低位，下一步左右气缸出", Color.Black);
                        });
                            //PublicClass.WriteOUT(0, 12, 0);//左气缸出
                            //PublicClass.WriteOUT(0, 13, 1);
                            PublicClass.WriteOUT(0, 6, 0);//左右气缸出
                            PublicClass.WriteOUT(0, 7, 1);
                            while (!VarClass.ReadIN[13] || !VarClass.ReadIN[16]) //左,右气缸出限
                            {

                                Thread.Sleep(5);

                                while (VarClass.Pause_Flag)
                                {
                                    Thread.Sleep(5);
                                }
                            }
                            myFrMain.Invoke((MethodInvoker)delegate
                            {
                                myFrMain.addLog("左右气缸出完，下一步小Z轴跑位，扫码Z轴去扫码位", Color.Black);
                            });
                            HomeStep = 4;
                            break;
                        }

                    case 4:
                        {
                        Home4:
                            Thread.Sleep(500);
                            PublicClass.AxisMove(1, VarClass.AutoSpeed[1], VarClass.SmallZ_Location[0]);   //小 Z轴跑高位
                                                                                                           //  PublicClass.AxisMove(2, VarClass.AutoSpeed[2], VarClass.ScanZ_Location[1]);   //扫码Z轴到扫描位
                            while ((LTDMC.dmc_check_done(0, 1) == 0) || (VarClass.SmallZ_Location[0] != VarClass.Pulse_Current[1]))
                            {

                                Thread.Sleep(5);

                                while (VarClass.Pause_Flag)
                                {


                                    PublicClass.AxisStop(0, 1, 0);   //小Z轴停止


                                    Thread.Sleep(5);
                                    if (!VarClass.Pause_Flag)
                                    {
                                        goto Home4;
                                    }

                                }



                            }
                            myFrMain.Invoke((MethodInvoker)delegate
                            {
                                myFrMain.addLog("小Z轴到达高位；XY轴归零", Color.Black);
                            });
                            HomeStep = 5;
                            break;

                        }
                    case 5:
                        {

                        Home5:
                            Thread.Sleep(500);
                            PublicClass.AxisHome(0, 3, 13, VarClass.Low_Wel[3], VarClass.High_Wel[3]); //X轴归零
                            PublicClass.AxisHome(0, 4, 13, VarClass.Low_Wel[4], VarClass.High_Wel[4]); //Y轴归零

                            ushort XAixsState = 99;
                            ushort YAixsState = 99;
                            Thread.Sleep(500);
                            while ((LTDMC.dmc_check_done(0, 3) == 0) || (XAixsState != 1) || (LTDMC.dmc_check_done(0, 4) == 0) || (YAixsState != 1))    //判断是否回零完
                            {
                                LTDMC.dmc_get_home_result(0, 3, ref XAixsState);
                                LTDMC.dmc_get_home_result(0, 4, ref YAixsState);

                                Thread.Sleep(10);

                                while (VarClass.Pause_Flag)
                                {


                                    PublicClass.AxisStop(0, 3, 0);   //X轴停止
                                    PublicClass.AxisStop(0, 4, 0);  //Y轴停止
                                    Thread.Sleep(5);
                                    if (!VarClass.Pause_Flag)
                                    {
                                        goto Home5;
                                    }
                                }



                            }
                            myFrMain.Invoke((MethodInvoker)delegate
                            {
                                myFrMain.addLog("归零完成", Color.Black);
                            });
                            VarClass.Homeing = false;  //归零标志
                            VarClass.HomeFinish = true;

                            HomeStep = 0;
                            break;

                        }
                }
            }


        }
        public void AutoMove()
        {
            AutoStep = 1;



            while (AutoStep != 0)
            {

                switch (AutoStep)
                {
                    case 1:
                        {



                        Auto1:
                            AxisMove(3, VarClass.AutoSpeed[3], VarClass.XAxis_Location[0]);   //X轴到换料位
                            AxisMove(4, VarClass.AutoSpeed[4], VarClass.YAxis_Location[0]);    //Y轴到换料位
                            //读取下标为3.4轴的运动状态 
                            while ((LTDMC.dmc_check_done(0, 3) == 0) || (VarClass.XAxis_Location[0] != VarClass.Pulse_Current[3])
                                || (LTDMC.dmc_check_done(0, 4) == 0) || (VarClass.YAxis_Location[0] != VarClass.Pulse_Current[4]))
                            {

                                Thread.Sleep(5);
                                while (VarClass.Pause_Flag)
                                {


                                    PublicClass.AxisStop(0, 3, 0);   //XAxis停止
                                    PublicClass.AxisStop(0, 4, 0);   //YAxis轴停止

                                    Thread.Sleep(5);
                                    if (!VarClass.Pause_Flag)
                                    {
                                        goto Auto1;
                                    }

                                }

                            }
                            if (VarClass.StopState == "outing")
                            {
                                VarClass.StopState = "outed";
                                myFrMain.Invoke((MethodInvoker)delegate
                                {

                                    myFrMain.addLog("XY轴到换料位，下步点击进入按钮", Color.Black);
                                });
                            }
                            else
                            {
                                myFrMain.Invoke((MethodInvoker)delegate
                                {
                                    myFrMain.addLog("XY轴到换料位，选择测试", Color.Black);
                                });
                            }
                            ///saoma
                            AutoStep = 2;
                            break;

                        }

                    case 2:
                        {
                            while ((VarClass.TestNumber == 0) || (VarClass.StopState != "")) //开始测试
                            {
                                Thread.Sleep(5);
                                while (VarClass.Pause_Flag)
                                {
                                    Thread.Sleep(5);
                                }

                            }

                            myFrMain.Invoke((MethodInvoker)delegate
                            {
                                myFrMain.addLog("选择测试完成，下步双手启动", Color.Black);
                            });
                            while (!VarClass.ReadIN[0] || !VarClass.ReadIN[1])   //双手启动
                            {
                                VarClass.HandsStart = true;
                                Thread.Sleep(5);
                                while (VarClass.Pause_Flag)
                                {
                                    Thread.Sleep(5);
                                }

                            }
                            VarClass.HandsStart = false;
                            while (!VarClass.ReadIN[5])   //接近感应器
                            {
                                MessageBox.Show("接近没有感应到");
                                Thread.Sleep(5);
                                while (VarClass.Pause_Flag)
                                {
                                    Thread.Sleep(5);
                                }
                            }
                            myFrMain.Invoke((MethodInvoker)delegate
                            {
                                myFrMain.addLog("接近感应到，下步XY轴去扫码位", Color.Black);
                            });
                        Auto2:
                            AxisMove(3, VarClass.AutoSpeed[3], VarClass.XAxis_Location[1]);   //X轴到扫码位
                            AxisMove(4, VarClass.AutoSpeed[4], VarClass.YAxis_Location[1]);    //Y轴到扫码位
                            while ((LTDMC.dmc_check_done(0, 3) == 0) || (VarClass.XAxis_Location[1] != VarClass.Pulse_Current[3]) || (LTDMC.dmc_check_done(0, 4) == 0) || (VarClass.YAxis_Location[1] != VarClass.Pulse_Current[4]))
                            {

                                Thread.Sleep(5);
                                while (VarClass.Pause_Flag)
                                {


                                    PublicClass.AxisStop(0, 3, 0);   //XAxis停止
                                    PublicClass.AxisStop(0, 4, 0);   //YAxis轴停止

                                    Thread.Sleep(5);
                                    if (!VarClass.Pause_Flag)
                                    {
                                        goto Auto2;
                                    }

                                }



                            }
                            myFrMain.Invoke((MethodInvoker)delegate
                            {
                                myFrMain.addLog("下步XY轴到扫码位，下步扫码Z轴去扫码位", Color.Black);
                            });
                        Auto50: PublicClass.AxisMove(2, VarClass.AutoSpeed[2], VarClass.ScanZ_Location[1]);   //扫码Z轴到扫描位
                            while ((LTDMC.dmc_check_done(0, 2) == 0) || (VarClass.ScanZ_Location[1] != VarClass.Pulse_Current[2]))
                            {

                                Thread.Sleep(5);

                                while (VarClass.Pause_Flag)
                                {



                                    PublicClass.AxisStop(0, 2, 0);   //扫码Z轴停止

                                    Thread.Sleep(5);
                                    if (!VarClass.Pause_Flag)
                                    {
                                        goto Auto50;
                                    }

                                }



                            }



                            myFrMain.Invoke((MethodInvoker)delegate
                            {
                                myFrMain.addLog("扫码Z轴到扫码位，下步扫码", Color.Black);
                            });










                            while (!VarClass.IsScanOK)
                            {
                                VarClass.ClientString[1] = "";
                                // Scan.ClientSend1("T");
                                Scan.ClientSend3("T");

                                // Scan.ClientSend3("T");
                                Thread.Sleep(2000);
                                if ((VarClass.ClientString[2] != "NG") && (VarClass.ClientString[2] != ""))
                                {
                                    string ScanString = HttpVar.barcode = VarClass.ClientString[2];


                                    VarClass.IsScanOK = true;
                                    myFrMain.Invoke((MethodInvoker)delegate
                                    {
                                        myFrMain.addLog("扫码成功:" + HttpVar.barcode + "，下步XY去测料位", Color.Black);
                                    });
                                }
                                else
                                {
                                    MessageBox.Show("扫码失败");
                                    myFrMain.Invoke((MethodInvoker)delegate
                                    {
                                        myFrMain.addLog("扫码失败", Color.Black);
                                    });


                                }
                            }
                            VarClass.IsScanOK = false;
                            AutoStep = 3;
                            break;

                        }

                    case 3:
                        {
                        Auto3:
                            AxisMove(3, VarClass.AutoSpeed[3], VarClass.XAxis_Location[2]);   //X轴到测料位
                            AxisMove(4, VarClass.AutoSpeed[4], VarClass.YAxis_Location[2]);   //Y轴到测料位
                            while ((LTDMC.dmc_check_done(0, 3) == 0) || (VarClass.XAxis_Location[2] != VarClass.Pulse_Current[3]) || (LTDMC.dmc_check_done(0, 3) == 0) || (VarClass.YAxis_Location[2] != VarClass.Pulse_Current[4]))
                            {

                                Thread.Sleep(5);
                                while (VarClass.Pause_Flag)
                                {


                                    PublicClass.AxisStop(0, 3, 0);   //XAxis停止
                                    PublicClass.AxisStop(0, 4, 0);   //YAxis轴停止

                                    Thread.Sleep(5);
                                    if (!VarClass.Pause_Flag)
                                    {
                                        goto Auto3;
                                    }

                                }



                            }
                            myFrMain.Invoke((MethodInvoker)delegate
                            {
                                myFrMain.addLog("XY轴到测料位，下步大轴下", Color.Black);
                            });
                            AutoStep = 4;
                            break;

                        }

                    case 4:
                        {
                        Auto4:
                            PublicClass.AxisJogMove(0, VarClass.AutoSpeed[0], 0);    //大轴下，速度，方向

                            while (!VarClass.ReadIN[10])   //对射光纤
                            {

                                Thread.Sleep(5);
                                while (VarClass.Pause_Flag)
                                {


                                    PublicClass.AxisStop(0, 0, 0);   //XAxis停止


                                    Thread.Sleep(5);
                                    if (!VarClass.Pause_Flag)
                                    {
                                        goto Auto4;
                                    }

                                }

                            }
                            PublicClass.AxisStop(0, 0, 0);   //XAxis停止

                            myFrMain.Invoke((MethodInvoker)delegate
                            {
                                myFrMain.addLog("大轴到位，下步左右气缸回", Color.Black);
                            });

                            //PublicClass.WriteOUT(0, 12, 1);//左气缸回
                            //PublicClass.WriteOUT(0, 13, 0);

                            AutoStep = 5;
                            break;

                        }

                    case 5:
                        {

                            PublicClass.WriteOUT(0, 6, 1);//左右气缸回
                            PublicClass.WriteOUT(0, 7, 0);
                            while (!VarClass.ReadIN[14] || !VarClass.ReadIN[17]) //左,右气缸回限
                            {

                                Thread.Sleep(5);
                                while (VarClass.Pause_Flag)
                                {
                                    Thread.Sleep(5);
                                }
                            }
                            Thread.Sleep(500);
                            myFrMain.Invoke((MethodInvoker)delegate
                            {
                                myFrMain.addLog("左,右气缸到回限，下步小Z轴下", Color.Black);
                            });
                        Auto5:
                            AxisMove(1, VarClass.AutoSpeed[1], VarClass.SmallZ_Location[1]);   //小Z轴下

                            while ((LTDMC.dmc_check_done(0, 1) == 0) || (VarClass.SmallZ_Location[1] != VarClass.Pulse_Current[1]))
                            {

                                Thread.Sleep(5);
                                while (VarClass.Pause_Flag)
                                {


                                    PublicClass.AxisStop(0, 1, 0);   //XAxis停止


                                    Thread.Sleep(5);
                                    if (!VarClass.Pause_Flag)
                                    {
                                        goto Auto5;
                                    }

                                }

                            }
                            VarClass.ImpactCurrentNum[VarClass.TestNumber - 1] = VarClass.ImpactCurrentNum[VarClass.TestNumber - 1] + 1;
                            myFrMain.Invoke((MethodInvoker)delegate
                            {
                                myFrMain.addLog("小轴到位，下步左右气缸出", Color.Black);
                            });
                            AutoStep = 10;
                            break;
                        }

                    case 10:
                        {

                            //PublicClass.WriteOUT(0, 12, 0);//左气缸出
                            //PublicClass.WriteOUT(0, 13, 1);
                            PublicClass.WriteOUT(0, 6, 0);//左右气缸出
                            PublicClass.WriteOUT(0, 7, 1);
                            while (!VarClass.ReadIN[13] || !VarClass.ReadIN[16]) //左,右气缸出限
                            {

                                Thread.Sleep(5);
                                while (VarClass.Pause_Flag)
                                {
                                    Thread.Sleep(5);
                                }
                            }

                            if ((VarClass.ImpactCurrentNum[VarClass.TestNumber - 1] < Convert.ToInt32(VarClass.Excel_specificationsNum[VarClass.TestNumber - 1])) && VarClass.StopState != "outing")
                            {
                                myFrMain.Invoke((MethodInvoker)delegate
                                {
                                    myFrMain.addLog("左,右气缸到出限，下步小Z轴上", Color.Black);
                                });
                            Auto10:
                                AxisMove(1, VarClass.AutoSpeed[1], VarClass.SmallZ_Location[0]);   //小Z轴到上位
                                while ((LTDMC.dmc_check_done(0, 1) == 0) || (VarClass.SmallZ_Location[0] != VarClass.Pulse_Current[1]))
                                {
                                    while (VarClass.Pause_Flag)
                                    {



                                        PublicClass.AxisStop(0, 1, 0);   //XIAOZ轴轴停止

                                        Thread.Sleep(5);
                                        if (!VarClass.Pause_Flag)
                                        {
                                            goto Auto10;
                                        }

                                    }

                                }

                                AutoStep = 5;
                                break;
                            }
                            else
                            {
                                myFrMain.Invoke((MethodInvoker)delegate
                                {
                                    myFrMain.addLog("左,右气缸到出限，下步大小Z轴上", Color.Black);
                                });
                                //  VarClass.ImpactCurrentNum[VarClass.TestNum-1] = 0;
                                AutoStep = 6;
                                break;

                            }



                        }
                    case 6:
                        {
                        Auto6:
                            AxisMove(0, VarClass.AutoSpeed[0], VarClass.BigZ_Location[0]);   //大Z轴到上位
                            AxisMove(1, VarClass.AutoSpeed[1], VarClass.SmallZ_Location[0]);   //小Z轴到上位
                            while ((LTDMC.dmc_check_done(0, 0) == 0) || (VarClass.BigZ_Location[0] != VarClass.Pulse_Current[0]) || (LTDMC.dmc_check_done(0, 1) == 0) || (VarClass.SmallZ_Location[0] != VarClass.Pulse_Current[1]))
                            {

                                Thread.Sleep(5);
                                while (VarClass.Pause_Flag)
                                {


                                    PublicClass.AxisStop(0, 0, 0);   //大Z轴停止
                                    PublicClass.AxisStop(0, 1, 0);   //大Z轴轴停止

                                    Thread.Sleep(5);
                                    if (!VarClass.Pause_Flag)
                                    {
                                        goto Auto6;
                                    }

                                }



                            }

                            if (VarClass.StopState == "outing")
                            { }
                            else

                            {
                                HttpVar.end_time[VarClass.TestNumber - 1] = (DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).ToString();
                                VarClass.TestEnd[VarClass.TestNumber - 1] = true;//测试完成
                                VarClass.Testing = false;
                            }


                            myFrMain.Invoke((MethodInvoker)delegate
                            {
                                myFrMain.addLog("大小轴到位，下步XY轴到换料位", Color.Black);
                            });
                            AutoStep = 1;
                            break;




                        }
                }
            }
        }




    }
}
