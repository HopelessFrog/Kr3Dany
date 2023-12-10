using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Security.Policy;

namespace Kr3G
{
    public interface IMainForm : IView
    {
        string LeftBorder { get; set; }
        string RightBorder { get; set; }
        string Step { get; set; }
        string C { get; set; }
        string A { get; set; }



        string FilePath { get; }


        string LogPath { get; set; }
        Image Image { get; }

        bool ShowGreetings { get; set; }
        Color TextColor { get; set; }

        Color BackGroundColor { get; set; }

        void Draw(GraphData graphData, string _label);

        void ClearFields();


        event EventHandler TryDraw;
        event EventHandler Clear;
        event EventHandler Removelast;
        event EventHandler OpenFile;
        event EventHandler SaveInitialData;
        event EventHandler SaveData;
        event EventHandler NoData;


    }

    public partial class MainForm : Form, IMainForm
    {
        [Required]
        public string LeftBorder
        {
            get => textBoxLeftBorder.Text;
            set
            {
                textBoxLeftBorder.Text = value;
                this.Refresh();
            }
        }

        public string RightBorder
        {
            get => textBoxRightBorder.Text;
            set
            {
                textBoxRightBorder.Text = value;
                this.Refresh();
            }
        }

        public string Step
        {
            get => textBoxStep.Text;
            set
            {
                textBoxStep.Text = value;
                this.Refresh();
            }
        }

        public string C
        {
            get => textBoxC.Text;
            set
            {
                textBoxC.Text = value;
                this.Refresh();
            }
        }
        public string A
        {
            get => textBoxA.Text;
            set
            {
                textBoxA.Text = value;
                this.Refresh();
            }
        }

        public string FilePath
        {
            get;
            private set;
        }

        public Image Image
        {
            get
            {
                using var ms = new MemoryStream(chart.Plot.GetImageBytes(), 0, chart.Plot.GetImageBytes().Length);
                Image image = Image.FromStream(ms, true);

                return image;
            }

        }

        public bool ShowGreetings { get; set; }

        public Color TextColor
        {
            get => this.ForeColor;
            set
            {
                //TextColor = value;
                this.ForeColor = value;
                menuStrip.ForeColor = value;
            }
        }

        public Color BackGroundColor
        {
            get => this.BackColor;
            set
            {
                this.BackColor = value;
                menuStrip.BackColor = value;
            }
        }

        public string LogPath
        {
            get;
            set;
        }



        public MainForm()
        {
            //ShowGreetings = showGreetings;
            FilePath = "";
            LogPath = "";
            InitializeComponent();

            //  BackGroundColor = Color.FromName(ConfigurationManager.AppSettings["BackColor"]);
            //TextColor = Color.FromName(ConfigurationManager.AppSettings["TextColor"]);





        }



        public event EventHandler? TryDraw;

        public event EventHandler? Clear;

        public event EventHandler? Removelast;

        public event EventHandler? OpenFile;

        public event EventHandler? SaveInitialData;

        public event EventHandler? SaveData;

        public event EventHandler? NoData;




        public new void Show()
        {
            Application.Run(this);
        }

        private void Greatings()
        {
            MessageBox.Show("Verbetskyi Dany  3 kr var 3  ", "Greatings", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        public void ClearFields()
        {
            textBoxLeftBorder.Text = "";
            textBoxRightBorder.Text = "";
            textBoxStep.Text = "";
            textBoxC.Text = "";
            this.Refresh();

        }
        private void drawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TryDraw?.Invoke(this, EventArgs.Empty);


        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chart.Plot.Clear();
            ClearFields();
            chart.Refresh();
            Clear?.Invoke(this, EventArgs.Empty);
        }
        private void removeLastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chart.Plot.GetPlottables().Length > 0)
            {

                chart.Plot.RemoveAt(chart.Plot.GetPlottables().Length - 1);
                chart.Refresh();

                Removelast?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                NoData?.Invoke(this, EventArgs.Empty);
            }

        }

        public void Draw(GraphData graphData, string _label)
        {
            var scatter = chart.Plot.AddScatter(graphData.xs, graphData.ys, label: _label);

            chart.Plot.Legend();

            chart.Refresh();

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text file|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = dialog.FileName;
            }
            else
            {
                FilePath = "";
            }



            if (FilePath != "") OpenFile?.Invoke(this, EventArgs.Empty);
        }

        private void initialToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (chart.Plot.GetPlottables().Length == 0)
            {
                NoData?.Invoke(this, EventArgs.Empty);
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.OverwritePrompt = true;
            dialog.Filter = "Text file|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = dialog.FileName;
            }
            else
            {
                FilePath = "";
            }

            if (FilePath != "") SaveInitialData?.Invoke(this, EventArgs.Empty);
        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (chart.Plot.GetPlottables().Length == 0)
            {
                NoData?.Invoke(this, EventArgs.Empty);
                return;
            }
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.OverwritePrompt = true;
            dialog.Filter = "Xlsx|*.xlsx";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = dialog.FileName;
            }
            else
            {
                FilePath = "";
            }
            if (FilePath != "") SaveData?.Invoke(this, EventArgs.Empty);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chart.Plot.AxisZoom(0, 0, 1500, 15000);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowGreetings = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowGreetings"]);
            if (ShowGreetings)
                Greatings();
            onToolStripMenuItem.Checked = ShowGreetings;
        }

       

       

      

        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowGreetings = onToolStripMenuItem.Checked;
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;


            settings["ShowGreetings"].Value = ShowGreetings.ToString();

            configFile.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);

        }

       

        
    }

}