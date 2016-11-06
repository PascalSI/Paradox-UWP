using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarm
{
    class Mappings
    {
        public static Dictionary<int,string> eventMap = new Dictionary<int, string>()
        {
            { 0, "Zone OK" },
            { 1, "Zone open" },
            { 2, "Partition status" },
            { 3, "Bell status (Partition 1)" },
            { 5, "Non-Reportable Event" },
            { 6, "Non-reportable event" },
            { 7, "PGM Activation" },
            { 8, "Button B pressed on remote" },
            { 9, "Button C pressed on remote" },
            { 10, "Button D pressed on remote" },
            { 11, "Button E pressed on remote" },
            { 12, "Cold start wireless zone" },
            { 13, "Cold start wireless module (Partition 1)" },
            { 14, "Bypass programming" },
            { 15, "User code activated output (Partition 1)" },
            { 16, "Wireless smoke maintenance signal" },
            { 17, "Delay zone alarm transmission" },
            { 18, "Zone signal strength weak 1 (Partition 1)" },
            { 19, "Zone signal strength weak 2 (Partition 1)" },
            { 20, "Zone signal strength weak 3 (Partition 1)" },
            { 21, "Zone signal strength weak 4 (Partition 1)" },
            { 22, "Button 5 pressed on remote" },
            { 23, "Button 6 pressed on remote" },
            { 24, "Fire delay started" },
            { 25, "N/A" },
            { 26, "Software access" },
            { 27, "Bus module event" },
            { 28, "StayD pass acknowledged" },
            { 29, "Arming with user" },
            { 30, "Special arming" },
            { 31, "Disarming with user" },
            { 32, "Disarming after alarm with user" },
            { 33, "Alarm cancelled with user" },
            { 34, "Special disarming" },
            { 35, "Zone bypassed" },
            { 36, "Zone in alarm" },
            { 37, "Fire alarm" },
            { 38, "Zone alarm restore" },
            { 39, "Fire alarm restore" },
            { 40, "Special alarm" },
            { 41, "Zone shutdown" },
            { 42, "Zone tampered" },
            { 43, "Zone tamper restore" },
            { 44, "New trouble (Partition 1, both for sub event 7" },
            { 45, "Trouble restored " },
            { 46, "Bus / EBus / Wireless module new trouble (Partition 1)" },
            { 47, "Bus / EBus / Wireless module trouble restored (Partition 1)" },
            { 48, "Special (Partition 1)" },
            { 49, "Low battery on zone" },
            { 50, "Low battery on zone restore" },
            { 51, "Zone supervision trouble" },
            { 52, "Zone supervision restore" },
            { 53, "Wireless module supervision trouble (Partition 1)" },
            { 54, "Wireless module supervision restore (Partition 1)" },
            { 55, "Wireless module tamper trouble (Partition 1)" },
            { 56, "Wireless module tamper restore (Partition 1)" },
            { 57, "Non-medical alarm (paramedic)" },
            { 58, "Zone forced" },
            { 59, "Zone included" },
            { 64, "System Status"}
        };

        public static Dictionary<int, string> subEventType = new Dictionary<int, string>()
        {
            {0, "zoneLabel" },
            {1, "zoneLabel"},
            {2, "partitionStatus"},
            {3, "bellStatus"},
            {6, "nonReportableEvents"},
            {8, "remoteLabel"},
            {9, "remoteLabel"},
            {10, "remoteLabel"},
            {11, "remoteLabel"},
            {12, "zoneLabel"},
            {13, "eventOpt1"},
            {14, "zoneLabel"},
            {15, "zoneLabel"},
            {16, "zoneLabel"},
            {17, "zoneLabel"},
            {18, "zoneLabel"},
            {19, "zoneLabel"},
            {20, "zoneLabel"},
            {21, "zoneLabel"},
            {22, "remoteLabel"},
            {23, "remoteLabel"},
            {24, "zoneLabel"},
            {25, "zoneLabel"},
            {26, "softwareAccess"},
            {27, "busModuleEvent"},
            {28, "zoneLabel"},
            {29, "userLabel"},
            {30, "specialArming"},
            {31, "userLabel"},
            {32, "userLabel"},
            {33, "userLabel"},
            {34, "specialDisarming"},
            {35, "zoneLabel"},
            {36, "zoneLabel"},
            {37, "zoneLabel"},
            {38, "zoneLabel"},
            {39, "zoneLabel"},
            {40, "specialAlarm"},
            {41, "zoneLabel"},
            {42, "zoneLabel"},
            {43, "zoneLabel"},
            {44, "newTrouble"},
            {45, "troubleRestored"},
            {46, "moduleTrouble"},
            {47, "moduleTroubleRestore"},
            {48, "special"},
            {49, "zoneLabel"},
            {50, "zoneLabel"},
            {51, "zoneLabel"},
            {52, "zoneLabel"},
            {53, "eventOpt1"},
            {54, "eventOpt1"},
            {55, "eventOpt1"},
            {56, "eventOpt1"},
            {57, "userLabel"},
            {58, "zoneLabel"},
            {59, "zoneLabel"},
            {64, "systemStatus"}
        };

        public static Dictionary<int, string> partitionStatus = new Dictionary<int, string>()
        {
            { 0, "N/A" },
            { 1, "N/A" },
            { 2, "Silent alarm" },
            { 3, "Buzzer alarm" },
            { 4, "Steady alarm" },
            { 5, "Pulse alarm" },
            { 6, "Strobe" },
            { 7, "Alarm stopped" },
            { 8, "Squawk ON (Partition 1)" },
            { 9, "Squawk OFF (Partition 1)" },
            { 10, "Ground Start (Partition 1)" },
            { 11, "Disarm partition" },
            { 12, "Arm partition" },
            { 13, "Entry delay started" },
            { 14, "Exit delay started" },
            { 15, "Pre-alarm delay" },
            { 16, "Report confirmation" },
            { 99, "Any partition status event" }
        };

        public static Dictionary<int, string> zoneLabel = new Dictionary<int, string>()
        {
            { 1, "Zone 1" },
            { 2, "Zone 2" },
            { 3, "Zone 3" },
            { 4, "Zone 4" },
            { 5, "Zone 5" },
            { 6, "Zone 6" },
            { 7, "Zone 7" },
            { 8, "Zone 8" },
            { 9, "Zone 9" },
            { 10, "Zone 10" },
            { 11, "Zone 11" },
            { 12, "Zone 12" },
            { 13, "Zone 13" },
            { 14, "Zone 14" },
            { 15, "Zone 15" },
            { 16, "Zone 16" },
            { 17, "Zone 17" },
            { 18, "Zone 18" },
            { 19, "Zone 19" },
            { 20, "Zone 20" },
            { 21, "Zone 21" },
            { 22, "Zone 22" },
            { 23, "Zone 23" },
            { 24, "Zone 24" },
            { 25, "Zone 25" },
            { 26, "Zone 26" },
            { 27, "Zone 27" },
            { 28, "Zone 28" },
            { 29, "Zone 29" },
            { 30, "Zone 30" },
            { 31, "Zone 31" },
            { 32, "Zone 32" }
        };

        public static Dictionary<int, string> bellStatus = new Dictionary<int, string>()
        {
            { 0, " Bell OFF" },
            { 1, " Bell ON" },
            { 2, " Bell squawk arm" },
            { 3, " Bell squawk disarm" },
            { 99, "Any bell status event" }
        };

        public static Dictionary<int, string> nonReportableEvents = new Dictionary<int, string>()
        {
            { 0, "Telephone line trouble" },
            { 1, "[ENTER]/[CLEAR]/[POWER] key was pressed (Partition 1 only)" },
            { 2, "N/A" },
            { 3, "Arm in stay mode" },
            { 4, "Arm in sleep mode" },
            { 5, "Arm in force mode" },
            { 6, "Full arm when armed in stay mode" },
            { 7, "PC fail to communicate (Partition 1)" },
            { 8, "Utility Key 1 pressed (keys [1] and [2]) (Partition 1)" },
            { 9, "Utility Key 2 pressed (keys [4] and [5]) (Partition 1)" },
            { 10, "Utility Key 3 pressed (keys [7] and [8]) (Partition 1)" },
            { 11, "Utility Key 4 pressed (keys [2] and [3]) (Partition 1)" },
            { 12, "Utility Key 5 pressed (keys [5] and [6]) (Partition 1)" },
            { 13, "Utility Key 6 pressed (keys [8] and [9]) (Partition 1)" },
            { 14, "Tamper generated alarm" },
            { 15, "Supervision loss generated alarm" },
            { 16, "N/A" },
            { 17, "N/A" },
            { 18, "N/A" },
            { 19, "N/A" },
            { 20, "Full arm when armed in sleep mode" },
            { 21, "Firmware upgrade -Partition 1 only (non-PGM event)" },
            { 22, "N/A" },
            { 23, "StayD mode activated" },
            { 24, "StayD mode deactivated" },
            { 25, "IP Registration status change" },
            { 26, "GPRS Registration status change" },
            { 99, "Any non-reportable event" }
        };

        public static Dictionary<int, string> userLabel = new Dictionary<int, string>()
        {
            { 1, "User Number 1" },
            { 2, "User Number 2" },
            { 3, "User Number 3" },
            { 4, "User Number 4" },
            { 5, "User Number 5" },
            { 6, "User Number 6" },
            { 7, "User Number 7" },
            { 8, "User Number 8" },
            { 9, "User Number 9" },
            { 10, "User Number 10" },
            { 11, "User Number 11" },
            { 12, "User Number 12" },
            { 13, "User Number 13" },
            { 14, "User Number 14" },
            { 15, "User Number 15" },
            { 16, "User Number 16" },
            { 17, "User Number 17" },
            { 18, "User Number 18" },
            { 19, "User Number 19" },
            { 20, "User Number 20" },
            { 21, "User Number 21" },
            { 22, "User Number 22" },
            { 23, "User Number 23" },
            { 24, "User Number 24" },
            { 25, "User Number 25" },
            { 26, "User Number 26" },
            { 27, "User Number 27" },
            { 28, "User Number 28" },
            { 29, "User Number 29" },
            { 30, "User Number 30" },
            { 31, "User Number 31" },
            { 32, "User Number 32" }
        };

        public static Dictionary<int, string> remoteLabel = new Dictionary<int, string>()
        {
            { 1, "Remote Control Number 1" },
            { 2, "Remote Control Number 2" },
            { 3, "Remote Control Number 3" },
            { 4, "Remote Control Number 4" },
            { 5, "Remote Control Number 5" },
            { 6, "Remote Control Number 6" },
            { 7, "Remote Control Number 7" },
            { 8, "Remote Control Number 8" },
            { 9, "Remote Control Number 9" },
            { 10, "Remote Control Number 10" },
            { 11, "Remote Control Number 11" },
            { 12, "Remote Control Number 12" },
            { 13, "Remote Control Number 13" },
            { 14, "Remote Control Number 14" },
            { 15, "Remote Control Number 15" },
            { 16, "Remote Control Number 16" },
            { 17, "Remote Control Number 17" },
            { 18, "Remote Control Number 18" },
            { 19, "Remote Control Number 19" },
            { 20, "Remote Control Number 20" },
            { 21, "Remote Control Number 21" },
            { 22, "Remote Control Number 22" },
            { 23, "Remote Control Number 23" },
            { 24, "Remote Control Number 24" },
            { 25, "Remote Control Number 25" },
            { 26, "Remote Control Number 26" },
            { 27, "Remote Control Number 27" },
            { 28, "Remote Control Number 28" },
            { 29, "Remote Control Number 29" },
            { 30, "Remote Control Number 30" },
            { 31, "Remote Control Number 31" },
            { 32, "Remote Control Number 32" },
            { 99, "Any remote control number" }
        };

        public static Dictionary<int, string> specialArming = new Dictionary<int, string>()
        {
            { 0, "Auto-arming (on time/no movement)" },
            { 1, "Late to close" },
            { 2, "No movement arming" },
            { 3, "Partial arming" },
            { 4, "Quick arming" },
            { 5, "Arming through WinLoad" },
            { 6, "Arming with keyswitch" },
            { 99, "Any special arming" }
        };

        public static Dictionary<int, string> specialDisarming = new Dictionary<int, string>()
        {
            { 0, "Auto-arm cancelled (on time/no movement)" },
            { 1, "Disarming through WinLoad" },
            { 2, "Disarming through WinLoad after alarm" },
            { 3, "Alarm cancelled through WinLoad" },
            { 4, "Paramedical alarm cancelled" },
            { 5, "Disarm with keyswitch" },
            { 6, "Disarm with keyswitch after an alarm" },
            { 7, "Alarm cancelled with keyswitch" },
            { 99, "Any special disarming" }
        };

        public static Dictionary<int, string> specialAlarm = new Dictionary<int, string>()
        {
            { 0, "Panic non-medical emergency" },
            { 1, "Panic medical" },
            { 2, "Panic fire" },
            { 3, "Recent closing" },
            { 4, "Global shutdown" },
            { 5, "Duress alarm" },
            { 6, "Keypad lockout (Partition 1)" },
            { 99, "Any special alarm event" }
        };

        public static Dictionary<int, string> newTrouble = new Dictionary<int, string>()
        {
            { 0, "N/A" },
            { 1, "AC failure" },
            { 2, "Battery failure" },
            { 3, "Auxiliary current overload" },
            { 4, "Bell current overload" },
            { 5, "Bell disconnected" },
            { 6, "Clock loss" },
            { 7, "Fire loop trouble" },
            { 8, "Fail to communicate to monitoring station telephone #1" },
            { 9, "Fail to communicate to monitoring station telephone #2" },
            { 11, "Fail to communicate to voice report" },
            { 12, "RF jamming" },
            { 13, "GSM RF jamming" },
            { 14, "GSM no service" },
            { 15, "GSM supervision lost" },
            { 16, "Fail To Communicate IP Receiver 1 (GPRS)" },
            { 17, "Fail To Communicate IP Receiver 2 (GPRS)" },
            { 18, "IP Module No Service" },
            { 19, "IP Module Supervision Loss" },
            { 20, "Fail To Communicate IP Receiver 1 (IP)" },
            { 21, "Fail To Communicate IP Receiver 2 (IP)" },
            { 99, "Any new trouble event" }
        };

        public static Dictionary<int, string> troubleRestored = new Dictionary<int, string>()
        {
            { 0, "Telephone line restore" },
            { 1, "AC failure restore" },
            { 2, "Battery failure restore" },
            { 3, "Auxiliary current overload restore" },
            { 4, "Bell current overload restore" },
            { 5, "Bell disconnected restore" },
            { 6, "Clock loss restore" },
            { 7, "Fire loop trouble restore" },
            { 8, "Fail to communicate to monitoring station telephone #1 restore" },
            { 9, "Fail to communicate to monitoring station telephone #2 restore" },
            { 11, "Fail to communicate to voice report restore" },
            { 12, "RF jamming restore" },
            { 13, "GSM RF jamming restore" },
            { 14, "GSM no service restore" },
            { 15, "GSM supervision lost restore" },
            { 16, "Fail To Communicate IP Receiver 1 (GPRS) restore" },
            { 17, "Fail To Communicate IP Receiver 2 (GPRS) restore" },
            { 18, "IP Module No Service restore" },
            { 19, "IP Module Supervision Loss restore" },
            { 20, "Fail To Communicate IP Receiver 1 (IP) restore" },
            { 21, "Fail To Communicate IP Receiver 2 (IP) restore" },
            { 99, "Any trouble event restore" }
        };

        public static Dictionary<int, string> softwareAccess = new Dictionary<int, string>
        {
            { 0, "Non-valid source ID" },
            { 1, "WinLoad direct" },
            { 2, "WinLoad through IP module" },
            { 3, "WinLoad through GSM module" },
            { 4, "WinLoad through modem" },
            { 9, "IP150 direct" },
            { 10, "VDMP3 direct" },
            { 11, "Voice through GSM module" },
            { 12, "Remote access" },
            { 13, "SMS through GSM module" },
            { 99, "Any software access" }
        };

        public static Dictionary<int, string> busModuleEvent = new Dictionary<int, string>()
        {
            { 0, "A bus module was added" },
            { 1, "A bus module was removed" },
            { 2, "2-way RF Module Communication Failure" },
            { 3, "2-way RF Module Communication Restored" }
        };

        public static Dictionary<int, string> moduleTrouble = new Dictionary<int, string>()
        {
            { 0, "Bus / EBus / Wireless module communication fault" },
            { 1, "Tamper trouble" },
            { 2, "Power fail" },
            { 3, "Battery failure" },
            { 99, "Any bus module new trouble event" }
        };

        public static Dictionary<int, string> moduleTroubleRestore = new Dictionary<int, string>()
        {
            { 0, "Bus / EBus / Wireless module communication fault restore" },
            { 1, "Tamper trouble restore" },
            { 2, "Power fail restore" },
            { 3, "Battery failure restore" },
            { 99, "Any bus module trouble restored event" }
        };

        public static Dictionary<int, string> special = new Dictionary<int, string>()
        {
            { 0, "System power up" },
            { 1, "Reporting test" },
            { 2, "Software log on" },
            { 3, "Software log off" },
            { 4, "Installer in programming mode" },
            { 5, "Installer exited programming mode" },
            { 6, "Maintenance in programming mode" },
            { 7, "Maintenance exited programming mode" },
            { 8, "Closing delinquency delay elapsed" },
            { 99, "Any special event" }
        };

        public static Dictionary<int, string> systemStatus = new Dictionary<int, string>()
        {
            { 0, "Follow Arm LED status" },
            { 1, "PGM pulse fast in alarm" },
            { 2, "PGM pulse fast in exit delay below 10 sec." },
            { 3, "PGM pulse slow in exit delay over 10 sec." },
            { 4, "PGM steady ON if armed" },
            { 5, "PGM OFF if disarmed" }
        };

        public static Dictionary<int, string> eventOpt1 = new Dictionary<int, string>()
        {
            { 1, "PGM Number 1" },
            { 2, "PGM Number 2" },
            { 3, "PGM Number 3" },
            { 4, "PGM Number 4" },
            { 5, "PGM Number 5" },
            { 6, "PGM Number 6" },
            { 7, "PGM Number 7" },
            { 8, "PGM Number 8" },
            { 9, "PGM Number 9" },
            { 10, "PGM Number 10" },
            { 11, "PGM Number 11" },
            { 12, "PGM Number 12" },
            { 13, "PGM Number 13" },
            { 14, "PGM Number 14" },
            { 15, "PGM Number 15" },
            { 16, "PGM Number 16" },
            { 17, "Wireless Repeater 1" },
            { 18, "Wireless Repeater 2" },
            { 19, "Wireless Keypad 1" },
            { 20, "Wireless Keypad 2" },
            { 21, "Wireless Keypad 3" },
            { 22, "Wireless Keypad 4" },
            { 27, "Wireless Siren 1" },
            { 28, "Wireless Siren 2" },
            { 29, "Wireless Siren 3" },
            { 30, "Wireless Siren 4" },
            { 99, "Any output number" }
        };

        public static Dictionary<int, string> partitionLabel = new Dictionary<int, string>()
        {
            { 1, "Partition 1" },
            { 2, "Partition 2" }
        };

        public static Dictionary<int, string> busModuleLabel = new Dictionary<int, string>()
        {
            { 1, "Bus Module 1" },
            { 2, "Bus Module 2" },
            { 3, "Bus Module 3" },
            { 4, "Bus Module 4" },
            { 5, "Bus Module 5" },
            { 6, "Bus Module 6" },
            { 7, "Bus Module 7" },
            { 8, "Bus Module 8" },
            { 9, "Bus Module 9" },
            { 10, "Bus Module 10" },
            { 11, "Bus Module 11" },
            { 12, "Bus Module 12" },
            { 13, "Bus Module 13" },
            { 14, "Bus Module 14" },
            { 15, "Bus Module 15" }
        };

        public static Dictionary<int, string> siteNameLabel = new Dictionary<int, string>() { { 1, "Site Name" } };

    }
}
