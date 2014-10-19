using PiggySyncWin.Domain;
using PiggySyncWin.Domain.Concrete;
using PiggySyncWin.WinUI.Infrastructure;
using PiggySyncWin.WinUI.Models;
using PiggySyncWin.WinUI.Models.Concrete;
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

namespace PiggySyncWin.WinUI.Views
{
    public partial class MainWindow : Form
    {
        // Przycisk autosynchronizacji
        bool autoSynchButton;
        // Przycisk synchronizacji
        bool synchButton;
        // Przycisk trybu
        bool modeButton;
        // Przycisk trybu proste
        bool simpleActive;
        // Przycisk trybu rozszerzonego
        bool complexActive;
        // Przycisk ustawien
        bool configButton;
        // Zmienne tymczasowe dla katalogu
        static String mainPath;
        static String path;
        // Uchwyt okna postepu
        ProgressWindow progressForm;
        List<String> devicesList;

        public void AddDeviaceToSyncList(string devName)
        {
            devicesList.Add(devName);
            listBox1.Items.Add(devName);
        }


        public MainWindow()
        {
            InitializeComponent();

            //Inicjalizacja zmiennych
            autoSynchButton = false;
            modeButton = true;
            simpleActive = true;
            complexActive = false;
            configButton = false;
            synchButton = false;
            mainPath = null;
            path = mainPath;
            devicesList=new List<String>();
            // Uchwyt do okna postepu
            progressForm = new ProgressWindow(this);
            
            this.Icon = (Properties.Resources.logo_green_small);
            panel1.Visible = true;
            panel3.Visible = true;
            
            // Dodatkowe ustawienia w kodzie (przyciski/tabela)
            button3.MouseClick += new MouseEventHandler(autoSynchMouseClick);
            
            dataGridView1.AutoSizeColumnsMode =DataGridViewAutoSizeColumnsMode.Fill;

            // Guziki "w górę" - grafika
            button5.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.up));
            button5.BackgroundImageLayout = ImageLayout.Stretch;

