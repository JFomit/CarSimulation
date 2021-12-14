using System;
using System.Collections.Generic;
using System.Text;

namespace CarSimulation
{
    class SeekRightBackwardCommand : SeekCommand, ICommand
    {
        public override int OpCode { get => opCode; }
        public const int opCode = 9;

        protected new const float RayCastAngle = 135f;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detectionRange">Range in from..to format, where car can detect obstacles</param>
        /// <param name="intervalsCount">amount of intervals, which conditional jumps are based on</param>
        public SeekRightBackwardCommand(int detectionRange, int intervalsCount) : base(detectionRange, intervalsCount)
        { }
    }
}
