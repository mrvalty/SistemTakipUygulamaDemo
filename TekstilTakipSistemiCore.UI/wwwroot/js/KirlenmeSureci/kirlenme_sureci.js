
$(document).ready(function () {
    hasarDurumu();
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
            url: "/KirlenmeSurec/MefrusatTurBilgiGetir/",
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
    var _hasardurumu = $("#hasardurumu").val();
    var _teslimdurumu = $("#teslimdurumu").val();
    var _tartimtarihi = $("#tartimtarihi").val();
    var _kullanimdami = $("#kullanimdami").is(":checked");
    var _hurdami = $("#hurdami").is(":checked");


    $.ajax({
        type: "POST",
        url: "/KirlenmeSurec/KirlenmeBilgileriKaydet/",
        dataType: "json",
        data:
        {
            EtiketNo: _etiketno,
            HasarDurumu: _hasardurumu,
            TeslimDurumu: _teslimdurumu,
            TartimTarihi: _tartimtarihi,
            KullanimdaMi: _kullanimdami,
            HurdaMi: _hurdami
        },
        success: function (res) {
            if (res.sonuc == 1) {
                window.location.href = 'KirlenmeSureci';
            }
            else {
                toastr.warning("Etiket no bilgisi olmadan kayıt yapılamaz.");
            }
        },
        error: function (request, status, error) {
            alert('Sistemsel bir hata oluştu');
        },
        complete: function () {

            window.location.href = 'KirlenmeSureci';
        }
    });

}

function hasarDurumu() {
    var getvalues = [
        {
            "value": "-1",
            "text": "Seçiniz..."
        }, {
            "value": "1",
            "text": "Yırtık"
        }, {
            "value": "2",
            "text": "Leke"
        }, {
            "value": "3",
            "text": "Çekme"
        }];
    for (var i = 0; i < getvalues.length; i++) {
        $('#hasardurumu').append($('<option>',
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

