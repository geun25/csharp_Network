using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace EHAAALib
{
    class UserInfo
    {
        internal string ID
        {
            get;
            private set;
        }

        internal DateTime LastKA
        {
            get;
            set;
        }

        internal string IPStr
        {
            get;
            set;
        }

        internal int SPort
        {
            get;
            set;
        }

        internal int FPort
        {
            get;
            set;
        }

        internal int BPort
        {
            get;
            set;
        }

        internal UserInfo(string id, string ip, int sport, int fport, int bport)
        {
            ID = id;
            IPStr = ip;
            SPort = sport;
            FPort = fport;
            BPort = bport;
        }
    }

    public class EHAAA:MarshalByRefObject
    {
        const string sfname = "member.xsl"; // 스키마 파일
        const string dfname = "member.xml"; // 데이터 파일 
        DataTable mtb = new DataTable("회원");
        Dictionary<string, UserInfo> ui_dic = new Dictionary<string, UserInfo>();
        Timer timer = null;

        public EHAAA()
        {
            Initialize();
        }

        ~EHAAA()
        {
            mtb.WriteXml(dfname);
        }

        private void Initialize()
        {
            timer = new Timer(CheckKeepAlive);
            timer.Change(0, 3000);
            if (File.Exists(sfname))
            {
                mtb.ReadXmlSchema(sfname);
                if (File.Exists(dfname))
                    mtb.ReadXml(dfname);
            }
            else
                DesignMTB();
        }

        private void DesignMTB()
        {
            DataColumn dc_id = new DataColumn("id", typeof(string));
            dc_id.Unique = true;
            dc_id.AllowDBNull = false; // 개체 무결성 보장
            mtb.Columns.Add(dc_id);

            DataColumn dc_pw = new DataColumn("pw", typeof(string));
            dc_pw.AllowDBNull = false;
            mtb.Columns.Add(dc_pw);

            DataColumn[] pkeys = new DataColumn[] { dc_id };
            mtb.PrimaryKey = pkeys;
            mtb.WriteXmlSchema(sfname);
        }

        void CheckKeepAlive(object state)
        {
            Console.Write(".");
            List<string> dlist = new List<string>();
            foreach(KeyValuePair<string, UserInfo> ui in ui_dic)
            {             
                TimeSpan ts = DateTime.Now - ui.Value.LastKA;
                if (ts.TotalSeconds > 9)
                    dlist.Add(ui.Key); // 예외 발생문제 때문에 삭제 목록에만 추가.
            }

            foreach(string id in dlist)
            {
                ui_dic.Remove(id);
                Logout2(id);
            }
        }

        private void Logout2(string id)
        {
            try
            {
                foreach (UserInfo ui in ui_dic.Values)
                {
                    string oip = ui.IPStr;
                    int obport = ui.BPort;
                    SendUserInfoAsync(oip, obport, id, "", 0, 0);
                }
            }
            catch
            {
            }
        }

        public void Logout(string id)
        {
            if (ui_dic.ContainsKey(id) == false)
                return;
            ui_dic.Remove(id);
            Logout2(id);
        }

        public bool Join(string id, string pw)
        {
            try
            {
                DataRow dr = mtb.NewRow();
                dr["id"] = id;
                dr["pw"] = pw;
                mtb.Rows.Add(dr);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void Withdraw(string id, string pw)
        {
            if(ui_dic.ContainsKey(id))
            {
                DataRow dr = mtb.Rows.Find(id);
                if (dr == null)
                    return;
                if(dr["pw"].ToString() == pw)
                {
                    mtb.Rows.Remove(dr);
                    Logout(id);
                }
            }
        }

        public int Login(string id, string pw)
        {
            try
            {
                DataRow dr = mtb.Rows.Find(id);
                if (dr == null)
                    return 1; // 미가입 ID
                if (ui_dic.ContainsKey(id) == false)
                {
                    if (dr["pw"].ToString() == pw)
                        return 0; // 로그인 성공
                    return 3; // 비밀번호 틀림
                }
                return 2; // 이미 로그인 중
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 3; // 예외 발생
            }
        }

        public void KeepAlive(string id)
        {
            try
            {
                if(ui_dic.ContainsKey(id))
                {
                    ui_dic[id].LastKA = DateTime.Now;
                }

            }
            catch
            {
            }
        }

        public void KeepAlive(string id, string ipstr, int sport, int fport, int bport)
        {
            Console.WriteLine($"{id}의 첫번째 KeepAlive,{ipstr},{bport}");
            try
            {
                UserInfo ui = new UserInfo(id, ipstr, sport, fport, bport);
                foreach (UserInfo oui in ui_dic.Values)
                {
                    Console.WriteLine($"Other:{oui.ID}");
                    string oip = oui.IPStr;
                    int osport = oui.SPort;
                    int ofport = oui.FPort;
                    int obport = oui.BPort;
                    SendUserInfoAsync(oip, obport, id, ipstr, sport, fport);
                    SendUserInfoAsync(ipstr, bport, oui.ID, oip, osport, ofport);
                }
                ui_dic[id] = ui;
                ui.LastKA = DateTime.Now;
            }
            catch
            {
            }
        }

        delegate void SUIDele(string oip, int obport, string id, string ipstr, int sport, int fport);

        private void SendUserInfoAsync(string oip, int obport, string id, string ipstr, int sport, int fport)
        {
            SUIDele dele = SendUserInfo;
            dele.BeginInvoke(oip, obport, id, ipstr, sport, fport, null, null);
        }

        private void SendUserInfo(string oip, int obport, string id, string ipstr, int sport, int fport)
        {
            try
            {
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(oip), obport);
                sock.Connect(iep);
                byte[] packet = new byte[1024];
                MemoryStream ms = new MemoryStream(packet);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(id);
                bw.Write(ipstr);
                bw.Write(sport);
                bw.Write(fport);
                bw.Close();
                ms.Close();
                sock.Send(packet);
                sock.Close();
            }
            catch
            {
                // 로그작성
            }
        }
    }
}
