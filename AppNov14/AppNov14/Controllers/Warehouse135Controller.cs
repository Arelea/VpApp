using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AppNov14.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace AppNov14.Controllers
{
    [Authorize]
    public class Warehouse135Controller : Controller
    {
        // Начало. Конфигурация для строки подключения

        private readonly IConfiguration _configuration;

        public Warehouse135Controller(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Получение уникальных значений типа материала для JQuery Cascade Dropdown list для CreateWarehouseFieldType

        [NonAction]
        public DataSet GetTypeOfMaterialWarehouse()
        {
            DataSet dataset = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("JQ_MultiController_GetDistinct_TypeOfMaterial", connection);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("in_SubsType", 1);
                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.Fill(dataset);
                }
                return dataset;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Получение уникальных значений имени типа материала для JQuery Autocomplete для CreateWarehouseFieldType

        public JsonResult GetNameTypesSecond(string type, string Prefix, int SubsType)
        {
            try
            {
                
                List<string> HalfTime = new List<string>();
                using (SqlConnection sqlConnection1 = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection1.Open();
                    SqlCommand command = new SqlCommand("JQ_MultiController_GetDistinct_NameOfTypeMaterial_DependentByTypeOfMaterial", sqlConnection1)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("in_TypeOfMaterial", type);
                    command.Parameters.AddWithValue("in_SubsType", SubsType);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HalfTime.Add(Convert.ToString(reader.GetString(0)));
                        }
                    }
                }
                var result = (from N in HalfTime
                              where N.Contains(Prefix)
                              select new { value = N });


                return Json(result);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Получение уникальных значений типа материала для JQuery Autocomplete для CreateWarehouseFields

        [HttpPost]
        public JsonResult GetTypes(string Prefix, int SubsType)
        {
            try
            {
                List<string> HalfTime = new List<string>();
                using (SqlConnection sqlConnection1 = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection1.Open();
                    SqlCommand cmd = new SqlCommand("JQ_MultiController_GetDistinct_TypeOfMaterial", sqlConnection1)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("in_SubsType", SubsType);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HalfTime.Add(Convert.ToString(reader.GetString(0)));
                        }
                    }
                }
                var result = (from N in HalfTime
                              where N.Contains(Prefix)
                              select new {value = N});

                return Json(result);
            }
            catch (SqlException ex)
            {
                throw ex;
            }        
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Получение уникальных значений имени типа материала для JQuery Autocomplete для CreateWarehouseFields

        [HttpPost]
        public JsonResult GetNameTypes(string Prefix, int SubsType)
        {
            try
            {
                List<string> HalfTime = new List<string>();
                using (SqlConnection sqlConnection1 = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection1.Open();
                    SqlCommand cmd = new SqlCommand("JQ_MultiController_GetDistinct_NameOfTypeMaterial", sqlConnection1)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("in_SubsType", SubsType);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HalfTime.Add(Convert.ToString(reader.GetString(0)));
                        }
                    }
                }
                var result = (from N in HalfTime
                              where N.Contains(Prefix)
                              select new { value = N });

                return Json(result);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Получение уникальных значений постовщика (Provider) для JQuery Autocomplete для CreateWarehouseFields

        [HttpPost]
        public JsonResult GetProvider(string Prefix, int SubsType)
        {
            try
            {
                List<string> HalfTime = new List<string>();
                using (SqlConnection sqlConnection1 = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection1.Open();
                    SqlCommand cmd = new SqlCommand("JQ_MultiController_GetDistinct_Provider", sqlConnection1)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("in_SubsType", SubsType);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HalfTime.Add(Convert.ToString(reader.GetString(0)));
                        }
                    }
                }
                var result = (from N in HalfTime
                              where N.Contains(Prefix)
                              select new { value = N });

                return Json(result);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Получение уникальных значений производителя (Manufacturer) для JQuery Autocomplete для CreateWarehouseFields

        [HttpPost]
        public JsonResult GetManufacturer(string Prefix, int SubsType)
        {
            try
            {
                List<string> HalfTime = new List<string>();
                using (SqlConnection sqlConnection1 = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection1.Open();
                    SqlCommand cmd = new SqlCommand("JQ_MultiController_GetDistinct_Manufacturer", sqlConnection1)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("in_SubsType", SubsType);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HalfTime.Add(Convert.ToString(reader.GetString(0)));
                        }
                    }
                }
                var result = (from N in HalfTime
                              where N.Contains(Prefix)
                              select new { value = N });


                return Json(result);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Индекс

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Вьюха создание новых полей склада

        [HttpGet]
        public IActionResult CreateWarehouseFields()
        {
            Warehouse135Model warehouse135 = new Warehouse135Model();
            DataSet dataset = GetTypeOfMaterialWarehouse();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                selectListItems.Add(new SelectListItem { Value = item["TypeOfMaterial"].ToString(), Text = item["TypeOfMaterial"].ToString() });
            }
            ViewBag.TypeList = selectListItems;
            return View(warehouse135);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Пост создание новых полей склада

        [HttpPost]
        public async Task<IActionResult> CreateWarehouseFields([Bind("Id,TypeOfMaterial,NameOfTypeMaterial,Provider,Manufacturer")] Warehouse135Model warehouse135)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                    {
                        sqlConnection.Open();
                        SqlCommand command = new SqlCommand("BasApp_Warehouse_Add", sqlConnection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                       
                        command.Parameters.AddWithValue("in_TypeOfMaterial", warehouse135.TypeOfMaterial);
                        command.Parameters.AddWithValue("in_NameOfTypeMaterial", warehouse135.NameOfTypeMaterial);
                        command.Parameters.AddWithValue("in_Provider", warehouse135.Provider);
                        command.Parameters.AddWithValue("in_Manufacturer", warehouse135.Manufacturer);
                        command.Parameters.AddWithValue("in_SubsType", 1);
                        await command.ExecuteNonQueryAsync();            
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                return RedirectToAction(nameof(CreateWarehouseFields));
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Вьюха создания нового типа и полей склада

        [Authorize(Roles = "Moderator, Admin")]
        [HttpGet]
        public IActionResult CreateWarehouseFieldType()
        {
            Warehouse135Model warehouse135 = new Warehouse135Model();
            
            return View(warehouse135);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Пост создания нового типа и полей склада

        [Authorize(Roles = "Moderator, Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateWarehouseFieldType([Bind("Id,TypeOfMaterial,NameOfTypeMaterial,Provider,Manufacturer")] Warehouse135Model warehouse135)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                    {
                        sqlConnection.Open();
                        SqlCommand command = new SqlCommand("BasApp_Warehouse_Add", sqlConnection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        command.Parameters.AddWithValue("in_TypeOfMaterial", warehouse135.TypeOfMaterial);
                        command.Parameters.AddWithValue("in_NameOfTypeMaterial", warehouse135.NameOfTypeMaterial);
                        command.Parameters.AddWithValue("in_Provider", warehouse135.Provider);
                        command.Parameters.AddWithValue("in_Manufacturer", warehouse135.Manufacturer);
                        command.Parameters.AddWithValue("in_SubsType", 1);
                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                return RedirectToAction(nameof(CreateWarehouseFieldType));
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Вьюха всего склада

        [HttpGet]
        public async Task<IActionResult> WarehouseViewAll()
        {
            if (ModelState.IsValid)
            {
                Warehouse135Model warehouse135 = new Warehouse135Model();

                List<string> DiffTypes = new List<string>();

                using (SqlConnection sqlConnection1 = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection1.Open();
                    SqlCommand cmd = new SqlCommand("BasApp_Warehouse_GetDistinct", sqlConnection1)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("in_SubsType", 1);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            DiffTypes.Add(Convert.ToString(reader.GetString(0)));
                        }
                    }
                    sqlConnection1.Close();
                }
          
                List<DataTable> DiffDataSets = new List<DataTable>();

                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                { 
                   foreach (var item in DiffTypes)
                   {
                        DataTable tdbl = new DataTable();
                        sqlConnection.Open();
                        SqlDataAdapter sqlDA = new SqlDataAdapter("BasApp_Warehouse_ViewAllData", sqlConnection);
                        sqlDA.SelectCommand.Parameters.AddWithValue("in_TypeOfMaterial", item);
                        sqlDA.SelectCommand.Parameters.AddWithValue("in_SubsType", 1);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.Fill(tdbl);
                        DiffDataSets.Add(tdbl);
                        sqlConnection.Close();
                   }
                }

                warehouse135.listil = DiffDataSets;
                warehouse135.list = DiffTypes.ToArray();

                return View(warehouse135);
            }
            return View(ModelState.ErrorCount);
        }
   
        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

    }
}
