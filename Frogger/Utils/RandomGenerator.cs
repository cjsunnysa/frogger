using System;

namespace Frogger.Utils
{
    public class RandomGenerator
    {
        private int _lastPos = 0;

        public double GetRandomPosition(int upper)
        {
            var randomGen = new Random (_lastPos);
            var pos = randomGen.Next (upper);

            _lastPos = (int)pos;

            return pos;
        }
    }
}