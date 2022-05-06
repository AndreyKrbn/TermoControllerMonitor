using SerialPortChating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace TermoControllerMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPortChat portChat = null;
        Thread ClearAlarm1 = null;
        Thread ClearAlarm2 = null;
        Thread ClearAlarm3 = null;
        Thread ClearAlarm4 = null;
        public MainWindow()
        {           
            InitializeComponent();
            portsNames.ItemsSource = SerialPortChat.PortNames;
            baudRates.ItemsSource = SerialPortChat.BaudRates.Select(x => (object)x).ToArray();
            baudRates.SelectedIndex = 4;
            btnMonitoring.IsEnabled = false;
            btnSettings.IsEnabled = false;
        }

        private void btnOpenClose_Click(object sender, RoutedEventArgs e)
        {
            if (portChat == null)
            {               
                portChat = new SerialPortChat((string)portsNames.SelectedItem, (int)baudRates.SelectedItem, "None", 8, "One", "None");
                portChat.PortOpen += PortChat_PortOpen;
                portChat.PortClose += PortChat_PortClose;
                portChat.RecivedAnswer += PortChat_RecivedAnswer;
                portChat.OpenPort();
                btnMonitoring.IsEnabled = true;
                btnSettings.IsEnabled = true;
            }
            else
            {
                portChat.ClosePort();
                portChat = null;
            }
        }

        private void PortChat_RecivedAnswer(byte[] obj)
        {
            switch (obj[2])
            {
                case 12:
                    ClearAlarm1?.Abort();
                    SetRedAlarm(IndicatorOverCurrent);
                    ClearAlarm1 = new Thread(() =>
                    {
                        SetGreenAlarm(IndicatorOverCurrent);
                    });
                    ClearAlarm1.Start();
                    break;
                case 13:
                    ClearAlarm2?.Abort();
                    SetRedAlarm(IndicatorOffCurrent);
                    ClearAlarm2 = new Thread(() =>
                    {
                        SetGreenAlarm(IndicatorOffCurrent);
                    });
                    ClearAlarm2.Start();
                    break;
                case 14:
                    ClearAlarm3?.Abort();
                    SetRedAlarm(IndicatorOverTemp);
                    ClearAlarm3 = new Thread(() =>
                    {
                        SetGreenAlarm(IndicatorOverTemp);
                    });
                    ClearAlarm3.Start();
                    break;
                case 15:
                    ClearAlarm4?.Abort();
                    SetRedAlarm(IndicatorOffTemp);
                    ClearAlarm4 = new Thread(() =>
                    {
                        SetGreenAlarm(IndicatorOffTemp);
                    });
                    ClearAlarm4.Start();
                    break;
                default:
                    break;
            }
        }

        private void SetRedAlarm(Ellipse ellipse)
        {
            MainGrid.Dispatcher.Invoke(new Action(() => ellipse.Fill = new SolidColorBrush(Colors.Red)));
        }

        private void SetGreenAlarm(Ellipse ellipse)
        {
            Thread.Sleep(5000);
            MainGrid.Dispatcher.Invoke(new Action(() => ellipse.Fill = new SolidColorBrush(Colors.Green)));
        }

        private void PortChat_PortClose()
        {
            Indicator.Fill = new SolidColorBrush(Colors.Red);
            btnOpenClose.Content = "Открыть";
        }

        private void PortChat_PortOpen()
        {
            Indicator.Fill = new SolidColorBrush(Colors.Green);
            btnOpenClose.Content = "Закрыть";
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsW = SettingsWindow.Create(portChat);
            settingsW.Owner = this;
            settingsW.Show();
        }

        private void btnMonitoring_Click(object sender, RoutedEventArgs e)
        {
            MonitorWindow MonitorW = MonitorWindow.Create(portChat);
            MonitorW.Owner = this;
            MonitorW.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (Window item in this.OwnedWindows)
            {
                item.Close();
            }
        }
    }
}
