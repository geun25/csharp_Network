using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2P_Messenger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_my_set_Click(object sender, EventArgs e)
        {
            string ip = tbox_my_ip.Text;
            int port = 0;
            if(int.TryParse(tbox_my_port.Text, out port) == false)
            {
                MessageBox.Show("포트를 잘못 입력하셨네요.");
                return;
            }
            SmsgServer sms = new SmsgServer(ip, port);
            sms.SmsgRecvEventHandler += Sms_SmsgRecvEventHandler;
            if (sms.Start() == false)
                MessageBox.Show("서버 가동 실패!");
            else
                tbox_my_ip.Enabled = tbox_my_port.Enabled = btn_my_set.Enabled = false;
        }

        private void Sms_SmsgRecvEventHandler(object sender, SmsgRecvEventArgs e)
        {
            AddMessage(string.Format($"{e.IPStr}:{e.Port} -> {e.Msg}"));
        }

        delegate void MyDele(string msg);
        private void AddMessage(string msg)
        {
            if(lbox_msg.InvokeRequired) // 크로스 스레드 문제 해결
            {
                MyDele dele = AddMessage;
                object[] objs = new object[] { msg };
                lbox_msg.BeginInvoke(dele, objs);
            }
            else
            {
                lbox_msg.Items.Add(msg);
            }
        }

        string other_ip;
        int other_port= 10300;
        private void btn_other_set_Click(object sender, EventArgs e)
        {
            other_ip = tbox_other_ip.Text;
            if(int.TryParse(tbox_other_port.Text, out other_port) == false)
            {
                MessageBox.Show("포트번호를 정수로 변환할 수 없습니다");
            }
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            SmsgClient.SendMsgAsync(other_ip, other_port, tbox_msg.Text);
            lbox_msg.Items.Add(string.Format($"{other_ip}:{other_port} <- {tbox_msg.Text}"));
            tbox_msg.Text = "";
        }
    }
}
