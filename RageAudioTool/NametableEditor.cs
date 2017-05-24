using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool
{
    public partial class NametableEditor : Form
    {
        public string NametableFilename { get; set; }

        public NametableEditor()
        {
            InitializeComponent();
        }

        private void ReadNametableEntries()
        {
            if (!File.Exists(NametableFilename)) return;

            DataTable dt = new DataTable();

            dt.Columns.Add("Hash Name");

            dt.Columns.Add("Hash Key");

            using (var reader = new BinaryReader(File.Open(NametableFilename, FileMode.Open)))
            {
                char result;

                string text = string.Empty;

                while (true)
                {
                    if (reader.BaseStream.Position >= reader.BaseStream.Length)
                        break;

                    text = string.Empty;

                    while ((result = reader.ReadChar()) != '\0')
                    {
                        text += result;
                    }

                    dt.Rows.Add(text, text.HashKey());
                }
            }

            dataGridView1.DataSource = dt;
        }

        protected override void OnLoad(EventArgs e)
        {
            ReadNametableEntries();

            base.OnLoad(e);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                string newValue = (string)dataGridView1.Rows[e.RowIndex].Cells[0].Value ?? "";

                dataGridView1.Rows[e.RowIndex].Cells[1].Value = newValue.HashKey();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(NametableFilename))
            {
                DataTable dt = dataGridView1.DataSource as DataTable;

                using (var writer = new IOBinaryWriter(File.Open(NametableFilename, FileMode.Create)))
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Visible)
                        {
                            var dgvCell = dataGridView1.Rows[i].Cells[0] as DataGridViewCell;

                            if (dgvCell.Value != null)
                            {
                                writer.WriteAnsi(((string)dgvCell.Value).ToLower());
                            }
                        }
                    }
                }
            }

            Close();
        }
    }
}
