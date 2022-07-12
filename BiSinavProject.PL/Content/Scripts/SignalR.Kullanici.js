$(function () {

    var ticker = $.connection.satisTicker;

    $(window).unload(function () {
        ticker.server.kullaniciCikis();
    });

    $.extend(ticker.client, {
        kullaniciGiris: function () {

        },
        kullaniciCikis: function () {

        },
        kullaniciKontrol: function (_kullaniciDurum) {

            var buttonTeklifVer = $("#buttonTeklifVer");
            var buttonTeklifCek = $("#buttonTeklifCek");

            switch (_kullaniciDurum) {

                case 1:
                    buttonTeklifVer.prop("disabled", false);
                    buttonTeklifCek.prop("disabled", true);
                    break;
                case 2:
                    buttonTeklifVer.prop("disabled", true);
                    buttonTeklifCek.prop("disabled", false);
                    break;
                case 3:
                    buttonTeklifVer.prop("disabled", true);
                    buttonTeklifCek.prop("disabled", true);
                    break;
                case 0:
                case 4:
                case 5:
                    buttonTeklifVer.prop("disabled", true);
                    buttonTeklifCek.prop("disabled", true);
                    break;
            }
        },
        kullaniciTeklifVer: function () {
            ticker.server.kullaniciKontrol();
        },
        kullaniciTeklifCek: function () {
            ticker.server.kullaniciKontrol();
        },        
        satisBaslat: function () {
            var buttonTeklifVer = $("#buttonTeklifVer");
            var buttonTeklifCek = $("#buttonTeklifCek");

            buttonTeklifVer.prop("disabled", false);
            buttonTeklifCek.prop("disabled", true);
        },
        satisBitir: function () {
            var buttonTeklifVer = $("#buttonTeklifVer");
            var buttonTeklifCek = $("#buttonTeklifCek");

            buttonTeklifVer.prop("disabled", true);
            buttonTeklifCek.prop("disabled", true);
        },
        satisKapat: function () {
            var buttonTeklifVer = $("#buttonTeklifVer");
            var buttonTeklifCek = $("#buttonTeklifCek");

            buttonTeklifVer.prop("disabled", true);
            buttonTeklifCek.prop("disabled", true);
        },

        updateSunucuZaman: function (_sunucuZaman) {
            var spanTarihSaat = document.getElementById('spanTarihSaat');
            spanTarihSaat.innerHTML = _sunucuZaman;
        },
        updateSatis: function (_satisEkran) {

            var spanCevirimDurum = document.getElementById('spanCevirimDurum');
            var spanSatisDurum = document.getElementById('spanSatisDurum');
            var spanAktifKullanicilar = document.getElementById('spanAktifKullanicilar');
            var spanTeklifVerenKullanicilar = document.getElementById('spanTeklifVerenKullanicilar');
            var spanSatisNo = document.getElementById('spanSatisNo');
            var spanAnlikFiyat = document.getElementById('spanAnlikFiyat');
            var spanOrtalamaFiyat = document.getElementById('spanOrtalamaFiyat');
            var spanSayac = document.getElementById('spanSayac');

            var divAnlikFiyat = $('#divAnlikFiyat');
            divAnlikFiyat.removeClass();
            
            var buttonTeklifVer = $("#buttonTeklifVer");
            var buttonTeklifCek = $("#buttonTeklifCek");
            
            var imgCevirimDurum = document.getElementById('imgCevirimDurum');

            spanCevirimDurum.innerHTML = '';
            spanSatisDurum.innerHTML = 'Pasif';
            spanAktifKullanicilar.innerHTML = '';
            spanTeklifVerenKullanicilar.innerHTML = '';
            spanSatisNo.innerHTML = '0';
            spanAnlikFiyat.innerHTML = '-';
            spanOrtalamaFiyat.innerHTML = '0';
            spanSayac.innerHTML = '-';
           
            switch (_satisEkran.SunucuDurum) {
                case 0:
                    spanCevirimDurum.innerHTML = "Çevirimdışı";
                    buttonTeklifVer.prop("disabled", true);
                    buttonTeklifCek.prop("disabled", true);
              
                    imgCevirimDurum.src = "../Content/Images/Icons/warning.png";
                    divAnlikFiyat.addClass('widget-head-color-box yellow-bg p-lg text-center');
                    break;
                case 1:
                    spanCevirimDurum.innerHTML = "Çevirimiçi";
                    
                    imgCevirimDurum.src = "../Content/Images/Icons/green_tick.png";
                    divAnlikFiyat.addClass('widget-head-color-box yellow-bg p-lg text-center');
                    break;
                case 2:
                    spanCevirimDurum.innerHTML = "Çevirimiçi";
                    spanSatisDurum.innerHTML = 'Aktif';
                    
                    imgCevirimDurum.src = "../Content/Images/Icons/green_tick.png";
                    divAnlikFiyat.addClass('widget-head-color-box lazur-bg p-lg text-center');
                    break;
                case 3:
                    spanCevirimDurum.innerHTML = "Çevirimiçi";
                    spanSatisDurum.innerHTML = 'Aktif';
                    
                    imgCevirimDurum.src = "../Content/Images/Icons/green_tick.png";
                    divAnlikFiyat.addClass('widget-head-color-box lazur-bg p-lg text-center');
                    break;
                case 4:
                    spanCevirimDurum.innerHTML = "Çevirimiçi";
                    spanSatisDurum.innerHTML = 'Aktif';
                    
                    imgCevirimDurum.src = "../Content/Images/Icons/green_tick.png";
                    divAnlikFiyat.addClass('widget-head-color-box red-bg p-lg text-center');
                    break;
                case 5:
                    spanCevirimDurum.innerHTML = "Çevirimiçi";
                    spanSatisDurum.innerHTML = 'Pasif';
                    buttonTeklifVer.prop("disabled", true);
                    buttonTeklifCek.prop("disabled", true);
                    
                    imgCevirimDurum.src = "../Content/Images/Icons/green_tick.png";
                    divAnlikFiyat.addClass('widget-head-color-box yellow-bg p-lg text-center');
                    break;
            case 6:
                    spanCevirimDurum.innerHTML = "Çevirimiçi";
                    spanSatisDurum.innerHTML = 'Pasif';
                    buttonTeklifVer.prop("disabled", true);
                    buttonTeklifCek.prop("disabled", true);
                    
                    imgCevirimDurum.src = "../Content/Images/Icons/green_tick.png";
                    divAnlikFiyat.addClass('widget-head-color-box yellow-bg p-lg text-center');
                    break;
            }

            if (_satisEkran.AktifSatisKey != -1) {
                spanSatisNo.innerHTML = _satisEkran.AktifSatisKey;
                spanAnlikFiyat.innerHTML = _satisEkran.SonFiyat;
                spanOrtalamaFiyat.innerHTML = _satisEkran.GenelTeklifOrtalama;
            }

            var diziAktifKullanicilar = _satisEkran.AktifKullanicilar;
            for (var i = 0; i < diziAktifKullanicilar.length; i++) {
                spanAktifKullanicilar.innerHTML += diziAktifKullanicilar[i] + '</br>';
            }

            var diziTeklifVerenKullanicilar = _satisEkran.TeklifVerenKullanicilar;
            for (var i = 0; i < diziTeklifVerenKullanicilar.length; i++) {
                spanTeklifVerenKullanicilar.innerHTML += diziTeklifVerenKullanicilar[i] + '</br>';

                
                if (_satisEkran.Sayac > 0)
                    spanSayac.innerHTML = _satisEkran.Sayac;
                else
                    spanSayac.innerHTML = '-';
            }
        }    
    });

    $.connection.
        hub.
        start().
        done(function () {

            ticker.server.kullaniciGiris();
            ticker.server.kullaniciKontrol();

            $("#buttonTeklifVer").click(function () {
                ticker.server.kullaniciTeklifVer();
            });
            $("#buttonTeklifCek").click(function () {
                ticker.server.kullaniciTeklifCek();
            });
        });
});