using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net;

namespace AX1_ChongJiLi20201221
{
    public partial class Scan : Form
    {
        public Scan()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientSend1("T");
        }
      //  Socket mySocket;
        public static TcpClient[] client = new TcpClient[3];
        public static Stream[] stream = new Stream[3];
        public static Thread[] ClientThread = new Thread[3];
      
      
        


        public static void Client_initial1(string[] IP, int[] Port)
        {
            try
            {
                for (int i = 0; i < IP.Length; i++)
                {
                    IPAddress ClientIP = IPAddress.Parse(IP[i]);     //不管設置為服務器還是客服端，IP都設置將要為服務器電腦的IP

                    client[i] = new TcpClient();

                    client[i].Connect(ClientIP, Port[i]);



                    stream[i] = client[i].GetStream();

                    if(i == 0)
                    { 
                    ClientThread[0] = new Thread(new ThreadStart(ClientReceive1));
                        ClientThread[0].Start();
                    }
                    else if (i == 1)
                    { 
                        ClientThread[1] = new Thread(new ThreadStart(ClientReceive2));
                        ClientThread[1].Start();
                    }
                    else  if (i == 2)
                    { 
                        ClientThread[2] = new Thread(new ThreadStart(ClientReceive3));
                        ClientThread[2].Start();
                    }
                  

                }
              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
              
            }



        }
        public static void ClientReceive1()
        {
            try
            {
                if (client[0].Connected)
                {
                    while (true)
                    {
                        byte[] ReadStr = new byte[1024];
                        stream[0].Read(ReadStr, 0, ReadStr.Length);

                        System.Text.ASCIIEncoding CliASCII = new System.Text.ASCIIEncoding();
                        string StrMessCli = CliASCII.GetString(ReadStr);
                         VarClass.ClientString[0] = StrMessCli.Substring(0, StrMessCli.IndexOf("\0"));
                        //VarClass.ClientString[0] = StrMessCli;

                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        public static void ClientSend1(string sendStr)
        {
            ASCIIEncoding Encod = new ASCIIEncoding();
            byte[] CsendByte = Encod.GetBytes(sendStr);

            stream[0].Write(CsendByte, 0, CsendByte.Length);

        }



        public static void ClientReceive2()
        {
            try
            {
                if (client[1].Connected)
                {
                    while (true)
                    {
                        byte[] ReadStr = new byte[1024];
                        stream[1].Read(ReadStr, 0, ReadStr.Length);

                        System.Text.ASCIIEncoding CliASCII = new System.Text.ASCIIEncoding();
                        string StrMessCli = CliASCII.GetString(ReadStr);
                          VarClass.ClientString[1] = StrMessCli.Substring(0, StrMessCli.IndexOf("\0"));
                       // VarClass.ClientString[1] = StrMessCli;

                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        public static void ClientSend2(string sendStr)
        {
            ASCIIEncoding Encod = new ASCIIEncoding();
            byte[] CsendByte = Encod.GetBytes(sendStr);

            stream[1].Write(CsendByte, 0, CsendByte.Length);

        }





        public static void ClientReceive3()
        {
            try
            {
                if (client[2].Connected)
                {
                    while (true)
                    {
                        byte[] ReadStr = new byte[1024];
                        stream[2].Read(ReadStr, 0, ReadStr.Length);

                        System.Text.ASCIIEncoding CliASCII = new System.Text.ASCIIEncoding();
                        string StrMessCli = CliASCII.GetString(ReadStr);
                       VarClass.ClientString[2] = StrMessCli.Substring(0, StrMessCli.IndexOf("\0"));
                    //VarClass.ClientString[2] = StrMessCli;

                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        public static void ClientSend3(string sendStr)
        {
            ASCIIEncoding Encod = new ASCIIEncoding();
            byte[] CsendByte = Encod.GetBytes(sendStr);

            stream[2].Write(CsendByte, 0, CsendByte.Length);

        }
        private void Scan_Load(object sender, EventArgs e)
        {

            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            textBox1.Text = VarClass.ClientString[0];

            textBox2.Text = VarClass.ClientString[1];
            textBox3.Text = VarClass.ClientString[2];


            timer1.Enabled = true;
        }

        private void Scan_FormClosed(object sender, FormClosedEventArgs e)
        {

            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClientSend2("T");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClientSend3("T");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = VarClass.ClientString[0] = "";
            textBox2.Text = VarClass.ClientString[1] = "";
            textBox3.Text = VarClass.ClientString[2] = "";

            //textBox1.Text = "";

            //textBox2.Text = "";
            //textBox3.Text = "";
        }
    }
}
