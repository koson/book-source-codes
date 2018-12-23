function SearchByID(id) {
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
            $("#results").html(table);
        },
        dataType: "json"
    });
}

function SearchByCompany(companyname) {
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
            $("#results").html(table);
        },
        dataType: "json"
    });
}

function SearchByContact(contactname) {
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
            $("#results").html(table);
        },
        dataType: "json"
    });
}

function SearchByCountry(country) {
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
            $("#results").html(table);
        },
        dataType: "json"
    });
}

