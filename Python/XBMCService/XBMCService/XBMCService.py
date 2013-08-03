# http://www.jordanhackworth.com/home-automation-with-xbmc/

# http://stackoverflow.com/questions/15734219/simple-python-udp-server-trouble-receiving-packets-from-clients-other-than-loca

import xbmc,xbmcgui
import subprocess,os
from socket import *
import sys, select
import binascii
 
class MyPlayer(xbmc.Player) :
        def __init__ (self):
            xbmc.Player.__init__(self)
 
        #http://stackoverflow.com/questions/3470398/list-of-integers-into-string-byte-array-python
        def onPlayBackStarted(self):
            if xbmc.Player().isPlayingVideo():
                client_socket.sendto(str(bytearray([0])) + "HTPC" + str(bytearray([1,3,9,0])) + "5", address);

        def onPlayBackResumed(self):
            if xbmc.Player().isPlayingVideo():
                client_socket.sendto(str(bytearray([0])) + "HTPC" + str(bytearray([1,3,9,0])) + "6", address);
                 
        def onPlayBackPaused(self):
            if xbmc.Player().isPlayingVideo():
                client_socket.sendto(str(bytearray([0])) + "HTPC" + str(bytearray([1,3,9,0])) + "7", address);
 
        def onPlayBackStopped(self):
            if (VIDEO == 1):
                client_socket.sendto(str(bytearray([0])) + "HTPC" + str(bytearray([1,3,9,0])) + "8", address);

        def onPlayBackEnded(self):
            if (VIDEO == 1):
                client_socket.sendto(str(bytearray([0])) + "HTPC" + str(bytearray([1,3,9,0])) + "9", address);


player=MyPlayer()

address = ('192.168.0.117', 9998)
client_socket = socket(AF_INET, SOCK_DGRAM)
client_socket.sendto(str(bytearray([0])) + "HTPC" + str(bytearray([1])), address);

VIDEO = 0
 
while(1):
    if xbmc.Player().isPlaying():
      if xbmc.Player().isPlayingVideo():
        VIDEO = 1
 
      else:
        VIDEO = 0
 
    xbmc.sleep(1000)