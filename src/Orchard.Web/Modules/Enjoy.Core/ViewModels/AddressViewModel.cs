using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.ViewModels
{
    public class AddressViewModel
    {
        public AddressViewModel(string province,string city,string area)
        {
            this.Province = province;
            this.City = city;
            this.Area = area;
        }
        public AddressViewModel(string strAddresss)
        {

        }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        //<select class="form-control province" id="@Html.IdFor(o=>o.Province)" name="@Html.NameOf(m => m.Province)" data-value="@Model.Province"> </select>
        //      <select class="form-control city" name="@Html.NameOf(m => m.City)" data-value="@Model.City"></select>
        //      <select class="form-control area" name="@Html.NameOf(m => m.Area)" data-value="@Model.Area"></select>
    }
}