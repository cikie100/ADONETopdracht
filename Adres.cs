namespace ADONETopdracht
{
    public class Adres
    {
        public Adres(int id, Straatnaam straatnaam, string huisnummer, string appartementnummer, string busnummer, string huisnummerlabel, Gemeente gemeente, int postcode, double d1, double d2)
        {
            ID = id;
            this.straatnaam = straatnaam;
            this.huisnummer = huisnummer;
            this.appartementnummer = appartementnummer;
            this.busnummer = busnummer;

            this.huisnummerlabel = huisnummerlabel;
            this.postcode = postcode;
            this.locatie.x = d1;
            this.locatie.y = d2;
        }

        public string appartementnummer { get; set; }
        public string busnummer { get; set; }
        public string huisnummer { get; set; }
        public string huisnummerlabel { get; set; }
        public int ID { get; set; }
        public AdresLocatie locatie { get; set; }
        public int postcode { get; set; }
        public Straatnaam straatnaam { get; set; }

        public override string ToString()
        {
            return $"Adres[" +
                $"{ID}," +
                $"{straatnaam}," +
                $"{huisnummer}," +
                $"{appartementnummer}," +
                $"{busnummer}," +
                $"{huisnummerlabel}," +
                $"{postcode}]";
        }
    }
}