using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JSON_CRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace JSON_CRUD.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //https://youtu.be/PUqw_FpI97g?list=PLDAPLmR-A0XQmqUPwziBhmNtH_nz1CIYF
            //https://www.chartjs.org/docs/latest/getting-started/
            //https://jsonformatter.curiousconcept.com/
            
            // masterModel
            var masterModel = new HomeIndexVM();

            // fill pie chart model
            var pieChartData = new PieChartVM();
            masterModel.PieChartData = pieChartData;
            return View(masterModel);
        }

        private PieChartVM GetPieChartData()
        {
            var model = new PieChartVM();
            var labels = new List<string>();
            labels.Add("Green");
            labels.Add("Blue"); 
            labels.Add("Gray"); 
            labels.Add("Purple");
            model.labels = labels;

            var datasets = new List<PieChartChildVM>();
            var childModel = new PieChartChildVM();

            var backgroundColorList = new List<string>();
            var dataList = new List<int>();

            foreach (var label in labels)
            {
                if (label == "Green")
                {
                    backgroundColorList.Add("#2ecc71");
                    dataList.Add(12);
                }
                if (label == "Blue")
                {
                    backgroundColorList.Add("##3498db");
                    dataList.Add(20);
                }
                if (label == "Gray")
                {
                    backgroundColorList.Add("#95a5a6");
                    dataList.Add(18);
                }
                if (label == "Purple")
                {
                    backgroundColorList.Add("#9b59b6");
                    dataList.Add(50);
                }

            }

            childModel.backgroundColor = backgroundColorList;
            childModel.data = dataList;
            datasets.Add(childModel);
            model.datasets = datasets;

            return model;
        }
    }
}
