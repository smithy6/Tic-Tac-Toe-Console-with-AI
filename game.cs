using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    class Program
    {
        static char[] board = new char[9]; // The board of the game
        static int currentPlayer = 1; // The current player (1 for player 1, 2 for player 2)
        static bool isComputer = false; // Whether the second player is a computer or not

        static void Main(string[] args)
        {
            InitializeBoard();

            // Ask the user if they want to play against a computer
            Console.WriteLine("Do you want to play against a computer? (y/n)");
            string input = Console.ReadLine();
            if (input == "y")
            {
                isComputer = true;
            }

            // Keep playing until the game is over
            bool gameOver = false;
            while (!gameOver)
            {
                PrintBoard();
                
                // Get the next move from the current player
                int move = -1;
                if (currentPlayer == 1)
                {
                    move = GetPlayerMove();
                }
                else
                {
                    if (isComputer)
                    {
                        move = GetComputerMove();
                    }
                    else
                    {
                        move = GetPlayerMove();
                    }
                }

                // Make the move
                board[move] = currentPlayer == 1 ? 'X' : 'O';

                // Check if the game is over
                gameOver = IsGameOver();

                // Switch players
                currentPlayer = currentPlayer == 1 ? 2 : 1;
            }
            
            // Print the final board
            PrintBoard();

            // Print a message to indicate who won
            if (IsWin())
            {
                Console.WriteLine("Player {0} wins!", currentPlayer);
            }
            else
            {
                Console.WriteLine("It's a draw!");
            }
        }

        // Initializes the board with empty spaces
        static void InitializeBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                board[i] = ' ';
            }
        }

        // Prints the current state of the board
        static void PrintBoard()
        {
            Console.Clear();
            Console.WriteLine(" {0} | {1} | {2} ", board[0], board[1], board[2]);
            Console.WriteLine("-----------");
            Console.WriteLine(" {0} | {1} | {2} ", board[3], board[4], board[5]);
            Console.WriteLine("-----------");
            Console.WriteLine(" {0} | {1} | {2} ", board[6], board[7], board[8]);
        }
        static int GetPlayerMove()
        {
            Console.WriteLine("Player {0}, enter a number (1-9): ", currentPlayer);
            string input = Console.ReadLine();
            int move = Int32.Parse(input);
            move--;
            if (move >= 0 && move <= 8 && board[move] == ' ')
            {
                return move;
            }
            else
            {
                Console.WriteLine("Invalid move. Try again.");
                return GetPlayerMove();
            }
        }

        // Gets a move from the computer
        static int GetComputerMove()
        {
            // Check if the computer can win in the next move
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == ' ')
                {
                    board[i] = 'O';
                    if (IsWin())
                    {
                        return i;
                    }
                    board[i] = ' ';
                }
            }

            // Check if the player can win in the next move and block them
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == ' ')
                {
                    board[i] = 'X';
                    if (IsWin())
                    {
                        return i;
                    }
                    board[i] = ' ';
                }
            }

            // Try to take one of the corners
            if (board[0] == ' ') return 0;
            if (board[2] == ' ') return 2;
            if (board[6] == ' ') return 6;
            if (board[8] == ' ') return 8;

            // Try to take the center
            if (board[4] == ' ') return 4;

            // Take any remaining space
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == ' ')
                {
                    return i;
                }
            }

            return -1;
        }

        // Returns true if the game is over
        static bool IsGameOver()
        {
            // The game is over if someone has won or there are no more empty spaces
            return IsWin() || !board.Contains(' ');
        }

        // Returns true if one of the players has won
        static bool IsWin()
        {
            // Check the rows
            if (board[0] != ' ' && board[0] == board[1] && board[1] == board[2])
            {
                return true;
            }
            if (board[3] != ' ' && board[3] == board[4] && board[4] == board[5])
            {
                return true;
            }
            if (board[6] != ' ' && board[6] == board[7] && board[7] == board[8])
            {
                return true;
            }

            // Check the columns
            if (board[0] != ' ' && board[0] == board[3] && board[3] == board[6])
            {
                return true;
            }
            if (board[1] != ' ' && board[1] == board[4] && board[4] == board[7])
            {
                return true;
            }
            if (board[2] != ' ' && board[2] == board[5] && board[5] == board[8])
            {
                return true;
            }

            // Check the diagonals
            if (board[0] != ' ' && board[0] == board[4] && board[4] == board[8])
            {
                return true;
            }
            if (board[2] != ' ' && board[2] == board[4] && board[4] == board[6])
            {
                return true;
            }

            return false;
        }
    }
}