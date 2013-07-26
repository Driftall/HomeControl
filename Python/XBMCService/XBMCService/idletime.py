# http://www.jordanhackworth.com/home-automation-with-xbmc/

import xbmc,xbmcgui
import subprocess,os
 
ILT = 0
IDLE_TIME_MIN = 15
s = 0
 
while(1):
  it = xbmc.getGlobalIdleTime()
  s = ((IDLE_TIME_MIN * 60) - it )
  if (s > 0):
    if (ILT == 1):
      if xbmc.Player().isPlayingVideo():
        pass
      else:
        os.system("wget --spider 'http://10.10.0.6:49451/data_request?id=lu_action&output_format=xml&serviceId=urn:upnp-org:serviceId:SwitchPower1&action=SetTarget&newTargetValue=1&DeviceNum=10'")
        ILT = 0
 
  elif (s < 0):
    if (ILT == 0 and xbmc.Player().isPlayingVideo() == False):
      os.system("wget --spider 'http://10.10.0.6:49451/data_request?id=lu_action&output_format=xml&serviceId=urn:upnp-org:serviceId:SwitchPower1&action=SetTarget&newTargetValue=0&DeviceNum=10'")
      ILT = 1
  xbmc.sleep(5000)
