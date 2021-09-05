using EHAAALib;
using FileClient;
using FileServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace P2P_Messenger
{
    public partial class MainForm : Form
    {
        string ID
        {
            get;
            set;
        }

        string PW
        {
            get;
            set;
        }

        public MainForm(string id, string pw)
        {
            ID = id;
            PW = pw;
            InitializeComponent();
        }

        private void btn_my_set_Click(object sender, EventArgs e)
        {
            string ip = tbox_my_ip.Text;
            int port = 0;
            if (int.TryParse(tbox_my_port.Text, out port) == false)
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
            if (lbox_msg.InvokeRequired) // 크로스 스레드 문제 해결
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
        int other_port = 10300;
        private void btn_other_set_Click(object sender, EventArgs e)
        {
            other_ip = tbox_other_ip.Text;
            if (int.TryParse(tbox_other_port.Text, out other_port) == false)
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

        private void lbox_msg_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void lbox_msg_DragDrop(object sender, DragEventArgs e)
        {
            FileSendClient fsc = new FileSendClient(other_ip, other_fport);
            fsc.SendFileDataEventHandler += Fsc_SendFileDataEventHandler;
            string[] fs = e.Data.GetData(DataFormats.FileDrop) as string[];
            foreach (string f in fs)
            {
                fsc.SendAsync(f);
                string msg = string.Format($"{other_ip}:{other_fport}에게 파일 전송 시작");
                AddMessage(msg);
            }
        }

        private void Fsc_SendFileDataEventHandler(object sender, SendFileDataEventArgs e)
        {
            if (e.Remain == 0)
            {
                string msg = string.Format($"{e.FileName}파일{e.Remain}bytes 남음...");
                AddMessage(msg);
            }
        }

        private void btn_my_fset_Click(object sender, EventArgs e)
        {
            string ip = tbox_my_ip.Text;
            int port = 0;
            if (int.TryParse(tbox_my_fport.Text, out port) == false)
            {
                MessageBox.Show("포트를 잘못 입력하셨네요.");
                return;
            }

            FileRecvServ frs = new FileRecvServ(ip, port);
            frs.AcceptedEventHandler += Frs_AcceptedEventHandler;
            frs.ClosedEventHandler += Frs_ClosedEventHandler;
            frs.FileDataRecvEventHandler += Frs_FileDataRecvEventHandler;
            frs.FileLengthRecvEventHandler += Frs_FileLengthRecvEventHandler;
            frs.RecvFileNameEventHandler += Frs_RecvFileNameEventHandler;

            if (frs.Start() == false)
                MessageBox.Show("파일 수신 서버 가동 실패");
            else
            {
                tbox_my_ip.Enabled = tbox_my_fport.Enabled = false;
                btn_my_set.Enabled = false;
            }
        }

        Dictionary<string, FileStream> fsdic = new Dictionary<string, FileStream>();
        private void Frs_RecvFileNameEventHandler(object sender, RecvFileNameEventArgs e)
        {
            string fname = e.FileName;
            int index = fname.LastIndexOf(@"\");
            if (index != -1)
                fname = fname.Substring(index + 1);
            FileStream fs = File.Create(fname);
            fsdic[e.FileName] = fs;
        }

        private void Frs_FileLengthRecvEventHandler(object sender, FileLengthRecvEventArgs e)
        {
            string msg = string.Format($"{e.RemoteEndPoint.Address}:{e.RemoteEndPoint.Port}에서 파일{e.FileName}, {e.Length} 전송시작");
            AddMessage(msg);
        }

        private void Frs_FileDataRecvEventHandler(object sender, FileDataRecvEventArgs e)
        {
            FileStream fs = fsdic[e.FileName];
            fs.Write(e.Data, 0, e.Data.Length);
            if (e.RemainLength == 0)
            {
                string msg = string.Format($"{e.RemoteEndPoint.Address}:{e.RemoteEndPoint.Port}에서 파일{e.FileName} 전송 완료");
                AddMessage(msg);
                fs.Close();
            }
        }

        private void Frs_ClosedEventHandler(object sender, ClosedEventArgs e)
        {
            string msg = string.Format($"{e.IPStr}:{e.Port}파일 전송을 마치고 연결 해제");
            AddMessage(msg);
        }

        private void Frs_AcceptedEventHandler(object sender, AcceptedEventArgs e)
        {
            string msg = string.Format($"{e.IPStr}:{e.Port}파일 전송을 위해 연결");
            AddMessage(msg);
        }

        int other_fport;
        private void btn_other_fset_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbox_other_fport.Text, out other_fport) == false)
                MessageBox.Show("포트 번호를 정수로 변환할 수 없습니다.");
        }

        EHAAA Eaaa
        {
            get
            {
                return Activator.GetObject(typeof(EHAAA), "http://172.30.1.47:10800" +
                    "/AAASVC") as EHAAA;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Eaaa.KeepAlive(ID);
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            Eaaa.Logout(ID);
            timer1.Enabled = false;
            Close();
        }

        private void btn_withdraw_Click(object sender, EventArgs e)
        {
            Eaaa.Withdraw(ID, PW);
            timer1.Enabled = false;
            Close();
        }

        int sport = 10400;
        int fport = 10200;
        int bport = 10600;
        private void MainForm_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            MySSet();
            MYFSet();
            UserInfoCSServer ucbs = new UserInfoCSServer(DefAddress.ToString(), bport);
            ucbs.UserInfoEventHandler += Ucbs_UserInfoEventHandler;
            if (ucbs.Start() == false)
                MessageBox.Show("헐...;;");
            bport = ucbs.Port;
            Eaaa.KeepAlive(ID, DefAddress.ToString(), sport, fport, bport);
        }

        private void Ucbs_UserInfoEventHandler(object sender, UserInfoEventArgs e)
        {
            if (e.FPort == 0)
            {
                UserInfoEventArgs ru = null;
                foreach (UserInfoEventArgs uiea in lbox_user.Items)
                {
                    if(uiea.ID == e.ID)
                    {
                        ru = uiea;
                        break;
                    }    
                }
                if(ru != null)
                    lbox_user.Items.Remove(e);
            }
            else
                lbox_user.Items.Add(e);
        }

        IPAddress DefAddress
        {
            get
            {
                string hname = Dns.GetHostName();
                IPHostEntry ihe = Dns.GetHostEntry(hname);
                foreach(IPAddress ipaddr in ihe.AddressList)
                {
                    if (ipaddr.AddressFamily == AddressFamily.InterNetwork)
                        return ipaddr;
                }
                return IPAddress.Any;
            }

        }

        private void MYFSet()
        {         
            FileRecvServ frs = new FileRecvServ(DefAddress.ToString(), fport);
            frs.AcceptedEventHandler += Frs_AcceptedEventHandler;
            frs.ClosedEventHandler += Frs_ClosedEventHandler;
            frs.FileDataRecvEventHandler += Frs_FileDataRecvEventHandler;
            frs.FileLengthRecvEventHandler += Frs_FileLengthRecvEventHandler;
            frs.RecvFileNameEventHandler += Frs_RecvFileNameEventHandler;

            if (frs.Start() == false)
                MessageBox.Show("파일 수신 서버 가동 실패");
            else
            {
                fport = frs.Port;
            }
        }

        private void MySSet()
        {
            
            SmsgServer sms = new SmsgServer(DefAddress.ToString(), sport);
            sms.SmsgRecvEventHandler += Sms_SmsgRecvEventHandler;
            if (sms.Start() == false)
                MessageBox.Show("서버 가동 실패!");
            else
                sport = sms.Port;
        }

        private void lbox_user_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserInfoEventArgs uie = lbox_user.SelectedItem as UserInfoEventArgs;
            other_ip = uie.IPStr;
            other_port = uie.SPort;
            other_fport = uie.FPort;
        }
    }
}
