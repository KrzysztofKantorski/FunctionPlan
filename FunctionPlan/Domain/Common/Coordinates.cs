namespace Domain.Common
{
    public record Coordinates
    {
        public double Latitude { get;}
        public double Longitude { get; }

        public Coordinates(double latitude, double longitude)
        {
            if (latitude < -90 || latitude > 90)
            {
                throw new ArgumentException("Incorrect latitude value");
            }

            if (longitude < -180 || longitude > 180)
            {
                throw new ArgumentException("Incorrect longitude value");
            }

            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
