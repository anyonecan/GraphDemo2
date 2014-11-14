using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Permissions;

namespace GraphDemo {
	public  partial class GraphDemo : Form {
		string filepath;
		FileSystemWatcher watcher = new FileSystemWatcher ();
		Read rr = new Read ();
		Plot plotter = new Plot ();
		
		public void renderFile () {
			Console.WriteLine ("Rendering "+ filepath);
			Stream myStream = File.OpenRead (filepath);
			rr.parse (myStream);
			plotter.render (rr, chart);
		}
		
		public GraphDemo () {
			InitializeComponent ();
			initFileWatcher ();
		}

		private void exitToolStripMenuItem_Click (object sender, EventArgs e) {
			Application.Exit ();
		}
        
		private void openToolStripMenuItem_Click (object sender, EventArgs e) {
			OpenFileDialog ff = new OpenFileDialog ();
			ff.InitialDirectory = "c:\\";
			ff.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
			ff.FilterIndex = 1;
			ff.RestoreDirectory = true;

			if (ff.ShowDialog () == DialogResult.OK) {
				filepath = ff.FileName;
				updateFileWatcher ();
				renderFile ();
#if guish //need to move this into its own function.			
				try {
					
					
					if ((myStream = ff.OpenFile ()) != null) {
						using (myStream) {
							rr = null;
							rr = new Read (myStream);
							string[] header = rr.get_Header ();
							//List<string> lX = new List<string>();
							//List<string> lY = new List<string>();
							//for (int i = 0; i < header.Length; i++)
							//{
							//    lX.Add(header[i]); lY.Add(header[i]);
							//}
							//Populate the ComboBoxes           -- don't need this, element labels could go where header is
							//xBox.DataSource = lX;
							//yBox.DataSource = lY;
							// Close the stream
							myStream.Close ();
						}
					}
				} catch (Exception err) {
					//Inform the user if we can't read the file
					MessageBox.Show (err.Message);
				}
#endif
			}
		
		}

		private void saveToolStripMenuItem_Click (object sender, EventArgs e) {
			SaveFileDialog ff = new SaveFileDialog ();

			ff.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
			ff.FilterIndex = 1;
			ff.RestoreDirectory = true;

			if (ff.ShowDialog () == DialogResult.OK) {
				Stream myStream;				
				if ((myStream = ff.OpenFile ()) != null) {
					using (myStream) {
#if guish
				    chart.SaveImage(myStream, System.Drawing.Imaging.ImageFormat.Jpeg);
#endif
					}
				}
			}
		}

		public void btnPlot_Click (object sender, EventArgs e) {
			if (rr != null) {//replace this null check with a Read.haveData() method
				Console.WriteLine ("Replot data");
				renderFile();
				//plotter.render (rr, /* xBox, yBox ,*/  chart);
			} else {
				MessageBox.Show ("Error, no data to plot! Please load csv file");
				return;
			}
		}

		private void chart_Click (object sender, EventArgs e) {

		}

		public void initFileWatcher () {
      //just watch for changes.
			watcher.NotifyFilter = NotifyFilters.LastWrite;
		
			// Add event handlers.
			watcher.Changed += new FileSystemEventHandler (OnChanged);
		}
		
		private void updateFileWatcher () {
			if (filepath != null && filepath.Length > 4) {
				Console.WriteLine ("Watch for changes to "+filepath);
				watcher.Path = Path.GetDirectoryName (filepath);  
				watcher.Filter = Path.GetFileName (filepath);
				// Begin watching.
				watcher.EnableRaisingEvents = true;
			} else {
				Console.WriteLine ("no longer watching a file");
				watcher.EnableRaisingEvents = false;
			}
		}

		//// Define the event handlers.
		private void OnChanged (object source, FileSystemEventArgs e) {
			// Specify what is done when a file is changed, created, or deleted.
			Console.WriteLine ("File: " + e.FullPath + " " + e.ChangeType);
			renderFile();
		}

		private void OnRenamed (object source, RenamedEventArgs e) {
			// Specify what is done when a file is renamed.
			Console.WriteLine ("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
		}
        
       
	}
}
