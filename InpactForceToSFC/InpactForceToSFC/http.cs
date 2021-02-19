using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows.Forms;


namespace InpactForceToSFC
{
 public    class http
    {
        public static string SFCtxtPath = Application.StartupPath + "SFCLog" + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
        public static string ReportStand_URL = "http://10.197.246.99:8080/ort/ort_api/input";
        public static string httpContentType = "application/json";

        public DataReport_senddata _DataReport_senddata = new DataReport_senddata();   //数据报告
      public DataReport_recivedata DataReportrecivedata = new DataReport_recivedata();

        public static string DataReport_JsonString;
        public static string DataReport_HttpReturnString;

  JavaScriptSerializer JSC = new JavaScriptSerializer();
        public struct DataReport_senddata
        {
            public string site;
            public string request_id;
            public string ip;
            public string barcode;
            public string barcode_type;
            public string process;
            
            public string station;
            public string measurement_type;
            public string measurement_item;
            public string equipment_id;
            public string fixture;
            public string fixture_version;
          
            public string s_time;
            public string e_time;
            public string inspector;
            public string config;
            public List<data_List> data;

            public string resv1;
            public string resv2;
            public string resv3;



        }
        public class data_List
        {
            public string measurement_position;
            public string result;
            public string start_time;
            public string end_time;
            public string failure_mode;
            public string failuremode_code;
            public string number;
            public string weight;
            public string height;
        }


        public class DataReport_recivedata
        {
            public string rc;
            public string rm;
            public string request_id;


        }
        public void DataReport_Run(int n)
        {
            string JsonString = "";
            try
            {
          
                _DataReport_senddata.site = HttpVar.site; 
                _DataReport_senddata.request_id = HttpVar.request_id;
                _DataReport_senddata.ip = HttpVar.ip;


                _DataReport_senddata.barcode = HttpVar.barcode;
                _DataReport_senddata.barcode_type = HttpVar.barcode_type;
                _DataReport_senddata.process = HttpVar.process;


                _DataReport_senddata.config= HttpVar.config;
                _DataReport_senddata.station = HttpVar.station;
                _DataReport_senddata.measurement_type = HttpVar.measurement_type;
                _DataReport_senddata.measurement_item = HttpVar.measurement_item;
                _DataReport_senddata.equipment_id = HttpVar.equipment_id;
                _DataReport_senddata.fixture = HttpVar.fixture;
                _DataReport_senddata.fixture_version = HttpVar.fixture_version;
                _DataReport_senddata.inspector = HttpVar.inspector;
                _DataReport_senddata.s_time = HttpVar.s_time;
                _DataReport_senddata.e_time = HttpVar.e_time;


                _DataReport_senddata.resv1 = HttpVar.resv1;
                _DataReport_senddata.resv2 = HttpVar.resv2;
                _DataReport_senddata.resv3 = HttpVar.resv3;



                _DataReport_senddata.data = new List<data_List>();
                for (int i = 0; i < n; i++)
                {
                    data_List list = new data_List();
                    list.measurement_position = HttpVar.measurement_position[i];     //填写
                    list.result = HttpVar.result[i];
                    list.start_time = HttpVar.start_time[i];     //填写
                    list.end_time = HttpVar.end_time[i];     //填写
                    list.failure_mode = HttpVar.failure_mode[i];     //填写
                    list.failuremode_code = HttpVar.failuremode_code[i];     //填写
                    list.number = HttpVar.number[i];     //填写
                    list.weight = HttpVar.weight[i];     //填写
                    list.height = HttpVar.height[i];     //填写

                    _DataReport_senddata.data.Add(list);
                }
               

     

                JsonString = JSC.Serialize(_DataReport_senddata);
                DataReport_JsonString = JsonString;   //显示
                string ReturnString = "";






               ////// send_http(ReportStand_URL, JsonString, "Post", out ReturnString);
               ////// DataReport_HttpReturnString = ReturnString;   //显示

               ////// JavaScriptSerializer json = new JavaScriptSerializer();
               ////// DataReportrecivedata = json.Deserialize<DataReport_recivedata>(ReturnString);//将Server反馈的数据解析
               //////HttpVar. DataReport_recivedata_rc = DataReportrecivedata.rc.ToString();
               ////// HttpVar.DataReport_recivedata_rm = DataReportrecivedata.rm.ToString();
               ////// HttpVar.DataReport_recivedata_request_id = DataReportrecivedata.request_id.ToString();





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //if (!File.Exists(cytxt))
            //{
            //    File.Copy(mytxt, cytxt);


            //}

            DataReport_JsonString = "数据报告发送：" + DateTime.Now.ToString("HH:mm:ss:") + DataReport_JsonString + "\r\n";
            File.AppendAllText(SFCtxtPath, DataReport_JsonString, Encoding.UTF8);
            DataReport_HttpReturnString = "数据报告接收：" + DateTime.Now.ToString("HH:mm:ss:") + DataReport_HttpReturnString + "\r\n" + "\r\n";
            File.AppendAllText(SFCtxtPath, DataReport_HttpReturnString, Encoding.UTF8);


        }

        public void send_http(string myaddress, string mySendString, string myMethod, out string myReturnString)
        {
            HttpWebRequest myHttpWebRequest = null;
            HttpWebResponse myHttpWebResponse = null;

            try
            {
                Encoding myEncoding = Encoding.UTF8;
                myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(myaddress);
                // myHttpWebRequest.Method = "Post";
                myHttpWebRequest.Method = myMethod;
                // myHttpWebRequest.Accept = "application/json,text/javascript,*/*";
                // myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";      //SCF定义
                //  myHttpWebRequest.ContentType = "application/json";      //定义
                myHttpWebRequest.ContentType = httpContentType;
                if (myMethod == "Post")
                {
                    byte[] data = myEncoding.GetBytes(mySendString);
                    using (Stream requstStrm = myHttpWebRequest.GetRequestStream())
                    {
                        requstStrm.Write(data, 0, data.Length);
                    }
                }
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                using (Stream responseStrm = myHttpWebResponse.GetResponseStream())
                {
                    StreamReader myStreamReader = new StreamReader(responseStrm, Encoding.UTF8);
                    myReturnString = myStreamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                myReturnString = "Error~~" + ex.ToString();
            }




        }

    }
}
