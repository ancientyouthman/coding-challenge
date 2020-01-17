using Newtonsoft.Json;
using ShipTracking.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace ShipTracking.Services
{
    public class DataService
    {
        private readonly string _dataRootPath;

        public DataService()
        {
            _dataRootPath = ConfigurationManager.AppSettings["DataRootPath"];
        }

        public string ReadGridData()
        {
            var filePath = HttpContext.Current.Server.MapPath($"{_dataRootPath}/GridConfig.json");
            var gridData = File.ReadAllText(filePath);
            return gridData;
        }

        public  UpdateAttempt UpdateGrid(string gridJson)
        {
            var filePath = HttpContext.Current.Server.MapPath($"{_dataRootPath}/GridConfig.json");
            File.WriteAllText(filePath, gridJson);
            return new UpdateAttempt { Success = true };
        }
    }
}