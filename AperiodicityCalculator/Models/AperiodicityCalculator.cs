using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AperiodicityCalculator.Models
{
    public class Information
    {
        [Display(Name = "Billing Type 付費方式")]
        public Billing BillingType { get; set; }

        [Display(Name = "Start Date 起始日")]
        [Required(ErrorMessage = "Not Empty")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date 到期日")]
        [Required(ErrorMessage = "Not Empty")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Renew Date 續約日")]
        [Required(ErrorMessage = "Not Empty 未輸入")]
        public DateTime RenewDate { get; set; }

        [Display(Name = "Selling Price 銷售價格")]
        [Required(ErrorMessage = "Not Empty 未輸入")]
        public Decimal? SellingPrice { get; set; }

        [Display(Name = "Charge Days 銷售天數")]
        public int ChargeDays { get; set; }

        [Display(Name = "Total Days 總天數")]
        public int TotalDays { get; set; }

        [Display(Name = "Selling Unit 一天價格")]
        public Decimal? SellingUnit { get; set; }

        [Display(Name = "Total Selling Unit 天總價格")]
        public Decimal? TotalSellingUnit { get; set; }
    }

    public enum Billing
    {
        Monthly,
        Annually
    }
}
