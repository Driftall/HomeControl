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
long timerCount = 0;
long timerInterval = 1000;
long keyPadCurrent = -1;
long keyPadTimeout = 10;
long messageCurrent = -1;
long messageTimeout = 2;

const byte rows = 4; //four rows
const byte cols = 3; //three columns
char keys[rows][cols] = {
  {
    '1','2','3'      }
  ,
  {
    '4','5','6'      }
  ,
  {
    '7','8','9'      }
  ,
  {
    '*','0','#'      }
};
byte rowPins[rows] = {
  7, 2, 3,5}; //connect to the row pinouts of the keypad
byte colPins[cols] = {
  6,8,4}; //connect to the column pinouts of the keypad

String months[12] = {
  "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Nov", "Dec" };
String days[8] = {
  "", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"};

//SocketProtocol
byte const cID = '0';
byte const cConnected = '1';
byte const cDebug = '2';
byte const cData = '3';
byte const cMessage = '4';

//DataProtocol
byte const dChangedValue = '0';
byte const dSetValue = '2';

//DeviceProtocol
byte const aDebug = '0';
byte const aLockStatus = '1';
byte const aIP = '2';
byte const aBatteryPercentage = '3';
byte const aBeep = '4';
byte const aDoorLCD = '5';
byte const aDateTime = '6';

//VariableProtocol
byte const vOff = '0';
byte const vOn = '1';
byte const vUnlock = '2';
byte const vQuickLock = '3';
byte const vFullLock = '4';

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
  Serial.println("Home Control Suite - Arduino Door");
  lcdHomeScreen();
  timerCount = millis();
}

void loop()
{
  if(Serial.available() >= 3)
  {
    byte socketData = Serial.read();
    if(socketData == cData) // Data received from server
    {
      byte deviceData = Serial.read();
      byte dataData = Serial.read();
      if(dataData == dSetValue) // Set value from server
      {
        byte variableData = Serial.read();
        //Process data
      }
    }
  }
  long mil = millis();
  if(mil - timerCount > timerInterval)
  {
    timerCount = mil;
    timerTick(); 
  }
  char key = keypad.getKey();
  if(key)
  {
    keyPressed(key);
  }
}

void timerTick()
{
  if(keyPadCurrent != -1)
  {
    if(keyPadCurrent > keyPadTimeout)
    {
      enteringKey = false;
      keyPadCurrent = -1;
      lcdCodeTimeoutScreen();
      messageCurrent = 0;
    }
    else
    {
      keyPadCurrent++;
    }
  }
  if(messageCurrent != -1)
  {
    if(messageCurrent > messageTimeout)
    {
      messageCurrent = -1;
      lcdHomeScreen();
    }
    else
    {
      messageCurrent++; 
    }
  }
}

void keyPressed(char key)
{
  if((key == '#') && (enteringKey == false)) // Quick lock
  {
    iKey = 0;
    systemLocked = true;
    lcdHomeScreen();
    Serial.println(cData + aLockStatus + dChangedValue + vQuickLock);
  } 
  else if(key == '*') // Key Entry
  {
    iKey = 0;
    if(enteringKey)
    {
      enteringKey = false;
      keyPadCurrent = -1;
      lcdHomeScreen();
    }
    else
    {
      enteringKey = true;
      keyPadCurrent = 0;
      lcdEnterKeyScreen();
    }
  }
  else if((key == '0') && (enteringKey == true))
  {
    iKey = 0;
    enteringKey = true;
    keyPadCurrent = 0;
    lcdEnterKeyScreen();
  }
  else if((key) && (enteringKey == true) && (key != '#') && (key != '*'))
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
        messageCurrent = 0;
        Serial.println("codeFailed");
      }
      else // code worked
      {
        if(systemLocked)
        {
          systemLocked = false;
          Serial.println(cData + aLockStatus + dChangedValue + vOff);
        }
        else //system wasnt locked
        {
          systemLocked = true;
          Serial.println(cData + aLockStatus + dChangedValue + vFullLock); //full lock
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

void lcdCodeTimeoutScreen()
{
  lcd.clear();
  lcd.print(day + " " + date + " " + month + " " + hour + ":" + minute);
  lcd.setCursor(0, 1);
  lcd.print("  Code Timeout  ");  
}