            button10.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.up));
            button10.BackgroundImageLayout = ImageLayout.Stretch;

            // Zegar testowy- do wyrzucenia
            timer1.Tick += new EventHandler(timer1_Tick); 
            timer1.Interval = (90) * (1);

            //Tworzenie ikony powiadomienia
            utworzIkonePowiadomienia();
            
            // Testowe dane dla urządzeń
            //listaUrzadzen.Add("Jasio-PC");
            //listaUrzadzen.Add("Czesio-PC");
            //listaUrzadzen.Add("Krzysio-PC");

            //listBox1.Items.Add(listaUrzadzen[0]);
            //listBox1.Items.Add(listaUrzadzen[1]);
            //listBox1.Items.Add(listaUrzadzen[2]);

            // POCZATEK KODU WOJTKA
            FileManager.Initialize();
            CertificateManager.Initialize();
            mainPath = XmlSettingsRepository.Instance.Settings.CurrentDirectory;
            showDirectory(mainPath);
            textBox1.Text = mainPath;
            textBox2.Text = mainPath;
            this.DidChangedDirectoryEvent = () => {
                FileManager.CreateRootFolder();
            };
            SyncManager main = new SyncManager();
            FileWatcher.Initialize(main);
            DeviaceHistoryManager.Initialize();
            main.Run();

            new TaskFactory().StartNew(() =>
            {
                do
                {
                    Invoke((MethodInvoker)(() =>
                    {
                        listBox1.Items.Clear();
                    }));
                    devicesList = main.GetActiveHostsNames();
                    foreach (var x in devicesList)
                    {
                        Invoke((MethodInvoker)( () =>
                        {
                            listBox1.Items.Add(x);
                        }));
                    }
                    System.Threading.Thread.Sleep(10000);
                } while (true);
            });
            // KONIEC KODU WOJTKA
        }

        // Obsluga minimalizacji
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIconIkonaPaska.ShowBalloonTip(3000);
                this.ShowInTaskbar = false;
            }
        }

        // Tworzenie ikony powiadomienia
        private void utworzIkonePowiadomienia()
        {
            // Ikona powiadomienia
            notifyIconIkonaPaska.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            notifyIconIkonaPaska.BalloonTipText = "Program wciąż działa w tle...";
            notifyIconIkonaPaska.BalloonTipTitle = "PiggySynch";
//            notifyIconIkonaPaska.Icon = ((System.Drawing.Icon)(Properties.Resources.logo_green_small)); //The tray icon to use
            notifyIconIkonaPaska.Text = "PiggySynch";
            notifyIconIkonaPaska.Visible = true;

            // Menu kontekstowe ikony powiadomienia na pasku
            ContextMenu contextMenu1 = new System.Windows.Forms.ContextMenu();
            MenuItem menuItem1 = new System.Windows.Forms.MenuItem();
            MenuItem menuItem2 = new System.Windows.Forms.MenuItem();
            MenuItem menuItem3 = new System.Windows.Forms.MenuItem();

            contextMenu1.MenuItems.AddRange(
                        new System.Windows.Forms.MenuItem[] { menuItem3, menuItem2, menuItem1 });
            textBox1.ReadOnly = true;

            //Inicjalizacja
            menuItem1.Index = 1;
            menuItem1.Text = "Zakończ";
            menuItem1.Click += new System.EventHandler(this.contextMenu_ExitClick) ;
            menuItem2.Index = 1;
            menuItem2.Text = "Otwórz";
            menuItem2.Click += new System.EventHandler(this.contextMenu_OpenClick);
            menuItem3.Index = 1;
            menuItem3.Text = "Synchronizuj";
            menuItem3.Click += new System.EventHandler(this.contextMenu_SynchClick);

            notifyIconIkonaPaska.ContextMenu = contextMenu1;

            this.CenterToScreen();
        }

        // Obsługa kliknięć  menu kontekstowego traya
        private void contextMenu_ExitClick(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void contextMenu_SynchClick(object sender, System.EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            progressForm.Show();
            synchButtonFunc();
        }

        private void contextMenu_OpenClick(object sender, System.EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        // Obsluga klikniecia ikony powiadomienia
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        // Obsluga przycisku włączania i wyłączania autosynchronizacji
        void autoSynchMouseClick(object sender, EventArgs e)
        {
            if (autoSynchButton == false)
            {
                this.button3.Image = ((System.Drawing.Image)(Properties.Resources.on));
                autoSynchButton = true;
            }
            else
            {
                this.button3.Image = ((System.Drawing.Image)(Properties.Resources.off));
                autoSynchButton = false;
            }
        }

        // Obsluga przycisku synchronizacji
        public void synchButtonClick(object sender, EventArgs e)
        {
            // Wywolanie ramki OknoPostepu
            progressForm.Show();
            synchButtonFunc();
        }

        // Funkcja włączająca/wyłączająca synchronizację
        public void synchButtonFunc()
        {
            if (synchButton == true)
            {
                timer1.Stop();
                synchButton = false;
                button1.Text = "Synchronizuj";
                progressForm.Hide();
                progressForm.setProgressBar(0);
            }
            else
            {
                timer1.Start();
                synchButton = true;
                this.label7.Text = DateTime.Now.ToString("dd-MM-yyyy (HH:mm)");
                button1.Text = "Przerwij";
            }
        }

        // Obsluga przycisku zmiany trybu na prosty
        private void modeSimpleButtonClick(object sender, EventArgs e)
        {
            if (simpleActive == false)
            {
                this.button2.Image = null;
                this.button8.Image = null;
                this.button2.Image = ((System.Drawing.Image)(Properties.Resources.prosty));
                this.button8.Image = ((System.Drawing.Image)(Properties.Resources.rozszerzonyB));
                simpleActive = true;
                complexActive = false;
                panel3.Visible = true;
                panel1.Visible = true;
                modeButton = true;
            }
        }

        // Obsluga przycisku zmiany trybu na rozszerzony
        private void modeComplexButtonClick(object sender, EventArgs e)
        {
            if (complexActive == false)
            {
                this.button2.Image = null;
                this.button8.Image = null;
                this.button2.Image = ((System.Drawing.Image)(Properties.Resources.prostyB));
                this.button8.Image = ((System.Drawing.Image)(Properties.Resources.rozszerzony));
                complexActive = true;
                simpleActive = false;
                panel3.Visible = true;
                panel1.Visible = false;
                modeButton = false;
            }
        }

        // Testowy timer - do usunięcia
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressForm.getProgressBar() >= 100)
            {
                progressForm.setProgressBar(0);
            }
            else
                progressForm.setProgressBar(progressForm.getProgressBar() + 1);
        }

        // Obsluga przycisku ustawień
        private void optionsButtonClick(object sender, EventArgs e)
        {
            if (configButton == false)
            {
                configButton = true;
                panel1.Visible = true;
                panel3.Visible = false;
            }
            else if (modeButton == true)
            {
                panel3.Visible = true;
                panel1.Visible = true;
                configButton = false;
            }
            else
            {
                panel3.Visible = true;
                panel1.Visible = false;
                configButton = false;
            }
        }

        // Funkcja load dla okna glownego
        private void Form1_Load(object sender, EventArgs e)
        {
            this.listView1.View = View.LargeIcon;
            this.imageList1.Images.Add(Properties.Resources.folder);
            this.imageList1.ImageSize = new Size(32, 32);
            this.listView1.LargeImageList = this.imageList1;
        }

        // Funkcja czyszcząca listę z ikonami
        private void resetImages(ImageList lista)
        {
            lista.Images.Clear();
            lista.Images.Add(Properties.Resources.folder);
        }
        
        // Funkcja ładująca ikonę
        public static Icon IconFromFilePath(string filePath)
        {
            var result = (Icon)null;

            try
            {
                result = Icon.ExtractAssociatedIcon(filePath);
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        // Funkcja przycinająca aktualny path (w górę katalogu)
        private bool cutPath(String zrodlo)
        {
            if (zrodlo.Length > mainPath.Length)
            {
                path = path.Substring(0, zrodlo.LastIndexOf("\\"));
                return true;
            }
            return false;
        }

        // Obsluga przycisku wyboru katalogu (poprzez podwójne kliknięcie na ikonę)
        private void listView1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems[0].ToolTipText == "Folder")
            {
                String klik = listView1.SelectedItems[0].Text;
                showDirectory(path + "/" + klik);
            }
        }

        // Obsluga przycisku "w górę"
        private void upButtonClick(object sender, EventArgs e)
        {
            if (mainPath != null)
            {
                if (cutPath(path) == true)
                {
                    showDirectory(path);
                }
            }
        }

        // Funkcja wyświetlająca katalog w obu trybach
        private void showDirectory(String zrodlo)
        {
            try
            {
                int counter = 0;
                int i = 0;
                // Reset danych o katalogu
                listView1.Items.Clear();
                resetImages(imageList1);
                dataGridView1.Rows.Clear();

                // Pobranie informacji o katalogu
                DirectoryInfo nodeDirInfo = new DirectoryInfo(zrodlo);
                path = nodeDirInfo.FullName;

                ListViewItem.ListViewSubItem[] subItems;
                ListViewItem item = null;
                
                // Pobranie informacji o folderach
                foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
                {
                    item = new ListViewItem(dir.Name, 0);
                    subItems = new ListViewItem.ListViewSubItem[]
                    {
                        new ListViewItem.ListViewSubItem(item, "Directory"), 
                            new ListViewItem.ListViewSubItem(item, 
						dir.LastAccessTime.ToShortDateString())
                    };

                    // Ustawienie danych i dodanie katalogów do listy i tabeli
                    item.ImageIndex = 0;
                    item.ToolTipText = "Folder";
                    item.SubItems.AddRange(subItems);

                    listView1.Items.Add(item);

                    this.dataGridView1.Rows.Add(dir.Name);
                    dataGridView1.Rows[counter].Tag = "Folder";
                    dataGridView1.Rows[counter++].Cells[0].Style.ForeColor = Color.RoyalBlue;
                
                }

                // Pobranie informacji o plikach
                foreach (FileInfo file in nodeDirInfo.GetFiles())
                {
                    item = new ListViewItem(file.Name, 1);
                    subItems = new ListViewItem.ListViewSubItem[]
                    { new ListViewItem.ListViewSubItem(item, "File"), 
                     new ListViewItem.ListViewSubItem(item, 
						file.LastAccessTime.ToShortDateString())};
                    
                    // Ustawienie danych i dodanie plików do listy i tabeli
                    this.imageList1.Images.Add(IconFromFilePath(file.FullName));
                    item.ImageIndex = ++i;
                    item.SubItems.AddRange(subItems);

                    listView1.Items.Add(item);
                    this.dataGridView1.Rows.Add(file.Name, file.CreationTime.ToString(), file.LastWriteTime.ToString());
              }
                //Zmiana rozmiaru 
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception e)
            {
                // Obsługa braku dostępu do folderu
                MessageBox.Show("BRAK DOSTĘPU!" + e);
                cutPath(path);
                showDirectory(path);
            }
        }

        // Obsługa przycisku wyboru folderu
        private void chooseFolderButtonClick(object sender, EventArgs e)
        {
            // Wybór folderu i wyświetlenie okna
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.Description = "Wybierz folder do synchronizacji";
            fd.ShowDialog();

            // Wybranie i wypisanie ścieżki
            if (fd.SelectedPath.Length > 1)
            {
                int numer = fd.SelectedPath.LastIndexOf("\\");
                String temp = fd.SelectedPath.ToString().Substring(numer + 1);
   
                if (numer == 2)
                {
                    temp = fd.SelectedPath.ToString().Replace("\\", "");
                }
      
                mainPath = fd.SelectedPath;
                
                XmlSettingsRepository.Instance.Settings.CurrentDirectory = mainPath; 
                XmlSettingsRepository.Instance.SaveSettings();
                FileWatcher.RefreshMonitoredDirectory(XmlSettingsRepository.Instance.Settings.CurrentDirectory);
                showDirectory(mainPath);
                textBox1.Text = mainPath;
                textBox2.Text = mainPath;
                DidChangedDirectoryEvent();
            }
        }

        public delegate void DidChangedDirectory();
        public DidChangedDirectory DidChangedDirectoryEvent
        {
            get;
            set;
        }

        // Obsługa podwójnego kliknięcia w folder w tabeli
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows[0].Tag != null)
            {
                if (dataGridView1.SelectedRows[0].Tag.ToString() == "Folder")
                {
                    String klik = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    showDirectory(path + "/" + klik);
                }
            }
        }
       
        // Obsluga wyszukiwarki urządzeń
        private void searchTextFunc(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (var item in devicesList)
            {
                if (item.ToLower().Contains(textBox3.Text.ToLower()))
                    listBox1.Items.Add(item.ToString());
            }

            if (textBox3.Text == "")
            {
 
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
