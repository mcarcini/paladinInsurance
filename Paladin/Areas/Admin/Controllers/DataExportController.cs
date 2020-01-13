using AutoMapper;
using Paladin.Models;
using Paladin.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Paladin.Areas.Admin.Controllers
{
    public class DataExportController : Controller
    {
        private PaladinDbContext _context;

        public DataExportController(PaladinDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetQuotesCSV()
        {
            var applicants = _context.Applicants.ToList();
            var mappedApplicants = new List<ApplicantVM>();
            foreach (var app in applicants)
            {
                mappedApplicants.Add(Mapper.Map<ApplicantVM>(app));
            }

            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream, Encoding.Default);

            foreach (var item in mappedApplicants)
            {
                var properties = typeof(ApplicantVM).GetProperties();
                foreach (var prop in properties)
                {
                    streamWriter.Write(GetValue(item, prop.Name));
                    streamWriter.Write(", ");
                }
                streamWriter.WriteLine();
            }

            streamWriter.Flush();
            stream.Position = 0;

            return File(stream, "text/csv");
        }

        public static string GetValue(object item, string propName)
        {
            return item.GetType().GetProperty(propName).GetValue(item, null).ToString() ?? "";
        }
    }
}