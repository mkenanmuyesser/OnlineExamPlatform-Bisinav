using BiSinavProject.PL.Class.Enum;
using System;
using System.Collections.Generic;

namespace BiSinavProject.PL.Class.CustomType
{
    [Serializable]
    public class DenemeType
    {
        public int DenemeKullaniciKey { get; set; }
        public int AktifDenemeKey { get; set; }
        public int AktifDersKey { get; set; }
        public string AktifDenemeAdi { get; set; }
        public List<SoruType> Sorular { get; set; }
        public int AktifSoruNo { get; set; }
        public SinavModeEnum SinavMode { get; set; }
        public DateTime BaslangicZamani { get; set; }
        public DateTime IslemZamani { get; set; }
        public int GecenSure { get; set; }
        public int ToplamDakika { get; set; }
        public bool SinavKayit { get; set; }
    }
}