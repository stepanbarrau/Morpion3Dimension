using System;
using System.Collections.Generic;
using System.Text;

namespace Morpion3Dimension.Model
{
    public delegate void Del(IPlayer player);

    public interface IPlayer
    {
        Symbol GetSymbol();
        Move AskMove();
        void SendGrid(Grid grid);
        void SendGameOver(bool victory);

        void SetDisconnection(Del handler);

    }
}
