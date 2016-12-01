using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;    
using RageAudioTool.Rage_Wrappers.DatFile;

namespace RageAudioTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ReadDat4File(string filename)
        {
            if (filename.Length < 1 || !File.Exists(filename)) return;

            StringBuilder builder = new StringBuilder();

            var bytes = File.ReadAllBytes(filename);

            var md = new RageAudioMetadata4(bytes);

            using (StreamWriter writer = new StreamWriter("md4.txt"))
            {
                writer.Write(md.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReadDat4File("audioconfig.dat4.rel");
        }
    }
}
