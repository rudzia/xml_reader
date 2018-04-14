using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.IO;

namespace xml_reader
{
    public partial class Form1 : Form
    {
        private
            XmlSerializer xmlserializer; //Object used for serialization and deserialization
            pola_skierowania skierowanie;//Object containing data from XML "skierowanie.xml"
            static bool XMLValid; //XML validity flag
            private bool isFillingFields; // czyto nie jest to samo co powyzej?

        public Form1()
        {
            InitializeComponent();
            skierowanie = new pola_skierowania();
            skierowanie.osoba = new dane_pacjenta();
            skierowanie.tablica_badan = new BadaniaBadanie_konsultacja[20];
            skierowanie.tablica_szkodliwosci = new SzkodliweCzynnik[20];
            xmlserializer = new XmlSerializer(typeof(pola_skierowania));
            isFillingFields = false;
        }
        private XmlReader CreateXmlReaderWithSchemaValidation(StreamReader stream)
        {
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.ValidationType = ValidationType.Schema;
            readerSettings.Schemas.Add(null, @"skierowanie.xsd");
            return XmlReader.Create(stream, readerSettings);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(openFileDialog1.FileName);
                XmlReader xmlReader = CreateXmlReaderWithSchemaValidation(streamReader);


                try
                {
                    xmlserializer = new XmlSerializer(typeof(pola_skierowania));
                    skierowanie = (pola_skierowania)xmlserializer.Deserialize(xmlReader);
                    FillFormFields();
                }
                catch (Exception)
                {
                    MessageBox.Show(
                        "Niepoprawny format dokumentu XML",
                        "Błąd",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                }
                finally
                {
                    streamReader.Close();
                    xmlReader.Close();
                }
            }
            /*
            xmlserializer = new XmlSerializer(typeof(pola_skierowania)); //construction of object for XML (de)serialization
            FileStream fs = new FileStream("skierowanie2.xml", FileMode.Open); //creating stream for reading from "zamowienie.xml" file
            XmlReader reader = XmlReader.Create(fs); //creating reader for reading from XML file to stream

            skierowanie = (pola_skierowania)xmlserializer.Deserialize(reader); //deserialization of XML file
            fs.Close(); //closing the input stream

            //Passing data to GUI
            textBox1.Text = skierowanie.nazwa_zakladu_pracy;
            textBox2.Text = skierowanie.osoba.imie;
            textBox3.Text = skierowanie.osoba.nazwisko;
            textBox4.Text = skierowanie.osoba.czas_zatrudnienia;
            textBox5.Text = skierowanie.osoba.ulica;
            textBox6.Text = skierowanie.osoba.regon;
            textBox7.Text = skierowanie.osoba.pesel;
            textBox8.Text = skierowanie.osoba.stanowisko;
            textBox9.Text = skierowanie.osoba.obecne_stanowisko;
            textBox10.Text = skierowanie.osoba.kod_pocztowy;
            textBox11.Text = skierowanie.dodatkowe_uwagi_A;
            textBox12.Text = skierowanie.dodatkowe_uwagi_B;
            textBox13.Text = skierowanie.lekarz;
            textBox14.Text = skierowanie.zlecajacy;
            textBox15.Text = skierowanie.osoba.miasto;
            comboBox1.Text = skierowanie.osoba.rodzaj_badania.ToString();
            dateTimePicker1.Value = skierowanie.data;

            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(skierowanie.tablica_szkodliwosci.Length);
            for (int i = 0; i < skierowanie.tablica_szkodliwosci.Length; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = (i + 1).ToString();
                dataGridView1.Rows[i].Cells[1].Value = skierowanie.tablica_szkodliwosci[i].rodzaj_czynnika;
                dataGridView1.Rows[i].Cells[2].Value = skierowanie.tablica_szkodliwosci[i].wyniki_pomiarow;
                dataGridView1.Rows[i].Cells[3].Value = skierowanie.tablica_szkodliwosci[i].NDS_NDN;
                dataGridView1.Rows[i].Cells[4].Value = skierowanie.tablica_szkodliwosci[i].uwagi;
            };

            dataGridView2.Rows.Clear();
            dataGridView2.Rows.Add(skierowanie.tablica_badan.Length);
            for (int i = 0; i < skierowanie.tablica_badan.Length; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = (i + 1).ToString();
                dataGridView2.Rows[i].Cells[1].Value = skierowanie.tablica_badan[i].badanie;
                dataGridView2.Rows[i].Cells[2].Value = skierowanie.tablica_badan[i].czestotliwosc;
                dataGridView2.Rows[i].Cells[3].Value = skierowanie.tablica_badan[i].uwagi;
            };
            toolStripStatusLabel1.Text = "Status: ";
            toolStripStatusLabel2.Text = "";
            */
        }
        private void FillFormFields()
        {
            //Passing data to GUI
            isFillingFields = true;
            textBox1.Text = skierowanie.nazwa_zakladu_pracy;
            textBox2.Text = skierowanie.osoba.imie;
            textBox3.Text = skierowanie.osoba.nazwisko;
            textBox4.Text = skierowanie.osoba.czas_zatrudnienia;
            textBox5.Text = skierowanie.osoba.ulica;
            textBox6.Text = skierowanie.osoba.regon;
            textBox7.Text = skierowanie.osoba.pesel;
            textBox8.Text = skierowanie.osoba.stanowisko;
            textBox9.Text = skierowanie.osoba.obecne_stanowisko;
            textBox10.Text = skierowanie.osoba.kod_pocztowy;
            textBox11.Text = skierowanie.dodatkowe_uwagi_A;
            textBox12.Text = skierowanie.dodatkowe_uwagi_B;
            textBox13.Text = skierowanie.lekarz;
            textBox14.Text = skierowanie.zlecajacy;
            textBox15.Text = skierowanie.osoba.miasto;
            comboBox1.Text = skierowanie.osoba.rodzaj_badania.ToString();
            dateTimePicker1.Value = skierowanie.data;

            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(skierowanie.tablica_szkodliwosci.Length);
            for (int i = 0; i < skierowanie.tablica_szkodliwosci.Length; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = (i + 1).ToString();
                dataGridView1.Rows[i].Cells[1].Value = skierowanie.tablica_szkodliwosci[i].rodzaj_czynnika;
                dataGridView1.Rows[i].Cells[2].Value = skierowanie.tablica_szkodliwosci[i].wyniki_pomiarow;
                dataGridView1.Rows[i].Cells[3].Value = skierowanie.tablica_szkodliwosci[i].NDS_NDN;
                dataGridView1.Rows[i].Cells[4].Value = skierowanie.tablica_szkodliwosci[i].uwagi;
            };

            dataGridView2.Rows.Clear();
            dataGridView2.Rows.Add(skierowanie.tablica_badan.Length);
            for (int i = 0; i < skierowanie.tablica_badan.Length; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = (i + 1).ToString();
                dataGridView2.Rows[i].Cells[1].Value = skierowanie.tablica_badan[i].badanie;
                dataGridView2.Rows[i].Cells[2].Value = skierowanie.tablica_badan[i].czestotliwosc;
                dataGridView2.Rows[i].Cells[3].Value = skierowanie.tablica_badan[i].uwagi;
            };
            toolStripStatusLabel1.Text = "Status: ";
            toolStripStatusLabel2.Text = "";
            isFillingFields = false;
        }
        private void updateTablcaSzkodliwosci() //metoda do obslugi tabeli
        {
            skierowanie.tablica_szkodliwosci = new SzkodliweCzynnik[dataGridView1.RowCount - 1];
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
                skierowanie.tablica_szkodliwosci[i] = new SzkodliweCzynnik();
                skierowanie.tablica_szkodliwosci[i].rodzaj_czynnika = dataGridView1.Rows[i].Cells[1].Value.ToString();
                skierowanie.tablica_szkodliwosci[i].wyniki_pomiarow = dataGridView1.Rows[i].Cells[2].Value.ToString();
                skierowanie.tablica_szkodliwosci[i].NDS_NDN = dataGridView1.Rows[i].Cells[3].Value.ToString();
                skierowanie.tablica_szkodliwosci[i].uwagi = dataGridView1.Rows[i].Cells[4].Value.ToString();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = comboBox1.GetItemText(comboBox1.SelectedItem);
            skierowanie.osoba.rodzaj_badania = (Badanie_type)Enum.Parse(typeof(Badanie_type), selected);
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            skierowanie.osoba.imie = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            skierowanie.osoba.nazwisko = textBox2.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            skierowanie.nazwa_zakladu_pracy = textBox2.Text;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            updateTablcaSzkodliwosci();
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            updateTablcaSzkodliwosci();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button_validatesave_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream(); //creating a stream for storing XML file with modified data
            XmlWriter writer = XmlWriter.Create(ms); //creating writer for writing XML file to stream
            xmlserializer.Serialize(writer, skierowanie);

            XmlReaderSettings xrset = new XmlReaderSettings(); //definition of XML reader settings
            xrset.ValidationType = ValidationType.Schema; //validation based on XML Schema
            ms.Position = 0; //setting the pointer on beginning of memory stream
            XmlReader reader = XmlReader.Create(ms, xrset); //creating a reader for reading XML from memory stream

            XmlDocument xdoc = new XmlDocument(); //creating an XML document
            xdoc.Load(reader); //loading document from memory stream
            xdoc.Schemas.Add(null, @"skierowanie.xsd"); //connecting the XML document with schema from "zamowienie.xsd"
            ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventCallback); //setting the event handler for handling incorrect validation events
            XMLValid = true; //setting the default value of XML validity flag for true
            xdoc.Validate(eventHandler); //performing validation

