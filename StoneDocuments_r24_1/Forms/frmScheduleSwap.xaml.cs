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
        public frmScheduleSwap(List<ViewSchedule> newScheduleList, List<ViewSchedule> curScheduleList)
        {
            InitializeComponent();

            foreach (ViewSchedule curSched in newScheduleList)
            {
                cmbNewSchedules.Items.Add(curSched);
            }

            cmbNewSchedules.SelectedIndex = 0;

            foreach (ViewSchedule curSched in curScheduleList)
            {
                cmbCurSchedules.Items.Add(curSched);
            }

            cmbCurSchedules.SelectedIndex = 0;
        }

        public ViewSchedule GetComboBoxViewScheduleSelectedItem()
        {
            return cmbNewSchedules.SelectedItem as ViewSchedule;
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
