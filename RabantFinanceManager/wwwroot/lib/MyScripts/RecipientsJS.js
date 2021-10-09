var $pac = $pac || {};
$pac.RecipientData = $pac.RecipientData || {};
$pac.Utilities = $pac.Utilities || {};
$pac.RecipientData.Data = function () {
    this.AddRecipient = function (sendId, title, gender, fname, lname, add1, town, country, telephone) {
        var obj, url;

        if ($('#' + fname).val() != "" && $('#' + lname).val() != "" && $('#' + telephone).val() != "" && $('#' + town).val() !="") {
            obj = {
                sendId: $('#' + sendId).val(),
                title: $('#' + title).val(),
                gender: $('#' + gender).val(),
                fname: $('#' + fname).val(),
                lname: $('#' + lname).val(),
                add1: $('#' + add1).val(),
                town: $('#' + town).val(),
                country: $('#' + country).val(),
                telephone: $('#' + telephone).val()
            };

            url = "/Clients/AddSenderRecipient";
            var v = new $pac.Utilities.Initializers();
            
            v.obj = obj; 
            v.url = url;
            v.handler = function (result) {
                if (result == "failed adding Recipient") new $pac.Exception(result);
                var myData = JSON.parse(result);
                var mySenderRecipients = [];
                mySenderRecipients = myData.allSenderRecipients;
                $("#ListBoxRecipientID option").remove();
                for (var i = 0; i < mySenderRecipients.length; i++) {
                    $('#ListBoxRecipientID').append('<option value="' + mySenderRecipients[i].value + '">' + mySenderRecipients[i].text + '</option>');
                }
                $('#buttonCloseNewRecipient').click();
                //$('#SenderID').val(result);
               // GetSenderRecipient(staticSenderID);
            };
            v.invoke();
        }
        else {
            alert("Please enter Recipients details");
            return false;
        }
    }
}
