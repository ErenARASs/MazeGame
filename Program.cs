using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame
{
    /*
        her biri arasinda bir bosluk olma zorunlulugu
        7 li 5 duvar ve 11 li 3 duvar yapma
        random p atama boardin icine 
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            int counter = 0;
            var board = Wall();
            for (int i = 0;i< 23; i++)
            {

                for (int j = 0; j < 53; j++)
                {
                    Console.Write(board[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine(counter);
        }


        // board creating
        public static string[,] CreateBoard()
        {
            var intBoard = Number();
            string[,] stringBoard = new string[23,53];
            for(int i = 0; i< 23; i++)
            {
                for(int j = 0;j <53; j++)
                {
                    if (intBoard[i, j] == 0)
                    {
                        stringBoard[i, j] = " ";
                    }
                    else if (intBoard[i, j] == 10)
                    {
                        stringBoard[i, j] = "0";
                    }

                    else
                    {
                        stringBoard[i, j] = Convert.ToString(intBoard[i, j]);
                    }
                    if (i == 0 || i == 22 || j == 0|| j == 52)
                    {
                        stringBoard[i, j] = "#";
                    }
                    
                }
            }
            return stringBoard;
        }

        // number creating
        static int[,] Number()
        {
            int[,] board = new int[23, 54];
            Random random = new Random();
            for (int i = 0; i<70; i++)
            {
                int number = random.Next(1,11);
                int a = random.Next(1, 22);
                int b = random.Next(1, 52);
                int checkValue = board[a, b];
                bool check = true; // deger bos oluncaya dek 
                while (check)
                {
                    if (checkValue == 0)
                    {
                        
                        board[a,b] = number;
                        check = false;
                    }
                    else
                    {
                        a = random.Next(1, 22);
                        b = random.Next(1, 52);
                        checkValue = board[a,b];
                    }
                }      
            }
            return board;
        }

        static string[,] Wall()
        {
            var board = CreateBoard();
            Random random = new Random();

            for (int i = 0;i< 20; i++)
            {
                bool check = true;
                int a = random.Next(1, 20);
                int a1 = a + 1;
                int a2 = a + 2;
                int b = random.Next(1, 50);
                int b1 = b + 1;
                int b2 = b + 2;
                int direction = random.Next(0, 2);
                List<string> checkList = new List<string>();
                // 3 wall 
                while (check) 
                {
                    
                    if (direction == 0) // horizontal
                    {
                        //checkList.Append(board[a-1, b-1]);

                        for (int j = -1; j < 2; j++)
                        {
                            for (int k = -1; k < 4; k++)
                            {
                                if (j == 0)
                                {
                                    checkList.Add(board[a , b - 1]);
                                    checkList.Add(board[a , b +3]);
                                }
                                else
                                {
                                    checkList.Add(board[a + j, b + k]);
                                }
                            }
                        }
                        if (!checkList.Contains("#"))
                        {
                            if (board[a, b] == " " && board[a, b1] == " " && board[a, b2] == " ")
                            {
                                board[a, b] = "#";
                                board[a, b1] = "#";
                                board[a, b2] = "#";
                                check = false;
                            }
                            else
                            {
                                a = random.Next(1, 22);
                                b = random.Next(1, 50);
                                b1 = b + 1;
                                b2 = b + 2;
                            }
                        }
                        else
                        {
                            a = random.Next(1, 22);
                            b = random.Next(1, 50);
                            b1 = b + 1;
                            b2 = b + 2;
                            checkList.RemoveRange(0, checkList.Count());
                        }
                    }
                    checkList.RemoveRange(0, checkList.Count());
                    if (direction == 1) // vertical
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            for (int k = -1; k < 4; k++)
                            {
                                if (j == 0)
                                {
                                    checkList.Add(board[a -1 , b ]);
                                    checkList.Add(board[a  +3, b ]);
                                }
                                else
                                {
                                    checkList.Add(board[a + k, b + j]);
                                }
                            }
                        }
                        if (!checkList.Contains("#"))
                        {
                            if (board[a, b] == " " && board[a1, b] == " " && board[a2, b] == " ")
                            {
                                board[a, b] = "#";
                                board[a1, b] = "#";
                                board[a2, b] = "#";
                                check = false;
                            }
                            else
                            {
                                a = random.Next(1, 20);
                                b = random.Next(1, 52);
                                a1 = a + 1;
                                a2 = a + 2;
                            }
                        }
                        else
                        {
                            a = random.Next(1, 20);
                            b = random.Next(1, 52);
                            a1 = a + 1;
                            a2 = a + 2;
                            checkList.RemoveRange(0, checkList.Count());
                        }
                    }
                }
            }
            for (int i = 0; i < 5; i++) // 7 wall
            {
                bool check = true;
                int a = random.Next(1, 16);
                int b = random.Next(1, 46);
                int direction = random.Next(0, 2);
                List<string> checkList = new List<string>();
                
                while (check)
                {

                    if (direction == 0) // horizontal
                    {
                        //checkList.Append(board[a-1, b-1]);

                        for (int j = -1; j < 2; j++)
                        {
                            for (int k = -1; k < 8; k++)
                            {
                                if (j == 0)
                                {
                                    checkList.Add(board[a, b - 1]);
                                    checkList.Add(board[a, b + 3]);
                                }
                                else
                                {
                                    checkList.Add(board[a + j, b + k]);
                                }
                            }
                        }
                        if (!checkList.Contains("#"))
                        {
                            if (board[a, b] == " " && board[a, b+1] == " " && board[a, b+2] == " " && board[a, b + 3] == " " && board[a, b + 4] == " " && board[a, b + 5] == " " && board[a, b + 6] == " ")
                            {
                                board[a, b] = "#";
                                board[a, b+1] = "#";
                                board[a, b+2] = "#";
                                board[a, b+3] = "#";
                                board[a, b+4] = "#";
                                board[a, b+5] = "#";
                                board[a, b+6] = "#";
                                check = false;
                            }
                            else
                            {
                                a = random.Next(1, 22);
                                b = random.Next(1, 46);
                            }
                        }
                        else
                        {
                            a = random.Next(1, 22);
                            b = random.Next(1, 46);
                            checkList.RemoveRange(0, checkList.Count());
                        }
                    }
                    checkList.RemoveRange(0, checkList.Count());
                    if (direction == 1) // vertical
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            for (int k = -1; k < 8; k++)
                            {
                                if (j == 0)
                                {
                                    checkList.Add(board[a - 1, b]);
                                    checkList.Add(board[a + 3, b]);
                                }
                                else
                                {
                                    checkList.Add(board[a + k, b + j]);
                                }
                            }
                        }
                        if (!checkList.Contains("#"))
                        {
                            if (board[a, b] == " " && board[a+1, b] == " " && board[a+2, b] == " " && board[a + 3, b] == " " && board[a + 4, b] == " " && board[a + 5, b] == " " && board[a + 6, b] == " ")
                            {
                                board[a, b] = "#";
                                board[a+1, b] = "#";
                                board[a+2, b] = "#";
                                board[a+3, b] = "#";
                                board[a+4, b] = "#";
                                board[a+5, b] = "#";
                                board[a+6, b] = "#";
                                check = false;
                            }
                            else
                            {
                                a = random.Next(1, 16);
                                b = random.Next(1, 52);
                            }
                        }
                        else
                        {
                            a = random.Next(1, 16);
                            b = random.Next(1, 52);
                            checkList.RemoveRange(0, checkList.Count());
                        }
                    }
                }
            }

            for (int i = 0; i < 3; i++) // 11 wall
            {
                bool check = true;
                int a = random.Next(1, 12);
                int b = random.Next(1, 42);
                int direction = random.Next(0, 2);
                List<string> checkList = new List<string>();

                while (check)
                {

                    if (direction == 0) // horizontal
                    {
                        //checkList.Append(board[a-1, b-1]);

                        for (int j = -1; j < 2; j++)
                        {
                            for (int k = -1; k < 12; k++)
                            {
                                if (j == 0)
                                {
                                    checkList.Add(board[a, b - 1]);
                                    checkList.Add(board[a, b + 3]);
                                }
                                else
                                {
                                    checkList.Add(board[a + j, b + k]);
                                }
                            }
                        }
                        if (!checkList.Contains("#"))
                        {
                            if (board[a, b] == " " && board[a, b + 1] == " " && board[a, b + 2] == " " && board[a, b + 3] == " " && board[a, b + 4] == " " && board[a, b + 5] == " " && board[a, b + 6] == " " && board[a, b + 7] == " " && board[a, b + 8] == " " && board[a, b + 9] == " " && board[a, b + 10] == " ")
                            {
                                board[a, b] = "#";
                                board[a, b + 1] = "#";
                                board[a, b + 2] = "#";
                                board[a, b + 3] = "#";
                                board[a, b + 4] = "#";
                                board[a, b + 5] = "#";
                                board[a, b + 6] = "#";
                                board[a, b + 7] = "#";
                                board[a, b + 8] = "#";
                                board[a, b + 9] = "#";
                                board[a, b + 10] = "#";
                                check = false;
                            }
                            else
                            {
                                a = random.Next(1, 22);
                                b = random.Next(1, 42);
                            }
                        }
                        else
                        {
                            a = random.Next(1, 22);
                            b = random.Next(1, 42);
                            checkList.RemoveRange(0, checkList.Count());
                        }
                    }
                    checkList.RemoveRange(0, checkList.Count());
                    if (direction == 1) // vertical
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            for (int k = -1; k < 12; k++)
                            {
                                if (j == 0)
                                {
                                    checkList.Add(board[a - 1, b]);
                                    checkList.Add(board[a + 3, b]);
                                }
                                else
                                {
                                    checkList.Add(board[a + k, b + j]);
                                }
                            }
                        }
                        if (!checkList.Contains("#"))
                        {
                            if (board[a, b] == " " && board[a + 1, b] == " " && board[a + 2, b] == " " && board[a + 3, b] == " " && board[a + 4, b] == " " && board[a + 5, b] == " " && board[a + 6, b] == " " && board[a + 7, b] == " " && board[a + 8, b] == " " && board[a + 9, b] == " " && board[a + 10, b] == " ")
                            {
                                board[a, b] = "#";
                                board[a + 1, b] = "#";
                                board[a + 2, b] = "#";
                                board[a + 3, b] = "#";
                                board[a + 4, b] = "#";
                                board[a + 5, b] = "#";
                                board[a + 6, b] = "#";
                                board[a + 7, b] = "#";
                                board[a + 8, b] = "#";
                                board[a + 9, b] = "#";
                                board[a + 10, b] = "#";
                                check = false;
                            }
                            else
                            {
                                a = random.Next(1, 12);
                                b = random.Next(1, 52);
                            }
                        }
                        else
                        {
                            a = random.Next(1, 12);
                            b = random.Next(1, 52);
                            checkList.RemoveRange(0, checkList.Count());
                        }
                    }
                }
            }
            return board;

        }

        

    }
}
