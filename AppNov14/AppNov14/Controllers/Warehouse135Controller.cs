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
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("TypeMaterialV1ProcedureForJQuery", connection);
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

        public JsonResult GetNameTypesSecond(string type, string Prefix)
        {
            try
            {
                
                List<string> HalfTime = new List<string>();
                using (SqlConnection sqlConnection1 = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection1.Open();
                    SqlCommand command = new SqlCommand("NameTypeMaterialV1ProcedureForJQuery", sqlConnection1)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("incomin_TypeOfMaterial", type);
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
        public JsonResult GetTypes(string Prefix)
        {
            try
            {
                List<string> HalfTime = new List<string>();
                using (SqlConnection sqlConnection1 = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection1.Open();
                    SqlCommand cmd = new SqlCommand("TypeMaterialV1ProcedureForJQuery", sqlConnection1);

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
        public JsonResult GetNameTypes(string Prefix)
        {
            try
            {
                List<string> HalfTime = new List<string>();
                using (SqlConnection sqlConnection1 = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection1.Open();
                    SqlCommand cmd = new SqlCommand("NameTypeMaterialV1ProcedureForJQueryAutocomplete", sqlConnection1);

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
        public JsonResult GetProvider(string Prefix)
        {
            try
            {
                List<string> HalfTime = new List<string>();
                using (SqlConnection sqlConnection1 = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection1.Open();
                    SqlCommand cmd = new SqlCommand("ProviderV1ProcedureForJQueryAutocomplete", sqlConnection1);

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
        public JsonResult GetManufacturer(string Prefix)
        {
            try
            {
                List<string> HalfTime = new List<string>();
                using (SqlConnection sqlConnection1 = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection1.Open();
                    SqlCommand cmd = new SqlCommand("ManufacturerV1ProcedureForJQueryAutocomplete", sqlConnection1);

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
            Warehouse135Model Warehouse135ViewModel = new Warehouse135Model();
            DataSet dataset = GetTypeOfMaterialWarehouse();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                selectListItems.Add(new SelectListItem { Value = item["TypeOfMaterial"].ToString(), Text = item["TypeOfMaterial"].ToString() });
            }
            ViewBag.TypeList = selectListItems;
            return View(Warehouse135ViewModel);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Пост создание новых полей склада

        [HttpPost]
        public async Task<IActionResult> CreateWarehouseFields([Bind("Id,TypeOfMaterial,NameOfTypeMaterial,Provider,Manufacturer")] Warehouse135Model Warehouse135View)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                    {
                        sqlConnection.Open();
                        SqlCommand command = new SqlCommand("WarehouseParametersV1", sqlConnection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                       
                        command.Parameters.AddWithValue("incomin_TypeOfMaterial", Warehouse135View.TypeOfMaterial);
                        command.Parameters.AddWithValue("incomin_NameOfTypeMaterial", Warehouse135View.NameOfTypeMaterial);
                        command.Parameters.AddWithValue("incomin_Provider", Warehouse135View.Provider);
                        command.Parameters.AddWithValue("incomin_Manufacturer", Warehouse135View.Manufacturer);                      
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
            Warehouse135Model Warehouse135ViewModel = new Warehouse135Model();
           
            return View(Warehouse135ViewModel);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Пост создания нового типа и полей склада

        [Authorize(Roles = "Moderator, Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateWarehouseFieldType([Bind("Id,TypeOfMaterial,NameOfTypeMaterial,Provider,Manufacturer")] Warehouse135Model Warehouse135View)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                    {
                        sqlConnection.Open();
                        SqlCommand command = new SqlCommand("WarehouseParametersV1", sqlConnection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        command.Parameters.AddWithValue("incomin_TypeOfMaterial", Warehouse135View.TypeOfMaterial);
                        command.Parameters.AddWithValue("incomin_NameOfTypeMaterial", Warehouse135View.NameOfTypeMaterial);
                        command.Parameters.AddWithValue("incomin_Provider", Warehouse135View.Provider);
                        command.Parameters.AddWithValue("incomin_Manufacturer", Warehouse135View.Manufacturer);
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

        // Начало. Вьюха всего склада

        [HttpGet]
        public async Task<IActionResult> WarehouseViewAll(Warehouse135Model Warehouse135View)
        {
            if (ModelState.IsValid)
            {
                List<string> HalfTime = new List<string>();
                await using (SqlConnection sqlConnection1 = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection1.Open();
                    SqlCommand cmd = new SqlCommand("WarehouseDistinctV1", sqlConnection1);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            HalfTime.Add(Convert.ToString(reader.GetString(0)));
                        }
                    }
                    sqlConnection1.Close();
                }
          
                List<DataTable> HalfTimeDT = new List<DataTable>();

                await using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                { 
                   foreach (var item in HalfTime)
                   {
                        DataTable tdbl = new DataTable();
                        sqlConnection.Open();
                        SqlDataAdapter sqlDA = new SqlDataAdapter("WarehouseShowV1", sqlConnection);
                        sqlDA.SelectCommand.Parameters.AddWithValue("incoming_Type", item);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.Fill(tdbl);
                        HalfTimeDT.Add(tdbl);
                        sqlConnection.Close();
                   }
                }

                Warehouse135View.listil = HalfTimeDT;
                Warehouse135View.list = HalfTime.ToArray();

                return View(Warehouse135View);
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

    }
}
