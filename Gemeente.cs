namespace ADONETopdracht
{
    public class Gemeente
    {
        public Gemeente(int nIScode, string gemeentenaam)
        {
            NIScode = nIScode;
            this.gemeentenaam = gemeentenaam;
        }

        public int NIScode { get; set; }
        public string gemeentenaam { get; set; }

        public override string ToString()
        {
            return $"Gemeente[" +
                $"{NIScode}," +
                $"{gemeentenaam}]";
        }
    }
}