using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xemay.Db;
using System.Data.SqlClient;


namespace xemay
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbConfig db = new DbConfig();
        private void Form1_Load(object sender, EventArgs e)
        {
            dgvXeMay.DataSource = db.Table("select * from XeMay");
        }
        // kiểm tra xem dữ liệu đã có chưa
        public bool IsValidate()
        {
            if (txtSoKhung.Text.Trim() == "")
            {
                MessageBox.Show("Số khung không được để trống!");
                return false;
            }
            if (txtSoMay.Text.Trim() == "")
            {
                MessageBox.Show("Số máy không được để trống!");
                return false;
            }
            if (txtMau.Text.Trim() == "")
            {
                MessageBox.Show("Màu không được để trống!");
                return false;
            }
            if (ccbDungTichXiLanh.SelectedIndex == -1)
            {
                MessageBox.Show("Dung tích xi lanh chưa chọn!");
                return false;
            }
            if (txtHangXe.Text.Trim() == "")
            {
                MessageBox.Show("Hãng xe không được để trống!");
                return false;
            }
            if (txtTenXe.Text.Trim() == "")
            {
                MessageBox.Show("Tên xe không được để trống!");
                return false;
            }
            if (ptbAnh.Image == null)
            {
                MessageBox.Show("Ảnh không được để trống!");
                return false;
            }
            return true;    
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //kiểm tra dữ liệu đã điền hết chưa
            if (IsValidate() == true)
            {
                string query = "INSERT INTO XeMay VALUES(" +
                    $"'{txtSoKhung.Text.Trim()}'," +
                    $"'{txtSoMay.Text.Trim()}'," +
                    $"N'{txtMau.Text.Trim()}'," +
                    $"{ccbDungTichXiLanh.SelectedItem}," +
                    $"'{txtHangXe.Text.Trim()}'," +
                    $"N'{txtTenXe.Text.Trim()}'," +
                    $"'{ptbAnh.ImageLocation}'" +
                    ")";
                if (MessageBox.Show("Bạn có muốn thêm dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //nếu người dùng chọn yes
                    try
                    {
                        db.Excute(query);//thực hiện câu lệnh
                        MessageBox.Show("Thêm dữ liệu thành công!");
                        Form1_Load(sender, e);//Load lại dữ liệu datagridview
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Thêm dữ liệu thất bại" + ex.Message);
                    }
                }
            }
        }

        private void lbChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //Đặt tiêu đề cho Dialog
            openFileDialog.Title = "Chọn ảnh xe";

            //Chỉ cho sử dụng file ảnh có đuôi file .png, .jpg, .jpeg
            openFileDialog.Filter = "Image file(*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            //Chỉ chọn được 1 file
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //gán đường dẫn ảnh được chọn vào picture box để hiện ảnh
                ptbAnh.ImageLocation = openFileDialog.FileName;
            }
        }

        private void dgvXeMay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSoKhung.Text = dgvXeMay.CurrentRow.Cells[0].Value.ToString();
            txtSoMay.Text = dgvXeMay.CurrentRow.Cells[1].Value.ToString();
            txtMau.Text = dgvXeMay.CurrentRow.Cells[2].Value.ToString();
            ccbDungTichXiLanh.SelectedItem = dgvXeMay.CurrentRow.Cells[3].Value.ToString();
            txtHangXe.Text = dgvXeMay.CurrentRow.Cells[4].Value.ToString();
            txtTenXe.Text = dgvXeMay.CurrentRow.Cells[5].Value.ToString();
            ptbAnh.ImageLocation = dgvXeMay.CurrentRow.Cells[6].Value.ToString();

        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string query = $"SELECT * FROM XeMay WHERE TenXe like '%{txtTenXe.Text.Trim()}%'";
            dgvXeMay.DataSource = db.Table(query);
            if (db.Table(query).Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy tên xe " + txtTenXe.Text.Trim() + "!");
            }
        }
        private void Refresh()
        {
            txtHangXe.Text = "";
            txtSoKhung.Text = "";
            txtSoMay.Text = "";
            txtTenXe.Text = "";
            txtMau.Text = "";
            ccbDungTichXiLanh.SelectedIndex = -1;
            ptbAnh.Image = null;
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtSoKhung.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng chọn xe cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có muốn xóa dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    string query = $"DELETE FROM XeMay WHERE SoKhung = '{txtSoKhung.Text.Trim()}'";
                    db.Excute(query);//thực hiện câu lệnh
                    MessageBox.Show("Xóa dữ liệu thành công!");
                    Form1_Load(sender, e);//Load lại dữ liệu datagridview
                    Refresh();//Xóa các control trên form
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xóa dữ liệu thất bại" + ex.Message);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //kiểm tra dữ liệu đã điền hết chưa
            if (IsValidate() == true)
            {
                // Lấy giá trị của các trường từ các controls
                string soKhung = txtSoKhung.Text.Trim();
                string soMay = txtSoMay.Text.Trim();
                string mau = txtMau.Text.Trim();
                int dungTichXiLanh = Convert.ToInt32(ccbDungTichXiLanh.SelectedItem);
                string hangXe = txtHangXe.Text.Trim();
                string tenXe = txtTenXe.Text.Trim();
                string anh = ptbAnh.ImageLocation;
                string query = $"UPDATE XeMay SET SoKhung = '{soKhung}', SoMay = '{soMay}', Mau = N'{mau}', DungTichXiLanh = {dungTichXiLanh}, HangXe = '{hangXe}', TenXe = N'{tenXe}', Anh = '{anh}' WHERE SoKhung = '{soKhung}'";

                if (MessageBox.Show("Bạn có muốn sửa dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //nếu người dùng chọn yes
                    try
                    {
                        db.Excute(query);//thực hiện câu lệnh
                        MessageBox.Show("Sửa dữ liệu thành công!");
                        Form1_Load(sender, e);//Load lại dữ liệu datagridview
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Sửa dữ liệu thất bại" + ex.Message);
                    }
                }
            }
        }

        private void btnXuatTheoHang_Click(object sender, EventArgs e)
        {

        }
    }
 }

