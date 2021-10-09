var $pac = $pac || {};
$pac.Utilities = $pac.Utilities || {};
$pac.Utilities = $pac.Utilities || {};
$pac.Utilities.UserLogin = "";
$pac.Utilities.Initializers = function () {
    this.url = "";
    this.obj = "";
    this.handler = "";
    this.invoke = function () {
        new $pac.Utilities.Method().BeginInvoke(this.url, this.obj, this.handler);
    }
    this.invokePost = function () {
        new $pac.Utilities.Method().BeginInvokePost(this.url, this.obj, this.handler)
    }
}
$pac.Utilities.Method = function () {
    this.BeginInvoke = function (url, obj, handler) {
        var data = JSON.stringify(obj);
        $.ajax({
            type: "GET",
            url: url,
            data: obj,//JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                // Do something interesting here.
                handler(msg);
            },
            error: function (e) {
                //alert(e.responseText);
            }
        });
    }
    this.BeginInvokeAll = function (url, obj, handlers) {
        $.ajax({
            type: "GET",
            url: url,
            data: obj,//JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                // Invoke all the methods passed.
                for (var i = 0; i < handlers.length; i++) {
                    handler(i)();
                }


            },
            error: function (e) {
                alert(e.responseText);
            }
        });
    }
    this.BeginInvokePost = function (url, obj, handler) {
        //var data = JSON.stringify(obj);
        $.ajax({
            type: 'POST',
            url: url,
            data: obj,//JSON.stringify(obj),
            contentType: 'application/json; charset=utf-8',    //contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                // Do something interesting here.
                handler(msg);
            },
            error: function (e) {
                //alert(e.responseText);
            }
        });
    }
}
$pac.Exception = function (message) { this.message = alert(message); return false; }
$pac.Utilities.FillOptions = function (filter, element) {

    if (filter === null) return false;
    var array = [];
    for (var i = 0; i < filter.length; i++) {
        var option = document.createElement("option");
        if (array.indexOf(filter[i]) == -1) {
            option.value = filter[i];
            option.innerHTML = filter[i];
            document.getElementById(element).appendChild(option);
            array.push(filter[i]);
        }
    }

};
$pac.Utilities.DateToday = function () {
    this.fullDate = function () {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!
        var yyyy = today.getFullYear();

        if (dd < 10) {
            dd = '0' + dd
        }

        if (mm < 10) {
            mm = '0' + mm
        }

        today = dd + '/' + mm + '/' + yyyy;
        //document.write(today);
        return today;
    }
    this.full_Date = function () {
        var date = new Date();
        var year = date.getFullYear();
        var month = date.getMonth() + 1;
        var day = date.getDate();
        if (day < 10) {
            day = '0' + day;
        }
        if (month < 10) {
            month = '0' + month;
        }
        //////var formattedDate = day + '-' + month + '-' + year
        var formattedDate = year + '-' + month + '-' + day
        //document.write(formattedDate);
        return formattedDate;
    }
    this.YearOfDate = function () {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!
        var year = today.getFullYear();
        return year;
    }
    this.MonthOfDate = function () {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!
        var year = today.getFullYear();
        return mm;
    }
    this.DayOfDate = function () {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!
        var year = today.getFullYear();
        return dd;
    }
}





