using System;
using System.Text;
using System.Windows.Forms;
using System.IO;    
using RageAudioTool.Rage_Wrappers.DatFile;
using RageAudioTool.Rage_Wrappers.DatFile.XML;

namespace RageAudioTool
{
    public partial class MainForm : Form
    {
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

            // File.WriteAllText(Path.ChangeExtension(filename, ".txt"), md.ToString());

            var xml = new ResourceXMLWriter5(Path.ChangeExtension(filename, ".xml"));

            xml.WriteData(md);

            /*
            using (StreamWriter writer = new StreamWriter(Path.ChangeExtension(filename, ".txt")))
            {
                writer.Write(md.ToString());
            }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
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

           // ReadDat4File("audio/config/sounds.dat54.rel");

           ReadDat4File("audio/config/sounds.dat54.rel");

            //  var md = new ResourceXMLReader5("audio/config/dlcjanuary2016_sounds.xml");

            //  var result = md.ReadData();

            //  MessageBox.Show(result.Length.ToString());

            //  ReadDat4File("audio/config/dlcmpheist_sounds.dat54.rel");

        }
    }
}
