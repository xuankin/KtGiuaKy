using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using De01.Models;

namespace De01
{
    
    public partial class Form1 : Form
    {
        List<SinhVien> lstSinhVien = new List<SinhVien>();
        List<Lop> lstLop = new List<Lop>();
        StudentDBContext context = new StudentDBContext();
        SinhVien SelectedStudent;
        public Form1()
        {
            InitializeComponent();
        }
        private void fillCBox()
        {
            cbLop.DataSource = lstLop;
            cbLop.ValueMember = "MaLop";
            cbLop.DisplayMember = "TenLop";

            
        }
        private void LoadData()
        {
            lstSinhVien = context.SinhViens.ToList();
            lstLop = context.Lops.ToList();
            List<ViewModel> lstVD = new List<ViewModel>();
            foreach(var item in lstSinhVien)
            {
                ViewModel vd = new ViewModel();
                vd.MaSv = item.MaSv;
                vd.HoTenSV= item.HoTenSV;
                vd.TenLop=item.Lop.TenLop;
                vd.NgaySinh= item.NgaySinh;
                lstVD.Add(vd);
            }
            dtgSinhVien.DataSource = lstVD;
        }
        private void ClearForm()
        {
            txtTenSinhVien.Clear();
            txtMaSinhVien.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            cbLop.SelectedIndex = -1;
            btnThem.Enabled = true;  
            btnSua.Enabled = true;   
            btnXoa.Enabled = true;   
            btnLuu.Enabled = false;  
            btnHuy.Enabled = false;

        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void dtgSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string selectedID = dtgSinhVien.Rows[e.RowIndex].Cells[0].Value.ToString();
                SelectedStudent = context.SinhViens.FirstOrDefault(s => s.MaSv == selectedID);
                if (SelectedStudent != null)
                {
                    txtMaSinhVien.Text = SelectedStudent.MaSv;
                    txtTenSinhVien.Text = SelectedStudent.HoTenSV;
                    dtpNgaySinh.Value = Convert.ToDateTime(SelectedStudent.NgaySinh);
                    cbLop.SelectedValue = SelectedStudent.MaLop;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            fillCBox();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string search = txtTimKiem.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(search))
            {
                var result = context.SinhViens
                            .Where(s => s.HoTenSV.Contains(search))
                            .ToList();
                List<ViewModel> lmd = new List<ViewModel>();
                foreach(var item in result)
                {
                    ViewModel vd = new ViewModel();
                    vd.MaSv = item.MaSv;
                    vd.HoTenSV = item.HoTenSV;
                    vd.NgaySinh = item.NgaySinh;
                    vd.TenLop =item.Lop.MaLop;
                    lmd.Add(vd);
                }
                dtgSinhVien.DataSource = lmd;
            }
            else
            {
                MessageBox.Show("Khong tim thay");
                LoadData();
                ClearForm() ;
            }
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Ban co chac chan thoat", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (rs == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == true)
            {
                try
                {
                    SinhVien sv = new SinhVien();
                    sv.MaSv = txtMaSinhVien.Text;
                    sv.HoTenSV = txtTenSinhVien.Text;
                    sv.MaLop = cbLop.SelectedValue.ToString();
                    sv.NgaySinh = dtpNgaySinh.Value;
                    context.SinhViens.Add(sv);
                    context.SaveChanges();
                    LoadData();
                    ClearForm();
                    MessageBox.Show("Them sinh vien thanh cong");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else if (btnSua.Enabled == true)
            {
                try
                {
                    if (SelectedStudent != null)
                    {
                        SelectedStudent.MaSv = txtMaSinhVien.Text;
                        SelectedStudent.HoTenSV = txtTenSinhVien.Text;
                        SelectedStudent.MaLop = cbLop.SelectedValue.ToString();
                        SelectedStudent.NgaySinh = dtpNgaySinh.Value;
                        context.SaveChanges();
                        LoadData();
                        ClearForm();
                        MessageBox.Show("Sua sinh vien thanh cong");
                    }
                    else
                    {
                        MessageBox.Show("Vui long chon sinh vien de sua");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (btnXoa.Enabled == true)
            {
                try
                {
                    if (SelectedStudent != null)
                    {
                        DialogResult rs = MessageBox.Show("Ban co chac muon xoa sinh vien co ma" + SelectedStudent.MaSv, "Xac Nhan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (rs == DialogResult.Yes)
                        {
                            context.SinhViens.Remove(SelectedStudent);
                            context.SaveChanges();
                            MessageBox.Show("Xoa sinh vien thanh cong");
                        }
                        LoadData();
                        ClearForm();
                        
                    }
                    else
                    {
                        MessageBox.Show("Vui long chon sinh vien de xoa");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            }
            
        

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}
