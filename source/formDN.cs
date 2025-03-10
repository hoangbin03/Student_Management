using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class formDN : Form
    {
        private formDangky fDK;
        private string connStr = @"Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\QuanLySinhVien1.mdb;Persist Security Info=False";
        formMain fMain;
        public formDN()
        {
            InitializeComponent();
            fDK = new formDangky(this);
            fMain = new formMain();
        }

        private void btnDK_Click(object sender, EventArgs e)
        {
            this.Hide();
            fDK.ShowDialog();
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDN.Text;
            string matKhau = EncodePassword(txtPass.Text); 

            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM NguoiDung WHERE TenDangNhap = @username AND Pass = @password";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", tenDangNhap);
                        cmd.Parameters.AddWithValue("@password", matKhau);
                        OleDbDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Close();
                            MessageBox.Show("Đăng nhập thành công");
                            this.Hide();
                            fMain.Show();
                        }
                        else
                        {
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thực hiện thao tác với cơ sở dữ liệu: {ex.Message}");
                }
            }
        }

        private string EncodePassword(string password)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
            string encodedPassword = Convert.ToBase64String(bytes);
            return encodedPassword;
        }
    }
}
