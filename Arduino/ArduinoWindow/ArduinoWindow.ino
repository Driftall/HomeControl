/*
  HomeControl ArduinoWindow
  Arduino interface for the HomeControl Suite
*/

String const ID = "0/";
String const Connected = "3/";
String const Data = "5/";

String arduinoID = "Arduino_Bedroom_Window_Big";
//String arduinoID = "Arduino_Bedroom_Window_Small";

void setup()
{
  Serial.begin(9600);
  Serial.println(ID + arduinoID);
  Serial.println("Home Control Suite - Arduino Window");
}

void loop()
{
  if(Serial.available() > 0)
  {
  }
}
