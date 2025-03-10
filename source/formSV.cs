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
    public partial class formSV : Form
    {

        public formSV()
        {
            InitializeComponent();
            this.Load += new EventHandler(formSV_Load);
    }

        private void formSV_Load(object sender, EventArgs e)
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
            string timkiem = tbTimKiemTheoTen.Text;
            string maLop = tbTimTheoLop.Text;
            string maKhoa = cbbTimTheoKhoa.SelectedValue.ToString();
            string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM SinhVien WHERE (MaSV  = @timkiem OR TenSV = @timkiem) AND MaKhoa = @maKhoa AND MaLopHC = @maLop";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@timkiem", timkiem);
                    cmd.Parameters.AddWithValue("@maKhoa", maKhoa);
                    cmd.Parameters.AddWithValue("@maLop", maLop);
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
                string query = "SELECT * FROM SinhVien";
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

                tbMaSV.Text = row.Cells["MaSV"].Value.ToString();
                tbHoTen.Text = row.Cells["TenSV"].Value.ToString();
                tbNgaysinh.Text = row.Cells["NamSinh"].Value.ToString();
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
                tbMaLopHC.Text = row.Cells["MaLopHC"].Value.ToString();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maSV = tbMaSV.Text;
            string hoTen = tbHoTen.Text;
            string ngaySinh = tbNgaysinh.Text;
            string gioiTinh = rdoNam.Checked ? "Nam" : "Nữ";
            string diachi = tbDiaChi.Text;
            string sodt = tbSoDt.Text;
            string email = tbEmail.Text;
            string malophc = tbMaLopHC.Text;
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
                string query = "UPDATE SinhVien SET TenSV = @hoTen, NamSinh = @ngaySinh, GioiTinh = @gioiTinh, DiaChi = @diachi, SoDT = @sodt, Email = @email, MaLopHC = @malophc, MaKhoa = @maKhoa WHERE MaSV = @maSV";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@hoTen", hoTen);
                    cmd.Parameters.AddWithValue("@ngaySinh", ngaySinh);
                    cmd.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@diachi", diachi);
                    cmd.Parameters.AddWithValue("@sodt", sodt);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@malophc", malophc);
                    cmd.Parameters.AddWithValue("@maKhoa", maKhoa);
                    cmd.Parameters.AddWithValue("@maSV", maSV);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Cập nhật thông tin thành công");
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maSV = tbMaSV.Text;
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
                string query = "DELETE FROM SinhVien WHERE MaSV = @maSV";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSV", maSV);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Xóa thông tin thành công");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string maSV = tbMaSV.Text;
            string hoTen = tbHoTen.Text;
            string ngaySinh = tbNgaysinh.Text;
            string gioiTinh = rdoNam.Checked ? "Nam" : "Nữ";
            string diachi = tbDiaChi.Text;
            string sodt = tbSoDt.Text;
            string email = tbEmail.Text;
            string malophc = tbMaLopHC.Text;
            string maKhoa = cbbTimTheoKhoa.SelectedValue.ToString();
            string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";

            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO SinhVien (MaSV, TenSV, NamSinh, GioiTinh, DiaChi, SoDT, Email, MaLopHC, MaKhoa) VALUES (@maSV, @hoTen, @ngaySinh, @gioiTinh, @diachi,@sodt, @email, @malophc, @maKhoa)";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSV", maSV);
                    cmd.Parameters.AddWithValue("@hoTen", hoTen);
                    cmd.Parameters.AddWithValue("@ngaySinh", ngaySinh);
                    cmd.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@maKhoa", maKhoa);
                    cmd.Parameters.AddWithValue("@diachi", diachi);
                    cmd.Parameters.AddWithValue("@sodt", sodt);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@malophc", malophc);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Thêm thông tin thành công");
        }
    }
}
      