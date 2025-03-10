using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class formDangky : Form
    {
        private formDN fDN;
        private string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";

        public formDangky(formDN form)
        {
            InitializeComponent();
            fDN = form;
        }

        private void btnHUY_Click(object sender, EventArgs e)
        {
            this.Hide();
            fDN.Show();
        }

        private void btnDK_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDN.Text;
            string tenNguoiDung = txtTenND.Text;
            string loaiNguoiDung = cbbLoaiNguoiDung.SelectedItem.ToString();
            string email = txtEmail.Text;
            string matKhau = EncodePassword(txtpass.Text); 

            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string sqlInsert = $"INSERT INTO NguoiDung (TenNguoiDung, TenDangNhap, Email, Pass, LoaiND) VALUES ('{tenNguoiDung}', '{tenDangNhap}', '{email}', '{matKhau}', '{loaiNguoiDung}')";
                    using (OleDbCommand cmd = new OleDbCommand(sqlInsert, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Đăng ký thành công!");
                    this.Hide();
                    fDN.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi, vui lòng nhập lại thông tin!!!");
                }
            }
        }

        //ma hoa mat khau
        private string EncodePassword(string password)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
            string encodedPassword = Convert.ToBase64String(bytes);
            return encodedPassword;
        }
    }
}
