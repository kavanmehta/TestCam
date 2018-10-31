
$(function () {
    jQuery("#webcam").webcam({
        width: 320,
        height: 240,
        mode: "save",
        swffile: '/Scripts/jscam.swf',
        onSave: function (data, ab) {

            $.ajax({
                type: "POST",
                url: '/Visitor/GetCapture',
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $("#imgCapture").css("visibility", "visible");
                    $("#imgCapture").attr("src", r);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        },
        onCapture: function () {
            webcam.save('/Visitor/Capture');
        }
    });
});
function Capture() {
    webcam.capture();
}

$(document).ready(function () {
    $('input[type=datetime]').datepicker({
        dateFormat: "dd/M/yy",
        changeMonth: true,
        changeYear: true,
        yearRange: "-60:+0",

    });

});

function Print(formContainer) {
    $.ajax({
        type: "POST",
        url: '/Visitor/VisitorIndex',
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {

        },
        failure: function (response) {
            alert(response.d);
        }
    });
    var wnd = window.open("");
    wnd.document.write($("#printPage").html());
    wnd.print();
}

function Cancel()
{
    window.location = '';
}

function submitAndPrint() {


    var frm = $("#visitorForm"),  // our form
           url = frm.attr("action"),  // our post action
           data = frm.serialize();     // our data to be posted

    $.post(url, data, function (data) {

        $("#printName").text(data.Name);
        $("#printContact").text(data.MobileNo);
        $("#printGateNumber").text(data.GateNumber);
        $("#printPurpose").text(data.Purpose);
        $("#printToMeet").text(data.ToMeet);
        $("#printValidUpTo").text(data.ValidUpTo);
        $("#printDate").text(data.Date);
        $("#printImage").attr("src", data.ImagePath);
     

        //var wnd = window.open("");

      //  window.document.write($("#printPage").html());
      //  window.print($("#printPage"));
        //var divName = "printPage";
        //var printContents = document.getElementById(divName).innerHTML;
        //var originalContents = document.body.innerHTML;

        //document.body.innerHTML = printContents;

        //window.print();

        //document.body.innerHTML = originalContents;
        var panel = document.getElementById("printPage");
        var printWindow = window.open('', '', '');
       
        printWindow.document.writeln('<!DOCTYPE html>');
        printWindow.document.writeln('<html><head><title></title>');
       
        printWindow.document.write('<link rel="stylesheet" href="~/Content/assets/demo/default/base/style.bundle.css"">');
        printWindow.document.write('<link rel="stylesheet" href="~/Content/assets/vendors/base/vendors.bundle.css"">');
        printWindow.document.write('<link rel="stylesheet" href="~/Content/assets/demo/default/base/style.bundle.css"">');
        printWindow.document.write('<link rel="stylesheet" href="~/Content/assets/vendors/base/vendors.bundle.css"">');
        printWindow.document.write('<link rel="stylesheet" href="~/Content/assets/vendors/base/vendors.bundle.css"">');
        printWindow.document.writeln('</head><body>');
        printWindow.document.writeln(panel.innerHTML);
        printWindow.document.writeln('</body></html>');
  


    });

    return false;


}