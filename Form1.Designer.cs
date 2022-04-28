
namespace puissance_4
{
    partial class Form1
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
            this.button_exit = new System.Windows.Forms.Button();
            this.button_online = new System.Windows.Forms.Button();
            this.panel_jeu = new System.Windows.Forms.Panel();
            this.monitor1 = new AroundTheWorld.Monitor();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_exit
            // 
            this.button_exit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_exit.Location = new System.Drawing.Point(1423, 46);
            this.button_exit.Margin = new System.Windows.Forms.Padding(6);
            this.button_exit.Name = "button_exit";
            this.button_exit.Size = new System.Drawing.Size(226, 63);
            this.button_exit.TabIndex = 0;
            this.button_exit.Text = "Exit";
            this.button_exit.UseVisualStyleBackColor = true;
            this.button_exit.Click += new System.EventHandler(this.button_exit_Click);
            // 
            // button_online
            // 
            this.button_online.Location = new System.Drawing.Point(1423, 147);
            this.button_online.Margin = new System.Windows.Forms.Padding(6);
            this.button_online.Name = "button_online";
            this.button_online.Size = new System.Drawing.Size(226, 67);
            this.button_online.TabIndex = 1;
            this.button_online.Text = "Play Online";
            this.button_online.UseVisualStyleBackColor = true;
            this.button_online.Click += new System.EventHandler(this.button_online_Click);
            // 
            // panel_jeu
            // 
            this.panel_jeu.BackColor = System.Drawing.Color.Transparent;
            this.panel_jeu.BackgroundImage = global::puissance_4.Properties.Resources.texture_bois;
            this.panel_jeu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_jeu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_jeu.Location = new System.Drawing.Point(2, 23);
            this.panel_jeu.Margin = new System.Windows.Forms.Padding(6);
            this.panel_jeu.Name = "panel_jeu";
            this.panel_jeu.Size = new System.Drawing.Size(810, 673);
            this.panel_jeu.TabIndex = 2;
            // 
            // monitor1
            // 
            this.monitor1.BackColor = System.Drawing.Color.Black;
            this.monitor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.monitor1.ForeColor = System.Drawing.Color.Green;
            this.monitor1.Location = new System.Drawing.Point(1210, 262);
            this.monitor1.Margin = new System.Windows.Forms.Padding(6);
            this.monitor1.Multiline = true;
            this.monitor1.Name = "monitor1";
            this.monitor1.NumberOfLines = 2;
            this.monitor1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.monitor1.Size = new System.Drawing.Size(448, 610);
            this.monitor1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(318, 781);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 91);
            this.label2.TabIndex = 5;
            // 
            // messageBox
            // 
            this.messageBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageBox.Location = new System.Drawing.Point(845, 23);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(400, 50);
            this.messageBox.TabIndex = 11;
            this.messageBox.Text = "Message à envoyer";
            this.messageBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.messageBox_KeyPress);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.BackgroundImage = global::puissance_4.Properties.Resources.Metal034_4K_Roughness;
            this.ClientSize = new System.Drawing.Size(1686, 927);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel_jeu);
            this.Controls.Add(this.monitor1);
            this.Controls.Add(this.button_online);
            this.Controls.Add(this.button_exit);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_online;
        internal System.Windows.Forms.Button button_exit;
        private System.Windows.Forms.Panel panel_jeu;
        private AroundTheWorld.Monitor monitor1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox messageBox;
    }
}

