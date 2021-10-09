var $pac = $pac || {};
$pac.Server = $pac.Server || {};
$pac.ShippingItems = $pac.ShippingItems || {};
$pac.ShippingItems.Parameters = function () {
    this.batchID = 0;
    this.senderID = 0;
    this.recipientID = 0;
    this.referenceNum = 0;
    //var batchID, senderID, RecipientID, referenceNum;
}
$pac.ShippingItems.ReferenceNumber = function () {
    this.GetRef = function (consumingFunction) {
        var obj, url, handler
        obj = {};
        url = "/TripDetails/GetNewRef";
        handler = function (jsondata) {
            consumingFunction(jsondata);
        };

        new $pac.Server.Method().BeginInvoke(url, obj, handler);

    }

}
$pac.ShippingItems.SearchCustomerDetails = function () {
    this.GetParameters = function (tel, postcode) {
        var clientUrl = window.location.search;
        if (clientUrl != "") {
            getParams(clientUrl);
        } else {
            findSender(tel, postcode);
        }
        function getParams(clientUrl) {
            var params = {};
            var parser = document.createElement('a');
            parser.href = clientUrl;
            var query = parser.search.substring(1);
            var vars = query.split('&');
            for (var i = 0; i < vars.length; i++) {
                var pair = vars[i].split('=');
                params[pair[0]] = decodeURIComponent(pair[1]);
                //alert(params[i].Tel);
            }
            document.getElementById(tel).innerHTML = params.Tel;// alert(params.Tel);
            document.getElementById(postcode).innerHTML = params.Postcode// alert(params.Postcode);
            findSender(params.Tel, params.Postcode);
        }
    }
    function findSender(tel, postcode) {
        if (tel != "" || postcode != "") {
            var pr = new $pac.Server.Initializers();
            pr.url = "/TripDetails/FindSenderRecord";
            pr.obj = { telephone: tel, postcode: postcode };
            pr.handler = function (jsondata) {
                var parsed = JSON.parse(jsondata);
                if (parsed.message == "No Record Found!") new $pac.Exception(parsed.message);
                //var parsed = JSON.parse(jsondata);
                if (parsed.message == "Record Found Successfully!") {

                    var id = parsed.Results.ID;
                    //pulling back the sender and recopients IDs
                    //===========================================================
                    new $pac.ShippingItems.Parameters().senderID = id;
                    var newRecipientBtn = document.getElementById('recModalBtnMob');
                    //var newRecipientBtn = document.getElementBy('recModalBtn');
                    newRecipientBtn.ID = id;
                    //===========================================================

                    var fullname = parsed.Results.FirstName + " " + parsed.Results.LastName; //result.split('~')[1];
                    $('#SenderID').val(fullname);
                    $('#SenderIDMob').val(fullname);
                    $('#SenderName').text(fullname);
                    $('#SenderNameMob').text(fullname);
                    GetSenderRecipient(id);
                    $('#catchError').show();
                    var err = document.getElementById('catchError');
                    err.innerHTML = "successful!";
                    $('#catchError').css('color', 'green');
                }
                else {
                    alert("Record not found please contact PACO");
                }
            }
            pr.invoke();
        }
        else {

            //var pr = new $pac.Server.Initializers();
            //pr.url = "/TripDetails/FindSenderRecord";
            //pr.obj = {};
            //pr.handler = function (jsondata) {
            //    var parsed = JSON.parse(jsondata);
            //    if (parsed.message == "No Record Found!") new $pac.Exception(parsed.message);
            //    //var parsed = JSON.parse(jsondata);
            //    if (parsed.message == "Record Found Successfully!") {

            //        var id = parsed.Results.ID;
            //        //pulling back the sender and recopients IDs
            //        //===========================================================
            //        new $pac.ShippingItems.Parameters().senderID = id;
            //        var newRecipientBtn = document.getElementById('recModalBtnMob');
            //        //var newRecipientBtn = document.getElementBy('recModalBtn');
            //        newRecipientBtn.ID = id;
            //        //===========================================================

            //        var fullname = parsed.Results.FirstName + " " + parsed.Results.LastName; //result.split('~')[1];
            //        $('#SenderID').val(fullname);
            //        $('#SenderIDMob').val(fullname);
            //        $('#SenderName').text(fullname);
            //        $('#SenderNameMob').text(fullname);
            //        GetSenderRecipient(id);
            //        $('#catchError').show();
            //        var err = document.getElementById('catchError');
            //        err.innerHTML = "successful!";
            //        $('#catchError').css('color', 'green');
            //    }
            //    else {
            //        alert("Record not found please contact PACO");
            //    }
            //}
            //pr.invoke();

            alert("Not Authorised! Please contact PACO");
            //kill app here..............................
        }
    }
}
$pac.ShippingItems.CustomerDetails = function () {
    this.Sender = function () {
        var mySender = $('#SenderIDMob').attr("disabled", "enabled").val();
        return mySender;
    }
    this.SenderDST = function () {
        var mySender = $('#SenderID').attr("disabled", "enabled").val();
        return mySender;
    }
    this.Recipient = function () {
        var myRecipient = $('#TextRecipientMob').attr("disabled", "enabled").val();
        return myRecipient;
    }
    this.RecipientDST = function () {
        var myRecipient = $('#TextRecipient').attr("disabled", "enabled").val();
        return myRecipient;
    }
}
$pac.ShippingItems.ClientOrder = function () {
    var ref;
    var orderItemsList = [];
    var sender;
    var recipient;
    var senderDST;
    var recipientDST;
    var senderRecipient;
    var batch;
    this.LoadItemModalDST = function (dropdown) {
        senderDST = new $pac.ShippingItems.CustomerDetails().SenderDST();
        recipientDST = new $pac.ShippingItems.CustomerDetails().RecipientDST();
        if (senderDST != "" && recipientDST != "") {
            $('#callItemModal').click();
            new $pac.ShippingItems.ClientOrder().LoadItemDetails(dropdown);
        }
        else {
            alert("Please select a sender and a recipient!")
            return;
        }
    }
    this.LoadItemModalMob = function (dropdown) {
        sender = new $pac.ShippingItems.CustomerDetails().Sender();
        recipient = new $pac.ShippingItems.CustomerDetails().Recipient();
        if (sender != "" && recipient != "") {
            $('#callItemModal').click();
            new $pac.ShippingItems.ClientOrder().LoadItemDetails(dropdown);
        }
        else {
            alert("Please select a sender and a recipient!")
            return;
        }
    }
    this.LoadItemDetails = function (dropdown) {
        var para = new $pac.Server.Initializers();
        para.url = "/TripDetails/LoadItems";
        para.obj = {};
        para.handler = function (jsondata) {
            if (jsondata == "failed adding account") new $pac.Exception(jsondata);
            var parsed = JSON.parse(jsondata);
            if (parsed.message = "Loaded successful") {
                $("#" + dropdown).empty();
                $pac.Utilities.FillOptions([""], dropdown);
                var sorted = parsed.Results.map(function (e) {
                    return e.Name
                }).sort();
                for (var i = 0; i < sorted.length; i++) {
                    var option = document.createElement("option");
                    option.value = sorted[i];//.Name;
                    option.innerHTML = sorted[i];//.Name;// + " " + parsed.Results[i].AccountType;
                    var element = document.getElementById(dropdown);
                    element.appendChild(option);
                }
            }
        }
        para.invoke();
    }
    this.ClearDynamicTable = function (tabBodyName) {
        $('#textBoxQty').val("");
        $('#selectShippingItems').val("");
        $('#textBoxDetails').val("");
        var dynamicTable = $('#' + tabBodyName).empty();
    }
    this.setFocusQty = function (focusedElement) {
        $('#' + focusedElement).focus();
    }
    this.NextItem = function (item, qty, details) {
        var itemValue = document.getElementById(item).value;
        var qtyValue = document.getElementById(qty).value
        if (itemValue.trim() == "") { new $pac.Exception("Please Seletect a valid Item"); $('#selectShippingItems').focus(); return; }
        if (qtyValue == "") { new $pac.Exception("Please input a valid Quantity"); $('#textBoxQty').focus(); return; }
        try {
            var tabFly = document.getElementById('itemTableFly');
            var trh = document.createElement("tr");
            var tr = document.createElement('tr');
            var td = document.createElement('td');
            td.className = "form-control btn-secondary"

            var ItemTb = document.getElementById('tabItemTbody');
            var tdItem = document.createElement('td'); tdItem.innerHTML = $('#' + item).val();
            var tdQty = document.createElement('td'); tdQty.innerHTML = $('#' + qty).val();
            var tdDetails = document.createElement('td'); tdDetails.innerHTML = $('#' + details).val();
            var btEdit = document.createElement('button'); btEdit.innerHTML = "Edit";
            //btEdit.ID = 1;
            tr.appendChild(tdItem);
            tr.appendChild(tdQty);
            tr.appendChild(tdDetails);
            tr.appendChild(btEdit);
            ItemTb.appendChild(tr);

            tabFly.appendChild(ItemTb);

            $('#textBoxQty').val("");
            $('#selectShippingItems').val("");
            $('#textBoxDetails').val("");
            $('#selectShippingItems').focus();
        } catch (e) {

        }
    }
    this.SubmitOrder = function (orderTable) {
        new $pac.ShippingItems.ReferenceNumber().GetRef(consumingFunction);
        var itemTabVar = document.getElementById(orderTable);//.getElementsByTagName('tr').length;
        var itemNamList = [];
        var qtyList = [];
        var detailsList = [];
        var orderList = [];
    }

    function consumingFunction(jsondata) {
        var senderId = $('#senderId').val();
        sender = new $pac.ShippingItems.CustomerDetails().Sender();
        recipient = new $pac.ShippingItems.CustomerDetails().Recipient();
        senderDST = new $pac.ShippingItems.CustomerDetails().SenderDST();
        recipientDST = new $pac.ShippingItems.CustomerDetails().RecipientDST();
        batch = $('#BatchNo').attr("disabled", "enabled").val();
        ref = jsondata;
        if (recipient != "") {
            senderRecipient = sender + "~" + recipient;
        }
        else {
            senderRecipient = senderDST + "~" + recipientDST;
        }
        orderItemsList.push(batch + "~" + ref + "~" + senderRecipient + "~" + senderId);
        $('#itemTableFly>tbody>tr').each(function () {
            var orderParam = $(this).find("td").html() + "~" + $(this).find("td").eq(1).html() + "~" + $(this).find("td").eq(2).html();
            orderItemsList.push(orderParam);
        });
        //alert(orderItemsList);
        var vrb = new $pac.Server.Initializers();
        vrb.obj = { itemList: orderItemsList };
        vrb.url = "/TripDetails/submitOrder";
        vrb.handler = function (jsondata) {
            if (jsondata == "failed adding account") new $pac.Exception(jsondata);
            var parsed = JSON.parse(jsondata);
            if (parsed.message = "Loaded successful") {
                //alert("Your Reference Number is: " + parsed.Results.ActualRef);
            }
            $('#buttonItemClose').click();
            //$('#findReloadMob').click();
            //orderItemsList = orderItemsList.slice(1);
            //window.location.href="TripListReport.cshtml";
            localStorage.setItem("clientOrderStore", JSON.stringify({
                ref: parsed.Results.ActualRef, list: orderItemsList
            }));
            window.location.assign("tripReportView");
            //sessionStorage.setItem("clientOrder", JSON.stringify({
            //    ref: parsed.Results.ActualRef, list: orderItemsList
            //}));
            new $pac.Reporting.Receipt(parsed.Results.ActualRef, orderItemsList);
        }
        vrb.invoke();
    }
}

