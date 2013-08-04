/*
  HomeControl Arduino Barebones
 Arduino interface for the HomeControl Suite
 */
//SocketProtocol
byte const cID = 0;
byte const cConnected = 1;
byte const cDebug = 2;
byte const cData = 3;
byte const cMessage = 4;

//DataProtocol
byte const dChangedValue = 0;
byte const dSetValue = 2;

//DeviceProtocol
byte const aDebug = 0;
byte const aLockStatus = 1;
byte const aIP = 2;
byte const aBatteryPercentage = 3;
byte const aBeep = 4;
byte const aDoorLCD = 5;
byte const aDateTime = 6;
byte const aXBMC = 9;

//VariableProtocol
String const vOff = "0";
String const vOn = "1";
String const vUnlock = "2";
String const vQuickLock = "3";
String const vFullLock = "4";

String arduinoID = "Arduino_Barebones";

#define BLUEPIN 7
#define GREENPIN 6
#define REDPIN 5
#define YELLOWPIN 4

boolean const DEBUG = true;

void setup()
{
  delay(2500);
  Serial.begin(9600);
  Serial.println(cID + arduinoID);
  Serial.println("Home Control Suite - Arduino Barebones");
  pinMode(BLUEPIN, OUTPUT);
  pinMode(GREENPIN, OUTPUT);
  pinMode(REDPIN, OUTPUT);
  pinMode(YELLOWPIN, OUTPUT);
}

void loop()
{
  if(Serial.available() >= 3)
  {
    byte socketData = Serial.read();
    if(socketData == cData) // Data received from server
    {
      if(DEBUG)
      {
        Serial.println("cData");
      }
      byte deviceData = Serial.read();
      byte dataData = Serial.read();
      if(dataData == dSetValue) // Set value from server
      {
        if(DEBUG)
        {
          Serial.println("dataData");
        }
        String variableData;
        if (Serial.available() >0) {
          char c = Serial.read();  //gets one byte from serial buffer
          variableData += c; //makes the string readString
        }
        if(deviceData == aXBMC)
        {
          if(DEBUG)
          {
            Serial.println("XBMC");
          }
          if(variableData == "1")
          {
              digitalWrite(BLUEPIN, HIGH);  
              digitalWrite(GREENPIN, HIGH);  
              digitalWrite(REDPIN, HIGH);  
              digitalWrite(YELLOWPIN, HIGH);
          }
          else
          {
              digitalWrite(BLUEPIN, LOW);  
              digitalWrite(GREENPIN, LOW);  
              digitalWrite(REDPIN, LOW);  
              digitalWrite(YELLOWPIN, LOW);            
          }
        }
      }
    }
  }
}


