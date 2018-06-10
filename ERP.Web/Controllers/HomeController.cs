using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Webdiyer.WebControls.Mvc;
using ERP.Common.Data;

namespace ERP.Web.Controllers
{
    public class HomeController : Controller
    {
        ERP.DAL.TableInfoRepository repository = new DAL.TableInfoRepository();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var tableInfos = repository.GetTableInfos();
            return View(tableInfos);
        }

        //
        // GET: /Home/Details/5

        public ActionResult Detail(int id, int pageIndex = 1)
        {
            //var tableDataInfo = repository.GetTableData(id);
            //return View(tableDataInfo);
           int pageSize = 20;
           var tabInfo = repository.GetTableInfo(id);
           var pagerInfo = ERP.DAL.SqlHelper.GetPagerData(tabInfo.Name, tabInfo.GetColumnSql(), null, "ID DESC", pageIndex, pageSize);

           TableDataInfo info = new TableDataInfo();
           info.TableData = pagerInfo.PagerData;
           info.TableInfo = tabInfo;

           PagedList<DataRow> arts = new PagedList<DataRow>(pagerInfo.PagerData.Select(), pageIndex, pageSize, pagerInfo.RecordCount);
           ViewBag.info = info;
           return View(arts);
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Home/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
