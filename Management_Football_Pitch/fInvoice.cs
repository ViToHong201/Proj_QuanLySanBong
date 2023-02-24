using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Management_Football_Pitch
{
    public partial class fInvoice : Form
    {
        Management_FootballPitchEntities db = new Management_FootballPitchEntities();

        public Pitch_Booked san { get; set; }
        public fInvoice()
        {
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }


        private void fInvoice_Load(object sender, EventArgs e)
        {
            var fp = db.Football_Pitch.Where(x => x.id_f_pitch == san.id_f_pitch).FirstOrDefault();
            int d = Convert.ToInt32(san.price.ToString().Replace(".0000", ""));
            string[] doanh_thu = d.ToString("N1").Split('.');
            lbAmount.Text = doanh_thu[0] + " VNĐ";

            lbAddress.Text = fp.address.ToString();
            lbDateUse.Text = san.date_use.Value.ToString("dd/MM/yyyy"); 
            lbMaHD.Text = san.id_p_book;
            lbPhone.Text = san.phone_orderer;
            lbPitchName.Text = fp.name_pitch;
            lbTimeSlot.Text = san.time_start.Value.ToString().Replace(":00,000000"," ")+" đến "+ san.time_end.Value.ToString().Replace(":00,000000", " ");
            lbNameCustomer.Text = san.orderer;
            lbDatePay.Text = san.date_book.Value.ToString("dd/MM/yyyy"); 
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            db.Pitch_Booked.Add(san);
            db.SaveChanges();

            this.Close();
        }
    }
}
