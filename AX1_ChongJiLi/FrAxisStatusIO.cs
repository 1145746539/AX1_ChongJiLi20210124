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
    public partial class FrAxisStatusIO : Form
    {
        public FrAxisStatusIO()
        {
            InitializeComponent();
        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;


            Watching(PictureBigZ_Alarm, VarClass.BigZ_AxisStatus[0]);
            Watching(PictureBigZ_EL1, VarClass.BigZ_AxisStatus[1]);
            Watching(PictureBigZ_EL2, VarClass.BigZ_AxisStatus[2]);
            Watching(PictureBigZ_emergency, VarClass.BigZ_AxisStatus[3]);
            Watching(PictureBigZ_ORG, VarClass.BigZ_AxisStatus[4]);

            Watching(PictureSmallZ_Alarm, VarClass.SmallZ_AxisStatus[0]);
            Watching(PictureSmallZ_EL1, VarClass.SmallZ_AxisStatus[1]);
            Watching(PictureSmallZ_EL2, VarClass.SmallZ_AxisStatus[2]);
            Watching(PictureSmallZ_emergency, VarClass.SmallZ_AxisStatus[3]);
            Watching(PictureSmallZ_ORG, VarClass.SmallZ_AxisStatus[4]);

            Watching(PictureScanZ_Alarm, VarClass.ScanZ_AxisStatus[0]);
            Watching(PictureScanZ_EL1, VarClass.ScanZ_AxisStatus[1]);
            Watching(PictureScanZ_EL2, VarClass.ScanZ_AxisStatus[2]);
            Watching(PictureScanZ_emergency, VarClass.ScanZ_AxisStatus[3]);
            Watching(PictureScanZ_ORG, VarClass.ScanZ_AxisStatus[4]);


            Watching(PictureXAxis_Alarm, VarClass.XAxis_AxisStatus[0]);
            Watching(PictureXAxis_EL1, VarClass.XAxis_AxisStatus[1]);
            Watching(PictureXAxis_EL2, VarClass.XAxis_AxisStatus[2]);
            Watching(PictureXAxis_emergency, VarClass.XAxis_AxisStatus[3]);
            Watching(PictureXAxis_ORG, VarClass.XAxis_AxisStatus[4]);

            Watching(PictureYAxis_Alarm, VarClass.YAxis_AxisStatus[0]);
            Watching(PictureYAxis_EL1, VarClass.YAxis_AxisStatus[1]);
            Watching(PictureYAxis_EL2, VarClass.YAxis_AxisStatus[2]);
            Watching(PictureYAxis_emergency, VarClass.YAxis_AxisStatus[3]);
            Watching(PictureYAxis_ORG, VarClass.YAxis_AxisStatus[4]);
            timer1.Enabled = true;
        }

        private void FrAxisStatusIO_Load(object sender, EventArgs e)
        {
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

        private void FrAxisStatusIO_FormClosed(object sender, FormClosedEventArgs e)
        {

            timer1.Enabled = false;
        }
    }
}
