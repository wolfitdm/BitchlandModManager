using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO.Compression;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using TinyJson;

namespace BitchlandCheatConsoleUpdaterGuiVersion
{
    public partial class Form1 : Form
    {
        private BackgroundWorker backgroundWorker;
        private string currentDownloadUrl;
        private string currentDownloadFile;
        private Dictionary<string, string> currentModJson;
        private string currentModJsonFileName;
        private List<Action> onWorkCompleted = new List<Action>();

        private string currentBepInExMod = "";
        private string currentIngameMod = "";
        private string currentBepInExModsBackup = "";
        private string currentIngameModsBackup = "";
        private int currentBepInExModsBackupIndex = 0;
        private int currentIngameModsBackupIndex = 0;

        private string[] currentBepInExModsBackups = null;
        private string[] currentIngameModsBackups = null;

        private Dictionary<string, string> currentBepInExModsHelp = new Dictionary<string, string>();
        private Dictionary<string, string> currentIngameModsHelp = new Dictionary<string, string>();

        public Form1()
        {
            try
            {
                initJsonDirectories();
                backgroundWorker = new BackgroundWorker();
                backgroundWorker.WorkerReportsProgress = true;
                backgroundWorker.DoWork += backgroundWorker_DoWork;
                backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
                backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
                InitializeComponent();

                initJsonFiles();

                this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
                this.comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;

                reinitMods();
                reinitBackups();

                this.comboBox1.SelectedIndexChanged += (s, e) =>
                {
                    object obj = this.comboBox1.SelectedItem;

                    if (obj != null && obj is string)
                    {
                        this.currentBepInExMod = (string)obj;
                    }
                };

                this.comboBox2.SelectedIndexChanged += (s, e) =>
                {
                    object obj = this.comboBox2.SelectedItem;

                    if (obj != null && obj is string)
                    {
                        this.currentIngameMod = (string)obj;
                    }
                };

                this.comboBox3.SelectedIndexChanged += (s, e) =>
                {
                    object obj = this.comboBox3.SelectedItem;

                    if (obj != null && obj is string)
                    {
                        this.currentBepInExModsBackup = (string)obj;
                        this.currentBepInExModsBackupIndex = this.comboBox3.SelectedIndex;
                    }
                };

                this.comboBox4.SelectedIndexChanged += (s, e) =>
                {
                    object obj = this.comboBox4.SelectedItem;

                    if (obj != null && obj is string)
                    {
                        this.currentIngameModsBackup = (string)obj;
                        this.currentIngameModsBackupIndex = this.comboBox4.SelectedIndex;
                    }
                };
            }
            catch (Exception ex)
            {
            }
        }

