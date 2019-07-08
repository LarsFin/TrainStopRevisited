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
        public void HavePassedName()
        {
            train.Name.ShouldBe(name);
        }

        [Fact]
        public void HasMovingStatus()
        {
            train.IsMoving.ShouldBeFalse();
        }

        [Fact]
        public void BeAbleToStartMovement()
        {
            train.StartMovement();
            train.IsMoving.ShouldBeTrue();
        }

        [Fact]
        public void BeAbleToStopMovement()
        {
            train.StartMovement();
            train.StopMovement();
            train.IsMoving.ShouldBeFalse();
        }
    }
}
