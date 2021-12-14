using System;
using System.Collections.Generic;
using System.Text;

namespace CarSimulation
{
    class SeekForwardCommand : SeekCommand, ICommand
    {
        public override int OpCode { get => opCode; }
        public const int opCode = 4;

        protected new const float RayCastAngle = 0f;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detectionRange">Range in from..to format, where car can detect obstacles</param>
        /// <param name="intervalsCount">amount of intervals, which conditional jumps are based on</param>
        public SeekForwardCommand(int detectionRange, int intervalsCount) : base(detectionRange, intervalsCount)
        { }
    }
}
