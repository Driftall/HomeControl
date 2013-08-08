import mosquitto;

#http://mosquitto.org/documentation/python/

def on_connect(mosq, obj, rc):
    print("rc: "+str(rc))

def on_message(mosq, obj, msg):
    if(msg.topic[:6] == "SERVER"):
        print(msg.topic[7:] +" "+ str(msg.payload))
    else:
        if(msg.topic == "CONNECTED"):
            client.publish(str(msg.payload) + "/BEEP");
        print(msg.topic +" "+ str(msg.payload))

def on_publish(mosq, obj, mid):
    print("mid: "+str(mid))

def on_subscribe(mosq, obj, mid, granted_qos):
    print("Subscribed: "+str(mid)+" "+str(granted_qos))

def on_log(mosq, obj, level, string):
    print(string)

client = mosquitto.Mosquitto("SERVER");

client.on_message = on_message
client.on_connect = on_connect
client.on_publish = on_publish
client.on_subscribe = on_subscribe

client.connect("192.168.0.117");
client.subscribe("SERVER/#", 0);
client.subscribe("CONNECTED", 0);

print("SERVER");

result = 0;
while result == 0:
    result = client.loop()

print("result: " + str(result));

