using System;
using System.Collections.Generic;
using System.Linq;
using JSON_CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Newtonsoft.Json;

namespace JSON_CRUD.Controllers
{
    public class PersonController : Controller
    {

        public ActionResult Index()
        {
            List<PersonModel> people = new List<PersonModel>();
            JSONReadWrite readWrite = new JSONReadWrite();
            people = JsonConvert.DeserializeObject<List<PersonModel>>(readWrite.Read("parrainagestotalGROS.json", "data"));

            var items = people.Select(p => p.Candidat).Distinct().ToList();

            ViewBag.CandidatItems = items;
            ViewBag.CountCandidat = people.Count();
            return View(people);
        }

        [HttpPost]
        public ActionResult Index(string yaz)
        {
            if (yaz == null) return View();
            string[] candidats = yaz.Split(',');
            List<PersonModel> people = new List<PersonModel>();
            JSONReadWrite readWrite = new JSONReadWrite();
            people = JsonConvert.DeserializeObject<List<PersonModel>>(readWrite.Read("parrainagestotalGROS.json", "data"));
            
            // List all candidats inside Json file
            var items = people.Select(p => p.Candidat).Distinct().ToList();
            ViewBag.CandidatItems = items;

            var slider = from v in people
                            .Where(c => candidats.Contains(c.Candidat))
                            .Take(50)
                            .OrderBy(c => c.Candidat)
                         select v;
            ViewBag.CountCandidat = slider.Count();
            return View("Index",slider);
        }
    }

    public class JSONReadWrite
    {
        public JSONReadWrite() { }

        public string Read(string fileName, string location)
        {
            string root = "wwwroot";
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                root,
                location,
                fileName);

            string jsonResult;

            using (StreamReader streamReader = new StreamReader(path,System.Text.Encoding.UTF8))
            {
                jsonResult = streamReader.ReadToEnd();
            }
            return jsonResult;
        }

        public void Write(string fileName, string location, string jSONString)
        {
            string root = "wwwroot";
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                root,
                location,
                fileName);

            using (var streamWriter = File.CreateText(path))
            {
                streamWriter.Write(jSONString);
            }
        }
    }
}