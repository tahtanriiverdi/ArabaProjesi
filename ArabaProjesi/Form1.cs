using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ArabaProjesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        if (File.Exists(temp))
        {
         string jsondata = File.ReadAllText(temp);
         arabalar = JsonSerializer.Deserialize<List<Araba>>(jsondata);
        }

            Gosterliste();
        }
        private void toolStripSplitButton2_ButtonClick(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void jsonToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        List<Araba> arabalar = new List<Araba>()
        {
            new Araba()
            {
                Marka = "Tofaş",
                Model ="Şahin",
                Yakit = "Benzin",
                Renk = "Pembe",
                Vites ="Manuel",
                KasaTipi = "Düz",
                Plaka = "34 G 3101",
                Aciklama ="Az hasarlı",
            },
            new Araba()
            {
                Marka = "Tesla",
                Model ="Model Y",
                Yakit = "Elektrikli",
                Renk = "Siyah",
                Vites ="Otomatik",
                KasaTipi = "Eğimli",
                Plaka = "31 A 3131",
                Aciklama ="Çıtır hasarlı",
            }
        };

        public void Gosterliste()
        {
            listView1.Items.Clear();
            foreach (Araba araba in arabalar)
            {
                Ekleliste(araba);
            }
        }
        public void Ekleliste(Araba araba)
        {
            ListViewItem item = new ListViewItem(new string[]{
                    araba.Marka,
                    araba.Model,
                    araba.Yakit,
                    araba.Renk,
                    araba.Vites,
                    araba.KasaTipi,
                    araba.Plaka,
                    araba.Aciklama,
                   });
            item.Tag= araba;
            listView1.Items.Add(item);

        }
        private void Duzenle(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }
            ListViewItem aItem = listView1.SelectedItems[0];
            Araba secili = aItem.Tag as Araba;

            Frmcar frm = new Frmcar()
            {
                Text = "Arabaları düzenle",
                araba = Clone(secili),
                StartPosition = FormStartPosition.CenterParent,
            };

            if (frm.ShowDialog() == DialogResult.OK)
            {
                secili = frm.araba;
                Duzenlearaba(aItem, secili);

            }
        }
        private void Ekleme(object sender, EventArgs e)
        {
            Frmcar frm = new Frmcar()
            {
                Text="Araba ekle",
                araba = new Araba(),
                StartPosition = FormStartPosition.CenterParent,
            };
            if (frm.ShowDialog() == DialogResult.OK)
            {
                arabalar.Add(frm.araba);
                Ekleliste(frm.araba);
            }
        }
        Araba Clone(Araba araba)
        {
            return new Araba()
            {
                id = araba.ID,
                Marka=araba.Marka,
                Model = araba.Model,
                KasaTipi = araba.KasaTipi,
                Yakit=araba.Yakit,
                Renk=araba.Renk,
                Vites=araba.Vites,
                Plaka=araba.Plaka,
                Aciklama=araba.Aciklama,

            };
        }

        void Duzenlearaba(ListViewItem aItem, Araba araba)
        {
            aItem.SubItems[0].Text = araba.Marka;
            aItem.SubItems[1].Text = araba.Model;
            aItem.SubItems[2].Text = araba.Yakit;
            aItem.SubItems[3].Text = araba.Renk;
            aItem.SubItems[4].Text = araba.Vites;
            aItem.SubItems[5].Text = araba.KasaTipi;
            aItem.SubItems[6].Text = araba.Plaka;
            aItem.SubItems[7].Text = araba.Aciklama;

            aItem.Tag=araba;
        }
        private void Silmetusu(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }
            ListViewItem aItem = listView1.SelectedItems[0];
            Araba secili = aItem.Tag as Araba;

           var sonuc= MessageBox.Show($"Seçili araba silinsin mi?\n\n{secili.Marka} {secili.Model}","Silmeyi onayla",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (sonuc == DialogResult.Yes)
            {
                arabalar.Remove(secili);
                listView1.Items.Remove(aItem);
            }
        }
        private void Dosyac(object sender, EventArgs e)
        {
            OpenFileDialog ofed = new OpenFileDialog()
            {
                Filter = "Json, Xml Formatları |*.json;*.xml",
            };
            if (ofed.ShowDialog() == DialogResult.OK)
            {
                if (ofed.FileName.ToLower().EndsWith("json"))
                {
                    string jsondata =File.ReadAllText(ofed.FileName);
                    arabalar = JsonSerializer.Deserialize<List<Araba>>(jsondata);
                }
                else if (ofed.FileName.ToLower().EndsWith("xml"))
                {
                    StreamReader sr = new StreamReader(ofed.FileName);
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Araba>));
                    arabalar = (List<Araba>)serializer.Deserialize(sr);
                    sr.Close();
                }
                Gosterliste();
            }    
        }
        string temp = Path.Combine(Application.CommonAppDataPath, "data");
        protected override void OnClosing(CancelEventArgs e)
        {
            string data = JsonSerializer.Serialize(arabalar);
            File.WriteAllText(temp, data);

            base.OnClosing(e);
        }
        [Serializable]

        public class Araba
        {

            public string id;

            [Browsable(false)]
            public string ID
            {
                get
                {
                    if (id == null)
                        id = Guid.NewGuid().ToString();
                    return id;
                }
                set { id = value; }

            }

            [Category("Bilgiler"), DisplayName("Marka")]
            public string Marka { get; set; }

            [Category("Bilgiler"), DisplayName("Model")]
            public string Model { get; set; }


            [Category("Bilgiler"), DisplayName("Yakıt")]
            public string Yakit { get; set; }

            [Category("Bilgiler"), DisplayName("Renk")]
            public string KasaTipi { get; set; }

            [Category("Bilgiler"), DisplayName("Vites")]
            public string Renk { get; set; }


            [Category("Bilgiler"), DisplayName("Kasa tipi")]
            public string Vites { get; set; }

            [Category("Bilgiler"), DisplayName("Plaka")]
            public string Plaka { get; set; }

            [Category("Açıklama"), DisplayName("Açıklama"),
                Description("Açıklamayı düzgün bir şekilde yazınız...")]
            public string Aciklama { get; set; }


        }

        private void g(object sender, UICuesEventArgs e)
        {

        }
        private void kaydettme(object sender, EventArgs e)
        {
            SaveFileDialog abi = new SaveFileDialog()
            {
                Filter = "Json Formatı|*.json|Xml Formatı|*.xml",
            };

            if (abi.ShowDialog() == DialogResult.OK)
            {
                if (abi.FileName.EndsWith("json"))
                {
                    string data = JsonSerializer.Serialize(arabalar);
                    File.WriteAllText(abi.FileName, data);

                }
                else if (abi.FileName.EndsWith("xml"))
                {
                    StreamWriter sw = new StreamWriter(abi.FileName);
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Araba>));
                    serializer.Serialize(sw, arabalar);
                    sw.Close();
                }
            }
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog();
        }
    }
}
