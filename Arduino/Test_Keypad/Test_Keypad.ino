#include <Keypad.h>

char key1 = 1;
char key2 = 2;
char key3 = 3;
char key4 = 4;

char inputKey1 = 0;
char inputKey2 = 0;
char inputKey3 = 0;
char inputKey4 = 0;

const byte rows = 4; //four rows
const byte cols = 3; //three columns
char keys[rows][cols] = {
  {
    '1','2','3'  }
  ,
  {
    '4','5','6'  }
  ,
  {
    '7','8','9'  }
  ,
  {
    '*','0','#'  }
};
byte rowPins[rows] = {
  7, 2, 3,5}; //connect to the row pinouts of the keypad
byte colPins[cols] = {
  6,8,4}; //connect to the column pinouts of the keypad
Keypad keypad = Keypad( makeKeymap(keys), rowPins, colPins, rows, cols );

void setup()
{
  Serial.begin(9600);
}

void loop()
{
  Serial.println(key1);
  char key = keypad.getKey();
  if(key)
  {
    Serial.println(key);
    inputKey1 = inputKey2;
    inputKey2 = inputKey3;
    inputKey3 = inputKey4;
    inputKey4 = key;
    if(key1 == inputKey1)
    {
      if(key2 == inputKey2)
      {
        if(key3 == inputKey3)
        {
          if(key4 == inputKey4)
          {
            Serial.println("Unlocked");
          }
        }
      }
    }
  }
}

