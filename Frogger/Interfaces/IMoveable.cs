using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChrisJones.Frogger.Drawing2D;

namespace ChrisJones.Frogger.Interfaces
{
    public interface IMoveable
    {
        void Move();
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();
    }
}
