using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace puissance_4
{
    public class Pion : PictureBox
    {
        public String coordonates = String.Empty;
        public String state = String.Empty;
        public PictureBox pictureBox = new PictureBox();

        public Pion()
        {
            pictureBox.Size = new System.Drawing.Size(50, 50);
            pictureBox.Image = Image.FromFile("../Images/vide.png");
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.BorderStyle = BorderStyle.None;
            pictureBox.BackColor = Color.Transparent;
        }


        protected override void OnClick(EventArgs e)
        {
       
            base.OnClick(e);
        }
        public void SetCoordonates(int a, int b)
        {
            this.coordonates = "("+a+":"+b+")";
        }
        public void SetState(String a)
        {
            state = a;
            if (a == "player1"){
                pictureBox.Image = Image.FromFile("../Images/jaune.png");
            }else if(a == "player2")
            {
                pictureBox.Image = Image.FromFile("../Images/rouge.png");
            }
        }

    }
}
