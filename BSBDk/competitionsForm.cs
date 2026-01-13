using System;
using System.Data;
using System.Windows.Forms;

namespace БСБДк
{
    public partial class CompetitionsForm : Form
    {
        public CompetitionsForm()
        {
            InitializeComponent();
            LoadCompetitions();
        }

        private void LoadCompetitions()
        {
            try
            {
                var data = DatabaseHelper.GetAllCompetitions();

                if (data.Rows.Count > 0)
                {
                    dataGridView1.DataSource = data;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.AllowUserToDeleteRows = false;

                    this.Text = $"Все соревнования ({data.Rows.Count} записей)";
                    dataGridView1.CellClick += DataGridView1_CellClick;
                }
                else
                {
                    //Если нет данных
                    dataGridView1.Columns.Clear();
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Add("Message", "Информация");
                    dataGridView1.Rows.Add("В базе данных нет соревнований.");
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    this.Text = "Все соревнования (нет данных)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки соревнований: {ex.Message}", "Ошибка");
                this.Close();
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Ищем столбец с ID
                if (row.Cells["CompetitionID"]?.Value != null)
                {
                    int competitionId = Convert.ToInt32(row.Cells["CompetitionID"].Value);
                    string competitionName = row.Cells["Name"]?.Value?.ToString() ?? "Без названия";

                    CompetitionGamesForm gamesForm = new CompetitionGamesForm(competitionId, competitionName);
                    gamesForm.Show();
                }
                else if (row.Cells["ID"]?.Value != null) // Альтернативное имя столбца
                {
                    int competitionId = Convert.ToInt32(row.Cells["ID"].Value);
                    string competitionName = row.Cells["Название"]?.Value?.ToString() ??
                                           row.Cells["Name"]?.Value?.ToString() ?? "Без названия";

                    CompetitionGamesForm gamesForm = new CompetitionGamesForm(competitionId, competitionName);
                    gamesForm.Show();
                }
            }
        }
    }
}