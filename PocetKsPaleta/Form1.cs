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
        public enum Adresses{ LoadingAdress, UnloadingAdress }
        public Adresses adresses { get; private set; }
        public string DateTimeN { get; private set; }
        public string DateTimeV { get; private set; }
        private Adress adress = new Adress();
        private string text;
        private string adresTextReceiver;
        private string adresText;
        private Context cntx = new Context();
        
        public Form1()
        {
            InitializeComponent();
            dateTimePickerVykl.Value = DateTime.Now.AddDays(2);
            dateTimePickerNakl.Value = DateTime.Now.AddDays(1);
            labelDatime.Text = DateTime.Now.ToLongDateString();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            text = "";
            Palette palette = new Palette();
            Product product = new Product();

            richTextBoxResult.Clear();

                palette.Apalette = double.Parse(textBoxPalA.Text);
                palette.Bpalette = double.Parse(textBoxPalB.Text);
                palette.Cpalette = double.Parse(textBoxPalC.Text);
                
                product.A = double.Parse(textBoxProdA.Text);
                product.B = double.Parse(textBoxProdB.Text);
                product.C = double.Parse(textBoxProdC.Text);
                product.Weight = double.Parse(textBoxWeight.Text);
                product.Count = int.Parse(textBoxCount.Text);
                product.MasterCartonPieces = int.Parse(textBoxKarton.Text);
                product.Prize = double.Parse(textBoxPrize.Text);

                if (product.A < 0 || product.B < 0 || product.C < 0)
                {
                    MessageBox.Show("Rozměr produktu nemůže mít zápornou hodnotu", "ERROR");
                }
                else
                    if (palette.Apalette < 0 || palette.Bpalette < 0 || palette.Cpalette < 0)
                    {
                        MessageBox.Show("Rozměr přepravní jednotky nemůže mít zápornou hodnotu", "ERROR");
                    }
                else
                    if (product.Weight < 0 || product.Count < 0 || product.Prize < 0)
                {
                    MessageBox.Show("Váha, cena a počet kusů nemůžou mít záporné hodnoty", "ERROR");
                }
                else
                    if (product.A > palette.Cpalette || product.B > palette.Cpalette || product.C > palette.Cpalette)
                {
                    MessageBox.Show("Rozměr produktu je větší, jak rozměr přepravní jednotky", "ERROR");
                }
                else
                {
                    palette.GetNumberOfPalettes();

                    richTextBoxResult.AppendText("Počet palet: " + palette.CountOfPalette.ToString() + "\n");
                    richTextBoxResult.AppendText("Váha plné palety: " + palette.WeightPalette.ToString() + "\n");
                    richTextBoxResult.AppendText("Počet kusů na plné paletě: " + palette.PiecesPerPallete.ToString() + "\n");
                    richTextBoxResult.AppendText("Počet kartonů celkem: " + product.Count / product.MasterCartonPieces + "\n");
                    richTextBoxResult.AppendText("Počet kartonů na plné paletě: " + palette.CartonsPerPallete.ToString() + "\n");
                    richTextBoxResult.AppendText("Počet kartonů na poslední paletě: " + palette.CountOfLastPalette.ToString() + "\n");
                    richTextBoxResult.AppendText("Výška palety: " + palette.HeightPalette.ToString() + "\n");
                    richTextBoxResult.AppendText("Výška poslední palety: " + palette.HeightLastPallete.ToString() + "\n");
                    richTextBoxResult.AppendText("Výška jedné řady: " + palette.LastProductValue.ToString() + "\n");
                    richTextBoxResult.AppendText("Cena plné palety palety: " + palette.PrizePerPallete.ToString() + "\n");
                    richTextBoxResult.AppendText("Cena poslední palety: " + palette.PrizePerLastPallete.ToString() + "\n");

                    text += "Zboží: \n\n";
                    for (int i = 1; i <= (int)palette.CountOfPalette; i++)
                    {

                        if (i < (int)palette.CountOfPalette)
                        {
                            text += (1 + "x paleta " + palette.Apalette + " x " + palette.Bpalette + " x " + palette.HeightPalette + ", " + palette.WeightPalette + " kg" + "\n" + "Hodnota pro pojištění: " + palette.PrizePerPallete + " Kč\n\n");
                        }
                        else
                        {
                            text += 1 + "x paleta " + palette.Apalette + " x " + palette.Bpalette + " x " + palette.HeightLastPallete + ", " + palette.WeightLastPalette + " kg" + "\n" + "Hodnota pro pojištění: " + palette.PrizePerLastPallete + "Kč\n\n";
                        }
                    }
                    text += "Celková váha: " + product.Weight * product.Count * product.MasterCartonPieces + "kg\n";
                    text += "Celková hodnota pro pojištění: " + product.Prize * product.Count * product.MasterCartonPieces + "Kč\n";
                }
            

        }

        private void buttonEmail_Click(object sender, EventArgs e)
        {
            AddAdressReceiver();
            CheckVyklAdress();
            AddAdressSender();
            CheckNaklAdress();

            Outlook._Application app = new Outlook.Application();
            Outlook.MailItem mail = (Outlook.MailItem)app.CreateItem(Outlook.OlItemType.olMailItem);
            mail.To = "lukashlavacek.lh@gmail.com";
            mail.Subject = "Agora poptávka";
            mail.Body = "Dobrý den,\n\nprosím o cenu na přepravu:\n\n"
                + label11.Text + "\n" + adresText + "\n\n"
                + label12.Text + "\n" + adresTextReceiver + "\n\n"
                + text + "\n"
                + "Datum nakládky: " + DateTimeN + "\n"
                + "Datum vykládky: " + DateTimeV + "\n" + "\nDěkuji";
            mail.Display(true);
        }

        // Misto nakladky
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

        }
        //misto vykladky
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
        //Vycisteni textboxu adresy nakladkove
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
        //Vycisteni textboxu adresy vykladkove
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
        //Predpripravena Agora adresa, nakladka
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
        //Predpripravena Agora adresa, vykladka
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
        //kontrola, zda-li je adresa jiz v databazi, pripadne pridani do DB
        private void CheckNaklAdress()
        {
            var isAdress = cntx.Adresses.Where(a => a.CompanyName == textBoxCompNakl.Text).Select(a => a.CompanyName);

            if (isAdress.Any())
            {
                MessageBox.Show("V seznamu se již tato adresa nachází");
            }
            else
            {
                MessageBox.Show("Do seznamu pridan");
                cntx.Adresses.Add(adress);
                cntx.SaveChanges();
            }
        }
        //kontrola, zda-li je adresa jiz v databazi, pripadne pridani do DB
        private void CheckVyklAdress()
        {

            var isAdress = cntx.Adresses.Where(a => a.CompanyName == textBoxCompVykl.Text).Select(a=> a.CompanyName);

            if (isAdress.Any())
            {
                MessageBox.Show("V seznamu se již tato adresa nachází");
            }
            else
            {
                MessageBox.Show("Do seznamu pridan");
                cntx.Adresses.Add(adress);
                cntx.SaveChanges();
            }
        }

        public void AddNaklToTextboxes(Adress adress)
        {
             textBoxCompNakl.Text = adress.CompanyName;
             textBoxStreetNakl.Text = adress.Street;
             textBoxCityNakl.Text = adress.City;
             textBoxZipNakl.Text = adress.ZipCode;
             textBoxStateNakl.Text = adress.State;
             textBoxPersonNakl.Text = adress.ContaktPerson;
             textBoxPhoneNakl.Text = adress.TelNumber;
        }

        public void AddVyklToTextboxes(Adress adress)
        {
            textBoxCompVykl.Text = adress.CompanyName;
            textBoxStreetVykl.Text = adress.Street;
            textBoxCityVykl.Text = adress.City;
            textBoxZipVykl.Text = adress.ZipCode;
            textBoxStateVykl.Text = adress.State;
            textBoxPersonVykl.Text = adress.ContaktPerson;
            textBoxPhoneVykl.Text = adress.TelNumber;
        }

        private void buttonSearchNakl_Click(object sender, EventArgs e)
        {           
            adresses = Adresses.LoadingAdress;
            Form2 form2 = new Form2(this);           
            form2.ShowDialog();                       
        }

        private void buttonSearchVykl_Click(object sender, EventArgs e)
        {
            adresses = Adresses.UnloadingAdress;
            Form2 form2 = new Form2(this);
            form2.ShowDialog();
        }

        private void dateTimePickerVykl_ValueChanged(object sender, EventArgs e)
        {           
            DateTimeV = dateTimePickerVykl.Value.Date.ToShortDateString();
        }

        private void dateTimePickerNakl_ValueChanged(object sender, EventArgs e)
        {           
            DateTimeN = dateTimePickerNakl.Value.Date.ToShortDateString();
        }
    }
        
    
}
