using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wallpaper_Engine_Fixer
{
    public partial class Form1 : Form
    {
        int pSucces = 1;
        public Form1()
        {
            InitializeComponent();
            label1.Parent = pictureBox1;
            label2.Parent = pictureBox1;
            applyBtn.Parent = pictureBox1;
            browseBtn1.Parent = pictureBox1;
            browseBtn2.Parent = pictureBox1;
            browseBtn2.TabIndex = 0;
            browseBtn1.TabIndex = 0;
            applyBtn.TabIndex = 0;
        }

        private void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation)
        {
            string shortcutLocation = System.IO.Path.Combine(shortcutPath, shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);
            shortcut.Description = "*DON'T DELETE!* - Created By Wallpaper Engine Fixer";
            shortcut.TargetPath = targetFileLocation;
            shortcut.WorkingDirectory = textBox1.Text;
            shortcut.Save();
        }

        private void applyBtn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty)
            {
                MessageBox.Show("Please do not leave text boxes blank!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    System.IO.File.WriteAllText(textBox1.Text + "\\Helper.bat", "wallpaper32.exe -control pause" + Environment.NewLine + "wallpaper32.exe -control openWallpaper -file " + '"' + textBox2.Text + '"');
                    CreateShortcut("Helper", Environment.GetFolderPath(Environment.SpecialFolder.Startup), textBox1.Text + "\\Helper.bat");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), ex.GetType().FullName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pSucces = 0;
                }


                if (pSucces == 1)
                {
                    MessageBox.Show("Wallpaper Engine Startup Fix completed successfully. Please restart your system","End...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void browseBtn1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (System.IO.File.Exists(folderBrowserDialog1.SelectedPath + "\\wallpaper32.exe"))
                {
                    textBox1.Text = folderBrowserDialog1.SelectedPath;
                }
                else
                {
                    MessageBox.Show("The file wallpaper32.exe was not found!", "System.IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    browseBtn1_Click(sender, e);
                }
            }
        }

        private void browseBtn2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
            }
        }
    }
}
