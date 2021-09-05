
namespace P2P_Messenger
{
    partial class StartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbox_id = new System.Windows.Forms.TextBox();
            this.tbox_pw = new System.Windows.Forms.TextBox();
            this.btn_join = new System.Windows.Forms.Button();
            this.btn_login = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(88, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 27);
            this.label2.TabIndex = 1;
            this.label2.Text = "PW:";
            // 
            // tbox_id
            // 
            this.tbox_id.Location = new System.Drawing.Point(180, 117);
            this.tbox_id.Name = "tbox_id";
            this.tbox_id.Size = new System.Drawing.Size(403, 28);
            this.tbox_id.TabIndex = 2;
            // 
            // tbox_pw
            // 
            this.tbox_pw.Location = new System.Drawing.Point(180, 157);
            this.tbox_pw.Name = "tbox_pw";
            this.tbox_pw.Size = new System.Drawing.Size(403, 28);
            this.tbox_pw.TabIndex = 3;
            // 
            // btn_join
            // 
            this.btn_join.Location = new System.Drawing.Point(295, 220);
            this.btn_join.Name = "btn_join";
            this.btn_join.Size = new System.Drawing.Size(141, 44);
            this.btn_join.TabIndex = 4;
            this.btn_join.Text = " 가입";
            this.btn_join.UseVisualStyleBackColor = true;
            this.btn_join.Click += new System.EventHandler(this.btn_join_Click);
            // 
            // btn_login
            // 
            this.btn_login.Location = new System.Drawing.Point(442, 220);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(141, 44);
            this.btn_login.TabIndex = 5;
            this.btn_login.Text = "로그인";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.btn_join);
            this.Controls.Add(this.tbox_pw);
            this.Controls.Add(this.tbox_id);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "StartForm";
            this.Text = "StartForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbox_id;
        private System.Windows.Forms.TextBox tbox_pw;
        private System.Windows.Forms.Button btn_join;
        private System.Windows.Forms.Button btn_login;
    }
}