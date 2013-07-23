const long interval = 2500;
const byte tempPin = 0;
long temp = 0;
long lastMillis = 0;

void setup()
{
  Serial.begin(9600);
  analogReference(INTERNAL1V1);
}

void loop()
{
  unsigned long currentMillis = millis();
  if(currentMillis - lastMillis > interval)
  {
    doTemperatureMeasurement();
    lastMillis = currentMillis;  
  } 
}

void printTemperature(int value) {
 byte len = 4;
 if (value == 100) len = 5;
 char buffer[len];
 sprintf(buffer, "%3i.%1i", value / 10, value % 10);
 Serial.println(buffer);
}

void doTemperatureMeasurement() {
 int aRead = analogRead(tempPin);
 temp = (aRead/10.23)*10;
 printTemperature(temp);
}
