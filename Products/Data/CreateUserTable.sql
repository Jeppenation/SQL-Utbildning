CREATE TABLE Users(
	Id int NOT NULL primary key,
	FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL,
	Email nvarchar(100) NOT NULL unique,
	Password nvarchar(100) NOT NULL,
)