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
    public partial class formDiem : Form
    {
        public formDiem()
        {
            InitializeComponent();

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maLop = tbTimKiemTheoMa.Text;
            string maSV = tbTimMaSV.Text;
            string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Diem WHERE MaLopHP = @maLop AND MaSV = @maSV";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@maLop", maLop);
                    cmd.Parameters.AddWithValue("@maSV", maSV);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvDiem.DataSource = dt;

                }
            }
        }

        private void btnTatCa_Click(object sender, EventArgs e)
        {

            string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Diem";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {

                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvDiem.DataSource = dt;

                }
            }
        }

        private void dgvDiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvDiem.Rows[e.RowIndex];

                tbMaDiem.Text = row.Cells["MaDiem"].Value.ToString();
                tbMaSV.Text = row.Cells["MaSV"].Value.ToString();
                tbMaLop.Text = row.Cells["MalopHP"].Value.ToString();
                tbMaMon.Text = row.Cells["MaMon"].Value.ToString();
                tbDiemCC.Text = row.Cells["Diem10"].Value.ToString();
                tbDiemGK.Text = row.Cells["Diem30"].Value.ToString();
                tbDiemThi.Text = row.Cells["Diem60"].Value.ToString();

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maDiem = tbMaDiem.Text;
            string malop = tbMaLop.Text;
            string maSV = tbMaSV.Text;
            string maMon = tbMaMon.Text;
            double diemCC = double.Parse(tbDiemCC.Text);
            double diemGK = double.Parse(tbDiemGK.Text);
            double diemThi = double.Parse(tbDiemThi.Text);
            double tongdiem = (0.1 * diemCC) + (0.3 * diemGK) + (0.6 * diemThi);
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
                string query = "UPDATE Diem SET MaSV = @maSV, MaMon = @maMon, Diem10 = @diemCC, Diem30 = @diemGK, Diem60 = @diemThi, TongDiem = @tongDiem, MaLopHP = @malop WHERE MaDiem = @maDiem";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@maSV", maSV);
                    cmd.Parameters.AddWithValue("@maMon", maMon);
                    cmd.Parameters.AddWithValue("@diemCC", diemCC);
                    cmd.Parameters.AddWithValue("@diemGK", diemGK);
                    cmd.Parameters.AddWithValue("@diemThi", diemThi);
                    cmd.Parameters.AddWithValue("@tongDiem", tongdiem);
                    cmd.Parameters.AddWithValue("@malop", malop);
                    cmd.Parameters.AddWithValue("@maDiem", maDiem);

                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Cập nhật điểm thành công");
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            string madiem = tbMaDiem.Text;
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
                string query = "DELETE FROM Diem WHERE MaDiem = @madiem";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@madiem", madiem);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Xóa thông tin thành công");
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            string malop = tbMaLop.Text;
            string maSV = tbMaSV.Text;
            string maMon = tbMaMon.Text;
            double diemCC = double.Parse(tbDiemCC.Text);
            double diemGK = double.Parse(tbDiemGK.Text);
            double diemThi = double.Parse(tbDiemThi.Text);
            double tongdiem = (0.1 * diemCC) + (0.3 * diemGK) + (0.6 * diemThi);
            string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";

            // Xác nhận việc cập nhật
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn thêm điểm không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO Diem (MaMon, Diem10, Diem30, Diem60, TongDiem, MaLopHP, MaSV) VALUES (@maMon, @diemCC, @diemGK, @diemThi, @tongDiem, @maLop, @maSV)";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {

                    
                    cmd.Parameters.AddWithValue("@maMon", maMon);
                    cmd.Parameters.AddWithValue("@diemCC", diemCC);
                    cmd.Parameters.AddWithValue("@diemGK", diemGK);
                    cmd.Parameters.AddWithValue("@diemThi", diemThi);
                    cmd.Parameters.AddWithValue("@tongDiem", tongdiem);
                    cmd.Parameters.AddWithValue("@malop", malop);
                    cmd.Parameters.AddWithValue("@maSV", maSV);

                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Thêm điểm thành công");
        }
    }
}
