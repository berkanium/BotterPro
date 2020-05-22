using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace BOTTER
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateControl();
        }
        private void UpdateControl()
        {
            if (CheckUpdate())
            {
                DialogResult dialog = MessageBox.Show("Yeni güncellemeler var. \n\rŞimdi Yüklemek istermisiniz?", "Güncelleme Bulundu", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialog == DialogResult.Yes)
                {
                    System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(updateMe));
                    t.Start(); this.Close();
                }
            }

        }
        public static void updateMe()
        {
            Application.Run(new UpdateControl());
        }
        private Boolean CheckUpdate()
        {
            Boolean ret;
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead("http://domain.com/app/check.php?v=" + Program.versionCode); //Burdaki domain.com kısmını kendinize göre düzenlemeniz gerek. Artık sunucunuzun ip numarasını yada varsa domain adresinizi yazıp php dosyasını nereye attıysanız onun yolunu bu kısma yazmanız gerek. check.php?v= kısmından sonra yazan kod ise Program.csde tanımladığımız program versionunu çekmeye yarıyor.
                StreamReader reader = new StreamReader(stream);
                String content = reader.ReadToEnd();

                if (content == "UPDATE") //Dönen veriyi kontrol ediyor. Dönen veri UPDATE ise güncelleme aşamasına geçiyor.         
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }

            }
            catch
            {
                ret = false;

            }
            return ret;
        }
        
         int dongu = 0;
        private void btnBaslat_Click(object sender, EventArgs e)
        {
            if (txtMetin.Text!="")
            {
                System.Threading.Thread.Sleep(3500);// Programı 3.5 Saniye Uykuya Alacak.
                numericUpDown1.Enabled = false;
                txtMetin.Enabled = false;
                trackBar1.Enabled = false;
                btnBaslat.Enabled = false;
                tmrControl.Interval = trackBar1.Value; //tmr hızını trackbar a göre ayarlıyoruz.
                tmrControl.Enabled = true;
               
            }
            else
            {
                MessageBox.Show("Metin Girişi Yapınız.","BOTTER",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            hiz_text.Text = trackBar1.Value.ToString() + " ms ";
        }

        private void tmrControl_Tick(object sender, EventArgs e)
        {
            if (dongu<=Convert.ToInt32(numericUpDown1.Value))//dinamik dongu değerini  her seferinde kontrol ediyoruz değer tekrarlama değerinden küçük ise işlem tamamlanıyor.
            {
                SendKeys.Send(txtMetin.Text+"\n"); //ekrana metni yazdırmak için sendkeys methodunu kullandım
                dongu += 1; //döngüyü tamamlamak için her seferinde döngüyü 1 arttırıyoz
            }
            if (dongu==Convert.ToInt32(numericUpDown1.Value))
            {
                tmrControl.Enabled = false;
                dongu = 0;
                numericUpDown1.Enabled = true;
                txtMetin.Enabled = true;
                trackBar1.Enabled = true;
                btnBaslat.Enabled = true;
             
                MessageBox.Show("İşlem Tamamlandı.","BOTTER",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void btnDurdur_Click(object sender, EventArgs e)
        {
           
            tmrControl.Enabled = false; 
            dongu = 0;
            numericUpDown1.Enabled = true;
            txtMetin.Enabled = true;
            trackBar1.Enabled = true;
            btnBaslat.Enabled = true;
            MessageBox.Show("İşlem İptal Edildi.","BOTTER",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

      

       

       
    }
}
