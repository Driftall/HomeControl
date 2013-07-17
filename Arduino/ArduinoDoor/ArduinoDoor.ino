#include <Keypad.h>
#include <LiquidCrystal.h>

/*
  HomeControl ArduinoDoor
 Arduino interface for the HomeControl Suite
 */

char savedKey[4] = {
  '1','9','9','3'};

char inputKey[4] = {
  ' ',' ',' ',' '};

int iKey = 0;

boolean systemLocked = false;

const byte rows = 4; //four rows
const byte cols = 3; //three columns
char keys[rows][cols] = {
  {'1','2','3'},
  {'4','5','6'},
  {'7','8','9'},
  {'*','0','#'}
};
byte rowPins[rows] = {
  7, 2, 3,5}; //connect to the row pinouts of the keypad
byte colPins[cols] = {
  6,8,4}; //connect to the column pinouts of the keypad

//SocketProtocol
String const cID = "0/";
String const cConnected = "1/";
String const cDebug = "2/";
String const cData = "3/";
String const cMessage = "4/";

//DataProtocol
String const dGetValue = "00/";
String const dGotValue = "01/";
String const dSetValue = "02/";

//DeviceProtocol
String const aDebug = "000/";
String const aLockStatus = "001/";
String const aIP = "002/";
String const aBatteryPercentage = "003/";
String const aBeep = "004/";
String const aDoorLCD = "005/";

//VariableProtocol
String const vOn = "1";
String const vOff = "0";

String arduinoID = "Arduino_Door";

Keypad keypad = Keypad( makeKeymap(keys), rowPins, colPins, rows, cols );
LiquidCrystal lcd(12, 11, 19, 18, 17, 16);

void setup()
{
  delay(2500);
  Serial.begin(9600);
  lcd.begin(16, 2);
  lcd.print("HomeControlSuite");
  lcd.setCursor(0, 1);
  lcd.print("  Arduino Door  ");
  delay(500);
  Serial.println(cID + arduinoID);
}

void loop()
{
  if(Serial.available() >= 5)
  {
    String socketData = "";
    for(int i = 0;i<2;i++)
    {
      socketData.concat(Serial.read());
    }
    if(socketData == cData)
    {
      String dataData = "";
      for(int i = 0;i<3;i++)
      {
        dataData.concat(Serial.read());
      }
      String arduinoData = "";
      for(int i = 0;i<4;i++)
      {
        arduinoData.concat(Serial.read());
      }
      String data = "";
      while(Serial.available() > 0)
      {
        data.concat(Serial.read());
      }
      if(arduinoData == aDoorLCD)
      {
        if(dataData == dSetValue)
        {
          lcd.print(data); 
        }
      }
    }
  }

  //-----------------------


  char key = keypad.getKey();
  if(key == '#')
  {
    iKey = 0;
    lcd.clear();
    lcd.print("     System     ");
    lcd.setCursor(0, 1);
    lcd.print("     Locked     ");
    Serial.println(cData + aLockStatus + dSetValue + vOn);
    systemLocked = true;
    digitalWrite(13,HIGH);
  } 
  else if(key == '*')
  {
    iKey = 0;
    lcd.clear();
    lcd.print("  Enter  code:  "); 
    lcd.setCursor(6, 1);
  }
  else if(key)
  {
    if(iKey == 0)
    {
      lcd.clear();
      lcd.print("  Enter  code:  "); 
      lcd.setCursor(6, 1);
    }
    inputKey[iKey] = key;
    lcd.print(key);
    if(iKey == 3)
    {
      boolean unlock = true;
      for(int i = 0; i<= 3; i++)
      {
        if(savedKey[i] != inputKey[i])
        {
          unlock = false;
        }
      }
      if(unlock == false) //code failed
      {
        lcd.clear();
        lcd.print("      Code      ");
        lcd.setCursor(0, 1);
        lcd.print("     Failed     ");
        Serial.println("codeFailed");
      }
      else // code worked
      {
        lcd.clear();
        lcd.print("     System     ");
        lcd.setCursor(0, 1);
        lcd.print("    Unlocked    ");
        Serial.println(cData + aLockStatus + dSetValue + vOff);
        systemLocked = false;
        digitalWrite(13,LOW);
      }
      iKey = 0;
    }
    else
    {
      iKey++;
      lcd.setCursor(6 + iKey, 1);
    }
  }
}









