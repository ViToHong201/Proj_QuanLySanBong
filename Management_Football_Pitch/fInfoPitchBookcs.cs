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
    public partial class fInfoPitchBookcs : Form
    {
        public string id_book { get; set; }

        Pitch_Booked san;


        Management_FootballPitchEntities db = new Management_FootballPitchEntities();
        public fInfoPitchBookcs()
        {
            InitializeComponent();

            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.Text = string.Empty;
            this.ControlBox = false;
        }

        private void InfoPitchBookcs_Load(object sender, EventArgs e)
        {
            san = db.Pitch_Booked.Where(x => x.id_p_book == id_book).FirstOrDefault();


            var fp = db.Football_Pitch.Where(x => x.id_f_pitch == san.id_f_pitch).FirstOrDefault();
            int d = Convert.ToInt32(san.price.ToString().Replace(".0000", ""));
            string[] doanh_thu = d.ToString("N1").Split('.');
            lbAmount.Text = doanh_thu[0] + " VNĐ";

            lbAddress.Text = fp.address.ToString();
            lbDateUse.Text = san.date_use.Value.ToString("dd/MM/yyyy");
            lbMaHD.Text = san.id_p_book;
            lbPhone.Text = san.phone_orderer;
            lbPitchName.Text = fp.name_pitch;
            lbTimeSlot.Text = san.time_start.Value.ToString().Replace(":00,000000", " ") + " đến " + san.time_end.Value.ToString().Replace(":00,000000", " ");
            lbNameCustomer.Text = san.orderer;
            lbDatePay.Text = san.date_book.Value.ToString("dd/MM/yyyy");
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Cancel_Pitch c = new Cancel_Pitch();
            c.id_cancel_p_book = san.id_p_book;
            c.id_time_slot = san.id_time_slot;
            c.id_f_pitch = san.id_f_pitch;
            c.orderer = san.orderer;
            c.phone_orderer = san.phone_orderer;
            c.time_start = san.time_start;
            c.time_end = san.time_end;
            c.price = san.price;
            c.date_cancel = DateTime.Now;
            c.date_book = san.date_book;
            c.date_use = san.date_use;

            db.Cancel_Pitch.Add(c);
            db.Pitch_Booked.Remove(san);
            db.SaveChanges();
            MessageBox.Show("Hủy Thành Công !","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information );
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
