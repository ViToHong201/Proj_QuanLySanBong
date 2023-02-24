using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Series = DevExpress.XtraCharts.Series;

namespace Management_Football_Pitch
{
    public partial class fStatistical : Form 
    {
        Management_FootballPitchEntities db = new Management_FootballPitchEntities();
        DateTime Today;
        string san;
        Button currentButton;
        
        public fStatistical()
        {
            InitializeComponent();
        }

        private void fStatistical_Load(object sender, EventArgs e)  
        {
            btnOk.Hide();
            comboxSan.Hide();
            Today = Convert.ToDateTime(dtStartDay.Text);
            comboxSan.DataSource = db.Football_Pitch.Select(x => x.name_pitch).ToList();
            label3.Text = dtStartDay.Text;
            label5.Text = dtEndDay.Text;
            DateTime startDate = Convert.ToDateTime(label3.Text);
            DateTime endDate = Convert.ToDateTime(label5.Text);
            LayDoanhThu(startDate,endDate);
            LaySoLuongDat(startDate, endDate);
            LaySoGioDat(startDate, endDate);

           
        }


        private void label3_Click(object sender, EventArgs e)
        {
            dtStartDay.Select();
            SendKeys.Send("%{DOWN}");
        }

        private void label5_Click(object sender, EventArgs e)
        {
            dtEndDay.Select();
            SendKeys.Send("%{DOWN}");

        }

        private void dtStartDay_ValueChanged(object sender, EventArgs e)
        {
            label3.Text = dtStartDay.Text;
        }

        private void dtEndDay_ValueChanged(object sender, EventArgs e)
        {
            label5.Text = dtEndDay.Text;
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            label3.Text = Today.ToString("dd/MM/yyyy");
            if (checkBox1.Checked)
            {
                san = "all";

            }
            else
            {
                san = comboxSan.SelectedItem.ToString();
            }
            DateTime startDate =Today;
            DateTime endDate =Today ;
            LayDoanhThu(startDate, endDate,san);
            LaySoGioDat(startDate, endDate, san);
            LaySoLuongDat(startDate, endDate, san);

        }

        private void btnLast7Day_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            label3.Text = Today.AddDays(-7).ToString("dd/MM/yyyy");

            if (checkBox1.Checked)
            {
                san = "all";

            }
            else
            {
                san = comboxSan.SelectedItem.ToString();
            }
            DateTime startDate = Today.AddDays(-7);
            DateTime endDate = Today;
            LayDoanhThu(startDate, endDate,san);
            LaySoGioDat(startDate, endDate, san);
            LaySoLuongDat(startDate, endDate, san);

        }

        private void btnLast30Day_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            label3.Text = Today.AddDays(-30).ToString("dd/MM/yyyy");

