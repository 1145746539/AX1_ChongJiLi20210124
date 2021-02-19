using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InpactForceToSFC
{
 public    class HttpVar
    {
        public static string site;
        public static string request_id;
        public static string ip;
        public static string barcode;
        public static string barcode_type;
        public static string process;
        public static string config;
        public static string station;
        public static string measurement_type;
        public static string measurement_item;
        public static string equipment_id;
        public static string fixture;
        public static string fixture_version;
        public static string inspector;
        public static string s_time;
        public static string e_time;

        public static string[] measurement_position = new string[9];   //一个治具9个节点
        public static string[] result = new string[9];
        public static string[] start_time = new string[9];
        public static string[] end_time = new string[9];
        public static string[] failure_mode = new string[9];
        public static string[] failuremode_code = new string[9];
        public static string[] number = new string[9];
        public static string[] weight = new string[9];
        public static string[] height = new string[9];

       


        public static string resv1;
        public static string resv2;
        public static string resv3;



        public static string DataReport_recivedata_rc;
        public static string DataReport_recivedata_rm;
        public static string DataReport_recivedata_request_id;

    }
}
