var CustomerApp = {};

CustomerApp.CustomerManager = (function () {

    var Get = function (callback) {
        $.ajax({
            url: "/api/customerservice",
            type: "GET",
            dataType: "json",
            success: function (data) {
                callback(data);
            }
        });
    }

    var GetByID = function (id, callback) {
        $.ajax({
            url: "/api/customerservice/" + id,
            type: "GET",
            dataType: "json",
            success: function (data) {
                callback(data);
            }
        });
    }

    var Post = function (obj, callback) {
        $.ajax({
            url: "/api/customerservice",
            type: "POST",
            data: JSON.stringify(obj),
            contentType: "application/json",
            dataType: "json",
            success: function (msg) {
                callback(msg);
            }
        });
    }

    var Put = function (obj, callback) {
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
    }

    var Delete = function (id, callback) {
        $.ajax({
            url: "/api/customerservice/" + id,
            type: "DELETE",
            dataType: "json",
            success: function (msg) {
                callback(msg);
            }
        });
    }

    return {
        SelectAll: Get,
        SelectByID: GetByID,
        Insert: Post,
        Update: Put,
        Delete: Delete
    };
}());

