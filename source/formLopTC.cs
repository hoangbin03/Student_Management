using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class formLopTC : Form
    {
        public formLopTC()
        {
            InitializeComponent();

            this.Load += new EventHandler(formLopHP_Load);
        }
        private void formLopHP_Load(object sender, EventArgs e)
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
            string maLopHP = tbTimKiemTheoTen.Text;
            string namHoc = tbTimTheoNam.Text;
            string maKhoa = cbbTimTheoKhoa.SelectedValue.ToString();
            string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM LopHP WHERE MaLopHP = @maLop AND NamHoc = @namHoc AND MaKhoa = @maKhoa";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@maLop", maLopHP);
                    cmd.Parameters.AddWithValue("@namHoc", namHoc);
                    cmd.Parameters.AddWithValue("@maKhoa", maKhoa);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvSV.DataSource = dt;

                }
            }
        }

        private void btnTatCa_Click(object sender, EventArgs e)
        {
            string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM LopHP";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {

                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvSV.DataSource = dt;

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
                using (OleDbCommand cmd = new OleDbCommand("SELECT TenMon FROM MonHoc", conn))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cbbMaMon.Items.Add(reader["TenMon"].ToString());
                        }
                    }
                }
            }
        }
        private void dgvSV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dgvSV.Rows[e.RowIndex];

                    tbMaLop.Text = row.Cells["MaLopHP"].Value.ToString();
                    tbNamHoc.Text = row.Cells["NamHoc"].Value.ToString();
                    if (int.TryParse(row.Cells["HocKi"].Value.ToString(), out int hocKi))
                    {
                        NUDhocki.Value = hocKi;
                    }
                    else
                    {
                        // Xử lý trường hợp không thể chuyển đổi giá trị thành số nguyên
                        MessageBox.Show("Không thể chuyển đổi giá trị Học kì từ bảng thành số nguyên. Vui lòng kiểm tra lại dữ liệu.");
                        NUDhocki.Value = NUDhocki.Minimum; // hoặc bất kỳ giá trị mặc định nào bạn muốn
                    }
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
                    using (OleDbCommand cmd = new OleDbCommand("SELECT TenMon FROM MonHoc WHERE MaMon = @MaMon", conn))
                    {
                        cmd.Parameters.AddWithValue("@MaMon", row.Cells["MaMon"].Value.ToString());

                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cbbMaMon.SelectedItem = reader["TenMon"].ToString();
                            }
                        }
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maLop = tbMaLop.Text;
            string namhoc = tbNamHoc.Text;
            int hocKi = (int)NUDhocki.Value;
            string tenGV = cbbMaGV.SelectedItem.ToString();
            string tenKhoa = cbbMaKhoa.SelectedItem.ToString();
            string monhoc = cbbMaMon.SelectedItem.ToString();
            string maGV = "";
            string maKhoa = "";
            string mamon = "";
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
                using (OleDbCommand cmd = new OleDbCommand("SELECT MaMon FROM MonHoc WHERE TenMon = @TenMon", conn))
                {
                    cmd.Parameters.AddWithValue("@TenMon", monhoc);
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            mamon = reader["MaMon"].ToString();
                        }
                    }
                }

                string query = "UPDATE LopHP SET HocKi = @hocki, NamHoc = @namhoc, MaMon = @mamon, MaGV = @maGV, MaKhoa = @maKhoa WHERE MaLopHP = @malop";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@hocki", hocKi);
                    cmd.Parameters.AddWithValue("@namhoc", namhoc);
                    cmd.Parameters.AddWithValue("@mamon", mamon);
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
                string query = "DELETE FROM LopHP WHERE MaLopHP = @malop";

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
            int hocKi = (int)NUDhocki.Value;
            string namhoc = tbNamHoc.Text;
            string monhoc = cbbMaMon.SelectedItem.ToString();
            string tenGV = cbbMaGV.SelectedItem.ToString();
            string tenKhoa = cbbMaKhoa.SelectedItem.ToString();
            string mamon = "";
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
                using (OleDbCommand cmd = new OleDbCommand("SELECT MaMon FROM MonHoc WHERE TenMon = @TenMon", conn))
                {
                    cmd.Parameters.AddWithValue("@TenMon", monhoc);
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            mamon = reader["MaMon"].ToString();
                        }
                    }
                }

                string query = "INSERT INTO LopHP (MaLopHP,HocKi, NamHoc, MaMon, MaGV, MaKhoa) VALUES (@malop, @hocki, @namhoc,@mamon, @maGV, @maKhoa)";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@malop", maLop);
                    cmd.Parameters.AddWithValue("@hocki", hocKi);
                    cmd.Parameters.AddWithValue("@namhoc", namhoc);
                    cmd.Parameters.AddWithValue("@mamon", mamon);
                    cmd.Parameters.AddWithValue("@maGV", maGV);
                    cmd.Parameters.AddWithValue("@maKhoa", maKhoa);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Cập nhật thông tin thành công");
        }
    }
    
}
    
