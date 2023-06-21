$(document).ready(function () {

    $('#paketlemelist').on('change', '.tblchk', function () {
        debugger;
        if ($('.tblchk:checked').length == $('.tblchk').length) {
            $('#chkAll').prop('checked', true);
        } else {
            $('#chkAll').prop('checked', false);
        }
        getCheckRecords();
    });


    $("#chkAll").change(function () {
        if ($(this).prop('checked')) {
            $('.tblchk').not(this).prop('checked', true);
        }
        else {
            $('.tblchk').not(this).prop('checked', false);
        }

        getCheckRecords();
    })
});

function getCheckRecords() {
    debugger;
    $(".selectedDiv").html("");
    $('.tblchk:checked').each(function () {
        debugger;
        if ($(this).prop('checked')) {
            if ($(".selectedDiv").children().length == 0) {
                var rec = "" + $(this).attr("data-id") + "  ,";
                $(".selectedDiv").append(rec);
            } else {
                var rec = ", " + $(this).attr("data-id") + " ";
                $(".selectedDiv").append(rec);
            }
        }
    });
}

function listele() {
    EtiketNoyaGoreListele();

    $('#etiketno').val("");
}

function EtiketNoyaGoreListele() {

    var _etiketno = $("#etiketno").val();
    if (_etiketno.length != 24) {
        toastr.warning("Lütfen 24 karakter girişi yapınız");
    }
    else {
        $.ajax({
            type: "POST",
            url: "/PaketAlani/EtiketNoGoreTabloyaEkle/",
            dataType: "json",
            data: { etiketno: _etiketno },
            success: function (res) {
                if (res.zSonuc == 1) {

                    var rowcount = $('#paketlemelist td').length;
                    if (rowcount == 0) {
                        var vYanitDizi = res.zDizi;
                        var content = '';

                        if (vYanitDizi.length != 0) {
                            for (var iSayac = 0; iSayac < vYanitDizi.length; iSayac++) {
                                content += "<tr>";
                                content += "<td style=''>" + vYanitDizi[iSayac].etiketNo + "</td>";
                                content += "<td style=''>" + vYanitDizi[iSayac].mefrusatTuru + "</td>";
                                content += "<td style=''><input type='checkbox' readonly='readonly' class='tblchk chk' data-id=" + vYanitDizi[iSayac].rownum + " /></td>";
                                content += "</tr>";
                            }

                            $('#paketlemelist tbody').html(content);
                        }
                    }
                    else {
                        var vYanitDizi = res.zDizi;
                        var content = '';

                        if (vYanitDizi.length != 0) {
                            for (var iSayac = 0; iSayac < vYanitDizi.length; iSayac++) {
                                content += "<tr>";
                                content += "<td style=''>" + vYanitDizi[iSayac].etiketNo + "</td>";
                                content += "<td style=''>" + vYanitDizi[iSayac].mefrusatTuru + "</td>";
                                content += "<td style=''><input type='checkbox' readonly='readonly' class='tblchk chk' data-id=" + vYanitDizi[iSayac].rownum + " /></td>";
                                content += "</tr>";
                            }

                            $('#paketlemelist tbody').append(content);
                        }

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
    //const test = JSON.stringify(etiketno);

}

function paketolustur() {

    var _odano = $("#odano").val();
    var _dataid = $('.selectedDiv').html();
    $.ajax({
        type: "POST",
        url: "/PaketAlani/PaketAlaniEkle/",
        dataType: "json",
        data: {
            dataid: _dataid,
            odano: _odano
        },
        success: function (res) {

            if (res.sonuc == 1) {

                toastr.success("Paketleme İşlemi Başarılı");
                window.location.href = 'PaketAlaniEkle';


            } if (res.sonuc == 0) {
                toastr.info("Mefruşat türünün kimliklendirme işlemi bulunmamaktadır.");
            }
            if (res.sonuc == 2) {
                toastr.warning("Lütfen oda numarasını boş geçmeyin...");
            }
        },
        error: function (request, status, error) {
            tostr.error("Bilinmeyen bir hata oluştu.");
        },
        complete: function () {
        }
    });
}

