var MyApp = {};

MyApp.Search = {};

MyApp.Search.SearchByID = function (id, target) {
    $.ajax({
        url: "/home/SearchByID",
        data: { "id": id },
        success: function (results) {
            var table = "<table border='1' cellpadding='10'>";
            for (var i = 0; i < results.length; i++) {
                table += "<tr>";
                table += "<td>" + results[i].CustomerID + "</td>";
                table += "<td>" + results[i].CompanyName + "</td>";
                table += "<td>" + results[i].ContactName + "</td>";
                table += "<td>" + results[i].Country + "</td>";
                table += "</tr>";
            }
            $("#" + target).html(table);
        },
        dataType: "json"
    });
}

MyApp.Search.SearchByCompany = function (companyname,target) {
    $.ajax({
        url: "/home/SearchByCompany",
        data: { "companyname": companyname },
        success: function (results) {
            var table = "<table border='1' cellpadding='10'>";
            for (var i = 0; i < results.length; i++) {
                table += "<tr>";
                table += "<td>" + results[i].CustomerID + "</td>";
                table += "<td>" + results[i].CompanyName + "</td>";
                table += "<td>" + results[i].ContactName + "</td>";
                table += "<td>" + results[i].Country + "</td>";
                table += "</tr>";
            }
            $("#" + target).html(table);
        },
        dataType: "json"
    });
}

MyApp.Search.SearchByContact = function (contactname,target) {
    $.ajax({
        url: "/home/SearchByContact",
        data: { "contactname": contactname },
        success: function (results) {
            var table = "<table border='1' cellpadding='10'>";
            for (var i = 0; i < results.length; i++) {
                table += "<tr>";
                table += "<td>" + results[i].CustomerID + "</td>";
                table += "<td>" + results[i].CompanyName + "</td>";
                table += "<td>" + results[i].ContactName + "</td>";
                table += "<td>" + results[i].Country + "</td>";
                table += "</tr>";
            }
            $("#" + target).html(table);
        },
        dataType: "json"
    });
}

MyApp.Search.SearchByCountry = function (country,target) {
    $.ajax({
        url: "/home/SearchByCountry",
        data: { "country": country },
        success: function (results) {
            var table = "<table border='1' cellpadding='10'>";
            for (var i = 0; i < results.length; i++) {
                table += "<tr>";
                table += "<td>" + results[i].CustomerID + "</td>";
                table += "<td>" + results[i].CompanyName + "</td>";
                table += "<td>" + results[i].ContactName + "</td>";
                table += "<td>" + results[i].Country + "</td>";
                table += "</tr>";
            }
            $("#" + target).html(table);
        },
        dataType: "json"
    });
}