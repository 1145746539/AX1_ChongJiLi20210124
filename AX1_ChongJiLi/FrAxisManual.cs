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

namespace AX1_ChongJiLi20201221
{
    public partial class FrAxisManual : Form
    {
        Thread HOME;
        public FrAxisManual()
        {
            InitializeComponent();
            
        }
       
        private void button4_Click(object sender, EventArgs e)
        {

        }



        private void FrManualAxis_Load(object sender, EventArgs e)
        {
            text_BigZ_ManualSpeed.Text = VarClass.ManualSpeed[0].ToString();
            text_SmallZ_ManualSpeed.Text = VarClass.ManualSpeed[1].ToString();
            text_ScanZ_ManualSpeed.Text = VarClass.ManualSpeed[2].ToString();
            text_Xaxis_ManualSpeed.Text = VarClass.ManualSpeed[3].ToString();
            text_Yaxis_ManualSpeed.Text = VarClass.ManualSpeed[4].ToString();


           
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            LB_BigZ_CurrentPulse.Text = VarClass.Pulse_Current[0].ToString();
            LB_SmallZ_CurrentPulse.Text = VarClass.Pulse_Current[1].ToString();
            LB_ScanZ_CurrentPulse.Text = VarClass.Pulse_Current[2].ToString();
            LB_Xaxis_CurrentPulse.Text = VarClass.Pulse_Current[3].ToString();
            LB_Yaxis_CurrentPulse.Text = VarClass.Pulse_Current[4].ToString();








            timer1.Enabled = true;




        }



        private void BT_BigZ_JOG1_MouseDown(object sender, MouseEventArgs e)    //大Z轴JOG-
        {
            if (VarClass. Manual)
            {
                LTDMC.dmc_set_profile_unit(0, 0, 100, VarClass.ManualSpeed[0], 0.1, 0.1, 0);
                LTDMC.dmc_set_s_profile(0, 0, 0, 0);
                LTDMC.dmc_vmove(0, 0, 0);
            }
        }

        private void BT_BigZ_JOG1_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(0, 0, 0);
        }

        private void BT_BigZ_JOG2_MouseDown(object sender, MouseEventArgs e)    //大Z轴JOG+
        {
            if (VarClass.Manual)
            {
                LTDMC.dmc_set_profile_unit(0, 0, 100, VarClass.ManualSpeed[0], 0.1, 0.1, 0);
                LTDMC.dmc_set_s_profile(0, 0, 0, 0);
                LTDMC.dmc_vmove(0, 0, 1);
            }
        }

