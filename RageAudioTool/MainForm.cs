using System;
using System.Text;
using System.Windows.Forms;
using System.IO;    
using RageAudioTool.Rage_Wrappers.DatFile;
using System.Data;
using System.Drawing;
using RageAudioTool.XML;

namespace RageAudioTool
{
    public partial class MainForm : Form
    {
        string _filename = string.Empty;

        private RageDataFile _currentFile = null;

        NametableEditor _subForm;

        public MainForm()
        {
            InitializeComponent();
            textBox1.Leave += TextBox1_Leave;
            textBox1.GotFocus += TextBox1_GotFocus;
            comboBox1.Leave += ComboBox1_Leave;
            comboBox1.GotFocus += ComboBox1_GotFocus;
            comboBox1.Items.Add("Type");
            comboBox1.Items.Add("Name");
            comboBox1.Items.Add("Category");
            comboBox1.Items.Add("Anything");
        }

        private void ComboBox1_GotFocus(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox1.ForeColor = SystemColors.WindowText;
        }

        private void TextBox1_GotFocus(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.ForeColor = SystemColors.WindowText;
        }

        private void ComboBox1_Leave(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                comboBox1.Text = "What";
                comboBox1.ForeColor = SystemColors.GrayText;
            }
        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 1)
            {
                textBox1.Text = "Search";
                textBox1.ForeColor = SystemColors.GrayText;
            }
        }

        private void ReadDat4File(string filename)
        {
            if (filename.Length < 1 || !File.Exists(filename)) return;

            StringBuilder builder = new StringBuilder();

            var md = new RageAudioMetadata5();

            using (RageDataFileReadReference file = new RageDataFileReadReference(filename))
            {
                md.Read(file);
            }

        //   using (RageDataFileWriteReference file = new RageDataFileWriteReference(Path.ChangeExtension(filename, ".output.rel54")))
        //    {
         //       md.Write(file);
         //   }

         // File.WriteAllText(Path.ChangeExtension(filename, ".txt"), md.ToString());

           var xml = new ResourceXmlWriter5(Path.ChangeExtension(filename, ".xml"));

             xml.WriteData(md);

            /*
            using (StreamWriter writer = new StreamWriter(Path.ChangeExtension(filename, ".txt")))
            {
                writer.Write(md.ToString());
            }*/
        }

        private void FillDataTable()
        {
      

            if (_currentFile == null) return;

            DataTable dt = new DataTable();

            if (_currentFile is RageAudioMetadata4)
            {
                dt.Columns.Add("Type");
                dt.Columns.Add("Name");

                for (int i = 0; i < _currentFile.DataItems.Length; i++)
                {
                    var item = _currentFile.DataItems[i];

                    dt.Rows.Add(item.GetType().Name, item.Name.ToString());
                }

                dataGridView1.DataSource = dt;

                DataGridViewColumn column = dataGridView1.Columns[0]; // column[1] selects the required column 
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                column = dataGridView1.Columns[1]; // column[1] selects the required column 
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            else
            {
                dt.Columns.Add("Type");
                dt.Columns.Add("Category");
                dt.Columns.Add("Name");

                for (int i = 0; i < _currentFile.DataItems.Length; i++)
                {
                    var item = _currentFile.DataItems[i];

                    string category = ((item is audSoundBase) ? (item as audSoundBase).Header.CategoryHash.HashName : "N/A");

                    dt.Rows.Add(item.GetType().Name, category, item.Name);
                }

                dataGridView1.DataSource = dt;

                DataGridViewColumn column = dataGridView1.Columns[0]; // column[1] selects the required column 
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                column = dataGridView1.Columns[1]; // column[1] selects the required column 
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _filename = string.Empty;

            _currentFile = null;

            using (var ofd = 
                new OpenFileDialog() { Multiselect = true,
                    Filter = "RAGE audio files (*.dat54.rel,*.dat151.rel,*.dat4.rel,*.xml)|*.dat54.rel;*.dat151.rel;*.dat4.rel;*.xml"
                })
            {
                var result = ofd.ShowDialog();

                switch (result)
                {
                    case DialogResult.Cancel: break;
                    case DialogResult.OK:

                        _filename = ofd.FileName;

                        Text = "RageAudioTool - [" + _filename + "]";

                        if (_filename.EndsWith(".dat54.rel") ||
                            _filename.EndsWith(".dat151.rel") ||
                            Path.GetFileNameWithoutExtension(_filename) == "speech2.dat4" ||
                            Path.GetFileNameWithoutExtension(_filename) == "speech.dat4")
                        {
                            _currentFile = new RageAudioMetadata5();

                            using (RageDataFileReadReference file = new RageDataFileReadReference(_filename))
                            {
                                _currentFile.Read(file);
                            }
                        }

                        else if (_filename.EndsWith(".dat4.rel"))
                        {
                            _currentFile = new RageAudioMetadata4();

                            using (RageDataFileReadReference file = new RageDataFileReadReference(_filename))
                            {
                                _currentFile.Read(file);
                            }
                        }

                        else if (Path.GetExtension(_filename) == ".xml")
                        {
                            var xml = new ResourceXmlReader5(_filename);

                            _currentFile = xml.ReadData();
                        }

                        break;
                }

                ofd.Dispose();
            }

            FillDataTable();
            //  var xmlReader = new ResourceXMLReader5("audio/config/sounds.dat54.xml");

            //   var data = xmlReader.ReadData();

            //   var xmlWriter = new ResourceXMLWriter5("audio/config/sounds1.dat54.xml");

            //    xmlWriter.WriteData(data);

            /*  StringBuilder builder = new StringBuilder();

              var md = new RageAudioMetadata4();

              using (RageDataFileReadReference file = new RageDataFileReadReference("audio/config/audioconfig.dat4.rel"))
              {
                  md.Read(file);
              }

              using (StreamWriter writer = new StreamWriter(Path.ChangeExtension("audioconfig.dat4.rel", ".txt")))
              {
                  writer.Write(md.ToString());
              }*/

            /*     using (var xmlWriter = System.Xml.XmlWriter.Create("xml.xml"))
                 {
                     var item = new audMultitrackSound();

                     var serializer = new DataContractSerializer(typeof(audMultitrackSound));

                     serializer.WriteObject(xmlWriter, item);
                 }
                 */

            // RageDataFileReadParameters p = new RageDataFileReadParameters("output.dat54");

            // var md = new RageAudioMetadata5();

            // md.Read(ref p);

            //  var result = md.Rebuild();

            //  File.WriteAllBytes("output.md1", result);

            //  ReadDat4File("audio/config/sounds.dat54.rel");

            //  ReadDat4File("audio/config/dlcbeach_sounds.dat54.rel");

            //  var md = new ResourceXMLReader5("audio/config/dlcjanuary2016_sounds.xml");

            //  var result = md.ReadData();

            //  MessageBox.Show(result.Length.ToString());

            //  ReadDat4File("audio/config/dlcmpheist_sounds.dat54.rel");
        }

        private static void ExpandGroup(PropertyGrid propertyGrid, string groupName)
        {
            GridItem root = propertyGrid.SelectedGridItem;
            //Get the parent
            while (root.Parent != null)
                root = root.Parent;

            if (root != null)
            {
                foreach (GridItem g in root.GridItems)
                {
                    if (g.GridItemType == GridItemType.Category && g.Label == groupName)
                    {
                        g.Expanded = true;
                        break;
                    }
                }
            }
        }

        private void dataGridView1_SelectedItemChanged(object sender, EventArgs e)
        {
            if (_currentFile == null ||
                dataGridView1.SelectedRows.Count < 1 ||
                dataGridView1.SelectedRows[0].Index > _currentFile.DataItems.Length)
                return;

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            DataTable dt = dataGridView1.DataSource as DataTable;

            if (dt == null) return;

            var realRowIdx = dt.Rows.IndexOf(((DataRowView)selectedRow.DataBoundItem).Row);
               
            propertyGrid1.SelectedObject = _currentFile.DataItems[realRowIdx];

            ExpandGroup(propertyGrid1, "audHashCollection");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_currentFile != null)
            {
                var extension = Path.GetExtension(_filename);

                string filter = string.Format("RAGE Audio File (*{0})|*{0}|XML File (*.xml)|*.xml|Text file (*.txt)|*.txt|All files (*.*)|*.*", extension);

                using (var sfd = new SaveFileDialog { Filter = filter })
                {
                    sfd.FileName = Path.GetFileName(_filename);

                    var result = sfd.ShowDialog();

                    switch (result)
                    {
                        case DialogResult.Cancel: return;
                        case DialogResult.OK:
                            Enabled = false;
                            string ext = Path.GetExtension(sfd.FileName);

                            if (ext == ".xml")
                            {
                                if (_currentFile is RageAudioMetadata4)
                                {
                                    MessageBox.Show("Error: Exporting of this format to xml is not yet supported.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                    return;

                                    //var xml = new ResourceXMLWriter5(Path.ChangeExtension(filename, ".xml"));

                                    // xml.WriteData(currentFile as RageAudioMetadata5);
                                }

                                var xml = new ResourceXmlWriter5(Path.ChangeExtension(sfd.FileName, ".xml"));

                                xml.WriteData(_currentFile as RageAudioMetadata5);
                            }

                            else if (ext == ".txt")
                            {
                                File.WriteAllText(Path.ChangeExtension(sfd.FileName, ".txt"), _currentFile.ToString());
                            }

                            else
                            {
                                using (RageDataFileWriteReference file = new RageDataFileWriteReference(sfd.FileName))
                                {
                                    _currentFile.Write(file);
                                }
                            }

                            Enabled = true;
                            MessageBox.Show(string.Format("Successfully exported to {0}.", sfd.FileName));

                            break;
                    }

                    sfd.Dispose();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_subForm == null || _subForm.IsDisposed)
            {
                _subForm = new NametableEditor();
            }

            if (!_subForm.Visible)
            {
                _subForm.NametableFilename =
                    Path.ChangeExtension(_filename, ".nametable");

                _subForm.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FillDataTable();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (dataGridView1.DataSource != null)
            {

                dataGridView1.Enabled = false;
                comboBox1.Enabled = false;
                button5.Enabled = false;

                DataTable dt = dataGridView1.DataSource as DataTable;

                if (textBox1.Text.Length > 0)
                {
                    string text = textBox1.Text;

                    uint hashKey = text.HashKey();

                    var index = comboBox1.SelectedIndex;

                    switch (index)
                    {
                        case 0:
                            dt.DefaultView.RowFilter =
                                string.Format("Type LIKE '%{0}%'", text);
                            break;
                        case 1:
                            dt.DefaultView.RowFilter =
                                string.Format("Name LIKE '%{0}%' OR Name LIKE '%{1:X}%'", text, hashKey);
                            break;
                        case 2:
                            dt.DefaultView.RowFilter =
                                string.Format("Category LIKE '%{0}%' OR Category LIKE '%{1:X}%'", text, hashKey);
                            break;

                        default:
                            dt.DefaultView.RowFilter =
                                string.Format("Name LIKE '%{0}%' OR Type LIKE '%{0}%' OR Name LIKE '%{1:X}%'", text, hashKey);
                            break;
                    }


                    dataGridView1.Enabled = true;
                    comboBox1.Enabled = true;
                    button5.Enabled = true;
                }

                else dt.DefaultView.RowFilter = null;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                DataTable dt = dataGridView1.DataSource as DataTable;
                dt.DefaultView.RowFilter = "";
            }
        }
    }
}
