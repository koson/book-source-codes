var CustomerApp = {};

CustomerApp.CustomerManager = (function () {
    return {
        SelectAll: function (callback) {
            $.ajax({
                url: "/api/customerservice",
                type: "GET",
                dataType: "json",
                success: function (data) {
                    callback(data);
                }
            });
        },
        SelectByID: function (id, callback) {
            $.ajax({
                url: "/api/customerservice/" + id,
                type: "GET",
                dataType: "json",
                success: function (data) {
                    callback(data);
                }
            });
        },
        Insert: function (obj, callback) {
            $.ajax({
                url: "/api/customerservice",
                type: "POST",
                data: JSON.stringify(obj),
                contentType:"application/json",
                dataType: "json",
                success: function (msg) {
                    callback(msg);
                }
            });
        },
        Update: function (obj, callback) {
            $.ajax({
                url: "/api/customerservice/" + obj.CustomerID,
                type: "PUT",
                data: JSON.stringify(obj),
                contentType: "application/json",
                dataType: "json",
                success: function (msg) {
                    callback(msg);
                }
            });
        },
        Delete: function (id, callback) {
            $.ajax({
                url: "/api/customerservice/" + id,
                type:"DELETE",
                dataType: "json",
                success: function (msg) {
                    callback(msg);
                }
            });
        }
    };
}());

