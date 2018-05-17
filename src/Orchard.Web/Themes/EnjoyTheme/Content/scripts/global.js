
(function ($, window, undefined) {

    var Enjoy = window.Enjoy = window.Enjoy || {};
    Enjoy.Dashboard = Enjoy.Dashboard || {};
    var Portal = Enjoy.Dashboard.Portal = Enjoy.Dashboard.Portal || {};
    
    ApplyProtocol = {}
    Portal.GetApplyProtocol = function () {
        
      
        
       
    }
    Portal.GetApplyProtocol();

    Portal.AppendAntiForgeryToken = function (data) {
        data = data = data || {};
        data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
        return data;
    };

}($, window, undefined));

