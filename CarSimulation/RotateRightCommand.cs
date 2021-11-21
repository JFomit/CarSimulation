using System;
using System.Collections.Generic;
using System.Text;

namespace CarSimulation
{
    class RotateRightCommand : ICommand
    {
        public int OpCode { get => opCode; }
        public const int opCode = 3;

        private readonly float RotationSpeed;

        public RotateRightCommand(float rotationSpeed)
        {
            RotationSpeed = rotationSpeed;
        }

        public int Execute(Agent thisAgent)
        {
            thisAgent.Rotate(-RotationSpeed);
            //going to next command
            return 1;
        }
    }
}
