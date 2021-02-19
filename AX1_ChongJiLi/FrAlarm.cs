using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AX1_ChongJiLi20201221
{
    public partial class FrAlarm : Form
    {
        public FrAlarm()
        {
            InitializeComponent();
        }

        private void FrAlarm_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)   //报警，正负极限
            {

                VarClass.BigZ_AxisAlarm[i] = false;
                VarClass.SmallZ_AxisAlarm[i] = false;
                VarClass.ScanZ_AxisAlarm[i] = false;
                VarClass.XAxis_AxisAlarm[i] = false;
                VarClass.YAxis_AxisAlarm[i] = false;


            }




            VarClass.AlarmDoor[0] = false;
            VarClass.AlarmDoor[1] = false;
            VarClass.AlarmDoor[2] = false;
            VarClass.AlarmRaster = false;
            VarClass.Alarm_LeftCylinder = false;
            VarClass.Alarm_RightCylinder = false;



            VarClass.Alarm_Total = false;    //总报警
           
            timer1.Enabled = true;
        }
        string strAll;
        string str = "";

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(listBoxAlarm.Items.Count>0)
            { 
           // listBoxAlarm.Items.RemoveAt(0);
                listBoxAlarm.Items.Clear();
            }
            if (VarClass.BigZ_AxisAlarm[0])
            { str = "大Z轴报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            if (VarClass.BigZ_AxisAlarm[1])
            { str = "大Z轴正极限报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            if (VarClass.BigZ_AxisAlarm[2])
            { str = "大Z轴负极限报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            if (VarClass.SmallZ_AxisAlarm[0])
            { str = "小Z轴报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            if (VarClass.SmallZ_AxisAlarm[1])
            { str = "小Z轴正极限报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            if (VarClass.SmallZ_AxisAlarm[2])
            { str = "小Z轴负极限报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            if (VarClass.ScanZ_AxisAlarm[0])
            {
                str = "扫码Z轴报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            if (VarClass.ScanZ_AxisAlarm[1])
            {
                str = "扫码Z轴正极限报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            if (VarClass.ScanZ_AxisAlarm[2])
            {
                str = "扫码Z轴负极限报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            if (VarClass.XAxis_AxisAlarm[0])
            {
                str = "X轴报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            if (VarClass.XAxis_AxisAlarm[1])
            {
                str = "X轴正极限报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            if (VarClass.XAxis_AxisAlarm[2])
            {
                str = "X轴负极限报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            if (VarClass.YAxis_AxisAlarm[0])
            {
                str = "Y轴报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            if (VarClass.YAxis_AxisAlarm[1])
            {
                str = "Y轴正极限报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
             
                listBoxAlarm.Items.Add(strAll);
            }
            if (VarClass.YAxis_AxisAlarm[2])
            {
                str = "Y轴负极限报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            if (VarClass.Alarm_LeftCylinder)
            {
                str = "左气缸报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            if (VarClass.Alarm_RightCylinder)
            {
                str = "右气缸报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            if (!VarClass.ReadIN[4])   
            {
                str = "急停";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            if (VarClass.AlarmDoor[0])
            {
                str = "左门报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            if (VarClass.AlarmDoor[1])
            {
                str = "后门报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            if (VarClass.AlarmDoor[2])
            {
                str = "右门报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            
                   if (VarClass.AlarmRaster)
            {
                str = "光栅报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }
            if (VarClass.Alarm_Total)
            {
                str = "总报警";
                strAll = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString("000") + " -- " + str + "\r\n";
                listBoxAlarm.Items.Add(strAll);
            }

            //  listBoxLog.ForeColor = myColor;
            listBoxAlarm.SelectedIndex = listBoxAlarm.Items.Count - 1;




            timer1.Enabled = false;





        }
    }
}
