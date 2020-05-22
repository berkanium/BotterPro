using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
 

namespace BOTTER
{
    public partial class UpdateControl : Form
    {
        public UpdateControl()
        {
            InitializeComponent();
        }
        private string destPath = Application.StartupPath + "\\dosya.exe";
        private string GuncelDosya = "http://domain.com/app/ue.exe";
        private string DosyaAdi;

        private void UpdateControl_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFileAsync(new Uri(GuncelDosya), destPath);

            string DosyaAdiUrlAdresi = GuncelDosya;
            int karaktersayisi = DosyaAdiUrlAdresi.LastIndexOf('/');
            DosyaAdi = DosyaAdiUrlAdresi.Remove(0, karaktersayisi + 1);
        }
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            ilerlemedurum_label.Text = DosyaAdi + " (%" + e.ProgressPercentage.ToString() + ")";
        }
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            ilerlemedurum_label.Text = "Tamamlandı";
        }
    }
}
