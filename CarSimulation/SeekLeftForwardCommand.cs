using System;
using System.Collections.Generic;
using System.Text;

namespace CarSimulation
{
    class SeekLeftForwardCommand : SeekCommand, ICommand
    {
        public override int OpCode { get => opCode; }
        public const int opCode = 11;

        protected new const float RayCastAngle = 315f;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detectionRange">Range in from..to format, where car can detect obstacles</param>
        /// <param name="intervalsCount">amount of intervals, which conditional jumps are based on</param>
        public SeekLeftForwardCommand(int detectionRange, int intervalsCount) : base(detectionRange, intervalsCount)
        { }
    }
}
