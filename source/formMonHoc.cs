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
    public partial class formMonHoc : Form
    {
        public formMonHoc()
        {
            InitializeComponent();

            this.Load += new EventHandler(formMonHoc_Load);
        }
        private void formMonHoc_Load(object sender, EventArgs e)
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
            string maMon = tbTimKiemTheoTen.Text;
            string maKhoa = cbbTimTheoKhoa.SelectedValue.ToString();
            string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM MonHoc WHERE MaMon = @maMon AND MaKhoa = @maKhoa";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@maSV", maMon);
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
                string query = "SELECT * FROM MonHoc";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {

                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvSV.DataSource = dt;

                }
            }
        }

        //sua
        private void button3_Click(object sender, EventArgs e)
        {
            string maMon = tbMaMon.Text;
            string tenmon = tbTenMon.Text;
            string sotc = tbSoTC.Text;
            string makhoa = tbMaKhoa.Text;
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
                string query = "UPDATE MonHoc SET  TenMon = @tenmon, SoTinChi = @sotc, MaKhoa = @makhoa WHERE MaMon = @mamon";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@tenmon", tenmon);
                    cmd.Parameters.AddWithValue("@sotc", sotc);
                    cmd.Parameters.AddWithValue("@makhoa", makhoa);
                    cmd.Parameters.AddWithValue("@mamon", maMon);

                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Cập nhật môn thành công");
        }

        private void dgvSV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvSV.Rows[e.RowIndex];

                tbMaMon.Text = row.Cells["MaMon"].Value.ToString();
                tbTenMon.Text = row.Cells["TenMon"].Value.ToString();
                tbSoTC.Text = row.Cells["SoTinChi"].Value.ToString();
                tbMaKhoa.Text = row.Cells["MaKhoa"].Value.ToString();

            }
        }
        //xóa
        private void button1_Click(object sender, EventArgs e)
        {
            string mamon = tbMaMon.Text;
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
                string query = "DELETE FROM MonHoc WHERE MaMon = @mamon";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@mamon", mamon);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Xóa thông tin thành công");
        }
        //Thêm
        private void button2_Click(object sender, EventArgs e)
        {
            string mamon = tbMaMon.Text;
            string tenmon = tbTenMon.Text;
            string sotc = tbSoTC.Text;
            string makhoa = tbMaKhoa.Text;
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
                string query = "INSERT INTO MonHoc (TenMon, SoTinChi, MaKhoa, MaMon) VALUES (@tenmon, @sotc, @makhoa, @mamon)";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@tenmon", tenmon);
                    cmd.Parameters.AddWithValue("@sotc", sotc);
                    cmd.Parameters.AddWithValue("@makhoa", makhoa);
                    cmd.Parameters.AddWithValue("@mamon", mamon);

                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Thêm môn thành công");
        }
    }
}
