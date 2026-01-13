using System;
using System.Windows.Forms;

namespace БСБДк
{
    public partial class GamesForm : Form
    {
        public GamesForm()
        {
            InitializeComponent();
            LoadGames();
        }

        private void LoadGames()
        {
            try
            {
                var data = DatabaseHelper.GetUpcomingGames();

                if (data.Rows.Count > 0)
                {
                    dataGridView1.DataSource = data;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    this.Text = $"Ближайшие игры ({data.Rows.Count} записей)";
                }
                else
                {
                    //Если нет данных
                    dataGridView1.Columns.Clear();
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Add("Message", "Информация");
                    dataGridView1.Rows.Add("Нет предстоящих игр.");
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    this.Text = "Ближайшие игры (нет данных)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки игр: {ex.Message}", "Ошибка");
                this.Close();
            }
        }
    }
}