using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BeamPro
{
    public partial class MainForm : Form
    {
        // Private objects
        private BackgroundWorker solveWorker = new BackgroundWorker();
        private string workingFile = "";

        // Program variables
        public static string DefaultDirectory = "C:\\UnlikelyFilename";

        // Default class constructor
        public MainForm()
        {
            InitializeComponent();

            solveWorker.WorkerReportsProgress = true;
            solveWorker.DoWork += new DoWorkEventHandler(solveWorker_DoWork);
            solveWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(solveWorker_RunWorkerCompleted);
            solveWorker.ProgressChanged += new ProgressChangedEventHandler(solveWorker_ProgressChanged);
        }

        // Load form
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Add beam types to the pallette
            componentPallette1.AddElementType(new BasicSectionDragDropObject());
            componentPallette1.AddElementType(new BasicConditionDragDropObject());
        }

        // Center view button click event handler
        private void toolStripZeroViewButton_Click(object sender, EventArgs e)
        {
            objectHolder1.CenterView();
        }

        // Event handler for clicking the solve button
        private void toolStripSolveButton_Click(object sender, EventArgs e)
        {
            AnalysisManager analysis = new AnalysisManager(objectHolder1);
            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Visible = true;
            toolStrip1.Enabled = false;
            objectHolder1.Enabled = false;
            componentPallette1.Enabled = false;
            solveWorker.RunWorkerAsync(analysis);
        }

        // Async DoWork function
        private void solveWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            e.Result = Process((AnalysisManager)e.Argument, worker);
        }

        // Async work completed function
        private void solveWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                AnalysisManager analysis = (AnalysisManager)e.Result;
                PrintResult(FormatOutputData(analysis));
            }
            toolStripProgressBar1.Visible = false;
            toolStrip1.Enabled = true;
            objectHolder1.Enabled = true;
            componentPallette1.Enabled = true;
        }

        // Async progress updater function
        private void solveWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
        }

        // Async helper function
        public AnalysisManager Process(AnalysisManager analysis, BackgroundWorker worker)
        {
            analysis.RunAnalysis(worker);
            return analysis;
        }

        // Output generation function
        private void PrintResult(IEnumerable<string[]> dataToWrite)
        {
            // Create output file unique name and ensure output directory exists
            if (!System.IO.Directory.Exists(MainForm.DefaultDirectory))
            {
                FolderBrowserDialog selectFolderDialog = new FolderBrowserDialog();
                if (selectFolderDialog.ShowDialog() == DialogResult.OK)
                {
                    MainForm.DefaultDirectory = selectFolderDialog.SelectedPath;
                }
            }
            string outputFileName = MainForm.DefaultDirectory + "\\Beam_Output_" + DateTime.Now.Month.ToString() + "-" +
                    DateTime.Now.Day.ToString() + "-" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() +
                    DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".csv";
            StreamWriter writer = new StreamWriter(outputFileName);

            // Create I/O stream and write output data
            foreach (string[] dataLine in dataToWrite)
            {
                for (int i = 0; i < dataLine.Length - 1; i++)
                {
                    writer.Write(dataLine[i] + ", ");
                }
                writer.Write(dataLine[dataLine.Length - 1]);
                writer.Write("\r\n");
            }
            writer.Dispose();
        }

        // Output IEnumerable<string> creator
        private IList<string[]> FormatOutputData(AnalysisManager results)
        {
            // Set up list of data to write
            List<string[]> dataToWrite = new List<string[]>();

            //Create list of output strings in csv format
            dataToWrite.Add(new string[] { "Node", "X", "Y", "theta", "P", "V", "M", "Sa", "Ss", "Sb" });
            IEnumerator<INode> nodesEnumerator = results.Nodes.GetEnumerator();
            IEnumerator<double?> nodeForces = results.Forces.GetEnumerator();
            IEnumerator<double> stressesEnumerator = results.Stresses.GetEnumerator();
            for (int i = 0; nodesEnumerator.MoveNext(); i++)
            {
                int n = 0;
                string[] line = new string[dataToWrite[0].Count()];
                line[n++] = i.ToString();
                for (int j = 0; j < nodesEnumerator.Current.Location.Count(); j++)
                {
                    line[n++] = nodesEnumerator.Current.Location[j].ToString();
                }
                for (int j = 0; j < nodesEnumerator.Current.Force.Count(); j++)
                {
                    nodeForces.MoveNext();
                    line[n++] = nodeForces.Current.Value.ToString();
                }
                for (int j = 0; j < nodesEnumerator.Current.Force.Count(); j++)
                {
                    stressesEnumerator.MoveNext();
                    line[n++] = stressesEnumerator.Current.ToString();
                }
                dataToWrite.Add(line);
            }

            return dataToWrite;
        }

        // Save as button event handler
        private void saveToolStripSaveAsMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog selectFileDialog = new SaveFileDialog();
            if (workingFile == String.Empty)
            {
                selectFileDialog.InitialDirectory = MainForm.DefaultDirectory;
            }
            else
            {
                selectFileDialog.InitialDirectory = workingFile.Trim().Remove(workingFile.LastIndexOf(@"\"));
            }
            selectFileDialog.Filter = "bpa files (*.bpa)|*.bpa|All files (*.*)|*.*";
            selectFileDialog.FilterIndex = 1;
            if (selectFileDialog.ShowDialog() == DialogResult.OK)
            {
                workingFile = selectFileDialog.FileName;
                MainForm.DefaultDirectory = workingFile.Trim().Remove(workingFile.LastIndexOf(@"\"));
            }
            objectHolder1.SaveAnalysis(workingFile);
            saveToolStripSaveMenuItem.Enabled = true;
        }

        // Save button event handler
        private void saveToolStripSaveMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                objectHolder1.SaveAnalysis(workingFile);
            }
            catch (System.Exception)
            {
                System.Windows.Forms.MessageBox.Show("Error with Save (is save path valid?)");
                saveToolStripSaveAsMenuItem_Click(sender, e);
            }
        }

        // Load button event handler
        private void toolStripLoadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectFileDialog = new OpenFileDialog();
            if (workingFile == String.Empty)
            {
                selectFileDialog.InitialDirectory = MainForm.DefaultDirectory;
            }
            else
            {
                selectFileDialog.InitialDirectory = workingFile.Trim().Remove(workingFile.LastIndexOf(@"\"));
            }
            selectFileDialog.Filter = "bpa files (*.bpa)|*.bpa|All files (*.*)|*.*";
            selectFileDialog.FilterIndex = 1;
            if (selectFileDialog.ShowDialog() == DialogResult.OK)
            {
                workingFile = selectFileDialog.FileName;
                MainForm.DefaultDirectory = workingFile.Trim().Remove(workingFile.LastIndexOf(@"\"));
            }
            objectHolder1.LoadAnalysis(workingFile);
            saveToolStripSaveMenuItem.Enabled = true;
        }
    }
}
