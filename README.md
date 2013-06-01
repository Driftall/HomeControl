HomeControl is a Home Control Suite that runs on a multitude of devices and interfaces with many others.

The main device is a Raspberry Pi, with an Arduino connected to expand the IO of the Pi.

The Pi will run a Java server program that will process all connections and commands from clients.
It will also run a client program showing various information on an attached screen.

XBMC will be interfaced using the JSON-RPC API, allowing control without having to modify your XBMC installation

Windows will be interfaced using a Notifier running in the system tray that will process any requests sent to the computer but will also report data back to the server. It has been implemented this way to remove the majority of intrusion but also allowing the user to see any issues that may occur.