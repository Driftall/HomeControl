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

//VariableProtocol
String const vOff = "0";
String const vOn = "1";
String const vUnlock = "2";
String const vQuickLock = "3";
String const vFullLock = "4";

String arduinoID = "Arduino_Barebones";

boolean const DEBUG = true;

void setup()
{
  delay(2500);
  Serial.begin(9600);
  Serial.println(cID + arduinoID);
  Serial.println("Home Control Suite - Arduino Barebones");
}

void loop()
{
  /*
  Serial.write(cData);
  Serial.write(aLockStatus);
  Serial.write(dChangedValue);
  Serial.println(vFullLock);
  */
  delay(5000);
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
        byte variableData = Serial.read();
        //Process data
      }
    }
  }
}

