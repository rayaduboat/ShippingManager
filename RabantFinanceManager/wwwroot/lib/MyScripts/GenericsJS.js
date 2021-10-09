var $rab = $rab || {};
var rab.Generics = rab.Generics || {}
$rab.Generics.Method = function () {
    this.BeginInvoke = function (url, obj, handler) {
        var data = JSON.stringify(obj);
        $.ajax({
            type: "GET",
            url: url,
            data: data,
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
$rab.Generics.SearchOrder = function (postcode, telephone) {
    try {
        var url, obj, handler;
        url = '/TripDetails/getSender';
        obj = { postcode: postcode, telephone: telephone };
        handler = function (result) {
            $('#SenderID').val(result);
        }
        new $rab.Generics.Method().BeginInvoke(url, obj, handler);
    } catch (e) {
    }
}
$rab.Generics.Initializers = function () {
    this.url = "";
    this.obj = "";
    this.handler = "";
    this.invoke = function () {
        new $rab.Generics.Method().BeginInvoke(this.url, this.obj, this.handler)
    }
}
$rab.Exception = function (message) { this.message = alert(message); return false; }
