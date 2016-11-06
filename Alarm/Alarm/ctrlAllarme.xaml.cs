using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Alarm
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ctrlAllarme : Page
    {
        Windows.Networking.Sockets.StreamSocket socket = new Windows.Networking.Sockets.StreamSocket();
        public ctrlAllarme()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.btnConnect.Label == "Connect")
            {
                try
                {
                                     
                    Windows.Networking.HostName serverHost = new Windows.Networking.HostName(txtIP.Text);

                    string serverPort = txtPort.Text;
                    await socket.ConnectAsync(serverHost, serverPort);

                    Stream streamOut = socket.OutputStream.AsStreamForWrite();
                    BinaryWriter writer = new BinaryWriter(streamOut);
                    Stream streamIn = socket.InputStream.AsStreamForRead();
                    BinaryReader reader = new BinaryReader(streamIn);

                    Boolean connOn = true;
                    int countLimit = handshake.Length + zonelabels.Length;

                    handshake[0] = App.createLoginString(this.txtPassword.Password);
                    int[] fixedLenght = new int[] { 53 };

                    this.txtEvent.Text = "Connecting...";
                    this.txtStatus.Text = "Status: Connecting...";
                    await comms(reader, writer, handshake, answerLenghtArray, 0);
                    this.txtEvent.Text = "Retrieving Zone Labels...";
                    this.txtStatus.Text = "Status: Connected";
                    this.btnConnect.Label = "Disconnect";
                    await comms(reader, writer, zonelabels, fixedLenght, 2);
                    int i = 1;
                    foreach (CheckBox item in this.gridView.Items)
                    {
                        item.Content = Mappings.zoneLabel[i];
                        i++;
                    }


                    while (connOn)
                    {
                        await comms(reader, writer, retrieveInfo, fixedLenght, 1000);
                    }

                    
                }
                catch (Exception er)
                {
                    Debug.WriteLine("ops...");
                    Debug.WriteLine(er.Message);

                }
            }
            else
            {
                socket.Dispose();
            }
        }

        private void txtUnknown_TextChanged(object sender, TextChangedEventArgs e)
        { }

        public async Task comms(BinaryReader reader, BinaryWriter writer, string[] messageArray, int[] loginAnswersLenght, int timing)
        {
            int msgCount = 0;
            foreach (string message in messageArray)
            {
                //Write data to the server
                byte[] byteArray = App.StringToByteArray(message);

                foreach (byte item in byteArray)
                {
                    writer.Write(item);
                }
                writer.Flush();

                Debug.WriteLine(message);
                Debug.WriteLine("---");
                
                //Read data from the server.
                int lunghezza = (msgCount < loginAnswersLenght.Length)? loginAnswersLenght[msgCount]: loginAnswersLenght[loginAnswersLenght.Length - 1];
                int currentByte = 0;
                byte[] response = new byte[lunghezza];

                while (currentByte < lunghezza)
                {
                    response[currentByte] = reader.ReadByte();
                    string hexOutput = String.Format("{0:X2}", response[currentByte]);
                    Debug.Write(hexOutput);
                    currentByte++;
                }
                Debug.WriteLine(".");
                if (timing > 0) { await interpretAnswerAsync(response, timing); }
                msgCount++;
            }
        }

        public async Task interpretAnswerAsync(byte[] response, int timing)
        {
            if (response[16] == 0x52)  //This is a status response string
            {
                switch (response[19])
                {
                    case 0x01:  //This string has Alarm State info
                        if (response[33] == 0x00)
                        { this.txtUnknown34.Text = "Disarmato"; }
                        else if (response[33] == 0x01)
                        { this.txtUnknown34.Text = "Totale"; }
                        else if (response[33] == 0x02 || response[32] == 0x03 || response[32] == 0x06)
                        { this.txtUnknown34.Text = "Notte"; }
                        else if (response[33] == 0x04 || response[32] == 0x05)
                        { this.txtUnknown34.Text = "Perimetrale"; }
                        else if (response[33] == 0x08 || response[32] == 0x09)
                        { this.txtUnknown34.Text = "Instant Armed"; }
                        else if (response[33] > 0x10)
                        { this.txtUnknown34.Text = "IN ALLARME!"; }
                        break;
                    case 0x00: //This string has date+time+power+sensor info
                        //get date&time info
                        this.txtYear.Text = Convert.ToString(response[25]) + Convert.ToString(response[26]);
                        this.txtMonth.Text = Convert.ToString(response[27]);
                        this.txtDay.Text = Convert.ToString(response[28]);
                        this.txtHours.Text = Convert.ToString(response[29]);
                        this.txtMinutes.Text = Convert.ToString(response[30]);
                        
                        //get power & battery info
                        double percentageVDC = ((Convert.ToDouble(response[31]) / 255) * 18.9) + 1.4; //VDC Voltage (FF= 20.3 - 00= 1.4 - 80= 10.9)
                        this.txtUnknown31.Text = percentageVDC.ToString();

                        double percentageDC = ((Convert.ToDouble(response[32]) / 255) * 22.8); //DC Voltage (FF= 22.8 - 00= 0.0 - 80= 11.5)
                        this.txtUnknown32.Text = percentageDC.ToString();

                        double percentageBatt = ((Convert.ToDouble(response[33]) / 255) * 22.8); //Battery Voltage (FF= 22.8 - 00= 0.0 - 80= 11.5)
                        this.txtUnknown33.Text = percentageBatt.ToString();

                        //get zone sensor status
                        int i = 0;
                        foreach (CheckBox item in this.gridView.Items)
                        {
                            if (i < 8) { item.IsChecked = (response[35] & Sensors[i]) == Sensors[i]; }
                            else if (i < 16) { item.IsChecked = (response[36] & Sensors[i-8]) == Sensors[i-8]; }
                            else if (i < 24) { item.IsChecked = (response[37] & Sensors[i-16]) == Sensors[i-16]; }
                            else if (i < 32) { item.IsChecked = (response[38] & Sensors[i-24]) == Sensors[i-24]; }
                            i++;
                        }
                        break;
                    
                    //Zone labels retrieving
                    case 0x10:
                        if (response[18] == 0x00)
                        { Mappings.zoneLabel[1] = App.retrieveZoneLabel(1, response); Mappings.zoneLabel[2] = App.retrieveZoneLabel(2, response); }
                        else if (response[18] == 0x01)
                        { Mappings.zoneLabel[17] = App.retrieveZoneLabel(1, response); Mappings.zoneLabel[18] = App.retrieveZoneLabel(2, response); }
                        break;
                    case 0x30:
                        if (response[18] == 0x00)
                        { Mappings.zoneLabel[3] = App.retrieveZoneLabel(1, response); Mappings.zoneLabel[4] = App.retrieveZoneLabel(2, response); }
                        else if (response[18] == 0x01)
                        { Mappings.zoneLabel[19] = App.retrieveZoneLabel(1, response); Mappings.zoneLabel[20] = App.retrieveZoneLabel(2, response); }
                        break;
                    case 0x50:
                        if (response[18] == 0x00)
                        { Mappings.zoneLabel[5] = App.retrieveZoneLabel(1, response); Mappings.zoneLabel[6] = App.retrieveZoneLabel(2, response); }
                        else if (response[18] == 0x01)
                        { Mappings.zoneLabel[21] = App.retrieveZoneLabel(1, response); Mappings.zoneLabel[22] = App.retrieveZoneLabel(2, response); }
                        break;
                    case 0x70:
                        if (response[18] == 0x00)
                        { Mappings.zoneLabel[7] = App.retrieveZoneLabel(1, response); Mappings.zoneLabel[8] = App.retrieveZoneLabel(2, response); }
                        else if (response[18] == 0x01)
                        { Mappings.zoneLabel[23] = App.retrieveZoneLabel(1, response); Mappings.zoneLabel[24] = App.retrieveZoneLabel(2, response); }
                        break;
                    case 0x90:
                        if (response[18] == 0x00)
                        { Mappings.zoneLabel[9] = App.retrieveZoneLabel(1, response); Mappings.zoneLabel[10] = App.retrieveZoneLabel(2, response); }
                        else if (response[18] == 0x01)
                        { Mappings.zoneLabel[25] = App.retrieveZoneLabel(1, response); Mappings.zoneLabel[26] = App.retrieveZoneLabel(2, response); }
                        break;
                    case 0xb0:
                        if (response[18] == 0x00)
                        { Mappings.zoneLabel[11] = App.retrieveZoneLabel(1, response); Mappings.zoneLabel[12] = App.retrieveZoneLabel(2, response); }
                        else if (response[18] == 0x01)
                        { Mappings.zoneLabel[27] = App.retrieveZoneLabel(1, response); Mappings.zoneLabel[28] = App.retrieveZoneLabel(2, response); }
                        break;
                    case 0xd0:
                        if (response[18] == 0x00)
                        { Mappings.zoneLabel[13] = App.retrieveZoneLabel(1, response); Mappings.zoneLabel[14] = App.retrieveZoneLabel(2, response); }
                        else if (response[18] == 0x01)
                        { Mappings.zoneLabel[29] = App.retrieveZoneLabel(1, response); Mappings.zoneLabel[30] = App.retrieveZoneLabel(2, response); }
                        break;
                    case 0xf0:
                        if (response[18] == 0x00)
                        { Mappings.zoneLabel[15] = App.retrieveZoneLabel(1, response); Mappings.zoneLabel[16] = App.retrieveZoneLabel(2, response); }
                        else if (response[18] == 0x01)
                        { Mappings.zoneLabel[31] = App.retrieveZoneLabel(1, response); Mappings.zoneLabel[32] = App.retrieveZoneLabel(2, response); }
                        break;

                    default:
                        break;
                }
            }
            else if (response[16] == 0xe2) //This is an Event response string
            {
                this.txtEvent.Text = "Ultimo evento: \n";
                //retrieve Event Date&Time
                this.txtEvent.Text += Convert.ToString(response[17]) + Convert.ToString(response[18]) + "-";
                this.txtEvent.Text += Convert.ToString(response[19])+"-";
                this.txtEvent.Text += Convert.ToString(response[20])+" ";
                this.txtEvent.Text += Convert.ToString(response[21])+":";
                this.txtEvent.Text += Convert.ToString(response[22])+"\n";

                //retrieve event type
                string evento = "";
                if (Mappings.eventMap.TryGetValue(response[23], out evento))
                { this.txtEvent.Text += evento + "\n"; }
                else
                { this.txtEvent.Text += "non identificato\n"; }

                //retrieve event subtype
                string subEvType = "";
                if (Mappings.subEventType.TryGetValue(response[23], out subEvType))
                {
                    Dictionary<int, string> subEventSelected = new Dictionary<int, string>();

                    switch (subEvType)
                    {
                        case "zoneLabel":
                            subEventSelected = Mappings.zoneLabel;
                            break;
                        case "partitionStatus":
                            subEventSelected = Mappings.partitionStatus;
                            break;
                        case "bellStatus":
                            subEventSelected = Mappings.bellStatus;
                            break;
                        case "nonReportableEvents":
                            subEventSelected = Mappings.nonReportableEvents;
                            break;
                        case "remoteLabel":
                            subEventSelected = Mappings.remoteLabel;
                            break;
                        case "eventOpt1":
                            subEventSelected = Mappings.eventOpt1;
                            break;
                        case "softwareAccess":
                            subEventSelected = Mappings.softwareAccess;
                            break;
                        case "busModuleEvent":
                            subEventSelected = Mappings.busModuleEvent;
                            break;
                        case "userLabel":
                            subEventSelected = Mappings.userLabel;
                            break;
                        case "specialArming":
                            subEventSelected = Mappings.specialArming;
                            break;
                        case "specialDisarming":
                            subEventSelected = Mappings.specialDisarming;
                            break;
                        case "specialAlarm":
                            subEventSelected = Mappings.specialAlarm;
                            break;
                        case "newTrouble":
                            subEventSelected = Mappings.newTrouble;
                            break;
                        case "troubleRestored":
                            subEventSelected = Mappings.troubleRestored;
                            break;
                        case "moduleTrouble":
                            subEventSelected = Mappings.moduleTrouble;
                            break;
                        case "moduleTroubleRestore":
                            subEventSelected = Mappings.moduleTroubleRestore;
                            break;
                        case "special":
                            subEventSelected = Mappings.special;
                            break;
                        case "systemStatus":
                            subEventSelected = Mappings.systemStatus;
                            break;
                        default:
                            this.txtEvent.Text += "sottotipo non identificato\n";
                            break;
                    }

                    //retrieve subEvent #
                    this.txtEvent.Text += "SubEvent: " + Convert.ToString(response[24]) + "\n";

                    //retrieve subEvent Label
                    string subEvLabel = "";
                    if (subEventSelected.TryGetValue(response[24], out subEvLabel))
                    { this.txtEvent.Text += subEvLabel + "\n"; }
                    else
                    { this.txtEvent.Text += "SubEv Label not found\n"; }
                }
                else
                { this.txtEvent.Text += "sottotipo non identificato\n"; }

                //retrieve additional encoded label
                string sensorLbl = "";
                for (int i = 31; i < 45; i++)
                {
                    sensorLbl += Convert.ToChar(response[i]);
                }
                this.txtEvent.Text += sensorLbl;
            }

            await Task.Delay(TimeSpan.FromMilliseconds(timing));
        }

        byte[] Sensors = new byte[] { 1, 2, 4, 8, 16, 32, 64, 128 }; //bitmask for sensor decoding

        //handshake connection strings - first element is dynamically created based on password
        //this sequence is always constant
        string[] handshake = new string[]
        {
            "This is going to be replaced by the password login string",
            "aa00000308f2000aeeeeeeeeeeeeeeee",
            "aa00000308f3000aeeeeeeeeeeeeeeee",
            "aa2500040800000aeeeeeeeeeeeeeeee72000000000000000000000000000000000000000000000000000000000000000000000072eeeeeeeeeeeeeeeeeeeeee",
            "aa26000308f8000aeeeeeeeeeeeeeeee0a500080000000000000000000000000000000000000000000000000000000000000000000d0eeeeeeeeeeeeeeeeeeee",
            "aa2500040800000aeeeeeeeeeeeeeeee5f20000000000000000000000000000000000000000000000000000000000000000000007feeeeeeeeeeeeeeeeeeeeee",
            "aa25000408000014eeeeeeeeeeeeeeee000000001604940000000000190000000000000000000000000000000000000000020000c9eeeeeeeeeeeeeeeeeeeeee",
            "",
            "aa25000408000014eeeeeeeeeeeeeeee50000000000000000000000000000000000000000000000000000000000000000000000050eeeeeeeeeeeeeeeeeeeeee",
            "aa25000408000014eeeeeeeeeeeeeeee50000e520000000000000000000000000000000000000000000000000000000000000000b0eeeeeeeeeeeeeeeeeeeeee"
        };

        //retrieve zone labels
        string[] zonelabels = new string[]
        {
            "aa25000408000014eeeeeeeeeeeeeeee50000010000000000000000000000000000000000000000000000000000000000000000060eeeeeeeeeeeeeeeeeeeeee",
            "aa25000408000014eeeeeeeeeeeeeeee50000030000000000000000000000000000000000000000000000000000000000000000080eeeeeeeeeeeeeeeeeeeeee",
            "aa25000408000014eeeeeeeeeeeeeeee500000500000000000000000000000000000000000000000000000000000000000000000a0eeeeeeeeeeeeeeeeeeeeee",
            "aa25000408000014eeeeeeeeeeeeeeee500000700000000000000000000000000000000000000000000000000000000000000000c0eeeeeeeeeeeeeeeeeeeeee",
            "aa25000408000014eeeeeeeeeeeeeeee500000900000000000000000000000000000000000000000000000000000000000000000e0eeeeeeeeeeeeeeeeeeeeee",
            "aa25000408000014eeeeeeeeeeeeeeee500000b0000000000000000000000000000000000000000000000000000000000000000000eeeeeeeeeeeeeeeeeeeeee",
            "aa25000408000014eeeeeeeeeeeeeeee500000d0000000000000000000000000000000000000000000000000000000000000000020eeeeeeeeeeeeeeeeeeeeee",
            "aa25000408000014eeeeeeeeeeeeeeee500000f0000000000000000000000000000000000000000000000000000000000000000040eeeeeeeeeeeeeeeeeeeeee",
            "aa25000408000014eeeeeeeeeeeeeeee50000110000000000000000000000000000000000000000000000000000000000000000061eeeeeeeeeeeeeeeeeeeeee",
            "aa25000408000014eeeeeeeeeeeeeeee50000130000000000000000000000000000000000000000000000000000000000000000081eeeeeeeeeeeeeeeeeeeeee",
            "aa25000408000014eeeeeeeeeeeeeeee500001500000000000000000000000000000000000000000000000000000000000000000a1eeeeeeeeeeeeeeeeeeeeee",
            "aa25000408000014eeeeeeeeeeeeeeee500001700000000000000000000000000000000000000000000000000000000000000000c1eeeeeeeeeeeeeeeeeeeeee",
            "aa25000408000014eeeeeeeeeeeeeeee500001900000000000000000000000000000000000000000000000000000000000000000e1eeeeeeeeeeeeeeeeeeeeee",
            "aa25000408000014eeeeeeeeeeeeeeee500001b0000000000000000000000000000000000000000000000000000000000000000001eeeeeeeeeeeeeeeeeeeeee",
            "aa25000408000014eeeeeeeeeeeeeeee500001d0000000000000000000000000000000000000000000000000000000000000000021eeeeeeeeeeeeeeeeeeeeee",
            "aa25000408000014eeeeeeeeeeeeeeee500001f0000000000000000000000000000000000000000000000000000000000000000041eeeeeeeeeeeeeeeeeeeeee"
        };

        //keepalive connection - first element requests Alarm status, second element requests Date&Time&Power&Zone info
        String[] retrieveInfo = new string[]
        {
            "aa25000408000014eeeeeeeeeeeeeeee500080000000000000000000000000000000000000000000000000000000000000000000d0eeeeeeeeeeeeeeeeeeeeee",
            "aa25000408000014eeeeeeeeeeeeeeee500080010000000000000000000000000000000000000000000000000000000000000000d1eeeeeeeeeeeeeeeeeeeeee"
        };

        int[] answerLenghtArray = new int[] { 48, 32, 48, 53, 32, 53 };

    }
}
