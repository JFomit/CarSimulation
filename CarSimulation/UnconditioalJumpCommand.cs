﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarSimulation
{
    class UnconditioalJumpCommand : ICommand
    {
        public int OpCode { get => opCode; }
        public const int opCode = 8;

        private readonly int Skip;

        public UnconditioalJumpCommand(int skip)
        {
            Skip = skip;
        }

        public int Execute(Agent thisAgent)
        {
            //going to next + skipping
            return Skip + 1;
        }
    }
}