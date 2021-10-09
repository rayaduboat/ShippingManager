var $pac = $pac || {};
$pac.Expenditure = $pac.Expenditure || {};
$pac.Utilities = $pac.Utilities || {};
$pac.Expenditure.Method = function () {
    this.BeginInvoke = function (url, obj, handler) {
        $.ajax({
            type: "POST",
            url: url,
            data: obj,// JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "JSON",
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
$pac.Expenditure.Initializers = function () {
    this.url = "";
    this.obj = "";
    this.handler = "";
    this.invoke = function () {
        new $pac.Expenditure.Method().BeginInvoke(this.url, this.obj, this.handler)
    }
}
$pac.Expenditure.Record = function () {
    this.AddExpenditure = function (batchid, name, description, paymode, createdate, amount) {
        var v = new $pac.Utilities.Initializers();
        var obj, url, handler;
        var errVal = document.getElementById('err');
        var expAmount = $('#' + amount).val();
        alert(expAmount);
        if (expAmount == 0.00 || expAmount=="") {
            $('#err').text("Please enter a valid amount");
            errVal.style.color = "red";
            $('#Amount').focus();
            return false;
        }
        else {
            
            v.obj = {
                BatchId: $('#'+batchid).val(),
                Name: $('#' + name).val(),
                Description: $('#' +description).val(),
                ModeOfPayments: $('#' + paymode).val(),
                CreatedDate: $('#' +createdate).val(),
                Amount: $('#' +amount).val(),
            };
            v.url = "/Expenditures/Create";
            v.handler = function (jsondata) {
                var results = JSON.parse(jsondata);
                alert(results.message);
                $('#Amount').val("");
                //ExpensesItemCollection();
            }
            //v.invoke();
            v.invokePost();
        }

    }
    this.LoadExpenses = function (itemname) {
        var item = $('#' + itemname).val(); //document.getElementById(itemname)
        var obj, url, handler;
        var v = new $pac.Expenditure.Initializers();
        v.obj = { itemName: item };
        v.url = "/Expenditures/LoadDescription";
        v.handler = function () {
            alert("Back");
        }
    }

    function AddExpenditure() {
        try {
            var expJsonFunc = new MyExpJson();
            var expJsonItems = new ExpensesItemCollection();

            expJsonFunc.data = JSON.stringify(expJsonItems);
            //expJsonFunc.data = passData;
            $.ajax(expJsonFunc)
        } catch (e) {

        }
    }

    //function MyExpJson() {

    //    this.Type = 'POST'
    //    this.url = '@Url.Action("PostExpenses", "Expenditures")'
    //    this.data = "{'data':'123'}"
    //    this.dataType = 'json'
    //    this.contentType = 'application/json; charset=utf-8'
    //    this.success = function (result) {
    //        alert("Expenditure added successfully");
    //    }
    //}

    function ExpensesItemCollection() {
        $('#BatchId').val("");
        $('#ExpensesItems').val("");
        $('#ListBoxDescription').val("");
        $('#ModeOfPayments').val("");
        $('#CreatedDate').val("");
        $('#Amount').val("");
        $('#Amount').focus();
    }
}