            if (checkBox1.Checked)
            {
                san = "all";

            }
            else
            {
                san = comboxSan.SelectedItem.ToString();
            }
            DateTime startDate = Today.AddDays(-30);
            DateTime endDate = Today;
            LayDoanhThu(startDate, endDate,san);
            LaySoGioDat(startDate, endDate, san);
            LaySoLuongDat(startDate, endDate, san);

        }

        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            int month = Today.Month;
            DateTime startDate = Convert.ToDateTime("01-"+month+"-2022");
            DateTime endDate = Today;
            if (checkBox1.Checked)
            {
                san = "all";

            }
            else
            {
                san = comboxSan.SelectedItem.ToString();
            }
            label3.Text = startDate.ToString("dd/MM/yyyy");
            LayDoanhThu(startDate, endDate,san);
            LaySoLuongDat(startDate, endDate, san);
            LaySoGioDat(startDate, endDate, san);
            ActivateButton(sender);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                san = "all";

            }
            else
            {
                san = comboxSan.SelectedItem.ToString();
            }

            DateTime startDate = Convert.ToDateTime(label3.Text);
            DateTime endDate = Convert.ToDateTime(label5.Text);
            LayDoanhThu(startDate, endDate,san);
            LaySoLuongDat(startDate, endDate, san);
            LaySoGioDat(startDate, endDate, san);

            AnNutOk();
           
        }

       
        private void btnCustom_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            btnOk.Show();
            dtStartDay.Enabled = true;
            dtEndDay.Enabled = true;
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                comboxSan.Hide();
            }
            else
            {
                comboxSan.Show();
            }
        }

        /////////////////
        
        public void LaySoSanHuy(DateTime startDate, DateTime endDate)
        {

            DateTime s_day = Convert.ToDateTime(startDate.ToString("yyyy/MM/dd"));
            DateTime e_day = Convert.ToDateTime(endDate.AddDays(1).ToString("yyyy/MM/dd"));
            var soSanHuy = db.Cancel_Pitch.Where(x=>x.date_cancel>= s_day & x.date_cancel<= e_day).Count();

            lbHuySan.Text = "NaN";

            if (soSanHuy != 0)
            {
                lbHuySan.Text = soSanHuy.ToString() ;
            }

        }

        public void LayKhungGioHot(DateTime startDate, DateTime endDate)
        {
           
            var khungGio = db.GetTimeSlotBooked(startDate.ToString("yyyy/MM/dd"),endDate.ToString("yyyy/MM/dd")).FirstOrDefault();

            lbKhungGio.Text ="NaN";

            if (khungGio != null)
            {
                string[] gio = khungGio.Column1.Replace(":00.0000000", " ").Split(' ');

                lbKhungGio.Text = gio[0] + " đến " + gio[1];
            }
           
        }

        public void LaySanDatNhieu(DateTime startDate, DateTime endDate)
        {

            var san = db.GetSumTimeBooked(startDate.ToString("yyyy/MM/dd"), endDate.ToString("yyyy/MM/dd")).OrderByDescending(x=>x.Số_Phút_Đặt).FirstOrDefault();
            lbSan.Text = "NaN";
            if (san!= null)
            {
                lbSan.Text = san.name_pitch;
            }
        }

        public void LayDoanhThu(DateTime startDate, DateTime endDate, string san = "all")
        {
            AnNutOk();
            LayKhungGioHot(startDate,endDate);
            LaySanDatNhieu(startDate,endDate);
            LaySoSanHuy(startDate, endDate);

            List<Pitch_Booked> doanhThu;
            List<Pitch_Booked> soLuotDat;
            if (san!="all")
            {
                  doanhThu = db.Pitch_Booked.Where(x => x.date_use >= startDate & x.date_use <= endDate & x.Football_Pitch.name_pitch ==san).ToList();
                  soLuotDat = db.Pitch_Booked.Where(x => x.date_use >= startDate & x.date_use <= endDate & x.Football_Pitch.name_pitch == san).ToList();

            }
            else
            {
                 doanhThu = db.Pitch_Booked.Where(x => x.date_use >= startDate & x.date_use <= endDate).ToList();
                soLuotDat = db.Pitch_Booked.Where(x => x.date_use >= startDate & x.date_use <= endDate).ToList();

            }


            var b = doanhThu.GroupBy(x => x.date_use).Select(x => new
            {
                date_use = x.Key.Value.ToString("dd/MM"),
                price = x.Select(xx => xx.price).Sum(xx => xx.Value)
            });

            chart1.DataSource = b;
            chart1.Series["Doanh Thu"].XValueMember = "date_use";
            chart1.Series["Doanh Thu"].YValueMembers = "price";

            chart1.DataBind();


            chart3.DataSource = b;
            chart3.Series["Doanh Thu"].XValueMember = "date_use";
            chart3.Series["Doanh Thu"].YValueMembers = "price";

            chart3.DataBind();

            var tong = doanhThu.Sum(x => x.price.Value);

            int d = Convert.ToInt32(tong.ToString().Replace(".0000", ""));

            string[] doanh_thu = d.ToString("N1").Split('.');
            lbDoanhThu.Text = doanh_thu[0] + " VNĐ";

        }

        public void LaySoLuongDat(DateTime startDate, DateTime endDate, string san = "all")
        {
            List<Pitch_Booked> soLuotDat;
            if (san != "all")
            {
                soLuotDat = db.Pitch_Booked.Where(x => x.date_use >= startDate & x.date_use <= endDate & x.Football_Pitch.name_pitch == san).ToList();
            }
            else
            {
                soLuotDat = db.Pitch_Booked.Where(x => x.date_use >= startDate & x.date_use <= endDate).ToList();

            }

            var b = soLuotDat.GroupBy(x => x.orderer).Select(x => new {
                orderer = x.Key.ToString(),
                id_p_book = x.Select(xx => xx.id_p_book).Count()
            }).Take(5);
            chart2.DataSource = b;
            chart2.Series["Doanh Thu"].XValueMember = "orderer";
            chart2.Series["Doanh Thu"].YValueMembers = "id_p_book";

            chart2.DataBind();


            lbSoLanDat.Text = soLuotDat.Select(x=>x.id_p_book).Count().ToString();
        }

        public void LaySoGioDat(DateTime startDate, DateTime endDate, string san = "all")
        {

            string start_Date = startDate.ToString("yyyy/MM/dd");
            string end_Date = endDate.AddHours(24).ToString("yyyy/MM/dd hh:mm:ss");
            var soGioDat_ALL = db.GetSumTimeBooked(start_Date, end_Date).ToList();


            var soGioDat_SAN = soGioDat_ALL;
            if (san != "all")
            {
                 soGioDat_SAN = soGioDat_ALL.Where(x => x.name_pitch == san).ToList();

            }
          
            int tongGioDat =0;
            foreach (var item in soGioDat_SAN)
            {
                tongGioDat += item.Số_Phút_Đặt.Value;
            }

            lbSoGioDat.Text = Convert.ToInt32(tongGioDat / 60) + " Giờ " + Convert.ToInt32(tongGioDat % 60)+" Phút";

        }

        private void DisableButton()
        {
            foreach (Control previousBtn in pnTop.Controls)
            {
                if (previousBtn.Tag!=null )
                {
                    if (previousBtn.Tag.ToString() == "btn")
                    {
                        previousBtn.BackColor = Color.MediumSlateBlue;
                    }
                }
               
            }
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = Color.Salmon;
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
              
                }
            }
        }

        private void AnNutOk()
        {
            btnOk.Hide();
            dtStartDay.Enabled = false;
            dtEndDay.Enabled = false;
        }




    }
}
