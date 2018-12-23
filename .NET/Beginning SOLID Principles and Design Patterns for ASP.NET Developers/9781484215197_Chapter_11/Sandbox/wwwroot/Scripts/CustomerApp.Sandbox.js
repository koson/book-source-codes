(function (global) {
    var CustomerApp = function (modules, callback) {
        if (!(this instanceof CustomerApp)) {
            return new CustomerApp(modules, callback);
        }
        for (var i = 0; i < modules.length; i++) {
            CustomerApp.modules[modules[i]](this);
        }
        console.log(this);
        callback(this);
    };
    CustomerApp.modules = {};
    global.CustomerApp = CustomerApp;
})(this);


CustomerApp.modules.nestedModule = {};

CustomerApp.modules.nestedModule.hello2 = function (sandbox) {
    sandbox.HelloWorld2 = function () { alert("Hello World 2!"); };
    sandbox.HelloUniverse2 = function () { alert("Hello Universe 2!"); };
}


CustomerApp.modules.hello = function (sandbox) {
    sandbox.HelloWorld = function () { alert("Hello World!"); };
    sandbox.HelloUniverse = function () { alert("Hello Universe!"); };
}

CustomerApp.modules.search = function (sandbox) {

    sandbox.SearchByID = function (id, target) {
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

    sandbox.SearchByCompany = function (companyname, target) {
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



    sandbox.SearchByContact = function (contactname, target) {
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



    sandbox.SearchByCountry = function (country, target) {
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

};

