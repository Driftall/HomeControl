import mosquitto;
import serial;

#http://mosquitto.org/documentation/python/

def on_connect(mosq, obj, rc):
    print("rc: "+str(rc))

def on_message(mosq, obj, msg):
    print(msg.topic[8:] +" "+str(msg.payload))

def on_publish(mosq, obj, mid):
    print("mid: "+str(mid))

def on_subscribe(mosq, obj, mid, granted_qos):
    print("Subscribed: "+str(mid)+" "+str(granted_qos))

def on_log(mosq, obj, level, string):
    print(string)

#arduino = serial.Serial("COM3", timeout=1);

client = mosquitto.Mosquitto("ARDUINO");
client.will_set("CONNECTION/ARDUINO", "DISCONNECTED", 0, True);

client.on_message = on_message
client.on_connect = on_connect
client.on_publish = on_publish
client.on_subscribe = on_subscribe

client.connect("192.168.0.117");
client.subscribe("ARDUINO/#", 0);
client.publish("CONNECTION/ARDUINO", "CONNECTED", 0, True);

print("ARDUINO");

result = 0;
while result == 0:
    result = client.loop()

print("result: " + str(result));