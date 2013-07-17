import socket
import sys
from thread import *
import select
import serial

'http://www.binarytides.com/python-socket-programming-tutorial/'

HOST = ''
PORT = 9999

class SocketProtocol:
    ID = "0/";
    Connected = "3/";
    Data = "5/";

class DataProtocol:
    getValue = "00/";
    gotValue = "01/";
    setValue = "02/";
    Message  = "03/";
    Debug    = "04/";

class WindowsProtocol:
    Volume = "001/";
    IP = "002/";
    BatteryPercentage = "003/";
    Beep = "004/";
    Lock = "050/";
    Unlock = "051/";

class ArduinoProtocol:
    Keypad = "101/";
    Display = "102/";

toSend = {};

print("HomeControl PiServer");
print("Raspberry Pi server for the HomeControl Suite");

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
print("Socket created");
try:
    server.bind((HOST, PORT));
except socket.error , msg:
    print("Bind failed. Error code: " + str(msg[0]) + " Message: " + msg[1]);
    sys.exit();

print("Socket bind complete");
server.listen(10);
print("Socket listening");

def clientThread(conn, ip, port):
    inputReady, outputReady, exceptReady = select.select([conn],[],[])
    for conn in inputReady:
        recvID = conn.recv(2048);
        if(recvID.startswith(SocketProtocol.ID)): 
            currentClient = recvID[2:];
            print(currentClient + " connected on " + ip + ":" + port);
            toSend[currentClient] = "";
            currentRun = True;
            while currentRun:
                try:
                    inputReady, outputReady, exceptReady = select.select([conn],[conn],[],0.5)
                    for conn in inputReady:
                        data = conn.recv(2048);
                        if data:
                            if(data.startswith(SocketProtocol.Data)):
                                DataReceivedFromClient(currentClient, data[2:]);
                    for conn in outputReady:
                        if(len(toSend[currentClient]) > 0):
                            conn.sendall(SocketProtocol.Data + toSend[currentClient]);
                            toSend[currentClient] = "";
                except:
                    print(currentClient + ">Timeout");
                    currentRun = False;
        conn.close();
        currentRun = False;
    
def serverThread():
    global server;
    server.setblocking(0);
    serverRunning = True;
    while serverRunning:
        try:
            inputReady, outputReady, exceptReady = select.select([server],[],[]);
            for server in inputReady:
                conn, addr = server.accept()
                conn.setblocking(0);
                start_new_thread(clientThread, (conn,addr[0],str(addr[1]), ));
        except:
            something = "yolo";

def DataReceivedFromClient(client, data):
    print(client + ">" + data);

def SendDataToClient(client, data):
    toSend[client] = SocketProtocol.Data + data;
    
def ProcessCommand(command):
    something = command;

running = 1;
start_new_thread(serverThread, ());
ser = serial.Serial("COM3", 9600);
serialID = ser.readline()[:-2]
if(serialID.startswith(SocketProtocol.ID)):
    serialID = serialID[2:];
    print(serialID + " connected on " + ser.portstr);

welcomeMessage = ser.readline()[:-2]
if(welcomeMessage.startswith(SocketProtocol.Connected)):
    print(serialID + ">" + welcomeMessage[2:]);

while running:
    incomingData = ser.readline()[:-2];
    print(serialID + ">" + incomingData);
    SendDataToClient("BLAKE-PC", serialID + ">" + incomingData);
    something = "yolo";
    
server.close();