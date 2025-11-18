using System;
using System.Data;
using Microsoft.Data.SqlClient;

string connectionString = "Data Source = (localdb)\\mssqllocaldb; Initial Catalog = OrderDB; Integrated Security = True ";
SqlConnection connection = new SqlConnection(connectionString);
try
  {
    connection.Open();
    Console.WriteLine("Получилось подключится");
    SqlCommand command = new SqlCommand();
    command.Connection = connection;
    command.CommandText = @"
CREATE TABLE Clients (
    ClientID INT PRIMARY KEY IDENTITY(1,1),
    ClientPhone NVARCHAR(50) NOT NULL,
    ClientEmail NVARCHAR(100) NOT NULL
);

CREATE TABLE Couriers (
    CourierID INT PRIMARY KEY IDENTITY(1,1),
    CourierPhone NVARCHAR(50) NOT NULL,
    CourierCity NVARCHAR(50) NOT NULL
);

CREATE TABLE Restaurants (
    RestaurantID INT PRIMARY KEY IDENTITY(1,1),
    RestaurantName NVARCHAR(100) NOT NULL,
    RestaurantType NVARCHAR(50) NOT NULL,
    RestaurantCity NVARCHAR(50) NOT NULL
);

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    OrderAmount INT NOT NULL,
    ClientID INT NOT NULL,
    RestaurantID INT NOT NULL,
    CourierID INT NOT NULL,
    OrderCity NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_Orders_Clients FOREIGN KEY (ClientID) 
        REFERENCES Clients(ClientID),
    CONSTRAINT FK_Orders_Restaurants FOREIGN KEY (RestaurantID) 
        REFERENCES Restaurants(RestaurantID),
    CONSTRAINT FK_Orders_Couriers FOREIGN KEY (CourierID) 
        REFERENCES Couriers(CourierID)
);

INSERT INTO Clients (ClientPhone, ClientEmail) VALUES 
('+7-911-534-43-67', 'ivanovchik@mail.ru'),
('+7-923-234-65-76', 'awalker@gmail.com'),
('+7-945-543-67-99', 'verstapen@yandex.ru');


INSERT INTO Couriers (CourierPhone, CourierCity) VALUES 
('+7-955-353-95-44', 'Красноярск'),
('+7-966-244-86-44', 'Москва'),
('+7-900-322-75-55', 'Санкт-Петербург');


INSERT INTO Restaurants (RestaurantName, RestaurantType, RestaurantCity) VALUES 
('Папа Джонс', 'Пиццерия', 'Красноярск'),
('Su', 'Японская кухня', 'Москва'),
('Бургер Кинг', 'Фастфуд', 'Санкт-Петербург');



INSERT INTO Orders (OrderAmount, ClientID, RestaurantID, CourierID, OrderCity) VALUES 
(1231, 1, 1, 1, 'Красноярск'),
(889, 2, 2, 2, 'Москва'),
(1500, 3, 3, 3, 'Санкт-Петербург');
";
    int num = command.ExecuteNonQuery();
    Console.WriteLine($"Строк изменено {num} ");
}
catch (Exception ex)
   {
    Console.WriteLine(ex.ToString());
   }
finally
   {
     connection.Close();
     Console.WriteLine("БД закрыта");
   }
