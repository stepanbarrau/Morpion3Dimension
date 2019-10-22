using System;
using System.Collections.Generic;
using System.Text;

namespace Morpion3Dimension.Model
{
    public class Game
    {
        IPlayer currentPlayer;
        IPlayer otherPlayer;
        IPlayer player1;
        IPlayer player2;

        Grid grid;
        Rules rules;
        public bool isOver;

        public Game(IPlayer player1, IPlayer player2)
        {
            this.player1 = player1;
            this.player2 = player2;
            rules = new Rules();
            start();
        }

        void start()
        {
            Console.WriteLine("game started");
            currentPlayer = player1; otherPlayer = player2;
            grid = new Grid();
            isOver = false;
            BroadcastGrid();

            while (! isOver)
            { 
                Move move = currentPlayer.AskMove();
                var symbol = currentPlayer.GetSymbol();

                // check if move is valid
                if (! rules.isValidMove(move, symbol, grid))
                {
                    continue;
                }

                // play move
                grid.PlayMove(move, symbol);
                BroadcastGrid();

                // check if player won

                if (rules.winCheck(move, symbol, grid))
                {
                    isOver = true;
                    currentPlayer.SendGameOver(WinType.win, rules.winningSequence);
                    otherPlayer.SendGameOver(WinType.lose, rules.winningSequence);
                }

                // otherwise it is the other player turn
                (currentPlayer, otherPlayer) = (otherPlayer, currentPlayer);

                // if it's a draw stop the game
                if (rules.IsDraw(grid))
                {
                    isOver = true;
                    currentPlayer.SendGameOver(WinType.noContest, rules.winningSequence); otherPlayer.SendGameOver(WinType.noContest, rules.winningSequence);
                }
            }
        }

        private void BroadcastGrid()
        {
            Console.WriteLine("broadcasing game");
            player1.SendGrid(grid);
            player2.SendGrid(grid);
        }
    }
}
