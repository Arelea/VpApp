using System;
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
     public class WarehouseLabController : Controller
     {
        // Начало. Конфигурация для строки подключения

        private readonly IConfiguration _configuration;

        public WarehouseLabController(IConfiguration configuration)
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
                    dataAdapter.SelectCommand.Parameters.AddWithValue("in_SubsType", 3);
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

        // Начало. Вьюха создание новых полей лабораторного склада

        [HttpGet]
        public IActionResult CreateWarehouseFieldsLab()
        {
            WarehouseLab warehouse = new WarehouseLab();
            DataSet dataset = GetTypeOfMaterialWarehouse();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                selectListItems.Add(new SelectListItem { Value = item["TypeOfMaterial"].ToString(), Text = item["TypeOfMaterial"].ToString() });
            }
            ViewBag.TypeList = selectListItems;
            return View(warehouse);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Пост создание новых полей лабораторного склада

        [HttpPost]
        public async Task<IActionResult> CreateWarehouseFieldsLab([Bind("Id,TypeOfMaterial,NameOfTypeMaterial,Provider,Manufacturer")] WarehouseLab warehouse)
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

                        command.Parameters.AddWithValue("in_TypeOfMaterial", warehouse.TypeOfMaterial);
                        command.Parameters.AddWithValue("in_NameOfTypeMaterial", warehouse.NameOfTypeMaterial);
                        command.Parameters.AddWithValue("in_Provider", warehouse.Provider);
                        command.Parameters.AddWithValue("in_Manufacturer", warehouse.Manufacturer);
                        command.Parameters.AddWithValue("in_SubsType", 2);
                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                return RedirectToAction(nameof(CreateWarehouseFieldsLab));
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Вьюха создания нового типа и полей лабораторного склада

        [Authorize(Roles = "Moderator, Admin")]
        [HttpGet]
        public IActionResult CreateWarehouseFieldTypeLab()
        {
            WarehouseLab warehouse = new WarehouseLab();

            return View(warehouse);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Пост создания нового типа и полей лабораторного склада

        [Authorize(Roles = "Moderator, Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateWarehouseFieldTypeLab([Bind("Id,TypeOfMaterial,NameOfTypeMaterial,Provider,Manufacturer")] WarehouseLab warehouse)
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

                        command.Parameters.AddWithValue("in_TypeOfMaterial", warehouse.TypeOfMaterial);
                        command.Parameters.AddWithValue("in_NameOfTypeMaterial", warehouse.NameOfTypeMaterial);
                        command.Parameters.AddWithValue("in_Provider", warehouse.Provider);
                        command.Parameters.AddWithValue("in_Manufacturer", warehouse.Manufacturer);
                        command.Parameters.AddWithValue("in_SubsType", 2);
                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                return RedirectToAction(nameof(CreateWarehouseFieldTypeLab));
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Вьюха всего склада + лаборатороного

        [HttpGet]
        public async Task<IActionResult> WarehouseViewLab(bool loc_SubsType)
        {
            if (ModelState.IsValid)
            {
                WarehouseLab warehouse = new WarehouseLab();

                List<string> DiffTypes = new List<string>();
                using (SqlConnection sqlConnection1 = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection1.Open();
                    SqlCommand cmd = new SqlCommand("BasApp_Warehouse_GetDistinct", sqlConnection1)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("in_SubsType", loc_SubsType == true ? 3 : 2);

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
                        sqlDA.SelectCommand.Parameters.AddWithValue("in_SubsType", loc_SubsType == true ? 3 : 2);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.Fill(tdbl);
                        DiffDataSets.Add(tdbl);
                        sqlConnection.Close();
                    }
                }

                warehouse.listil = DiffDataSets;
                warehouse.list = DiffTypes.ToArray();

                return View(warehouse);
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Пост лабораторного склада

        [HttpPost]
        public async Task<IActionResult> WarehouseViewLab(WarehouseLab warehouse)
        {
            if (ModelState.IsValid)
            {
                await WarehouseViewLab(warehouse.WarehouseSubs);
                return View();
            }
            return View(ModelState.ErrorCount);
        }
    }
}
