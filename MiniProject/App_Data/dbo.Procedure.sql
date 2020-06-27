CREATE PROCEDURE restaurant_insert
	@resturant_name varchar(255),
	@resturant_cuisine varchar(255),
	@resturant_location varchar(255)
AS
	INSERT INTO restaurants(restaurant_name, restaurant_cusine, restaurant_location) values(@resturant_name, @resturant_cuisine, @resturant_location)
RETURN 
