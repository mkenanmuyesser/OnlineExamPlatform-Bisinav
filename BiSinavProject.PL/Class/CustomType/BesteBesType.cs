using BiSinavProject.PL.Class.Enum;
using System;
using System.Collections.Generic;

namespace BiSinavProject.PL.Class.CustomType
{
    [Serializable]
    public class BesteBesType
    {
        public int AktifDersKey { get; set; }
        public string AktifDersAdi { get; set; }
        public List<SoruType> Sorular { get; set; }
        public int AktifSoruNo { get; set; }
        public SinavModeEnum SinavMode { get; set; }
        public DateTime BaslangicZamani { get; set; }
        public DateTime BitisZamani { get; set; }
        public bool SinavKayit { get; set; }
    }
}