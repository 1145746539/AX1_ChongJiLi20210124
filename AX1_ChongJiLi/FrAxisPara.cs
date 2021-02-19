using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using csLTDMC;
using DevComponents.DotNetBar;
using System.IO;
using InpactForceToSFC;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace AX1_ChongJiLi20201221
{
    public partial class FrAxisPara : Form
    {
        public FrAxisPara()
        {
            InitializeComponent();
        }
        GroupBox[] myGroupBox;
        Label[] myLabel;
        Button[] myButton;
        private void FrAxisPara_Load(object sender, EventArgs e)
        {
            // fileListBox1.Path = "D:\\ModeData\\CJModeData";
            myGroupBox = new GroupBox[] { group1, group2, group3, group4, group5, group6, group7, group8, group9 };
            myLabel = new Label[] { Label_Pos1, Label_Pos2, Label_Pos3, Label_Pos4, Label_Pos5, Label_Pos6, Label_Pos7, Label_Pos8, Label_Pos9 };
            myButton = new Button[] { btSave1, btSave2, btSave3, btSave4, btSave5, btSave6, btSave7, btSave8, btSave9 };
            for (int i = 0; i < myGroupBox.Length; i++)
            { myGroupBox[i].Visible = false; }

            textHighLocation_Big.Text = VarClass.BigZ_Location[0].ToString();
            textAutoSpeed_Big.Text = VarClass.AutoSpeed[0].ToString();
            textHomeSpeed_Big.Text = VarClass.High_Wel[0].ToString();

            textHighLocation_Small.Text = VarClass.SmallZ_Location[0].ToString();
            textLowLocation_Small.Text = VarClass.SmallZ_Location[1].ToString();
            textAutoSpeed_Small.Text = VarClass.AutoSpeed[1].ToString();
            textHomeSpeed_Small.Text = VarClass.High_Wel[1].ToString();

            textHighLocation_Scan.Text = VarClass.ScanZ_Location[0].ToString();
            textScanLocation_Scan.Text = VarClass.ScanZ_Location[1].ToString();
            textAutoSpeed_Scan.Text = VarClass.AutoSpeed[2].ToString();
            textHomeSpeed_Scan.Text = VarClass.High_Wel[2].ToString();

            textChangeLocation_XAxis.Text = VarClass.XAxis_Location[0].ToString();
           textScanLocation_XAxis.Text = VarClass.XAxis_Location[1].ToString();
           textImpact_Location_XAxis.Text = VarClass.XAxis_Location[2].ToString();
            textAutoSpeed_XAxis.Text = VarClass.AutoSpeed[3].ToString();
            textHomeSpeed_XAxis.Text = VarClass.High_Wel[3].ToString();

            textChangeLocation_YAxis.Text = VarClass.YAxis_Location[0].ToString();
            textScanLocation_YAxis.Text = VarClass.YAxis_Location[1].ToString();
            textImpact_Location_YAxis.Text = VarClass.YAxis_Location[2].ToString();
            textAutoSpeed_YAxis.Text = VarClass.AutoSpeed[4].ToString();
            textHomeSpeed_YAxis.Text = VarClass.High_Wel[4].ToString();
            timer1.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)  //保存
        {
            VarClass.BigZ_Location[0] = Convert.ToDouble(textHighLocation_Big.Text);
            VarClass.AutoSpeed[0] = Convert.ToDouble(textAutoSpeed_Big.Text);
            VarClass.High_Wel[0] = Convert.ToDouble(textHomeSpeed_Big.Text);

            VarClass.SmallZ_Location[0] = Convert.ToDouble(textHighLocation_Small.Text);
            VarClass.SmallZ_Location[1] = Convert.ToDouble(textLowLocation_Small.Text);
            VarClass.AutoSpeed[1] = Convert.ToDouble(textAutoSpeed_Small.Text);
            VarClass.High_Wel[1] = Convert.ToDouble(textHomeSpeed_Small.Text);

            VarClass.ScanZ_Location[0] = Convert.ToDouble(textHighLocation_Scan.Text);
            // VarClass.ScanZ_Location[1] = Convert.ToDouble(textScanLocation_Scan.Text);
            VarClass.AutoSpeed[2] = Convert.ToDouble(textAutoSpeed_Scan.Text);
            VarClass.High_Wel[2] = Convert.ToDouble(textHomeSpeed_Scan.Text);

            VarClass.XAxis_Location[0] = Convert.ToDouble(textChangeLocation_XAxis.Text);
            //VarClass.XAxis_Location[1] = Convert.ToDouble(textScanLocation_XAxis.Text);
            //VarClass.XAxis_Location[2] = Convert.ToDouble(textImpact_Location_XAxis.Text);
            VarClass.AutoSpeed[3] = Convert.ToDouble(textAutoSpeed_XAxis.Text);
            VarClass.High_Wel[3] = Convert.ToDouble(textHomeSpeed_XAxis.Text);

            VarClass.YAxis_Location[0] = Convert.ToDouble(textChangeLocation_YAxis.Text);
            //VarClass.YAxis_Location[1] = Convert.ToDouble(textScanLocation_YAxis.Text);
            //VarClass.YAxis_Location[2] = Convert.ToDouble(textImpact_Location_YAxis.Text);
            VarClass.AutoSpeed[4] = Convert.ToDouble(textAutoSpeed_YAxis.Text);
            VarClass.High_Wel[4] = Convert.ToDouble(textHomeSpeed_YAxis.Text);


            if (MessageBox.Show("是否保存", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                PublicClass.WritePrivateProfileString("AxisPara", "HighLocation_Big", VarClass.BigZ_Location[0].ToString(), VarClass.iniPath);//
                PublicClass.WritePrivateProfileString("AxisPara", "AutoSpeed_Big", VarClass.AutoSpeed[0].ToString(), VarClass.iniPath);//
                PublicClass.WritePrivateProfileString("AxisPara", "HomeSpeed_Big", VarClass.High_Wel[0].ToString(), VarClass.iniPath);//

                PublicClass.WritePrivateProfileString("AxisPara", "HighLocation_Small", VarClass.SmallZ_Location[0].ToString(), VarClass.iniPath);//
                PublicClass.WritePrivateProfileString("AxisPara", "LowLocation_Small", VarClass.SmallZ_Location[1].ToString(), VarClass.iniPath);//
                PublicClass.WritePrivateProfileString("AxisPara", "AutoSpeed_Small", VarClass.AutoSpeed[1].ToString(), VarClass.iniPath);//
                PublicClass.WritePrivateProfileString("AxisPara", "HomeSpeed_Small", VarClass.High_Wel[1].ToString(), VarClass.iniPath);//

                PublicClass.WritePrivateProfileString("AxisPara", "HighLocation_Scan", VarClass.ScanZ_Location[0].ToString(), VarClass.iniPath);//
                PublicClass.WritePrivateProfileString("AxisPara", "ScanLocation_Scan", VarClass.ScanZ_Location[1].ToString(), VarClass.iniPath);//
                PublicClass.WritePrivateProfileString("AxisPara", "AutoSpeed_Scan", VarClass.AutoSpeed[2].ToString(), VarClass.iniPath);//
                PublicClass.WritePrivateProfileString("AxisPara", "HomeSpeed_Scan", VarClass.High_Wel[2].ToString(), VarClass.iniPath);//

                PublicClass.WritePrivateProfileString("AxisPara", "ChangeLocation_XAxis", VarClass.XAxis_Location[0].ToString(), VarClass.iniPath);//
                PublicClass.WritePrivateProfileString("AxisPara", "ScanLocation_XAxis", VarClass.XAxis_Location[1].ToString(), VarClass.iniPath);//
                PublicClass.WritePrivateProfileString("AxisPara", "Impact_Location_XAxis", VarClass.XAxis_Location[2].ToString(), VarClass.iniPath);//
                PublicClass.WritePrivateProfileString("AxisPara", "AutoSpeed_XAxis", VarClass.AutoSpeed[3].ToString(), VarClass.iniPath);//
                PublicClass.WritePrivateProfileString("AxisPara", "HomeSpeed_XAxis", VarClass.High_Wel[3].ToString(), VarClass.iniPath);//

                PublicClass.WritePrivateProfileString("AxisPara", "ChangeLocation_YAxis", VarClass.YAxis_Location[0].ToString(), VarClass.iniPath);//
                PublicClass.WritePrivateProfileString("AxisPara", "ScanLocation_YAxis", VarClass.YAxis_Location[1].ToString(), VarClass.iniPath);//
                PublicClass.WritePrivateProfileString("AxisPara", "Impact_Location_YAxis", VarClass.YAxis_Location[2].ToString(), VarClass.iniPath);//
                PublicClass.WritePrivateProfileString("AxisPara", "AutoSpeed_YAxis", VarClass.AutoSpeed[4].ToString(), VarClass.iniPath);//
                PublicClass.WritePrivateProfileString("AxisPara", "HomeSpeed_YAxis", VarClass.High_Wel[4].ToString(), VarClass.iniPath);//
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBigZLocation.Text = VarClass.Pulse_Current[0].ToString();
            textSmallZLocation.Text = VarClass.Pulse_Current[1].ToString();
            textScanZLocation.Text = VarClass.Pulse_Current[2].ToString();
            textXAxisLocation.Text = VarClass.Pulse_Current[3].ToString();
            textYAxisLocation.Text = VarClass.Pulse_Current[4].ToString();
            ExcelName_Lab.Text = VarClass.ExcelName;
            ExcelName_Lab.BackColor = Color.Red;
            if(VarClass.TestNumber>0)
            { 
            Label_CurrentPos.Text = "位置" + VarClass.TestNumber.ToString()+ "：" + HttpVar.measurement_position[VarClass.TestNumber - 1];
                Label_CurrentPos.BackColor = Color.Red;

            }
            for (int i = 0; i < VarClass.btShowTimes; i++)
            {
                myGroupBox[i].Visible = true;
                myLabel[i].Text = "位置"+ (i+1).ToString() + "：" + HttpVar.measurement_position[i];
            }
        }

        private void BT_BigZ_JOG1_MouseDown(object sender, MouseEventArgs e)    //大Z轴JOG-
        {
            if (VarClass.Manual)
            {
                LTDMC.dmc_set_profile_unit(0, 0, 100, VarClass.ManualSpeed[0], 0.1, 0.1, 0);
                LTDMC.dmc_set_s_profile(0, 0, 0, 0);
                LTDMC.dmc_vmove(0, 0, 0);
            }
        }

        private void BT_BigZ_JOG1_MouseUp(object sender, MouseEventArgs e)//大Z轴停
        {
            LTDMC.dmc_stop(0, 0, 0);
        }

        private void BT_BigZ_JOG2_MouseDown(object sender, MouseEventArgs e)  //大Z轴JOG+
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

        private void button3_MouseDown(object sender, MouseEventArgs e)    //小Z轴JOG-
        {
            if (VarClass.Manual)
            {
                LTDMC.dmc_set_profile_unit(0, 1, 200, VarClass.ManualSpeed[1], 0.1, 0.1, 0);
                LTDMC.dmc_set_s_profile(0, 1, 0, 0);
                LTDMC.dmc_vmove(0, 1, 0);
            }
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(0, 1, 0);
        }

        private void button2_MouseDown(object sender, MouseEventArgs e) //小Z轴JOG+
        {
            if (VarClass.Manual)
            {
                LTDMC.dmc_set_profile_unit(0, 1, 100, VarClass.ManualSpeed[1], 0.1, 0.1, 0);
                LTDMC.dmc_set_s_profile(0, 1, 0, 0);
                LTDMC.dmc_vmove(0, 1, 1);
            }
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(0, 1, 0);
        }

        private void button5_MouseDown(object sender, MouseEventArgs e)    //扫码Z轴JOG-
        {
            if (VarClass.Manual)
            {
                LTDMC.dmc_set_profile_unit(0, 2, 100, VarClass.ManualSpeed[2], 0.1, 0.1, 0);
                LTDMC.dmc_set_s_profile(0, 2, 0, 0);
                LTDMC.dmc_vmove(0, 2, 0);
            }
        }

        private void button5_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(0, 2, 0);
        }

        private void button4_MouseDown(object sender, MouseEventArgs e)  //扫码Z轴JOG+
        {
            if (VarClass.Manual)
            {
                LTDMC.dmc_set_profile_unit(0, 2, 100, VarClass.ManualSpeed[2], 0.1, 0.1, 0);
                LTDMC.dmc_set_s_profile(0, 2, 0, 0);
                LTDMC.dmc_vmove(0, 2, 1);
            }
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(0, 2, 0);
        }

        private void button7_MouseDown(object sender, MouseEventArgs e)     //X轴JOG-
        {
            if (VarClass.Manual)
            {
                LTDMC.dmc_set_profile_unit(0, 3, 100, VarClass.ManualSpeed[3], 0.1, 0.1, 0);
                LTDMC.dmc_set_s_profile(0, 3, 0, 0);
                LTDMC.dmc_vmove(0, 3, 0);
            }
        }

        private void button7_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(0, 3, 0);
        }

        private void button6_MouseDown(object sender, MouseEventArgs e)   //X轴JOG+
        {
            if (VarClass.Manual)
            {
                LTDMC.dmc_set_profile_unit(0, 3, 100, VarClass.ManualSpeed[3], 0.1, 0.1, 0);
                LTDMC.dmc_set_s_profile(0, 3, 0, 0);
                LTDMC.dmc_vmove(0, 3, 1);
            }
        }

        private void button6_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(0, 3, 0);
        }

        private void button9_MouseDown(object sender, MouseEventArgs e)   //Y轴JOG-
        {
            if (VarClass.Manual)
            {
                LTDMC.dmc_set_profile_unit(0, 4, 100, VarClass.ManualSpeed[4], 0.1, 0.1, 0);
                LTDMC.dmc_set_s_profile(0, 4, 0, 0);
                LTDMC.dmc_vmove(0, 4, 0);
            }
        }

        private void button9_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(0, 4, 0);
        }

        private void button8_MouseDown(object sender, MouseEventArgs e)  //Y轴JOG+
        {
            if (VarClass.Manual)
            {
                LTDMC.dmc_set_profile_unit(0, 4, 100, VarClass.ManualSpeed[4], 0.1, 0.1, 0);
                LTDMC.dmc_set_s_profile(0, 4, 0, 0);
                LTDMC.dmc_vmove(0, 4, 1);
            }
        }

        private void button8_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(0, 4, 0);
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否保存", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                if (textScanLocation_Scan.Text != "" || textScanLocation_XAxis.Text != "" || textImpact_Location_XAxis.Text != "" || textScanLocation_YAxis.Text != "" || textImpact_Location_YAxis.Text != "")
                {
                    VarClass.ScanZ_Location[1] = Convert.ToDouble(textScanLocation_Scan.Text);
                    VarClass.XAxis_Location[1] = Convert.ToDouble(textScanLocation_XAxis.Text);
                    VarClass.XAxis_Location[2] = Convert.ToDouble(textImpact_Location_XAxis.Text);
                    VarClass.YAxis_Location[1] = Convert.ToDouble(textScanLocation_YAxis.Text);
                    VarClass.YAxis_Location[2] = Convert.ToDouble(textImpact_Location_YAxis.Text);

                    Button bt = sender as Button;
                    string name = bt.Name;


                    int num = Convert.ToInt32(name.Substring(6, 1)) - 1;
                    VarClass.Excel_ScanPos_XAxis[num] = VarClass.XAxis_Location[1].ToString();
                    VarClass.Excel_ScanPos_YAxis[num] = VarClass.YAxis_Location[1].ToString();
                    VarClass.Excel_ScanPos_ZAxis[num] = VarClass.ScanZ_Location[1].ToString();
                    VarClass.Excel_testPos_XAxis[num] = VarClass.XAxis_Location[2].ToString();
                    VarClass.Excel_testPos_YAxis[num] = VarClass.YAxis_Location[2].ToString();
                    addExcel(num);
                }
                else
                { MessageBox.Show("请选择位置号"); }
            }
        }

        public void addExcel(int num)
        {

            int rowCell = 10 + num * 2;//讀取excel要寫入的行號
            string path = "D:\\ModeData\\CJModeData\\" + VarClass.ExcelName + ".xls";
            if (VarClass.ExcelName != "")
            {
                try
                {
                    using (Stream stream = File.OpenRead(path))//读取流
                    {
                        IWorkbook workbook = new HSSFWorkbook(stream);//创建一个工作簿指向读取的流，读取的流excel内容存在到工作簿中
                        ISheet sheet = workbook.GetSheetAt(0);//获取第0个工作表
                        IRow row = sheet.CreateRow(rowCell);//创建行
                                                            //  row.CreateCell(10, CellType.STRING).SetCellValue(datashuzu);//将数组的第i个数据写入到第rowIndex行第i个单元格
                        row.CreateCell(2, CellType.STRING).SetCellValue(VarClass.Excel_ScanPos_XAxis[num]);//将数组的第i个数据写入到第rowIndex行第i个单元格
                        row.CreateCell(3, CellType.STRING).SetCellValue(VarClass.Excel_ScanPos_YAxis[num]);//将数组的第i个数据写入到第rowIndex行第i个单元格
                        row.CreateCell(4, CellType.STRING).SetCellValue(VarClass.Excel_ScanPos_ZAxis[num]);//将数组的第i个数据写入到第rowIndex行第i个单元格
                        row.CreateCell(5, CellType.STRING).SetCellValue(VarClass.Excel_testPos_XAxis[num]);//将数组的第i个数据写入到第rowIndex行第i个单元格
                        row.CreateCell(6, CellType.STRING).SetCellValue(VarClass.Excel_testPos_YAxis[num]);//将数组的第i个数据写入到第rowIndex行第i个单元格
                        using (Stream stream1 = File.OpenWrite(path))//写入数据到excel
                        {
                            workbook.Write(stream1);//将流写入工作簿
                        }
                    }
                }
                catch (Exception ex)
                { MessageBox.Show("Excel被打开"); }
            }
            else
            { MessageBox.Show("请在主画面选择Excel"); }
            // File.WriteAllText("excel单元格.txt", (rowCell + 1).ToString());
        }

        private void Label_Pos_Click(object sender, EventArgs e)
        {
            Label bt = sender as Label;
            string name = bt.Name;


            int num = Convert.ToInt32(name.Substring(9, 1)) - 1;
            for (int i = 0; i < VarClass.btShowTimes; i++)
            {
                if(i== num)
                    myLabel[i].BackColor= Color.Red;
                else
                    myLabel[i].BackColor = Color.Gainsboro;
            }
       
            VarClass.XAxis_Location[1] = Convert.ToDouble(VarClass.Excel_ScanPos_XAxis[num]);
            VarClass.YAxis_Location[1] = Convert.ToDouble(VarClass.Excel_ScanPos_YAxis[num]);
            VarClass.ScanZ_Location[1] = Convert.ToDouble(VarClass.Excel_ScanPos_ZAxis[num]);
            VarClass.XAxis_Location[2] = Convert.ToDouble(VarClass.Excel_testPos_XAxis[num]);
            VarClass.YAxis_Location[2] = Convert.ToDouble(VarClass.Excel_testPos_YAxis[num]);
            textScanLocation_XAxis.Text = VarClass.XAxis_Location[1].ToString();
            textScanLocation_YAxis.Text = VarClass.YAxis_Location[1].ToString();
            textScanLocation_Scan.Text = VarClass.ScanZ_Location[1].ToString();

          
            textImpact_Location_XAxis.Text = VarClass.XAxis_Location[2].ToString();

           
            textImpact_Location_YAxis.Text = VarClass.YAxis_Location[2].ToString();
        }

     

   

    
    }
}
