using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class formMain : Form
    {
        formDiem fDiem;
        formGV fGV;
        formLopHC fLop;
        formLopTC fLopTC;
        formMonHoc fMonHoc;
        formSV fSV;
        baocaoo fbaocao;
        public formMain()
        {
            InitializeComponent();

            this.IsMdiContainer = true;

            fDiem = new formDiem();
            fGV = new formGV();
            fLop = new formLopHC();
            fLopTC = new formLopTC();
            fMonHoc = new formMonHoc();
            fSV = new formSV();
            fbaocao = new baocaoo();

            fDiem.MdiParent = this; fDiem.Dock = DockStyle.Fill;

            fGV.MdiParent = this; fGV.Dock = DockStyle.Fill;

            fLop.MdiParent = this; fLop.Dock = DockStyle.Fill;

            fLopTC.MdiParent = this; fLopTC.Dock = DockStyle.Fill;

            fMonHoc.MdiParent = this; fMonHoc.Dock = DockStyle.Fill;

            fSV.MdiParent = this; fSV.Dock = DockStyle.Fill;

            fbaocao.MdiParent = this; fbaocao.Dock = DockStyle.Fill;

            setAllItemMenuLeftColorDefault();
            hideAllForm();


        }
        private void hideAllForm()
        {
            fDiem.Hide();
            fGV.Hide();
            fLop.Hide();
            fMonHoc.Hide();
            fSV.Hide();
            fbaocao.Hide();
        }


        
        private void setAllItemMenuLeftColorDefault()
        {

           
        }


        private void btnDiem_Click(object sender, EventArgs e)
        {
            setAllItemMenuLeftColorDefault();
            hideAllForm();
            fDiem.Show();
        }

        private void báoCáoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setAllItemMenuLeftColorDefault();
            hideAllForm();
            fbaocao.Show();
        }

        private void btnLopHC_Click(object sender, EventArgs e)
        {
            setAllItemMenuLeftColorDefault();
            hideAllForm();
            fLop.Show();
        }

        private void btnLopHP_Click(object sender, EventArgs e)
        {
            setAllItemMenuLeftColorDefault();
            hideAllForm();
            fLopTC.Show();
        }

        private void btnMonHoc_Click(object sender, EventArgs e)
        {
            setAllItemMenuLeftColorDefault();
            hideAllForm();
            fMonHoc.Show();
        }

        private void btnSV_Click(object sender, EventArgs e)
        {
            setAllItemMenuLeftColorDefault();
            hideAllForm();
            fSV.Show();
        }

        private void btnGV_Click_1(object sender, EventArgs e)
        {
            setAllItemMenuLeftColorDefault();
            hideAllForm();
            fGV.Show();
        }

    }
}

       

