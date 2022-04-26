using System;
using System.Windows.Forms;

namespace A22_Ex05
{
    public class CowsAndBullsGame
    {
        [STAThread]
        public void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SettingsForm settingsForm = new SettingsForm();
            Application.Run(settingsForm);
        }
    }
}
