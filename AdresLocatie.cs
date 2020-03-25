namespace ADONETopdracht
{
    public class AdresLocatie
    {
        public AdresLocatie(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public AdresLocatie(int id, double x, double y)
        {
            Id = id;
            this.x = x;
            this.y = y;
        }

        public int Id { get; set; }
        public double x { get; set; }
        public double y { get; set; }
    }
}