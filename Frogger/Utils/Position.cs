namespace Frogger.Utils
{
    public class Position
    {
        public double XPos { get; private set; }
        public double YPos { get; private set; }

        public Position(double xpos, double ypos)
        {
            SetPosition(xpos, ypos);
        }

        public void SetPosition(double xpos, double ypos)
        {
            XPos = xpos;
            YPos = ypos;
        }
    }
}