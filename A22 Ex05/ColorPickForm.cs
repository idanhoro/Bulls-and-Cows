using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace A22_Ex05
{
    internal class ColorPickForm : Form
    {
        private readonly byte r_NumberOfOptionsToPick;
        private readonly int r_OffsetX = 5;
        private readonly int r_OffsetY = 5;
        private readonly int r_ButtonGap = 45;
        private readonly List<Button> r_ButtonsColorPik = new List<Button>();
        private Button m_ButtonToChangeColor;

        internal Button ButtonToChangeColor
        {
            set { m_ButtonToChangeColor = value; }
        }

        internal ColorPickForm(byte i_NumberOfOptionsToPick)
        {
            r_NumberOfOptionsToPick = i_NumberOfOptionsToPick;
            initializeComponent();
        }

        private void initializeComponent()
        {
            int buttonwidth;
            int buttonheight;
            for (int i = 0; i < r_NumberOfOptionsToPick; i++)
            {
                Button buttonColorPick = new Button();
                buttonwidth = r_OffsetX + (r_ButtonGap * (i / 2));
                buttonheight = r_OffsetY + (r_ButtonGap * (i % 2));
                buttonColorPick.Location = new Point(buttonwidth, buttonheight);
                buttonColorPick.Size = new Size(40, 40);
                buttonColorPick.BackColor = AvailableColors.sr_ColorsList[i];
                buttonColorPick.Click += new EventHandler(this.selectColor);
                r_ButtonsColorPik.Add(buttonColorPick);
                Controls.Add(buttonColorPick);
            }

            int width = (r_OffsetX * 2) + (r_ButtonGap * ((r_NumberOfOptionsToPick / 2) + (r_NumberOfOptionsToPick % 2)));
            int height = (r_OffsetY * 2) + (r_ButtonGap * 2);
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ClientSize = new Size(width, height);
            this.Name = "ColorPickForm";
            this.ResumeLayout(false);
        }

        private void selectColor(object sender, EventArgs e)
        {
            Button buttonSender = sender as Button;
            int indexToRelease = AvailableColors.sr_ColorsList.IndexOf(m_ButtonToChangeColor.BackColor);
            m_ButtonToChangeColor.BackColor = buttonSender.BackColor;
            if (indexToRelease != -1)
            {
                r_ButtonsColorPik[indexToRelease].Enabled = true;
            }

            buttonSender.Enabled = false;
            this.Close();
        }
    }
}
