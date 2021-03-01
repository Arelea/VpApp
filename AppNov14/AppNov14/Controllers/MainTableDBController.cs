using AppNov14.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppNov14.Controllers
{
    [Authorize]
    public class MainTableDBController : Controller
    {
        // Начало. Конфигурация для строки подключения

        private readonly IConfiguration _configuration;

        public MainTableDBController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

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

        // Начало. Получение уникаольных значений имени типа материала для JQuery Cascade Dropdown list

        [NonAction]
        public DataSet GetNameOfTypeMaterialDB(string type, int SubsType)
        {
            DataSet dataset = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("JQ_MultiController_GetDistinct_NameOfTypeMaterial_DependentByTypeOfMaterial", connection);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("in_TypeOfMaterial", type);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("in_SubsType", SubsType);
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

        // Начало. Получение уникаольных значений поставщика (провайдера) для JQuery Cascade Dropdown list

        [NonAction]
        public DataSet GetProviderDB(string type, string typename, int SubsType)
        {
            try
            {
                DataSet dataset = new DataSet();
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("ProviderV1ProcedureForJQuery", connection);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("incomin_TypeOfMaterial", type);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("incomin_NameOfTypeMaterial", typename);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("in_SubsType", SubsType);
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

        // Начало. Получение уникаольных значений производителя (Manufacturer) для JQuery Cascade Dropdown list

        [NonAction]
        public DataSet GetManufacturerDB(string type, string typename, string provider, int SubsType)
        {
            try
            {
                DataSet dataset = new DataSet();
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("ManufacturerV1ProcedureForJQuery", connection);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("in_TypeOfMaterial", type);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("in_NameOfTypeMaterial", typename);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("in_Provider", provider);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("in_SubsType", SubsType);
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

        // Начало. Получение уникаольных значений производителя (Manufacturer) для JQuery Cascade Dropdown list

        [NonAction]
        public DataSet GetIndexationDB(string type, string typename, string provider, string manufacturer, int subsType)
        {
            try
            {
                DataSet dataset = new DataSet();
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("JQ_MainTableDB_GetIndexation", connection);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("in_TypeOfMaterial", type);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("in_NameOfTypeMaterial", typename);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("in_Provider", provider);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("in_Manufacturer", manufacturer);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("in_SubsType", subsType);
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

        // Начало. Json для имени типа материала для JQuery Cascade Dropdown list

        [HttpGet]
        public JsonResult GetNameTypes(string type, int subsType)
        {
            DataSet dataset = GetNameOfTypeMaterialDB(type, subsType);
            List<SelectListItem> selectListItems1 = new List<SelectListItem>();
            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                selectListItems1.Add(new SelectListItem { Value = item["NameOfTypeMaterial"].ToString(), Text = item["NameOfTypeMaterial"].ToString() });
            }
            return Json(selectListItems1);          
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Json для поставщика (Провайдера) для JQuery Cascade Dropdown list

        [HttpGet]
        public JsonResult GetProvider(string type, string typename, int subsType)
        {
            DataSet dataset = GetProviderDB(type, typename, subsType);
            List<SelectListItem> selectListItems2 = new List<SelectListItem>();
            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                selectListItems2.Add(new SelectListItem { Value = item["Provider"].ToString(), Text = item["Provider"].ToString() });
            }
            return Json(selectListItems2);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Json для производителя (Manufacturer) для JQuery Cascade Dropdown list

        [HttpGet]
        public JsonResult GetManufacturer(string type, string typename, string provider, int subsType)
        {
            DataSet dataset = GetManufacturerDB(type, typename, provider, subsType);
            List<SelectListItem> selectListItems3 = new List<SelectListItem>();
            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                selectListItems3.Add(new SelectListItem { Value = item["Manufacturer"].ToString() , Text= item["Manufacturer"].ToString() });
            }
            return Json(selectListItems3);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Json для идексации (Indexation) для JQuery Cascade Dropdown list

        [HttpGet]
        public JsonResult GetIndexation(string type, string typename, string provider, string manufacturer, int subsType)
        {
            DataSet dataset = GetIndexationDB(type, typename, provider, manufacturer, subsType);
            List<SelectListItem> selectListItems4 = new List<SelectListItem>();
            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                selectListItems4.Add(new SelectListItem { Value = item["Indexation"].ToString(), Text = item["Indexation"].ToString() });
            }
            return Json(selectListItems4);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Вьюха главной страницы.

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Вьюха добавления сырья на склад. Здесь формируем вьюбэг для Типа материла

        [HttpGet]
        public IActionResult CreateIncomingOrder()
        {
            MainTableDBIncoming TableViewModel = new MainTableDBIncoming();
            DataSet dataset = GetTypeOfMaterialDB();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                selectListItems.Add(new SelectListItem { Value = item["TypeOfMaterial"].ToString(), Text = item["TypeOfMaterial"].ToString() });
            }
            ViewBag.TypeList = selectListItems;

            TableViewModel.Gild = 135;

            return View(TableViewModel);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Вьюха отправления сырья со склада на производство. Здесь формируем вьюбэг для Типа материла

        [HttpGet]
        public IActionResult CreateOutcomingOrder()
        {
            MainTableDB TableViewModel = new MainTableDB();
            DataSet dataset = GetTypeOfMaterialDB();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                selectListItems.Add(new SelectListItem { Value = item["TypeOfMaterial"].ToString(), Text = item["TypeOfMaterial"].ToString() });
            }
            ViewBag.TypeList = selectListItems;

            List<SelectListItem> selectListItemsLine = new List<SelectListItem>();
            selectListItemsLine.Add(new SelectListItem() { Text = "Bolshevik", Value = "Bolshevik" });
            selectListItemsLine.Add(new SelectListItem() { Text = "Xinda1", Value = "Xinda1" });
            selectListItemsLine.Add(new SelectListItem() { Text = "Biersdorff", Value = "Biersdorff" });

            TableViewModel.listyLine = selectListItemsLine;

            DataSet datasetPartiesNames = GetNameOfParty();
            List<SelectListItem> selectListItemsPartiesNames = new List<SelectListItem>();
            foreach (DataRow item in datasetPartiesNames.Tables[0].Rows)
            {
                selectListItemsPartiesNames.Add(new SelectListItem { Value = item["Name"].ToString(), Text = item["Name"].ToString() });
            }
            TableViewModel.listyPartiesNames = selectListItemsPartiesNames;

            TableViewModel.Gild = 135;
            
            return View(TableViewModel);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Вьюха возвращения сырья с производства на склад. Здесь формируем вьюбэг для Типа материла

        [HttpGet]
        public IActionResult CreateReturnOrder()
        {
            MainTableDB TableViewModel = new MainTableDB();
            DataSet dataset = GetTypeOfMaterialDB();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                selectListItems.Add(new SelectListItem { Value = item["TypeOfMaterial"].ToString(), Text = item["TypeOfMaterial"].ToString() });
            }
            ViewBag.TypeList = selectListItems;

            DataSet datasetParties = GetParties(2);
            List<SelectListItem> selectListItemsParties = new List<SelectListItem>();
            foreach (DataRow item in datasetParties.Tables[0].Rows)
            {
                selectListItemsParties.Add(new SelectListItem { Value = item["Name"].ToString(), Text = item["Name"].ToString() });
            }
            TableViewModel.listyParties = selectListItemsParties;

            List<SelectListItem> selectListItemsLine = new List<SelectListItem>();
            selectListItemsLine.Add(new SelectListItem() { Text = "Bolshevik", Value = "Bolshevik" });
            selectListItemsLine.Add(new SelectListItem() { Text = "Xinda1", Value = "Xinda1" });
            selectListItemsLine.Add(new SelectListItem() { Text = "Biersdorff", Value = "Biersdorff" });

            TableViewModel.listyLine = selectListItemsLine;

            DataSet datasetPartiesNames = GetNameOfParty();
            List<SelectListItem> selectListItemsPartiesNames = new List<SelectListItem>();
            foreach (DataRow item in datasetPartiesNames.Tables[0].Rows)
            {
                selectListItemsPartiesNames.Add(new SelectListItem { Value = item["Name"].ToString(), Text = item["Name"].ToString() });
            }
            TableViewModel.listyPartiesNames = selectListItemsPartiesNames;

            TableViewModel.Gild = 135;

            return View(TableViewModel);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Пост добавления сырья на склад

        [HttpPost]
        public async Task<IActionResult> CreateIncomingOrder([Bind("Id,Gild,TypeOfMaterial,NameOfTypeMaterial,Quantity, Provider,Manufacturer,NumberOfDocument,Indexation,DocDate,Employee,IpAdress,AutoDate,Remarks, OperationType")] MainTableDBIncoming TableView)
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
                        command.Parameters.AddWithValue("incomin_Line", "1");
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
                return RedirectToAction(nameof(CreateIncomingOrder));
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Пост отправления сырья со склада на производство

        [HttpPost]
        public async Task<IActionResult> CreateOutcomingOrder([Bind("Id,Gild,TypeOfMaterial,NameOfTypeMaterial,Quantity,Leftovers,Provider,Manufacturer,Document,NumberOfDocument,Indexation,Line,DocDate,Employee,IpAdress,AutoDate,Remarks, OperationType")] MainTableDB TableView)
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
                        command.Parameters.AddWithValue("incomin_Line", TableView.Line);
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
                return RedirectToAction(nameof(CreateOutcomingOrder));
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Пост возвращения сырья с производства на склад

        [HttpPost]
        public async Task<IActionResult> CreateReturnOrder([Bind("Id,Gild,TypeOfMaterial,NameOfTypeMaterial,Quantity,Leftovers,Provider,Manufacturer,Document,NumberOfDocument,Indexation,Line,DocDate,Employee,IpAdress,AutoDate,Remarks, OperationType")] MainTableDB TableView)
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
                        command.Parameters.AddWithValue("incomin_Line", TableView.Line);
                        command.Parameters.AddWithValue("incomin_DocDate", TableView.DocDate);
                        command.Parameters.AddWithValue("incomin_Employee", User.Identity.Name);
                        command.Parameters.AddWithValue("incomin_IpAdress", Request.HttpContext.Connection.RemoteIpAddress.ToString());
                        command.Parameters.AddWithValue("incomin_AutoDate", DateTime.Now);
                        command.Parameters.AddWithValue("incomin_Remarks", TableView.Remarks ??= "1");
                        command.Parameters.AddWithValue("incomin_OperationType", 3);
                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                return RedirectToAction(nameof(CreateReturnOrder));
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Вьюха последних 10 добавлений сотрудника

        [HttpGet]
        public async Task<IActionResult> MyLastAddings()
        {
            if (ModelState.IsValid)
            {
                DataTable tdbl = new DataTable();
                await using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter("BasApp_MainTableDB_ShowEmpLastData", sqlConnection);
                    sqlDA.SelectCommand.Parameters.AddWithValue("in_Employee", User.Identity.Name);
                    sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDA.Fill(tdbl);
                }
                return View(tdbl);
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Вьюха последних 25 добавлений всех сотрудников

        [HttpGet]
        public async Task<IActionResult> EveryoneLastAddings()
        {
            if (ModelState.IsValid)
            {
                DataTable tdbl = new DataTable();
                await using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter("BasApp_MainTableDB_ShowData", sqlConnection);          
                    sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;             
                    sqlDA.Fill(tdbl);                 
                }
                return View(tdbl);
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Вьюха компонента удаления и редактирования записей с поиском

        [Authorize(Roles = "Moderator, Admin")]
        [HttpGet]
        public IActionResult EditDeleteAllData(DateTime dateStart, DateTime dateFinish, int? numberOfRecords)
        {
            try
            {
                InnerModelToPost TableViewModel = new InnerModelToPost();
                if (dateStart == null || dateFinish == null || numberOfRecords == null)
                {
                    dateStart = DateTime.Now.AddMonths(-1);
                    dateFinish = DateTime.Now.AddDays(1);
                    numberOfRecords = 200;
                }
                                 
                DataTable tdbl = new DataTable();
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {                     
                    sqlConnection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter("BasApp_MultiController_ShowDataForEditOrDelete", sqlConnection);
                    sqlDA.SelectCommand.Parameters.AddWithValue("in_Id", numberOfRecords);
                    sqlDA.SelectCommand.Parameters.AddWithValue("in_DateStart", dateStart);
                    sqlDA.SelectCommand.Parameters.AddWithValue("in_DateFinish", dateFinish);
                    sqlDA.SelectCommand.Parameters.AddWithValue("in_SubsType", 1);
                    sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDA.Fill(tdbl);
                }
                TableViewModel.DateStart = dateStart;
                TableViewModel.DateFinish = dateFinish;
                TableViewModel.NumberOfRecords = (int)numberOfRecords;

                TableViewModel.listil = tdbl;
                return View(TableViewModel);          
            }
            catch(SqlException ex)
            {
                throw ex;
            }
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Пост компонента удаления и редактирования записей с поиском. Получаем дату от и до, и количество -
        // - последних записей. Передаем их во вьюху.

        [Authorize(Roles = "Moderator, Admin")]
        [HttpPost]
        public IActionResult EditDeleteAllData(InnerModelToPost TableViewModel)
        {
            if (ModelState.IsValid)
            {
                EditDeleteAllData(TableViewModel.DateStart, TableViewModel.DateFinish, TableViewModel.NumberOfRecords);
                return View();
            }
            return View(ModelState.ErrorCount);          
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Вьюха компонента редактирования записей. Передаем туда значение Id той записи, на которую кликнули -
        // - и получаем форму уже с данными той записи, которая была выбранна

        [Authorize(Roles = "Moderator, Admin")]
        [HttpGet]
        public IActionResult EditTableRow(int? id)
        {
            MainTableDB TableViewModel = new MainTableDB();
            if (id > 0)
            {
                TableViewModel = FetchRecordById(id);
            }
            DataSet datasetType = GetTypeOfMaterialDB();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (DataRow item in datasetType.Tables[0].Rows)
            {
                selectListItems.Add(new SelectListItem { Value = item["TypeOfMaterial"].ToString(), Text = item["TypeOfMaterial"].ToString() });
            }
            ViewBag.TypeList = selectListItems;

            DataSet datasetTypeName = GetNameOfTypeMaterialDB(TableViewModel.TypeOfMaterial, 1);
            List<SelectListItem> selectListItemsTypeName = new List<SelectListItem>();
            foreach (DataRow item in datasetTypeName.Tables[0].Rows)
            {
                selectListItemsTypeName.Add(new SelectListItem { Value = item["NameOfTypeMaterial"].ToString(), Text = item["NameOfTypeMaterial"].ToString() });
            }
            TableViewModel.listyNameType = selectListItemsTypeName;

            DataSet datasetProvider = GetProviderDB(TableViewModel.TypeOfMaterial, TableViewModel.NameOfTypeMaterial, 1);
            List<SelectListItem> selectListItemsProvider = new List<SelectListItem>();
            foreach (DataRow item in datasetProvider.Tables[0].Rows)
            {
                selectListItemsProvider.Add(new SelectListItem { Value = item["Provider"].ToString(), Text = item["Provider"].ToString() });
            }
            TableViewModel.listyProvider = selectListItemsProvider;

            DataSet datasetManufacturer = GetManufacturerDB(TableViewModel.TypeOfMaterial, TableViewModel.NameOfTypeMaterial, TableViewModel.Provider, 1);
            List<SelectListItem> selectListItemsManufacturer = new List<SelectListItem>();
            foreach (DataRow item in datasetManufacturer.Tables[0].Rows)
            {
                selectListItemsManufacturer.Add(new SelectListItem { Value = item["Manufacturer"].ToString(), Text = item["Manufacturer"].ToString() });
            }
            TableViewModel.listyManufacturer = selectListItemsManufacturer;

            DataSet datasetPartiesNames = GetNameOfParty();
            List<SelectListItem> selectListItemsPartiesNames = new List<SelectListItem>();
            foreach (DataRow item in datasetPartiesNames.Tables[0].Rows)
            {
                selectListItemsPartiesNames.Add(new SelectListItem { Value = item["Name"].ToString(), Text = item["Name"].ToString() });
            }
            TableViewModel.listyPartiesNames = selectListItemsPartiesNames;

            int Actiontype = 0;

            if (TableViewModel.OperationType == 1)
            {
                Actiontype = 1;
            }
            else if (TableViewModel.OperationType == 2 || TableViewModel.OperationType == 3)
            {
                Actiontype = 2;
            }
            else
            {

            }
            DataSet datasetParties = GetParties(Actiontype);
            List<SelectListItem> selectListItemsParties = new List<SelectListItem>();
            foreach (DataRow item in datasetParties.Tables[0].Rows)
            {
                selectListItemsParties.Add(new SelectListItem { Value = item["Name"].ToString(), Text = item["Name"].ToString() });
            }
            TableViewModel.listyParties = selectListItemsParties;

            List<SelectListItem> selectListItemsLine = new List<SelectListItem>();
            selectListItemsLine.Add(new SelectListItem() { Text = "Bolshevik", Value = "Bolshevik" });
            selectListItemsLine.Add(new SelectListItem() { Text = "Xinda1", Value = "Xinda1" });
            selectListItemsLine.Add(new SelectListItem() { Text = "Biersdorff", Value = "Biersdorff" });

            TableViewModel.listyLine = selectListItemsLine;

            return View(TableViewModel);
          
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Пост редактирования

        [Authorize(Roles = "Moderator, Admin")]
        [HttpPost]
        public async Task<IActionResult> EditTableRow([Bind("Id,Gild,TypeOfMaterial,NameOfTypeMaterial,Quantity,Leftovers,Provider,Manufacturer,Document,NumberOfDocument,Indexation,Line,DocDate,Employee,IpAdress,AutoDate,Remarks")] MainTableDB TableView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                    {
                        sqlConnection.Open();
                        SqlCommand command = new SqlCommand("NewTestProcedure2Update", sqlConnection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        command.Parameters.AddWithValue("incomin_Id", TableView.Id);
                        command.Parameters.AddWithValue("incomin_Gild", TableView.Gild);
                        command.Parameters.AddWithValue("incomin_TypeOfMaterial", TableView.TypeOfMaterial);
                        command.Parameters.AddWithValue("incomin_NameOfTypeMaterial", TableView.NameOfTypeMaterial);
                        command.Parameters.AddWithValue("incomin_Quantity", TableView.Quantity);
                        command.Parameters.AddWithValue("incomin_Provider", TableView.Provider);
                        command.Parameters.AddWithValue("incomin_Manufacturer", TableView.Manufacturer);
                        command.Parameters.AddWithValue("incomin_Document", TableView.Document);
                        command.Parameters.AddWithValue("incomin_NumberOfDocument", TableView.NumberOfDocument);
                        command.Parameters.AddWithValue("incomin_Indexation", TableView.Indexation);
                        command.Parameters.AddWithValue("incomin_Line", TableView.Line);
                        command.Parameters.AddWithValue("incomin_DocDate", TableView.DocDate);
                        command.Parameters.AddWithValue("incomin_Employee", User.Identity.Name);
                        command.Parameters.AddWithValue("incomin_IpAdress", Request.HttpContext.Connection.RemoteIpAddress.ToString());
                        command.Parameters.AddWithValue("incomin_Remarks", TableView.Remarks ??= "1");
                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                return RedirectToAction(nameof(EditDeleteAllData));
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Вьюха удаления. Получаем значения той записи, котору. хотим удалить по Id

        [Authorize(Roles = "Moderator, Admin")]
        [HttpGet]
        public IActionResult DeleteTableRow(int? id)
        {
            MainTableDB TableViewModel = FetchRecordById(id);
            return View(TableViewModel);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Пост удаления

        [Authorize(Roles = "Moderator, Admin")]
        [HttpPost, ActionName("DeleteTableRow")]
        public async Task<IActionResult> DeleteTableRowConfirm(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                    {
                        sqlConnection.Open();
                        SqlCommand command = new SqlCommand("NewTestProcedure3Delete", sqlConnection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        command.Parameters.AddWithValue("incomin_Id", id);                      
                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                return RedirectToAction(nameof(EditDeleteAllData));
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Ищем запись по Id для удаления или редактирования и возвращаем ее поля

        [NonAction]
        public MainTableDB FetchRecordById(int? id)
        {
            MainTableDB mainTableDB = new MainTableDB();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    DataTable dataTable = new DataTable();
                   
                    sqlConnection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter("ViewRecordById", sqlConnection);
                    sqlDA.SelectCommand.Parameters.AddWithValue("in_Id", id);
                    sqlDA.SelectCommand.Parameters.AddWithValue("in_SubsType", 1);
                    sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDA.Fill(dataTable);
                    if (dataTable.Rows.Count == 1)
                    {
                        mainTableDB.Id = Convert.ToInt32(dataTable.Rows[0]["Id"].ToString());

                        mainTableDB.Gild = Convert.ToInt32(dataTable.Rows[0]["Gild"].ToString());

                        mainTableDB.TypeOfMaterial = dataTable.Rows[0]["TypeOfMaterial"].ToString();

                        mainTableDB.NameOfTypeMaterial = dataTable.Rows[0]["NameOfTypeMaterial"].ToString();

                        mainTableDB.Quantity = Convert.ToDecimal(dataTable.Rows[0]["Quantity"].ToString());

                        mainTableDB.Leftovers = Convert.ToDecimal(dataTable.Rows[0]["Leftovers"].ToString());

                        mainTableDB.Provider = dataTable.Rows[0]["Provider"].ToString();

                        mainTableDB.Manufacturer = dataTable.Rows[0]["Manufacturer"].ToString();

                        mainTableDB.Document = dataTable.Rows[0]["Document"].ToString();

                        mainTableDB.NumberOfDocument = dataTable.Rows[0]["NumberOfDocument"].ToString();

                        mainTableDB.Indexation = dataTable.Rows[0]["Indexation"].ToString();

                        mainTableDB.Line = dataTable.Rows[0]["Line"].ToString();

                        mainTableDB.DocDate = Convert.ToDateTime(dataTable.Rows[0]["DocDate"].ToString());

                        mainTableDB.Employee = dataTable.Rows[0]["Employee"].ToString();

                        mainTableDB.IpAdress = dataTable.Rows[0]["IpAdress"].ToString();

                        mainTableDB.AutoDate = Convert.ToDateTime(dataTable.Rows[0]["AutoDate"].ToString());

                        mainTableDB.Remarks = dataTable.Rows[0]["Remarks"].ToString();

                        mainTableDB.OperationType = Convert.ToInt32(dataTable.Rows[0]["OperationType"].ToString());
                    }               
                    return mainTableDB;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ 


        [HttpGet]
        public async Task<IActionResult> ImportExcelFile(DateTime dateStart, DateTime dateFinish)
        {
            try
            {
                InnerModelToPost TableViewModel = new InnerModelToPost();
                if (dateStart == DateTime.MinValue || dateFinish == DateTime.MinValue)
                {
                    dateStart = DateTime.Now.AddMonths(-1);
                    dateFinish = DateTime.Now.AddDays(1);
                }

                int count = 0;

                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("_Excel_GetNumberOfRecords", sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("in_DateStart", dateStart);
                    cmd.Parameters.AddWithValue("in_DateFinish", dateFinish);
                    cmd.Parameters.AddWithValue("in_SubsType", 1);

                    SqlParameter parameter = cmd.Parameters.Add("out_Count", SqlDbType.Int);
                    parameter.Direction = ParameterDirection.ReturnValue;
                    await cmd.ExecuteNonQueryAsync();
                    count = (int)cmd.Parameters["out_Count"].Value;                           
                }
                
                TableViewModel.Count = count;
                TableViewModel.DateStart = dateStart;
                TableViewModel.DateFinish = dateFinish;

                return View(TableViewModel);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        [HttpPost]
        public async Task<IActionResult> ImportExcelFile(InnerModelToPost TableViewModel)
        {
            if (ModelState.IsValid)
            {
                await ImportExcelFile(TableViewModel.DateStart, TableViewModel.DateFinish);
                return View();
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
        public IActionResult DownloadExcel(string dateStart, string dateFinish)
        {
            DataTable tdbl = new DataTable();

            try
            {
                InnerModelToPost TableViewModel = new InnerModelToPost();               
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter("_Excel_GetData", sqlConnection);
                    sqlDA.SelectCommand.Parameters.AddWithValue("incoming_DateStart", Convert.ToDateTime(dateStart));
                    sqlDA.SelectCommand.Parameters.AddWithValue("incoming_DateFinish", Convert.ToDateTime(dateFinish));
                    sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDA.Fill(tdbl);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            var stream = new MemoryStream();      

            using (ExcelPackage package = new ExcelPackage(stream))
            {                      
                var worksheet = package.Workbook.Worksheets.Add("Listy1");
                worksheet.Cells.LoadFromDataTable(tdbl, true);
                worksheet.Column(13).Style.Numberformat.Format = "dd:mm:yyyy hh:mm:ss";
                worksheet.Column(16).Style.Numberformat.Format = "dd:mm:yyyy hh:mm:ss";
                worksheet.Cells.AutoFitColumns();
                worksheet.Cells[1, 1, 1, 18].AutoFilter = true;

                using (ExcelRange heading = worksheet.Cells[1,1,1,18])
                {
                    var colory = heading.Style.Fill;
                    colory.PatternType = ExcelFillStyle.Solid;
                    colory.BackgroundColor.SetColor(Color.LightBlue);
                    heading.Style.Border.BorderAround(ExcelBorderStyle.Thick);
                
                }

                //using (ExcelRange body = worksheet.Cells[2, 1, tdbl.Rows.Count + 1, 18])
                //{
                //    var colory = body.Style.Fill;
                //    colory.PatternType = ExcelFillStyle.LightDown;
                //    colory.BackgroundColor.SetColor(Color.LightGreen);
                //}

                package.Save();           
            }

            stream.Position = 0;
            string excelname = $"VestPlast.DataProd.{DateTime.Now}.xlsx";

            return File(stream, "application/vnd.openformats-officedocument.spreadsheetml.sheet", excelname);       
        }

        [NonAction]
        public DataSet GetParties(int TypeOfAction)
        {
            DataSet dataset = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("_JQParties", connection);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("incomin_OperationType", TypeOfAction);
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

        // Начало. Вьюха для получения состава партий

        [HttpGet]
        public IActionResult GetPartiesInfo(string numberOfDocument)
        {

            try
            {
                MainTableParties mainTableDB = new MainTableParties();

                DataSet datasetParties = GetParties(2);
                List<SelectListItem> selectListItemsParties = new List<SelectListItem>();
                selectListItemsParties.Add(new SelectListItem { Value = null, Text = "-- Не выбрано --" });
                foreach (DataRow item in datasetParties.Tables[0].Rows)
                {
                    selectListItemsParties.Add(new SelectListItem { Value = item["Name"].ToString(), Text = item["Name"].ToString() });
                }
                mainTableDB.listyParties = selectListItemsParties;


                mainTableDB.NumberOfDocument = numberOfDocument;
              

                DataTable tdbl = new DataTable();
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter("_JQParties_Info", sqlConnection);
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_NumberOfDocument", numberOfDocument ??= "Null");
                    sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDA.Fill(tdbl);
                }

                mainTableDB.listil = tdbl;
                return View(mainTableDB);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Пост для получения состава партий

        [HttpPost]
        public IActionResult GetPartiesInfo(MainTableParties mainTableDB)
        {
            if (ModelState.IsValid)
            {
                GetPartiesInfo(mainTableDB.NumberOfDocument);
                return View();
            }
            return View(ModelState.ErrorCount);
        }
        
        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Получения всех уникальных наименований партий

        [NonAction]
        public DataSet GetNameOfParty()
        {
            DataSet dataset = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("_JQPartyNames", connection);
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

        // Начало. JSON для наименнований партий

        [HttpPost]
        public JsonResult GetNamePartyJson(string Prefix)
        {
            try
            {
                List<string> HalfTime = new List<string>();
                using (SqlConnection sqlConnection1 = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection1.Open();
                    SqlCommand cmd = new SqlCommand("_JQPartyNames", sqlConnection1);

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

        // Начало. Добовление наименования партии

        [HttpGet]
        public IActionResult AddPartyName()
        {
            MainTableParties mainTableDB = new MainTableParties();
            return View(mainTableDB);          
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _


        // Начало. Пост добовления наименования партии

        [HttpPost]
        public async Task<IActionResult> AddPartyName(MainTableParties mainTableDB)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                    {
                        sqlConnection.Open();
                        SqlCommand command = new SqlCommand("_AddParty", sqlConnection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        command.Parameters.AddWithValue("incomin_Name", mainTableDB.Document);
                        command.Parameters.AddWithValue("incomin_AutoDate", DateTime.Now);
                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                return RedirectToAction(nameof(AddPartyName));
            }

            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
        
        [HttpGet]
        public IActionResult ViewIndex(string TypeOfMaterial, string NameOfTypeMaterial, string Provider, string Manufacturer)
        {
            MainTableIndex TableViewModel = new MainTableIndex();
            DataSet dataset = GetTypeOfMaterialDB();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                selectListItems.Add(new SelectListItem { Value = item["TypeOfMaterial"].ToString(), Text = item["TypeOfMaterial"].ToString() });
            }
            ViewBag.TypeList = selectListItems;
            
                try
                {
                    DataTable tdbl = new DataTable();
                    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                    {
                        sqlConnection.Open();
                        SqlDataAdapter sqlDA = new SqlDataAdapter("BasApp_MainTableDB_GetIndexInfo", sqlConnection);
                        sqlDA.SelectCommand.Parameters.AddWithValue("in_TypeOfMaterial", TypeOfMaterial ??= "Null");
                        sqlDA.SelectCommand.Parameters.AddWithValue("in_NameOfTypeMaterial", NameOfTypeMaterial ??= "Null");
                        sqlDA.SelectCommand.Parameters.AddWithValue("in_Provider", Provider ??= "Null");
                        sqlDA.SelectCommand.Parameters.AddWithValue("in_Manufacturer", Manufacturer ??= "Null");
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.Fill(tdbl);
                    }

                TableViewModel.listil = tdbl;
                    return View(TableViewModel);
                }
                catch (SqlException ex)
                {
                    throw ex;
                }

        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _


        // Начало. Пост добовления наименования партии

        [HttpPost]
        public IActionResult ViewIndex(MainTableIndex mainTableDB)
        {
            

            if (ModelState.IsValid)
            {
                ViewIndex(mainTableDB.TypeOfMaterial, mainTableDB.NameOfTypeMaterial, mainTableDB.Provider, mainTableDB.Manufacturer);
                return View();
            }

            return View(ModelState.ErrorCount);
        }
    }
    
}

