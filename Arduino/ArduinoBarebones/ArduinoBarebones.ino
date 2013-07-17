/*
  HomeControl Arduino Barebones
  Arduino interface for the HomeControl Suite
*/
//SocketProtocol
String const ID = "0/";
String const Connected = "3/";
String const Data = "5/";

//DataProtocol
String const getValue = "00/";
String const gotValue = "01/";
String const setValue = "02/";

//ArduinoProtocol
//String const Output = "101/";

String arduinoID = "Arduino_Barebones";

void setup()
{
  delay(2500);
  Serial.begin(9600);
  Serial.println(ID + arduinoID);
  Serial.println("Home Control Suite - Arduino Barebones");
}

void loop()
{
  if(Serial.available() > 2)
  {
    String socketData = "";
    for(int i = 0;i<2;i++)
    {
      socketData.concat(Serial.read());
    }
    String arduinoData = "";
    if(socketData == Data)
    {
      String dataData = "";
      for(int i = 0;i<3;i++)
      {
        dataData.concat(Serial.read());
      }
      while(Serial.available() > 0)
      {
        arduinoData.concat(Serial.read());
      }
    }

    while(Serial.available() > 0)
    {
      if(index < 256)
      {
        inChar = Serial.read();
        inData[index] = inChar;
        index++;
        inData[index] = '\0';
      }
    }

    String socketData = inData;
    if(socketData.startsWith(Data))
    {
      String dataData = socketData.substring(2,5);
      String arduinoData = socketData.substring(5);
      String data = arduinoData.substring(4);
      if(arduinoData.startsWith(Display))
      {
        if(dataData == setValue)
        {
          //set something
        }
      }
    }
  }
}
