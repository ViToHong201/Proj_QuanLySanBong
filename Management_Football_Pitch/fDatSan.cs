using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Management_Football_Pitch
{
    public partial class fDatSan : Form
    {

        Management_FootballPitchEntities db = new Management_FootballPitchEntities();
        public fDatSan()
        {
            InitializeComponent();
        }

        private void fDatSan_Load(object sender, EventArgs e)
        {

            comboxSan.DataSource = db.Football_Pitch.Select(x=>x.name_pitch).ToList();
            cbToHour.SelectedIndex = 0;
            cbFromHour.SelectedIndex = 1;

            cbToMin.SelectedIndex = 0;
            cbFromMin.SelectedIndex =2;
        }

        private void btnDatSan_Click(object sender, EventArgs e)
        {
            if (txtTenKH.Text == "")
            {
                txtTenKH.BackColor = Color.MistyRose;
                txtTenKH.Focus();
                return;
            }
            if (txtPhone.Text == "")
            {
                txtPhone.BackColor = Color.MistyRose;
                txtPhone.Focus();
                return;
            }
            int phut = Convert.ToInt32(cbToMin.Text) + 1;
            string gio_bat_dau = cbToHour.Text + ":" + cbToMin.Text;
            string gio_bat_dau_parameter = cbToHour.Text + ":" + phut;
            string gio_ket_thuc = cbFromHour.Text + ":" + cbFromMin.Text;
            string sdt = txtPhone.Text;

            DateTime ngay_dung = Convert.ToDateTime((dateTimePickerLichDa.Value).ToString("yyyy/MM/dd"));
            string san = comboxSan.SelectedItem.ToString();
             //ngay_dung.ToString("yyyy/mm/dd");
            //ngay_dung = "2022-11-20";
            var a = db.CheckEmptyFootballPitch(gio_bat_dau_parameter, gio_ket_thuc,ngay_dung,san).First();

            if (a.Value == 0)
            {
                string tenkh = txtTenKH.Text;
                Pitch_Booked pb = new Pitch_Booked();
                var pitch = db.Football_Pitch.Where(x => x.name_pitch == comboxSan.Texts).First();
                string tmp = DateTime.Now.ToString("yyyy:MM:dd:hh:mm:ss");
                string id = tmp.Replace(":", "");

                pb.orderer = tenkh;
                pb.date_book = DateTime.Now;
                pb.date_use = ngay_dung;
                pb.time_start = TimeSpan.Parse(cbToHour.Text+":"+ cbToMin.Text);
                pb.time_end = TimeSpan.Parse(gio_ket_thuc);
                pb.id_f_pitch = pitch.id_f_pitch;
                pb.price = pitch.price_1;
                pb.id_p_book = id;
                pb.phone_orderer = sdt;
                //pb.Football_Pitch = pitch;
              
                fInvoice f = new fInvoice();
                f.san = pb;
                
                f.ShowDialog();
                //MessageBox.Show("Đặt Thành Công !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                string mes = "Khung Giờ: "+ gio_bat_dau + " đến "+ gio_ket_thuc + " đã được đặt";
                MessageBox.Show(mes,"Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
