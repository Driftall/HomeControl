import socket
import sys
from thread import *
import select
import serial

'http://www.binarytides.com/python-socket-programming-tutorial/'

HOST = ''
PORT = 9999

ID = "0/";
Connected = "3/";
Data = "5/";

class Protocol:
    getValue = "00/";

toSend = {};

print("HomeControl PiServer");
print("Raspberry Pi server for the HomeControl Suite");

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
print("Socket created");

try:
    server.bind((HOST, PORT));
except (socket.error , msg):
    print("Bind failed. Error code: " + str(msg[0]) + " Message: " + msg[1]);
    sys.exit();

print("Socket bind complete");
server.listen(10);
print("Socket listening");

def clientThread(conn):
    inputReady, outputReady, exceptReady = select.select([conn],[],[])
    for conn in inputReady:
        recvID = conn.recv(2048);
        if(recvID.startswith(ID)): 
            currentClient = recvID[2:];
            toSend[currentClient] = "";
            currentRun = True;
            while currentRun:
                try:
                    inputReady, outputReady, exceptReady = select.select([conn],[conn],[],0.5)
                    for conn in inputReady:
                        data = conn.recv(2048);
                        if data:
                            if(data.startswith(Data)):
                                DataReceivedFromClient(currentClient, data[2:]);
                    for conn in outputReady:
                        if(len(toSend[currentClient]) > 0):
                            conn.sendall(Data + toSend[currentClient]);
                            toSend[currentClient] = "";
                except:
                    print(currentClient + ">Timeout");
                    currentRun = False;
        conn.close();
        currentRun = False;
    
def serverThread():
    print("thread")
    global server;
    server.setblocking(0);
    serverRunning = True;
    while serverRunning:
        try:
            inputReady, outputReady, exceptReady = select.select([server],[],[]);
            for server in inputReady:
                conn, addr = server.accept()
                conn.setblocking(0);
                print("Connected with " + addr[0] + ":" + str(addr[1]));
                start_new_thread(clientThread, (conn,));
                print("started")
        except:
            something = "yolo";

def DataReceivedFromClient(client, data):
    print(client + ">" + data);

def SendDataToClient(client, data):
    toSend[client] = Data + data;
    
running = 1;
start_new_thread(serverThread, ());
ser = serial.Serial("COM3", 9600);
serialID = ser.readline()[:-2]
if(serialID.startswith(ID)):
    serialID = serialID[2:];
while running:
    incomingData = ser.readline()[:-2];
    print(serialID + ">" + incomingData);
    SendDataToClient("BLAKE-PC", serialID + ">" + incomingData);
    something = "yolo";
    
server.close();