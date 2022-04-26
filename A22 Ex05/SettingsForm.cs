using System;
using System.Windows.Forms;

namespace A22_Ex05
{
    internal class SettingsForm : Form
    {
        private readonly int r_MinNumberOfChances = 4;
        private readonly int r_MaxNumberOfChances = 10;
        private Button m_ButtonNumberOfChances;
        private Button m_ButtonStart;
        private int m_NumberOfChances = 8;

        internal int NumberOfChances
        {
            get { return m_NumberOfChances; }
        }

        internal SettingsForm()
        {
            initializeComponent();
        }

        private void initializeComponent()
        {
            this.m_ButtonNumberOfChances = new Button();
            this.m_ButtonStart = new Button();
            this.SuspendLayout();

            // ButtonNumberOfChances
            this.m_ButtonNumberOfChances.Location = new System.Drawing.Point(56, 45);
            this.m_ButtonNumberOfChances.Name = "Number of chances";
            this.m_ButtonNumberOfChances.Size = new System.Drawing.Size(337, 74);
            this.m_ButtonNumberOfChances.TabIndex = 0;
            this.m_ButtonNumberOfChances.Text = "Number of chances : 8";
            this.m_ButtonNumberOfChances.UseVisualStyleBackColor = true;
            this.m_ButtonNumberOfChances.Click += new System.EventHandler(this.increaseNumberOfChances);

            // ButtonStart
            this.m_ButtonStart.Location = new System.Drawing.Point(276, 180);
            this.m_ButtonStart.Name = "Start button";
            this.m_ButtonStart.Size = new System.Drawing.Size(135, 45);
            this.m_ButtonStart.TabIndex = 1;
            this.m_ButtonStart.Text = "Start";
            this.m_ButtonStart.UseVisualStyleBackColor = true;
            this.m_ButtonStart.Click += new EventHandler(this.button2_Click);

            // SettingsForm
            this.ClientSize = new System.Drawing.Size(445, 260);
            this.Controls.Add(this.m_ButtonNumberOfChances);
            this.Controls.Add(this.m_ButtonStart);

            this.Name = "SettingsForm";
            this.ResumeLayout(false);
        }

        private void increaseNumberOfChances(object sender, EventArgs e)
        {
            Button buttonSender = sender as Button;
            m_NumberOfChances = ((m_NumberOfChances - r_MinNumberOfChances + 1) % (r_MaxNumberOfChances - r_MinNumberOfChances + 1)) + r_MinNumberOfChances;
            buttonSender.Text = string.Format("Number of chances : {0}", m_NumberOfChances);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            GameTableForm gameTableForm = new GameTableForm((byte)NumberOfChances, 4, 8);
            gameTableForm.ShowDialog();
            Close();
        }
    }
}
