
$(document).ready(function () {

})

function paketdetay(paketno) {

    $.ajax({
        type: "POST",
        url: "/PaketAlani/PaketBilgileriDetay/",
        dataType: "json",
        data: {
            paketNo: paketno
        },
        success: function (res) {
            if (res.sonuc == 1) {
                var paketdetaydizi = res.paketlist;
                var content = '';

                if (paketdetaydizi.length != 0) {
                    for (var i = 0; i < paketdetaydizi.length; i++) {
                        content += "<tr>";
                        content += "<td style=''>" + paketdetaydizi[i].paketNo + "</td>";
                        content += "<td style=''>" + paketdetaydizi[i].etiketNo + "</td>";
                        content += "<td style=''>" + paketdetaydizi[i].mefrusatTuru + "</td>";
                        content += "<td style=''>" + paketdetaydizi[i].odaNo + "</td>";
                        content += "<td style=''>" + paketdetaydizi[i].olusturan + "</td>";
                        content += "<td style=''>" + moment(paketdetaydizi[i].olusturmaZamani).format('YYYY-MM-DD HH:mm:ss')+ "</td>";
                        content += "</tr>";
                    }

                    $('#modal-detay tbody').html(content);
                }

            }
            
        },
        error: function (request, status, error) {
            toastr.error("Bilinmeyen bir hata oluştu.");
        },
        complete: function () {
            $('#exampleModal').modal('show');
        }
    });
}