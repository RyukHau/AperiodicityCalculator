using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AperiodicityCalculator.Models;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace AperiodicityCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Information model)
        {
            if (ModelState.IsValid)
            {
                int chargeDays;
                int totalDays;
                decimal sellingunit;
                decimal totalsellingunit;

                chargeDays = chargeDay(model.BillingType.ToString(), model.StartDate, model.EndDate, model.RenewDate);
                totalDays = getFullDays(model.BillingType.ToString(), model.StartDate, model.RenewDate);
                sellingunit = (decimal)((model.SellingPrice) / totalDays);
                totalsellingunit = sellingunit * chargeDays;

                Information result = new Information
                {
                    BillingType = model.BillingType,
                    StartDate = model.StartDate.Date,
                    EndDate = model.EndDate,
                    RenewDate = model.RenewDate,
                    ChargeDays = chargeDays,
                    TotalDays = totalDays,
                    SellingPrice = model.SellingPrice,
                    SellingUnit = sellingunit,
                    TotalSellingUnit = totalsellingunit
                };

                return View(result);
            }

            return View(); ;
        }

        /*public IActionResult Privacy()
        {
            return View();
        }*/

        public int chargeDay(String billingType, DateTime startDate, DateTime endDate, DateTime renewDate)
        {
            DateTime chargeEndDate;
            
            switch(billingType)
            {
                case "Monthly":
                    {                 
                        if(startDate.Day < renewDate.Day )
                        {
                            int count = ((renewDate.Year - startDate.Year) * 12) + (renewDate.Month - startDate.Month);

                            if(count != 0)
                            {
                                chargeEndDate = renewDate.AddMonths(-count + 1);
                            }
                            else
                            {
                                chargeEndDate = renewDate;
                            }
                        }
                        else
                        {
                            DateTime temp = startDate.AddMonths(1);
                            chargeEndDate = new DateTime(temp.Year, temp.Month, renewDate.Day);
                        }

                        if(endDate <= chargeEndDate)
                        {
                            chargeEndDate = endDate;
                        }

                        return new TimeSpan(chargeEndDate.Ticks - startDate.Ticks).Days + 1;
                    }
                case "Annually":
                    {
                        if(endDate <= renewDate)
                        {
                            chargeEndDate = endDate;
                        }
                        else
                        {
                            chargeEndDate = renewDate;
                        }

                        return new TimeSpan(chargeEndDate.Ticks - startDate.Ticks).Days + 1;
                    }
                default:
                    {
                        return -1;
                    }
            }
        }

        public int getFullDays(String billingType, DateTime startDate, DateTime renewDate)
        {
            DateTime chargeMonth;
            
            switch(billingType)
            {
                case "Monthly":
                    {
                        if(startDate.Day < renewDate.Day)
                        {
                            chargeMonth = startDate.AddMonths(-1);
                        }
                        else
                        {
                            chargeMonth = startDate;
                        }

                        return DateTime.DaysInMonth(chargeMonth.Year, chargeMonth.Month);

                    }
                case "Annually":
                    {
                        return new TimeSpan(renewDate.AddDays(-1).Ticks - renewDate.AddYears(-1).Ticks).Days + 1;
                    }
                default:
                    {
                        return -1;
                    }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
