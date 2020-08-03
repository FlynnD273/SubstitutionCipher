using SubstitutionCipher.Resources;
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

namespace SubstitutionCipher
{
    public partial class CryptoForm : Form
    {
        private string key = "";
        private string file;
        public CryptoForm()
        {
            InitializeComponent();
            KeyBox.Text = CipherGenerator.Alpha;
            KeyLabel.Text = CipherGenerator.Alpha;
        }

        private void KeyBox_TextChanged(object sender, EventArgs e)
        {
            KeyBox.Text = KeyBox.Text.ToLower();
            KeyBox.Text = string.Concat(KeyBox.Text.Distinct());
            foreach (char c in CipherGenerator.Alpha)
            {
                if (!KeyBox.Text.Contains(c))
                {
                    KeyBox.Text += c;
                }
            }

            StringBuilder sb = new StringBuilder(KeyBox.Text);
            for (int i = 0; i < sb.Length; i++)
            {
                if (!CipherGenerator.Alpha.Contains(sb[i]))
                {
                    sb.Remove(i--, 1);
                }
            }
            KeyBox.Text = sb.ToString();

            key = KeyBox.Text;
        }
        private void SourceFileBox_TextChanged(object sender, EventArgs e)
        {
            if (!File.Exists(SourceFileBox.Text) && !SourceFileBox.Focused)
            {
                SourceFileBox.Text = "";
                file = "";
            }
            else
            {
                file = SourceFileBox.Text;
            }
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(key);
        }

        private void SelectFilebutton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*";
            ofd.InitialDirectory = Path.GetFullPath(Environment.SpecialFolder.MyDocuments.ToString());

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SourceFileBox.Text = ofd.FileName;
            }
        }

        private void EncodeButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(SourceFileBox.Text))
            {
                string cleartext = File.ReadAllText(file);
                Cipher cipher = new Cipher(key);
                string code = cipher.Encode(cleartext);
                string name = Path.GetFileNameWithoutExtension(file);
                if (name.EndsWith("_Decoded"))
                {
                    name = name.Remove(name.Length - 8);
                }
                string filepath = Path.Combine(Path.GetDirectoryName(file), name + "_Encoded.txt");
                File.WriteAllText(filepath, code);

                new Process
                {
                    StartInfo = new ProcessStartInfo(filepath)
                    {
                        UseShellExecute = true
                    }
                }.Start();
            }
        }

        private void DecodeButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(SourceFileBox.Text))
            {
                string code = File.ReadAllText(file);
                Cipher cipher = new Cipher(key);
                string cleartext = cipher.Decode(code);
                string name = Path.GetFileNameWithoutExtension(file);
                if (name.EndsWith("_Encoded"))
                {
                    name = name.Remove(name.Length - 8);
                }
                string filepath = Path.Combine(Path.GetDirectoryName(file), name + "_Decoded.txt");
                File.WriteAllText(filepath, cleartext);

                new Process
                {
                    StartInfo = new ProcessStartInfo(filepath)
                    {
                        UseShellExecute = true
                    }
                }.Start();
            }
        }

        private void CrackButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(SourceFileBox.Text))
            {
                string code = File.ReadAllText(file);
                CipherCracker c = new CipherCracker(code);
                var d = c.Decipher();
                string filepath = Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file) + "_Deciphered.txt");
                File.WriteAllText(filepath, $"KEY: {CipherGenerator.Alpha} = {d.Item2}\n\n{d.Item1}");

                new Process
                {
                    StartInfo = new ProcessStartInfo(filepath)
                    {
                        UseShellExecute = true
                    }
                }.Start();
            }
        }

        private void RandomKeyButton_Click(object sender, EventArgs e)
        {
            KeyBox.Text = CipherGenerator.CreateRandomKey();
        }
    }
}
