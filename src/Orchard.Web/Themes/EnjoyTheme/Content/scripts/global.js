
(function ($, window, undefined) {

    var Enjoy = window.Enjoy = window.Enjoy || {};
    Enjoy.Dashboard = Enjoy.Dashboard || {};
    var Portal = Enjoy.Dashboard.Portal = Enjoy.Dashboard.Portal || {};
    
   
    Portal.AMapWebServiceKey = function () {
        return "ec28437080b2fd8b1c9f4c448371c2a8";
    };
    Portal.QueryByAddress = function (address, callback) {
        var url = "https://restapi.amap.com/v3/geocode/geo?address=" + address + "&output=json&key=" + Portal.AMapWebServiceKey();
        $.ajax({
            type: "GET",
            url: url,
            data: {},
            success: function (data) {
                callback(data);
            }
        });
    };

    Portal.AppendAntiForgeryToken = function (data) {
        data = data = data || {};
        data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
        return data;
    };
    Portal.GeoByAddress = function (address,callback) {
        $.ajax({
            type: "GET",
            url: "https://restapi.amap.com/v3/geocode/geo?address=" + address,
            data: {},
            success: function (data) {
                callback(data);
            }
        });
    };

}($, window, undefined));

