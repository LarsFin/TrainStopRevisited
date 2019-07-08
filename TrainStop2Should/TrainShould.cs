using Shouldly;
using TrainStop2;
using Xunit;

namespace TrainStop2Should
{
    public class TrainShould
    {
        Train train;
        string name = "SWR0299";

        public TrainShould()
        {
            train = new Train(name);
        }

        [Fact]
        public void havePassedName()
        {
            train.Name.ShouldBe(name);
        }
    }
}
