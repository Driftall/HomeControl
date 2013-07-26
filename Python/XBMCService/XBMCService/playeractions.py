# http://www.jordanhackworth.com/home-automation-with-xbmc/

import xbmc,xbmcgui
import subprocess,os
 
class MyPlayer(xbmc.Player) :
 
        def __init__ (self):
            xbmc.Player.__init__(self)
 
        def onPlayBackStarted(self):
            if xbmc.Player().isPlayingVideo():
                os.system("wget --spider 'http://10.10.0.6:49451/data_request?id=lu_action&output_format=xml&serviceId=urn:micasaverde-com:serviceId:HomeAutomationGateway1&action=RunScene&SceneNum=19'")
 
        def onPlayBackEnded(self):
            if (VIDEO == 1):
                os.system("wget --spider 'http://10.10.0.6:49451/data_request?id=lu_action&output_format=xml&serviceId=urn:micasaverde-com:serviceId:HomeAutomationGateway1&action=RunScene&SceneNum=2'")
 
        def onPlayBackStopped(self):
            if (VIDEO == 1):
                os.system("wget --spider 'http://10.10.0.6:49451/data_request?id=lu_action&output_format=xml&serviceId=urn:micasaverde-com:serviceId:HomeAutomationGateway1&action=RunScene&SceneNum=2'")
 
        def onPlayBackPaused(self):
            if xbmc.Player().isPlayingVideo():
                os.system("wget --spider 'http://10.10.0.6:49451/data_request?id=lu_action&output_format=xml&serviceId=urn:micasaverde-com:serviceId:HomeAutomationGateway1&action=RunScene&SceneNum=18'")
 
        def onPlayBackResumed(self):
            if xbmc.Player().isPlayingVideo():
                os.system("wget --spider 'http://10.10.0.6:49451/data_request?id=lu_action&output_format=xml&serviceId=urn:micasaverde-com:serviceId:HomeAutomationGateway1&action=RunScene&SceneNum=19'")
player=MyPlayer()
 
VIDEO = 0
 
while(1):
    if xbmc.Player().isPlaying():
      if xbmc.Player().isPlayingVideo():
        VIDEO = 1
 
      else:
        VIDEO = 0
 
    xbmc.sleep(1000)