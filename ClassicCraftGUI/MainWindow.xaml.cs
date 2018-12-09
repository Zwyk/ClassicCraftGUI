using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace ClassicCraftGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TargetError_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(NbSim != null)
            {
                NbSim.IsEnabled = (string)((ComboBoxItem)((ComboBox)sender).SelectedValue).Content == "Fixed number";
            }
        }

        private void LogFight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(TargetError != null && NbSim != null)
            {
                if ((string)((ComboBoxItem)((ComboBox)sender).SelectedValue).Content == "Enabled")
                {
                    TargetError.IsEnabled = false;
                    NbSim.IsEnabled = true;
                    NbSim.SelectedIndex = 0;
                    NbSim.Items.RemoveAt(2);
                    NbSim.Items.RemoveAt(2);
                    NbSim.Items.RemoveAt(2);
                }
                else
                {
                    TargetError.IsEnabled = true;
                    NbSim.IsEnabled = (string)((ComboBoxItem)TargetError.SelectedValue).Content == "Fixed number";
                    NbSim.Items.Add("100");
                    NbSim.Items.Add("1000");
                    NbSim.Items.Add("10000");
                }
            }
        }

        private void BossLifeMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BossLowLifeTime != null)
            {
                BossLowLifeTime.IsEnabled = (string)((ComboBoxItem)((ComboBox)sender).SelectedValue).Content == "Custom";
            }
        }

        private static readonly Regex REG1 = new Regex("[^0-9.]+");

        private void BossLowLifeTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = REG1.IsMatch(e.Text);
        }

        private static readonly Regex REG2 = new Regex("[^0-9]+");

        private void BossLevel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = REG2.IsMatch(e.Text);
        }

        private void Class_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PlayerTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(AddPlayer != null && AddPlayer.IsSelected)
            {
                string s = XamlWriter.Save(SampleTab);

                StringReader stringReader = new StringReader(s);

                XmlReader xmlReader = XmlTextReader.Create(stringReader, new XmlReaderSettings());
                XmlReaderSettings sx = new XmlReaderSettings();

                object x = XamlReader.Load(xmlReader);

                PlayerTabs.Items.Insert(PlayerTabs.Items.Count-1, x);

                ((TabItem)x).Header = "Player" + (PlayerTabs.Items.Count - 1);

                PlayerTabs.SelectedIndex = PlayerTabs.Items.Count - 2;
            }
        }
    }
}
