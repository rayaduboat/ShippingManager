var $rab = $rab || {};
$rab.TripManagement = $rab.TripManagement || {};
$rab.Generics = $rab.Generics || {};
$rab.TripManagement.Expenditures = function () {

}
$rab.TripManagement.Senders = function () {
    this.FindSender = function (tel, postcode) {
        var vtel = $('#' + tel).val().replace(" ", "");
        var vpostcode = $('#' + postcode).val().replace(" ", "");
        //var obj, url;
        if (vpostcode == "" || vtel == "") {
            new $rab.Exception("Postcode and Telephone required!");
        }
        else {
            findSender(vtel, vpostcode);
        }
    }
}
$rab.TripManagement.Recipients = function () {

}
$rab.TripManagement.Orders = functions = function () {
    
}
$rab.TripManagement.Batches = function () {

}
$rab.TripManagement.Incomes = function () {

}
$rab.TripManagement.Reporting = function () {

}
$rab.TripManagement.Receipt = function () {

}
function findSender(tel, postcode) {
    if (tel != "" || postcode != "") {
        var pr = new $rab.Generics.Initializers();
        pr.url = "/TripDetails/getSender";
        pr.obj = { telephone: tel, postcode: postcode };
        pr.handler = function (jsondata) {
            var id = jsondata.split('~')[0];
            var fullname = jsondata.split('~')[1];
            var senderName = document.getElementById('txtSenderFullname');
            senderName.value = fullname;
            staticSenderID = id;//===========================================
            $('#mySenderId').val(id);
            $('#catchError').show();
            var err = document.getElementById('catchError');
            err.innerHTML = "successful!";
            $('#catchError').css('color', 'green');
            $('#txtSenderFullname').val(fullname);//=======================
            $('#catchError').show();
            var err = document.getElementById('catchError');
            err.innerHTML = "successful!";
            $('#catchError').css('color', 'green');
           // GetSenderRecipient(id);
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
    var s = new $rab.Generics.Initializers();
    s.url = "/TripDetails/LoadRecipientsToGrid";
    s.obj = { Id: id };
    s.handler = function (result) {
        var myData = JSON.parse(result);
        var myRecipientReceived = [];
        myRecipientReceived = myData.results;
        $("#ListBoxRecipientID option").remove();
        for (var i = 0; i < myRecipientReceived.length; i++) {
            $('#ListBoxRecipientID').append('<option value="' + myRecipientReceived[i].value + '">' + myRecipientReceived[i].text + '</option>');
        }
    }
    s.invoke();

}

$pac.TripManagement.ClientLink = function () {
    this.SendToClient = function (checkboxItem, fnam, lnam, addr1, town, postcode, tel) {
        var vtel = $('#' + tel).val().replace(" ", "");
        var vpostcode = $('#' + postcode).val().replace(" ", "");
        var vfnam = $('#' + fnam).val();
        var vlnam = $('#' + lnam).val();
        var vaddr1 = $('#' + addr1).val();
        var vtown = $('#' + town).val();
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
                callClientModule(obj, url, vtel, vpostcode);
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
                callClientModule(obj, url, vtel, vpostcode);
            }
        }
        //new $pac.TripManagement.ClientLink().ClearFields(checkboxItem, fnam, lnam, addr1, town, postcode, tel);
    }
    this.LinkCopy = function (element) {
        var copyText = document.getElementById(element);
        //copyText.select();
        document.execCommand("copy");
        //alert("Copied the text: " + copyText.value);
    }
    this.DisableEnableFields = function (checkboxItem, fnam, lnam, addr1, town, postcode, tel) {

        if (document.getElementById(checkboxItem).checked) {
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
            $('#' + mySection).hide();
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
    function callClientModule(obj, url, telephone, postcode) {
        // alert(postcode + " " + telephone);

        var v = new $pac.Utilities.Initializers();
        v.obj = obj;
        v.url = url;
        v.handler = function (jsondata) {
            if (jsondata == "failed adding Sender") new $pac.Exception(jsondata);
            alert("Goodbye");
            var parsed = JSON.parse(jsondata);
            //if (parsed.message == "Added Successfuly" || parsed.message == "Customer already Exist") {
            //var sendUrl = "pacoshipping.rabcomsolutions.com/TripDetails/Create";
            var sendUrl = "rayaduboat-001-site1.itempurl.com/Clients/Create";

            //gET THE UNIQUE STRING IN THE PARAMETER
            //===================================================================================
            var randLetter = String.fromCharCode(65 + Math.floor(Math.random() * 26));
            var uniqid = randLetter + Date.now();
            var myKey = btoa(Math.random()).substring(0, 12)
            var uniqid1 = uniqid.substring(0, 4); //+ "_" + "01e" + MI + "e02" +;
            var uniqid2 = uniqid.substring(5, uniqid.length);
            var uniqid3 = myKey.substring(0, 8);
            var uniqid4 = myKey.substring(9, myKey.length);
            var tel1 = telephone.substring(5, telephone.length);
            var tel2 = telephone.substring(0, 5);
            var finalKey = "?!" + uniqid1 + "_" + uniqid3 + "#01e2#" + tel1 + "_" + tel2 + uniqid2 + "_" + uniqid4 + "e02!" + postcode;
            var qString = finalKey.toLowerCase();



            //var qString = CreateUniqueString(telephone, postcode);// "?Tel=" + telephone + "&Postcode=" + postcode;
            //var clientURL = sendUrl + CreateUniqueString(telephone, postcode);
            //var qString = "?Tel=" + telephone + "&Postcode=" + postcode;//CHECK THIS LATER************************
            var clientURL = sendUrl + qString;
            alert("Customer Link is: " + clientURL);
            $('#customerLink').text(clientURL);
            $('#myUrl').text(clientURL);
            //var myCopy = document.getElementById('myUrl');
            //var myCopy = document.getElementById('myUrl');
            document.execCommand('copy');

            //}
            //
            //alert("Customer not in Database. Please add details!");
            // }
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

                $('#callMyItemModal').click();
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
        //var obj, url;
        if (vpostcode == "" || vtel == "") {
            new $rab.Exception("Postcode and Telephone required!");
        }
        else {
            findSender(vtel, vpostcode);
        }
    }
}