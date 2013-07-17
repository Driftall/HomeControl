#include <Keypad.h>
#include <LiquidCrystal.h>

char savedKey[4] = {
  '1','9','9','3'};

char inputKey[4] = {
  ' ',' ',' ',' '};

int iKey = 0;

const byte rows = 4; //four rows
const byte cols = 3; //three columns
char keys[rows][cols] = {
  {
    '1','2','3'                }
  ,
  {
    '4','5','6'                }
  ,
  {
    '7','8','9'                }
  ,
  {
    '*','0','#'                }
};
byte rowPins[rows] = {
  7, 2, 3,5}; //connect to the row pinouts of the keypad
byte colPins[cols] = {
  6,8,4}; //connect to the column pinouts of the keypad
Keypad keypad = Keypad( makeKeymap(keys), rowPins, colPins, rows, cols );
LiquidCrystal lcd(12, 11, 19, 18, 17, 16);

void setup()
{
  Serial.begin(9600);
  Serial.println("0/Arduino_Door");
  delay(500);
  Serial.println("3/Home Control Suite - Arduino Door");
  lcd.begin(16, 2);
  lcd.print("HomeControlSuite");
  lcd.setCursor(0, 1);
  lcd.print("  Arduino Door  ");
}

void loop()
{
  char key = keypad.getKey();
  if(key == '#')
  {
    iKey = 0;
    lcd.clear();
    lcd.print("     System     ");
    lcd.setCursor(0, 1);
    lcd.print("     Locked     ");
    Serial.println("lock");
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
        Serial.println("unlock");
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





