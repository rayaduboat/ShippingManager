var $pac = $pac || {};
$pac.myScript = $pac.myScript || {};
$pac.myScript.SearchClient = function () {
    this.getSender = function (tel, postcode) {

    }
}
$pac.myScript.Getdata = function () {
    this.BeginInvoke = function (url, obj, handler) {
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(obj),
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
            type: "POST",
            url: url,
            data: JSON.stringify(obj),
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
}
$pac.myScript.Initializers = function () {
    this.url = "";
    this.obj = "";
    this.handler = "";
    this.invoke = function () {
        new $pac.Server.Method().BeginInvoke(this.url, this.obj, this.handler)
    }
}
$pac.Exception = function (message) { this.message = alert(message); return false; }
$pac.myScript.FillDropDown = function (filter, element) {

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
$pac.myScript.DateToday = function () {
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

        today = mm + '/' + dd + '/' + yyyy;
        document.write(today);
        return today;
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

