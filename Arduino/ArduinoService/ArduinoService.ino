/*
  HomeControl ArduinoService
  Arduino interface for the HomeControl Suite
*/
#include <SoftwareSerial.h>

//SoftwareSerial SSerial1(10, 11); // RX, TX
//SoftwareSerial SSerial2(8, 9); //RX, TX

String const ID = "0/";
String const Connected = "3/";
String const Data = "5/";

String s1Name = "";
String s2Name = "";

void setup()
{
  Serial.begin(9600);
  Serial.println(ID + "Arduino_Bedroom_Main");
  Serial.println("Home Control Suite - Arduino Service");
  //SSerial1.begin(4800);
  //SSerial2.begin(4800);
}

void loop()
{
  delay(2500);
  Serial.println("Sup yo");
  /*if(Serial.available() > 0)
  {
    Serial.println("Serial>" + Serial.read());
  }  
  if(SSerial1.available() > 0)
  {
    String data = SSerial1.read();
    if(data.startsWith(ID))
    {
     s1Name =  data.substring(2);
    }
    else
    {
      Serial.println(s1Name + ">" + data);
    }
  }
  if(SSerial2.available() > 0)
  {
    String data = SSerial2.read();
    if(data.startsWith(ID))
    {
     s2Name =  data.substring(2);
    }
    else
    {
      Serial.println(s2Name + ">" + data);
    }
  }*/
}
