using System;
using System.Collections.Generic;
using System.Text;

namespace CarSimulation
{
    class MoveForwardCommand : ICommand
    {
        public int OpCode { get => opCode; }

        public const int opCode = 0;
        private readonly float MoveSpeed;

        public MoveForwardCommand(float moveSpeed)
        {
            MoveSpeed = moveSpeed;
        }

        public int Execute(Agent thisAgent)
        {
            thisAgent.Move(MoveSpeed);
            //going to next command
            return 1;
        }
    }
}
