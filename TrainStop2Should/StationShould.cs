using Shouldly;
using TrainStop2;
using Xunit;

namespace TrainStop2Should
{
    public class StationShould
    {
        Station station;
        string name = "London Waterloo";

        public StationShould()
        {
            station = new Station(name);
        }

        [Fact]
        public void HavePassedName()
        {
            station.Name.ShouldBe(name);
        }
    }
}
