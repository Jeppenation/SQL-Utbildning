DECLARE @ArticleNumber nvarchar(200) ='P-0006'
DECLARE @Title nvarchar(200) = 'Product 1'
DECLARE @Description nvarchar(max)
DECLARE @Price money = 100.00

IF EXISTS (SELECT 1 FROM Products WHERE ArticleNumber = @ArticleNumber) SELECT * FROM Products WHERE ArticleNumber = @ArticleNumber ELSE SELECT 'Product does not exist exists' AS Message 
	

