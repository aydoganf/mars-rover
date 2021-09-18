using MarsRoverProject.Contracts.Data;
using System;

namespace MarsRoverProject.Domain
{
    public class RoverCommand : IRoverCommand
    {
        public string Selector { get; internal set; }
        public string Command { get; internal set; }

        public RoverCommand(string selector, string command)
        {
            Selector = selector;
            Command = command;
        }

        public ILocationInfo GetSelectorLocation()
        {
            string[] selectorArr = Selector.Split(' ');

            if (selectorArr.Length < 2)
                throw new InvalidOperationException("Selector commands must have location info");

            int x = 0;
            int y = 0;

            for (int i = 0; i < selectorArr.Length; i++)
            {
                if (i == 2)
                    break;

                if (int.TryParse(selectorArr[i], out int l) == false)
                    throw new InvalidOperationException("Invalid location selector message!");

                if (i == 0)
                    x = l;
                else
                    y = l;
            }

            return new Location(x, y);
        }

        public string GetHeading()
        {
            string[] selectorArr = Selector.Split(' ');

            if (selectorArr.Length < 3)
                throw new InvalidOperationException("Selector commands must have location and heading info");

            return selectorArr[2].ToString().ToUpper();
        }

        public IMovementCommand GetMovementCommand()
        {
            return new MovementCommand(Command);
        }
    }
}
