using Autodesk.Revit.DB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace StoneDocuments_r24_1
{
    /// <summary>
    /// Interaction logic for frmSheetMaker.xaml
    /// </summary>
    public partial class frmSheetMaker : Window
    {
        ObservableCollection<clsSheetData> SheetList { get; set; }
        ObservableCollection<Element> TBlockData { get; set; }
        ObservableCollection<View> ViewData { get; set; }
        ObservableCollection<ViewSchedule> ScheduleData { get; set; }

        public List<Element> elemList;

        public frmSheetMaker(List<Element> TblockList, List<View> ViewList, List<ViewSchedule> ScheduleList)
        {
            InitializeComponent();

            SheetList = new ObservableCollection<clsSheetData>();
            TBlockData = new ObservableCollection<Element>(TblockList);
            ViewData = new ObservableCollection<View>(ViewList);
            ScheduleData = new ObservableCollection<ViewSchedule>(ScheduleList);

            sheetGrid.ItemsSource = SheetList;
            cmbTitleblock.ItemsSource = TBlockData;
            cmbViews.ItemsSource = ViewData;
            cmbSchedules.ItemsSource = ScheduleData;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SheetList.Add(new clsSheetData());
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (clsSheetData curRow in SheetList)
                {
                    if (sheetGrid.SelectedItem == curRow)
                        SheetList.Remove(curRow);
                }
            }
            catch (Exception)
            { }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            SheetList.Clear();

            OpenFileDialog selectFile = new OpenFileDialog();
            selectFile.Multiselect = false;
            selectFile.RestoreDirectory = true;
            selectFile.Filter = "*csv file (*.csv)|*.csv";

            if (selectFile.ShowDialog() == true)
            {
                // read the csv file
                string[] sheetArray = System.IO.File.ReadAllLines(selectFile.FileName);

                foreach (string sheetString in sheetArray)
                {
                    string[] cellData = sheetString.Split(',');

                    clsSheetData curSD = new clsSheetData();
                    curSD.SheetNumber = cellData[0];
                    curSD.SheetName = cellData[1];

                    // add method to get view by name

                    // add method to get titleblock by name

                    SheetList.Add(curSD);
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = "";
            System.Windows.Forms.FolderBrowserDialog folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            folderDialog.RootFolder = Environment.SpecialFolder.MyDocuments;

            if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folderPath = folderDialog.SelectedPath;
                string csvFilePath = folderPath + "\\sheet list export.csv";

                using (StreamWriter writer = new StreamWriter(csvFilePath))
                {
                    foreach (clsSheetData curSheet in SheetList)
                    {
                        string sheetNum = "";
                        string sheetName = "";
                        string view = "";
                        string titleBlock = "";

                        if (curSheet.SheetName != null)
                            sheetName = curSheet.SheetName;
                        if (curSheet.SheetNumber != null)
                            sheetNum = curSheet.SheetNumber;
                        if (curSheet.SelectedView != null)
                            view = curSheet.SelectedView.Name;
                        if (curSheet.Titleblock != null)
                            titleBlock = curSheet.Titleblock.Name;

                        writer.WriteLine(sheetNum + "," + sheetName + "," + view + "," + titleBlock);
                    }
                }
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        public List<clsSheetData> GetSheetData()
        {
            return SheetList.ToList();
        }
    }
}
