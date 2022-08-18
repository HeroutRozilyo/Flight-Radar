using FlightModel;
using PFlight.viewmodel;
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

namespace PFlight.views.UserControls
{
    /// <summary>
    /// Interaction logic for AutoCompleteTextBoxUserControl.xaml
    /// </summary>
    public partial class AutoCompleteTextBoxUserControl : UserControl
    {
        string defutlText = "Search in your History here";
        private ObservableCollection<string> autoSuggestionList = new ObservableCollection<string>();
        public FlightData flightData = new FlightData();
        public static screen1VM CurrentVM { get; set; }

        public AutoCompleteTextBoxUserControl()
        {
            try
            {
                
                // Initialization.  
                this.InitializeComponent();
                this.autoTextBox.Text = defutlText;
                autoListPopup.StaysOpen = false;
                CloseAutoSuggestionBox();
                CurrentVM = new screen1VM();
                



            }
            catch (Exception ex)
            {
                // Info.  
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.Write(ex);
            }
        }
        


        #region Protected / Public properties.  

        /// <summary>  
        /// Gets or sets Auto suggestion list property.  
        /// </summary>  
        public ObservableCollection<string> AutoSuggestionList
        {
            get { return this.autoSuggestionList; }
            set { this.autoSuggestionList = value; }
            
        }

        #endregion

        #region Open Auto Suggestion box method  

        /// <summary>  
        ///  Open Auto Suggestion box method  
        /// </summary>  
        private void OpenAutoSuggestionBox()
        {
            try
            {
                // Enable.  
                this.autoListPopup.Visibility = Visibility.Visible;
                this.autoListPopup.IsOpen = true;
                this.autoList.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                // Info.  
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.Write(ex);
            }
        }

        #endregion

        #region Close Auto Suggestion box method  

        /// <summary>  
        ///  Close Auto Suggestion box method  
        /// </summary>  
        private void CloseAutoSuggestionBox()
        {
            try
            {
                // Enable.  
                this.autoListPopup.Visibility = Visibility.Collapsed;
                this.autoListPopup.IsOpen = false;
                this.autoList.Visibility = Visibility.Collapsed;
               
            }
            catch (Exception ex)
            {
                // Info.  
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.Write(ex);
            }
        }

        #endregion

        #region Auto Text Box text changed the method  

        /// <summary>  
        ///  Auto Text Box text changed method.  
        /// </summary>  
        /// <param name="sender">Sender parameter</param>  
        /// <param name="e">Event parameter</param>  
        private void AutoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                // Verification.  
                if (string.IsNullOrEmpty(this.autoTextBox.Text))
                {
                    // Disable.  
                    this.CloseAutoSuggestionBox();

                    // Info.  
                    return;
                }
                

                // Enable.  
                this.OpenAutoSuggestionBox();

                // Settings.  
                this.autoList.ItemsSource = this.AutoSuggestionList.Where(p => p.ToLower().Contains(this.autoTextBox.Text.ToLower())).ToList();
            }
            catch (Exception ex)
            {
                // Info.  
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.Write(ex);
            }
        }

        #endregion
        public delegate void SeletedChangeUC(object sender, EventArgs e);
        public event SeletedChangeUC selectedChangeUC;

        #region Auto list selection changed method  

        /// <summary>  
        ///  Auto list selection changed method.  
        /// </summary>  
        /// <param name="sender">Sender parameter</param>  
        /// <param name="e">Event parameter</param>  
        private void AutoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = (System.Windows.Controls.ListBox)sender; //to get the line

            string code = list.SelectedItems[0].ToString();
           
            try
            {
                // Verification.  
                if (this.autoList.SelectedIndex <= -1)
                {
                    // Disable.  
                    this.CloseAutoSuggestionBox();

                    // Info.  
                    return;
                }

                // Disable.  
                this.CloseAutoSuggestionBox();

                // Settings.  
                this.autoTextBox.Text = this.autoList.SelectedItem.ToString();
              //  this.autoList.SelectedIndex = -1;

                 flightData = CurrentVM.GetFlightCode(code);
       
                if(selectedChangeUC!=null)
                {
                    selectedChangeUC(sender, e);
                }
                this.autoListPopup.IsOpen = false;
               
                
            }
            catch (Exception ex)
            {
                // Info.  
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.Write(ex);
            }
           
        }
        private void autoTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text == defutlText)
            {
                autoTextBox.Text = "";

            }
            this.OpenAutoSuggestionBox();

            // Settings.  
            this.autoList.ItemsSource = this.AutoSuggestionList.ToList();
            autoListPopup.StaysOpen = true;
            autoListPopup.PlacementTarget = autoTextBox;

        }

        private void autoTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            autoListPopup.StaysOpen = false;

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            autoList.SelectedIndex = 0;

        }

        private void autoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
                autoList.SelectedIndex = 1;

        }
    }

        #endregion
    
}