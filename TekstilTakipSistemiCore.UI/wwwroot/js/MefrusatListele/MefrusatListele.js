$(document).ready(function () {

    MefrusatListeGetir();
});

function MefrusatListeGetir() {
    $.ajax({
        type: "POST",
        url: "/Kimliklendirme/KimliklendirmeMefrusatListele/",
        data: JSON.stringify
            ({

            }),

        contentType: "application/json; charset=utf-8",

        dataType: "json",

        beforeSend: function () {

        },
        error: function (request, status, error) {


        },
        success: function (msg) {
            var _dizi = msg;
            //alert(_dizi.MefrusatTuru);
            var content = '';
            content += "<option value='-1'>Seçiniz...</option>";
            for (i = 0; i < _dizi.length; i++) {
                content += "<option value='" + _dizi[i].mefrusatId + "'>" + _dizi[i].mefrusatTuru + "</option>";
                //alert(_dizi[i].FirstName);
            }
            $("#mefrusatlist").html(content);

            //$('#event_entry_modal2').modal({
            //    show: true,

            //});
        },
        complete: function () {

        }
    });
}