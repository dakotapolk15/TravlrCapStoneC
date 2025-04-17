
public class TravelService : ITravelService
{
    public List<string> GetTravelData()
    {
        return new List<string>
        {
            "Beach Getaway",
            "Mountain Retreat",
            "City Tour"
        };
    }
}
