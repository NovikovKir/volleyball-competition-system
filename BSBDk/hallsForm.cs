using System;
using System.Windows.Forms;

namespace БСБДк
{
    public partial class HallsForm : Form
    {
        public HallsForm()
        {
            InitializeComponent();
            LoadHalls();
        }

        private void LoadHalls()
        {
            try
            {
                var data = DatabaseHelper.GetAllHalls();

                if (data.Rows.Count > 0)
                {
                    dataGridView1.DataSource = data;
                    this.Text = $"Список залов ({data.Rows.Count} записей)";
                }
                else
                {
                    //Если нет данных
                    dataGridView1.Columns.Clear();
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Add("Message", "Информация");
                    dataGridView1.Rows.Add("В базе данных нет залов.");
                    this.Text = "Список залов (нет данных)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки залов: {ex.Message}", "Ошибка");
                this.Close();
            }
        }
    }
}