        private void reinitMods()
        {
            string[] bepInExMods = Directory.GetFiles("BepInExMods");
            string[] ingameMods = Directory.GetFiles("IngameMods");

            string[] bepInExModsHelp = new string[bepInExMods.Length];
            string[] ingameModsHelp = new string[ingameMods.Length];

            for (int i = 0; i < bepInExMods.Length; i++)
            {
                try
                {
                    string file = File.ReadAllText(bepInExMods[i]);
                    Dictionary<string, string> jsonFile = file.FromJson<Dictionary<string, string>>();

                    if (jsonFile == null)
                    {
                        jsonFile = new Dictionary<string, string>();
                        jsonFile.Add("help", "");
                    }

                    if (jsonFile.TryGetValue("help", out string help))
                    {
                        bepInExModsHelp[i] = help;
                    }
                    else
                    {
                        bepInExModsHelp[i] = "";
                    }
                }
                catch (Exception ex)
                {
                }
                bepInExMods[i] = Path.GetFileNameWithoutExtension(bepInExMods[i]);
            }

            for (int i = 0; i < ingameMods.Length; i++)
            {
                try
                {
                    string file = File.ReadAllText(ingameMods[i]);
                    Dictionary<string, string> jsonFile = file.FromJson<Dictionary<string, string>>();

                    if (jsonFile == null)
                    {
                        jsonFile = new Dictionary<string, string>();
                        jsonFile.Add("help", "");
                    }

                    if (jsonFile.TryGetValue("help", out string help))
                    {
                        ingameModsHelp[i] = help;
                    }
                    else
                    {
                        ingameModsHelp[i] = "";
                    }
                }
                catch (Exception ex)
                {
                }
                ingameMods[i] = Path.GetFileNameWithoutExtension(ingameMods[i]);
            }

            this.currentBepInExModsHelp.Clear();

            for (int i = 0; i < bepInExMods.Length; i++)
            {
                this.currentBepInExModsHelp.Add(bepInExMods[i], bepInExModsHelp[i]);
            }

            this.currentIngameModsHelp.Clear();

            for (int i = 0; i < ingameModsHelp.Length; i++)
            {
                this.currentIngameModsHelp.Add(ingameMods[i], ingameModsHelp[i]);
            }

            this.comboBox1.Items.Clear();
            this.comboBox2.Items.Clear();

            this.comboBox1.Items.AddRange(bepInExMods);
            this.comboBox2.Items.AddRange(ingameMods);
        }
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            UpdateLabel.Text = "Start Download... Please Wait...";
            var worker = sender as BackgroundWorker;
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(currentDownloadUrl, currentDownloadFile);
                    client.DownloadProgressChanged += (s, e) =>
                    {
                        worker.ReportProgress(e.ProgressPercentage);
                    };
                    client.DownloadFileCompleted += (s, e) =>
                    {
                        worker.ReportProgress(100);
                        // any other code to process the file
                    };
                }
            }
            catch (Exception ex)
            {
                worker.ReportProgress(100);
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            downloadProgressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            downloadProgressBar.Value = 100;

            if (e.Cancelled)
            {
                MessageBox.Show("The user has be cancelled the download!", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (e.Error != null)
            {
                MessageBox.Show($"Error: {e.Error.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < onWorkCompleted.Count; i++)
            {
                onWorkCompleted[i].Invoke();
            }
        }

        private void initDownloadProgressBar(string downloadUrl, string downloadFile, Action onWorkCompletedAction)
        {
            if (backgroundWorker.IsBusy)
            {
                MessageBox.Show("Download already in progress, please wait!", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.downloadProgressBar.Maximum = 100;
            this.downloadProgressBar.Step = 1;
            this.downloadProgressBar.Value = 5;
            currentDownloadUrl = downloadUrl;
            currentDownloadFile = downloadFile;
            backgroundWorker.RunWorkerAsync();
            onWorkCompleted.Clear();
            onWorkCompleted.Add(onWorkCompletedAction);
        }
        private void initVersionFile(string path, string mod, bool isIngameMod)
        {
            string modFile = Path.Combine(path, mod + ".json");

            if (!File.Exists(modFile))
            {
                return;
            }

            string file = File.ReadAllText(modFile);
            Dictionary<string, string> jsonFile = new Dictionary<string, string>();

            jsonFile = file.FromJson<Dictionary<string, string>>();

            string version = "v1.0.0";
            string downloadUrl = "";
            string downloadFile = "";

            if (jsonFile.TryGetValue("version", out string modVersion))
            {
                version = modVersion;
            }
            else
            {
                MessageBox.Show("version", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (jsonFile.TryGetValue("versionurl", out string versionurl))
            {
                downloadUrl = versionurl;
            }
            else
            {
                MessageBox.Show("versionurl", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (jsonFile.TryGetValue("versionfile", out string versionfile))
            {
                downloadFile = versionfile;
            }
            else
            {
                MessageBox.Show("downloadFile", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (File.Exists(downloadFile))
            {
                File.Delete(downloadFile);
            }

            string downloadUrlMod = "";
            string downloadFileMod = "";

            if (jsonFile.TryGetValue("downloadurl", out string downloadUrlReal))
            {
                downloadUrlMod = downloadUrlReal;
            }
            else
            {
                MessageBox.Show("downloadUrlMod", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (jsonFile.TryGetValue("downloadfile", out string downloadFileReal))
            {
                downloadFileMod = downloadFileReal;
            }
            else
            {
                MessageBox.Show("downloadFileMod", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            currentModJson = jsonFile;
            currentModJsonFileName = modFile;

            this.downloadProgressBar.Maximum = 100;
            this.downloadProgressBar.Step = 1;
            this.downloadProgressBar.Value = 5;

            string defaultText = this.UpdateLabel.Text;

            this.UpdateLabel.Text = "Download Version File from this mod... please wait";

            Action downloadModCompletedAction = () =>
            {
                string zipPath = downloadFileMod;
                string mainAppPath = Process.GetCurrentProcess().MainModule.FileName;
                string appDir = Path.GetDirectoryName(mainAppPath);
                string extractDirectory = appDir;
                string backupName = "BepInEx";
                string backupDir = Path.Combine(extractDirectory, backupName);

                string backupDirectoryBepInEx = "BepInExMods_Backups";
                string backupDirectoryIngame = "IngameMods_Backups";

                string bepInExModsDownloads = "BepInExMods_Downloads";
                string ingameModsDownloads = "IngameMods_Downloads";

                backupDirectoryBepInEx = Path.Combine(appDir, backupDirectoryBepInEx);
                backupDirectoryIngame = Path.Combine(appDir, backupDirectoryIngame);

                bepInExModsDownloads = Path.Combine(appDir, bepInExModsDownloads);
                ingameModsDownloads = Path.Combine(appDir, ingameModsDownloads);

                Directory.CreateDirectory(backupDirectoryBepInEx);
                Directory.CreateDirectory(backupDirectoryIngame);
                Directory.CreateDirectory(bepInExModsDownloads);
                Directory.CreateDirectory(ingameModsDownloads);

                string newZipPath = isIngameMod ? ingameModsDownloads : bepInExModsDownloads;
                newZipPath = Path.Combine(newZipPath, zipPath);

                if (File.Exists(newZipPath))
                {
                    File.Delete(newZipPath);
                }

                File.Move(zipPath, newZipPath);

                if (isIngameMod)
                {
                    backupDir = "Assets";
                    backupDir = Path.Combine(backupDir, "Mods");
                    extractDirectory = Path.Combine(extractDirectory, backupDir);
                    backupName = "Assets_Mods";
                }

                string bepinexdir = backupDir;
                string backupFile = "backup_" + backupName + "_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".zip";

                backupFile = Path.Combine(isIngameMod ? backupDirectoryIngame : backupDirectoryBepInEx, backupFile);

                Directory.CreateDirectory(bepinexdir);

                try
                {
                    ZipFile.CreateFromDirectory(bepinexdir, backupFile, CompressionLevel.SmallestSize, true);
                }
                catch
                {

                }

                reinitBackups();

                try
                {
                    ZipFile.ExtractToDirectory(newZipPath, extractDirectory, Encoding.UTF8, true);
                    UpdateLabel.Text = "Download complete. Update complete...";
                    try
                    {
                        File.WriteAllText(currentModJsonFileName, currentModJson.ToJson());
                    }
                    catch { }
                    MessageBox.Show("Download complete. Update complete...", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Update failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.UpdateLabel.Text = defaultText;
            };

            Action downloadVersionFileCompletedAction = () =>
            {
                string content = File.ReadAllText(downloadFile);
                Dictionary<string, string> contentFile = content.FromJson<Dictionary<string, string>>();
                if (contentFile.TryGetValue("version", out string modVersion))
                {
                    if (modVersion != version)
                    {
                        DialogResult dialogResult = MessageBox.Show($"Current Version {version}, New version {modVersion} available, download and install it? Yes/No/Cancel", "Complete", MessageBoxButtons.YesNoCancel);

                        if (dialogResult == DialogResult.Yes)
                        {
                            currentModJson["version"] = modVersion;
                            initDownloadProgressBar(downloadUrlMod, downloadFileMod, downloadModCompletedAction);
                        }
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show($"Current Version {version}, No new version available! Still download and install it ? Yes/No/Cancel", "Complete", MessageBoxButtons.YesNoCancel);

                        if (dialogResult == DialogResult.Yes)
                        {
                            initDownloadProgressBar(downloadUrlMod, downloadFileMod, downloadModCompletedAction);
                        }
                    }
                }
                this.UpdateLabel.Text = defaultText;
            };

            initDownloadProgressBar(downloadUrl, downloadFile, downloadVersionFileCompletedAction);
        }

        private void downloadFile(string downloadUrl, string downloadFile)
        {
            Action downloadModCompletedAction = () =>
            {
                string zipPath = downloadFile;
                string mainAppPath = Process.GetCurrentProcess().MainModule.FileName;
                string appDir = Path.GetDirectoryName(mainAppPath);
                string extractDirectory = appDir;

                try
                {
                    ZipFile.ExtractToDirectory(downloadFile, extractDirectory, Encoding.UTF8, true);
                }
                catch (Exception ex)
                {
                }
            };

            initDownloadProgressBar(downloadUrl, downloadFile, downloadModCompletedAction);
        }

        private Action buildAction(int i, int length, string[] downloadUrls, string[] downloadFiles, Action lastAction)
        {
            Action nextAction = null;

            if ((i + 1) < length)
            {
                nextAction = buildAction(i + 1, length, downloadUrls, downloadFiles, lastAction);
            }
            else
            {
                nextAction = () =>
                {
                    lastAction.Invoke();
                };
            }

            string downloadUrl = downloadUrls[i];
            string downloadFile = downloadFiles[i];

            Action action = () =>
            {
                initDownloadProgressBar(downloadUrl, downloadFile, nextAction);
            };

            return action;
        }

        private void download2Files(string[] downloadUrls, string[] downloadFiles)
        {
            if (downloadUrls == null || downloadFiles == null)
            {
                return;
            }

            if (downloadUrls.Length < 2 || downloadFiles.Length < 2)
            {
                return;
            }

            if (downloadUrls.Length != downloadFiles.Length)
            {
                return;
            }

            if (downloadFiles.Length % 2 != 0)
            {
                return;
            }

            Action downloadModReallyCompletedAction = () =>
            {
                string mainAppPath = Process.GetCurrentProcess().MainModule.FileName;
                string appDir = Path.GetDirectoryName(mainAppPath);
                string extractDirectory = appDir;

                initJsonDirectories();

                string[] bepInExMods = Directory.GetFiles("BepInExMods");
                string[] ingameMods = Directory.GetFiles("IngameMods");

                Dictionary<string, string> bepInExFiles = new Dictionary<string, string>();
                Dictionary<string, string> ingameModFiles = new Dictionary<string, string>();

                for (int i = 0; i < bepInExMods.Length; i++)
                {
                    try
                    {
                        string file = File.ReadAllText(bepInExMods[i]);
                        Dictionary<string, string> jsonFile = file.FromJson<Dictionary<string, string>>();
                        if (jsonFile == null)
                        {
                            continue;
                        }

                        if (jsonFile.TryGetValue("version", out string version))
                        {
                            bepInExFiles.Add(Path.GetFileNameWithoutExtension(bepInExMods[i]), version);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }

                for (int i = 0; i < ingameMods.Length; i++)
                {
                    try
                    {
                        string file = File.ReadAllText(ingameMods[i]);
                        Dictionary<string, string> jsonFile = file.FromJson<Dictionary<string, string>>();
                        if (jsonFile == null)
                        {
                            continue;
                        }

                        if (jsonFile.TryGetValue("version", out string version))
                        {
                            ingameModFiles.Add(Path.GetFileNameWithoutExtension(ingameMods[i]), version);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }

                for (int i = 0; i < downloadFiles.Length; i++)
                {
                    try
                    {
                        ZipFile.ExtractToDirectory(downloadFiles[i], extractDirectory, Encoding.UTF8, true);
                    }
                    catch (Exception ex)
                    {
                    }
                }

                string[] bepInExMods2 = Directory.GetFiles("BepInExMods");
                string[] ingameMods2 = Directory.GetFiles("IngameMods");

                for (int i = 0; i < bepInExMods2.Length; i++)
                {
                    try
                    {
                        string file = File.ReadAllText(bepInExMods2[i]);
                        Dictionary<string, string> jsonFile = file.FromJson<Dictionary<string, string>>();

                        if (jsonFile == null)
                        {
                            continue;
                        }

                        string key = Path.GetFileNameWithoutExtension(bepInExMods2[i]);

                        if (bepInExFiles.ContainsKey(key))
                        {
                            jsonFile["version"] = bepInExFiles[key];
                            File.WriteAllText(bepInExMods2[i], jsonFile.ToJson());
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }

                for (int i = 0; i < ingameMods2.Length; i++)
                {
                    try
                    {
                        string file = File.ReadAllText(ingameMods2[i]);
                        Dictionary<string, string> jsonFile = file.FromJson<Dictionary<string, string>>();

                        if (jsonFile == null)
                        {
                            continue;
                        }

                        string key = Path.GetFileNameWithoutExtension(ingameMods2[i]);

                        if (ingameModFiles.ContainsKey(key))
                        {
                            jsonFile["version"] = ingameModFiles[key];
                            File.WriteAllText(ingameMods2[i], jsonFile.ToJson());
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }

                reinitMods();

                for (int i = 0; i < downloadFiles.Length; i++)
                {
                    if (File.Exists(downloadFiles[i]))
                    {
                        File.Delete(downloadFiles[i]);
                    }
                }

                UpdateLabel.Text = "Download complete. Update complete...";

                downloadProgressBar.Value = 0;

                UpdateLabel.Text = "";
            };

            if (downloadFiles.Length == 2)
            {
                Action downloadModCompletedAction = () =>
                {
                    initDownloadProgressBar(downloadUrls[1], downloadFiles[1], downloadModReallyCompletedAction);
                };

                initDownloadProgressBar(downloadUrls[0], downloadFiles[0], downloadModCompletedAction);
            }
            else if (downloadFiles.Length > 2)
            {

                int length = downloadFiles.Length;

                Action firstAction = buildAction(0, length, downloadUrls, downloadFiles, downloadModReallyCompletedAction);

                firstAction.Invoke();
            }

        }

        private void initJsonDirectories()
        {
            string mainAppPath = Process.GetCurrentProcess().MainModule.FileName;
            string appDir = Path.GetDirectoryName(mainAppPath);

            string ingameMods = Path.Combine(appDir, "IngameMods");
            string bepinexMods = Path.Combine(appDir, "BepInExMods");

            Directory.CreateDirectory(ingameMods);
            Directory.CreateDirectory(bepinexMods);
        }

        private void initJsonFiles()
        {
            initModSource("getmoremods", "https://github.com/wolfitdm/BitchlandModManager/releases/download/v1.0.0/BepInExMods.zip", "https://github.com/wolfitdm/BitchlandModManager/releases/download/v1.0.0/IngameMods.zip", "BepInExMods.zip", "IngameMods.zip");
            initModSources();
        }

        private void initModSources()
        {
            try
            {
                string mainAppPath = Process.GetCurrentProcess().MainModule.FileName;
                string appDir = Path.GetDirectoryName(mainAppPath);

                string ingameMods = Path.Combine(appDir, "IngameMods");
                string bepinexMods = Path.Combine(appDir, "BepInExMods");

                Directory.CreateDirectory(ingameMods);
                Directory.CreateDirectory(bepinexMods);

                Dictionary<string, string> getmoremodss = null;

                List<string> downloadUrls = new List<string>();
                List<string> downloadFiles = new List<string>();

                string modsSources = Path.Combine(appDir, "ModsSources");

                Directory.CreateDirectory(modsSources);

                string[] files = Directory.GetFiles(modsSources);

                if (files == null || files.Length == 0)
                {
                    return;
                }

                for (int i = 0; i < files.Length; i++)
                {
                    files[i] = Path.GetFileNameWithoutExtension(files[i]);

                    getmoremodss = getModSource(files[i]);

                    if (getmoremodss == null)
                    {
                        continue;
                    }

                    downloadUrls.Add(getmoremodss["BepInExMods"]);
                    downloadFiles.Add(getmoremodss["BepInExModsFileName"]);

                    downloadUrls.Add(getmoremodss["IngameMods"]);
                    downloadFiles.Add(getmoremodss["IngameModsFileName"]);
                }

                download2Files(downloadUrls.ToArray(), downloadFiles.ToArray());
            }
            catch (Exception ex)
            {
            }
        }
        public bool initModSource(string name, string bepInExModsDownloadLink, string ingameModsDownloadLink, string bepInExModsFileName, string ingameModsFileName)
        {
            try
            {
                string mainAppPath = Process.GetCurrentProcess().MainModule.FileName;
                string appDir = Path.GetDirectoryName(mainAppPath);
                string modsSources = Path.Combine(appDir, "ModsSources");

                Directory.CreateDirectory(modsSources);

                string modSource = Path.Combine(modsSources, name + ".json");

                Dictionary<string, string> getmoremodss = new Dictionary<string, string>();

                getmoremodss.Add("IngameMods", ingameModsDownloadLink);
                getmoremodss.Add("BepInExMods", bepInExModsDownloadLink);
                getmoremodss.Add("IngameModsFileName", ingameModsFileName);
                getmoremodss.Add("BepInExModsFileName", bepInExModsFileName);

                File.WriteAllText(modSource, getmoremodss.ToJson());

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Dictionary<string, string> getModSource(string name)
        {
            try
            {
                string mainAppPath = Process.GetCurrentProcess().MainModule.FileName;
                string appDir = Path.GetDirectoryName(mainAppPath);
                string modsSources = Path.Combine(appDir, "ModsSources");

                Directory.CreateDirectory(modsSources);

                string modSource = Path.Combine(modsSources, name + ".json");

                Dictionary<string, string> getmoremodss = new Dictionary<string, string>();

                if (!File.Exists(modSource))
                {
                    return null;
                }

                string allText = File.ReadAllText(modSource);

                Dictionary<string, string> jsonFile = allText.FromJson<Dictionary<string, string>>();

                if (jsonFile == null)
                {
                    return null;
                }

                if (jsonFile.TryGetValue("IngameMods", out string ingameModsUrl))
                {
                    if (ingameModsUrl == null || ingameModsUrl == string.Empty)
                        return null;
                    getmoremodss["IngameMods"] = ingameModsUrl;
                }
                else
                {
                    return null;
                }

                if (jsonFile.TryGetValue("BepInExMods", out string bepInExModsUrl))
                {
                    if (bepInExModsUrl == null || bepInExModsUrl == string.Empty)
                        return null;
                    getmoremodss["BepInExMods"] = bepInExModsUrl;
                }
                else
                {
                    return null;
                }

                if (jsonFile.TryGetValue("IngameModsFileName", out string ingameModsFileName))
                {
                    if (ingameModsFileName == null || ingameModsFileName == string.Empty)
                        return null;
                    getmoremodss["IngameModsFileName"] = ingameModsFileName;
                }
                else
                {
                    return null;
                }

                if (jsonFile.TryGetValue("BepInExModsFileName", out string bepInExModsFileName))
                {
                    if (bepInExModsFileName == null || bepInExModsFileName == string.Empty)
                        return null;
                    getmoremodss["BepInExModsFileName"] = bepInExModsFileName;
                }
                else
                {
                    return null;
                }


                return getmoremodss;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentBepInExMod == null || currentBepInExMod.Length == 0)
                {
                    return;
                }

                if (backgroundWorker.IsBusy)
                {
                    MessageBox.Show("Download already in progress, please wait!", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                initVersionFile("BepInExMods", currentBepInExMod, false);
            }
            catch (Exception ex)
            {
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentIngameMod == null || currentIngameMod.Length == 0)
                {
                    return;
                }

                if (backgroundWorker.IsBusy)
                {
                    MessageBox.Show("Download already in progress, please wait!", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                initVersionFile("IngameMods", currentIngameMod, true);
            }
            catch (Exception ex)
            {
            }
        }

        private void getmoremods_Click(object sender, EventArgs e)
        {
            try
            {
                if (backgroundWorker.IsBusy)
                {
                    MessageBox.Show("Download already in progress, please wait!", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                initJsonFiles();
            }
            catch (Exception ex)
            {
            }
        }
        private string createBackup(bool isIngameMod)
        {
            string mainAppPath = Process.GetCurrentProcess().MainModule.FileName;
            string appDir = Path.GetDirectoryName(mainAppPath);
            string extractDirectory = appDir;
            string backupName = "BepInEx";
            string backupDir = Path.Combine(extractDirectory, backupName);

            string backupDirectoryBepInExDefault = "BepInExMods_Backups";
            string backupDirectoryIngameDefault = "IngameMods_Backups";

            string bepInExModsDownloads = "BepInExMods_Downloads";
            string ingameModsDownloads = "IngameMods_Downloads";

            string backupDirectoryBepInEx = Path.Combine(appDir, backupDirectoryBepInExDefault);
            string backupDirectoryIngame = Path.Combine(appDir, backupDirectoryIngameDefault);

            bepInExModsDownloads = Path.Combine(appDir, bepInExModsDownloads);
            ingameModsDownloads = Path.Combine(appDir, ingameModsDownloads);

            Directory.CreateDirectory(backupDirectoryBepInEx);
            Directory.CreateDirectory(backupDirectoryIngame);
            Directory.CreateDirectory(bepInExModsDownloads);
            Directory.CreateDirectory(ingameModsDownloads);

            if (isIngameMod)
            {
                backupDir = "Assets";
                backupDir = Path.Combine(backupDir, "Mods");
                extractDirectory = Path.Combine(extractDirectory, backupDir);
                backupName = "Assets_Mods";
            }

            string bepinexdir = backupDir;
            string backupFileDefault = "backup_" + backupName + "_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".zip";

            string backupFile = Path.Combine(isIngameMod ? backupDirectoryIngame : backupDirectoryBepInEx, backupFileDefault);

            Directory.CreateDirectory(bepinexdir);

            try
            {
                ZipFile.CreateFromDirectory(bepinexdir, backupFile, CompressionLevel.SmallestSize, true);
            }
            catch
            {

            }

            reinitBackups();

            string installedBackupDir = isIngameMod ? backupDirectoryIngameDefault : backupDirectoryBepInExDefault;

            return Path.Combine(installedBackupDir, backupFileDefault);
        }

        private void reinitBackups()
        {
            try
            {
                string mainAppPath = Process.GetCurrentProcess().MainModule.FileName;
                string appDir = Path.GetDirectoryName(mainAppPath);

                string ingameMods = Path.Combine(appDir, "IngameMods_Backups");
                string bepinexMods = Path.Combine(appDir, "BepInExMods_Backups");

                Directory.CreateDirectory(ingameMods);
                Directory.CreateDirectory(bepinexMods);

                string[] bepInExModsBackups = Directory.GetFiles("BepInExMods_Backups");
                string[] ingameModsBackups = Directory.GetFiles("IngameMods_Backups");

                currentBepInExModsBackups = new string[bepInExModsBackups.Length];
                currentIngameModsBackups = new string[ingameModsBackups.Length];

                for (int i = 0; i < bepInExModsBackups.Length; i++)
                {
                    currentBepInExModsBackups[i] = bepInExModsBackups[i];
                    bepInExModsBackups[i] = Path.GetFileNameWithoutExtension(bepInExModsBackups[i]);
                    string[] splits = bepInExModsBackups[i].Split("_");

                    string newString = "";

                    string day = "";
                    string month = "";
                    string year = "";
                    string hour = "";
                    string minute = "";
                    string seconds = "";

                    int length = splits.Length;
                    for (int j = 0; j < splits.Length; j++)
                    {
                        switch (splits[j])
                        {
                            case "Assets":
                            case "Mods":
                            case "backup":
                            case "BepInEx":
                                {
                                    continue;
                                }
                                break;

                            default:
                                {
                                    if (day == string.Empty)
                                    {
                                        day = splits[j];
                                    }
                                    else if (month == string.Empty)
                                    {
                                        month = splits[j];
                                    }
                                    else if (year == string.Empty)
                                    {
                                        year = splits[j];
                                    }
                                    else if (hour == string.Empty)
                                    {
                                        hour = splits[j];
                                    }
                                    else if (minute == string.Empty)
                                    {
                                        minute = splits[j];
                                    }
                                    else if (seconds == string.Empty)
                                    {
                                        seconds = splits[j];
                                    }
                                }
                                break;
                        }
                    }

                    newString = hour + ":" + minute + ":" + seconds + " - " + day + "." + month + "." + year;

                    bepInExModsBackups[i] = newString;
                }

                for (int i = 0; i < ingameModsBackups.Length; i++)
                {
                    currentIngameModsBackups[i] = ingameModsBackups[i];
                    ingameModsBackups[i] = Path.GetFileNameWithoutExtension(ingameModsBackups[i]);
                    string[] splits = ingameModsBackups[i].Split("_");

                    string newString = "";

                    int length = splits.Length;
                    string day = "";
                    string month = "";
                    string year = "";
                    string hour = "";
                    string minute = "";
                    string seconds = "";
                    for (int j = 0; j < splits.Length; j++)
                    {
                        switch (splits[j])
                        {
                            case "Assets":
                            case "Mods":
                            case "backup":
                            case "BepInEx":
                                {
                                    continue;
                                }
                                break;

                            default:
                                {
                                    if (day == string.Empty)
                                    {
                                        day = splits[j];
                                    }
                                    else if (month == string.Empty)
                                    {
                                        month = splits[j];
                                    }
                                    else if (year == string.Empty)
                                    {
                                        year = splits[j];
                                    }
                                    else if (hour == string.Empty)
                                    {
                                        hour = splits[j];
                                    }
                                    else if (minute == string.Empty)
                                    {
                                        minute = splits[j];
                                    }
                                    else if (seconds == string.Empty)
                                    {
                                        seconds = splits[j];
                                    }
                                }
                                break;
                        }
                    }

                    newString = hour + ":" + minute + ":" + seconds + " - " + day + "." + month + "." + year;

                    ingameModsBackups[i] = newString;
                }


                this.comboBox3.Items.Clear();
                this.comboBox4.Items.Clear();

                this.comboBox3.Items.AddRange(bepInExModsBackups);
                this.comboBox4.Items.AddRange(ingameModsBackups);
            }
            catch (Exception ex)
            {
            }
        }

        private void initBackupFile(string backupFileName, int index, bool ingameMod)
        {
            string backupDirectory = ingameMod ? "IngameMods_Backups" : "BepInExMods_Backups";

            downloadProgressBar.Value = 0;

            try
            {
                string mainAppPath = Process.GetCurrentProcess().MainModule.FileName;
                string appDir = Path.GetDirectoryName(mainAppPath);

                string backupModsDir = Path.Combine(appDir, backupDirectory);

                Directory.CreateDirectory(backupModsDir);

                string modPath = ingameMod ? currentIngameModsBackups[index] : currentBepInExModsBackups[index];

                string extractDirectory = ingameMod ? Path.Combine(appDir, "Assets") : appDir;

                string modDirectory = ingameMod ? Path.Combine(extractDirectory, "Mods") : Path.Combine(extractDirectory, "BepInEx");

                DialogResult dialogResult = MessageBox.Show("Delete the old mod directory ? Yes/No/Cancel", "Delete the old mod directory?", MessageBoxButtons.YesNoCancel);

                if (dialogResult == DialogResult.Yes)
                {
                    if (Directory.Exists(modDirectory))
                    {
                        Directory.Delete(modDirectory, true);
                    }

                    Directory.CreateDirectory(modDirectory);
                }
                else if (dialogResult == DialogResult.No)
                {
                    Directory.CreateDirectory(modDirectory);
                }

                ZipFile.ExtractToDirectory(modPath, extractDirectory, true);

                UpdateLabel.Text = "Backup installed complete...";

                downloadProgressBar.Value = 100;

                MessageBox.Show("Backup installed complete...", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentBepInExModsBackup == null || currentBepInExModsBackup.Length == 0)
                {
                    return;
                }

                if (backgroundWorker.IsBusy)
                {
                    MessageBox.Show("Download already in progress, please wait!", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                initBackupFile(currentBepInExModsBackup, currentBepInExModsBackupIndex, false);
            } catch (Exception ex) 
            {
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentIngameModsBackup == null || currentIngameModsBackup.Length == 0)
                {
                    return;
                }

                if (backgroundWorker.IsBusy)
                {
                    MessageBox.Show("Download already in progress, please wait!", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                initBackupFile(currentIngameModsBackup, currentIngameModsBackupIndex, true);
            } catch(Exception ex) 
            {
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (backgroundWorker.IsBusy)
                {
                    MessageBox.Show("Download already in progress, please wait!", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                downloadProgressBar.Value = 0;
                string backupCreated = createBackup(false);
                downloadProgressBar.Value = 100;
                MessageBox.Show($"Backup created {backupCreated} complete...", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception ex)
            {
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (backgroundWorker.IsBusy)
                {
                    MessageBox.Show("Download already in progress, please wait!", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                downloadProgressBar.Value = 0;
                string backupCreated = createBackup(true);
                downloadProgressBar.Value = 100;
                MessageBox.Show($"Backup created {backupCreated} complete...", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
            }
        }
        public static void OpenUrl(string url)
        {
            try
            {
                // .NET Core / .NET 5+ safe way
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch
            {
                // Fallback for older .NET or restricted environments
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url.Replace("&", "^&")}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    return;
                }
            }
        }
        private void helpBepInExMods_Click(object sender, EventArgs e)
        {
            try
            {
                if (backgroundWorker.IsBusy)
                {
                    MessageBox.Show("Download already in progress, please wait!", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (currentBepInExMod == null || currentBepInExMod.Length == 0)
                {
                    return;
                }

                if (!currentBepInExModsHelp.ContainsKey(currentBepInExMod))
                {
                    return;
                }

                string url = "";

                try
                {
                    url = currentBepInExModsHelp[currentBepInExMod];
                }
                catch { }

                if (url == "")
                {
                    return;
                }

                OpenUrl(url);
            } catch (Exception ex) 
            {
            }
        }

        private void helpIngameMods_Click(object sender, EventArgs e)
        {
            try
            {
                if (backgroundWorker.IsBusy)
                {
                    MessageBox.Show("Download already in progress, please wait!", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (currentIngameMod == null || currentIngameMod.Length == 0)
                {
                    return;
                }

                if (!currentIngameModsHelp.ContainsKey(currentIngameMod))
                {
                    return;
                }

                string url = "";

                try
                {
                    url = currentIngameModsHelp[currentIngameMod];
                }
                catch { }

                if (url == "")
                {
                    return;
                }

                OpenUrl(url);
            } catch (Exception ex)
            {
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
