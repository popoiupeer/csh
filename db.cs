using System;
using System.Data;
using Microsoft.Data.SqlClient;

string connectionString = "Data Source = (localdb)\\mssqllocaldb; Initial Catalog = aboba; Integrated Security = True ";
SqlConnection connection = new SqlConnection(connectionString);
try
{
    connection.Open();
    Console.WriteLine("Получилось подключиться");
    bool flag = true;
    while (flag)
    {
        Console.WriteLine(@"Что вы хотите сделать?:
    1)Выбрать все из таблицы
    2)Добавить данные в таблицу
    3)ВЫХОД
    ");
        int choice = Convert.ToInt16(Console.ReadLine());
        switch (choice)
        {
            case 1:
                Console.WriteLine(@"Какие таблицы выбрать?:
                1)Employees
                2)Department
                3)Orders
                    ");
                int tablechoice = Convert.ToInt16(Console.ReadLine());
                string sqlExpression = "";

                switch (tablechoice)
                {
                    case 1:
                        sqlExpression = "SELECT * FROM Employees";
                        SqlCommand command1 = new SqlCommand(sqlExpression, connection);
                        SqlDataReader reader = command1.ExecuteReader();

                        string column1 = reader.GetName(0);
                        string column2 = reader.GetName(1);
                        string column3 = reader.GetName(2);
                        Console.WriteLine($"{column1}\t{column2}\t{column3}");
                        while (reader.Read())
                        {
                            object id = reader.GetValue(0);
                            object name = reader.GetValue(1);
                            object depid = reader.GetValue(2);
                            Console.WriteLine($"{id}\t{name}\t{depid}");
                        }
                        reader.Close();
                        break;

                    case 2:
                        sqlExpression = "SELECT * FROM Departments";
                        SqlCommand command2 = new SqlCommand(sqlExpression, connection);
                        SqlDataReader reader1 = command2.ExecuteReader();

                        string column4 = reader1.GetName(0);
                        string column5 = reader1.GetName(1);
                        Console.WriteLine($"{column4}\t{column5}");
                        while (reader1.Read())
                        {
                            object did = reader1.GetValue(0);
                            object dname = reader1.GetValue(1);
                            Console.WriteLine($"{did}\t{dname}");
                        }
                        reader1.Close();
                        break;


                    case 3:
                        sqlExpression = "SELECT * FROM Orders";
                        SqlCommand command3 = new SqlCommand(sqlExpression, connection);
                        SqlDataReader reader3 = command3.ExecuteReader();

                        string column6 = reader3.GetName(0);
                        string column7 = reader3.GetName(1);
                        string column8 = reader3.GetName(2);
                        Console.WriteLine($"{column6}\t{column7}\t{column8}");
                        while (reader3.Read())
                        {
                            object orderid = reader3.GetValue(0);
                            object employeeid = reader3.GetValue(1);
                            object orderdate = reader3.GetValue(2);
                            Console.WriteLine($"{orderid} \t {employeeid} \t {orderdate}");
                        }
                        reader3.Close();
                        break;
                }
                break;

            case 2:
                Console.WriteLine(@"В какую таблицу добавить данные?:
                1)Employees
                2)Departments
                3)Orders
                    ");
                int incerttablechoice = Convert.ToInt16(Console.ReadLine());

                switch (incerttablechoice)
                {
                    case 1:
                        Console.WriteLine("Введите имя и фамилию сотрудника: ");
                        string ename = Console.ReadLine();
                        Console.WriteLine("Введите номер его департамента: ");
                        string edepid = Console.ReadLine();

                        string sqlExpression2 = $"INSERT INTO Employees (Name, DepartmentID) VALUES ('{ename}',{edepid})";
                        SqlCommand command2 = new SqlCommand(sqlExpression2, connection);
                        int asd = command2.ExecuteNonQuery();
                        Console.WriteLine($"Добавлено {asd} строк в таблицу Employees ");

                        break;

                    case 2:
                        Console.WriteLine("Введите название департамента:");
                        string departName = Console.ReadLine();
                        string sqlExpression3 = $"INSERT INTO Departments (DepartmentName) VALUES ('{departName}')";
                        SqlCommand command3 = new SqlCommand(sqlExpression3, connection);
                        int asd2 = command3.ExecuteNonQuery();
                        Console.WriteLine($"Добавлено {asd2} строк в таблицу Department ");
                        break;

                    case 3:
                        Console.WriteLine("Введите ID сотрудника: ");
                        string employeeId = Console.ReadLine();
                        Console.WriteLine("Введите дату заказа (гггг-мм-дд): ");
                        string orderDate = Console.ReadLine();

                        string sqlExpression4 = $"INSERT INTO Orders (EmployeeID, OrderDate) VALUES ({employeeId}, '{orderDate}')";
                        SqlCommand command4 = new SqlCommand(sqlExpression4, connection);
                        int asd3 = command4.ExecuteNonQuery();
                        Console.WriteLine($"Добавлено {asd3} строк в таблицу Orders");
                        break;
                }


                break;
            case 3:
                flag = false;
                break;
        }
    }
    string sqlExpressionStart = @"
CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(50) NOT NULL
);


CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    DepartmentID INT NOT NULL,
    CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentID) 
        REFERENCES Departments(DepartmentID)
);


CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT NOT NULL,
    OrderDate DATE NOT NULL,
    CONSTRAINT FK_Orders_Employees FOREIGN KEY (EmployeeID) 
        REFERENCES Employees(EmployeeID)
);


INSERT INTO Departments (DepartmentName) VALUES 
('IT'),
('Sales'),
('Marketing'),
('Finance'),
('HR'),
('Operations'),
('Customer Service');


INSERT INTO Employees (Name, DepartmentID) VALUES 
('John Smith', 1),
('Maria Johnson', 1),
('Alex Brown', 2),
('Helen Davis', 2),
('Dmitry Wilson', 3),
('Olga Miller', 3),
('Sergei Taylor', 4),
('Anna Anderson', 4),
('Paul Thomas', 5),
('Natalie Jackson', 6),
('Andrew White', 7),
('Irina Harris', 7);


INSERT INTO Orders (EmployeeID, OrderDate) VALUES 
(1, '2024-01-15'),
(1, '2024-01-20'),
(2, '2024-01-16'),
(3, '2024-01-17'),
(3, '2024-01-22'),
(3, '2024-01-25'),
(4, '2024-01-18'),
(5, '2024-01-19'),
(6, '2024-01-21'),
(7, '2024-01-23'),
(8, '2024-01-24'),
(9, '2024-01-26'),
(10, '2024-01-27'),
(11, '2024-01-28'),
(12, '2024-01-29'),
(1, '2024-02-01'),
(2, '2024-02-02'),
(3, '2024-02-03');

";

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
