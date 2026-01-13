using System;
using System.Data;
using System.Windows.Forms;

namespace БСБДк
{
    public partial class CompetitionGamesForm : Form
    {
        private int competitionId;
        private string competitionName;

        public CompetitionGamesForm(int compId, string compName)
        {
            InitializeComponent();
            competitionId = compId;
            competitionName = compName;

            this.Text = $"Игры соревнования: {competitionName}";
            LoadGamesForCompetition();
        }

        private void LoadGamesForCompetition()
        {
            try
            {
                DataTable gamesData = GetGamesForCompetitionFromDB();

                if (gamesData.Rows.Count > 0)
                {
                    dataGridView1.DataSource = gamesData;
                    this.Text = $"Игры соревнования: {competitionName} ({gamesData.Rows.Count} записей)";
                }
                else
                {
                    //Если нет данных
                    dataGridView1.Columns.Clear();
                    dataGridView1.Rows.Clear();

                    dataGridView1.Columns.Add("Message", "Информация");
                    dataGridView1.Rows.Add("Для этого соревнования нет запланированных игр.");

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    this.Text = $"Игры соревнования: {competitionName} (нет данных)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки игр: {ex.Message}", "Ошибка");
                this.Close();
            }
        }

        private DataTable GetGamesForCompetitionFromDB()
        {
            try
            {
                string query = @"
                    SELECT 
                        g.GameID,
                        g.GameDate,
                        g.GameTime,
                        g.Score,
                        h.Name AS HallName,
                        t1.Name AS Team1,
                        t2.Name AS Team2
                    FROM Game g
                    JOIN Hall h ON g.HallName = h.Name
                    JOIN Team t1 ON g.FirstTeamID = t1.TeamID
                    JOIN Team t2 ON g.SecondTeamID = t2.TeamID
                    WHERE g.CompetitionID = @CompetitionID
                    ORDER BY g.GameDate, g.GameTime";

                using (var conn = new System.Data.SqlClient.SqlConnection(
                    System.Configuration.ConfigurationManager.ConnectionStrings["voleyballCompetitionsDB"].ConnectionString))
                using (var cmd = new System.Data.SqlClient.SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CompetitionID", competitionId);

                    using (var adapter = new System.Data.SqlClient.SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
            }
            catch
            {
                return new DataTable();
            }
        }
    }
}