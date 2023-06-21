
$(document).ready(function () {
    yikamaDurumu();
    teslimDurumu();
});

function listele() {
    EtiketNoyaGoreMefrusatGetir();
}

function EtiketNoyaGoreMefrusatGetir() {

    var _etiketno = $("#etiketno").val();
    if (_etiketno.length != 24) {
        toastr.warning("Lütfen 24 karakterli etiket numarasını giriniz.");
    }
    else {
        $.ajax({
            type: "POST",
            url: "/TemizlikSurec/MefrusatTurBilgiGetir/",
            dataType: "json",
            data: { etiketno: _etiketno },
            success: function (res) {
                /*alert(res.mefrusatTur)*/
                $("#mefrusattur").val(res.mefrusatTur);
            },
            error: function (request, status, error) {
                alert('Sistemsel bir hata oluştu');
            },
            complete: function () {


            }
        });
    }
}

function kaydet() {
    var _etiketno = $("#etiketno").val();
    var _yikamadurumu = $("#yikamadurumu").val();
    var _teslimdurumu = $("#teslimdurumu").val();
    var _tartimtarihi = $("#tartimtarihi").val();
    var _amlajlandimi = $("#amlajlandimi").is(":checked");
    var _bohcalandimi = $("#bohcalandimi").is(":checked");

   
        $.ajax({
            type: "POST",
            url: "/TemizlikSurec/TemizlikBilgileriKaydet/",
            dataType: "json",
            data:
            {
                EtiketNo: _etiketno,
                YikamaDurumu: _yikamadurumu,
                TeslimDurumu: _teslimdurumu,
                TartimTarihi: _tartimtarihi,
                AmbalanlandiMi: _amlajlandimi,
                BohcalandiMi: _bohcalandimi
            },
            success: function (res) {
                if (res.sonuc == 1) {
                    window.location.href = 'TemizSureci';
                }
                else {
                    toastr.warning("Etiket no bilgisi olmadan kayıt yapılamaz.");
                }
            },
            error: function (request, status, error) {
                alert('Sistemsel bir hata oluştu');
            },
            complete: function () {

                window.location.href = 'TemizSureci';
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

function teslimDurumu() {
    var getvalues = [
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
    for (var i = 0; i < getvalues.length; i++) {
        $('#teslimdurumu').append($('<option>',
            {
                value: getvalues[i].value,
                text: getvalues[i].text

            }

        ));
    }

}

