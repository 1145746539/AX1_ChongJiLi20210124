namespace AX1_ChongJiLi20201221
{
    partial class FrAlarm
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
            this.components = new System.ComponentModel.Container();
            this.listBoxAlarm = new System.Windows.Forms.ListBox();
            this.button4 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // listBoxAlarm
            // 
            this.listBoxAlarm.FormattingEnabled = true;
            this.listBoxAlarm.ItemHeight = 12;
            this.listBoxAlarm.Location = new System.Drawing.Point(12, 12);
            this.listBoxAlarm.Name = "listBoxAlarm";
            this.listBoxAlarm.Size = new System.Drawing.Size(450, 556);
            this.listBoxAlarm.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(603, 395);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(115, 61);
            this.button4.TabIndex = 11;
            this.button4.Text = "报警刷新";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1141, 595);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.listBoxAlarm);
            this.Name = "FrAlarm";
            this.Text = "FrAlarm";
            this.Load += new System.EventHandler(this.FrAlarm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxAlarm;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Timer timer1;
    }
}