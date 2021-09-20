namespace MarsRoverTests.Poco
{
    public class ReLocateTestParameter
    {
        public int InitialLocationX { get; set; }
        public int InitialLocationY { get; set; }
        public string InitialHeading { get; set; }

        public string Command { get; set; }

        public int ExpectedLocationX { get; set; }
        public int ExpectedLocationY { get; set; }
        public string ExpectedHeading { get; set; }
    }
}
