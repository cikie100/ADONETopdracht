namespace ADONETopdracht
{
    public class Straatnaam
    {
        public Straatnaam(int id, string straatnaam, Gemeente gem)
        {
            this.gemeente = gem;
            ID = id;
            this.straatnaam = straatnaam;
        }

        public Gemeente gemeente { get; set; }
        public int ID { get; set; }
        public string straatnaam { get; set; }

        public override string ToString()
        {
            return $"Straatnaam[" +
                $"{ID}," +
                $"{straatnaam}," +
                $"{gemeente},]";
        }
    }
}