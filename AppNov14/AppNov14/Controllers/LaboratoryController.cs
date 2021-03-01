using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AppNov14.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace AppNov14.Controllers
{
    [Authorize]
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
        public DataSet GetTypeOfMaterialDB(int subsType)
        {
            DataSet dataset = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("JQ_MultiController_GetDistinct_TypeOfMaterial", connection);
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

        // Начало. Получение значений партий

        [NonAction]
        public DataSet GetParties()
        {
            DataSet dataset = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("JQ_Laboratory_PartyNames", connection);
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
            DataSet dataset = GetTypeOfMaterialDB(2);
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
            DataSet dataset = GetTypeOfMaterialDB(3);
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
        public async Task<IActionResult> CreateIncomingOrderLab([Bind("Id,TypeOfMaterial,NameOfTypeMaterial,Quantity, Provider,Manufacturer,Document, NumberOfDocument,Indexation,DocDate,Employee,IpAdress,AutoDate,Remarks, OperationType")] Laboratory laboratory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                    {
                        sqlConnection.Open();
                        SqlCommand command = new SqlCommand("BasApp_Laboratory_AddToLabTable", sqlConnection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        command.Parameters.AddWithValue("incomin_TypeOfMaterial", laboratory.TypeOfMaterial);
                        command.Parameters.AddWithValue("incomin_NameOfTypeMaterial", laboratory.NameOfTypeMaterial);
                        command.Parameters.AddWithValue("incomin_Quantity", laboratory.Quantity);
                        command.Parameters.AddWithValue("incomin_Provider", laboratory.Provider);
                        command.Parameters.AddWithValue("incomin_Manufacturer", laboratory.Manufacturer);
                        command.Parameters.AddWithValue("incomin_Document", "1");
                        command.Parameters.AddWithValue("incomin_NumberOfDocument", "1");
                        command.Parameters.AddWithValue("incomin_Indexation", laboratory.Indexation);
                        command.Parameters.AddWithValue("incomin_DocDate", laboratory.DocDate);
                        command.Parameters.AddWithValue("incomin_Employee", User.Identity.Name);
                        command.Parameters.AddWithValue("incomin_IpAdress", Request.HttpContext.Connection.RemoteIpAddress.ToString());
                        command.Parameters.AddWithValue("incomin_AutoDate", DateTime.Now);
                        command.Parameters.AddWithValue("incomin_Remarks", laboratory.Remarks ??= "1");
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
        public async Task<IActionResult> CreateOutcomingOrderLab([Bind("Id,TypeOfMaterial,NameOfTypeMaterial,Quantity,Leftovers,Provider,Manufacturer,Document,NumberOfDocument,Indexation,DocDate,Employee,IpAdress,AutoDate,Remarks, OperationType")] Laboratory laboratory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                    {
                        sqlConnection.Open();
                        SqlCommand command = new SqlCommand("BasApp_Laboratory_AddToLabTable", sqlConnection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        command.Parameters.AddWithValue("incomin_TypeOfMaterial", laboratory.TypeOfMaterial);
                        command.Parameters.AddWithValue("incomin_NameOfTypeMaterial", laboratory.NameOfTypeMaterial);
                        command.Parameters.AddWithValue("incomin_Quantity", laboratory.Quantity);
                        command.Parameters.AddWithValue("incomin_Provider", laboratory.Provider);
                        command.Parameters.AddWithValue("incomin_Manufacturer", laboratory.Manufacturer);
                        command.Parameters.AddWithValue("incomin_Document", laboratory.Document);
                        command.Parameters.AddWithValue("incomin_NumberOfDocument", laboratory.NumberOfDocument);
                        command.Parameters.AddWithValue("incomin_Indexation", laboratory.Indexation);
                        command.Parameters.AddWithValue("incomin_DocDate", laboratory.DocDate);
                        command.Parameters.AddWithValue("incomin_Employee", User.Identity.Name);
                        command.Parameters.AddWithValue("incomin_IpAdress", Request.HttpContext.Connection.RemoteIpAddress.ToString());
                        command.Parameters.AddWithValue("incomin_AutoDate", DateTime.Now);
                        command.Parameters.AddWithValue("incomin_Remarks", laboratory.Remarks ??= "1");
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

        // Начало. Вьюха последних 25 добавлений всех сотрудников

        [HttpGet]
        public async Task<IActionResult> LastAddingsLab()
        {
            if (ModelState.IsValid)
            {
                DataTable tdbl = new DataTable();
                await using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter("BasApp_Laboratory_ShowData", sqlConnection);
                    sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDA.Fill(tdbl);
                }
                return View(tdbl);
            }
            return View(ModelState.ErrorCount);
        }

        // Начало. Вьюха компонента удаления и редактирования записей с поиском

        [Authorize(Roles = "Moderator, Admin")]
        [HttpGet]
        public IActionResult EditDeleteAllDataLab(DateTime dateStart, DateTime dateFinish, int? numberOfRecords)
        {
            try
            {
                InnerModelToPost TableViewModel = new InnerModelToPost();
                if (dateStart == DateTime.MinValue || dateFinish == DateTime.MinValue || numberOfRecords == null)
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
                    sqlDA.SelectCommand.Parameters.AddWithValue("in_SubsType", 2);
                    sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDA.Fill(tdbl);
                }
                TableViewModel.DateStart = dateStart;
                TableViewModel.DateFinish = dateFinish;
                TableViewModel.NumberOfRecords = (int)numberOfRecords;

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

        // Начало. Пост компонента удаления и редактирования записей с поиском. Получаем дату от и до, и количество -
        // - последних записей. Передаем их во вьюху.

        [Authorize(Roles = "Moderator, Admin")]
        [HttpPost]
        public IActionResult EditDeleteAllDataLab(InnerModelToPost TableViewModel)
        {
            if (ModelState.IsValid)
            {
                EditDeleteAllDataLab(TableViewModel.DateStart, TableViewModel.DateFinish, TableViewModel.NumberOfRecords);
                return View();
            }
            return View(ModelState.ErrorCount);
        }

        //Конец
        //  _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало.Вьюха компонента редактирования записей.Передаем туда значение Id той записи, на которую кликнули -
        // - и получаем форму уже с данными той записи, которая была выбранна

       [Authorize(Roles = "Moderator, Admin")]
       [HttpGet]
        public IActionResult EditTableRowLab(int? id, int operationType)
        {
            Laboratory laboratory = new Laboratory();
            if (id > 0)
            {
                laboratory = FetchRecordById(id);
            }

            DataSet datasetType = GetTypeOfMaterialDB(operationType + 1);
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (DataRow item in datasetType.Tables[0].Rows)
            {
                selectListItems.Add(new SelectListItem { Value = item["TypeOfMaterial"].ToString(), Text = item["TypeOfMaterial"].ToString() });
            }
            ViewBag.TypeList = selectListItems;

            DataSet datasetTypeName = GetNameOfTypeMaterialDB(laboratory.TypeOfMaterial, 3);
            List<SelectListItem> selectListItemsTypeName = new List<SelectListItem>();
            foreach (DataRow item in datasetTypeName.Tables[0].Rows)
            {
                selectListItemsTypeName.Add(new SelectListItem { Value = item["NameOfTypeMaterial"].ToString(), Text = item["NameOfTypeMaterial"].ToString() });
            }
            laboratory.listyNameType = selectListItemsTypeName;

            DataSet datasetProvider = GetProviderDB(laboratory.TypeOfMaterial, laboratory.NameOfTypeMaterial, 3);
            List<SelectListItem> selectListItemsProvider = new List<SelectListItem>();
            foreach (DataRow item in datasetProvider.Tables[0].Rows)
            {
                selectListItemsProvider.Add(new SelectListItem { Value = item["Provider"].ToString(), Text = item["Provider"].ToString() });
            }
            laboratory.listyProvider = selectListItemsProvider;

            DataSet datasetManufacturer = GetManufacturerDB(laboratory.TypeOfMaterial, laboratory.NameOfTypeMaterial, laboratory.Provider, 3);
            List<SelectListItem> selectListItemsManufacturer = new List<SelectListItem>();
            foreach (DataRow item in datasetManufacturer.Tables[0].Rows)
            {
                selectListItemsManufacturer.Add(new SelectListItem { Value = item["Manufacturer"].ToString(), Text = item["Manufacturer"].ToString() });
            }
            laboratory.listyManufacturer = selectListItemsManufacturer;

            List<SelectListItem> selectListItemsPartiesNames = new List<SelectListItem>
            {
                new SelectListItem() { Text = "K6", Value = "K6" },
                new SelectListItem() { Text = "K7", Value = "K7" },
                new SelectListItem() { Text = "K8", Value = "K8" }
            };
            laboratory.listyPartiesNames = selectListItemsPartiesNames;

        
            DataSet datasetParties = GetParties();
            List<SelectListItem> selectListItemsParties = new List<SelectListItem>();
            foreach (DataRow item in datasetParties.Tables[0].Rows)
            {
                selectListItemsParties.Add(new SelectListItem { Value = item["Name"].ToString(), Text = item["Name"].ToString() });
            }
            laboratory.listyParties = selectListItemsParties;

            return View(laboratory);

        }

       // Конец
       //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

       //  Начало.Пост редактирования

        [Authorize(Roles = "Moderator, Admin")]
        [HttpPost]
        public async Task<IActionResult> EditTableRowLab([Bind("Id,TypeOfMaterial,NameOfTypeMaterial,Quantity,Leftovers,Provider,Manufacturer,Document,NumberOfDocument,Indexation,DocDate,Employee,IpAdress,AutoDate,Remarks")] MainTableDB TableView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                    {
                        sqlConnection.Open();
                        SqlCommand command = new SqlCommand("BasApp_Laboratory_UpdateLabTable", sqlConnection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        command.Parameters.AddWithValue("incomin_Id", TableView.Id);
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
                        command.Parameters.AddWithValue("incomin_Remarks", TableView.Remarks ??= "1");
                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                return RedirectToAction(nameof(EditDeleteAllDataLab));
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Вьюха удаления. Получаем значения той записи, котору. хотим удалить по Id

        [Authorize(Roles = "Moderator, Admin")]
        [HttpGet]
        public IActionResult DeleteTableRowLab(int? id)
        {
            Laboratory laboratory = FetchRecordById(id);
            return View(laboratory);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Пост удаления

        [Authorize(Roles = "Moderator, Admin")]
        [HttpPost, ActionName("DeleteTableRowLab")]
        public async Task<IActionResult> DeleteTableRowConfirm(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                    {
                        sqlConnection.Open();
                        SqlCommand command = new SqlCommand("BasApp_Laboratory_DeleteAtLabTable", sqlConnection)
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
                return RedirectToAction(nameof(EditDeleteAllDataLab));
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Ищем запись по Id для удаления или редактирования и возвращаем ее поля

        [NonAction]
        public Laboratory FetchRecordById(int? id)
        {
            Laboratory laboratory = new Laboratory();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    DataTable dataTable = new DataTable();

                    sqlConnection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter("ViewRecordById", sqlConnection);
                    sqlDA.SelectCommand.Parameters.AddWithValue("in_Id", id);
                    sqlDA.SelectCommand.Parameters.AddWithValue("in_SubsType", 2);
                    sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDA.Fill(dataTable);
                    if (dataTable.Rows.Count == 1)
                    {
                        laboratory.Id = Convert.ToInt32(dataTable.Rows[0]["Id"].ToString());

                        laboratory.TypeOfMaterial = dataTable.Rows[0]["TypeOfMaterial"].ToString();

                        laboratory.NameOfTypeMaterial = dataTable.Rows[0]["NameOfTypeMaterial"].ToString();

                        laboratory.Quantity = Convert.ToDecimal(dataTable.Rows[0]["Quantity"].ToString());

                        laboratory.Leftovers = Convert.ToDecimal(dataTable.Rows[0]["Leftovers"].ToString());

                        laboratory.Provider = dataTable.Rows[0]["Provider"].ToString();

                        laboratory.Manufacturer = dataTable.Rows[0]["Manufacturer"].ToString();

                        laboratory.Document = dataTable.Rows[0]["Document"].ToString();

                        laboratory.NumberOfDocument = dataTable.Rows[0]["NumberOfDocument"].ToString();

                        laboratory.Indexation = dataTable.Rows[0]["Indexation"].ToString();

                        laboratory.DocDate = Convert.ToDateTime(dataTable.Rows[0]["DocDate"].ToString());

                        laboratory.Employee = dataTable.Rows[0]["Employee"].ToString();

                        laboratory.IpAdress = dataTable.Rows[0]["IpAdress"].ToString();

                        laboratory.AutoDate = Convert.ToDateTime(dataTable.Rows[0]["AutoDate"].ToString());

                        laboratory.Remarks = dataTable.Rows[0]["Remarks"].ToString();

                        laboratory.OperationType = Convert.ToInt32(dataTable.Rows[0]["OperationType"].ToString());
                    }
                    return laboratory;
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
        public async Task<IActionResult> ImportExcelFileLab(DateTime dateStart, DateTime dateFinish)
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
                    cmd.Parameters.AddWithValue("in_SubsType", 2);

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
        public async Task<IActionResult> ImportExcelFileLab(InnerModelToPost TableViewModel)
        {
            if (ModelState.IsValid)
            {
                await ImportExcelFileLab(TableViewModel.DateStart, TableViewModel.DateFinish);
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
                    SqlDataAdapter sqlDA = new SqlDataAdapter("_Excel_GetDataLab", sqlConnection);
                    sqlDA.SelectCommand.Parameters.AddWithValue("in_DateStart", Convert.ToDateTime(dateStart));
                    sqlDA.SelectCommand.Parameters.AddWithValue("in_DateFinish", Convert.ToDateTime(dateFinish));
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
                worksheet.Column(11).Style.Numberformat.Format = "dd:mm:yyyy hh:mm:ss";
                worksheet.Column(14).Style.Numberformat.Format = "dd:mm:yyyy hh:mm:ss";
                worksheet.Cells.AutoFitColumns();
                worksheet.Cells[1, 1, 1, 16].AutoFilter = true;

                using (ExcelRange heading = worksheet.Cells[1, 1, 1, 16])
                {
                    var colory = heading.Style.Fill;
                    colory.PatternType = ExcelFillStyle.Solid;
                    colory.BackgroundColor.SetColor(Color.LightBlue);
                    heading.Style.Border.BorderAround(ExcelBorderStyle.Thick);

                }

                package.Save();
            }

            stream.Position = 0;
            string excelname = $"VestPlast.DataLab.{DateTime.Now}.xlsx";

            return File(stream, "application/vnd.openformats-officedocument.spreadsheetml.sheet", excelname);
        }

        // Начало. Вьюха для получения состава партий

        [HttpGet]
        public IActionResult GetPartiesInfoLab(string numberOfDocument)
        {

            try
            {
                LaboratoryParties laboratoryParties = new LaboratoryParties();

                DataSet datasetParties = GetParties();
                List<SelectListItem> selectListItemsParties = new List<SelectListItem>();
                selectListItemsParties.Add(new SelectListItem { Value = null, Text = "-- Не выбрано --" });
                foreach (DataRow item in datasetParties.Tables[0].Rows)
                {
                    selectListItemsParties.Add(new SelectListItem { Value = item["Name"].ToString(), Text = item["Name"].ToString() });
                }
                laboratoryParties.listyParties = selectListItemsParties;

                laboratoryParties.NumberOfDocument = numberOfDocument;

                DataTable tdbl = new DataTable();
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter("BasApp_Laboratory_GetDataOfParty", sqlConnection);
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_NumberOfDocument", numberOfDocument ??= "Null");
                    sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDA.Fill(tdbl);
                }

                laboratoryParties.listil = tdbl;
                return View(laboratoryParties);
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
        public IActionResult GetPartiesInfoLab(LaboratoryParties laboratoryParties)
        {
            if (ModelState.IsValid)
            {
                GetPartiesInfoLab(laboratoryParties.NumberOfDocument);
                return View();
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

    }
}
