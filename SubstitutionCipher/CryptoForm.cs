using SubstitutionCipher.Resources;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//This is the form with all the controls

namespace SubstitutionCipher
{
    public partial class CryptoForm : Form
    {
        private string key = "";
        private string file;
        public CryptoForm()
        {
            InitializeComponent();

            //Initialize the labels
            KeyBox.Text = CipherGenerator.Alpha;
            KeyLabel.Text = CipherGenerator.Alpha;
        }

        //Make sure there aren't any invalid or repeating characters
        private void KeyBox_Validated (object sender, EventArgs e)
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

        //Check if it's a valid filepath
        private void SourceFileBox_Validated (object sender, EventArgs e)
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

        //Copy the key into clipboard
        private void CopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(key);
        }

        //Open a text file
        private void SelectFilebutton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*";
            ofd.InitialDirectory = Path.GetFullPath(Environment.SpecialFolder.MyDocuments.ToString());

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SourceFileBox.Text = ofd.FileName;
                file = ofd.FileName;
            }
        }

        //Encipher text from a file (No text boxes yet)
        private void EncodeButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(file) && file != "")
            {
                string cleartext = File.ReadAllText(file);
                Cipher cipher = new Cipher(key);
                string code = cipher.Encode(cleartext);
                string name = Path.GetFileNameWithoutExtension(file);
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

        //Decipher from a text file
        private void DecodeButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(file) && file != "")
            {
                string code = File.ReadAllText(file);
                Cipher cipher = new Cipher(key);
                string cleartext = cipher.Decode(code);
                string name = Path.GetFileNameWithoutExtension(file);
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

        //Decipher code with an unknown key from a text file
        private void CrackButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(file) && file != "")
            {
                string code = File.ReadAllText(file);
                CipherCracker c = new CipherCracker(code);
                var d = c.Decipher();
                string filepath = Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file) + "_Deciphered.txt");
                File.WriteAllText(filepath, $"KEY: {CipherGenerator.Alpha} = {d.Item2}\n\n{d.Item1}");
                KeyBox.Text = d.Item2;

                new Process
                {
                    StartInfo = new ProcessStartInfo(filepath)
                    {
                        UseShellExecute = true
                    }
                }.Start();
            }
        }

        //Generate a random key
        private void RandomKeyButton_Click(object sender, EventArgs e)
        {
            KeyBox.Text = CipherGenerator.CreateRandomKey();
            key = KeyBox.Text;
        }
    }
}
