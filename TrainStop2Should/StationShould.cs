using Shouldly;
using System.Collections.Generic;
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

        [Fact]
        public void HaveListForTrains()
        {
            station.Trains.ShouldBeOfType<List<ITrain>>();
        }
    }
}
