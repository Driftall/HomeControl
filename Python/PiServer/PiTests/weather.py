import urllib2 
import json 

api_key = '078b7bd311a5dd63'
postal_code = 'EC1A 1BB'
#connection = 'http://api.wunderground.com/api/' + api_key + '/conditions/q/' + postal_code + '.json'
connection = 'http://api.wunderground.com/api/078b7bd311a5dd63/conditions/q/EC1A%201BB.json'

f = urllib2.urlopen(connection) 
json_string = f.read() 
parsed_json = json.loads(json_string) 

city = parsed_json['current_observation']['observation_location']['city']
postcode = postal_code
weatherdesc = parsed_json['current_observation']['weather'] 
tempc = parsed_json['current_observation']['temp_c'] 
humidity = parsed_json['current_observation']['relative_humidity'] 
winddesc = parsed_json['current_observation']['wind_string'] 
windspeed = parsed_json['current_observation']['wind_mph']
windgustspeed = parsed_json['current_observation']['wind_gust_mph'] 

print "Current temperature in %s is: %s" % (location, temp_c) 
f.close()
input()