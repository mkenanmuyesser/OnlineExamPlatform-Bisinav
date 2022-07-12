namespace BiSinavProject.PL.Class.CustomType
{
    public class SoruType
    {
        public int DersKey { get; set; }
        public string DersAdi { get; set; }
        public int SoruKey { get; set; }
        public int SoruNo { get; set; }
        public string Soru { get; set; }
        public string SoruDogruSik { get; set; }
        public byte SoruSikSayisi { get; set; }
        public string SoruAciklama { get; set; }
       
        public string SoruIsaretlenenSik { get; set; }
        public bool SoruGoruntulendiMi { get; set; }
    }
}