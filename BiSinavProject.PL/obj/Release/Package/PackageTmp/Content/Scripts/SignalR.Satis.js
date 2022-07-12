$(function () {

    var ticker = $.connection.satisTicker;

    $(window).unload(function () {
        ticker.server.satisCevirimdisi();
        //$("#divSatis").css('zIndex', 1000);
    });

    $.extend(ticker.client, {
        satisBaslat: function () {
            var buttonSatisIptal = $("#buttonSatisIptal");
            var buttonSatisAnaSayfa = $("#buttonSatisAnaSayfa");
            buttonSatisIptal.prop("disabled", true);
            buttonSatisAnaSayfa.prop("disabled", true);
        },
        satisBitir: function () {
            var buttonSatisaBasla = $("#buttonSatisaBasla");
            var buttonSatisiKapat = $("#buttonSatisiKapat");
            buttonSatisaBasla.prop("disabled", true);
            buttonSatisiKapat.prop("disabled", true);
            var hiddenSatisKey = document.getElementById('hiddenSatisKey');
            hiddenSatisKey.innerHTML = "-1";

            location.reload();
        },
        satisKapat: function () {
            var buttonSatisaBasla = $("#buttonSatisaBasla");
            var buttonSatisiKapat = $("#buttonSatisiKapat");
            var buttonSatisIptal = $("#buttonSatisIptal");
            var buttonSatisAnaSayfa = $("#buttonSatisAnaSayfa");
            buttonSatisaBasla.prop("disabled", false);
            buttonSatisiKapat.prop("disabled", true);
            buttonSatisIptal.prop("disabled", false);
            buttonSatisAnaSayfa.prop("disabled", false);
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
            
            var buttonSatisaBasla = $("#buttonSatisaBasla");
            var buttonSatisiKapat = $("#buttonSatisiKapat");
            
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
                    buttonSatisaBasla.prop("disabled", true);
                    buttonSatisiKapat.prop("disabled", true);
                    
                    imgCevirimDurum.src = "../../Content/Images/Icons/warning.png";
                    divAnlikFiyat.addClass('widget-head-color-box yellow-bg p-lg text-center');
                    break;
                case 1:
                    spanCevirimDurum.innerHTML = "Çevirimiçi";
                    buttonSatisaBasla.prop("disabled", true);
                    buttonSatisiKapat.prop("disabled", true);
                    
                    imgCevirimDurum.src = "../../Content/Images/Icons/green_tick.png";
                    divAnlikFiyat.addClass('widget-head-color-box yellow-bg p-lg text-center');
                    break;
                case 2:
                    spanCevirimDurum.innerHTML = "Çevirimiçi";
                    spanSatisDurum.innerHTML = 'Aktif';
                    buttonSatisaBasla.prop("disabled", true);
                    buttonSatisiKapat.prop("disabled", false);
                    
                    imgCevirimDurum.src = "../../Content/Images/Icons/green_tick.png";
                    divAnlikFiyat.addClass('widget-head-color-box lazur-bg p-lg text-center');
                    break;
                case 3:
                    spanCevirimDurum.innerHTML = "Çevirimiçi";
                    spanSatisDurum.innerHTML = 'Aktif';
                    buttonSatisaBasla.prop("disabled", true);
                    buttonSatisiKapat.prop("disabled", false);
                    
                    imgCevirimDurum.src = "../../Content/Images/Icons/green_tick.png";
                    divAnlikFiyat.addClass('widget-head-color-box lazur-bg p-lg text-center');
                    break;
                case 4:
                    spanCevirimDurum.innerHTML = "Çevirimiçi";
                    spanSatisDurum.innerHTML = 'Aktif';
                    buttonSatisaBasla.prop("disabled", true);
                    buttonSatisiKapat.prop("disabled", false);
                    
                    imgCevirimDurum.src = "../../Content/Images/Icons/green_tick.png";
                    divAnlikFiyat.addClass('widget-head-color-box red-bg p-lg text-center');
                    break;
                case 5:
                    spanCevirimDurum.innerHTML = "Çevirimiçi";
                    spanSatisDurum.innerHTML = 'Pasif';
                    buttonSatisaBasla.prop("disabled", true);
                    buttonSatisiKapat.prop("disabled", true);
                    
                    imgCevirimDurum.src = "../../Content/Images/Icons/green_tick.png";
                    divAnlikFiyat.addClass('widget-head-color-box yellow-bg p-lg text-center');
                    break;
                case 6:
                    spanCevirimDurum.innerHTML = "Çevirimiçi";
                    spanSatisDurum.innerHTML = 'Pasif';
                    buttonSatisaBasla.prop("disabled", false);
                    buttonSatisiKapat.prop("disabled", true);
                    
                    imgCevirimDurum.src = "../../Content/Images/Icons/green_tick.png";
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
        },
    });

    // Start the connection
    $.connection.
        hub.
        start().
        done(function () {

            ticker.server.satisCevirimici();

            $("#buttonSatisaBasla").click(function () {
                var satisKey = document.getElementById('hiddenSatisKey').value;
                var satisBaslangicFiyati = document.getElementById('hiddenSatisBaslangicFiyati').innerText;
                ticker.server.satisBaslat(satisKey, satisBaslangicFiyati);
            });
            $("#buttonSatisiKapat").click(function () {
                ticker.server.satisKapat();
            });
        });
});