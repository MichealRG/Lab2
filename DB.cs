using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Lab2
{
    class DB
    {
        public void SelectOrder(IDbConnection connection, int id)
        {
            var sql = "SELECT * FROM Orders O JOIN [Order Details] OD ON O.OrderID=OD.OrderID WHERE O.OrderID=@id";
            
            var resultOrder =  default(Order); //jakbym wpisał null to by nie odmyslil sie jakeigo typu to bedzie, chce dostac pod-pisanego nulla tylko danego typu

            var result = connection.Query<Order, OrderDetails, Order>(//<typ numer 1, typ numer 2, return>
                sql,
                (order, orderDetails) => //mapowanie
                {
                    resultOrder ??= order; //jezeli jest nullerm to zrob to a jezlei nie to pomin czyli to co na dole
                    //if (resultOrder==null)
                    //{
                    //    resultOrder = order;
                    //}
                   
                    resultOrder.Details.Add(orderDetails);
                    return resultOrder;
                },
                new { id },
                splitOn: "OrderID"//object params czyli parametyr do funkcji
                );
        }
        public void Select(IDbConnection connection) //dzieki idb connection dapper bd działa niezaleznie od teog co uzywam
        {
            var sql = "SELECT * FROM Region";
            var regions = connection.Query<Region>(sql); //query z dappera pobiera z connectiona i wsadza, Dapper dodaje nam metody do mysqlConnection, sam przemapował dapper na obiekt
            foreach (var item in regions)
            {
                Console.WriteLine($"{item.RegionId}: {item.RegionDescription}");
            }
        }
        public int Insert(IDbConnection connection, Region region)
        {
            var insertSql = "INSERT INTO Region(regionId, regionDescription) VALUES (@regionId, @regionDescription)"; //jezeli chce w execute robic obiektem to te parametry musza miec  taka nazwa jak propy z obiektu
            return connection.Execute(insertSql, region
               // new { id = 100, desc = "dapper" } //anonimowa klasa 
                );

        }
        public int Insert(IDbConnection connection, int id, string description) //metody są przeciążone insert/ przeładowane
        {
            var insertSql = "INSERT INTO Region(regionId, regionDescription) VALUES (@regionId, @regionDescription)"; //jezeli chce w execute robic obiektem to te parametry musza miec  taka nazwa jak propy z obiektu
            return connection.Execute(insertSql,
                new {
                    id,
                    desc = description } //mozna skracać nazwę można jezeli ta nazwa jest taka sama  
                );
        }
        public int Delete(IDbConnection connection, int id)
        {
            var sql = "DELETE FROM Region WHERE regionId=@id";
            return connection.Execute(sql, new { id });
        }
        


    }
}
