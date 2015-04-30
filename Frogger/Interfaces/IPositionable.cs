using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ChrisJones.Frogger.Drawing2D;

namespace ChrisJones.Frogger.Interfaces
{
    public interface IPositionable
    {
        void SetPosition(Position position);
        Position GetPosition();
    }
}
