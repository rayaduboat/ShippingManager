$(document).ready(function () {

    $('#BatchID').on("change", function () {
        $('#catchErrorRef').hide();
        $('#BatchID').css('background-color', 'white');
        var btch = document.getElementById('batch');
        btch.innerHTML = $("#BatchID option:selected").prop("selected", true).text();
    });

    $('#TextSearch').keydown(function () {
        var err = document.getElementById('catchErrorRef');
        $('#catchErrorRef').hide();
        $('#TextSearch').css('background-color', 'white');
    });

    $('#refSearch').on("click", function () {

        var err = document.getElementById('catchErrorRef');
        var batch = $("#BatchID option:selected").prop("selected", true).val();
        var refNo = $('#TextSearch').val();
        if (batch == "" || refNo == "") {
            $('#catchErrorRef').show();
            err.innerHTML = "Please enter your BatchID and Reference Number";
            $('#BatchID').val("");
            $('#TextSearch').val("");
            $('#BatchID').css('background-color', 'red');
            $('#TextSearch').css('background-color', 'red');
            $('#BatchID').focus();
            return false;
        }
        else {
            $('#catchErrorRef').hide();
            var dataSend = batch + "~" + refNo;
            $.ajax({
                Type: 'POST',
                url: '@Url.Action("getOrder", "TripDetails")',
                //url: $(this).closest('form').attr('getOrder'),
                data: { 'data': '"' + dataSend + '"' },
                dataType: 'JSON',
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    alert(result);
                }
            });
        }
    });
});

