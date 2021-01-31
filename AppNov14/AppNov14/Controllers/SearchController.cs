﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AppNov14.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AppNov14.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {

        // Начало. Конфигурация для строки подключения

        private readonly IConfiguration _configuration;

        public SearchController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        // Начало. Получение уникальных значений типа материала для JQuery Cascade Dropdown list для CreateWarehouseFieldType

        [NonAction]
        public DataSet GetTypeOfMaterialSearch()
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

        [NonAction]
        public DataSet GetNameOfTypeMaterialSearch(string type)
        {
            DataSet dataset = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("JQ_MultiController_GetDistinct_NameOfTypeMaterial_DependentByTypeOfMaterial", connection);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("in_TypeOfMaterial", type);
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
        [NonAction]
        public DataSet GetNameOfTypeMaterialSearch()
        {
            DataSet dataset = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("JQ_MultiController_GetDistinct_NameOfTypeMaterial", connection);
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
      

        [NonAction]
        public DataSet GetProviderSearch()
        {
            try
            {
                DataSet dataset = new DataSet();
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("JQ_MultiController_GetDistinct_Provider", connection);
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

        [NonAction]
        public DataSet GetManufacturerSearch()
        {
            try
            {
                DataSet dataset = new DataSet();
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("JQ_MultiController_GetDistinct_Manufacturer", connection);
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

        // Начало. Json для имени типа материала для JQuery Cascade Dropdown list
        [HttpGet]
        public JsonResult GetTypesSearchJson(string typename, string provider, string manufacturer)
        {
            List<SelectListItem> selectListItemsType = new List<SelectListItem>();

            DataSet dataset = GetTypeOfMaterialSearch();

            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                selectListItemsType.Add(new SelectListItem { Value = item["TypeOfMaterial"].ToString(), Text = item["TypeOfMaterial"].ToString() });
            }

            return Json(selectListItemsType);
        }

        // Начало. Json для имени типа материала для JQuery Cascade Dropdown list

        [HttpGet]
        public JsonResult GetNameTypesSearchJson(string type)
        {
            List<SelectListItem> selectListItemsNameType = new List<SelectListItem>();

            DataSet datasetNameType = GetNameOfTypeMaterialSearch(type);

            foreach (DataRow item in datasetNameType.Tables[0].Rows)
            {
                selectListItemsNameType.Add(new SelectListItem { Value = item["NameOfTypeMaterial"].ToString(), Text = item["NameOfTypeMaterial"].ToString() });
            }

            return Json(selectListItemsNameType);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

        public IActionResult Index()
        {
            return View();
        }

        // Начало. Вьюха для поиска по отправке сырья на склад

        [HttpGet]
        public IActionResult SerachInIncomingOrder(DateTime dateStart, DateTime dateFinish, string type, string typeName, string provider, string manufacturer, string numberOfDocument, string indexation, string employee)
        {

            try
            {
                SearchModel SearchViewModel = new SearchModel();

                DataSet datasetType = GetTypeOfMaterialSearch();
                List<SelectListItem> selectListItemsType = new List<SelectListItem>();
                selectListItemsType.Add(new SelectListItem { Value = null, Text = "-- Не выбрано --" });
                foreach (DataRow item in datasetType.Tables[0].Rows)
                {
                    selectListItemsType.Add(new SelectListItem { Value = item["TypeOfMaterial"].ToString(), Text = item["TypeOfMaterial"].ToString() });
                }
                SearchViewModel.listyType = selectListItemsType;

                DataSet datasetTypeName = GetNameOfTypeMaterialSearch();
                List<SelectListItem> selectListItemsTypeName = new List<SelectListItem>();
                selectListItemsTypeName.Add(new SelectListItem { Value = null, Text = "-- Не выбрано --" });
                foreach (DataRow item in datasetTypeName.Tables[0].Rows)
                {
                    selectListItemsTypeName.Add(new SelectListItem { Value = item["NameOfTypeMaterial"].ToString(), Text = item["NameOfTypeMaterial"].ToString() });
                }
                SearchViewModel.listyNameType = selectListItemsTypeName;

                DataSet datasetProvider = GetProviderSearch();
                List<SelectListItem> selectListItemsProvider = new List<SelectListItem>();
                selectListItemsProvider.Add(new SelectListItem { Value = null, Text = "-- Не выбрано --" });
                foreach (DataRow item in datasetProvider.Tables[0].Rows)
                {
                    selectListItemsProvider.Add(new SelectListItem { Value = item["Provider"].ToString(), Text = item["Provider"].ToString() });
                }
                SearchViewModel.listyProvider = selectListItemsProvider;

                DataSet datasetManufacturer = GetManufacturerSearch();
                List<SelectListItem> selectListItemsManufacturer = new List<SelectListItem>();
                selectListItemsManufacturer.Add(new SelectListItem { Value = null, Text = "-- Не выбрано --" });
                foreach (DataRow item in datasetManufacturer.Tables[0].Rows)
                {
                    selectListItemsManufacturer.Add(new SelectListItem { Value = item["Manufacturer"].ToString(), Text = item["Manufacturer"].ToString() });
                }
                SearchViewModel.listyManufacturer = selectListItemsManufacturer;

                
                DataSet datasetParties = GetParties(1);
                List<SelectListItem> selectListItemsParties = new List<SelectListItem>();
                selectListItemsParties.Add(new SelectListItem { Value = null, Text = "-- Не выбрано --" });
                foreach (DataRow item in datasetParties.Tables[0].Rows)
                {
                    selectListItemsParties.Add(new SelectListItem { Value = item["Name"].ToString(), Text = item["Name"].ToString() });
                }
                SearchViewModel.listyParties = selectListItemsParties;

                if (dateStart == DateTime.MinValue && dateFinish == DateTime.MinValue && type == null && typeName == null && provider == null && manufacturer == null && numberOfDocument == null && indexation == null && employee == null)
                {
                    dateStart = DateTime.Now.AddDays(-20);
                    dateFinish = DateTime.Now.AddDays(1);
                    SearchViewModel.DateStart = dateStart;
                    SearchViewModel.DateFinish = dateFinish;
                }
                else
                {
                    SearchViewModel.DateStart = dateStart;
                    SearchViewModel.DateFinish = dateFinish;
                    SearchViewModel.TypeOfMaterial = type;
                    SearchViewModel.NameOfTypeMaterial = typeName;
                    SearchViewModel.Provider = provider;
                    SearchViewModel.Manufacturer = manufacturer;
                    SearchViewModel.NumberOfDocument = numberOfDocument;
                    SearchViewModel.Indexation = indexation;
                    SearchViewModel.Employee = employee;
                }

                DataTable tdbl = new DataTable();
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter("SerachIncomingOrderInMainTable", sqlConnection);
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_DateStart", dateStart);
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_DateFinish", dateFinish);
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_TypeOfMaterial", type ??= "Null");
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_NameOfTypeMaterial", typeName ??= "Null");
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_Provider", provider ??= "Null");
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_Manufacturer", manufacturer ??= "Null");
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_NumberOfDocument", numberOfDocument ??= "Null");
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_Indexation", indexation ??= "Null");
                    sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDA.Fill(tdbl);
                }

                SearchViewModel.listil = tdbl;
                return View(SearchViewModel);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        // Начало. Пост поиска по отправке сырья на склад

        [HttpPost]
        public IActionResult SerachInIncomingOrder(SearchModel SearchViewModel)
        {
            if (ModelState.IsValid)
            {
                SerachInIncomingOrder(SearchViewModel.DateStart, SearchViewModel.DateFinish, SearchViewModel.TypeOfMaterial, SearchViewModel.NameOfTypeMaterial, SearchViewModel.Provider, SearchViewModel.Manufacturer, SearchViewModel.NumberOfDocument, SearchViewModel.Indexation, SearchViewModel.Employee);
                return View();
            }
            return View(ModelState.ErrorCount);
        }

        // Конец
        //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _




        [HttpGet]
        public IActionResult SerachInOutcomingOrder(DateTime dateStart, DateTime dateFinish, string type, string typeName, string provider, string manufacturer, string document, string numberOfDocument, string indexation, string line, string employee)
        {

            try
            {
                SearchModel SearchViewModel = new SearchModel();

                DataSet datasetType = GetTypeOfMaterialSearch();
                List<SelectListItem> selectListItemsType = new List<SelectListItem>();
                selectListItemsType.Add(new SelectListItem { Value = null, Text = "-- Не выбрано --" });
                foreach (DataRow item in datasetType.Tables[0].Rows)
                {
                    selectListItemsType.Add(new SelectListItem { Value = item["TypeOfMaterial"].ToString(), Text = item["TypeOfMaterial"].ToString() });
                }
                SearchViewModel.listyType = selectListItemsType;

                DataSet datasetTypeName = GetNameOfTypeMaterialSearch();
                List<SelectListItem> selectListItemsTypeName = new List<SelectListItem>();
                selectListItemsTypeName.Add(new SelectListItem { Value = null, Text = "-- Не выбрано --" });
                foreach (DataRow item in datasetTypeName.Tables[0].Rows)
                {
                    selectListItemsTypeName.Add(new SelectListItem { Value = item["NameOfTypeMaterial"].ToString(), Text = item["NameOfTypeMaterial"].ToString() });
                }
                SearchViewModel.listyNameType = selectListItemsTypeName;

                DataSet datasetProvider = GetProviderSearch();
                List<SelectListItem> selectListItemsProvider = new List<SelectListItem>();
                selectListItemsProvider.Add(new SelectListItem { Value = null, Text = "-- Не выбрано --" });
                foreach (DataRow item in datasetProvider.Tables[0].Rows)
                {
                    selectListItemsProvider.Add(new SelectListItem { Value = item["Provider"].ToString(), Text = item["Provider"].ToString() });
                }
                SearchViewModel.listyProvider = selectListItemsProvider;

                DataSet datasetManufacturer = GetManufacturerSearch();
                List<SelectListItem> selectListItemsManufacturer = new List<SelectListItem>();
                selectListItemsManufacturer.Add(new SelectListItem { Value = null, Text = "-- Не выбрано --" });
                foreach (DataRow item in datasetManufacturer.Tables[0].Rows)
                {
                    selectListItemsManufacturer.Add(new SelectListItem { Value = item["Manufacturer"].ToString(), Text = item["Manufacturer"].ToString() });
                }
                SearchViewModel.listyManufacturer = selectListItemsManufacturer;

                DataSet datasetPartiesNames = GetNameOfParty();
                List<SelectListItem> selectListItemsPartiesNames = new List<SelectListItem>();
                selectListItemsPartiesNames.Add(new SelectListItem { Value = null, Text = "-- Не выбрано --" });
                foreach (DataRow item in datasetPartiesNames.Tables[0].Rows)
                {
                    selectListItemsPartiesNames.Add(new SelectListItem { Value = item["Name"].ToString(), Text = item["Name"].ToString() });
                }
                SearchViewModel.listyPartiesNames = selectListItemsPartiesNames;

                DataSet datasetParties = GetParties(2);
                List<SelectListItem> selectListItemsParties = new List<SelectListItem>();
                selectListItemsParties.Add(new SelectListItem { Value = null, Text = "-- Не выбрано --" });
                foreach (DataRow item in datasetParties.Tables[0].Rows)
                {
                    selectListItemsParties.Add(new SelectListItem { Value = item["Name"].ToString(), Text = item["Name"].ToString() });
                }
                SearchViewModel.listyParties = selectListItemsParties;

                List<SelectListItem> selectListItemsLine = new List<SelectListItem>();
                selectListItemsLine.Add(new SelectListItem() { Text = "-- Не выбрано --", Value = null });
                selectListItemsLine.Add(new SelectListItem() { Text = "Bolshevik", Value = "Bolshevik" });
                selectListItemsLine.Add(new SelectListItem() { Text = "Xinda1", Value = "Xinda1" });
                selectListItemsLine.Add(new SelectListItem() { Text = "Biersdorff", Value = "Biersdorff" });

                SearchViewModel.listyLine = selectListItemsLine;

                if (dateStart == DateTime.MinValue && dateFinish == DateTime.MinValue && type == null && typeName == null && provider == null && manufacturer == null && document == null && numberOfDocument == null && indexation == null && line == null && employee == null)
                {
                    dateStart = DateTime.Now.AddDays(-20);
                    dateFinish = DateTime.Now.AddDays(1);
                    SearchViewModel.DateStart = dateStart;
                    SearchViewModel.DateFinish = dateFinish;
                }
                else
                {
                    SearchViewModel.DateStart = dateStart;
                    SearchViewModel.DateFinish = dateFinish;
                    SearchViewModel.TypeOfMaterial = type;
                    SearchViewModel.NameOfTypeMaterial = typeName;
                    SearchViewModel.Provider = provider;
                    SearchViewModel.Manufacturer = manufacturer;
                    SearchViewModel.Document =document;
                    SearchViewModel.NumberOfDocument = numberOfDocument;
                    SearchViewModel.Indexation = indexation;
                    SearchViewModel.Line = line;
                    SearchViewModel.Employee = employee;
                }

                DataTable tdbl = new DataTable();
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter("SerachOutcomingOrderInMainTable", sqlConnection);
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_DateStart", dateStart);
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_DateFinish", dateFinish);
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_TypeOfMaterial", type ??= "Null");
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_NameOfTypeMaterial", typeName ??= "Null");
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_Provider", provider ??= "Null");
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_Manufacturer", manufacturer ??= "Null");
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_Document", document ??= "Null");
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_NumberOfDocument", numberOfDocument ??= "Null");
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_Indexation", indexation ??= "Null");
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_Line", line ??= "Null");
                    sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDA.Fill(tdbl);
                }

                SearchViewModel.listil = tdbl;
                return View(SearchViewModel);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        // Начало. Пост поиска по отправке сырья на склад

        [HttpPost]
        public IActionResult SerachInOutcomingOrder(SearchModel SearchViewModel)
        {
            if (ModelState.IsValid)
            {
                SerachInOutcomingOrder(SearchViewModel.DateStart, SearchViewModel.DateFinish, SearchViewModel.TypeOfMaterial, SearchViewModel.NameOfTypeMaterial, SearchViewModel.Provider, SearchViewModel.Manufacturer, SearchViewModel.Document, SearchViewModel.NumberOfDocument, SearchViewModel.Indexation, SearchViewModel.Line, SearchViewModel.Employee);
                return View();
            }
            return View(ModelState.ErrorCount);
        }

        // Конец

        [HttpGet]
        public IActionResult SerachInReturnOrder(DateTime dateStart, DateTime dateFinish, string type, string typeName, string provider, string manufacturer, string document, string numberOfDocument, string indexation, string line, string employee)
        {

            try
            {
                SearchModel SearchViewModel = new SearchModel();

                DataSet datasetType = GetTypeOfMaterialSearch();
                List<SelectListItem> selectListItemsType = new List<SelectListItem>();
                selectListItemsType.Add(new SelectListItem { Value = null, Text = "-- Не выбрано --" });
                foreach (DataRow item in datasetType.Tables[0].Rows)
                {
                    selectListItemsType.Add(new SelectListItem { Value = item["TypeOfMaterial"].ToString(), Text = item["TypeOfMaterial"].ToString() });
                }
                SearchViewModel.listyType = selectListItemsType;

                DataSet datasetTypeName = GetNameOfTypeMaterialSearch();
                List<SelectListItem> selectListItemsTypeName = new List<SelectListItem>();
                selectListItemsTypeName.Add(new SelectListItem { Value = null, Text = "-- Не выбрано --" });
                foreach (DataRow item in datasetTypeName.Tables[0].Rows)
                {
                    selectListItemsTypeName.Add(new SelectListItem { Value = item["NameOfTypeMaterial"].ToString(), Text = item["NameOfTypeMaterial"].ToString() });
                }
                SearchViewModel.listyNameType = selectListItemsTypeName;

                DataSet datasetProvider = GetProviderSearch();
                List<SelectListItem> selectListItemsProvider = new List<SelectListItem>();
                selectListItemsProvider.Add(new SelectListItem { Value = null, Text = "-- Не выбрано --" });
                foreach (DataRow item in datasetProvider.Tables[0].Rows)
                {
                    selectListItemsProvider.Add(new SelectListItem { Value = item["Provider"].ToString(), Text = item["Provider"].ToString() });
                }
                SearchViewModel.listyProvider = selectListItemsProvider;

                DataSet datasetManufacturer = GetManufacturerSearch();
                List<SelectListItem> selectListItemsManufacturer = new List<SelectListItem>();
                selectListItemsManufacturer.Add(new SelectListItem { Value = null, Text = "-- Не выбрано --" });
                foreach (DataRow item in datasetManufacturer.Tables[0].Rows)
                {
                    selectListItemsManufacturer.Add(new SelectListItem { Value = item["Manufacturer"].ToString(), Text = item["Manufacturer"].ToString() });
                }
                SearchViewModel.listyManufacturer = selectListItemsManufacturer;

                DataSet datasetPartiesNames = GetNameOfParty();
                List<SelectListItem> selectListItemsPartiesNames = new List<SelectListItem>();
                selectListItemsPartiesNames.Add(new SelectListItem { Value = null, Text = "-- Не выбрано --" });
                foreach (DataRow item in datasetPartiesNames.Tables[0].Rows)
                {
                    selectListItemsPartiesNames.Add(new SelectListItem { Value = item["Name"].ToString(), Text = item["Name"].ToString() });
                }
                SearchViewModel.listyPartiesNames = selectListItemsPartiesNames;

                DataSet datasetParties = GetParties(2);
                List<SelectListItem> selectListItemsParties = new List<SelectListItem>();
                selectListItemsParties.Add(new SelectListItem { Value = null, Text = "-- Не выбрано --" });
                foreach (DataRow item in datasetParties.Tables[0].Rows)
                {
                    selectListItemsParties.Add(new SelectListItem { Value = item["Name"].ToString(), Text = item["Name"].ToString() });
                }
                SearchViewModel.listyParties = selectListItemsParties;

                List<SelectListItem> selectListItemsLine = new List<SelectListItem>();
                selectListItemsLine.Add(new SelectListItem() { Text = "-- Не выбрано --", Value = null });
                selectListItemsLine.Add(new SelectListItem() { Text = "Bolshevik", Value = "Bolshevik" });
                selectListItemsLine.Add(new SelectListItem() { Text = "Xinda1", Value = "Xinda1" });
                selectListItemsLine.Add(new SelectListItem() { Text = "Biersdorff", Value = "Biersdorff" });

                SearchViewModel.listyLine = selectListItemsLine;


                if (dateStart == DateTime.MinValue && dateFinish == DateTime.MinValue && type == null && typeName == null && provider == null && manufacturer == null && document == null && numberOfDocument == null && indexation == null && line == null && employee == null)
                {
                    dateStart = DateTime.Now.AddDays(-20);
                    dateFinish = DateTime.Now.AddDays(1);
                    SearchViewModel.DateStart = dateStart;
                    SearchViewModel.DateFinish = dateFinish;
                }
                else
                {
                    SearchViewModel.DateStart = dateStart;
                    SearchViewModel.DateFinish = dateFinish;
                    SearchViewModel.TypeOfMaterial = type;
                    SearchViewModel.NameOfTypeMaterial = typeName;
                    SearchViewModel.Provider = provider;
                    SearchViewModel.Manufacturer = manufacturer;
                    SearchViewModel.Document = document;
                    SearchViewModel.NumberOfDocument = numberOfDocument;
                    SearchViewModel.Indexation = indexation;
                    SearchViewModel.Line = line;
                    SearchViewModel.Employee = employee;
                }

                DataTable tdbl = new DataTable();
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter("SerachReturnOrderInMainTable", sqlConnection);
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_DateStart", dateStart);
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_DateFinish", dateFinish);
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_TypeOfMaterial", type ??= "Null");
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_NameOfTypeMaterial", typeName ??= "Null");
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_Provider", provider ??= "Null");
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_Manufacturer", manufacturer ??= "Null");
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_Document", document ??= "Null");
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_NumberOfDocument", numberOfDocument ??= "Null");
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_Indexation", indexation ??= "Null");
                    sqlDA.SelectCommand.Parameters.AddWithValue("incomin_Line", line ??= "Null");
                    sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDA.Fill(tdbl);
                }

                SearchViewModel.listil = tdbl;
                return View(SearchViewModel);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        // Начало. Пост поиска по отправке сырья на склад

        [HttpPost]
        public IActionResult SerachInReturnOrder(SearchModel SearchViewModel)
        {
            if (ModelState.IsValid)
            {
                SerachInReturnOrder(SearchViewModel.DateStart, SearchViewModel.DateFinish, SearchViewModel.TypeOfMaterial, SearchViewModel.NameOfTypeMaterial, SearchViewModel.Provider, SearchViewModel.Manufacturer, SearchViewModel.Document, SearchViewModel.NumberOfDocument, SearchViewModel.Indexation, SearchViewModel.Line, SearchViewModel.Employee);
                return View();
            }
            return View(ModelState.ErrorCount);
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
    }
    //_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
}


