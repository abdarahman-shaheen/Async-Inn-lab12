# Async-Inn
this is a simple api project that we will make on a several labs and it starts on the with the first lab 11 which will include the ERD(Entity Relationship Diagram and in lab 12 which is this one we will include the asp.net console web application that we did 
in this app we have made 3 models which are :

1.Room

2.Hotel

3.Amenity

and we made a controller to view it in a jason.
Take notice:
you need to downloads packages as microsoft entity framework tool,microsoft entity framework design,microsoft entity framework SQL server.
## Digram:
![diagram](https://github.com/abdarahman-shaheen/Async-Inn-lab12/blob/master/async.png)


## Explanation:

1. Location entity: Represents a hotel location.
   - `location_id`: Unique identifier for each hotel location.
   - `name`: Name of the hotel location.
   - `city`: City where the hotel is located.
   - `state`: State where the hotel is located.
   - `address`: Street address of the hotel.
   - `phone_number`: Contact phone number for the hotel location.
   - `#no_room`:how many rooms in the location.

2. Room entity: Represents a hotel room.
   - `room_id`: Unique identifier for each hotel room.
   - `room_number`: Room number of the hotel room.
   - `nickname`: Nickname given to the room for better differentiation.
   - `layout_design `: Type of the room (e.g., one-bedroom, two-bedroom, studio).


3. Amenity entity: Represents the amenities available in a room.
   - `amenity_id`: Unique identifier for each amenity.
   - `name`: Name of the amenity.


4.Room_Amenity (associative table): Represents the amenities available in a room with each other .
 - `room_id`: forign key for each hotel room.
 - `amenity_id`: forign key for each hotel room.
 - `price`: Price of the room.
 - `pet_friendly`: Indicates whether the room is pet-friendly.
   
5.Location_Room (associative table): Represents the location for each room  .
 - `room_id`: forign key for each hotel room.
 - `location_id`: forign key for each hotel location.
## Relationships:
- Each location can have multiple rooms (many-to-many relationship): The location entity is related to the Room entity through an associative table as there would be many roms inmany locations.
- Each room can have multiple amenities (many-to-many relationship): The Amenity entity is related to the Room entity through an associative table.
##  Joint Entity Table with Payload Tables:
the Room_Amenity table serves as a Joint Entity Table with Payload table, consisting of foreign keys that reference the primary keys of the Room and Amenity tables and includes  additional attributes or payload information.

additional attributes:
   - `price`: Price of the room.
   - `pet_friendly`: Indicates whether the room is pet-friendly.
##  Pure Joint Tables:
the Location_Room table serves as a  Pure Joint table, consisting of foreign keys that reference the primary keys of the location  and Room tables and doesnot includes  any additional attributes or payload information.

## Repository Pattern:
HotelService and RoomService and AmenityService which serves as a centralized data access component following characteristics of the Repository Pattern. Despite being named as a "service," it functions as a repository for (hotel/room/amenity )related data operations. It abstracts the underlying data storage (database) using Entity Framework Core and exposes methods to handle creating, retrieving, updating, and deleting (hotel/room/amenity) data.

The IHotel IRoom IAmenity interfaces serves as a contract that any class representing a (hotel/room/amenity) repository . By defining these methods in the interface, the application can achieve decoupling between the data access layer and the rest of the application, enabling flexible data storage implementations and improving testability and maintainability.

## Relationships:
- Each location can have multiple rooms (many-to-many relationship): The location entity is related to the Room entity through an associative table as there would be many roms inmany locations.
- Each room can have multiple amenities (many-to-many relationship): The Amenity entity is related to the Room entity through an associative table.
##  Joint Entity Table with Payload Tables:
the Room_Amenity table serves as a Joint Entity Table with Payload table, consisting of foreign keys that reference the primary keys of the Room and Amenity tables and includes  additional attributes or payload information.
additional attributes:
   - `price`: Price of the room.
   - `pet_friendly`: Indicates whether the room is pet-friendly.
##  Pure Joint Tables:
the Location_Room table serves as a  Pure Joint table, consisting of foreign keys that reference the primary keys of the location  and Room tables and doesnot includes  any additional attributes or payload information.

Pattern**.
Despite being named as a "service," it functions as a repository for (hotel/room/amenity )related data operations. 
It abstracts the underlying data storage (database) using Entity Framework Core and exposes methods to handle creating, retrieving, updating, and deleting (hotel/room/amenity) data.

The `IHotel` `IRoom` `IAmenity` interfaces serves as a contract that any class representing a (hotel/room/amenity)   repository .
By defining these methods in the interface, the application can achieve decoupling between the data access layer and the rest of the application, enabling flexible data storage implementations and improving testability and maintainability.

