var $pac = $pac || {};
$pac.Recipient = $pac.Recipient || {};
$pac.Recipient.AddRecipient = function () {
    //this.NewRecipient = function () {
        if ($('#selectNewMemberRecGender').val() == "" ||
               $('#textboxNewMemberRecFirstName').val() == "" ||
               $('#textboxNewMemberRecLastName').val() == "" ||
               $('#textboxNewMemberRecAddressLineone').val() == "" ||
               $('#textboxNewMemberRecPostcode').val() == "" ||
               $('#textboxNewMemberRecTown').val() == "" ||
               $('#selectNewMemberRecCountry').val() == "" ||
               $('#textboxNewMemberRecTelephone').val() == "" ||
               $('#textboxNewMemberRecEmail').val() == "" ||
               $('#textboxNewMemberRecFullName').val() == "") {
                alert("Please fill in the right details");
        } else {
           // alert("Add Recipient");
            //var myData = new collectModalRecData();
            //var myData = new $pac.Recipient.AddRecipient().collectModalRecData();
           // var p = new $pac.Server.Initializers();
            var url, obj, handler;
            url = "/TripDetails/addModalNewRecipient";
            obj = {
                selectNewMemberRecTitle: $('#selectNewMemberRecTitle').val(),
                selectNewMemberRecGender: $('#selectNewMemberRecGender').val(),
                textboxNewMemberRecFirstName: $('#textboxNewMemberRecFirstName').val(),
                textboxNewMemberRecLastName: $('#textboxNewMemberRecLastName').val(),
                textboxNewMemberRecAddressLineone: $('#textboxNewMemberRecAddressLineone').val(),
                //textboxNewMemberRecAddressLinetwo: $('#textboxNewMemberRecAddressLinetwo').val(),
                //textboxNewMemberRecPostcode: $('#textboxNewMemberRecPostcode').val(),
                textboxNewMemberRecTown: $('#textboxNewMemberRecTown').val(),
                selectNewMemberRecCountry: $('#selectNewMemberRecCountry').val(),
                textboxNewMemberRecTelephone: $('#textboxNewMemberRecTelephone').val(),
                //textboxNewMemberRecEmail: $('#textboxNewMemberRecEmail').val(),
                textboxNewMemberRecFullName: $('#textboxNewMemberRecFullName').val(),
                //senderId: $('#textboxNewMemberRecFullName').val(),
                SenderID: document.getElementById('recModalBtnMob').ID,
            };
            handler = function (jsondata) {
                if (jsondata == "failed Adding Recipient") new $pac.Exception(jsondata);
                var parsed = JSON.parse(jsondata);
                if (parsed.message = "Recipient added Successfully") {
                   // alert(parsed.message);
                    //var customerData = JSON.parse(jsondata);
                    //pull sender and recipients IDs back
                    //=========================================================================
                    new $pac.ShippingItems.Parameters().senderID = parsed.Results.senderID;
                    new $pac.ShippingItems.Parameters().recipientID = parsed.Results.recipientID;
                    //=========================================================================
                    var RecFirstName = $('#textboxNewMemberRecFirstName').val();
                    var RecLastName = $('#textboxNewMemberRecLastName').val();
                    var RecTel = $('#textboxNewMemberRecTelephone').val();
                    var myRecipientReceived = RecFirstName.replace(" ", "").trim() + " " + RecLastName.replace(" ", "").trim() + " " + RecTel.replace(" ", "").trim();
                    $('#ListBoxRecipientID').append('<option>' + myRecipientReceived + '</option>');
                    $('#ListBoxRecipientIDMob').append('<option>' + myRecipientReceived + '</option>');
                    $('#TextRecipient').val(myRecipientReceived);
                    $('#RecipientName').text(myRecipientReceived);
                    $('#RecipientNameMob').text(myRecipientReceived);
                    $('#buttonRecCloseNewPerson').click();
                }
            }
             new $pac.Server.Method().BeginInvoke(url, obj, handler);
            //p.invoke();
            //new $pac.Recipient.AddRecipient().addModalRecipient(url, obj);
           // addModalRecipient(url, obj);
        }
    }
//}

