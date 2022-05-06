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
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TermoControllerMonitor
{
    /// <summary>
    /// Interaction logic for MonitorWindow.xaml
    /// </summary>
    public partial class MonitorWindow : Window
    {
        private static MonitorWindow instance;
        private SerialPortChat portChat;
        private System.Threading.Timer commandTimer = null;
        private MonitorWindow()
        {
            InitializeComponent();
            this.gAGaugeFanExt.MaxValue = 4000;
            this.gAGaugeFanInt.MaxValue = 4000;
            this.gAGaugeFanExt.MinValue = 0;
            this.gAGaugeFanInt.MinValue = 0;
            this.gAGaugeFanExt.ScaleLinesMajorStepValue = 400;
            this.gAGaugeFanInt.ScaleLinesMajorStepValue = 400;
            this.gAGaugeFanExt.Center = new System.Drawing.Point(120, 100);
            this.gAGaugeFanInt.Center = new System.Drawing.Point(120, 100);
            this.gAGaugeFanExt.GaugeLabels.Add(new AGaugeLabel() { Position = new System.Drawing.Point(100, 130), Text = "RPM 0" });
            this.gAGaugeFanInt.GaugeLabels.Add(new AGaugeLabel() { Position = new System.Drawing.Point(100, 130), Text = "RPM 0" });
        }

        public static MonitorWindow Create(SerialPortChat portChat)
        {
            if (instance == null)
            {
                instance = new MonitorWindow();
            }
            instance.portChat = portChat;
            instance.portChat.RecivedAnswer += PortChat_RecivedAnswer;
            instance.WindowState = WindowState.Normal;
            instance.commandTimer = new Timer(new TimerCallback(SendCommand));
            instance.commandTimer.Change(500, 3000);
            return instance;
        }

        private static void PortChat_RecivedAnswer(byte[] obj)
        {
            try
            {
                switch (obj[2])
                {
                    case 7:
                        instance.MonitorGid.Dispatcher.Invoke(new Action(() => { instance.gAGaugeFanInt.Value = (float)(obj[3] / 255.0) * 4000;
                        instance.gAGaugeFanInt.GaugeLabels[0].Text = "RPM " + instance.gAGaugeFanInt.Value.ToString("f0");}));
                        break;
                    case 8:
                        instance.MonitorGid.Dispatcher.Invoke(new Action(() => { instance.gAGaugeFanExt.Value = (float)(obj[3] / 255.0) * 4000;
                        instance.gAGaugeFanExt.GaugeLabels[0].Text = "RPM " + instance.gAGaugeFanExt.Value.ToString("f0");}));
                break;
                    case 9:
                        instance.currentCondition.Dispatcher.Invoke(new Action(() => instance.currentCondition.Text = obj[3].ToString()));
                        break;
                    case 10:
                        instance.currentHeater.Dispatcher.Invoke(new Action(() => instance.currentHeater.Text = obj[3].ToString()));
                        break;
                    case 11:
                        instance.voltageHeater.Dispatcher.Invoke(new Action(() => instance.voltageHeater.Text = obj[3].ToString()));
                        break;
                    default:
                        break;
                }
            }
            catch (NullReferenceException ex)
            {

            }
        }

        private static void SendCommand(object state)
        {
            try
            {
                instance.portChat.WriteComand(new SerialCommand(new byte[] { 0xAA, 0x01, 0x07, 0x00 }, 4)); //Получить скорость вращения внутренних вентиляторов
                instance.portChat.WriteComand(new SerialCommand(new byte[] { 0xAA, 0x01, 0x08, 0x00 }, 4)); //Получить скорость вращения внешних вентиляторов
                instance.portChat.WriteComand(new SerialCommand(new byte[] { 0xAA, 0x01, 0x09, 0x00 }, 4)); //Получить ток кондиционера
                instance.portChat.WriteComand(new SerialCommand(new byte[] { 0xAA, 0x01, 0x0A, 0x00 }, 4)); //Получить ток нагревателя 
                instance.portChat.WriteComand(new SerialCommand(new byte[] { 0xAA, 0x01, 0x0B, 0x00 }, 4)); //Получить напряжение нагревателя
            }
            catch (NullReferenceException ex)
            {

            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            instance.portChat.RecivedAnswer -= PortChat_RecivedAnswer;            
            instance.commandTimer.Dispose();
            instance = null;
        }
    }
}
