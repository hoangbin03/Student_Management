using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLySinhVien
{
    public partial class formLopHC : Form
    {
        public formLopHC()
        {
            InitializeComponent();
            this.Load += new EventHandler(formLopHC_Load);
        }

        private void formLopHC_Load(object sender, EventArgs e)
        {
            FillComboBoxes(sender, e);
            string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = "SELECT MaKhoa, TenKhoa FROM Khoa";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    cbbTimTheoKhoa.DataSource = dt;
                    cbbTimTheoKhoa.DisplayMember = "TenKhoa";

                    cbbTimTheoKhoa.ValueMember = "MaKhoa";
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maLop = tbTimKiemMaLop.Text;
            string maKhoa = cbbTimTheoKhoa.SelectedValue.ToString();
            string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM LopHC WHERE MaKhoa = @maKhoa AND MaLopHC = @maLop";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@maKhoa", maKhoa);
                    cmd.Parameters.AddWithValue("@maLop", maLop);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvHC.DataSource = dt;
                }
            }
        }

        private void btnTatCa_Click(object sender, EventArgs e)
        {
            string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM LopHC";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {

                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvHC.DataSource = dt;
                }
            }
        }

        
        private void FillComboBoxes(object sender, EventArgs e)
        {
            string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT TenKhoa FROM Khoa", conn))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cbbMaKhoa.Items.Add(reader["TenKhoa"].ToString());
                        }
                    }
                }
                using (OleDbCommand cmd = new OleDbCommand("SELECT TenGV FROM GiangVien", conn))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cbbMaGV.Items.Add(reader["TenGV"].ToString());
                        }
                    }
                }
            }
        }

        private void dgvHC_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dgvHC.Rows[e.RowIndex];

                    tbMaLop.Text = row.Cells["MaLopHC"].Value.ToString();
                    tbTenLopHC.Text = row.Cells["TenLopHC"].Value.ToString();
                    tbNamNhapHoc.Text = row.Cells["NamNhapHoc"].Value.ToString();
                    using (OleDbCommand cmd = new OleDbCommand("SELECT TenKhoa FROM Khoa WHERE MaKhoa = @MaKhoa", conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKhoa", row.Cells["MaKhoa"].Value.ToString());

                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cbbMaKhoa.SelectedItem = reader["TenKhoa"].ToString();
                            }
                        }
                    }
                    using (OleDbCommand cmd = new OleDbCommand("SELECT TenGV FROM GiangVien WHERE MaGV = @MaGV", conn))
                    {
                        cmd.Parameters.AddWithValue("@MaGV", row.Cells["MaGV"].Value.ToString());

                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cbbMaGV.SelectedItem = reader["TenGV"].ToString();
                            }
                        }
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maLop = tbMaLop.Text;
            string tenlop = tbTenLopHC.Text;
            string namnhaphoc = tbNamNhapHoc.Text;
            string tenGV = cbbMaGV.SelectedItem.ToString();
            string tenKhoa = cbbMaKhoa.SelectedItem.ToString();
            string maGV = "";
            string maKhoa = "";
            string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";

            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT MaGV FROM GiangVien WHERE TenGV = @TenGV", conn))
                {
                    cmd.Parameters.AddWithValue("@TenGV", tenGV);
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            maGV = reader["MaGV"].ToString();
                        }
                    }
                }
                using (OleDbCommand cmd = new OleDbCommand("SELECT MaKhoa FROM Khoa WHERE TenKhoa = @TenKhoa", conn))
                {
                    cmd.Parameters.AddWithValue("@TenKhoa", tenKhoa);
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            maKhoa = reader["MaKhoa"].ToString();
                        }
                    }
                }

                string query = "UPDATE LopHC SET TenLopHC = @tenlop, NamNhapHoc = @namnhaphoc, MaGV = @maGV, MaKhoa = @maKhoa WHERE MaLopHC = @malop";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tenlop", tenlop);
                    cmd.Parameters.AddWithValue("@namnhaphoc", namnhaphoc);
                    cmd.Parameters.AddWithValue("@maGV", maGV);
                    cmd.Parameters.AddWithValue("@maKhoa", maKhoa);
                    cmd.Parameters.AddWithValue("@malop", maLop);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Cập nhật thông tin thành công");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string malop = tbMaLop.Text;
            string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";

            // Xác nhận việc xóa
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = "DELETE FROM LopHC WHERE MaLopHC = @malop";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@malop", malop); ;

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Xóa thông tin thành công");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string maLop = tbMaLop.Text;
            string tenlop = tbTenLopHC.Text;
            string namnhaphoc = tbNamNhapHoc.Text;
            string tenGV = cbbMaGV.SelectedItem.ToString();
            string tenKhoa = cbbMaKhoa.SelectedItem.ToString();
            string maGV = "";
            string maKhoa = "";
            string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";

            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT MaGV FROM GiangVien WHERE TenGV = @TenGV", conn))
                {
                    cmd.Parameters.AddWithValue("@TenGV", tenGV);
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            maGV = reader["MaGV"].ToString();
                        }
                    }
                }
                using (OleDbCommand cmd = new OleDbCommand("SELECT MaKhoa FROM Khoa WHERE TenKhoa = @TenKhoa", conn))
                {
                    cmd.Parameters.AddWithValue("@TenKhoa", tenKhoa);
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            maKhoa = reader["MaKhoa"].ToString();
                        }
                    }
                }

                string query = "INSERT INTO LopHC (MaLopHC, TenLopHC, NamNhapHoc, MaGV, MaKhoa) VALUES (@malop, @tenlop, @namnhaphoc, @maGV, @maKhoa)";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@malop", maLop);
                    cmd.Parameters.AddWithValue("@tenlop", tenlop);
                    cmd.Parameters.AddWithValue("@namnhaphoc", namnhaphoc);
                    cmd.Parameters.AddWithValue("@maGV", maGV);
                    cmd.Parameters.AddWithValue("@maKhoa", maKhoa);
                    

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Thêm thông tin thành công");
        }
    }
}
