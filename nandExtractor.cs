/*  This file is part of Wii NAND Extractor.
 *  Copyright (C) 2009 Ben Wilson
 *  
 *  Wii NAND Extractor is free software: you can redistribute it and/or
 *  modify it under the terms of the GNU General Public License as published
 *  by the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  Wii NAND Extractor is distributed in the hope that it will be
 *  useful, but WITHOUT ANY WARRANTY; without even the implied warranty
 *  of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace NAND_Extractor
{
    public partial class nandExtractor : Form
    {
        public nandExtractor()
        {
            InitializeComponent();
        }


        /*
         * Class for fst entries.
         */
        public class fst_t
        {
            public byte[] filename = new byte[0x0B];
            public byte mode;
            public byte attr;
            public UInt16 sub;
            public UInt16 sib;
            public UInt32 size;
            public UInt32 uid;
            public UInt16 gid;
            public UInt32 x3;
        }


        /*
         * Globals.
         */
        public static byte[] key = new byte[16];
        public static byte[] iv = new byte[16];
        public static BinaryReader rom;
        public static Int32 loc_super;
        public static Int32 loc_fat;
        public static Int32 loc_fst;
        public static string nandFilename;
        public static string extractPath;
        public static int type = -1;


        /*
         * Core functions.
         */
        private void fileOpen()
        {
            FileDialog fd = new OpenFileDialog();
            fd.Filter = "Wii NAND dump (*.bin,*.img)|*.bin;*.img|All files (*.*)|*.*";
            fd.Title = "Open Wii NAND dump file";

            if (fd.ShowDialog(this) != DialogResult.Cancel)
            {
                string filename = fd.FileName.Remove(0, fd.FileName.LastIndexOf("\\") + 1);
                string details_desc = "   mode|attr   uid:gid   filesize (in bytes)";

                statusText(string.Format("Loading {0} for viewing...", filename));

                int i = filename.LastIndexOf(".");
                if (i > 0)
                    filename = filename.Remove(i, filename.Length - i);

                extractPath = Directory.GetCurrentDirectory() + "\\" + filename;
                if (File.Exists(extractPath))
                    extractPath += "-extract";

                info.Items["size"].Text = "0";
                info.Items["files"].Text = "0";
                fileView.Nodes.Clear();
                fileView.Nodes.Add("0000", filename + details_desc, 2, 2);
                
                nandFilename = fd.FileName;

                if (initNand())
                {
                    viewFST(0, 0);

                    fileView.Sort();
                    fileView.Nodes["0000"].Expand();

                    rom.Close();                    
                }
                else
                    msg_Error("Invalid or unsupported dump.");

                statusText(string.Empty);
            }
            
        }

        private bool initNand()
        {
            rom = new BinaryReader(File.Open(nandFilename, 
                                        FileMode.Open, 
                                        FileAccess.Read,
                                        FileShare.Read), 
                                    Encoding.ASCII);

                
            type = getDumpType(rom.BaseStream.Length);
            
            if ( getKey(type) )
            {
                //Console.WriteLine(BitConverter.ToString(key).Replace("-", string.Empty));

                try
                {
                    loc_super = findSuperblock();
                }
                catch
                {
                    statusText("Invalid or non-ECC NAND dump");
                    fileView.Nodes.Clear();
                    msg_Error("Can't find superblock.\nAre you sure this is a Full (with ECC) or BootMii NAND dump?");
                    return false;
                }

                //Console.WriteLine("Superblock @ {0}", loc_super);

                Int32[] n_fatlen = { 0x010000, 0x010800, 0x010800 };
                loc_fat = loc_super;
                loc_fst = loc_fat + 0x0C + n_fatlen[type];
                return true;

            }
            return false;
        }

        private int getDumpType(Int64 FileSize)
        {
            Int64[] sizes = { 536870912,    // type 0 | 536870912 == no ecc
                              553648128,    // type 1 | 553648128 == ecc
                              553649152 };  // type 2 | 553649152 == old bootmii
            for (int i = 0; i < sizes.Count(); i++)
                if (sizes[i] == FileSize)
                    return i;
            return -1;
        }

        private bool getKey(int type)
        {
            switch (type)
            {
                case 0:
                    key = readKeyfile(Directory.GetCurrentDirectory() + "\\keys.bin");
                    if (key != null)
                        return true;
                    break;
                
                case 1:
                    key = readKeyfile(Directory.GetCurrentDirectory() + "\\keys.bin");
                    if (key != null)
                        return true;
                    break;
                
                case 2:
                    rom.BaseStream.Seek(0x21000158, SeekOrigin.Begin);
                    if (rom.Read(key, 0, 16) > 0)
                        return true;
                    break;
            }

            if (Properties.Settings.Default.nand_key.Length == 32)
            {
                key = strToByte(Properties.Settings.Default.nand_key);
                msg_Info(string.Format("No new key data found, using manually entered key\n{0}\n\n" + 
                    "MAKE SURE THIS IS THE RIGHT KEY OR YOUR\nEXTRACTED FILES WILL NOT DECRYPT CORRECTLY!", 
                    BitConverter.ToString(key).Replace("-", string.Empty) ));
                return true;
            }
            msg_Error("Something went horribly wrong and I can't find the key.\nTry entering it manually from the File menu.");
            return false;
        }

        public static byte[] readKeyfile(string path)
        {
            byte[] retval = new byte[16];
            
            if (File.Exists(path))
            {
                try
                {
                    BinaryReader br = new BinaryReader(File.Open(path,
                                FileMode.Open,
                                FileAccess.Read,
                                FileShare.Read),
                                Encoding.ASCII);
                    br.BaseStream.Seek(0x158, SeekOrigin.Begin);
                    br.Read(retval, 0, 16);
                    br.Close();
                    return retval;
                }
                catch
                {
                    msg_Error(string.Format("Can't open key.bin:\n{0}\n" +
                                            "Try closing any program(s) that may be accessing it.",
                                            path));
                    return null;
                }
            }
            else
            {
                msg_Error(string.Format("You tried to open a file that doesn't exist:\n{0}", path));
                return null;
            }
        }

        private Int32 findSuperblock()
        {
            Int32 loc, current, last = 0;
            Int32[] n_start = { 0x1FC00000, 0x20BE0000, 0x20BE0000 },
                    n_end   = { 0x20000000, 0x21000000, 0x21000000 },
                    n_len   = { 0x40000, 0x42000, 0x42000 };

            rom.BaseStream.Seek(n_start[type] + 4, SeekOrigin.Begin);

            for (loc = n_start[type]; loc < n_end[type]; loc += n_len[type])
            {
                current = (int) bswap(rom.ReadUInt32());
                
                if (current > last)
                    last = current;
                else
                    return loc - n_len[type];

                rom.BaseStream.Seek(n_len[type] - 4, SeekOrigin.Current);
            }

            return -1;
        }

        private byte[] getCluster(UInt16 cluster_entry)
        {
            Int32[] n_clusterlen = { 0x4000, 0x4200, 0x4200 };
            Int32[] n_pagelen = { 0x800, 0x840, 0x840 };

            byte[] cluster = new byte[0x4000];
            byte[] page = new byte[ n_pagelen[type] ];

            rom.BaseStream.Seek(cluster_entry * n_clusterlen[type], SeekOrigin.Begin);

            for (int i = 0; i < 8; i++)
            {
                page = rom.ReadBytes( n_pagelen[type] );
                Buffer.BlockCopy(page, 0, cluster, i * 0x800, 0x800);
            }

            return aesDecrypt(key, iv, cluster);
        }

        private UInt16 getFAT(UInt16 fat_entry)
        {
            /*
             * compensate for "off-16" storage at beginning of superblock
             * 53 46 46 53   XX XX XX XX   00 00 00 00
             * S  F  F  S     "version"     padding?
             *   1     2       3     4       5     6
             */
            fat_entry += 6;

            // location in fat of cluster chain
            Int32[] n_fat = { 0, 0x20, 0x20 };
            int loc = loc_fat + ((((fat_entry / 0x400) * n_fat[type]) + fat_entry) * 2);

            rom.BaseStream.Seek(loc, SeekOrigin.Begin);
            return bswap(rom.ReadUInt16());
        }

        private fst_t getFST(UInt16 entry)
        {
            fst_t fst = new fst_t();

            // compensate for 64 bytes of ecc data every 64 fst entries
            Int32[] n_fst = { 0, 2, 2 };
            int loc_entry = (((entry / 0x40) * n_fst[type]) + entry) * 0x20;

            rom.BaseStream.Seek(loc_fst + loc_entry, SeekOrigin.Begin);

            fst.filename = rom.ReadBytes(0x0C);
            fst.mode = rom.ReadByte();
            fst.attr = rom.ReadByte();
            fst.sub = bswap(rom.ReadUInt16());
            fst.sib = bswap(rom.ReadUInt16());
            fst.size = bswap(rom.ReadUInt32());
            fst.uid = bswap(rom.ReadUInt32());
            fst.gid = bswap(rom.ReadUInt16());
            fst.x3 = bswap(rom.ReadUInt32());

            fst.mode &= 1;

            return fst;
        }


        /*
         * Viewer functions.
         */
        private void viewFST(UInt16 entry, UInt16 parent)
        {
            fst_t fst = getFST(entry);

            if (fst.sib != 0xffff)
                viewFST(fst.sib, parent);

            addEntry(fst, entry, parent);
            
            info.Items["size"].Text = (Convert.ToInt32(info.Items["size"].Text) + (int)fst.size).ToString();
            info.Items["files"].Text = (Convert.ToInt32(info.Items["files"].Text) + 1).ToString();
            Application.DoEvents();
        }

        private void addEntry(fst_t fst, UInt16 entry, UInt16 parent)
        {
            string details;
            string[] modes = { "d|", "f|" };
            TreeNode[] node = fileView.Nodes.Find(parent.ToString("x4"), true);
            
            details = ASCIIEncoding.ASCII.GetString(fst.filename).Replace("\0", " ");
            details += txtPadLeft(modes[fst.mode], 5);
            details += txtPadRight( fst.attr.ToString(), 3 );
            details += string.Format("{0}:{1}", 
                            fst.uid.ToString("x4").ToUpper(),
                            fst.gid.ToString("x4").ToUpper() );
            if (fst.size > 0)
                details += txtPadLeft(fst.size.ToString("d"), 11) + "B";
           
            if (entry != 0)
            {
                if (node.Count() > 0)
                    node[0].Nodes.Add(entry.ToString("x4"), details, fst.mode, fst.mode);
                else
                    fileView.Nodes.Add(entry.ToString("x4"), details, fst.mode, fst.mode);
            }

            if (fst.mode == 0 && fst.sub != 0xffff)
                viewFST(fst.sub, entry);
        }
        

        /*
         * Extraction functions.
         */
        private void extractNAND()
        {
            statusText("Extracting NAND...");

            rom = new BinaryReader(File.Open(nandFilename,
                                                    FileMode.Open,
                                                    FileAccess.Read,
                                                    FileShare.Read),
                                                Encoding.ASCII);
            if (!Directory.Exists(extractPath))
                Directory.CreateDirectory(extractPath);

            extractFST(0, "");

            statusText(string.Empty);
            
            rom.Close();
        }

        private void extractFST(UInt16 entry, string parent)
        {
            fst_t fst = getFST(entry);

            if (fst.sib != 0xffff)
                extractFST(fst.sib, parent);

            switch (fst.mode)
            {
                case 0:
                    extractDir(fst, entry, parent);
                    break;
                case 1:
                    extractFile(fst, entry, parent);
                    break;
                default:
                    msg_Error(String.Format("Ignoring unsupported mode {0}.\n\t\t(FST entry #{1})",
                                                fst.mode, 
                                                entry.ToString("x4")));
                    break;
            }
        }

        private void extractSingleFST(UInt16 entry, string parent)
        {
            if (!Directory.Exists(extractPath))
                Directory.CreateDirectory(extractPath);

            fst_t fst = getFST(entry);

            switch (fst.mode)
            {
                case 0:
                    extractDir(fst, entry, parent);
                    break;
                case 1:
                    extractFile(fst, entry, parent);
                    break;
                default:
                    msg_Error(String.Format("Ignoring unsupported mode {0}.\n\t\t(FST entry #{1})",
                                                fst.mode, 
                                                entry.ToString("x4")));
                    break;
            }
        }

        private void extractDir(fst_t fst, UInt16 entry, string parent)
        {
            string filename = ASCIIEncoding.ASCII.GetString(fst.filename).Replace("\0", string.Empty);

            statusText(string.Format("Extracting:  {0}", filename));

            if (filename != "/")
            {
                if (parent != "/" && parent != "")
                    filename = parent + "\\" + filename;
             
                Directory.CreateDirectory(extractPath + "\\" + filename);
            }

            if (fst.sub != 0xffff)
                extractFST(fst.sub, filename);
        }

        private void extractFile(fst_t fst, UInt16 entry, string parent)
        {
            UInt16 fat;
            int cluster_span = (int) (fst.size / 0x4000) + 1;
            byte[] cluster = new byte[0x4000],
                   data = new byte[cluster_span * 0x4000];

            string filename = "\\" + parent + "\\" +
                            ASCIIEncoding.ASCII.GetString(fst.filename).
                            Replace("\0", string.Empty).
                            Replace(":", "-");

            try
            {
                BinaryWriter bw = new BinaryWriter(File.Open(extractPath + filename,
                                                                FileMode.Create,
                                                                FileAccess.Write,
                                                                FileShare.Read),
                                                            Encoding.ASCII);
                fat = fst.sub;
                for (int i = 0; fat < 0xFFF0; i++)
                {
                    statusText(string.Format("Extracting:  {0} (cluster {1})", filename, fat));
                    Buffer.BlockCopy(getCluster(fat), 0, data, i * 0x4000, 0x4000);
                    fat = getFAT(fat);
                }

                bw.Write(data, 0, (int)fst.size);
                bw.Close();
            }
            catch
            {
                msg_Error(string.Format("Can't open file for writing:\n{0}",
                                            extractPath + filename) );
            }
        }



        /*
         * Crypto functions (encryption unused, but included for reference).
         * Key required length of 16 bytes.
         * IV can be from 1 to 16 byte(s) and will be padded with 0x00.
         */
        private byte[] aesDecrypt(byte[] key, byte[] iv, byte[] enc_data)
        {
            // zero out any remaining iv bytes
            byte[] iv16 = new byte[16];
            Buffer.BlockCopy(iv, 0, iv16, 0, iv.Length);

            RijndaelManaged aes = new RijndaelManaged();
            aes.Padding = PaddingMode.None;
            aes.Mode = CipherMode.CBC;

            ICryptoTransform decryptor = aes.CreateDecryptor(key, iv16);
            MemoryStream memoryStream = new MemoryStream(enc_data);
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                      decryptor,
                                                      CryptoStreamMode.Read);

            byte[] dec_data = new byte[enc_data.Length];

            int decryptedByteCount = cryptoStream.Read(dec_data, 0,
                                                   dec_data.Length);

            memoryStream.Close();
            cryptoStream.Close();

            //Console.WriteLine("Decrypted {0} bytes:", decryptedByteCount);

            Application.DoEvents();
            return dec_data;
        }

        private byte[] aesEncrypt(byte[] key, byte[] iv, byte[] dec_data)
        {
            // zero out any remaining iv bytes
            byte[] iv16 = new byte[16];
            Buffer.BlockCopy(iv, 0, iv16, 0, iv.Length);

            RijndaelManaged aes = new RijndaelManaged();
            aes.Padding = PaddingMode.None;
            aes.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = aes.CreateEncryptor(key, iv16);
            MemoryStream memoryStream = new MemoryStream(dec_data);
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                      encryptor,
                                                      CryptoStreamMode.Read);

            byte[] enc_data = new byte[dec_data.Length];

            int encryptedByteCount = cryptoStream.Read(enc_data, 0,
                                                   enc_data.Length);

            memoryStream.Close();
            cryptoStream.Close();

            //Console.WriteLine("Encrypted {0} bytes:", encryptedByteCount);

            Application.DoEvents();
            return enc_data;
        }


        /*
         * Helper/misc functions.
         */
        public static void msg_Error(string message)
        {
            MessageBox.Show(Form.ActiveForm, message, "Error!", 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Error);
        }

        public static void msg_Info(string message)
        {
            MessageBox.Show(Form.ActiveForm, message, "Information!", 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Information);
        }

        public static UInt16 bswap(UInt16 value)
        {
            return (UInt16)((0x00FF & (value >> 8))
                             | (0xFF00 & (value << 8)));
        }

        public static UInt32 bswap(UInt32 value)
        {
            UInt32 swapped = ((0x000000FF) & (value >> 24)
                             | (0x0000FF00) & (value >> 8)
                             | (0x00FF0000) & (value << 8)
                             | (0xFF000000) & (value << 24));
            return swapped;
        }

        public static byte[] strToByte(string hexString)
        {
            hexString = System.Text.RegularExpressions.Regex.Replace(hexString.ToUpper(), "[^0-9A-F]", string.Empty);
            byte[] b = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i += 2)
                b[i / 2] = byte.Parse(hexString.Substring(i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

            return b;
        }

        public void statusText(string message)
        {
            status.Text = message.Replace("\\", "/");
            if (message == string.Empty)
            {
                contextExtract.Enabled = true;
                extractAllFileMenu.Enabled = true;
            }
            else
            {
                contextExtract.Enabled = false;
                extractAllFileMenu.Enabled = false;
            }
            Application.DoEvents();
        }

        public void nandExtractor_Resize(object sender, System.EventArgs e)
        {
            this.fileView.Width = Size.Width - 28;
            this.fileView.Height = Size.Height - 102; // 80 w/out new info bar
        }

        private string txtPadLeft(string textString, int desiredLength)
        {
            while (textString.Length < desiredLength)
                textString = string.Concat(" ", textString);
            return textString;
        }
        
        private string txtPadRight(string textString, int desiredLength)
        {
            while (textString.Length < desiredLength)
                textString = string.Concat(textString, " ");
            return textString;
        }


        /*
         * Menu functions.
         */

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileOpen();
        }

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extractTime.Text = string.Empty;
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            extractNAND();

            stopwatch.Stop();

            extractTime.Text = stopwatch.Elapsed.ToString();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this,   "Wii NAND Extractor\n" +
                                    "Version " + Application.ProductVersion + "\n\n" +
                                    "Copyright 2009 Ben Wilson / parannoyed\n" +
                                    "http://sites.google.com/site/parannoyedwii/", "About",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Question);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void contextExtract_Click(object sender, EventArgs e)
        {
            if (fileView.SelectedNode == null)
                msg_Error("Try choosing a file/directory.");
            else
            {
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();

                rom = new BinaryReader(File.Open(nandFilename,
                                                    FileMode.Open,
                                                    FileAccess.Read,
                                                    FileShare.Read),
                                                Encoding.ASCII);
                if (rom.BaseStream.Length > 0)
                    extractSingleFST(Convert.ToUInt16(fileView.SelectedNode.Name, 16), "");

                rom.Close();

                stopwatch.Stop();

                statusText(string.Empty);
                extractTime.Text = stopwatch.Elapsed.ToString();
            }
        }

        private void fileView_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button != MouseButtons.Right)
                return;
            fileView.SelectedNode = fileView.GetNodeAt(e.X,e.Y);
        }

        private void enterNandKeyMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new NandKey();
            frm.ShowDialog(this);
        }
    }
}
