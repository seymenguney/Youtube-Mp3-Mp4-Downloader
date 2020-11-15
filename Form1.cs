using System;
using System.IO;
using System.Windows.Forms;
using VideoLibrary;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Drawing;
using static System.Windows.Forms.LinkLabel;
using MediaToolkit;
using System.Net;

namespace Youtube_MP3_MP4_Download
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Boolean format = true;

        //T--> MP3 F--> MP4


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Please Select Folder" })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    GetTitle();
                    MessageBox.Show("Download Started Please Wait.", "İnformation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var yt = YouTube.Default;
                    var video = await yt.GetVideoAsync(link.Text);
                    File.WriteAllBytes(fbd.SelectedPath + @"\" + video.FullName, await video.GetBytesAsync());

                    var inputfile = new MediaToolkit.Model.MediaFile { Filename = fbd.SelectedPath + @"\" + video.FullName };
                    var outputfile = new MediaToolkit.Model.MediaFile { Filename = $"{fbd.SelectedPath + @"\" + video.FullName }.mp3" };

                    using (var enging = new Engine())
                    {
                        enging.GetMetadata(inputfile);
                        enging.Convert(inputfile, outputfile);
                    }

                    if (format == true)
                    {
                        File.Delete(fbd.SelectedPath + @"\" + video.FullName);
                    }

                    else
                    {
                        File.Delete($"{fbd.SelectedPath + @"\" + video.FullName}.mp3");
                    }

                    progressBar1.Value = 100;

                    MessageBox.Show("Download Complete.", "İnformation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Select File Path Please.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            format = true;
        }
        void GetTitle()
        {
            WebRequest want = HttpWebRequest.Create(link.Text);
            WebResponse reply;
            reply = want.GetResponse();
            StreamReader sr = new StreamReader(reply.GetResponseStream());
            string coming = sr.ReadToEnd();
            int start = coming.IndexOf("<title>") + 7;
            int finish = coming.Substring(start).IndexOf("</title>");
            string cominginfo = coming.Substring(start, finish);
            label1.Text = (cominginfo);            
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            format = false;
        }
    }
}

