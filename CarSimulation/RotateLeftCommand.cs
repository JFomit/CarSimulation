using System;
using System.Collections.Generic;
using System.Text;

namespace CarSimulation
{
    class RotateLeftCommand : ICommand
    {
        public int OpCode { get => opCode; }
        public const int opCode = 2;

        private readonly float RotationSpeed;

        public RotateLeftCommand(float rotationSpeed)
        {
            RotationSpeed = rotationSpeed;
        }

        public int Execute(Car thisAgent)
        {
            thisAgent.Rotate(RotationSpeed);
            //going to next command
            return 1;
        }
    }
}
