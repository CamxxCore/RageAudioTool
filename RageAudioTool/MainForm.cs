using System;
using System.Text;
using System.Windows.Forms;
using System.IO;    
using RageAudioTool.Rage_Wrappers.DatFile;
using RageAudioTool.Rage_Wrappers.DatFile.XML;
using System.Data;

namespace RageAudioTool
{
    public partial class MainForm : Form
    {
        string filename = string.Empty;

        private RageDataFile currentFile = null;

        NametableEditor subForm;

        public MainForm()
        {
            InitializeComponent();
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

           var xml = new ResourceXMLWriter5(Path.ChangeExtension(filename, ".xml"));

             xml.WriteData(md);

            /*
            using (StreamWriter writer = new StreamWriter(Path.ChangeExtension(filename, ".txt")))
            {
                writer.Write(md.ToString());
            }*/
        }

        private void FillDataTable()
        {
            if (currentFile != null && File.Exists(filename))
            {
                currentFile.Dispose();

                using (RageDataFileReadReference file = new RageDataFileReadReference(filename))
                {
                    currentFile.Read(file);
                }

                DataTable dt = new DataTable();

                if (currentFile is RageAudioMetadata4)
                {
                    dt.Columns.Add("Type");
                    dt.Columns.Add("Name");

                    for (int i = 0; i < currentFile.DataItems.Length; i++)
                    {
                        var item = currentFile.DataItems[i];

                        dt.Rows.Add(new string[] { item.GetType().Name, item.Name.ToString() });
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

                    for (int i = 0; i < currentFile.DataItems.Length; i++)
                    {
                        var item = currentFile.DataItems[i];

                        string category = ((item is audSoundBase) ? (item as audSoundBase).CategoryName.HashName : "N/A");

                        dt.Rows.Add(new string[] { item.GetType().Name, category, item.Name.ToString() });
                    }

                    dataGridView1.DataSource = dt;

                    DataGridViewColumn column = dataGridView1.Columns[0]; // column[1] selects the required column 
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                    column = dataGridView1.Columns[1]; // column[1] selects the required column 
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }       
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            filename = string.Empty;

            currentFile = null;

            using (var ofd = 
                new OpenFileDialog() { Multiselect = true,
                    Filter = "RAGE audio files (*.dat54.rel,*.dat151.rel,*.dat4.rel)|*.dat54.rel;*.dat151.rel;*.dat4.rel"
                })
            {
                var result = ofd.ShowDialog();

                switch (result)
                {
                    case DialogResult.Cancel: break;
                    case DialogResult.OK:

                        filename = ofd.FileName;

                        Text = "RageAudioTool - [" + filename + "]";

                        if (filename.EndsWith(".dat54.rel") ||
                            filename.EndsWith(".dat151.rel") ||
                            Path.GetFileNameWithoutExtension(filename) == "speech2.dat4" ||
                            Path.GetFileNameWithoutExtension(filename) == "speech.dat4")
                        {
                            currentFile = new RageAudioMetadata5();
                        }

                        else if (filename.EndsWith(".dat4.rel"))
                        {
                            currentFile = new RageAudioMetadata4();
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

        private void dataGridView1_SelectedItemChanged(object sender, EventArgs e)
        {
            if (currentFile == null ||
                dataGridView1.SelectedRows.Count < 1 ||
                dataGridView1.SelectedRows[0].Index > currentFile.DataItems.Length)
                return;

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            DataTable dt = dataGridView1.DataSource as DataTable;

            var realRowIdx = dt.Rows.IndexOf(((DataRowView)selectedRow.DataBoundItem).Row);
               
            propertyGrid1.SelectedObject = currentFile.DataItems[realRowIdx];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentFile != null)
            {
                var extension = Path.GetExtension(filename);

                string filter = string.Format("RAGE Audio File (*{0})|*{0}|XML File (*.xml)|*.xml|Text file (*.txt)|*.txt|All files (*.*)|*.*", extension);

                using (var sfd = new SaveFileDialog { Filter = filter })
                {
                    sfd.FileName = Path.GetFileName(filename);

                    var result = sfd.ShowDialog();

                    switch (result)
                    {
                        case DialogResult.Cancel: return;
                        case DialogResult.OK:
                            Enabled = false;
                            string ext = Path.GetExtension(sfd.FileName);

                            if (ext == ".xml")
                            {
                                if (currentFile is RageAudioMetadata4)
                                {
                                    MessageBox.Show("Error: Exporting of this format to xml is not yet supported.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                    return;

                                    //var xml = new ResourceXMLWriter5(Path.ChangeExtension(filename, ".xml"));

                                    // xml.WriteData(currentFile as RageAudioMetadata5);
                                }

                                else
                                {
                                    var xml = new ResourceXMLWriter5(Path.ChangeExtension(sfd.FileName, ".xml"));

                                    xml.WriteData(currentFile as RageAudioMetadata5);
                                }
                            }

                            else if (ext == ".txt")
                            {
                                File.WriteAllText(Path.ChangeExtension(sfd.FileName, ".txt"), currentFile.ToString());
                            }

                            else
                            {
                                using (RageDataFileWriteReference file = new RageDataFileWriteReference(sfd.FileName))
                                {
                                    currentFile.Write(file);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null)
            {
                DataTable dt = (dataGridView1.DataSource as DataTable);

                if (textBox1.Text.Length > 0)
                {
                    string text = textBox1.Text;

                    uint hashKey = text.HashKey();

                    dt.DefaultView.RowFilter =
                        string.Format("Name LIKE '%{0}%' OR Name LIKE '%{1}%'", text, hashKey.ToString("X"));
                }

                else dt.DefaultView.RowFilter = null;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (subForm == null || subForm.IsDisposed)
            {
                subForm = new NametableEditor();
            }

            if (!subForm.Visible)
            {
                subForm.NametableFilename =
                    Path.ChangeExtension(filename, ".nametable");

                subForm.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FillDataTable();
        }
    }
}
