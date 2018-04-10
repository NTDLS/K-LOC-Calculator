using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace K_LOC_Calculator
{
    public partial class FormMain : Form
    {
        private string StandardTypes = "c,cpp,cs,h,xaml,htm,html,vb,tt,xml,sql,res,cmd,bat,ps1,config";


        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            comboBoxMask.Items.AddRange(StandardTypes.Split(',').ToList().OrderBy(o => o).ToArray());

            List<string> maskList;

            try
            {
                textBoxSourceCodeLocation.Text = GetRegistryString("", "SourceCodeLocation", "");

                string masks = GetRegistryString("", "FileMasks", StandardTypes);
                if (masks.Length == 0)
                {
                    masks = StandardTypes;
                }
                maskList = masks.Split(',').ToList().OrderBy(o => o).ToList();
            }
            catch
            {
                maskList = StandardTypes.Split(',').ToList().OrderBy(o => o).ToList();
            }

            foreach (string ext in maskList)
            {
                AddExtensionToList(ext);
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                StringBuilder masks = new StringBuilder();

                foreach (string ext in listBoxMasks.Items)
                {
                    masks.AppendFormat("{0},", ext);
                }

                if (masks.Length > 0)
                {
                    masks.Length--; //Trim off the trailing comma.
                }

                SetRegistryString("", "SourceCodeLocation", textBoxSourceCodeLocation.Text);
                SetRegistryString("", "FileMasks", masks.ToString());
            }
            catch
            {
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxMask.Text.Trim().Length > 0)
            {
                AddExtensionToList(comboBoxMask.Text);
            }
            comboBoxMask.Focus();
        }

        void AddExtensionToList(string ext)
        {
            if (ext.StartsWith("."))
            {
                ext = "*" + ext;
            }
            else if (ext.StartsWith("*."))
            {
            }
            else
            {
                ext = "*." + ext;
            }

            if (listBoxMasks.Items.Contains(ext.ToLower()) == false)
            {
                listBoxMasks.Items.Add(ext.ToLower());
                listBoxMasks.Sorted = true;
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxMasks.SelectedItem != null)
            {
                int selectedIndex = listBoxMasks.SelectedIndex;
                listBoxMasks.Items.Remove(listBoxMasks.SelectedItem);

                if (listBoxMasks.Items.Count > selectedIndex)
                {
                    listBoxMasks.SelectedIndex = selectedIndex;
                }
                else if (listBoxMasks.Items.Count > 0 && listBoxMasks.Items.Count <= selectedIndex)
                {
                    listBoxMasks.SelectedIndex = listBoxMasks.Items.Count - 1;
                }
                else if (listBoxMasks.Items.Count > 0)
                {
                    listBoxMasks.SelectedIndex = 0;
                }

            }
            listBoxMasks.Focus();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            fbd.SelectedPath = textBoxSourceCodeLocation.Text;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBoxSourceCodeLocation.Text = fbd.SelectedPath;
            }
        }

        void CancelOperation()
        {
            buttonCalculate.Text = "Cancelling...";
            buttonCalculate.Enabled = false;
            klocState.Worker.CancelAsync();
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            if (buttonCalculate.Text == "Cancel")
            {
                CancelOperation();
                return;
            }
            else if (buttonCalculate.Text == "Calculate")
            {
                klocState = new KLOCState();

                buttonCalculate.Text = "Cancel";
                klocState.BasePath = textBoxSourceCodeLocation.Text;

                listViewProgress.Items.Clear();

                klocState.Worker = new BackgroundWorker();
                klocState.Worker.WorkerReportsProgress = true;
                klocState.Worker.ProgressChanged += Worker_ProgressChanged;
                klocState.Worker.DoWork += Worker_DoWork;
                klocState.Worker.RunWorkerAsync();
                klocState.Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                klocState.Worker.WorkerSupportsCancellation = true;

                foreach (string ext in listBoxMasks.Items)
                {
                    klocState.Masks.Add(ext.ToLower());
                }
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (buttonCalculate.Text == "Cancel")
            {
                MessageBox.Show("Complete!", "K-LOC Calculator", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (buttonCalculate.Text == "Cancelling...")
            {
                MessageBox.Show("The operation was cancelled.", "K-LOC Calculator", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            buttonCalculate.Text = "Calculate";
            buttonCalculate.Enabled = true;
        }

        #region Registry.

        public const string RegistryKey = "SOFTWARE\\NetworkDLS\\K-LOC Calculator";

        static void CreateRegistryFolder(string path)
        {
            Registry.LocalMachine.CreateSubKey(path);
        }

        static public void SetRegistryString(string subPath, string valueName, string value)
        {
            RegistryKey regKey = Registry.LocalMachine;
            CreateRegistryFolder(RegistryKey + "\\" + subPath);
            RegistryKey regSubKey = regKey.OpenSubKey(RegistryKey + "\\" + subPath, true);
            regSubKey.SetValue(valueName, value);
        }
        static public string GetRegistryString(string subPath, string valueName)
        {
            return GetRegistryString(subPath, valueName, "");
        }

        static public string GetRegistryString(string subPath, string valueName, string Default)
        {
            RegistryKey regKey = Registry.LocalMachine;
            CreateRegistryFolder(RegistryKey + "\\" + subPath);
            RegistryKey regSubKey = regKey.OpenSubKey(RegistryKey + "\\" + subPath);

            object value = regSubKey.GetValue(valueName);

            if (value != null)
            {
                string stringValue = value.ToString();

                if (stringValue == null)
                {
                    return Default;
                }

                return stringValue;
            }

            return Default;
        }

        static public void SetRegistryInt(string subPath, string valueName, int value)
        {
            RegistryKey regKey = Registry.LocalMachine;
            CreateRegistryFolder(RegistryKey + "\\" + subPath);
            RegistryKey regSubKey = regKey.OpenSubKey(RegistryKey + "\\" + subPath, true);
            regSubKey.SetValue(valueName, value);
        }

        static public int GetRegistryInt(string subPath, string valueName)
        {
            return GetRegistryInt(subPath, valueName, 0);
        }

        static public int GetRegistryInt(string subPath, string valueName, int Default)
        {
            RegistryKey regKey = Registry.LocalMachine;
            RegistryKey regSubKey = regKey.OpenSubKey(RegistryKey + "\\" + subPath);

            Object value = regSubKey.GetValue(valueName);

            if (value == null)
            {
                return Default;
            }

            return int.Parse(value.ToString());
        }

        static public void SetRegistryBool(string subPath, string valueName, bool value)
        {
            SetRegistryInt(subPath, valueName, Convert.ToInt32(value));
        }

        static public bool GetRegistryBool(string subPath, string valueName)
        {
            return GetRegistryInt(subPath, valueName) != 0;
        }

        static public bool GetRegistryBool(string subPath, string valueName, bool Default)
        {
            return GetRegistryInt(subPath, valueName, Convert.ToInt32(Default)) != 0;
        }

        #endregion

        #region Calculate.

        private class KLOCState
        {
            public List<string> Masks = new List<string>();
            public Dictionary<string, int> Counts = new Dictionary<string, int>();
            public string BasePath;
            public BackgroundWorker Worker;
            public int TotalLineCount = 0;
            public int BlankLineCount = 0;
            public int CommentsLineCount = 0;
            public int FilesCount = 0;
        }

        KLOCState klocState;

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            EnumerateFiles(klocState.BasePath);
            if (klocState.Worker.CancellationPending)
            {
                return;
            }

            foreach (var folder in Directory.EnumerateDirectories(klocState.BasePath))
            {
                if (klocState.Worker.CancellationPending)
                {
                    return;
                }

                EnumerateFiles(folder);
            }
        }

        void EnumerateFiles(string path)
        {
            try
            {
                foreach (string mask in klocState.Masks)
                {

                    foreach (var filePath in Directory.EnumerateFiles(path, mask, SearchOption.AllDirectories))
                    {
                        if (klocState.Worker.CancellationPending)
                        {
                            return;
                        }

                        ProcessFile(mask, filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    if (MessageBox.Show(ex.Message + "\r\n  Continue?", "K-LOC Calculator", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                    {
                        klocState.Worker.CancelAsync();
                    }
                }));
            }
        }

        enum CommentLineResult
        {
            None,
            SingleLine,
            BeginMultiLine,
            EndMultiLine
        }

        static Regex xmlMultiCommentBegin = new Regex("/<!--", RegexOptions.IgnoreCase);
        static Regex xmlMultiCommentEnd = new Regex("-->/", RegexOptions.IgnoreCase);

        static Regex cMultiCommentBegin = new Regex(@"\/\*", RegexOptions.IgnoreCase);
        static Regex cMultiCommentEnd = new Regex(@"\*\/", RegexOptions.IgnoreCase);

        static Regex cSingleCommentBegin = new Regex(@"\/\/", RegexOptions.IgnoreCase);
        static Regex vbSingleCommentBegin = new Regex(@"\'", RegexOptions.IgnoreCase);

        CommentLineResult IsCommentLine(string mask, string line)
        {
            if (mask == "*.xml" || mask == "*.html")
            {
                var inMatches = xmlMultiCommentBegin.Matches(line);
                var outMatches = xmlMultiCommentEnd.Matches(line);
                if (inMatches.Count > 0 || outMatches.Count > 0)
                {
                    if (inMatches.Count > outMatches.Count)
                    {
                        return CommentLineResult.BeginMultiLine;
                    }
                    else if (outMatches.Count > inMatches.Count)
                    {
                        return CommentLineResult.EndMultiLine;
                    }
                    else
                    {
                        return CommentLineResult.SingleLine;
                    }
                }
            }
            else if (mask == "*.bat" || mask == "*.cmd")
            {
                if (line.ToLower().StartsWith("rem"))
                {
                    return CommentLineResult.SingleLine;
                }
            }
            else if (mask == "*.c" || mask == "*.cpp" || mask == "*.cs" || mask == "*.sql")
            {
                if (vbSingleCommentBegin.Matches(line).Count > 0)
                {
                    return CommentLineResult.SingleLine;
                }
            }
            else if (mask == "*.c" || mask == "*.cpp" || mask == "*.cs" || mask == "*.sql")
            {
                if (cSingleCommentBegin.Matches(line).Count > 0)
                {
                    return CommentLineResult.SingleLine;
                }

                var inMatches = cMultiCommentBegin.Matches(line);
                var outMatches = cMultiCommentEnd.Matches(line);
                if (inMatches.Count > 0 || outMatches.Count > 0)
                {
                    if (inMatches.Count > outMatches.Count)
                    {
                        return CommentLineResult.BeginMultiLine;
                    }
                    else if (outMatches.Count > inMatches.Count)
                    {
                        return CommentLineResult.EndMultiLine;
                    }
                    else
                    {
                        return CommentLineResult.SingleLine;
                    }
                }
            }

            return CommentLineResult.None;
        }

        void ProcessFile(string mask, string fileName)
        {
            klocState.FilesCount++;

            if (klocState.Counts.ContainsKey(mask) == false)
            {
                klocState.Counts.Add(mask, 0);
            }

            int lineCount = klocState.Counts[mask];

            bool ignoreBlankLines = true;
            bool ignoreCommentLines = true;

            using (var reader = File.OpenText(fileName))
            {
                string line;

                int commentNestLevel = 0;

                do
                {
                    if (klocState.Worker.CancellationPending)
                    {
                        return;
                    }

                    if ((line = reader.ReadLine()) != null)
                    {
                        if (line.Trim().Length == 0) //Ignore blank lines.
                        {
                            klocState.BlankLineCount++;
                            if (ignoreBlankLines == true)
                            {
                                continue;
                            }
                        }

                        CommentLineResult commentResult = IsCommentLine(mask, line);

                        if (commentResult != CommentLineResult.None)
                        {
                            klocState.CommentsLineCount++;

                            if (commentResult == CommentLineResult.BeginMultiLine)
                            {
                                commentNestLevel++;
                            }
                            else if (commentResult == CommentLineResult.EndMultiLine)
                            {
                                commentNestLevel--;
                            }

                            if (ignoreCommentLines == true)
                            {
                                continue;
                            }
                        }

                        lineCount++;
                        klocState.TotalLineCount++;
                    }
                } while (line != null);
            }

            klocState.Counts[mask] = lineCount;

            klocState.Worker.ReportProgress(0, new KeyValuePair<string, int>(mask, lineCount));
        }

        int ListMaskIndex(string mask)
        {
            foreach (ListViewItem item in listViewProgress.Items)
            {
                if (item.Text == mask)
                {
                    return item.Index;
                }
            }

            return -1;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (typeof(Exception).IsAssignableFrom(e.UserState.GetType()))
            {
                Exception ex = (Exception)e.UserState;
                MessageBox.Show(ex.Message, "K-LOC Calculator", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                KeyValuePair<string, int> maskCount = (KeyValuePair<string, int>)e.UserState;

                int itemIndex = ListMaskIndex(maskCount.Key);
                if (itemIndex < 0)
                {
                    ListViewItem item = new ListViewItem(maskCount.Key);
                    item.SubItems.Add(maskCount.Value.ToString("N0"));
                    listViewProgress.Items.Add(item);
                    listViewProgress.Sorting = SortOrder.Ascending;
                }
                else
                {
                    listViewProgress.Items[itemIndex].SubItems[1].Text = maskCount.Value.ToString("N0");
                }

                string totalMask = "( Code Lines )";
                int totalItemIndex = ListMaskIndex(totalMask);
                if (totalItemIndex < 0)
                {
                    ListViewItem item = new ListViewItem(totalMask);
                    item.SubItems.Add(klocState.TotalLineCount.ToString("N0"));
                    listViewProgress.Items.Add(item);
                    listViewProgress.Sorting = SortOrder.Ascending;
                }
                else
                {
                    listViewProgress.Items[totalItemIndex].SubItems[1].Text = klocState.TotalLineCount.ToString("N0");
                }


                string blankMask = "( Blank Lines )";
                int blankItemIndex = ListMaskIndex(blankMask);
                if (blankItemIndex < 0)
                {
                    ListViewItem item = new ListViewItem(blankMask);
                    item.SubItems.Add(klocState.BlankLineCount.ToString("N0"));
                    listViewProgress.Items.Add(item);
                    listViewProgress.Sorting = SortOrder.Ascending;
                }
                else
                {
                    listViewProgress.Items[blankItemIndex].SubItems[1].Text = klocState.BlankLineCount.ToString("N0");
                }

                string commentsMask = "( Comments )";
                int commentsItemIndex = ListMaskIndex(commentsMask);
                if (commentsItemIndex < 0)
                {
                    ListViewItem item = new ListViewItem(commentsMask);
                    item.SubItems.Add(klocState.CommentsLineCount.ToString("N0"));
                    listViewProgress.Items.Add(item);
                    listViewProgress.Sorting = SortOrder.Ascending;
                }
                else
                {
                    listViewProgress.Items[commentsItemIndex].SubItems[1].Text = klocState.CommentsLineCount.ToString("N0");
                }

                string filesMask = "( Files )";
                int filesItemIndex = ListMaskIndex(filesMask);
                if (filesItemIndex < 0)
                {
                    ListViewItem item = new ListViewItem(filesMask);
                    item.SubItems.Add(klocState.FilesCount.ToString("N0"));
                    listViewProgress.Items.Add(item);
                    listViewProgress.Sorting = SortOrder.Ascending;
                }
                else
                {
                    listViewProgress.Items[filesItemIndex].SubItems[1].Text = klocState.FilesCount.ToString("N0");
                }
            }
        }

        #endregion

    }
}
