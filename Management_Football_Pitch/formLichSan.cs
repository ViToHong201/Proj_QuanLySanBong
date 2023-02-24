using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Management_Football_Pitch
{
    public partial class fLichSan : Form
    {
        Random rn;
        Management_FootballPitchEntities db = new Management_FootballPitchEntities();
        

        public fLichSan()
        {
            rn = new Random();
            InitializeComponent();
        }

        private void fLichSan_Load(object sender, EventArgs e)
        {
            KhoiTaoSan(GetValueDay());
        }

        private List<Pitch_Booked> GetValueDay()
        {
            string[] dt = dateTimePicker1.Value.GetDateTimeFormats('g');
            string[] date = dt[26].Split(' ');
            DateTime day = Convert.ToDateTime(date[0]);
            var a = db.Pitch_Booked.Where(x => x.date_use == day & x.state.ToLower()!="huy").ToList();

            return a;
        }

        public void btn_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            fInfoPitchBookcs f = new fInfoPitchBookcs();
            f.id_book = btn.Tag.ToString();
            f.ShowDialog();
            KhoiTaoSan(GetValueDay());


        }

        public void KhoiTaoSan(List<Pitch_Booked> ls_p)
        {
            panel4.Controls.Clear();
            panel6.Controls.Clear();
            ToolTip toolTip1 = new ToolTip();

            //var d = ls_p.OrderByDescending(x => x.Football_Pitch.id_f_pitch).ToList();
            var ls_san = db.Football_Pitch.OrderByDescending(x=>x.name_pitch).ToList();
            

            foreach (var san in ls_san)
            {
                int a = rn.Next(100, 170);
                int b = rn.Next(100, 170);
                int c = rn.Next(100, 170);
                Button btn = new Button();
                btn.Text = san.name_pitch;
                btn.Dock = DockStyle.Top;
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = Color.FromArgb(a, b, c);
                btn.Margin = new Padding(0, 0, 0, 5);
                btn.Height = 70;
                btn.Font = new Font("Microsoft Sans Serif", 12F);
                btn.ForeColor = Color.FromName("white");

                toolTip1.AutoPopDelay = 10000;
                toolTip1.InitialDelay = 100;
                toolTip1.ReshowDelay = 100;
                // Force the ToolTip text to be displayed whether or not the form is active.
                toolTip1.ShowAlways = true;
                toolTip1.SetToolTip(btn, "DC: " + san.address + "\r\n" + "Loại Sân: " + san.type_pitch);

                panel6.Controls.Add(btn);

                
                TableLayoutPanel tableLayoutPanel2 = new TableLayoutPanel();
                tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 50F));
                tableLayoutPanel2.ColumnCount = 64;
                tableLayoutPanel2.RowCount = 1;
                for (int i = 0; i < 65; i++)
                {
                    tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 50F));

                }

                tableLayoutPanel2.Size = new Size(1072, 70);
                tableLayoutPanel2.Dock = DockStyle.Top;
                tableLayoutPanel2.Margin = new Padding(0);
                panel4.Controls.Add(tableLayoutPanel2);


                var gio_san = ls_p.Where(x => x.Football_Pitch.id_f_pitch == san.id_f_pitch).ToList();

                foreach (var item1 in gio_san)
                {
                    var time_start = item1.time_start.Value;

                    int gio_bat_dau = time_start.Hours;
                    int phut_bat_dau = time_start.Minutes;

                    var time_end = item1.time_end.Value;

                    int gio_kt = time_end.Hours;
                    int phut_kt = time_end.Minutes;

                    // vị trí bắt đầu (gio -7)*4 + (phut / 15)
                    // vị trí kết thúc (gio -7)*4 + (phut / 15) - 1

                    int vitri_bat_dau = (gio_bat_dau - 7) * 4 + (phut_bat_dau / 15);
                    int vitri_ket_thuc = (gio_kt - 7) * 4 + (phut_kt / 15) - 1;

                    var thoi_gian_dung = item1.time_end - item1.time_start;

                    HienThiGio(tableLayoutPanel2, vitri_bat_dau, vitri_ket_thuc, item1.orderer, item1.id_p_book,item1.phone_orderer);
                }

                

            }

           
        }

        public void HienThiGio(TableLayoutPanel tableLayoutPanel2, int vitridau , int vitricuoi, string ngươidat, string id_book, string sdt)
        {
            ToolTip toolTip1 = new ToolTip();

            toolTip1.AutoPopDelay = 10000;
            toolTip1.InitialDelay = 100;
            toolTip1.ReshowDelay = 100;
            toolTip1.ShowAlways = true;
            

            var a = rn.Next(0, 255);
            var b = rn.Next(0, 255);
            var c = rn.Next(0, 255);

            int so_btn = vitricuoi - vitridau;
            for (int i = 0; i != so_btn+1; i++)
            {
                Button btn = new Button();
                btn.Margin = new Padding(0, 2, 0, 2);
                btn.BackColor = Color.FromArgb(a, b, c);
                btn.Dock = DockStyle.Fill;
                btn.FlatAppearance.BorderSize = 0;
                btn.Tag = id_book;
                btn.FlatStyle = FlatStyle.Flat;
                btn.Click += new EventHandler(btn_click);

                int giobd = (vitridau / 4 + 7);
                int phutbd = (vitridau % 4 * 15);

                int giokt = (int)((vitricuoi+1) / 4 + 7);
                int phutkt = ((vitricuoi + 1) % 4 * 15);

                string gio_bd = giobd + "h" + phutbd;
                string gio_kt = giokt + "h" + phutkt;
                toolTip1.SetToolTip(btn, "Giờ Đăt:"+gio_bd+" - "+gio_kt+"\r\nNgười Đặt: "+ngươidat+ "\r\nSĐT: " + sdt);

                tableLayoutPanel2.SetRow(btn, 0);
                tableLayoutPanel2.SetColumn(btn,vitridau+i);
                tableLayoutPanel2.Controls.Add(btn);
            }

           
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            KhoiTaoSan(GetValueDay());
        }
    }
}
