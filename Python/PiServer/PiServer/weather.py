import urllib2 
import json 

#http://www.wunderground.com/weather/api/d/docs?d=resources/code-samples&MR=1

def getWeather(postcode):
    api_key = '078b7bd311a5dd63'
    postal_code = str.replace(postcode, " ", "%20")
    connection = 'http://api.wunderground.com/api/' + api_key + '/conditions/q/' + postal_code + '.json'

    f = urllib2.urlopen(connection) 
    json_string = f.read() 
    parsed_json = json.loads(json_string) 

    global results
    results = {}

    results['city'] = parsed_json['current_observation']['observation_location']['city']
    results['postcode'] = postal_code
    results['weatherdesc'] = parsed_json['current_observation']['weather'] 
    results['tempc'] = parsed_json['current_observation']['temp_c'] 
    results['humidity'] = parsed_json['current_observation']['relative_humidity'] 
    results['winddesc'] = parsed_json['current_observation']['wind_string'] 
    results['windspeed'] = parsed_json['current_observation']['wind_mph']
    results['windgustspeed'] = parsed_json['current_observation']['wind_gust_mph'] 

    f.close()

    return results