        private void BT_BigZ_JOG2_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(0, 0, 0);
        }

        private void BT_SmallZ_JOG1_MouseDown(object sender, MouseEventArgs e)    //小Z轴JOG-
        {
            if (VarClass.Manual)
            {
                LTDMC.dmc_set_profile_unit(0, 1, 200, VarClass.ManualSpeed[1], 0.1, 0.1, 0);
                LTDMC.dmc_set_s_profile(0, 1, 0, 0);
                LTDMC.dmc_vmove(0, 1, 0);
            }
        }

        private void BT_SmallZ_JOG1_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(0, 1, 0);
        }



        private void BT_SmallZ_JOG2_MouseDown(object sender, MouseEventArgs e)   //小Zz轴JOG+
        {
            if (VarClass.Manual)
            {
                LTDMC.dmc_set_profile_unit(0, 1, 100, VarClass.ManualSpeed[1], 0.1, 0.1, 0);
                LTDMC.dmc_set_s_profile(0, 1, 0, 0);
                LTDMC.dmc_vmove(0, 1, 1);
            }
        }

        private void BT_SmallZ_JOG2_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(0, 1, 0);
        }
        private void BT_scanJOG1_MouseDown(object sender, MouseEventArgs e)   //扫码Z轴JOG-
        {
            if (VarClass.Manual)
            {
                LTDMC.dmc_set_profile_unit(0, 2, 100, VarClass.ManualSpeed[2], 0.1, 0.1, 0);
                LTDMC.dmc_set_s_profile(0, 2, 0, 0);
                LTDMC.dmc_vmove(0, 2, 0);
            }
        }

        private void BT_scanJOG1_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(0, 2, 0);
        }
        private void BT_scanJOG2_MouseDown(object sender, MouseEventArgs e)    //扫码Z轴JOG+
        {
            if (VarClass.Manual)
            {
                LTDMC.dmc_set_profile_unit(0, 2, 100, VarClass.ManualSpeed[2], 0.1, 0.1, 0);
                LTDMC.dmc_set_s_profile(0, 2, 0, 0);
                LTDMC.dmc_vmove(0, 2, 1);
            }
        }

        private void BT_scanJOG2_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(0, 2, 0);
        }
        private void BT_Xaxis_JOG1_MouseDown(object sender, MouseEventArgs e)  //衡川X轴JOG-
        {
            if (VarClass.Manual)
            {
                LTDMC.dmc_set_profile_unit(0, 3, 100, VarClass.ManualSpeed[3], 0.1, 0.1, 0);
                LTDMC.dmc_set_s_profile(0, 3, 0, 0);
                LTDMC.dmc_vmove(0, 3, 0);
            }
        }

        private void BT_Xaxis_JOG1_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(0, 3, 0);
        }

        private void BT_Xaxis_JOG2_MouseDown(object sender, MouseEventArgs e)   //衡川X轴JOG+
        {
            if (VarClass.Manual)
            {
                LTDMC.dmc_set_profile_unit(0, 3, 100, VarClass.ManualSpeed[3], 0.1, 0.1, 0);
                LTDMC.dmc_set_s_profile(0, 3, 0, 0);
                LTDMC.dmc_vmove(0, 3, 1);
            }
        }

        private void BT_Xaxis_JOG2_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(0, 3, 0);
        }

        private void BT_Yaxis_JOG1_MouseDown(object sender, MouseEventArgs e)
        {
            if (VarClass.Manual)
            {
                LTDMC.dmc_set_profile_unit(0, 4, 100, VarClass.ManualSpeed[4], 0.1, 0.1, 0);
                LTDMC.dmc_set_s_profile(0, 4, 0, 0);
                LTDMC.dmc_vmove(0, 4, 0);
            }
        }

        private void BT_Yaxis_JOG1_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(0, 4, 0);
        }

        private void BT_Yaxis_JOG2_MouseDown(object sender, MouseEventArgs e)
        {
            if (VarClass.Manual)
            {
                LTDMC.dmc_set_profile_unit(0, 4, 100, VarClass.ManualSpeed[4], 0.1, 0.1, 0);
                LTDMC.dmc_set_s_profile(0, 4, 0, 0);
                LTDMC.dmc_vmove(0, 4, 1);
            }
        }

        private void BT_Yaxis_JOG2_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(0, 4, 0);
        }

        private void BT_BigZ_location_Click(object sender, EventArgs e) //大Z轴手动定位
        {
            if (VarClass.Manual)
            {
                double Location = Convert.ToDouble(text_BigZ_location.Text);
               PublicClass.AxisMove(0, VarClass.ManualSpeed[0], Location);
            }
        }

        private void BT_SmallZ_location_Click(object sender, EventArgs e)   //小Z轴手动定位
        {
            if (VarClass.Manual)
            {
                double Location = Convert.ToDouble(text_SmallZ_location.Text);
                PublicClass.AxisMove(1, VarClass.ManualSpeed[1], Location);
            }
        }

        private void BT_scan_location_Click(object sender, EventArgs e)    //扫码手动定位
        {
            if (VarClass.Manual)
            {
                double Location = Convert.ToDouble(textZ_Scan_location.Text);
               PublicClass.AxisMove(2, VarClass.ManualSpeed[2], Location);
            }
        }

        private void BT_Xaxis_location_Click(object sender, EventArgs e)
        {
            if (VarClass.Manual)
            {
                double Location = Convert.ToDouble(text_Xaxis_location.Text);
                PublicClass.AxisMove(3, VarClass.ManualSpeed[3], Location);
            }
        }

        private void BT_Yaxis_location_Click(object sender, EventArgs e)
        {
            if (VarClass.Manual)
            {
                double Location = Convert.ToDouble(text_Yaxis_location.Text);
                PublicClass.AxisMove(4, VarClass.ManualSpeed[4], Location);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VarClass.ManualSpeed[0] = Convert.ToDouble(text_BigZ_ManualSpeed.Text);
            VarClass.ManualSpeed[1] = Convert.ToDouble(text_SmallZ_ManualSpeed.Text);
            VarClass.ManualSpeed[2] = Convert.ToDouble(text_ScanZ_ManualSpeed.Text);
            VarClass.ManualSpeed[3] = Convert.ToDouble(text_Xaxis_ManualSpeed.Text);
            VarClass.ManualSpeed[4] = Convert.ToDouble(text_Yaxis_ManualSpeed.Text);

            if (MessageBox.Show("是否保存", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                PublicClass.WritePrivateProfileString("ManualSpeed", "BigZ", VarClass.ManualSpeed[0].ToString(), VarClass.iniPath);//
                PublicClass.WritePrivateProfileString("ManualSpeed", "SmallZ", VarClass.ManualSpeed[1].ToString(), VarClass.iniPath);//
                PublicClass.WritePrivateProfileString("ManualSpeed", "ScanZ", VarClass.ManualSpeed[2].ToString(), VarClass.iniPath);//
                PublicClass.WritePrivateProfileString("ManualSpeed", "Xaxis", VarClass.ManualSpeed[3].ToString(), VarClass.iniPath);//
                PublicClass.WritePrivateProfileString("ManualSpeed", "Yaxis", VarClass.ManualSpeed[4].ToString(), VarClass.iniPath);//
            }
        }

        private void FrManualAxis_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void BT_BigZ_JOG2_Click(object sender, EventArgs e)
        {

        }

        private void BT_BigZ_JOG1_Click(object sender, EventArgs e)
        {

        }
       int  Num =100;
     



        void home()

        {
         
            if (Num==0)
            Home_alone(0,0,7, VarClass.Low_Wel[0], VarClass.High_Wel[0]);
            else  if (Num == 1)
                Home_alone(0, 1, 7, VarClass.Low_Wel[1], VarClass.High_Wel[1]);
            else if (Num == 2)
                Home_alone(0, 2, 7, VarClass.Low_Wel[2], VarClass.High_Wel[2]);
            else if (Num == 3)
                Home_alone(0, 3, 13, VarClass.Low_Wel[3], VarClass.High_Wel[3]);
            else if (Num == 4)
                Home_alone(0, 4, 13, VarClass.Low_Wel[4], VarClass.High_Wel[4]);
            

        }
        void Home_alone(ushort Cardno, ushort Axis,ushort home_mode, double Low_Vel, double High_Vel)
        {
           // LTDMC.nmc_set_axis_enable(0, 0);  //使能
            ///Thread.Sleep(1000);
           PublicClass . AxisHome(Cardno,Axis, home_mode,Low_Vel, High_Vel);
            ushort homerres = 99;

            while ((LTDMC.dmc_check_done(Cardno, Axis) == 0) || (homerres != 1))    //判断是否回零完
            {
                LTDMC.dmc_get_home_result(Cardno, Axis, ref homerres);
                Application.DoEvents();

                if (VarClass.ReadIN[4])  //jiting
                {
                    return;
                }
                Thread.Sleep(10);
            }
        }

        private void BT_BigZHome_Click(object sender, EventArgs e)   //
        {
            Num = 0;
            HOME = new Thread(new ThreadStart(home));
            HOME.Start();
        }

        private void BT_SmallZHome_Click(object sender, EventArgs e)
        {
            Num = 1;
            HOME = new Thread(new ThreadStart(home));
            HOME.Start();
        }

        private void BT_ScanZHome_Click(object sender, EventArgs e)
        {
            Num = 2;
            HOME = new Thread(new ThreadStart(home));
            HOME.Start();
        }

        private void BT_XAxisHome_Click(object sender, EventArgs e)
        {
            Num = 3;
            HOME = new Thread(new ThreadStart(home));
            HOME.Start();
        }

        private void BT_YAxisHome_Click(object sender, EventArgs e)
        {
            Num = 4;
            HOME = new Thread(new ThreadStart(home));
            HOME.Start();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void BT_SmallZ_JOG2_Click(object sender, EventArgs e)
        {

        }
    }
    }
