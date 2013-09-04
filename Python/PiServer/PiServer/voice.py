#from espeak import espeak
from subprocess import PIPE, Popen

def speak(voice, toSpeak):
    if voice == 1:
        dosomething = True
        #espeak.synth(toSpeak);
    elif voice == 2:
        dosomething = True
        process = Popen(['flite', '-voice', 'slt', '-t', "'" + toSpeak + "'"], stdout=PIPE);