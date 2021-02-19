using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InpactForceToSFC
{
    public partial class FrToSFC : Form
    {
        public FrToSFC()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            http _http = new http();
            HttpVar.site= "GL";   //读excel  site
            HttpVar.request_id = DateTime.Now.ToString("yyyyMMddHHmmssfff").ToString() + "000001";     //写死
            HttpVar.ip = "10.175.42.242";   //能改
            HttpVar.barcode = "";  //机台读取
            HttpVar.barcode_type = "SP"; //能改
            HttpVar.process = "BG ASSY";  //读excel   process

            HttpVar.station = "BracetoTrim";   //读excel  station
            HttpVar.measurement_type = "S";  //读excel  measurement_ type  ，然后选择
            HttpVar.measurement_item = "Impact";  //读excel  measurement_ item
            HttpVar.equipment_id = "B2029K004";    //读excel  equipment_id
            HttpVar.fixture = "A2-6131404-L020";  //读excel   fixture
            HttpVar.fixture_version = "C";       //fixture的最后一位

            HttpVar.s_time = (DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).ToString();  //第一次冲击开始;
            HttpVar.e_time = (DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).ToString();  //最后冲击一次结束;
            HttpVar.inspector = "1/1";  //能改
            HttpVar.config = "TEST";  //写到画面
            HttpVar.measurement_position[0] = "R-BB-外";  //读excel  位置
            HttpVar.result[0] = "";   //机台判断，实际冲击次数与excel规格值(次數)
            HttpVar.start_time[0] = (DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).ToString();  //开始时间;
            HttpVar.end_time[0] = (DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).ToString();  //开始时间;
            HttpVar.failure_mode[0] = "100次衝擊OK";  //读excel failure_mode，然后选择
            HttpVar.failuremode_code[0] = "F";     //failure_mode的最后一位
            HttpVar.number[0] = "1";   //实际冲击次数
            HttpVar.weight[0] = "500";   //读excel 重量
            HttpVar.height[0] = "10";  //读excel 高度


            HttpVar.resv1 = "";   //填空
            HttpVar.resv2 = "";   //填空
            HttpVar.resv3 = "";   //填空
            _http. DataReport_Run(1);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;


            textBox1.Text = http.DataReport_JsonString;


            textBox2.Text = http.DataReport_HttpReturnString;

            textBox3.Text = http.ReportStand_URL;
            timer1.Enabled = true;
        }
    }
}
