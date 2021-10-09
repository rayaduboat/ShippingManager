$('#buttonSubmitNewPerson').on("click", function () {
    if ($('#selectNewMemberGender').val() == "" ||
       $('#textboxNewMemberFirstName').val() == "" ||
       $('#textboxNewMemberLastName').val() == "" ||
       $('#textboxNewMemberAddressLineone').val() == "" ||
       $('#textboxNewMemberPostcode').val() == "" ||
       $('#textboxNewMemberTown').val() == "" ||
       $('#selectNewMemberCountry').val() == "" ||
       $('#textboxNewMemberTelephone').val() == "" ||
       $('#textboxNewMemberEmail').val() == "") {

        alert("Please fill in the right details");
        //$('#textboxNewMemberAddressLinetwo').val()==""||
    } else {
        var myData = new collectModalSendData();
        var url, obj;
        url = "/TripDetails/addModalNewSender";
        obj = JSON.stringify(myData);
        addModalSender(url,obj);
    }

});

function dataCenter() {
   myData= new collectModalSendData();
}

function addModalSender(url,obj) {
    $.ajax({
        type: "POST",
        url: url,
        data: obj,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            // Do something interesting here.
            alert(msg);
            var pcode = $('#textboxNewMemberPostcode').val();
            $('#findPostcode').val(pcode);
            var tel = $('#textboxNewMemberTelephone').val();
            $('#findTel').val(tel);
            $('#findSearch').click();
            //clearFields();
            $('#buttonCloseNewPerson').click();
        },
        error: function (e) {
            //alert(e.responseText);
            alert(msg);
        }
    });
}

function clearFields() {
    $('#selectNewMemberGender').val("");
    $('#textboxNewMemberFirstName').val(""); 
    $('#textboxNewMemberLastName').val("");
    $('#textboxNewMemberAddressLineone').val("");
    $('#textboxNewMemberPostcode').val("");
    $('#textboxNewMemberTown').val("");
    $('#selectNewMemberCountry').val("");
    $('#textboxNewMemberTelephone').val(""); 
    $('#textboxNewMemberEmail').val("");
}
//function addModalSender() {
//    try {
//        var myData = new collectModalSendData();
//        var myJson = new modalSenderJson();
//        myJson.data = JSON.stringify(myData);
//        //myJson.data = myData;
//        $.ajax(myJson);
//    } catch (e) {

//    }
//}
//function modalSenderJson() {
//    this.type = 'POST'
//    this.url = '/TripDetails/addModalNewSender'//'@Url.Action("addModalNewSender", "TripDetails")'
//    this.data = "{'data':'123'}"
//    this.contentType = 'application/json; charset=utf-8'
//    this.success = function (result) {
//        //alert("Data here");
//    }
//}


function collectModalSendData() {
    this.selectNewMemberTitle = $('#selectNewMemberTitle').val();
    this.selectNewMemberGender = $('#selectNewMemberGender').val();
    this.textboxNewMemberFirstName = $('#textboxNewMemberFirstName').val();
    this.textboxNewMemberLastName = $('#textboxNewMemberLastName').val();
    this.textboxNewMemberAddressLineone = $('#textboxNewMemberAddressLineone').val();
    this.textboxNewMemberAddressLinetwo = $('#textboxNewMemberAddressLinetwo').val();
    this.textboxNewMemberPostcode = $('#textboxNewMemberPostcode').val();
    this.textboxNewMemberTown = $('#textboxNewMemberTown').val();
    this.selectNewMemberCountry = $('#selectNewMemberCountry').val();
    this.textboxNewMemberTelephone = $('#textboxNewMemberTelephone').val();
    this.textboxNewMemberEmail = $('#textboxNewMemberEmail').val();
}