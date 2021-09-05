using EHAAALib;
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
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void btn_join_Click(object sender, EventArgs e)
        {
            if (Eaaa.Join(tbox_id.Text, tbox_pw.Text))
                MessageBox.Show("가입 완료");
            else
                MessageBox.Show("가입 실패");
        }

        EHAAA Eaaa
        {
            get
            {
                return Activator.GetObject(typeof(EHAAA), "http://172.30.1.47:10800/AAASVC") as EHAAA;
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            int re = Eaaa.Login(tbox_id.Text, tbox_pw.Text);
            if (re == 0)
            {
                MainForm mf = new MainForm(tbox_id.Text, tbox_pw.Text);
                mf.FormClosed += Mf_FormClosed;
                this.Visible = false;
                mf.ShowDialog();
            }
            else
                MessageBox.Show(string.Format($"로그인 실패 - {re}"));
        }

        private void Mf_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Visible = true;
        }
    }
}
