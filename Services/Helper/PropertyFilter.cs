using DataAccess.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Database_Models;
using Models.JsonModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class PropertyFilter : IPropertyFilter
    {
        protected readonly ApplicationDbContext _context;

        public PropertyFilter(ApplicationDbContext context)
        {
            _context = context;
        }
        //Async not working 
        public async Task<IEnumerable<Property>> Filter(string formcollection)
        {
            try
            {
                //await _context.co;
                var mod = JsonSerializer.Deserialize<JsonFilterModel>(formcollection);
                string[] PriceParts = mod.PriceRange.Split(',');
                string[] AreaParts = mod.PropertyGeo.Split(",");
                string[] BedRoomParts = mod.BedRoomsRange.Split(',');
                string[] BathRoomParts = mod.BathRoomsRange.Split(',');

                JsonResponseViewModel model = new JsonResponseViewModel();
                var KeyWord = new SqlParameter("@PropertyName", mod.KeyWord);
                var PriceRangeMin = new SqlParameter("@PriceRangeMin", PriceParts[0]);
                var PriceRangeMax = new SqlParameter("@PriceRangeMax", PriceParts[1]);
                var AreaRangeMin = new SqlParameter("@AreaRangeMin", AreaParts[0]);
                var AreaRangeMax = new SqlParameter("@AreaRangeMax", AreaParts[1]);
                var BedRoomMin = new SqlParameter("@BedRoomMin", BedRoomParts[0]);
                var BedRoomMax = new SqlParameter("@BedRoomMax", BedRoomParts[1]);
                var BathRoomMin = new SqlParameter("@BathRoomMin", BathRoomParts[0]);
                var BathRoomMax = new SqlParameter("@BathRoomMax", BathRoomParts[1]);
                var City = new SqlParameter("@City", mod.City);
                var Status = new SqlParameter("@Status", mod.Status);
                var FirePlace = new SqlParameter("@FirePlace", mod.FirePlace);
                var DualSinks = new SqlParameter("@DoubleSinks", mod.DualSinks);
                var SwimmingPool = new SqlParameter("@SwimmingPool", mod.SwimmingPool);
                var Stories = new SqlParameter("@Stories", mod.Stories);
                var EmergencyExit = new SqlParameter("@EmergencyExit", mod.EmergencyExit);
                var LaundryRoom = new SqlParameter("@LaundryRoom", mod.LaundryRoom);
                var JogPath = new SqlParameter("@JogPath", mod.JogPath);
                var Ceilings = new SqlParameter("@Ceilings", mod.Ceilings);
                var HurricaneShutters = new SqlParameter("@HurricaneShutters", mod.HurricaneShutters);
                var PropertyDate = new SqlParameter("@PropertyDate", mod.PropertyDate);
                var PropertyPrice = new SqlParameter("@PropertyPrice", mod.PropertyPrice);
                var Offset = new SqlParameter("@Offset", Convert.ToInt64(mod.Offset));
                var users = await _context.properties
                    .FromSqlRaw
                    ("[dbo].[FilterProperties] @PropertyName,@PriceRangeMin,@PriceRangeMax,@AreaRangeMin,@AreaRangeMax,@BedRoomMin,@BedRoomMax,@BathRoomMin,@BathRoomMax,@City,@Status,@FirePlace,@DoubleSinks,@SwimmingPool,@Stories,@EmergencyExit,@LaundryRoom,@JogPath,@Ceilings,@HurricaneShutters,@PropertyDate,@PropertyPrice,@Offset"
                    , KeyWord, PriceRangeMin, PriceRangeMax, AreaRangeMin, AreaRangeMax, BedRoomMin, BedRoomMax, BathRoomMin, BathRoomMax, City, Status, FirePlace, DualSinks, SwimmingPool, Stories, EmergencyExit, LaundryRoom, JogPath, Ceilings, HurricaneShutters, PropertyDate, PropertyPrice, Offset)
                    .AsNoTracking().ToListAsync();
                //    var filteredJson = JsonSerializer.Serialize(users);
                //   model.ResponseCode = 200;
                //model.ResponseMessage = filteredJson;
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }


            /*   try
               {

                   var databaseFacade = _context.Database;

                   // Open a connection manually
                   var connection = databaseFacade.GetDbConnection();
                   connection.Open();

                   try
                   {
                       // Create a DbCommand object for the stored procedure
                       using var command = connection.CreateCommand();
                       command.CommandText = "[dbo].[FilterProperties]";
                       command.CommandType = System.Data.CommandType.StoredProcedure;


                       // Set up any parameters needed by the stored procedure
                       // command.Parameters.Add(...);

                       // Execute the stored procedure
                       using var reader = command.ExecuteReader();
                       var dataList = new List<Property>();
                       // Read the result set
                       while (reader.Read())
                       {
                           // Access the columns using reader.GetXXX methods
                           var cc = reader.GetInt32(reader.GetOrdinal("PropertyId"));
                           var data = new Property
                           {
                               PropertyId = reader.GetInt32(reader.GetOrdinal("PropertyId")),
                               PropertyName = reader.GetString(reader.GetOrdinal("PropertyName")),
                               CoverImageUrl = reader.GetString(reader.GetOrdinal("CoverImageUrl")),
                               // CatagoryId=reader.GetInt32(reader.GetOrdinal("CategoryId")),
                               PropertyPrice = reader.GetInt32(reader.GetOrdinal("PropertyPrice")),
                               PropertyDescription = reader.GetString(reader.GetOrdinal("PropertyDescription")),
                               DateTime = reader.GetDateTime(reader.GetOrdinal("DateTime")),
                               //StatusId=reader.GetInt32(reader.GetOrdinal("StatusId")),
                               Area = reader.GetInt32(reader.GetOrdinal("Area")),
                               BedRooms = reader.GetInt32(reader.GetOrdinal("BedRooms")),
                               //Garage=reader.GetInt32(reader.GetOrdinal("Garage")),
                               //  BathRooms=reader.GetInt32(reader.GetOrdinal("BathRooms")),
                               //CityId=reader.GetInt32(reader.GetOrdinal("CityId"))
                           };

                           dataList.Add(data);
                           // Process or use the retrieved values as needed
                       }
                       connection.Close();
                       var filteredJson = JsonSerializer.Serialize(dataList);
                       model.ResponseCode = 200;
                       model.ResponseMessage= filteredJson;

                   }
                   catch (Exception ex)
                   {
                       Console.WriteLine(ex.ToString());
                   }
               }
               catch (Exception ex)
               {
                   Console.WriteLine(ex.ToString());
               }
   */
            //MAKE DB CALL and handle the response


        }
        public async Task<int> FilterCount(string formCollection)
        {
            try
            {
                var mod = JsonSerializer.Deserialize<JsonFilterModel>(formCollection);
                string[] PriceParts = mod.PriceRange.Split(',');
                string[] AreaParts = mod.PropertyGeo.Split(",");
                string[] BedRoomParts = mod.BedRoomsRange.Split(',');
                string[] BathRoomParts = mod.BathRoomsRange.Split(',');

                JsonResponseViewModel model = new JsonResponseViewModel();
                var KeyWord = new SqlParameter("@PropertyName", mod.KeyWord);
                var PriceRangeMin = new SqlParameter("@PriceRangeMin", PriceParts[0]);
                var PriceRangeMax = new SqlParameter("@PriceRangeMax", PriceParts[1]);
                var AreaRangeMin = new SqlParameter("@AreaRangeMin", AreaParts[0]);
                var AreaRangeMax = new SqlParameter("@AreaRangeMax", AreaParts[1]);
                var BedRoomMin = new SqlParameter("@BedRoomMin", BedRoomParts[0]);
                var BedRoomMax = new SqlParameter("@BedRoomMax", BedRoomParts[1]);
                var BathRoomMin = new SqlParameter("@BathRoomMin", BathRoomParts[0]);
                var BathRoomMax = new SqlParameter("@BathRoomMax", BathRoomParts[1]);
                var City = new SqlParameter("@City", mod.City);
                var Status = new SqlParameter("@Status", mod.Status);
                var FirePlace = new SqlParameter("@FirePlace", mod.FirePlace);
                var DualSinks = new SqlParameter("@DoubleSinks", mod.DualSinks);
                var SwimmingPool = new SqlParameter("@SwimmingPool", mod.SwimmingPool);
                var Stories = new SqlParameter("@Stories", mod.Stories);
                var EmergencyExit = new SqlParameter("@EmergencyExit", mod.EmergencyExit);
                var LaundryRoom = new SqlParameter("@LaundryRoom", mod.LaundryRoom);
                var JogPath = new SqlParameter("@JogPath", mod.JogPath);
                var Ceilings = new SqlParameter("@Ceilings", mod.Ceilings);
                var HurricaneShutters = new SqlParameter("@HurricaneShutters", mod.HurricaneShutters);
                var PropertyDate = new SqlParameter("@PropertyDate", mod.PropertyDate);
                var PropertyPrice = new SqlParameter("@PropertyPrice", mod.PropertyPrice);
                var result = await _context.properties
                    .FromSqlRaw
                    ("[dbo].[FilterPropertiesCount] @PropertyName,@PriceRangeMin,@PriceRangeMax,@AreaRangeMin,@AreaRangeMax,@BedRoomMin,@BedRoomMax,@BathRoomMin,@BathRoomMax,@City,@Status,@FirePlace,@DoubleSinks,@SwimmingPool,@Stories,@EmergencyExit,@LaundryRoom,@JogPath,@Ceilings,@HurricaneShutters,@PropertyDate,@PropertyPrice"
                    , KeyWord, PriceRangeMin, PriceRangeMax, AreaRangeMin, AreaRangeMax, BedRoomMin, BedRoomMax, BathRoomMin, BathRoomMax, City, Status, FirePlace, DualSinks, SwimmingPool, Stories, EmergencyExit, LaundryRoom, JogPath, Ceilings, HurricaneShutters, PropertyDate, PropertyPrice)
                    .AsNoTracking().ToListAsync();
                //    var filteredJson = JsonSerializer.Serialize(users);
                //   model.ResponseCode = 200;
                //model.ResponseMessage = filteredJson;
                return result.Count();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }
    }
}