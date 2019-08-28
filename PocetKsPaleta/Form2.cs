using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PocetKsPaleta
{
    public partial class Form2 : Form
    {
        private Form1 f1;
        public Form2(Form1 f1)
        {
            InitializeComponent();
            this.f1 = f1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: Tento řádek načte data do tabulky 'adresyDataSet.Adresses'. Můžete jej přesunout nebo jej odstranit podle potřeby.
            this.adressesTableAdapter.Fill(this.adresyDataSet.Adresses);

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (f1.Choice == 0)
            {
                f1.AddNaklToTextboxes(GetObjectOfCurrentRow());
            }
            else
            {
                f1.AddVyklToTextboxes(GetObjectOfCurrentRow());
            }
                        
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public Adress GetObjectOfCurrentRow()
        {
            Context cntx = new Context();
            Adress[] ad = cntx.Adresses.ToArray();
            int index = dataGridView1.CurrentRow.Index;
            return ad[index];
        }

    }
}
