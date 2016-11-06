using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Alarm
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        Stream outStream;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Debug.WriteLine("provo a connettermi");
                //Create a StreamSocketListener to start listening for TCP connections.
                Windows.Networking.Sockets.StreamSocketListener socketListener = new Windows.Networking.Sockets.StreamSocketListener();

                //Hook up an event handler to call when connections are received.
                socketListener.ConnectionReceived += SocketListener_ConnectionReceived;

                //Start listening for incoming TCP connections on the specified port. You can specify any port that' s not currently in use.
                await socketListener.BindServiceNameAsync("10000");
                Debug.WriteLine("connesso");
            }
            catch (Exception er)
            {
                Debug.WriteLine("ops...");
                Debug.WriteLine(er.Message);
            }
        }

        private void SocketListener_ConnectionReceived(Windows.Networking.Sockets.StreamSocketListener sender,
Windows.Networking.Sockets.StreamSocketListenerConnectionReceivedEventArgs args)
        {
            Debug.WriteLine("connessione ricevuta");

            //Read line from the remote client.
            Stream inStream = args.Socket.InputStream.AsStreamForRead();
            outStream = args.Socket.OutputStream.AsStreamForWrite();

            msgCount = 0;
            int byteCount = 0;
            BinaryReader reader = new BinaryReader(inStream);
            Boolean connOn = true;
            while (connOn)
            {
                try
                {
                    byte request = reader.ReadByte();
                    byteCount++;
                    string hexOutput = String.Format("{0:X2}", request);
                    Debug.Write(hexOutput);

                    //await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    //() =>
                    //{
                    //    textBlock.Text += hexOutput;
                    //});

                    if (byteCount == msgLenght[msgCount])
                    {
                        byteCount = 0;
                        msgSend();
                        if (msgCount < 3)
                        {
                            msgCount++;
                        }
                    }
                   

                }
                catch 
                {
                    Debug.WriteLine("Remote host closed connection");
                    connOn = false;
                }

            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ctrlAllarme));
        }

        private void msgSend()
        {
            Debug.WriteLine("---");
            byte[] byteArray = new byte[] { };
            BinaryWriter writer = new BinaryWriter(outStream);

            if (enterKeepalive)
            {
                Debug.WriteLine(keepalive[sendCounter]);
                byteArray = App.StringToByteArray(keepalive[sendCounter]);
                sendCounter++;
                if (sendCounter == keepalive.Length) { sendCounter = 0; }
            }
            else
            { 
                Debug.WriteLine(handshake[sendCounter]);              
                byteArray = App.StringToByteArray(handshake[sendCounter]);
                sendCounter++;
                if (sendCounter == handshake.Length) { enterKeepalive = true; sendCounter = 0; }
            }

            foreach (byte i in byteArray)
            {
                writer.Write(i);
            }
            writer.Flush();

        }



        int sendCounter = 0;
        string[] handshake = new string[]
        {
            "aa19000138f0000000eeeeeeeeeeeeee0042364132364435303741393533313032002001267101cc93eeeeeeeeeeeeee",
            "aa01000138f2000000eeeeeeeeeeeeee00eeeeeeeeeeeeeeeeeeeeeeeeeeeeee",
            "aa1100013af3000000eeeeeeeeeeeeee0100000000000000000000000000000000eeeeeeeeeeeeeeeeeeeeeeeeeeeeee",
            "aa2500023200000000eeeeeeeeeeeeee72ff04020001a41f01049400290bbc4109910206020b02140972fc57535036303030000001",
            "aa0100017af8000000eeeeeeeeeeeeee01eeeeeeeeeeeeeeeeeeeeeeeeeeeeee",
            "aa2500027200000000eeeeeeeeeeeeee000000001604940000000000000000000000000000000000000000000000000000000000ae",
            "aa2500027200000000eeeeeeeeeeeeee10000000000000000000000000000000000000000000000000000000000000000000000010",
            "aa2500027200000000eeeeeeeeeeeeeee21410081b0e0e300200000000000000000000000000000000000000000000020000000079",
            "aa2500027200000000eeeeeeeeeeeeee520000000000000c4e2c00000000000000000000706f72746120696e67726573736f2020c8",
            "aa2500027200000000eeeeeeeeeeeeee52000e52290bbc411fa4000000000000000100000000000000000000000000000000008027",
            "aa2500027200000000eeeeeeeeeeeeee52000ca8000000000000000000000000000000000000000000000000000000000000000006",
            "aa2500027200000000eeeeeeeeeeeeee52000cc8000000000000000000000000000000000000000000000000000000000000000026",
            "aa2500027200000000eeeeeeeeeeeeee52000ce8000000000000000000000000000000000000000000000000000000000000000046",
            "aa2500027200000000eeeeeeeeeeeeee52000d480000000000000000000000000000000000000000000000000000000000000000a7",
            "aa2500027200000000eeeeeeeeeeeeee52000f613f002b9300011b10e6710000000000000000000000000000000000000000000042",
            "aa2500027200000000eeeeeeeeeeeeee52000f810000000000000000000000000000000000000000000000000000000000000000e2",
            "aa2500027200000000eeeeeeeeeeeeee52000fa1000000000000000000000000000000000000000000000000000000000000000002",
            "aa2500027200000000eeeeeeeeeeeeee52000fc1000000000000000000000000000000000000000000000000000000000000000022",
            "aa2500027200000000eeeeeeeeeeeeee52000fe100000000000000000000000000000000000000000000001e1e1e1e1e1e1f1f1f53",
            "aa2500027200000000eeeeeeeeeeeeeec200250300080000000001000003a45b000000000001220001010800000000000000000022",
            "aa2500027200000000eeeeeeeeeeeeee5200800000000000001410081b0e0ed0999600fbfe0000000000000000000000000000002d",
            "aa2500027200000000eeeeeeeeeeeeee520080010000000000000000000000000000000000000000010000000000000000000000d4",
            "aa2500027200000000eeeeeeeeeeeeee52008002000000000000000000000000000000000080000000000000000000000000000054",
            "aa2500027200000000eeeeeeeeeeeeee520080030000000000000000000000000000000000000000000000000000000000000000d5",
            "aa2500027200000000eeeeeeeeeeeeee520080040000000000000000000000000000000000000000000000000000000000000000d6",
            "aa2500027200000000eeeeeeeeeeeeee520080050000000000000000000000000000000000000000000000000000000000000000d7"
        };

        string[] keepalive = new string[]
        {
            "aa2500027200000000eeeeeeeeeeeeee520080010000000000000000000000000000000000000000010000000000000000000000d4",
            "aa2500027200000000eeeeeeeeeeeeee52008002000000000000000000000000000000000080000000000000000000000000000054",
            "aa2500027200000000eeeeeeeeeeeeee520080030000000000000000000000000000000000000000000000000000000000000000d5",
            "aa2500027200000000eeeeeeeeeeeeee520080040000000000000000000000000000000000000000000000000000000000000000d6",
            "aa2500027200000000eeeeeeeeeeeeee520080050000000000000000000000000000000000000000000000000000000000000000d7",
            "aa2500027200000000eeeeeeeeeeeeee52008006010000000000000000000000000080500080500080500000000000000000000049",
            "aa2500027200000000eeeeeeeeeeeeee5200800000000000001410081b0e0ed1999600000000000000000000000000000000000035"
        };

        int[] msgLenght = new int[] { 32, 16, 16, 64 };

        bool enterKeepalive = false;

        int msgCount = 0;
        
        private void chkActive(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            int sensor = Convert.ToInt32(checkbox.Name.Substring(7));
            byte[] ctrlBytes = App.StringToByteArray(keepalive[6]);
            if (sensor < 1000)
            {
                ctrlBytes[35] += Convert.ToByte(sensor);
            }
            else if (sensor < 2000)
            {
                ctrlBytes[36] += Convert.ToByte(sensor-1000);
            }
            else
            {
                ctrlBytes[37] += Convert.ToByte(sensor - 2000);
            }
            keepalive[6] = App.formatMessage(ctrlBytes);
        }

        private void chkInactive(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            int sensor = Convert.ToInt32(checkbox.Name.Substring(7));
            byte[] ctrlBytes = App.StringToByteArray(keepalive[6]);
            if (sensor < 1000)
            {
                ctrlBytes[35] -= Convert.ToByte(sensor);
            }
            else if (sensor < 2000)
            {
                ctrlBytes[36] -= Convert.ToByte(sensor - 1000);
            }
            else
            {
                ctrlBytes[37] -= Convert.ToByte(sensor - 2000);
            }
            keepalive[6] = App.formatMessage(ctrlBytes);
        }

        private void txtUnknown_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            int unknownByte = Convert.ToInt32(textbox.Name.Substring(textbox.Name.Length - 2)); //the textBox number that has changed
            
            //check that input is only Hex chars
            int n = 0;
            if (!int.TryParse(textbox.Text, System.Globalization.NumberStyles.HexNumber, System.Globalization.NumberFormatInfo.CurrentInfo, out n) &&
              textbox.Text != String.Empty)
            {
                textbox.Text = textbox.Text.Remove(textbox.Text.Length - 1, 1);
                textbox.SelectionStart = textbox.Text.Length;
            }
        
            //If a valid Hex byte is inserted modify the status string
            if (textbox.Text.Length == 2)
            {
                byte[] ctrlBytes = App.StringToByteArray(keepalive[0]);
                
                //Select byte to modify based on which textBox has been changed
                ctrlBytes[unknownByte] = App.StringToByteArray(textbox.Text)[0]; 
                
                //set new values in status string
                keepalive[0] = App.formatMessage(ctrlBytes); 

            }
        }
    }
}
