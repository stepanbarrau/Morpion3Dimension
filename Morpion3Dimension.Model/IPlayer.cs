using System;
using System.Collections.Generic;
using System.Text;

namespace Morpion3Dimension.Model
{
    public interface IPlayer
    {
        Symbol GetSymbol();
        Move AskMove();
        void SendGrid(Grid grid);
        void SendGameOver(WinType winType, Position[] winningSequence);

    }
}
