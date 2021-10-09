var $pac = $pac || {};
$pac.Batch = $pac.Batch || {};
$pac.Batch.CreateNewBatch = function (currentBatch) {
    var previousBatch = $('#' + currentBatch).val();
    CreateNewBatch(previousBatch);
    function CreateNewBatch(data) {
        var stringPart;
        var numbergPart;
        var newNumberPart;
        var newBatchNumber;

        var yrString = data.substring(2, 6);
        //alert(yrString);
        var thisYear = new $pac.Utilities.DateToday().YearOfDate();
        alert(thisYear)
        if (thisYear == yrString) {
            stringPart = data.substring(0, 6);
            numbergPart = data.substring(7, 10);
            alert(stringPart);
            alert(numbergPart);
            newNumberPart = parseInt(numbergPart, 10) + 1;
            alert(newNumberPart);
            if (newNumberPart.toString().length == 1) { newBatchNumber = stringPart + "-00" + newNumberPart; }
            else if (newNumberPart.toString().length == 2) { newBatchNumber = stringPart + "-0" + newNumberPart;}
            else if (newNumberPart.toString().length == 3) { newBatchNumber = stringPart + "-" + newNumberPart; }
            $('#NewBatch').val(newBatchNumber);
        }
        else {
            newBatchNumber = "PA" + thisYear + "-" + "001";
            $('#NewBatch').val(newBatchNumber);
        }
                
    }
}
$pac.Batch.SaveNewBatch = function (newbatch) {
    var newbatchVal = $('#' + newbatch).val();
    var v = new $pac.Server.Initializers();
    v.obj = { newBatch: newbatchVal };
    v.url = "/TripDetails/AddBatch";
    v.handler = function (jsondata) {
        alert()
    }
    v.invoke();
}