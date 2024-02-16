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
                            label1.Text = $"Toplam Boyut: {FormatSize(totalSize)}";
                            label2.Text = $"Sıkıştırılmış Boyut: {FormatSize(totalCompressedSize)}";
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
                        MessageBox.Show("Bir hata oluştu: " + ex.Message);
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

        }



        private List<string> GetTxtFilesList()
        {
            var txtFilesList = new List<string>();
            string orgDataPath = Path.Combine(Application.StartupPath, "Data\\OrgData");

            var txtFiles = Directory.GetFiles(orgDataPath, "*.txt");

            foreach (var filePath in txtFiles)
            {
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                txtFilesList.Add(fileName); // Dosya adlarını orijinal halleriyle sakla
            }

            return txtFilesList;
        }

        private Dictionary<string, List<string>> SearchFilesByBrandAndType(string brand, string type)
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


        private void Btn_Listele_Click(object sender, EventArgs e)
        {
            var brand = textBox1.Text.Trim();
            var type = textBox2.Text.Trim();

            int Klasör = 0;
            int Dosya = 0;

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

                label5.Text = $"{Klasör} Klasör,  {Dosya} Dosya bulundu.";
            }
            else
            {
                MessageBox.Show("Lütfen markabilgisi giriniz.");
            }
        }

        private void Btn_EdzExport_Click(object sender, EventArgs e)
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

                        // Hedef dosya yolu
                        string destinationFilePath = Path.Combine(selectedFolderPath, Path.GetFileName(firstLine));

                        // Dosyayı seçilen klasöre kopyala
                        File.Copy(firstLine, destinationFilePath, true); // true, varolan dosyanın üzerine yazılmasını sağlar

                        MessageBox.Show($"Dosya başarıyla indirildi:\n{destinationFilePath}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                // Dosya bulunamazsa hata mesajı göster
                MessageBox.Show($"{ExportData} dosyası bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
