using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace StoneDocuments_r24_1.Forms
{
    /// <summary>
    /// Interaction logic for frmScheduleSwap.xaml
    /// </summary>
    public partial class frmScheduleSwap :Window
    {
        ObservableCollection<ViewSchedule> ScheduleData { get; set; }

        public frmScheduleSwap(List<ViewSchedule> ScheduleList)
        {
            InitializeComponent();

            ScheduleData = new ObservableCollection<ViewSchedule>(ScheduleList);

            cmbSchedules.DataContext = ScheduleData;
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
    }
}
