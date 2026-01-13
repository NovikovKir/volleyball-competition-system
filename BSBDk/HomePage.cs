using БСБДк;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace БСБДк
{
    public partial class HomePage : Form
    {
        private void CheckDatabaseConnection()
        {
            //Тест подключения
            bool isConnected = DatabaseHelper.TestConnection();

            if (!isConnected)
            {
                DialogResult result = MessageBox.Show(
                    "Нет подключения к базе данных.\n" +
                    "Хотите продолжить?",
                    "Предупреждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    Application.Exit();
                }
            }
        }
        public HomePage()
        {
            InitializeComponent();

            CheckDatabaseConnection();
        }

        //Профиль
        private void button1_Click(object sender, EventArgs e)
        {
            ProfileForm profileForm = new ProfileForm();
            profileForm.Show();
        }
        //Соревнования
        private void button2_Click(object sender, EventArgs e)
        {
            CompetitionsForm compForm = new CompetitionsForm();
            compForm.Show();

        }
        //Залы
        private void button3_Click(object sender, EventArgs e)
        {
            HallsForm hallsForm = new HallsForm();
            hallsForm.Show();
        }
        //Ближайшие игры
        private void button4_Click(object sender, EventArgs e)
        {
            GamesForm gamesForm = new GamesForm();
            gamesForm.Show();
        }
    }
}
