using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace БСБДк
{
    public class DatabaseHelper
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["voleyballCompetitionsDB"].ConnectionString;
        }

        //Профиль user2 (ParticipantID = 2)
        public static DataTable GetMyProfile()
        {
            try
            {
                string query = "SELECT * FROM Participant WHERE ParticipantID = 2";

                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки профиля: {ex.Message}", "Ошибка БД");
                return new DataTable();
            }
        }

        //Все залы
        public static DataTable GetAllHalls()
        {
            try
            {
                string query = "SELECT Name, City FROM Hall ORDER BY City, Name";

                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки залов: {ex.Message}", "Ошибка БД");
                return new DataTable();
            }
        }

        //Все соревнования
        public static DataTable GetAllCompetitions()
        {
            try
            {
                string query = "SELECT CompetitionID, Name, StartDate, EndDate FROM Competition ORDER BY StartDate DESC";

                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки соревнований: {ex.Message}", "Ошибка БД");
                return new DataTable();
            }
        }

        //Ближайшие игры
        public static DataTable GetUpcomingGames()
        {
            try
            {
                string query = @"
                    SELECT TOP 10 
                        g.GameID,
                        g.GameDate,
                        g.GameTime,
                        g.Score,
                        h.Name AS HallName,
                        c.Name AS CompetitionName,
                        t1.Name AS Team1,
                        t2.Name AS Team2
                    FROM Game g
                    JOIN Hall h ON g.HallName = h.Name
                    JOIN Competition c ON g.CompetitionID = c.CompetitionID
                    JOIN Team t1 ON g.FirstTeamID = t1.TeamID
                    JOIN Team t2 ON g.SecondTeamID = t2.TeamID
                    WHERE g.GameDate >= CAST(GETDATE() AS DATE)
                    ORDER BY g.GameDate, g.GameTime";

                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки игр: {ex.Message}", "Ошибка БД");
                return new DataTable();
            }
        }

        //Проверка подключения
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к БД:\n{ex.Message}\n\n" +
                               "Проверьте:\n1. Запущен ли SQL Server\n" +
                               "2. Правильность строки подключения в App.config",
                               "Ошибка подключения");
                return false;
            }
        }
    }
}