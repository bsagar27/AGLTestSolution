using CatLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CatLibrary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var client = new HttpClient();
            HttpResponseMessage result = null;
            string responseResult = string.Empty;
            List<Owner> owners = new List<Owner>();
            // Get web service path value
            string pathValue = ConfigurationManager.AppSettings["catWebService"];
            //Consume web service
            client.BaseAddress = new Uri(pathValue);
            var responseTask = client.GetAsync("People.json");
            responseTask.Wait();
            result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                responseResult = responseTask.Result.Content.ReadAsStringAsync().Result;
                owners = JsonConvert.DeserializeObject<List<Owner>>(responseResult);
                ViewData["MaleOwnerModel"] = GenerateFilteredPetsList(owners, "male");
                ViewData["FemaleOwnerModel"] = GenerateFilteredPetsList(owners, "female");
                return View("Index");
            }
            else //web api sent error response 
            {
                return View("Error");
            }

        }

        private static List<SelectListItem> GenerateFilteredPetsList(List<Owner> owners, string filter)
        {
            var petList = new List<SelectListItem>();
            var petsOwner = owners.Where(o => o.Pets != null && o.Gender.ToLower().Equals(filter));
            var catsOwners = (from pets in petsOwner
                              select new Owner()
                              {
                                  Name = pets.Name,
                                  Gender = pets.Gender,
                                  Age = pets.Age,
                                  Pets = pets.Pets.Where(p => p.Type.ToLower() == "cat").ToList()
                              }).ToList();



            foreach (var owner in catsOwners)
            {
                if (owner.Pets != null)
                {
                    foreach (var ownerPet in owner.Pets)
                    {
                        var pet = new SelectListItem();

                        pet.Text = ownerPet.Name;
                        pet.Value = ownerPet.Name;
                        petList.Add(pet);
                    }
                }

            }

            return petList.OrderBy(p => p.Value).ToList();

        }
               
      
    }
}