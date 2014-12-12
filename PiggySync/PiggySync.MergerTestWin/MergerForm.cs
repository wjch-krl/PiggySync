using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Runtime.Serialization.Formatters;

namespace PiggySync.MergerTestWin
{
	public partial class MergerForm : Form
	{
		private string fileAPath;
		private string fileBPath;
		private string resultPath;

		public MergerForm ()
		{
			InitializeComponent ();
		}

		private void button1_Click (object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog () == System.Windows.Forms.DialogResult.OK)
			{
				fileAPath = openFileDialog1.FileName;
			}
		}

		private void fileBButton_Click (object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog () == System.Windows.Forms.DialogResult.OK)
			{
				fileBPath = openFileDialog1.FileName;
			}
		}

		private void mergeButton_Click (object sender, EventArgs e)
		{
			if (saveFileDialog1.ShowDialog () == System.Windows.Forms.DialogResult.OK)
			{
				resultPath = saveFileDialog1.FileName;
				if (String.IsNullOrWhiteSpace (fileAPath) || String.IsNullOrWhiteSpace (fileBPath))
				{
					MessageBox.Show ("First load files.");
				}
				var merger = new FileMerger.FileMerger (fileAPath, fileBPath, resultPath);
				if (merger.MergeFiles ())
				{
					richTextBox1.Lines = File.ReadAllLines (resultPath);
					richTextBox1.Enabled = true;
					MessageBox.Show ("Files succesfully Merged.");
				}
				else
				{
					richTextBox1.Lines = new []{ string.Empty };
					richTextBox1.Enabled = false;
					MessageBox.Show ("Can't merge files.");
				}
			}

		}
	}
}
