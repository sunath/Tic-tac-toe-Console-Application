using System;

namespace Tic_tac_toe
{
    class Program
    {

        private static string[,] TicTacToeTable = new string[3, 3]

        {
           {"1","2","3" },
           {"4","5","6" },
           {"7","8","9" }
        };

        private static string[,] defaultTable = new string[3, 3]

       {
           {"1","2","3" },
           {"4","5","6" },
           {"7","8","9" }
       };


        public static bool IsPlayerOneChance = true;

        static string playerOneMark = "O";
        static string playerTwoMark = "X";

        static bool IsMoveSuccess;

        static bool IsPlayerOneWon = false;
        static bool IsPlayerTwoWon = false;
        static bool AnyPlayerWon = false;
        static bool IsHasToRestart = false;
        static void Main(string[] args)
        {

            while (true)
            {

                while (!AnyPlayerWon && !IsHasToRestart)
                {
                    DisplayTicTacToe();
                    while (!IsMoveSuccess)
                    {
                        Console.Write("{0} Enter a position you want to get: ", (IsPlayerOneChance ? "Player1" : "Player2"));
                        int number;
                        bool success = int.TryParse(Console.ReadLine(), out number);
                        if (success)
                            IsMoveSuccess = PlayerMove(number);

                        IsHasToRestart = CheckAllMovementBlock();
                        if (!IsMoveSuccess)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Please Enter a Number Which is not used and below than 10");
                            Console.ForegroundColor = ConsoleColor.White;
                        }




                    }
                    if (IsMoveSuccess)
                    {
                        IsMoveSuccess = false;
                        IsPlayerOneWon = IsAnyPlayerComplete(playerOneMark);
                        IsPlayerTwoWon = IsAnyPlayerComplete(playerTwoMark);
                        AnyPlayerWon = IsPlayerOneWon || IsPlayerTwoWon;
                        IsPlayerOneChance = !IsPlayerOneChance;
                        Console.Clear();
                    }

                }

                if (!IsHasToRestart)
                {
                    Console.WriteLine("Player{0} Won the game", IsPlayerOneWon ? "1" : "2");

                }
                else
                {
                    Console.WriteLine("Game is tied");
                }
                Console.Write("In order to restart press any key that you want : ");
                Console.ReadKey();
                AnyPlayerWon = false;
                IsPlayerOneWon = false;
                IsPlayerTwoWon = false;
                IsPlayerOneChance = true;
                IsHasToRestart = false;
                TicTacToeTable = defaultTable;
                Console.Clear();

            }
        }

        public static void DisplayTicTacToe()
        {
            for (int i = 0; i < TicTacToeTable.GetLength(0); i++)
            {
                string[] currentFields = new string[TicTacToeTable.GetLength(1)];
                for(int j = 0; j < currentFields.Length; j++)
                {
                    currentFields[j] = TicTacToeTable[i,j];
                }

                /*
                string message =$"                    |                     |                     |\n "                          +
                                $" {currentFields[0]} | {currentFields[1]}  | {currentFields[2]}  |\n"+
                                $"_________________________________________________________________";
                */

                string displayFormat = " {0} | {1} | {2}";
                string displaySquarUp = "___|___|___";

                string message = String.Format(displayFormat, currentFields[0], currentFields[1], currentFields[2])+"\n"+(i != TicTacToeTable.GetLength(0) - 1 ? displaySquarUp : null);

                Console.WriteLine("   |   |");
                Console.WriteLine(message);
                
            }
        }

        public static bool PlayerMove(int number)
        {
            bool state = false;
            switch (number)
            {
                case 1:
                    state = ChangePosition(0,0);
                    break;
                case 2:
                    state = ChangePosition(0, 1);
                    break;
                case 3:
                    state = ChangePosition(0, 2);
                    break;
                case 4:
                    state = ChangePosition(1, 0);
                    break;
                case 5:
                    state = ChangePosition(1, 1);
                    break;
                case 6:
                    state = ChangePosition(1, 2);
                    break;
                case 7:
                    state = ChangePosition(2, 0);
                    break;
                case 8:
                    state = ChangePosition(2, 1);
                    break;
                case 9:
                    state = ChangePosition(2, 2);
                    break;
                default:
                    state = false;
                    break;




            }
            return state;

        }

        public static bool ChangePosition(int row,int column)
        {
            if (!PlayerAlreadyGotPosition(row, column))
            {
                TicTacToeTable[row, column] = IsPlayerOneChance ? playerOneMark : playerTwoMark;
                return true;
            }
            return false;
        }

        public static bool PlayerAlreadyGotPosition(int row,int col)
        {
            if(TicTacToeTable[row,col] == playerOneMark || TicTacToeTable[row, col] == playerTwoMark)
            {
                return true;
            }
            return false;
        }

        public static bool IsAnyPlayerComplete(string playerSign)
        {
            if (CheckPlayerRows(playerSign))
            {
                return true;
            }
            if (CheckPlayerColumn(playerSign))
            {
                return true;
            }
            if (CheckPlayerOverToRight(playerSign))
            {
                return true;
            }
            if (CheckPlayerOverToLeft(playerSign))
            {
                return true;
            }
            return false;

        }
        public static bool CheckPlayerRows(string playerSign)
        {

            bool isSuccess = true;

            for (int i = 0; i < TicTacToeTable.GetLength(0); i++)
            {

                for (int j = 0; j < TicTacToeTable.GetLength(0); j++)
                {
                    isSuccess = TicTacToeTable[i, j] == playerSign;
                    if (!isSuccess)
                    {
                        break;
                    }

                }
                if (isSuccess)
                {
                    break;
                }
            }
            return isSuccess;
        }
    
        public static bool CheckPlayerColumn(string playerSign)
        {
            bool isSuccess = true;
                for(int column = 0; column < TicTacToeTable.GetLength(1); column++)
                {
                    for(int row = 0; row < TicTacToeTable.GetLength(1); row++)
                    {
                    isSuccess = TicTacToeTable[row, column] == playerSign;
                        if (!isSuccess)
                        {
                            break;
                        }
                    }
                    if (isSuccess)
                    {
                        break;
                    }

                }
            return isSuccess;
        }

        public static bool CheckPlayerOverToRight(string playerSign)
        {
            bool isSuccess = true;

            for(int i = 0 , j = 0;i< TicTacToeTable.GetLength(0);i++,j++)
            {
                isSuccess = TicTacToeTable[i, j] == playerSign;
                if (!isSuccess)
                {
                    break;
                }
            }

            return isSuccess;
        }
    
        public static bool CheckPlayerOverToLeft(string playerSign)
        {
            bool isSuccess = true;
            for(int i =0 , j = TicTacToeTable.GetLength(1) -1; i < TicTacToeTable.GetLength(0); i++, j--)
            {
                isSuccess = TicTacToeTable[i, j] == playerSign;
                if (!isSuccess)
                {
                    break;
                }
            }
            return isSuccess;
        }


        public static bool CheckAllMovementBlock()
        {
            bool isSuccess = true;
            foreach(string item in TicTacToeTable)
            {
                isSuccess = item == playerOneMark || item == playerTwoMark;
                if (!isSuccess)
                {
                    break;
                }
            }
            return isSuccess;
        }
    }
}
