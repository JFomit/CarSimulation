﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarSimulation
{
    class SeekLeftCommand : SeekCommand, ICommand
    {
        public override int OpCode { get => opCode; }
        public const int opCode = 6;

        protected new const float RayCastAngle = 270f;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detectionRange">Range in from..to format, where car can detect obstacles</param>
        /// <param name="intervalsCount">amount of intervals, which conditional jumps are based on</param>
        public SeekLeftCommand(int detectionRange, int intervalsCount) : base(detectionRange, intervalsCount)
        { }
    }
}
