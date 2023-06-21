
$(document).ready(function () {
    yikamaDurumu();
});
function TezlikDetaylistele() {
    MefrusataGoreListele();

    $('#mefrusatlist').val("");
}

function MefrusataGoreListele() {

    var _mefrusatlist = $("#mefrusatlist").val();
   
        $.ajax({
            type: "POST",
            url: "/TemizlikDetayRapor/TemizlikDetayRaporuListele/",
            dataType: "json",
            data: {
                mefrusatlist: _mefrusatlist
            },
            success: function (res) {

                if (res.zSonuc == 1) {

                    
                        var vYanitDizi = res.zDizi;
                        var content = '';
                        var getYikamaDurumu = [
                            {
                                "value": "-1",
                                "text": "Seçiniz..."
                            }, {
                                "value": "1",
                                "text": "Yıkamada"
                            }, {
                                "value": "2",
                                "text": "Kurutmada"
                            }, {
                                "value": "3",
                                "text": "Ütülemede"
                            }, {
                                "value": "4",
                                "text": "Çamaşırhanede"
                            }];
                        var getTeslimDurumu = [
                            {
                                "value": "-1",
                                "text": "Seçiniz..."
                            }, {
                                "value": "1",
                                "text": "Servis"
                            }, {
                                "value": "2",
                                "text": "Kişi"
                            }];

                        if (vYanitDizi.length != 0) {
                            for (var iSayac = 0; iSayac < vYanitDizi.length; iSayac++) {
                                content += "<tr>";
                                content += "<td style=''>" + vYanitDizi[iSayac].etiketNo + "</td>";
                                content += "<td style=''>" + vYanitDizi[iSayac].mefrusatTur + "</td>";
                                content += "<td style=''>" + getYikamaDurumu[vYanitDizi[iSayac].yikamaDurumu].text + "</td>";
                                content += "<td style=''>" + getTeslimDurumu[vYanitDizi[iSayac].teslimDurumu].text  + "</td>";
                                content += "<td style=''>" + vYanitDizi[iSayac].yikanmaSayisi + "</td>";
                                content += "<td style=''>" + vYanitDizi[iSayac].kirlenmeSayisi + "</td>";
                                content += "</tr>";
                            }

                            $('#example1 tbody').html(content);
                        }
                }
                else {
                    toastr.warning("Etiket no sistemde mevcut değil.");
                }
            },
            error: function (request, status, error) {
                toastr.error("Bilinmeyen bir hata oluştu.");
            },
            complete: function () {

            }
        });

}
function yikamaDurumu() {
    var getvalues = [
        {
            "value": "-1",
            "text": "Seçiniz..."
        }, {
            "value": "1",
            "text": "Yıkamada"
        }, {
            "value": "2",
            "text": "Kurutmada"
        }, {
            "value": "3",
            "text": "Ütülemede"
        }, {
            "value": "4",
            "text": "Çamaşırhanede"
        }];
    for (var i = 0; i < getvalues.length; i++) {
        $('#yikamadurumu').append($('<option>',
            {
                value: getvalues[i].value,
                text: getvalues[i].text

            }

        ));
    }

}

