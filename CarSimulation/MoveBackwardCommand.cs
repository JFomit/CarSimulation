using System;
using System.Collections.Generic;
using System.Text;

namespace CarSimulation
{
    class MoveBackwardCommand : ICommand
    {
        public int OpCode { get => opCode; }

        public const int opCode = 1;
        private readonly float MoveSpeed;

        public MoveBackwardCommand(float moveSpeed)
        {
            MoveSpeed = moveSpeed;
        }

        public int Execute(Car thisAgent)
        {
            float motion = -MoveSpeed;
            if (thisAgent.IsPunished)
            {
                motion *= thisAgent.PunismentSpeedModifier;
            }
            thisAgent.Move(motion);
            //going to next command
            return 1;
        }
    }
}
