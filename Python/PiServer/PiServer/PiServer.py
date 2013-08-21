import mosquitto;
import voice;
import weather;
import time;

#http://mosquitto.org/documentation/python/

def on_connect(mosq, obj, rc):
    print("rc: "+str(rc))

def on_message(mosq, obj, msg):
    if(msg.topic[:6] == "SERVER"):
        print(msg.topic[7:] +" "+ str(msg.payload))
    elif(msg.topic[:10] == "CONNECTION"):
        clientStr = msg.topic[11:];
        print(clientStr + ">" + str(msg.payload));
        if(str(msg.payload) == "CONNECTED"):
            clients.append(clientStr);
            connected.append(clientStr);
        elif(str(msg.payload) == "DISCONNECTED"):
            connected.remove(clientStr);

        voice.speak(2, clientStr + " " + str(msg.payload));

def on_publish(mosq, obj, mid):
    print("mid: "+str(mid))

def on_subscribe(mosq, obj, mid, granted_qos):
    print("Subscribed: "+str(mid)+" "+str(granted_qos))

def on_log(mosq, obj, level, string):
    print(string)


clients = []
connected = []

client = mosquitto.Mosquitto("SERVER");

client.on_message = on_message
client.on_connect = on_connect
client.on_publish = on_publish
client.on_subscribe = on_subscribe

client.connect("192.168.0.117");
client.subscribe("SERVER/#", 0);
client.subscribe("CONNECTION/#", 0);

voice.speak(2, "Server started");

print("SERVER");

def wakeUp():
    weatherResults = weather.getWeather("EC1A 1BB");
    nowTime = time.localtime(time.time())
    year = nowTime[0]
    month = nowTime[1]
    day = nowTime[2]
    hour = nowTime[3]
    minute = nowTime[4]
    days = ["", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"]
    months = ["", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"]
    hourString = hour;
    dayTimeString = "AM";
    if(hour > 12):
        dayTimeString = "PM";
        hourString = str(hour - 12)
    minuteString = "";
    if minute > 9:
        minuteString = ':' + str(minute)
    elif minute > 0:
        minuteString = ':0' + str(minute)

    dateString = "today is " + "someday" + " the " + str(day) + "th of " + months[month]
    finalString = hourString + minuteString + " " + dayTimeString + ", " + dateString
    wakeupString = "Good Morning Sir, it is " + finalString + ", the weather in " + weatherResults['city'] + " is " + str(weatherResults['tempc']) + " degrees and " + weatherResults['weatherdesc']
    print wakeupString

result = 0;
wakeupCalled = False;
while result == 0:
    nowTime = time.localtime(time.time())
    hour = nowTime[3]
    minute = nowTime[4]
    alarmHour = 13
    alarmMinute = 4
    if hour == alarmHour and minute == alarmMinute and wakeupCalled == False:
        wakeupCalled = True;
        wakeUp()
    elif hour == alarmHour and minute > alarmMinute:
        wakeupCalled = False;
    result = client.loop()

print("result: " + str(result));

