using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using csLTDMC;

namespace AX1_ChongJiLi20201221
{
    public partial class FrManualCylinderIO : Form
    {
        public FrManualCylinderIO()
        {
            InitializeComponent();
        }

        private void FrManualCylinder_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)  //
        {
           if(!VarClass.ReadOUT[3])
            PublicClass.WriteOUT(0, 3, 0);//0打开
           else
                PublicClass.WriteOUT(0, 3, 1);//1关闭



        }

        private void button2_Click(object sender, EventArgs e)   //
        {
            if (!VarClass.ReadOUT[4])
                PublicClass.WriteOUT(0, 4, 0);//0打开
            else
                PublicClass.WriteOUT(0, 4, 1);//0打开


        }
        private void button4_Click(object sender, EventArgs e)  //左气缸出右气缸
        {
            PublicClass.WriteOUT(0, 6, 0);//0打开，1关闭
            PublicClass.WriteOUT(0, 7, 1);
        }

        private void button3_Click(object sender, EventArgs e)//左气缸出右气缸回
        {
            PublicClass.WriteOUT(0, 7, 0);//0打开，1关闭
            PublicClass.WriteOUT(0, 6, 1);
        }
  

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
          

            Watching(PictureBox0, VarClass.ReadIN[0]);
            Watching(PictureBox1, VarClass.ReadIN[1]);
            Watching(PictureBox2, VarClass.ReadIN[2]);
            Watching(PictureBox3, VarClass.ReadIN[3]);
            Watching(PictureBox4, VarClass.ReadIN[4]);
            Watching(PictureBox5, VarClass.ReadIN[5]);
            Watching(PictureBox6, VarClass.ReadIN[6]);
            Watching(PictureBox7, VarClass.ReadIN[7]);
            Watching(PictureBox8, VarClass.ReadIN[8]);
            Watching(PictureBox9, VarClass.ReadIN[9]);
            Watching(PictureBox10, VarClass.ReadIN[10]);
            Watching(PictureBox11, VarClass.ReadIN[11]);
            Watching(PictureBox12, VarClass.ReadIN[12]);
            Watching(PictureBox13, VarClass.ReadIN[13]);
            Watching(PictureBox14, VarClass.ReadIN[14]);
            Watching(PictureBox15, VarClass.ReadIN[15]);
            Watching(PictureBox16, VarClass.ReadIN[16]);
            Watching(PictureBox17, VarClass.ReadIN[17]);
            Watching(PictureBox18, VarClass.ReadIN[18]);
            Watching(PictureBox19, VarClass.ReadIN[19]);
            Watching(PictureBox20, VarClass.ReadIN[20]);
            Watching(PictureBox21, VarClass.ReadIN[21]);
            Watching(PictureBox22, VarClass.ReadIN[22]);
            Watching(PictureBox23, VarClass.ReadIN[23]);



            Watching(pictureBox_Out0, VarClass.ReadOUT[0]);
            Watching(pictureBox_Out1, VarClass.ReadOUT[1]);
            Watching(pictureBox_Out2, VarClass.ReadOUT[2]);
            Watching(pictureBox_Out3, VarClass.ReadOUT[3]);
            Watching(pictureBox_Out4, VarClass.ReadOUT[4]);
            Watching(pictureBox_Out5, VarClass.ReadOUT[5]);
            Watching(pictureBox_Out6, VarClass.ReadOUT[6]);
            Watching(pictureBox_Out7, VarClass.ReadOUT[7]);
            Watching(pictureBox_Out8, VarClass.ReadOUT[8]);
            Watching(pictureBox_Out9, VarClass.ReadOUT[9]);
            Watching(pictureBox_Out10, VarClass.ReadOUT[10]);
            timer1.Enabled = true;
        }
        private void Watching(System.Windows.Forms.PictureBox myPictureBox, bool myBoolean)
        {
            if (myBoolean)
            {
                myPictureBox.Image = new Bitmap(VarClass.GreenBmp);
            }
            else
            {
                myPictureBox.Image = new Bitmap(VarClass.RedBmp);

            }

        }
        private void FrManualCylinder_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }

       

        private void label59_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!VarClass.ReadOUT[0])
                PublicClass.WriteOUT(0, 0, 0);//0打开
            else
                PublicClass.WriteOUT(0, 0, 1);//0打开
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!VarClass.ReadOUT[1])
                PublicClass.WriteOUT(0, 1, 0);//0打开
            else
                PublicClass.WriteOUT(0, 1, 1);//0打开
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!VarClass.ReadOUT[8])
                PublicClass.WriteOUT(0, 8, 0);//0打开
            else
                PublicClass.WriteOUT(0, 8, 1);//
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!VarClass.ReadOUT[2])
                PublicClass.WriteOUT(0, 2, 0);//0打开
            else
                PublicClass.WriteOUT(0, 2, 1);//

        }
    }
}
