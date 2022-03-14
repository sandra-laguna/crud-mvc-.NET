using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCCrud.Models;
using MVCCrud.Models.ViewModels;

namespace MVCCrud.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        public ActionResult Index()
        {
            List<ListDataViewModel> lst;
            using (CrudEntities db = new CrudEntities())
            {
                lst = (from d in db.data
                       select new ListDataViewModel
                       {
                           Id = d.id,
                           Name = d.name,
                           Birth_date = d.birth_date,
                           Email = d.email
                       }).ToList();

            }
            return View(lst);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(DataViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (CrudEntities db = new CrudEntities())
                    {
                        var oData = new data();
                        oData.email = model.Email;
                        oData.birth_date = model.Birth_date;
                        oData.name = model.Name;

                        db.data.Add(oData);
                        db.SaveChanges();
                    }

                    return Redirect("~/Data");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public ActionResult Edit(int Id)
        {
            DataViewModel model = new DataViewModel();
            using (CrudEntities db = new CrudEntities())
            {
                var oData = db.data.Find(Id);
                model.Name = oData.name;
                model.Email = oData.email;
                model.Birth_date = oData.birth_date;
                model.Id = oData.id;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DataViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (CrudEntities db = new CrudEntities())
                    {
                        var oData = db.data.Find(model.Id);
                        oData.email = model.Email;
                        oData.birth_date = model.Birth_date;
                        oData.name = model.Name;

                        db.Entry(oData).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    return Redirect("~/Data");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            using (CrudEntities db = new CrudEntities())
            {
                var oData = db.data.Find(Id);
                db.data.Remove(oData);
                db.SaveChanges();
            }
            return Redirect("~/Data");
        }

    }
}