            //Writing the memory stream to a file "zamowienie_mod.xml" if the document is valid
            if (XMLValid)
            {
                FileStream fs = new FileStream("skierowanie_gen.xml", FileMode.Create);
                ms.Position = 0;
                ms.CopyTo(fs);
                fs.Close();
                //toolStripStatusLabel2.Text = "XML valid - marshalling to \"zamowienie_mod.xml\" file";
                toolStripStatusLabel2.Text = "Walidacja XML pomyślna - dane zapisano do pliku \"zamowienie_gen.xml\"";
            }
            else
            {
                //toolStripStatusLabel2.Text = "XML invalid";
                toolStripStatusLabel2.Text = "Nieprawidłowe dane dokumentu";
            }
            ms.Close();

        
        }
        //Callback function for serving the events of incorrect validation
        static void ValidationEventCallback(object sender, ValidationEventArgs e)
        {
            string msg;
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    //MessageBox.Show(e.Message, "Validation Error", MessageBoxButtons.OK);
                    msg = "Walidacja nie przebiegła pomyślnie: Błąd\n" + e.Message;
                    MessageBox.Show(msg, "Walidacja: Błąd", MessageBoxButtons.OK);
                    XMLValid = false;
                    break;
                case XmlSeverityType.Warning:
                    //MessageBox.Show(e.Message, "Validation Warning", MessageBoxButtons.OK);
                    msg = "Walidacja nie przebiegła pomyślnie: Ostrzeżenie\n" + e.Message;
                    MessageBox.Show(msg, "Walidacja: Ostrzeżenie", MessageBoxButtons.OK);
                    XMLValid = false;
                    break;
            }

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            skierowanie.data = dateTimePicker1.Value;
        }

        private void updateTablicaBadan() //metoda do obslugi tabeli
        {
            skierowanie.tablica_badan = new BadaniaBadanie_konsultacja[dataGridView2.RowCount - 1];
            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = i + 1;
                skierowanie.tablica_badan[i] = new BadaniaBadanie_konsultacja();
                skierowanie.tablica_badan[i].badanie = dataGridView2.Rows[i].Cells[1].Value.ToString();
                skierowanie.tablica_badan[i].czestotliwosc = dataGridView2.Rows[i].Cells[2].Value.ToString();
                skierowanie.tablica_badan[i].uwagi = dataGridView2.Rows[i].Cells[3].Value.ToString();
            }
        }

        private void dataGridView2_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            updateTablicaBadan();
        }

        private void dataGridView2_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            updateTablicaBadan();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            skierowanie.osoba.pesel = textBox7.Text;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            skierowanie.osoba.ulica = textBox5.Text;
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            skierowanie.osoba.kod_pocztowy = textBox10.Text;
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            skierowanie.osoba.miasto = textBox15.Text;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            skierowanie.osoba.regon = textBox6.Text;
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            skierowanie.osoba.obecne_stanowisko = textBox9.Text;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            skierowanie.osoba.stanowisko = textBox8.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            skierowanie.osoba.czas_zatrudnienia = textBox4.Text;
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            skierowanie.zlecajacy = textBox14.Text;
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("skierowanie", Namespace = "", IsNullable = false)]
    public partial class pola_skierowania
    {

        private dane_pacjenta osobaField;

        private SzkodliweCzynnik[] tablica_szkodliwosciField;

        private string dodatkowe_uwagi_AField;

        private System.DateTime dataField;

        private string zlecajacyField;

        private BadaniaBadanie_konsultacja[] tablica_badanField;

        private string dodatkowe_uwagi_BField;

        private string lekarzField;

        private string nazwa_zakladu_pracyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public dane_pacjenta osoba
        {
            get
            {
                return this.osobaField;
            }
            set
            {
                this.osobaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("czynnik", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public SzkodliweCzynnik[] tablica_szkodliwosci
        {
            get
            {
                return this.tablica_szkodliwosciField;
            }
            set
            {
                this.tablica_szkodliwosciField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string dodatkowe_uwagi_A
        {
            get
            {
                return this.dodatkowe_uwagi_AField;
            }
            set
            {
                this.dodatkowe_uwagi_AField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime data
        {
            get
            {
                return this.dataField;
            }
            set
            {
                this.dataField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string zlecajacy
        {
            get
            {
                return this.zlecajacyField;
            }
            set
            {
                this.zlecajacyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("badanie_konsultacja", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public BadaniaBadanie_konsultacja[] tablica_badan
        {
            get
            {
                return this.tablica_badanField;
            }
            set
            {
                this.tablica_badanField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string dodatkowe_uwagi_B
        {
            get
            {
                return this.dodatkowe_uwagi_BField;
            }
            set
            {
                this.dodatkowe_uwagi_BField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string lekarz
        {
            get
            {
                return this.lekarzField;
            }
            set
            {
                this.lekarzField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nazwa_zakladu_pracy
        {
            get
            {
                return this.nazwa_zakladu_pracyField;
            }
            set
            {
                this.nazwa_zakladu_pracyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class dane_pacjenta
    {

        private string imieField;

        private string nazwiskoField;

        private string peselField;

        private string ulicaField;

        private string kod_pocztowyField;

        private string miastoField;

        private string czas_zatrudnieniaField;

        private string stanowiskoField;

        private string regonField;

        private string obecne_stanowiskoField;

        private Badanie_type rodzaj_badaniaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string imie
        {
            get
            {
                return this.imieField;
            }
            set
            {
                this.imieField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string nazwisko
        {
            get
            {
                return this.nazwiskoField;
            }
            set
            {
                this.nazwiskoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
        public string pesel
        {
            get
            {
                return this.peselField;
            }
            set
            {
                this.peselField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ulica
        {
            get
            {
                return this.ulicaField;
            }
            set
            {
                this.ulicaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string kod_pocztowy
        {
            get
            {
                return this.kod_pocztowyField;
            }
            set
            {
                this.kod_pocztowyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string miasto
        {
            get
            {
                return this.miastoField;
            }
            set
            {
                this.miastoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string czas_zatrudnienia
        {
            get
            {
                return this.czas_zatrudnieniaField;
            }
            set
            {
                this.czas_zatrudnieniaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string stanowisko
        {
            get
            {
                return this.stanowiskoField;
            }
            set
            {
                this.stanowiskoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
        public string regon
        {
            get
            {
                return this.regonField;
            }
            set
            {
                this.regonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string obecne_stanowisko
        {
            get
            {
                return this.obecne_stanowiskoField;
            }
            set
            {
                this.obecne_stanowiskoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public Badanie_type rodzaj_badania
        {
            get
            {
                return this.rodzaj_badaniaField;
            }
            set
            {
                this.rodzaj_badaniaField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    public enum Badanie_type
    {

        /// <remarks/>
        wstepne,

        /// <remarks/>
        okresowe,

        /// <remarks/>
        kontrolne,

        /// <remarks/>
        koncowe,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("zmiana stanowiska pracy")]
        zmianastanowiskapracy,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SzkodliweCzynnik
    {

        private string rodzaj_czynnikaField;

        private string wyniki_pomiarowField;

        private string nDS_NDNField;

        private string uwagiField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string rodzaj_czynnika
        {
            get
            {
                return this.rodzaj_czynnikaField;
            }
            set
            {
                this.rodzaj_czynnikaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string wyniki_pomiarow
        {
            get
            {
                return this.wyniki_pomiarowField;
            }
            set
            {
                this.wyniki_pomiarowField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NDS_NDN
        {
            get
            {
                return this.nDS_NDNField;
            }
            set
            {
                this.nDS_NDNField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string uwagi
        {
            get
            {
                return this.uwagiField;
            }
            set
            {
                this.uwagiField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class BadaniaBadanie_konsultacja
    {

        private string badanieField;

        private string czestotliwoscField;

        private string uwagiField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string badanie
        {
            get
            {
                return this.badanieField;
            }
            set
            {
                this.badanieField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string czestotliwosc
        {
            get
            {
                return this.czestotliwoscField;
            }
            set
            {
                this.czestotliwoscField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string uwagi
        {
            get
            {
                return this.uwagiField;
            }
            set
            {
                this.uwagiField = value;
            }
        }
    }
}