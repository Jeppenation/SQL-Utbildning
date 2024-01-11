CREATE TABLE Products(
	ArticleNumber nvarchar(200) NOT NULL primary key,
	Title nvarchar(200) NOT NULL,
	Description nvarchar(max) null,
	Price money NOT NULL,
)