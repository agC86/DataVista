﻿using DataVista.Extensions;
using DataVista.SystemTools;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Shapes;

namespace DataVista.Database
{
    public class DataConverter
    {
        #region FIELDS
        private static string? _winpath = SystemTools.WinPath.MyDocuments + @"\Datavista\DataConverter\";
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Needs constructor to verify <see cref="Path"/>
        /// </summary>
        public DataConverter()
        {
            if (!Directory.Exists(WinPath))
            {
                Directory.CreateDirectory(WinPath);
            }
        }
        #endregion

        #region PROPERTIES
        public static string? WinPath
        {
            get
            {
                return _winpath;
            }
            set
            {
                _winpath = value;
            }
        }
        #endregion

        #region SQL
        /// <summary>
        /// Pass in a valid full filepath
        ///<para> Example: <code>string filePath = "C:\Users\Admin\source\repos\MyProject\SQL\Procedures.sql"</code></para>
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string ReadSqlFile(string filePath)
        {

            string query = String.Empty;
            string fileType = System.IO.Path.GetExtension(filePath).ToLower();

            try
            {
                if (File.Exists(filePath) && fileType == ".sql")
                {

                    query = File.ReadAllText(filePath);

                }
                else
                {
                    throw new ArgumentException("Invalid SQL file path provided.");
                }
            }
            catch (Exception ex)
            {
                query = $"Query: {query} {Environment.NewLine}" +
                    $"Exception: {ex}";
            }
            return query;
        }

        public static string ReadSqlDialog()
        {
            string query = String.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "SQL files (*.sql)|*.sql|All files (*.*)|*.*";
            try
            {
                if (openFileDialog.ShowDialog() == true)
                {

                    string filePath = openFileDialog.FileName;
                    query = File.ReadAllText(filePath);
                }
                else
                {
                    throw new ArgumentException("Invalid SQL file path provided.");
                }
            }
            catch (Exception ex)
            {
                query = $"Query: {query} {Environment.NewLine}" +
                    $"Exception: {ex}";

            }
            return query;
        }
        #endregion

        #region XML
        public void SerializeXml(DataSet dataSet)
        {
            dataSet.WriteXmlSchema(WinPath);
            dataSet.WriteXml(WinPath);
        }

        public void SerializeXml(DataSet dataSet, string winPath)
        {
            dataSet.WriteXmlSchema(winPath);
            dataSet.WriteXml(winPath);
        }

        public DataSet DeSerializeXml(DataSet dataSet)
        {
            dataSet.ReadXmlSchema(WinPath);
            dataSet.ReadXml(WinPath);

            return dataSet;
        }

        public DataSet DeSerializeXml(DataSet dataSet, string winPath)
        {
            dataSet.ReadXmlSchema(winPath);
            dataSet.ReadXml(winPath);

            return dataSet;
        }
        #endregion
    }
}
