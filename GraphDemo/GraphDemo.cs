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


namespace GraphDemo
{
    public  partial class  GraphDemo : Form
    {
        
        Read rr;  // a variable of type Read

        public GraphDemo()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog ff = new OpenFileDialog();

            ff.InitialDirectory = "c:\\";
            ff.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            ff.FilterIndex = 1;
            ff.RestoreDirectory = true;

            if (ff.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = ff.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            rr = null;
                            rr = new Read(myStream);
                            string[] header = rr.get_Header();
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
                            myStream.Close();
                        }
                    }
                }
                catch (Exception err)
                {
                    //Inform the user if we can't read the file
                    MessageBox.Show(err.Message);
                }
            }
          CreateFileWatcher( "C:\\blah\\" );  // tip from Andy
           // CreateFileWatcher("c:\\blah\\testv.csv" );  // tip from Andy
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog ff = new SaveFileDialog();

            ff.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
            ff.FilterIndex = 1;
            ff.RestoreDirectory = true;

            if (ff.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = ff.OpenFile()) != null)
                {
                    using (myStream)
                    {
                        chart.SaveImage(myStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }
            }
        }

        public void btnPlot_Click(object sender, EventArgs e)
        {
            if (rr != null)
            {
                Plot pl = new Plot(rr,/* xBox, yBox ,*/  chart);
            }
            else
            {
                MessageBox.Show("Error, no data to plot! Please load csv file");
                return;
            }
        }

        private void chart_Click(object sender, EventArgs e)
        {

        }
        public void CreateFileWatcher(string path)
        {
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = path;  // file/path here
            /* Watch for changes in LastAccess and LastWrite times, and 
               the renaming of files or directories. */
            //watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
            //   | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.NotifyFilter =  NotifyFilters.LastWrite   ;
            // Only watch csv files.
            watcher.Filter = "testY.csv";

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            //watcher.Created += new FileSystemEventHandler(OnChanged);
            //watcher.Deleted += new FileSystemEventHandler(OnChanged);
            //watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        //// Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        }
        
       
    }
}
