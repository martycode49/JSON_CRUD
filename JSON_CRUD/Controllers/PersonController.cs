using System;
using System.Collections.Generic;
using System.Linq;
using JSON_CRUD.Models;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Hosting;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JSON_CRUD.Controllers
{
    public class PersonController : Controller
    {

        public IActionResult Index()
        {
            List<PersonModel> people = new List<PersonModel>();
            JSONReadWrite readWrite = new JSONReadWrite();
            people = JsonConvert.DeserializeObject<List<PersonModel>>(readWrite.Read("parrainagestotal.json", "data"));

            var items = people.Select(p => p.Candidat).Distinct();
            ViewBag.CandidatItems = new SelectList(items,"CandidatItems");
            var items2 = people.Select(p => p.Departement).Distinct();
            ViewBag.DepartItems = new SelectList(items2, "DepartItems");

            return View(people);
        }

        [HttpPost]
        public IActionResult Index(PersonModel personModel)
        {
            List<PersonModel> people = new List<PersonModel>();
            JSONReadWrite readWrite = new JSONReadWrite();
            people = JsonConvert.DeserializeObject<List<PersonModel>>(readWrite.Read("parrainagestotal.json", "data"));
            var key = ViewBag.CandidatItems;

            IEnumerable<PersonModel> person = people.Where(
                p => p.Candidat == personModel.Candidat 
                && p.Departement == personModel.Departement);

            return View(person);
        }

        [HttpPost]
        public IActionResult Delete(string name)
        {
            List<PersonModel> people = new List<PersonModel>();
            JSONReadWrite readWrite = new JSONReadWrite();
            people = JsonConvert.DeserializeObject<List<PersonModel>>(readWrite.Read("parrainagestotal.json", "data"));

            PersonModel toRemove = (PersonModel)people.Where(x => x.Nom == name);
            people.Remove(toRemove);

            string jSONString = JsonConvert.SerializeObject(people);
            readWrite.Write("parrainagestotal.json", "data", jSONString);

            return RedirectToAction("index", "Person");
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

            using (StreamReader streamReader = new StreamReader(path))
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