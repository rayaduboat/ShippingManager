var $pac = $pac || {};
$pac.TripManagement = $pac.TripManagement || {};
$pac.Utilities = $pac.Utilities || {};
$pac.TripManagement.SenderRecord = [];
$pac.TripManagement.LoginUser = function () {
    this.setLoginStorage = function (postcode,telephone,email) {
        localStorage.setItem("loginUser", JSON.stringify({
            email: email, telephone: telephone, postcode: postcode
        }));
    }
    this.getLoginStorage = function () {
        var myLoginUser = localStorage.getItem("loginUser")
        var user = JSON.parse(myLoginUser);
        if (user.email !="info@pacoshipping.com") {
            $('#txtTel').val(user.telephone);
            $('#txtPostcode').val(user.postcode);
            $('#findCustomerDetails').click();
        }
       // alert(user.telephone);
    }
}
$pac.TripManagement.DeleteClientOrder = function () {
    this.WholeOrder = function (batch, refNo) {
        var url, obj, handler;
        $('#catchErrorRef').hide();
        obj = { batch: batch, myref: refNo };
        url = "/TripDetails/DeleteOrderJS";
        handler = function (jsondata) {
            if (jsondata == "Failed Deleting Order") new $pac.Exception(jsondata);
            alert(jsondata);
            window.location.reload();
        }
        new $pac.Utilities.Method().BeginInvoke(url, obj, handler);
    }
    this.OrderItem = function (itemID) {

    }
}
$pac.TripManagement.ClientLink = function () {
    this.SendToClient = function (checkboxItem, fnam, lnam, addr1, town, postcode, tel, link, mUrl) {
        var vtel = $('#' + tel).val().replace(" ", "");
        var vpostcode = $('#' + postcode).val().replace(" ", "");
        var vfnam = $('#' + fnam).val();
        var vlnam = $('#' + lnam).val();
        var vaddr1 = $('#' + addr1).val();
        var vtown = $('#' + town).val();
        var vlink = $('#' + link).val();
        var obj, url;
        if (document.getElementById(checkboxItem).checked) {
            if (vpostcode == "" || vtel == "") {
                new $pac.Exception("Postcode and Telephone required!");
                $('#' + postcode).focus();
            }
            else {
                obj = {
                    postcode: vpostcode,
                    telephone: vtel
                };
                url = "/TripDetails/quickSearchSender";
                this.getCustomerLink(obj, url, vtel, vpostcode, link, mUrl);
            }
        }
        else {
            if (vfnam == "" || vtown == "" || vpostcode == "" || vtel == "" || vlnam == "") {
                new $pac.Exception("All Sender's Details required");
            }
            else {
                obj = {
                    fname: vfnam,
                    lname: vlnam,
                    address1: vaddr1,
                    town: vtown,
                    postcode: vpostcode.replace(" ", ""),
                    telephone: vtel.replace(" ", "")
                };
                url = "/TripDetails/SearchSender";

                this.getCustomerLink(obj, url, vtel, vpostcode, link, mUrl);
                //new TripManagement.ClientLink.getCustomerLink(obj, url, vtel, vpostcode)
                //getCustomerLink(obj, url, vtel, vpostcode);
            }
        }
        //new $pac.TripManagement.ClientLink().ClearFields(checkboxItem, fnam, lnam, addr1, town, postcode, tel); callClientModule
    }
    this.LinkCopy = function (element) {
        var copyText = document.getElementById(element);
        //copyText.select();
        document.execCommand("copy");
        //alert("Copied the text: " + copyText.value);
    }
    this.DisableEnableFields = function (checkboxItem, fnam, lnam, addr1, town, postcode, tel) {

        if (document.getElementById(checkboxItem).checked) {
            $('#' + fnam).val("");
            $('#' + lnam).val("");
            $('#' + addr1).val("");
            $('#' + town).val("");
            $('#' + fnam).attr("disabled", "disabled");
            $('#' + lnam).attr("disabled", "disabled");
            $('#' + addr1).attr("disabled", "disabled");
            $('#' + town).attr("disabled", "disabled");
            $('#' + postcode).focus();
        } else {
            $('#' + fnam).prop('disabled', false);
            $('#' + lnam).prop('disabled', false);
            $('#' + addr1).prop('disabled', false);
            $('#' + town).prop('disabled', false);
            $('#' + fnam).focus();
        }
    }
    this.ShowCustomerLink = function (customerLink, mySection) {
        if (document.getElementById(customerLink).checked) {
            //var isChecked = $('#' + customerLink).is(":checked");
            $('#' + mySection).addClass("doShow");
            //$('#' + mySection).addClass("doShow");
        }
        else {
            //$('#' + mySection).hide();
            $('#' + mySection).addClass("noShow");
        }

        ////var isChecked = $('#' + customerLink)
        ////if (document.getElementById(customerLink).checked) {
        ////    $('#' + mySection).addClass("doShow");
        ////}

        //////if (document.getElementById(customerLink).unchecked) {
        ////if (document.getElementById(customerLink).unchecked) {
        ////    $('#' + mySection).addClass("noShow");
        ////}

        //else {
        //    $('#' + mySection).addClass("noShow");
        //}
    }
    this.ClearFields = function (checkboxItem, fnam, lnam, addr1, town, postcode, tel) {
        $('#' + fnam).val("");
        $('#' + lnam).val("");
        $('#' + addr1).val("");
        $('#' + town).val("");
        $('#' + postcode).val("");
        $('#' + tel).val("");

    }
    this.getCustomerLink = function (obj, url, telephone, postcode, link, mUrl) {
        // alert(postcode + " " + telephone);

        var v = new $pac.Utilities.Initializers();
        v.obj = obj;
        v.url = url;
        v.handler = function (jsondata) {
            if (jsondata == "failed adding Sender") new $pac.Exception(jsondata);
            var parsed = JSON.parse(jsondata);
            var sendUrl = "rayaduboat-001-site2.itempurl.com/Clients/Create?";


            var clientURL = sendUrl + parsed.customerLinkHash;
            $('#' + link).val(clientURL);
            $('#' + mUrl).val(clientURL);
            $('#txtLinkToSend').text(clientURL);
            var myCopy = document.getElementById(mUrl);
            copyToClipboard(myCopy);
            //document.execCommand('copy');
        };
        v.invoke();
    }
    this.loadCustOrder = function (batch, refNo) {
        var url, obj, handler;
        $('#catchErrorRef').hide();
        obj = { batch: batch, myref: refNo };
        url = "/TripDetails/getOrderJS";

        handler = function (jsondata) {
            if (jsondata == "failed getting items") new $pac.Exception(jsondata);
            var parsed = JSON.parse(jsondata);
            if (parsed.message = "Loaded successful") {

                var mytable = document.getElementById('itemTableFly');
                var mytableBody = document.getElementById('tabItemTbody');

                //var town = document.getElementById('txtTown');
                //var recipient = document.getElementById('txtTown');
                $(mytableBody).empty();
                refNo = parsed.result[0].actualRef;
                $('#txtTown').text(parsed.result[0].town);
                $('#txtRecipient').text(parsed.result[0].theRecipient);
                for (var i = 0; i < parsed.result.length; i++) {

                    try {
                        var tr = document.createElement('tr');
                        var tdTripID = document.createElement('td'); tdTripID.innerHTML = parsed.result[i].tripID;
                        var tdItemName = document.createElement('td'); tdItemName.innerHTML = parsed.result[i].itemName;
                        //var tdComment = document.createElement('td'); tdComment.innerHTML = parsed.result[i].comment;
                        var tdQuantity = document.createElement('td'); tdQuantity.innerHTML = parsed.result[i].quantity;
                        var tdEditBtn = document.createElement('button'); tdEditBtn.innerHTML = "Edit"; tdEditBtn.record = parsed.result[i];
                        var tdDeleteBtn = document.createElement('button'); tdDeleteBtn.innerHTML = "Del"; tdDeleteBtn.record = parsed.result[i];

                        tdEditBtn.onclick = function () {
                            // $('#myItemsModalEdit').attr('display', 'block');*************************
                            //$('#myItemsModalEdit').css('display', 'block');
                            // $('#callItemEditModal').click();
                        }
                        tdDeleteBtn.onclick = function () {
                            var itemId = this.record.TripID;
                            // $pac.TripManagement.DeleteClient().OrderItem(itemId);*******************
                        }
                        //tr.appendChild(tdTripID)
                        tr.appendChild(tdItemName)
                        //tr.appendChild(tdComment)
                        tr.appendChild(tdQuantity)
                        tr.appendChild(tdEditBtn)
                        tr.appendChild(tdDeleteBtn)
                        mytableBody.appendChild(tr)
                    } catch (e) {
                        alert(e.message);
                    }
                }

                mytable.appendChild(mytableBody);
                ////var err = document.getElementById('catchErrorRef');
                ////$('#catchErrorRef').show();
                ////$('#catchErrorRef').css('color', 'green');
                ////err.innerHTML = "Successful!";

                // $('#callMyItemModal').click();
            }
            else {
                ////var err = document.getElementById('catchErrorRef');
                ////$('#catchErrorRef').show();
                ////$('#catchErrorRef').css('color', 'red');
                ////err.innerHTML = "Order cannot be found!";
                ////alert(err.innerHTML);
                //$('#RefID').attr('disabled', true);
                //$('#TextSearch').val("");
                //$('#TextSearch').focus();
            }
        }
        new $pac.Utilities.Method().BeginInvoke(url, obj, handler);
    }
    this.CreateUniqueString = function (tel, postcode) {
        var randLetter = String.fromCharCode(65 + Math.floor(Math.random() * 26));
        var uniqid = randLetter + Date.now();
        var myKey = btoa(Math.random()).substring(0, 12)
        var uniqid1 = uniqid.substring(0, 4); //+ "_" + "01e" + MI + "e02" +;
        var uniqid2 = uniqid.substring(5, uniqid.length);
        var uniqid3 = myKey.substring(0, 8);
        var uniqid4 = myKey.substring(9, myKey.length);
        var finalKey = uniqid1 + "_" + uniqid3 + "01e1" + tel + uniqid2 + "_" + uniqid4 + "e02" + postcode;
        finalKey = finalKey.toLowerCase();
        //alert(uniqid);
        //alert(myKey);
        //alert(uniqid1);
        //alert(uniqid2);
        //alert(uniqid3);
        //alert(uniqid4);
        alert(finalKey);
    }
    this.GetSender = function (tel, postcode) {
        var vtel = $('#' + tel).val().replace(" ", "");
        var vpostcode = $('#' + postcode).val().replace(" ", "");
        var obj, url;
        if (vpostcode == "" || vtel == "") {
            new $pac.Exception("Postcode and Telephone required!");
        }
        else {
            obj = {
                postcode: vpostcode,
                telephone: vtel
            };
            var link = document.getElementById('customerLinkA');
            var mUrl = document.getElementById('myUrlDTA');
            url = "/TripDetails/quickSearchSender";
            var orderTel = vtel;
            $('#orderTel').val(vtel);
            $('#orderPostcode').val(vpostcode);
            this.getCustomerLink(obj, url, vtel, vpostcode, link, mUrl);


            findSender(vtel, vpostcode);
        }
    }
    this.CheckOrderStatus = function (param) {
        var v = new $pac.Utilities.Initializers();
        v.obj = { Link: param };
        v.url = "/Clients/IsLinkAvailable";
        v.handler = function (jsondata) {
            if (jsondata == "Link Not Available!") { new $pac.Exception(jsondata); window.location.assign("../TripDetails/GoodBye"); }
            else {
                if (jsondata != "") {
                    var paramList = [];
                    var parsed = JSON.parse(jsondata);
                    paramList = parsed.uniqueString.split('~');
                    $('#txtPostcode').val(paramList[1]);
                    $('#txtTel').val(paramList[2]);
                    $('#txtPostcode').attr('disabled', true);
                    $('#txtTel').attr('disabled', true);

                    switch (parsed.orderStatus) {
                        case 0:
                            LoadSenderRecord(paramList[2], paramList[1]);
                            break;
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            LoadExistingOrders(parsed.refNum);
                            break;
                        default:
                    }
                }

                else {
                    $('#sectionFindMe').css("display", "block");
                }
            }
        }
        v.invoke();

    }
    this.LoadSenderRecord = function (e) {
        var e = e;
        var err = document.getElementById('catchError');
        var postcode = $('#txtPostcode').val();
        var telephone = $('#txtTel').val();
        if (postcode == "" || telephone == "") {
            $('#catchError').show();
            err.innerHTML = "Please enter your postcode and telephone";
            $('#catchError').css('color', 'red');
            $('#txtPostcode').val("");
            $('#txtTel').val("");
            $('#txtPostcode').css('background-color', 'red');
            $('#txtTel').css('background-color', 'red');
            $('#txtPostcode').focus();
            return false;
        }
        else {

            $('#catchError').hide();
            var dataSend = postcode + "~" + telephone;
            $.ajax({
                Type: 'GET',
                url: '@Url.Action("getSender","TripDetails")',
                data: { 'data': '"' + dataSend + '"' },
                dataType: 'JSON',
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    if (result !== "No Record Found!") {
                        var id = result.split('~')[0];
                        var fullname = result.split('~')[1];
                        var senderName = document.getElementById('txtSenderFullname');
                        senderName.value = fullname;
                        $('#tSenderId').val(id);
                        staticSenderID = id;//===========================================
                        $('#ModalSenderId').val(id);
                        $('#catchError').show();
                        var err = document.getElementById('catchError');
                        err.innerHTML = "successful!";
                        $('#catchError').css('color', 'green');
                        GetSenderRecipient(id);
                    }
                    else {
                        $('#catchError').show();
                        var err = document.getElementById('catchError');
                        err.innerHTML = result;
                        $('#txtPostcode').val("");
                        $('#txtTel').val("");
                        $('#txtPostcode').focus();
                        $('#catchError').css('color', 'red');
                    }
                }
            });
        }
    }
    this.UpdateSender = function (data) {
        getSenderRecordToUpdate(data);
        $('#callUpDateMyDetailsModal').click();
        //$('#' + sendermodal).click();
    }
    this.SaveSenderRecord = function (shippersid, senderid, title, gender, firstname, lastname, add1, add2, postcode, town, country, telephone, email) {
        var obj, url, handler;
        var v = new $pac.Utilities.Initializers();
        var vshippersid = $('#' + shippersid).val();
        var vsenderid = $('#' + senderid).val();
        var vtitle = $('#' + title).val();
        var vgender = $('#' + gender).val();
        var vfirstname = $('#' + firstname).val();
        var vlastname = $('#' + lastname).val();
        var vadd1 = $('#' + add1).val();
        var vadd2 = $('#' + add2).val();
        var vpostcode = $('#' + postcode).val();
        var vtown = $('#' + town).val();
        var vcountry = $('#' + country).val();
        var vtelephone = $('#' + telephone).val();
        var vemail = $('#' + email).val();
        //var vtitle = $('' + title).val();
        if (vsenderid == "" || vfirstname == "" || vlastname == "" ||
            vpostcode == "" || vtown == "" || vtelephone == "" || vemail == ""
        ) {

            new $pac.Exception("Please complete All your Details");
            return false;
        }
        else {
            v.obj = {
                shippersId: vshippersid,
                senderid: vsenderid,
                title: vtitle,
                gender: vgender,
                firstname: vfirstname,
                lastname: vlastname,
                addresslineone: vadd1,
                addresslinetwo: vadd2,
                postcode: vpostcode,
                posttown: vtown,
                country: vcountry,
                telephone: vtelephone,
                emailaddress: vemail
            };
            var uniqueString = vshippersid + "~" + vpostcode + "~" + vtelephone
            v.url = "/TripDetails/UpdateSenderRecord";
            //var newUniqueCode=
            v.handler = function (jsondata) {
                alert(jsondata);
                $('#buttonCloseSenderUpd').click();
                UpdateCustomerLink(uniqueString)
                //$('#menuLogout').click();
                location.reload();
            }
            v.invoke();
        }

    }
    //this.CreateLicense = function (email, startDate, users) {

    //}
}
$pac.TripManagement.SearchOrder = function () {
    this.ClientOders = function (refNo, batchNo) {
        alert("Check function");
        $('#RefID').attr('disabled', false);
        var err = document.getElementById('catchErrorRef');
        //var batch = $("#BatchID option:selected").prop("selected", true).val();
        //var refNo = $('#TextSearch').val();
        if (batch == "" || refNo == "") {
            $('#catchErrorRef').show();
            err.innerHTML = "Please enter your BatchID and Reference Number";
            $('#BatchID').val("");
            $('#TextSearch').val("");
            $('#BatchID').css('background-color', 'red');
            $('#TextSearch').css('background-color', 'red');
            $('#BatchID').focus();

            $('#BatchIDMob').val("");
            $('#TextSearchMob').val("");
            $('#BatchIDMob').css('background-color', 'red');
            $('#TextSearchMob').css('background-color', 'red');
            $('#BatchIDMob').focus();

            return false;
        }
        else {
            $('#catchErrorRef').hide();
            var dataSend = batch + "~" + refNo;
            $.ajax({
                pe: 'POST',
                url: '@Url.Action("getOrder", "TripDetails")',
                //url: $(this).closest('form').attr('getOrder'),
                data: { 'data': '"' + dataSend + '"' },
                dataType: 'JSON',
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    if (result.length > 0) {
                        var mytableBody = document.getElementById('tabBody');
                        $(mytableBody).empty();
                        $('#SenderName').text(result[0].TheSender);
                        sender = result[0].TheSender;
                        email = result[0].EmailAddress;
                        $('#RecipientName').text(result[0].TheRecipient);
                        recipients = result[0].TheRecipient;
                        $('#RefID').val(result[0].ActualRef.toUpperCase());
                        refNo = result[0].ActualRef.toUpperCase();
                        $('#RefID').attr('disabled', true);
                        $('#originalTotalCost').val(result[0].Total);
                        $('#originalTotalCost').attr('disabled', true);
                        $('#TotalCost').focus();
                        for (var i = 0; i < result.length; i++) {

                            try {
                                var tr = document.createElement('tr');
                                var tdTripID = document.createElement('td'); tdTripID.innerHTML = result[i].TripID;

                                var tdItemName = document.createElement('td'); tdItemName.innerHTML = result[i].ItemName;
                                var tdComment = document.createElement('td'); tdComment.innerHTML = result[i].Comment;
                                var tdQuantity = document.createElement('td'); tdQuantity.innerHTML = result[i].Quantity;
                                var tdEditBtn = document.createElement('button'); tdEditBtn.innerHTML = "EDIT";
                                var tdDeleteBtn = document.createElement('button'); tdDeleteBtn.innerHTML = "DELETE";
                                tr.appendChild(tdTripID)

                                tr.appendChild(tdItemName)
                                tr.appendChild(tdComment)
                                tr.appendChild(tdQuantity)
                                tr.appendChild(tdEditBtn)
                                tr.appendChild(tdDeleteBtn)
                                mytableBody.appendChild(tr)

                            } catch (e) {
                                alert(e.message);
                            }


                        }
                        var err = document.getElementById('catchErrorRef');
                        $('#catchErrorRef').show();
                        $('#catchErrorRef').css('color', 'green');

                        err.innerHTML = "Successful!";
                        alert(err.innerHTML);
                    }
                    else {
                        var err = document.getElementById('catchErrorRef');
                        $('#catchErrorRef').show();
                        $('#catchErrorRef').css('color', 'red');
                        err.innerHTML = "Order cannot be found!";
                        alert(err.innerHTML);
                        $('#RefID').attr('disabled', true);
                        $('#TextSearch').val("");
                        $('#TextSearch').focus();
                    }

                }
            });
        }
    }

}
$pac.TripManagement.Parameters = function () {
    this.batchID = 0;
    this.senderID = 0;
    this.recipientID = 0;
    this.referenceNum = 0;
    //var batchID, senderID, RecipientID, referenceNum;
}
$pac.TripManagement.ReferenceNumber = function () {
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
$pac.TripManagement.SearchCustomerDetails = function () {
    this.GetSender = function (tel, postcode) {
        var vtel = $('#' + tel).val().replace(" ", "");
        var vpostcode = $('#' + postcode).val().replace(" ", "");
        //var obj, url;
        if (vpostcode == "" || vtel == "") {
            new $pac.Exception("Postcode and Telephone required!");
        }
        else {
            findSender(tel, postcode);
            GetSenderRecipient(id);
        }
    }
}
$pac.TripManagement.Shipper = function () {
    this.NewBatch = function (shippersid) {
        var obj, url, handler;
        var shipper = $('#' + shippersid).val();
        if (shipper != 0) {
            obj = { id: shipper };//{ 'data': '"' + shipper + '"' };
        }

        var v = new $pac.Utilities.Initializers();
        v.obj = obj;
        v.url = "/Batches/GetNewBatch";
        v.handler = function (jsondata) {
            if (jsondata == "Failed Creating Batch") new $pac.Exception(jsondata);
            var parsed = JSON.parse(jsondata);
            //alert(parsed.newBatch);
            $('#ActualBatch').val(parsed.newBatch);
            $('#saveNewBatch').prop("disabled", false)
        }
        v.invoke();
    }
}
$pac.TripManagement.Receipt = function () {
    this.PrintReceipt = function () {
        var recTitle = document.getElementById('ReportTitle');
        var data = [];
        var mystoredData = localStorage.getItem("clientReceiptStore");
        var myStore = JSON.parse(mystoredData);
        var refnumber = myStore.ref;
        if (myStore.oStatus != "Created") {
            $('#txtInvoice').css('display', 'block');
            $('#txtInvoice').text("Order Cost: £" + myStore.oCost);
        }
        data = myStore.list;
        $('#ReportTitle').text(myStore.myText);
        $('#txtStatus').text(myStore.oStatus);
        $('#refNum').text(refnumber);
        var myTable = document.getElementById('tableReceipt');
        var myTabBody = document.getElementById('itemTbody');
        var checkData = data[0];
        var whatVar = typeof (checkData);

        if (whatVar.toUpperCase() == "STRING") {
            data.forEach(function (item) {
                var tr = document.createElement('tr');
                var tdItemName = document.createElement('td'); tdItemName.innerHTML = item.split('~')[0];
                var tdItemQty = document.createElement('td'); tdItemQty.innerHTML = item.split('~')[1];
                tr.className = "w3-light-grey"
                tr.appendChild(tdItemName);
                tr.appendChild(tdItemQty);
                myTabBody.appendChild(tr);
            });
        }
        else {
            data.forEach(function (item) {
                var tr = document.createElement('tr');
                var tdItemName = document.createElement('td'); tdItemName.innerHTML = item.name;
                var tdItemQty = document.createElement('td'); tdItemQty.innerHTML = item.quantity;
                tr.className = "w3-light-grey"
                tr.appendChild(tdItemName);
                tr.appendChild(tdItemQty);
                myTabBody.appendChild(tr);
            });
        }
        myTable.appendChild(myTabBody);
    }
    this.DownloadReceipt = function () {
        //var receiptUrl = "rayaduboat-001-site1.itempurl.com/TripDetails/tripReportView";
        var Url = window.location.href;
        var obj, url, handler;
        var p = new $pac.Utilities.Initializers();
        p.obj = { 'data': '"' + Url + '"' };//  { myUrl: Url };
        p.url = "/Clients/HtmlToPdf";
        p.handler = function (jsondata) {
            if (jsondata == "failed") new $pac.Exception(jsondata);
            alert(jsondata);
        }
        p.invoke();
    }
}
$pac.TripManagement.CustomerDetails = function () {
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
$pac.TripManagement.ClientOrder = function () {
    var ref;
    var orderItemsList = [];
    var sender;
    var recipient;
    var senderDST;
    var recipientDST;
    var senderRecipient;
    var batch;
    this.Checkout = function (bId, sId, rId, rfId, ordTab,orderTel,orderPost) {
        //sId = loadedSenderDetail.senderId;
        var senderUrl = window.location.href;
        var senderLinkString = senderUrl.split('?')[1];

        var flyTable = document.getElementById("itemTable");
        if (flyTable.rows.length > 1) {
            var answer = window.confirm("Do you want to add this order to trip?");
            if (answer) {
                var obj, url, handler;
                var formattedDate = new $pac.Utilities.DateToday().full_Date();
                var v = new $pac.Utilities.Initializers();

                var orderItemsList = [];
                $('#' + ordTab).each(function () {
                    var orderParam = {
                        name: $(this).find("td").html(),
                        quantity: $(this).find("td").eq(1).html()
                    };
                    orderItemsList.push(orderParam);
                });
                var vBatchID = $('#' + bId).val();
                var vorderTel = $('#' + orderTel).val();
                var vorderPost = $('#' + orderPost).val();
                //var vSenderID = $('#' + sId).val();
                var vSenderID = loadedSenderDetail.senderId;
                var vRecipientId = $('#' + rId).val();
                var vRefNumber = $('#' + rfId).val();

                var mylist = JSON.stringify(orderItemsList);
                var obj = {
                    batchId: vBatchID,
                    senderId: vSenderID,
                    recipientId: vRecipientId,
                    actualRef: vRefNumber,
                    orderDate: formattedDate,
                    orderList: mylist,
                    urlLink: senderLinkString,
                    orderTelephone: vorderTel,
                    orderPostcode: vorderPost
                };
                v.obj = obj;
                v.url = "/Clients/CustomerCheckout";
                v.handler = function (jsondata) {
                    if (jsondata == "Failed loading items") new $pac.Exception(jsondata);
                    var parsed = JSON.parse(jsondata);
                    alert(parsed.result);
                    if (jsondata = "Order Added Successfully!!") {
                        $("#tripRecipientId option").remove();
                        $('#btnClearTable').click();
                        //collecting list of items from a table
                        //======================================
                        var orderItemsList = [];
                        $('#itemTable>tbody>tr').each(function () {
                            var orderParam = $(this).find("td").html() + "~" + $(this).find("td").eq(1).html();// + "~" + $(this).find("td").eq(2).html();
                            orderItemsList.push(orderParam);
                        });

                        //keeping a local storage called clientReceiptStore
                        //==================================================
                        localStorage.setItem("clientReceiptStore", JSON.stringify({
                            ref: vRefNumber, list: orderItemsList, myText: "Thanks for your order", oStatus: "Created"
                        }));
                        window.location.assign("../TripDetails/tripReportView");
                    }
                }
                v.invoke();
            }
            else {
                return false;
            }
        } else {
            var err = document.getElementById('errMsg');
            err.innerHTML = "Please Add orders to bacsket before checking out";
            err.style.color = "red";
            $('#Quantity').val("");
            $('#Name').focus();
        }
    }
    this.Invoice = function (bId, grdTot, rfId) {
        var obj, url, handler;
        var senderLinkString;
        var senderUrl = window.location.href;
        if (senderUrl.indexOf("?" > -1)) {
            senderLinkString = senderUrl.split('?')[1];
        }
        if (senderUrl.indexOf("#" > -1)) {
            senderLinkString = senderUrl.split('#')[1];
        }
        var grandTot = $('#' + grdTot).val();
        if (grandTot == "") {
            alert("Please enter an amount");
            $('#GrandTotal').focus();
            return false;
        }
        else {
            var v = new $pac.Utilities.Initializers();

            v.obj = {
                refNumber: $('#' + rfId).val(),
                grandTotal: $('#' + grdTot).val(),
                batchId: $('#' + bId).val(),
                hashString: senderLinkString
            };
            v.url = "/TripDetails/ChargeCustomerOders";
            v.handler = function (jsondata) {
                if (jsondata == "Failed loading items") new $pac.Exception(jsondata);
                var parsed = JSON.parse(jsondata);
                alert(parsed.result);
                $('#btnCloseChargeModal').click();
                $('#SearchItem').val("");
                location.reload();
                $('#SearchItem').focus();
            };
            v.invoke();
        }
    }
    this.OrderStatus = function (refnum) {
        var v = new $pac.Utilities.Initializers();
        v.obj = { RefNum: refnum };
        v.url = "/Clients/CustomersExistingOrders";
        v.handler = function (jsondata) { }
    }
    this.CheckAllClientOrders = function (clientId) {

    }
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
    this.LoadModalItemForCharge = function (refNum) {
        $('#ActualRefIt').val(refNum);
        var obj, url, handler;
        obj = { id: refNum }
        var v = new $pac.Utilities.Initializers();
        v.obj = obj;
        v.url = "/TripDetails/GetOrderParamByRef";
        v.handler = function (jsondata) {
            var data = JSON.parse(jsondata);
            if (data.results.length > 0) {
                //alert(data.msg);
                var ItemList = [];
                ItemList = data.results;
                var counter = ItemList.length;
                sdID = data.results[0].senderID;
                sdNam = data.results[0].senderName;
                sdTel = data.results[0].senderTelephone;
                rcID = data.results[0].recipientID;;
                rcNam = data.results[0].recipientName;
                rcTel = data.results[0].recipientTelephone;
                refID = data.results[0].refNum;

                $('#txtSenderName').text(sdNam);
                $('#txtSenderTelephone').text(sdTel);
                $('#txtSenderTown').text(data.results[0].senderTown);
                $('#txtRecipientName').text(rcNam);
                $('#txtRecipientTelephone').text(rcTel);
                $('#txtRecipientTown').text(data.results[0].recipientTown);
                $('#BatchNumber').val(data.results[0].batchID);

                var sendername = document.getElementById('txtSenderName')
                var sendertelephone = document.getElementById('txtSenderTelephone')
                var recipientname = document.getElementById('txtRecipientName')
                var recipienttelephone = document.getElementById('txtRecipientTelephone')
                var actualref = document.getElementById('ActualRefNum')
                $('#ActualRefNum').val(refID);
                $('#GrandTotal').val(data.results[0].totalCost);
                $('#GrandTotal').focus();

                var mainTable = document.getElementById("TableMainInvoice");
                var currentTableBody = document.getElementById('tableBodyInvoice');
                $(currentTableBody).empty();
                for (var i = 0; i < counter; i++) {

                    try {
                        var tr = document.createElement('tr');
                        var tdTripId = document.createElement('td'); tdTripId.innerHTML = data.results[i].tripId;
                        var tdItemName = document.createElement('td'); tdItemName.innerHTML = data.results[i].itemName;
                        var tdItemQty = document.createElement('td'); tdItemQty.innerHTML = data.results[i].qty;
                        tr.appendChild(tdTripId)
                        tr.appendChild(tdItemName)
                        tr.appendChild(tdItemQty)
                        currentTableBody.appendChild(tr)

                    } catch (e) {
                        alert(e.message);
                    }
                }
                mainTable.appendChild(currentTableBody);
                //$('#callChargeModal').click();
                //$('#GrandTotal').focus();

            }
            else {
                alert("Reference Number does not Exist");
                return false;
            }
            $('#callChargeModal').click();
            $('#GrandTotal').focus();
        }
        v.invoke();
    }
    this.UpdatAllBatchesVisible = function (checkboxItem, divItem) {
        if (document.getElementById(checkboxItem).checked) {
            $('#' + divItem).css("display", "inline-block");
            //document.getElementById(divItem).style.display("inline-block");
        }
        else {
            $('#' + divItem).css("display", "none");
        }
    }
    this.UpdateAllTrips = function () {

    }
    this.PendingOrders = function () {
        $.ajax({
            type: 'GET',
            url: '@Url.Action("currentTrip", "TripDetails")',
            data: "",
            dataType: 'JSON',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                var counter = result.length;
                if (result.length > 0) {
                    $('#sectionPendingOrders').addClass("doShow");
                    var mainTable = document.getElementById("itemsTabCurrent");
                    var currentTableBody = document.getElementById('tabBodyCurrent');
                    for (var i = 0; i < counter; i++) {
                        try {
                            var tr = document.createElement('tr');
                            var tdActualRef = document.createElement('td'); tdActualRef.innerHTML = result[i].actualRef;
                            var tdTheSender = document.createElement('td'); tdTheSender.innerHTML = result[i].theSender;
                            var tdsendAddress = document.createElement('td'); tdsendAddress.innerHTML = result[i].sendAddress;
                            var tdSenTel = document.createElement('td'); tdSenTel.innerHTML = result[i].sendTel;
                            var btLoad = document.createElement('button'); btLoad.innerHTML = "Load"; btLoad.record = result[i];
                            var btDel = document.createElement('button'); btDel.innerHTML = "Delete"; btDel.record = result[i];
                            var btCharge = document.createElement('button'); btCharge.innerHTML = "ChargeOrder"; btCharge.record = result[i];
                            btLoad.onclick = function () {
                                var bID = this.record.batchID;
                                var myRef = this.record.actualRef;
                                new $pac.TripManagement.ClientLink().loadCustOrder(bID, myRef);
                                $('#callOrderItemsModal').click();
                            }
                            btDel.onclick = function () {
                                var bID = this.record.batchID;
                                var myRef = this.record.actualRef;
                                new $pac.TripManagement.DeleteClientOrder().WholeOrder(bID, myRef);
                            }
                            btCharge.onclick = function () {
                                var bID = this.record.batchID;
                                var myRef = this.record.actualRef;
                                new $pac.TripManagement.ClientOrder().LoadModalItemForCharge(myRef);
                                $('#callChargeModal').click();
                                $('#GrandTotal').focus();
                            }
                            tr.appendChild(tdActualRef)
                            tr.appendChild(tdTheSender)
                            tr.appendChild(tdSenTel)
                            tr.appendChild(btLoad)
                            tr.appendChild(btCharge)
                            currentTableBody.appendChild(tr)
                        } catch (e) {
                            alert(e.message);
                        }
                    }
                    mainTable.appendChild(currentTableBody);
                    document.getElementById('errorMsg').innerHTML = "";
                }
                else {

                    document.getElementById('errorMsg').innerHTML = "There is no current order";
                }
            }
        });
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
$pac.TripManagement.Orders = function () {
    this.Checkout = function (bId, sId, rId, rfId, ordTab) {
        var obj, url, handler;
        var formattedDate = new $pac.Utilities.DateToday().fullDate;
        var orderItemsList = [];
        $('#' + ordTab).each(function () {
            var orderParam = $(this).find("td").html() + "~" + $(this).find("td").eq(1).html();// + "~" + $(this).find("td").eq(2).html();
            orderItemsList.push(orderParam);
        });


        var vBatchID = $('#' + bId).val();
        var vSenderID = $('#' + sId).val();
        var vRecipientId = $('#' + rId).val();
        var vRefNumber = $('#' + rfId).val();

        var mylist = JSON.stringify(itemList);
        var obj = {
            batchId: vBatchID,
            senderId: vSenderID,
            recipientId: vRecipientId,
            actualRef: vRefNumber,
            orderDate: formattedDate,
            orderList: mylist,
        };





        //obj = JSON.stringify({'obj': param });
        url = "/Clients/LoadTrip2";
        this.SendCustomerOrders(param, url);
    }
    this.SendCustomerOrders = function (param, url) {
        var v = new $pac.Utilities.Initializers();
        v.obj = { obj: param };// obj;
        v.url = url;
        v.handler = function (jsondata) {
            if (jsondata == "Failed loading items") new $pac.Exception(jsondata);
            var parsed = JSON.parse(jsondata);
            alert(parsed.result);
            if (jsondata = "Order Added Successfully!!") {
                $("#tripRecipientId option").remove();
                $('#btnClearTable').click();
            };
        }
        v.invoke();
    }
    this.OrderStatus = function (refnum) {
        var v = new $pac.Utilities.Initializers();
        v.obj = { RefNum: refnum };
        v.url = "/Clients/CustomersExistingOrders";
        v.handler = function (jsondata) { }
    }
}
$pac.TripManagement.Reporting = function () {
    this.Expenditure = function (callExpButton) {
        $('#' + callExpButton).click();
    }
}
function findSender(tel, postcode) {
    if (tel != "" || postcode != "") {
        var loginUser = document.getElementById('userEmail');
        var pr = new $pac.Utilities.Initializers();
        pr.url = "/TripDetails/getSender";
        pr.obj = { telephone: tel, postcode: postcode };
        pr.handler = function (jsondata) {
            if (jsondata == "No Record Found!") {
                //$('#catchError').show();
                var err = document.getElementById('catchError');
                document.getElementById("catchError").style.display = "block";
                err.innerHTML = "";
                err.innerHTML = "No Record Found!";
                err.style.color = "red";
                //$('#catchError').css('color', 'red');
            }
            else {
                
                var data = JSON.parse(jsondata);
                var id = data.results.split('~')[0];
                //var id = jsondata.split('~')[0];
                //var fullname = jsondata.split('~')[1];

                //keeping a local storage called senderRecordStore
                //==================================================
                localStorage.setItem("loginSender", JSON.stringify({
                    senderData: data.senderrecord
                }));
                $('#tSenderId').val(id);
                var fullname = data.results.split('~')[1];
                loadedSenderDetail = data.senderrecord;
                $pac.TripManagement.SenderRecord = data.senderrecord;
                var senderName = document.getElementById('txtSenderFullname');
                senderName.value = fullname;
                staticSenderID = id;//===========================================
                $('#mySenderId').val(id);
                //$('#catchError').show();
                //////var err = document.getElementById('catchError');
                //////document.getElementById("catchError").style.display = "block";
                //////err.innerHTML = "successful!";
                //$('#catchError').css('color', 'green');
                //////err.style.color = "green";
                $('#txtSenderFullname').val(fullname);//=========================
                //////$('#catchError').show();
                //var err = document.getElementById('catchError');
                //err.innerHTML = "successful!";
                //$('#catchError').css('color', 'green');
                GetSenderRecipient(id);
                
            }
        }
        pr.invoke();
    }
    else {
        alert("Not Authorised! Please contact PACO");
    }
}
function GetSenderRecipient(id) {
    $('#senderId').val(id)
    var senderSelected = id;
    staticSenderID = id;
    var s = new $pac.Utilities.Initializers();
    s.url = "/TripDetails/LoadRecipientsToGrid";
    s.obj = { Id: id };
    s.handler = function (result) {
        $('#SenderRecipientSection').css("display", "block");
        var myData = JSON.parse(result);
        var myRecipientReceived = [];
        myRecipientReceived = myData.results;
        $("#ListBoxRecipientID option").remove();
        for (var i = 0; i < myRecipientReceived.length; i++) {
            $('#ListBoxRecipientID').append('<option value="' + myRecipientReceived[i].value + '">' + myRecipientReceived[i].text + '</option>');
        }
        var errorMsg = document.getElementById('catchError');
        errorMsg.innerHTML = "";
        document.getElementById("catchError").style.display = "block";
        errorMsg.innerHTML = "Successful!";
        errorMsg.style.color = "green";
    }
    s.invoke();

}
function copyToClipboard(elem) {
    //elem.tagName === "input" || elem.tagName === "textarea";
    // create hidden text element, if it doesn't already exist
    var targetId = "_hiddenCopyText_";
    var isInput = elem.tagName === "INPUT" || elem.tagName === "TEXTAREA";
    var origSelectionStart, origSelectionEnd;
    if (isInput) {
        // can just use the original source element for the selection and copy
        target = elem;
        origSelectionStart = elem.selectionStart;
        origSelectionEnd = elem.selectionEnd;
    } else {
        // must use a temporary form element for the selection and copy
        target = document.getElementById(targetId);
        if (!target) {
            var target = document.createElement("textarea");
            target.style.position = "absolute";
            target.style.left = "-9999px";
            target.style.top = "0";
            target.id = targetId;
            document.body.appendChild(target);
        }
        target.textContent = elem.textContent;
    }
    // select the content
    var currentFocus = document.activeElement;
    target.focus();
    target.setSelectionRange(0, target.value.length);

    // copy the selection
    var succeed;
    try {
        succeed = document.execCommand("copy");
    } catch (e) {
        succeed = false;
    }
    // restore original focus
    //////if (currentFocus && typeof currentFocus.focus === "function") {
    //////    currentFocus.focus();
    //////}

    if (isInput) {
        // restore prior selection
        elem.setSelectionRange(origSelectionStart, origSelectionEnd);
    } else {
        // clear temporary content
        target.textContent = "";
    }
    return succeed;
}
function LoadSenderRecord(tel, postcode) {
    var v = new $pac.Utilities.Initializers();
    v.obj = { Telephone: tel, Postcode: postcode };
    v.url = "/TripDetails/customerDetails";
    v.handler = function (jsondata) {
        if (jsondata !== "No Record Found!") {
            var data = JSON.parse(jsondata);
            //var id = jsondata.split('~')[0];
            var id = data.results.split('~')[0];
            //var fullname = jsondata.split('~')[1];
            var fullname = data.results.split('~')[1];//.split('~')[1];

            //keeping a local storage called senderRecordStore
            //==================================================
            //////localStorage.setItem("senderRecordStore", JSON.stringify({
            //////    senderData: data.senderrecord
            //////}));
            //window.location.assign("../TripDetails/tripReportView");



            loadedSenderDetail = data.senderrecord;
            var senderName = document.getElementById('txtSenderFullname');
            senderName.value = fullname;
            $('#tSenderId').val(id);
            staticSenderID = id;//===========================================
            $('#ModalSenderId').val(id);
            $('#catchError').show();
            var err = document.getElementById('catchError');
            err.innerHTML = "successful!";
            $('#catchError').css('color', 'green');
            $('#sender_Id').val(id);
            GetSenderRecipient(id);
        }
        else {
            $('#catchError').show();
            var err = document.getElementById('catchError');
            err.innerHTML = result;
            $('#txtPostcode').val("");
            $('#txtTel').val("");
            $('#txtPostcode').focus();
            $('#catchError').css('color', 'red');
        }
    };
    v.invoke();
}
function LoadExistingOrders(ReferenceNum) {
    var v = new $pac.Utilities.Initializers();
    v.obj = { RefNum: ReferenceNum };
    v.url = "/Clients/CustomersExistingOrders";
    v.handler = function (jsondata) {
        if (jsondata != "Failed Getting Orders") {
            var existOrdersList = [];
            var parsed = JSON.parse(jsondata);
            existOrdersList = parsed.result;
            //keeping a local storage called clientReceiptStore
            //==================================================
            //localStorage.setItem("clientReceiptStore", JSON.stringify({
            //    ref: existOrdersList[0].refNum, orderlist: existOrdersList
            //}));
            ////// var orderStatus = new $pac.TripManagement.Orders().OrderStatus(existOrdersList[0].refNum);
            localStorage.setItem("clientReceiptStore", JSON.stringify({
                ref: existOrdersList[0].refNum, list: existOrdersList, myText: "Your existing order below:", oStatus: parsed.oStatus, oCost: parsed.cost
            }));

            //window.location.assign("/Clients/ExistingOrders");
            window.location.assign("../TripDetails/tripReportView");
        }
    };
    v.invoke();
}
var getSenderRecordToUpdate = function (data) {
    //collect other records for sender for modal form
    //====================================================
    //$('#textSenderFullNameUpd').val(data.senderData.senderId) 
    //$('#textSenderFullNameUpd').val(data.senderData.shippersId);
    //$('#ModalSenderId').val(data.senderData.senderId)
    //$('#ModalSenderId').val(data.senderData.senderId)
    //$('#selectSenderTitleUpd').val(data.senderData.title);
    //$('#selectSenderGenderUpd').val(data.senderData.gender);
    //$('#textSenderFirstNameUpd').val(data.senderData.firstName);
    //$('#textSenderLastNameUpd').val(data.senderData.lastName);
    //$('#textSenderAdd1Upd').val(data.senderData.addressLineOne);
    //$('#textSenderAdd2Upd').val(data.senderData.addressLineTwo);
    //$('#textSenderPostcodeUpd').val(data.senderData.postCode);
    //$('#textSenderTownUpd').val(data.senderData.postTown);
    //$('#selectSenderCountryUpd').val(data.senderData.country);
    //$('#textSenderTelephoneUpd').val(data.senderData.telephone);
    //$('#textSenderEmailUpd').val(data.senderData.emailAddress);

    $('#textSenderFullNameUpd').val(data.shippersId);
    $('#ModalSenderId').val(data.senderId)
    $('#ModalSenderId').val(data.senderId)
    $('#selectSenderTitleUpd').val(data.title);
    $('#selectSenderGenderUpd').val(data.gender);
    $('#textSenderFirstNameUpd').val(data.firstName);
    $('#textSenderLastNameUpd').val(data.lastName);
    $('#textSenderAdd1Upd').val(data.addressLineOne);
    $('#textSenderAdd2Upd').val(data.addressLineTwo);
    $('#textSenderPostcodeUpd').val(data.postCode);
    $('#textSenderTownUpd').val(data.postTown);
    $('#selectSenderCountryUpd').val(data.country);
    $('#textSenderTelephoneUpd').val(data.telephone);
    $('#textSenderEmailUpd').val(data.emailAddress);

    //====================================================
}
function UpdateCustomerLink(data) {
    var obj, url, handler;
    var v = new $pac.Utilities.Initializers();
    v.obj = { uniqueString: data };
    v.url = ""
}
//$(function () {
//    /*Store loginUser's details for navigation
//     * ==============================================
//   */

//    try {
//        alert(ViewBag.loginUser.firstName);
//        ////////localStorage.setItem("loginUser", JSON.stringify({
//        ////////    email: vRefNumber, list: orderItemsList, myText: "Thanks for your order", oStatus: "Created"
//        ////////}));
//        //localStorage.setItem("loginUser",email:"",)
//        //if (localStorage.getItem("user") === null) { location.href = $rab.HomePage; return; }
//        //$rab.Login.User = JSON.parse(localStorage.getItem("user"));
//        // check if credentials is registered
//        //$("#aLogout").click(function () { new $rab.Initialise.Menus().Logout()() });
//        //$rab.Configuration();
//        //alert(localStorage.getItem("user"));


//        //$rab.Initialise.Menus();
//        //new $rab.Security.LockDown().AccountMenu();
//        //new $rab.Security.LockDown().DisplayLoggedInUser();
//        //new $rab.Security.LockDown().FinanceMenu();
//        //new $rab.Security.LockDown().HideMenuDropdowns();
//        //new $rab.Security.LockDown().AddTheme();
//    } catch (e) {
//        alert(e.message);
//    }
//});

