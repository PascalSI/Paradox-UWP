.# Paradox-UWP
Simple UWP for Win10 Mobile &amp; Desktop to connect to Paradox Alarm Systems.

This app allows connecting to SP6000 (and probably other) Paradox Alarm Systems using the IP150 IP connection module.
I created this app since the original iParadox app is not available for WinPhones (only Android and iOS...sigh).
Tested from both LAN and external connection, it's very simple and basic but it works...there are of course still a lot of things to be implemented and checked (I've been learning C# while creating this app so please have mercy on me!)

Idea and Mappings based on @Tertiush ParadoxIP150v2 (https://github.com/Tertiush/ParadoxIP150v2)

Requires Win10 RS1 (14393), created with VS15 preview5
Tested on Desktop PC and Lumia735 both with Win10 Anniversary Edition (just compile & deploy from VS)

TODO:
- improve UI (need to use something else instead of checkboxes for Zone status)
- Add icons & graphics
- Add translations
- Try & learn proper coding style :P
- Add MQTT support to connect to MQTT server (using M2MQTT Package by Paolo Patierno)
- Add support for secure connection to alarm (this might be tough without access to Paradox SDK)
- Add control options to send commands to the alarm system (arm, disarm)

A FEW INFO ON THE PROTOCOL:
I started following @Tertiush suggestion to use Wireshark and capture a few sessions between my PC (using Winload) and the alarm system. From there I created a quick app acting as a "fake" alarm panel to experiment a bit on sending data to Winload and see what happened (might share that code if someone is interested, but it's very ugly). Here are the results (this strings are the HEX bytes sent from the App (Q) and from the Alarm (A), I've added spaces to highglight the bytes with a meaning):

Handshake (this is sent only once to authenticate with the alarm system):
(Q)"This is going to be replaced by the password login string"
(A)"aa19000138f0000000eeeeeeeeeeeeee0042364132364435303741393533313032002001267101cc93eeeeeeeeeeeeee"	-> changes every time
(Q)"aa00000308f2000aeeeeeeeeeeeeeeee"
(A)"aa01000138f2000000eeeeeeeeeeeeee00eeeeeeeeeeeeeeeeeeeeeeeeeeeeee"
(Q)"aa00000308f3000aeeeeeeeeeeeeeeee"
(A)"aa1100013af3000000eeeeeeeeeeeeee0100000000000000000000000000000000eeeeeeeeeeeeeeeeeeeeeeeeeeeeee"
(Q)"aa2500040800000aeeeeeeeeeeeeeeee72000000000000000000000000000000000000000000000000000000000000000000000072eeeeeeeeeeeeeeeeeeeeee"
(A)"aa2500023200000000eeeeeeeeeeeeee72ff04020001a41f01049400290bbc4109910206020b02140972fc57535036303030000001"
(Q)"aa26000308f8000aeeeeeeeeeeeeeeee0a500080000000000000000000000000000000000000000000000000000000000000000000d0eeeeeeeeeeeeeeeeeeee"
(A)"aa0100017af8000000eeeeeeeeeeeeee01eeeeeeeeeeeeeeeeeeeeeeeeeeeeee"
(Q)"aa2500040800000aeeeeeeeeeeeeeeee5f20000000000000000000000000000000000000000000000000000000000000000000007feeeeeeeeeeeeeeeeeeeeee"
(A)"aa2500027200000000eeeeeeeeeeeeee000000001604940000000000000000000000000000000000000000000000000000000000ae"
(Q)"aa25000408000014eeeeeeeeeeeeeeee000000001604940000000000190000000000000000000000000000000000000000020000c9eeeeeeeeeeeeeeeeeeeeee"
(A)"aa2500027200000000eeeeeeeeeeeeee10000000000000000000000000000000000000000000000000000000000000000000000010"
(A)"aa2500027200000000eeeeeeeeeeeeeee 21410081b0e0e 300200000000000000000000000000000000000000000000020000000079"	--> date&time
(Q)"aa25000408000014eeeeeeeeeeeeeeee50000000000000000000000000000000000000000000000000000000000000000000000050eeeeeeeeeeeeeeeeeeeeee"
(A)"aa2500027200000000eeeeeeeeeeeeee520000000000000c4e2c00000000000000000000706f72746120696e67726573736f2020c8" ->the long string at the end is the label of the delayed sensor (the front door in my case)(Q)"aa25000408000014eeeeeeeeeeeeeeee50000e520000000000000000000000000000000000000000000000000000000000000000b0eeeeeeeeeeeeeeeeeeeeee"
(A)"aa2500027200000000eeeeeeeeeeeeee52000e52 290bbc41 1fa4000000000000000100000000000000000000000000000000008027" ->handshake end with alarm module serial

Keepalive (this is resent every second to keep the connection from being closed):
(Q)"aa25000408000014eeeeeeeeeeeeeeee500080000000000000000000000000000000000000000000000000000000000000000000d0eeeeeeeeeeeeeeeeeeeeee"
(A)"aa2500027200000000eeeeeeeeeeeeee520080000000000000 1410081b 0e0e d0999600 fafe0000 000000000000000000000000002c" -> date+time+voltages+status
(Q)"aa25000408000014eeeeeeeeeeeeeeee500080010000000000000000000000000000000000000000000000000000000000000000d1eeeeeeeeeeeeeeeeeeeeee"
(A)"aa2500027200000000eeeeeeeeeeeeee5200800100000000000000000000000000 00 000000000000010000000000000000000000d4" -> alarm status

Here is how the data is encoded:
Date: it's a simple hex to dec conversion 
  byte1= Year (first two digits) 
  byte2= Year (last two digits)
  byte3= Month
  byte4= Day
  eg. x1410081b = d20160827 =  2016-08-27 (August 27th 2016)

Time: as for the date, just convert hex to dec
  byte1= Hours
  byte2= Minutes
  eg. x0e0e = d1414 = 14:14 (or 2:14pm if you like)
 
Voltages: this are encoded on a custom voltage scale (from 00=min to FF=max), it seems a linear scale
  byte1: VDC (FF= 20.3 - 00= 1.4 - 80= 10.9)
  byte2: DC (FF= 22.8 - 00= 0.0 - 80= 11.5)
  byte3: Battery (FF= 22.8 - 00= 0.0 - 80= 11.5)
  byte4: Unknown, for me it is always 00 - probably is part of another module I don't have installed in my system
  
Zone status: this is a bitmask on the sensor status (0: clear, 1: active) divided in groups of 8 each
  byte1: zones from 1 to 8
  byte2: zones from 9 to 16
  byte3: zones from 17 to 24
  byte4: zones from 25 to 32
  (maybe there are more, again not sure how to check this as my system supports only 32 zones)
  eg. x fa fe 00 00 = b 11111010  11111110 00000000 00000000 = zones 1,3,9 and 17 to 32 are closed (0) while the others are open
