using System;
using System.Windows.Forms;

namespace БСБДк
{
    public partial class ProfileForm : Form
    {
        public ProfileForm()
        {
            InitializeComponent();
            LoadProfile();
        }

        private void LoadProfile()
        {
            try
            {
                
                //Загрузка данных из БД
                var data = DatabaseHelper.GetMyProfile();

                //Очистка и заполнение DataGridView
                dataGridView1.DataSource = data;

                //Обновление заголовка
                this.Text = $"Мой профиль ({data.Rows.Count} записей)";

                //Если нет данных
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show("Нет данных в БД");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

    }
}