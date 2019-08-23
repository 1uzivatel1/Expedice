using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace PocetKsPaleta
{
    public partial class Form1 : Form
    {
        Adress adress = new Adress();
        string text = "";
        string adresTextReceiver = "";

        Context cntx = new Context();

        string adresText;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            text = "";
            richTextBoxResult.Clear();

            Palette palette = new Palette();
            palette.Apalette = double.Parse(textBoxPalA.Text);
            palette.Bpalette = double.Parse(textBoxPalB.Text);
            palette.Cpalette = double.Parse(textBoxPalC.Text);

            Product product = new Product();
            product.A = double.Parse(textBoxProdA.Text);
            product.B = double.Parse(textBoxProdB.Text);
            product.C = double.Parse(textBoxProdC.Text);
            product.Weight = double.Parse(textBoxWeight.Text);
            product.Count = int.Parse(textBoxCount.Text);
            product.MasterCartonPieces = int.Parse(textBoxKarton.Text);
            product.Prize = double.Parse(textBoxPrize.Text);

            palette.GetNumberOfPalettes(product);

            richTextBoxResult.AppendText("Počet palet: " + palette.CountOfPalette.ToString() + "\n");
            richTextBoxResult.AppendText("Váha plné palety: " + palette.WeightPalette.ToString() + "\n");
            richTextBoxResult.AppendText("Počet kusů na plné paletě: " + palette.PiecesPerPallete.ToString() + "\n");
            richTextBoxResult.AppendText("Počet kartonů celkem: " + product.Count / product.MasterCartonPieces + "\n");
            richTextBoxResult.AppendText("Počet kartonů na plné paletě: " + palette.CartonsPerPallete.ToString() + "\n");
            richTextBoxResult.AppendText("Počet kartonů na poslední paletě: " + palette.CountOfLastPalette.ToString() + "\n");
            richTextBoxResult.AppendText("Výška palety: " + palette.HeightPalette .ToString() + "\n");
            richTextBoxResult.AppendText("Výška poslední palety: " + palette.HeightLastPallete.ToString() +"\n");
            richTextBoxResult.AppendText("Výška jedné řady: " + palette.LastProductValue.ToString() + "\n");
            richTextBoxResult.AppendText("Cena plné palety palety: " + palette.PrizePerPallete.ToString() + "\n");
            richTextBoxResult.AppendText("Cena poslední palety: " + palette.PrizePerLastPallete.ToString() + "\n");

            text += "Zboží: \n\n";
            for (int i = 1; i <= (int)palette.CountOfPalette; i++)
            {
                
                if (i < (int)palette.CountOfPalette)
                {
                    text += (1 + "x paleta " + palette.Apalette + " x " + palette.Bpalette + " x " + palette.HeightPalette + ", " + palette.WeightPalette + " kg" +"\n" + "Hodnota pro pojištění: " + palette.PrizePerPallete + " Kč\n\n");
                }
                else
                {
                    text += 1 + "x paleta " + palette.Apalette + " x " + palette.Bpalette + " x " + palette.HeightLastPallete + ", " + palette.WeightLastPalette + " kg" + "\n" + "Hodnota pro pojištění: " + palette.PrizePerLastPallete + "Kč\n\n";
                }
            }
            text += "Celková váha: " + product.Weight * product.Count * product.MasterCartonPieces + "kg\n";
            text += "Celková hodnota pro pojištění: " + product.Prize * product.Count * product.MasterCartonPieces + "Kč";
        }

        private void buttonEmail_Click(object sender, EventArgs e)
        {
            AddAdressReceiver();
            CheckVyklAdress();

            Outlook._Application app = new Outlook.Application();
            Outlook.MailItem mail = (Outlook.MailItem)app.CreateItem(Outlook.OlItemType.olMailItem);
            mail.To = "lukashlavacek.lh@gmail.com";
            mail.Subject = "Agora poptávka";
            mail.Body = "Dobrý den,\n\nprosím o cenu na přepravu:\n\n" 
                + label11.Text + "\n" + adresText + "\n\n"
                + label12.Text + "\n" + adresTextReceiver + "\n\n"
                + text + "\n\nDěkuji";
            mail.Display(true);
        }

        public void AddAdressSender()
        {

            adress.CompanyName = textBoxCompNakl.Text;
            adress.Street = textBoxStreetNakl.Text;
            adress.City = textBoxCityNakl.Text;
            adress.ZipCode = textBoxZipNakl.Text;
            adress.State = textBoxStateNakl.Text;
            adress.ContaktPerson = textBoxPersonNakl.Text;
            adress.TelNumber = textBoxPhoneNakl.Text;
            
            adresText = 
                adress.CompanyName + "\n" +
                adress.Street + "\n" +
                adress.City + "\n" +
                adress.ZipCode + "\n" +
                adress.State + "\n" +
                adress.ContaktPerson + "\n" +
                adress.TelNumber;
            cntx.Adresses.Add(adress);
            cntx.SaveChanges();
        }

        public void AddAdressReceiver()
        {

            adress.CompanyName = textBoxCompVykl.Text;
            adress.Street = textBoxStreetVykl.Text;
            adress.City = textBoxCityVykl.Text;
            adress.ZipCode = textBoxZipVykl.Text;
            adress.State = textBoxStateVykl.Text;
            adress.ContaktPerson = textBoxPersonVykl.Text;
            adress.TelNumber = textBoxPhoneVykl.Text;

            adresTextReceiver =
                adress.CompanyName + "\n" +
                adress.Street + "\n" +
                adress.City + "\n" +
                adress.ZipCode + "\n" +
                adress.State + "\n" +
                adress.ContaktPerson + "\n" +
                adress.TelNumber;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxCompNakl.Clear();
            textBoxStreetNakl.Clear();
            textBoxCityNakl.Clear();
            textBoxZipNakl.Clear();
            textBoxStateNakl.Clear();
            textBoxPersonNakl.Clear();
            textBoxPhoneNakl.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxCompVykl.Clear();
            textBoxStreetVykl.Clear();
            textBoxCityVykl.Clear();
            textBoxZipVykl.Clear();
            textBoxStateVykl.Clear();
            textBoxPersonVykl.Clear();
            textBoxPhoneVykl.Clear();
        }

        private void ButtonAgoraNakl_Click(object sender, EventArgs e)
        {
            textBoxCompNakl.Text = "Agora DMT, a.s.";
            textBoxStreetNakl.Text = "Řípská 11c";
            textBoxCityNakl.Text = "Brno - Slatina";
            textBoxZipNakl.Text = "62700";
            textBoxStateNakl.Text = "Česká republika";
            textBoxPersonNakl.Text = "Lukáš Hlaváček";
            textBoxPhoneNakl.Text = "+420 515 913 876";
        }

        private void ButtonAgoraVykl_Click(object sender, EventArgs e)
        {
            textBoxCompVykl.Text = "Agora DMT, a.s.";
            textBoxStreetVykl.Text = "Řípská 11c";
            textBoxCityVykl.Text = "Brno - Slatina";
            textBoxZipVykl.Text = "62700";
            textBoxStateVykl.Text = "Česká republika";
            textBoxPersonVykl.Text = "Lukáš Hlaváček";
            textBoxPhoneVykl.Text = "+420 515 913 876";
        }

        private void CheckNaklAdress()
        {
            var dotaz = cntx.Adresses.Where(c => c.CompanyName == textBoxCompNakl.Text)
                                      .Where(c => c.Street == textBoxStreetNakl.Text)
                                      .Where(c => c.City == textBoxCityNakl.Text)
                                      .Select(c => c.CompanyName);

            if (dotaz == null)
            {
                cntx.Adresses.Add(adress);
                cntx.SaveChanges();
            }
            else
            {
                MessageBox.Show("V seznamu se již tato adresa nachází");
            }
        }

        private void CheckVyklAdress()
        {
            List<Adress> LA = new List<Adress>();
            LA = cntx.Adresses.ToList();
            var myVariable = (from l in LA
                              where l.CompanyName == textBoxCompNakl.Text
                              select l.CompanyName);

            label12.Text = myVariable.ToString();

            if (myVariable.Any())
            {
                MessageBox.Show("Do seznamu pridan");
            }
            else
            {
                MessageBox.Show("V seznamu se již tato adresa nachází");
            }
        }
    }
}
