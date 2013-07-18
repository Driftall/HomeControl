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

boolean enteringKey = false;
boolean systemLocked = false;
long startMillis = 0;
long timeout = 10000;

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
String const dChangedValue = "00/";
String const dSetValue = "02/";

//DeviceProtocol
String const aDebug = "000/";
String const aLockStatus = "001/";
String const aIP = "002/";
String const aBatteryPercentage = "003/";
String const aBeep = "004/";
String const aDoorLCD = "005/";

//VariableProtocol
String const vOff = "0";
String const vOn = "1";
String const vQuickLock = "2";
String const vFullLock = "3";

String arduinoID = "Arduino_Door";

Keypad keypad = Keypad( makeKeymap(keys), rowPins, colPins, rows, cols );
LiquidCrystal lcd(12, 11, 19, 18, 17, 16);

String day = "Thu";
String date = "18";
String month = "Jul";
String hour = "11";
String minute = "25";

void setup()
{
  lcd.begin(16, 2);
  lcdSplashScreen();
  delay(2500);
  Serial.begin(9600);
  delay(500);
  Serial.println(cID + arduinoID);
  lcdHomeScreen();
}

void loop()
{
  char key = keypad.getKey();
  if(key)
  {
    keyPressed(key);
  }
}


void keyPressed(char key)
{
  if(key == '#') // Quick lock
  {
    iKey = 0;
    systemLocked = true;
    lcdHomeScreen();
    Serial.println(cData + aLockStatus + dSetValue + vQuickLock);
  } 
  else if(key == '*') // Key Entry
  {
    iKey = 0;
    enteringKey = true;
    lcdEnterKeyScreen();
  }
  else if((key) && (enteringKey == true))
  {
    inputKey[iKey] = key;
    lcd.setCursor(11+iKey,1);
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
        enteringKey = false;
        lcdCodeFailedScreen();
        Serial.println("codeFailed");
        startMillis = 0;
      }
      else // code worked
      {
        if(systemLocked)
        {
          systemLocked = false;
          Serial.println(cData + aLockStatus + dSetValue + vOff);
        }
        else //system wasnt locked
        {
          systemLocked = true;
          Serial.println(cData + aLockStatus + dSetValue + vFullLock); //full lock
        }
        enteringKey = false;
        lcdHomeScreen();
      }
      iKey = 0;
    }
    else
    {
      iKey++;
    }
  }
}

void lcdSplashScreen()
{
  lcd.clear();
  lcd.print("HomeControlSuite");
  lcd.setCursor(0, 1);
  lcd.print("  Arduino Door  ");
}

void lcdHomeScreen()
{
  lcd.clear();
  lcd.print(day + " " + date + " " + month + " " + hour + ":" + minute);
  lcd.setCursor(0, 1);
  if(systemLocked)
  {
    lcd.print("Status:   Locked");
  }
  else
  {
    lcd.print("Status: Unlocked");
  }
}

void lcdEnterKeyScreen()
{
  lcd.clear();
  lcd.print(day + " " + date + " " + month + " " + hour + ":" + minute);
  lcd.setCursor(0, 1);
  lcd.print("Enter Key:      ");
}

void lcdCodeFailedScreen()
{
  lcd.clear();
  lcd.print(day + " " + date + " " + month + " " + hour + ":" + minute);
  lcd.setCursor(0, 1);
  lcd.print("  Code  Failed  ");
}

void lcdCodeTimeout()
{
  lcd.clear();
  lcd.print(day + " " + date + " " + month + " " + hour + ":" + minute);
  lcd.setCursor(0, 1);
  lcd.print("  Code Timeout  ");  
}

/*  if(Serial.available() >= 5)
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
 */
