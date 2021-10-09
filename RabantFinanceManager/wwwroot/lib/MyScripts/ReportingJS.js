var $pac = $pac || {};
$pac.Reporting = $pac.Reporting || {};
$pac.Reporting.Receipt = function () {
    var myStore=JSON.parse(localStorage.getItem("clientOrderStore"))
    var refnumber = myStore.ref;//itemlist[0].split('~')[1];
    var recipient = myStore.list[0].split('~')[3]; //itemlist[0].split('~')[3];
    myStore.list = myStore.list.slice(1);//itemlist = itemlist.slice(1);
    $('#refNum').text("Reference No: " + refnumber);
    $('#recipentDetails').text("Recipient: " + recipient);
     var myTabBody = document.getElementById('itemTbody');
    myStore.list.forEach(function (item) {
         var tr = document.createElement('tr');
        var tdItemName = document.createElement('td'); tdItemName.innerHTML = item.split('~')[0];
        var tdItemQty = document.createElement('td'); tdItemQty.innerHTML = item.split('~')[1];
        var tdItemDetails = document.createElement('td'); tdItemDetails.innerHTML = item.split('~')[2];
        tr.className = "w3-light-grey"
        tr.appendChild(tdItemName);
        tr.appendChild(tdItemQty);
        tr.appendChild(tdItemDetails);
        myTabBody.appendChild(tr);
    });
    myTable.appendChild(myTabBody);
}