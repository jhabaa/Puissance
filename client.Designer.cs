
namespace puissance_4
{
    partial class client
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(client));
            this.button_fermer = new System.Windows.Forms.Button();
            this.panel_jeu = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.monitor1 = new AroundTheWorld.Monitor();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_fermer
            // 
            this.button_fermer.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_fermer.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button_fermer.FlatAppearance.BorderSize = 20;
            this.button_fermer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Teal;
            this.button_fermer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_fermer.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_fermer.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button_fermer.Location = new System.Drawing.Point(1328, 760);
            this.button_fermer.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button_fermer.Name = "button_fermer";
            this.button_fermer.Size = new System.Drawing.Size(248, 83);
            this.button_fermer.TabIndex = 1;
            this.button_fermer.Text = "Close";
            this.button_fermer.UseVisualStyleBackColor = false;
            this.button_fermer.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel_jeu
            // 
            this.panel_jeu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_jeu.BackgroundImage")));
            this.panel_jeu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_jeu.Location = new System.Drawing.Point(24, 23);
            this.panel_jeu.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panel_jeu.Name = "panel_jeu";
            this.panel_jeu.Size = new System.Drawing.Size(814, 677);
            this.panel_jeu.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(276, 744);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 91);
            this.label1.TabIndex = 5;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // monitor1
            // 
            this.monitor1.BackColor = System.Drawing.Color.Black;
            this.monitor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.monitor1.ForeColor = System.Drawing.Color.Green;
            this.monitor1.Location = new System.Drawing.Point(1228, 25);
            this.monitor1.Margin = new System.Windows.Forms.Padding(6);
            this.monitor1.MaxLength = 32767454;
            this.monitor1.Multiline = true;
            this.monitor1.Name = "monitor1";
            this.monitor1.NumberOfLines = 2;
            this.monitor1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.monitor1.Size = new System.Drawing.Size(344, 689);
            this.monitor1.TabIndex = 10;
            // 
            // messageBox
            // 
            this.messageBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageBox.Location = new System.Drawing.Point(842, 760);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(400, 50);
            this.messageBox.TabIndex = 12;
            this.messageBox.Text = "Message à envoyer";
            this.messageBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.messageBox_KeyPress);
            // 
            // client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1600, 865);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.monitor1);
            this.Controls.Add(this.panel_jeu);
            this.Controls.Add(this.button_fermer);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "client";
            this.Text = "client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_fermer;
        private System.Windows.Forms.Panel panel_jeu;
        private AroundTheWorld.Monitor monitor1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox messageBox;
    }
}