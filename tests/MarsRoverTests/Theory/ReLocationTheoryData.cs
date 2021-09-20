using MarsRoverTests.Poco;
using Xunit;

namespace MarsRoverTests.Theory
{
    public class ReLocationTheoryData : TheoryData<ReLocateTestParameter>
    {
        public ReLocationTheoryData()
        {
            Add(new ReLocateTestParameter
            {
                InitialLocationX = 1,
                InitialLocationY = 1,
                InitialHeading = "N",
                Command = "RMMLMMLMMMMMLMRM",
                ExpectedLocationX = -3,
                ExpectedLocationY = 2,
                ExpectedHeading = "W"
            });

            Add(new ReLocateTestParameter
            {
                InitialLocationX = 0,
                InitialLocationY = 0,
                InitialHeading = "S",
                Command = "MLMLMMLMMLMLMR",
                ExpectedLocationX = 0,
                ExpectedLocationY = 0,
                ExpectedHeading = "S"
            });

            Add(new ReLocateTestParameter
            {
                InitialLocationX = 3,
                InitialLocationY = 2,
                InitialHeading = "E",
                Command = "LLLLLM",
                ExpectedLocationX = 3,
                ExpectedLocationY = 3,
                ExpectedHeading = "N"
            });
        }
    }
}
