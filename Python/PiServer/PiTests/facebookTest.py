import facebook

token = 'CAAG1nDJn3IABADrPIi2D2N26P32Hrjglew8nZAvFtGjZA9RfhEAwufrel78VySxQnIWTrOvuE3tDFly6cX2uMcT9T3moAILZAhhP3YVafmOvxy5WUDnDfQiEyb2obiqZAL2YsZCLwOwB5BZBlVkrt0i3w7Fx9BHCEZD'

graph = facebook.GraphAPI(token)
profile = graph.get_object("me")
friends = graph.get_connections("me", "friends")

friend_list = [friend['name'] for friend in friends['data']]

print friend_list

input("hello")
