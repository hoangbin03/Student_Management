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
    public partial class formGV : Form
    {
        public formGV()
        {
            InitializeComponent();

            this.Load += new EventHandler(formGV_Load);
        }
        private void formGV_Load(object sender, EventArgs e)
        {
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
            string maGV = tbTimKiemTheoTen.Text;
            string maKhoa = cbbTimTheoKhoa.SelectedValue.ToString();
            string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM GiangVien WHERE MaGV = @maGV AND MaKhoa = @maKhoa";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@maGV", maGV);
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
                string query = "SELECT * FROM GiangVien";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {

                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvSV.DataSource = dt;

                }
            }
        }

        private void dgvSV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvSV.Rows[e.RowIndex];

                tbMaGV.Text = row.Cells["MaGV"].Value.ToString();
                tbHoTen.Text = row.Cells["TenGV"].Value.ToString();
                tbNgaysinh.Text = row.Cells["NgaySinh"].Value.ToString();
                string gioiTinh = row.Cells["GioiTinh"].Value.ToString();
                if (gioiTinh == "Nam")
                {
                    rdoNam.Checked = true;
                    rdoNu.Checked = false;
                }
                else if (gioiTinh == "Nữ")
                {
                    rdoNam.Checked = false;
                    rdoNu.Checked = true;
                }
                tbDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                tbSoDt.Text = row.Cells["SoDT"].Value.ToString();
                tbEmail.Text = row.Cells["Email"].Value.ToString();
            }
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string maGV = tbMaGV.Text;
            string hoTen = tbHoTen.Text;
            string ngaySinh = tbNgaysinh.Text;
            string gioiTinh = rdoNam.Checked ? "Nam" : "Nữ";
            string diachi = tbDiaChi.Text;
            string sodt = tbSoDt.Text;
            string email = tbEmail.Text;
            string maKhoa = cbbTimTheoKhoa.SelectedValue.ToString();
            string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";

            // Xác nhận việc cập nhật
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn sửa không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = "UPDATE GiangVien SET TenGV = @hoTen, NgaySinh = @ngaySinh, GioiTinh = @gioiTinh, DiaChi = @diachi, SoDT = @sodt, Email = @email, MaKhoa = @maKhoa WHERE MaGV = @maGV";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@hoTen", hoTen);
                    cmd.Parameters.AddWithValue("@ngaySinh", ngaySinh);
                    cmd.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@diachi", diachi);
                    cmd.Parameters.AddWithValue("@sodt", sodt);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@maKhoa", maKhoa);
                    cmd.Parameters.AddWithValue("@maGV", maGV);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Cập nhật thông tin thành công");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string maGV = tbMaGV.Text;
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
                string query = "DELETE FROM GiangVien WHERE MaGV = @maGV";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSV", maGV);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Xóa thông tin thành công");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string maGV = tbMaGV.Text;
            string hoTen = tbHoTen.Text;
            string ngaySinh = tbNgaysinh.Text;
            string gioiTinh = rdoNam.Checked ? "Nam" : "Nữ";
            string sodt = tbSoDt.Text;
            string diachi = tbDiaChi.Text;
            string email = tbEmail.Text;
            string maKhoa = cbbTimTheoKhoa.SelectedValue.ToString();
            string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";

            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO GiangVien (MaGV, TenGV, NgaySinh, GioiTinh, SoDT, DiaChi, Email, MaKhoa) VALUES (@maGV, @hoTen, @ngaySinh, @gioiTinh, @sodt, @diachi, @email, @maKhoa)";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maGV", maGV);
                    cmd.Parameters.AddWithValue("@hoTen", hoTen);
                    cmd.Parameters.AddWithValue("@ngaySinh", ngaySinh);
                    cmd.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@sodt", sodt);
                    cmd.Parameters.AddWithValue("@diachi", diachi);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@maKhoa", maKhoa);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Thêm thông tin thành công");
        }
    }
}
