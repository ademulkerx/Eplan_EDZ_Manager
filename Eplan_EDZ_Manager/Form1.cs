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
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Resources;
using System.Reflection;
using System.Security.Policy;

namespace Eplan_EDZ_Manager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Edz İmport

        private long totalSize = 0;
        private long totalCompressedSize = 0;

        private async void Btn_SourceFile_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = folderBrowserDialog.SelectedPath;
                    List<string> edzFiles = FindEdzFiles(folderPath);
                    treeView1.Nodes.Clear();

                    int progressBarSayac = 0;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = edzFiles.Count;
                    progressBar1.Value = 0;


                    totalSize = 0;
                    totalCompressedSize = 0;


                    foreach (var edzFile in edzFiles)
                    {
                        // Asenkron işlemi başlat
                        await ListEdzFileContentsAsync(@"C:\Program Files\7-Zip\7z.exe", edzFile);

                        this.BeginInvoke(new Action(() =>
                        {
                            progressBar1.Value = ++progressBarSayac;
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, LanguageConvert("msg2_DosyaAktarim_Baslik"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        string SSize = " 0 Byte";
        string TSize = " 0 Byte";
        private async Task ListEdzFileContentsAsync(string sevenZipPath, string edzFilePath)
        {
            // Asenkron işlemi bir arka plan iş parçacığında başlat
            await Task.Run(() =>
            {
                try
                {
                    string arguments = $"l \"{edzFilePath}\"";



                    ProcessStartInfo procStartInfo = new ProcessStartInfo
                    {
                        FileName = sevenZipPath,
                        Arguments = arguments,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using (Process proc = new Process { StartInfo = procStartInfo })
                    {
                        proc.Start();

                        string output = proc.StandardOutput.ReadToEnd();
                        string error = proc.StandardError.ReadToEnd();

                        proc.WaitForExit();

                        if (!string.IsNullOrEmpty(error))
                        {
                            this.Invoke(new Action(() =>
                            {
                                MessageBox.Show("Hata çıktısı: " + error);
                            }));
                            return;
                        }

                        string[] lines = output.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                        bool dataFound = false;

                        List<string> linesToSave = new List<string>();
                        string searchPattern = "items\\macro";
                        linesToSave.Add(edzFilePath);

                        foreach (string line in lines)
                        {
                            if (line.Contains(searchPattern))
                            {

                                int index = line.IndexOf(searchPattern);
                                string cleanLine = (index < 0) ? line : line.Substring(index);
                                linesToSave.Add(cleanLine);
                                dataFound = true;

                            }
                        }

                        var parts = lines[lines.Length - 2].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        totalSize += long.Parse(parts[2]);

                        // Dosyanın boyutunu al
                        // Toplam boyuta ekle
                        totalCompressedSize += (long)new FileInfo(edzFilePath).Length;

                        // UI thread'ine geri dön ve label'ları güncelle
                        this.Invoke(new Action(() =>
                        {
                            TSize = $"{FormatSize(totalSize)}";
                            SSize = $"{FormatSize(totalCompressedSize)}";
                            Lbl_ToplamBoyut.Text = $"{LanguageConvert("Lbl_ToplamBoyut")} {TSize}";
                            Lbl_SBoyut.Text = $"{LanguageConvert("Lbl_SBoyut")} {SSize}";
                        }));


                        string orgDataPath = Path.Combine(Application.StartupPath, "Data\\OrgData");
                        Directory.CreateDirectory(orgDataPath); // Klasör yoksa oluştur
                        string txtFileName = Path.GetFileNameWithoutExtension(edzFilePath) + ".txt";
                        string txtFilePath = Path.Combine(orgDataPath, txtFileName);

                        // Eğer items\macro içeren veri yoksa, "Data bulunamadı" yaz
                        if (!dataFound)
                        {
                            linesToSave.Add("Data bulunamadı");
                        }

                        // Çıktıyı .txt dosyasına yaz
                        File.WriteAllLines(txtFilePath, linesToSave);

                        // UI thread'ine geri dön ve TreeView'ı güncelle
                        this.Invoke(new Action(() =>
                        {
                            TreeNode rootNode = new TreeNode(Path.GetFileName(edzFilePath));
                            treeView1.Nodes.Add(rootNode);

                            if (dataFound)
                            {
                                foreach (string line in linesToSave)
                                {
                                    rootNode.Nodes.Add(new TreeNode(line));
                                }
                            }
                            else
                            {
                                rootNode.Nodes.Add(new TreeNode("Data bulunamadı"));
                            }

                            // TreeView'ı kapalı hale getir
                            rootNode.Collapse();
                        }));
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show(ex.Message, LanguageConvert("msg2_DosyaAktarim_Baslik"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }));
                }
            });
        }


        private string FormatSize(long bytes)
        {
            var sizes = new[] { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }


        private List<string> FindEdzFiles(string folderPath)
        {
            return Directory.GetFiles(folderPath, "*.edz", SearchOption.AllDirectories).ToList();
        }

        #endregion


        private void Form1_Load(object sender, EventArgs e)
        {
            string userLanguage = Properties.Settings.Default.Language;
            //string userLanguagde = TR.Btn_EdzExport;

            comboBoxLanguages.SelectedItem = userLanguage;
            UpdateUI(userLanguage);
        }

        private Dictionary<string, List<string>> SearchFilesByBrandAndType(string brand, string type)
        {
            try
            {
                var results = new Dictionary<string, List<string>>();
                string orgDataPath = Path.Combine(Application.StartupPath, "Data\\OrgData");
                var txtFiles = Directory.GetFiles(orgDataPath, "*.txt");

                foreach (var filePath in txtFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    if (fileName.ToUpper().Contains(brand.ToUpper()))
                    {
                        var lines = File.ReadAllLines(filePath);

                        // İlk satırı yoksayarak geri kalan satırları işle
                        var matchedLines = new List<string>();
                        for (int i = 1; i < lines.Length; i++) // i=1 ile başlayarak ilk satırı atlıyoruz
                        {
                            if (lines[i].ToUpper().Contains(type.ToUpper()))
                            {
                                matchedLines.Add(lines[i]);
                            }
                        }

                        if (matchedLines.Count > 0)
                        {
                            results.Add(fileName, matchedLines);
                        }
                    }
                }

                return results;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, LanguageConvert("msg2_DosyaAktarim_Baslik"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return null;
        }

        int Klasör = 0;
        int Dosya = 0;
        private void Btn_Listele_Click(object sender, EventArgs e)
        {
            try
            {
                var brand = textBox1.Text.Trim();
                var type = textBox2.Text.Trim();

                

                if (!string.IsNullOrWhiteSpace(brand))
                {
                    var results = SearchFilesByBrandAndType(brand, type);
                    treeView2.Nodes.Clear(); // TreeView temizlenir

                    foreach (var result in results)
                    {
                        // Her dosya adı için bir ana düğüm oluştur
                        TreeNode fileNode = new TreeNode(result.Key);
                        treeView2.Nodes.Add(fileNode);
                        Klasör++;

                        // Eşleşen her satır için çocuk düğümler oluştur
                        foreach (var line in result.Value)
                        {
                            Dosya++;
                            fileNode.Nodes.Add(new TreeNode(line));
                        }
                    }

                    Lbl_Find.Text = $"{Klasör} {LanguageConvert("Klasor")},  {Dosya} {LanguageConvert("DosyaBulundu")}";
                }
                else
                {//msgMarka
                    MessageBox.Show($"{LanguageConvert("msgMarka")}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, LanguageConvert("msg2_DosyaAktarim_Baslik"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private async void Btn_EdzExport_Click(object sender, EventArgs e)
        {
            try
            {
                // Dosyanın var olup olmadığını kontrol et
                string firstLine = File.ReadLines(Path.Combine(Application.StartupPath, "Data\\OrgData", ExportData)).FirstOrDefault();

                if (File.Exists(firstLine))
                {
                    // Klasör seçim diyalogunu aç
                    using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                    {
                        if (folderDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Seçilen klasörün yolunu al
                            string selectedFolderPath = folderDialog.SelectedPath;

                            // Hedef dosya yolu (hedef klasör yolu ile dosyanın adını birleştirerek)
                            string destinationFilePath = Path.Combine(selectedFolderPath, Path.GetFileName(firstLine));

                            // İlerleme göstergesi için bir Progress<T> nesnesi oluştur
                            var progressIndicator = new Progress<int>(percent =>
                            {
                                // Güvenli bir şekilde UI thread'inde ProgressBar'ı güncelle
                                progressBar2.Invoke(new Action(() =>
                                {
                                    progressBar2.Value = percent;
                                }));
                            });
                            pictureBox1.Visible = true;
                            Lbl_DownloadInfo.Visible = true;
                            await Task.Delay(100);


                            // Asenkron dosya kopyalama işlemini başlat
                            await CopyFileAsync(firstLine, destinationFilePath, progressIndicator);
                            await Task.Delay(1000);
                            pictureBox1.Visible = false;
                            Lbl_DownloadInfo.Visible = false;
                            //msg1DosyaAktarim
                            MessageBox.Show($"{LanguageConvert("msg1_DosyaAktarim")}\n>> {destinationFilePath}", $"{LanguageConvert("msg1_DosyaAktarim_Baslik")}\n>> {destinationFilePath}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    //msg2DosyaAktarim
                    // Dosya bulunamazsa hata mesajı göster
                    MessageBox.Show($"{ExportData} {LanguageConvert("msg2_DosyaAktarim")}", $"{LanguageConvert("msg2_DosyaAktarim_Baslik")}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, LanguageConvert("msg2_DosyaAktarim_Baslik"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        /// <summary>
        /// Kaynak dosyadan hedef dosyaya asenkron bir şekilde veri kopyalar. İşlemin ilerlemesini
        /// bir IProgress&lt;int&gt; arayüzü üzerinden raporlar. Kopyalama işlemi, büyük dosyalar için optimize edilmiş
        /// 1MB'lık bir buffer kullanır ve her bir parçanın kopyalanmasının ardından ilerlemeyi günceller.
        /// </summary>
        /// <param name="kaynakYolu">Okunacak kaynak dosyanın tam yolu.</param>
        /// <param name="hedefDosyaYolu">Verilerin yazılacağı hedef dosyanın tam yolu.</param>
        /// <param name="progress">Kopyalama işleminin ilerlemesini yüzde olarak raporlayacak IProgress&lt;int&gt; nesnesi.</param>
        /// <returns>Task, asenkron işlemin tamamlanmasını temsil eder.</returns>
        private async Task CopyFileAsync(string kaynakYolu, string hedefDosyaYolu, IProgress<int> progress)
        {
            try
            {
                const int bufferSize = 1024 * 1024; // Örneğin, 1MB tampon boyutu.
                byte[] buffer = new byte[bufferSize];
                int bytesRead;
                long totalBytesRead = 0;

                using (var sourceStream = new FileStream(kaynakYolu, FileMode.Open, FileAccess.Read))
                {
                    long totalBytes = sourceStream.Length;
                    using (var destinationStream = new FileStream(hedefDosyaYolu, FileMode.Create, FileAccess.Write))
                    {
                        while ((bytesRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await destinationStream.WriteAsync(buffer, 0, bytesRead);
                            totalBytesRead += bytesRead;
                            int percentComplete = (int)(totalBytesRead * 100 / totalBytes);
                            progress.Report(percentComplete);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, LanguageConvert("msg2_DosyaAktarim_Baslik"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }







        string ExportData = "";
        private void treeView2_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Tıklanan düğümü al
            TreeNode clickedNode = e.Node;

            // Ana düğümü bul
            while (clickedNode.Parent != null)
            {
                clickedNode = clickedNode.Parent;
            }

            ExportData = $"{clickedNode.Text}.txt";
        }


        // 'Language' klasörü ve 'EN.resx' dosyası için ResourceManager nesnesini oluşturun.
        // 'YourNamespace' kısmını, kaynak dosyalarınızın gerçek namespace'i ile değiştirin.
        ResourceManager resourceManager;

        // Belirtilen dil kodu için yeni bir kültür oluşturun.
        CultureInfo cultureInfo;

        private void comboBoxLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedLanguage = comboBoxLanguages.SelectedItem.ToString();
                // Ayarı kaydet
                Properties.Settings.Default.Language = selectedLanguage;
                Properties.Settings.Default.Save();


                resourceManager = new ResourceManager($"Eplan_EDZ_Manager.Language.{selectedLanguage}", typeof(Form1).Assembly);

                cultureInfo = new CultureInfo(selectedLanguage);
                // Dil değişikliğini uygula
                ChangeLanguage(selectedLanguage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, LanguageConvert("msg2_DosyaAktarim_Baslik"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }



        private void ChangeLanguage(string lang)
        {
            CultureInfo culture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // Arayüzdeki metinleri güncelle
            UpdateUI(lang);
        }


        public string LanguageConvert(string text)
        {
            return resourceManager.GetString(text, cultureInfo);
        }

        private void UpdateUI(string lang)
        {
            

            // Kontrolleri güncelleyin.
            Btn_EdzExport.Text = LanguageConvert("Btn_EdzExport");
            Btn_SourceFile.Text = LanguageConvert("Btn_SourceFile");
            gBox_export.Text = LanguageConvert("gBox_export");
            gBox_import.Text = LanguageConvert("gBox_import");
            Language.Text = LanguageConvert("Language");
            Lbl_Find.Text = $"{Klasör} {LanguageConvert("Klasor")},  {Dosya} {LanguageConvert("DosyaBulundu")}"; 
            Lbl_Marka.Text = LanguageConvert("Lbl_Marka");
            Lbl_SBoyut.Text = LanguageConvert("Lbl_SBoyut") + SSize;
            Lbl_ToplamBoyut.Text = LanguageConvert("Lbl_ToplamBoyut") + TSize;
            Lbl_Type.Text = LanguageConvert("Lbl_Type");
            Btn_Listele.Text = LanguageConvert("Btn_Listele");
            Lbl_DownloadInfo.Text = LanguageConvert("Lbl_DownloadInfo");

            // ... ve diğer kontroller.
        }



    }
}
