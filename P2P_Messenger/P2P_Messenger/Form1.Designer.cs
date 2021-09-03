
namespace P2P_Messenger
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbox_my_ip = new System.Windows.Forms.TextBox();
            this.tbox_my_port = new System.Windows.Forms.TextBox();
            this.btn_my_set = new System.Windows.Forms.Button();
            this.btn_other_set = new System.Windows.Forms.Button();
            this.tbox_other_port = new System.Windows.Forms.TextBox();
            this.tbox_other_ip = new System.Windows.Forms.TextBox();
            this.lbox_msg = new System.Windows.Forms.ListBox();
            this.tbox_msg = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.btn_other_fset = new System.Windows.Forms.Button();
            this.tbox_other_fport = new System.Windows.Forms.TextBox();
            this.btn_my_fset = new System.Windows.Forms.Button();
            this.tbox_my_fport = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbox_my_ip
            // 
            this.tbox_my_ip.Location = new System.Drawing.Point(59, 37);
            this.tbox_my_ip.Name = "tbox_my_ip";
            this.tbox_my_ip.Size = new System.Drawing.Size(653, 28);
            this.tbox_my_ip.TabIndex = 0;
            // 
            // tbox_my_port
            // 
            this.tbox_my_port.Location = new System.Drawing.Point(730, 37);
            this.tbox_my_port.Name = "tbox_my_port";
            this.tbox_my_port.Size = new System.Drawing.Size(179, 28);
            this.tbox_my_port.TabIndex = 1;
            // 
            // btn_my_set
            // 
            this.btn_my_set.Location = new System.Drawing.Point(933, 37);
            this.btn_my_set.Name = "btn_my_set";
            this.btn_my_set.Size = new System.Drawing.Size(136, 27);
            this.btn_my_set.TabIndex = 2;
            this.btn_my_set.Text = "설정";
            this.btn_my_set.UseVisualStyleBackColor = true;
            this.btn_my_set.Click += new System.EventHandler(this.btn_my_set_Click);
            // 
            // btn_other_set
            // 
            this.btn_other_set.Location = new System.Drawing.Point(933, 71);
            this.btn_other_set.Name = "btn_other_set";
            this.btn_other_set.Size = new System.Drawing.Size(136, 27);
            this.btn_other_set.TabIndex = 5;
            this.btn_other_set.Text = "설정";
            this.btn_other_set.UseVisualStyleBackColor = true;
            this.btn_other_set.Click += new System.EventHandler(this.btn_other_set_Click);
            // 
            // tbox_other_port
            // 
            this.tbox_other_port.Location = new System.Drawing.Point(730, 71);
            this.tbox_other_port.Name = "tbox_other_port";
            this.tbox_other_port.Size = new System.Drawing.Size(179, 28);
            this.tbox_other_port.TabIndex = 4;
            // 
            // tbox_other_ip
            // 
            this.tbox_other_ip.Location = new System.Drawing.Point(59, 71);
            this.tbox_other_ip.Name = "tbox_other_ip";
            this.tbox_other_ip.Size = new System.Drawing.Size(653, 28);
            this.tbox_other_ip.TabIndex = 3;
            // 
            // lbox_msg
            // 
            this.lbox_msg.AllowDrop = true;
            this.lbox_msg.FormattingEnabled = true;
            this.lbox_msg.ItemHeight = 18;
            this.lbox_msg.Location = new System.Drawing.Point(59, 105);
            this.lbox_msg.Name = "lbox_msg";
            this.lbox_msg.Size = new System.Drawing.Size(1355, 526);
            this.lbox_msg.TabIndex = 6;
            this.lbox_msg.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbox_msg_DragDrop);
            this.lbox_msg.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbox_msg_DragEnter);
            // 
            // tbox_msg
            // 
            this.tbox_msg.Location = new System.Drawing.Point(59, 675);
            this.tbox_msg.Name = "tbox_msg";
            this.tbox_msg.Size = new System.Drawing.Size(1195, 28);
            this.tbox_msg.TabIndex = 7;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(1278, 673);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(136, 28);
            this.btn_send.TabIndex = 8;
            this.btn_send.Text = "전송";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // btn_other_fset
            // 
            this.btn_other_fset.Location = new System.Drawing.Point(1278, 72);
            this.btn_other_fset.Name = "btn_other_fset";
            this.btn_other_fset.Size = new System.Drawing.Size(136, 27);
            this.btn_other_fset.TabIndex = 12;
            this.btn_other_fset.Text = "설정";
            this.btn_other_fset.UseVisualStyleBackColor = true;
            this.btn_other_fset.Click += new System.EventHandler(this.btn_other_fset_Click);
            // 
            // tbox_other_fport
            // 
            this.tbox_other_fport.Location = new System.Drawing.Point(1075, 72);
            this.tbox_other_fport.Name = "tbox_other_fport";
            this.tbox_other_fport.Size = new System.Drawing.Size(179, 28);
            this.tbox_other_fport.TabIndex = 11;
            // 
            // btn_my_fset
            // 
            this.btn_my_fset.Location = new System.Drawing.Point(1278, 38);
            this.btn_my_fset.Name = "btn_my_fset";
            this.btn_my_fset.Size = new System.Drawing.Size(136, 27);
            this.btn_my_fset.TabIndex = 10;
            this.btn_my_fset.Text = "설정";
            this.btn_my_fset.UseVisualStyleBackColor = true;
            this.btn_my_fset.Click += new System.EventHandler(this.btn_my_fset_Click);
            // 
            // tbox_my_fport
            // 
            this.tbox_my_fport.Location = new System.Drawing.Point(1075, 38);
            this.tbox_my_fport.Name = "tbox_my_fport";
            this.tbox_my_fport.Size = new System.Drawing.Size(179, 28);
            this.tbox_my_fport.TabIndex = 9;
            // 
            // Form1
            // 
            this.AcceptButton = this.btn_send;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1555, 753);
            this.Controls.Add(this.btn_other_fset);
            this.Controls.Add(this.tbox_other_fport);
            this.Controls.Add(this.btn_my_fset);
            this.Controls.Add(this.tbox_my_fport);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.tbox_msg);
            this.Controls.Add(this.lbox_msg);
            this.Controls.Add(this.btn_other_set);
            this.Controls.Add(this.tbox_other_port);
            this.Controls.Add(this.tbox_other_ip);
            this.Controls.Add(this.btn_my_set);
            this.Controls.Add(this.tbox_my_port);
            this.Controls.Add(this.tbox_my_ip);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbox_my_ip;
        private System.Windows.Forms.TextBox tbox_my_port;
        private System.Windows.Forms.Button btn_my_set;
        private System.Windows.Forms.Button btn_other_set;
        private System.Windows.Forms.TextBox tbox_other_port;
        private System.Windows.Forms.TextBox tbox_other_ip;
        private System.Windows.Forms.ListBox lbox_msg;
        private System.Windows.Forms.TextBox tbox_msg;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Button btn_other_fset;
        private System.Windows.Forms.TextBox tbox_other_fport;
        private System.Windows.Forms.Button btn_my_fset;
        private System.Windows.Forms.TextBox tbox_my_fport;
    }
}

