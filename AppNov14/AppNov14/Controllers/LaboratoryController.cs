using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AppNov14.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AppNov14.Controllers
{
    public class LaboratoryController : Controller
    {
        // Начало. Конфигурация для строки подключения

        private readonly IConfiguration _configuration;

        public LaboratoryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        // Начало. Получение уникаольных значений типа материала для JQuery Cascade Dropdown list

        [NonAction]
        public DataSet GetTypeOfMaterialDB()
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

        // Начало. Вьюха добавления сырья на склад. Здесь формируем вьюбэг для Типа материла

        [HttpGet]
        public IActionResult CreateIncomingOrderLab()
        {
            Laboratory laboratory = new Laboratory();
            DataSet dataset = GetTypeOfMaterialDB();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                selectListItems.Add(new SelectListItem { Value = item["TypeOfMaterial"].ToString(), Text = item["TypeOfMaterial"].ToString() });
            }
            ViewBag.TypeList = selectListItems;

            return View(laboratory);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Вьюха отправления сырья со склада на производство. Здесь формируем вьюбэг для Типа материла

        [HttpGet]
        public IActionResult CreateOutcomingOrderLab()
        {
            Laboratory laboratory = new Laboratory();
            DataSet dataset = GetTypeOfMaterialDB();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                selectListItems.Add(new SelectListItem { Value = item["TypeOfMaterial"].ToString(), Text = item["TypeOfMaterial"].ToString() });
            }
            ViewBag.TypeList = selectListItems;

            List<SelectListItem> selectListItemsPartiesNames = new List<SelectListItem>
            {
                new SelectListItem() { Text = "K6", Value = "K6" },
                new SelectListItem() { Text = "K7", Value = "K7" },
                new SelectListItem() { Text = "K8", Value = "K8" }
            };
            laboratory.listyPartiesNames = selectListItemsPartiesNames;

            return View(laboratory);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Пост добавления сырья на склад

        [HttpPost]
        public async Task<IActionResult> CreateIncomingOrderLab([Bind("Id,TypeOfMaterial,NameOfTypeMaterial,Quantity, Provider,Manufacturer,NumberOfDocument,Indexation,DocDate,Employee,IpAdress,AutoDate,Remarks, OperationType")] MainTableDBIncoming TableView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                    {
                        sqlConnection.Open();
                        SqlCommand command = new SqlCommand("NewTestProcedure1", sqlConnection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        command.Parameters.AddWithValue("incomin_Gild", TableView.Gild);
                        command.Parameters.AddWithValue("incomin_TypeOfMaterial", TableView.TypeOfMaterial);
                        command.Parameters.AddWithValue("incomin_NameOfTypeMaterial", TableView.NameOfTypeMaterial);
                        command.Parameters.AddWithValue("incomin_Quantity", TableView.Quantity);
                        command.Parameters.AddWithValue("incomin_Provider", TableView.Provider);
                        command.Parameters.AddWithValue("incomin_Manufacturer", TableView.Manufacturer);
                        command.Parameters.AddWithValue("incomin_Document", "ТНН");
                        command.Parameters.AddWithValue("incomin_NumberOfDocument", TableView.NumberOfDocument);
                        command.Parameters.AddWithValue("incomin_Indexation", TableView.Indexation);
                        command.Parameters.AddWithValue("incomin_DocDate", TableView.DocDate);
                        command.Parameters.AddWithValue("incomin_Employee", User.Identity.Name);
                        command.Parameters.AddWithValue("incomin_IpAdress", Request.HttpContext.Connection.RemoteIpAddress.ToString());
                        command.Parameters.AddWithValue("incomin_AutoDate", DateTime.Now);
                        command.Parameters.AddWithValue("incomin_Remarks", TableView.Remarks ??= "1");
                        command.Parameters.AddWithValue("incomin_OperationType", 1);
                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                return RedirectToAction(nameof(CreateIncomingOrderLab));
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Пост отправления сырья со склада на производство

        [HttpPost]
        public async Task<IActionResult> CreateOutcomingOrderLab([Bind("Id,TypeOfMaterial,NameOfTypeMaterial,Quantity,Leftovers,Provider,Manufacturer,Document,NumberOfDocument,Indexation,DocDate,Employee,IpAdress,AutoDate,Remarks, OperationType")] MainTableDB TableView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                    {
                        sqlConnection.Open();
                        SqlCommand command = new SqlCommand("NewTestProcedure1", sqlConnection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        command.Parameters.AddWithValue("incomin_Gild", TableView.Gild);
                        command.Parameters.AddWithValue("incomin_TypeOfMaterial", TableView.TypeOfMaterial);
                        command.Parameters.AddWithValue("incomin_NameOfTypeMaterial", TableView.NameOfTypeMaterial);
                        command.Parameters.AddWithValue("incomin_Quantity", TableView.Quantity);
                        command.Parameters.AddWithValue("incomin_Provider", TableView.Provider);
                        command.Parameters.AddWithValue("incomin_Manufacturer", TableView.Manufacturer);
                        command.Parameters.AddWithValue("incomin_Document", TableView.Document);
                        command.Parameters.AddWithValue("incomin_NumberOfDocument", TableView.NumberOfDocument);
                        command.Parameters.AddWithValue("incomin_Indexation", TableView.Indexation);
                        command.Parameters.AddWithValue("incomin_DocDate", TableView.DocDate);
                        command.Parameters.AddWithValue("incomin_Employee", User.Identity.Name);
                        command.Parameters.AddWithValue("incomin_IpAdress", Request.HttpContext.Connection.RemoteIpAddress.ToString());
                        command.Parameters.AddWithValue("incomin_AutoDate", DateTime.Now);
                        command.Parameters.AddWithValue("incomin_Remarks", TableView.Remarks ??= "1");
                        command.Parameters.AddWithValue("incomin_OperationType", 2);
                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                return RedirectToAction(nameof(CreateOutcomingOrderLab));
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Вьюха последних 20 добавлений всех сотрудников

        [HttpGet]
        public async Task<IActionResult> LastAddingsLab()
        {
            if (ModelState.IsValid)
            {
                DataTable tdbl = new DataTable();
                await using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter("ShowAllLastData", sqlConnection);
                    sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDA.Fill(tdbl);
                }
                return View(tdbl);
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

    }
}
