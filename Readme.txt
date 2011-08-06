MajorSilenceConnect is a basic replacement for ultravnc single click that uses the regular ultravnc server winvnc.exe. I find that this works much better on Windows Vista/7. Requires .NET 2.0.

Currently this project only supports the port forwarding setup (does not support repeater mode) because that is all I need. It should be simple enough to add repeater support if it is ever needed.


How compile MajorSilenceConnect:

You need Visual Studio C# 2010. In the resources/helpdesk.xml you will find:

  <helpdesk>
    <support>Support 1</support>
    <address>localhost:5500</address>
  </helpdesk>

<support> is what shows in the gui that the client will double click.
<address> is the address/port that it will attempt to connect to. 