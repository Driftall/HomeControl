///Home Control Suite - Arduino - LED Test

#define BLUEPIN 7
#define GREENPIN 6
#define REDPIN 5
#define YELLOWPIN 4

void setup()
{
  pinMode(BLUEPIN, OUTPUT);
  pinMode(GREENPIN, OUTPUT);
  pinMode(REDPIN, OUTPUT);
  pinMode(YELLOWPIN, OUTPUT);
}

void loop()
{
  digitalWrite(BLUEPIN, HIGH);
  delay(250);
  digitalWrite(BLUEPIN, LOW);
  digitalWrite(GREENPIN, HIGH);
  delay(250);
  digitalWrite(GREENPIN, LOW);
  digitalWrite(REDPIN, HIGH);
  delay(250);
  digitalWrite(REDPIN, LOW);
  digitalWrite(YELLOWPIN, HIGH);
  delay(250);
  digitalWrite(YELLOWPIN, LOW);
}

