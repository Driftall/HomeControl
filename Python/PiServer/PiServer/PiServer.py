import mosquitto;
import voice;

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

result = 0;
while result == 0:
    result = client.loop()

print("result: " + str(result));

