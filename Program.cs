using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static int life = 5;
        static void Main(string[] args)
        {
            Player();
        }
        static void Printing(char[,] board) // setCursorPosition 
        {
            int cursorX = 1;
            int cursorY = 1;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i,j] == '0')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(cursorX + j, cursorX + i);
                        Console.Write(board[i, j]);
                        Console.ResetColor();
                    }
                    else if (board[i,j] == '#')
                    {
                        Console.ForegroundColor= ConsoleColor.Green;
                        Console.SetCursorPosition(cursorX + j, cursorX + i);
                        Console.Write(board[i, j]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.SetCursorPosition(cursorX + j, cursorX + i);
                        Console.Write(board[i, j]);
                    }
                    
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(55, 10);
            Console.WriteLine($"Life : {life} ");
        }
        static char[,] CreateWall(int size, int value, ref char[,] board)
        {
            Random random = new Random();
            for (int k = 0; k < value; k++) // The number of repetitions
            {
                int direction = random.Next(0, 2); // direction 
                while (true)
                {
                    int row, col;
                    int rowSize = 21 - size; // maximum right coordinate
                    int colSize = 51 - size; // maximum bottom coordinates
                    char[] checkList = new char[3 * size + 6];
                    if (direction == 0)  //vertical
                    {
                        row = random.Next(2, rowSize);
                        col = random.Next(2, 50);
                        for (int i = -1; i <= size; i++)
                        {
                            for (int j = -1; j < 2; j++)
                            {
                                checkList = checkList.Concat(new char[] { board[row + i, col + j] }).ToArray(); // add coordinates to the list
                            }
                        }
                    }
                    else // horizantal
                    {
                        row = random.Next(2, 20);
                        col = random.Next(2, colSize);
                        for (int i = -1; i <= size; i++)
                        {
                            for (int j = -1; j < 2; j++)
                            {
                                checkList = checkList.Concat(new char[] { board[row + j, col + i] }).ToArray();
                            }
                        }
                    }
                    if (!checkList.Contains('#')) // if it is not in the list
                    {
                        for (int i = 0; i < size; i++)
                        {
                            if (direction == 0) // vertical
                            {
                                board[row + i, col] = '#';
                            }
                            else // horizantal
                            {
                                board[row, col + i] = '#';
                            }
                        }
                        Array.Clear(checkList, 0, checkList.Length); // clear the the checkList
                        break;
                    }
                    else
                    {
                        Array.Clear(checkList, 0, checkList.Length);
                    }
                }
            }
            return board;
        }
        static char[,] CreateNumbers()
        {
            char[,] board = new char[23, 53];
            CreateWall(11, 3, ref board); // 11 tall wall
            CreateWall(7, 5, ref board); // 7 tall wall
            CreateWall(3, 20, ref board); // 3 tall wall
            Random random = new Random();
            for (int i = 0; i < 70; i++)
            {
                int row, col, number;
                number = random.Next(48, 58); // ascii value
                char numberChar = Convert.ToChar(number);
                row = random.Next(1, 22);
                col = random.Next(1, 52);
                bool checkNumber = true;
                while (checkNumber)
                {
                    if (board[row, col] == '\0') // if coordinate is null
                    {
                        board[row, col] = numberChar;
                        checkNumber = false;
                    }
                    else
                    {
                        row = random.Next(1, 22);
                        col = random.Next(1, 52);
                    }
                }
            }
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (i == 0 || i == 22 || j == 0 || j == 52)
                    {
                        board[i, j] = '#';
                    }
                    else if (board[i, j] == '\0')
                    {
                        board[i, j] = ' ';
                    }
                }
            }
            return board;
        }
        static void Player()
        {
            var board = CreateNumbers();
            Random random = new Random();
            bool controlPlayer = true;
            int row = random.Next(1, 21);
            int col = random.Next(1, 51);   
            while (controlPlayer) // Defining the position of P
            {
                if (board[row, col] == ' ')
                {
                    board[row, col] = 'P';
                    controlPlayer = false;
                }
                else
                {
                    row = random.Next(1, 21);
                    col = random.Next(1, 51);
                }
            }
            Printing(board); // first printing
            ConsoleKeyInfo cki;
            cki = Console.ReadKey(true);


            while (life > 0)  // game flow
                Pushing(cki, board, row, col);
            Console.SetCursorPosition(55,20);
            Console.WriteLine("game over");
        }
        static void Pushing(ConsoleKeyInfo cki, char[,] board, int row, int col)
        {
            int timeCounter = 0;
            while (life > 0 )
            {
                if (Console.KeyAvailable)
                {
                    cki = Console.ReadKey(true);
                    int size = 0;
                    int tempRow = 0;
                    int tempCol = 0;
                    int indexControlX = 0;
                    int indexControlY = 0;
                    if (cki.Key == ConsoleKey.LeftArrow)
                    {
                        size = col - 1;
                        tempCol = col - 1;
                        tempRow = row;
                        indexControlX = 0;
                        indexControlY = -1;
                    }
                    else if (cki.Key == ConsoleKey.RightArrow)
                    {
                        size = 51 - col;
                        tempCol = col + 1;
                        tempRow = row;
                        indexControlX = 0;
                        indexControlY = 1;
                    }
                    else if (cki.Key == ConsoleKey.UpArrow)
                    {
                        size = row - 1;
                        tempCol = col;
                        tempRow = row - 1;
                        indexControlX = -1;
                        indexControlY = 0;
                    }
                    else if (cki.Key == ConsoleKey.DownArrow)
                    {
                        size = 21 - row;
                        tempCol = col;
                        tempRow = row + 1;
                        indexControlX = 1;
                        indexControlY = 0;
                    }

                    char[] list = new char[size]; // pushing islemini yapacagimiz liste 
                    int elemanCount = 0;
                    for (int i = 0; i < list.Length; i++)
                    {
                        if (board[tempRow + (i * indexControlX), tempCol + (i * indexControlY)] != ' ' && board[tempRow + (i * indexControlX), tempCol + (i * indexControlY)] != '#')
                        {  // bir sonraki duvar bos veya duvar degilse 
                            list[i] = board[tempRow + (i * indexControlX), tempCol + (i * indexControlY)];// butun satiri vaya sutunun degerlerini alir ve onu listeye atar 
                            elemanCount = elemanCount + 1;
                        }
                        else
                            break;
                    }
                    for (int i = 1; i <= list.Length; i++)
                    {
                        if (list[(i - 1)] != '\0')
                        {
                            if (board[tempRow, tempCol] == '0')
                            {
                                life = Life(ref life);
                            }
                            if (board[tempRow + (elemanCount * indexControlX), tempCol + (elemanCount * indexControlY)] == '#' && IsSort(list) && list[1] != '\0')
                            {
                                board[row, col] = ' ';
                                row = tempRow;
                                col = tempCol;
                                board[row, col] = 'P';
                                if (i < list.Length)
                                {
                                    board[tempRow + (i * indexControlX), tempCol + (i * indexControlY)] = list[(i - 1)];
                                    board[tempRow + (elemanCount * indexControlX), tempCol + (elemanCount * indexControlY)] = '#';
                                    RandomNumberCreate(ref board);
                                }
                            }
                            else if (IsSort(list) && board[tempRow + (elemanCount * indexControlX), tempCol + (elemanCount * indexControlY)] != '#')
                            {
                                board[row, col] = ' ';
                                row = tempRow;
                                col = tempCol;
                                board[row, col] = 'P';
                                board[tempRow + (i * indexControlX), tempCol + (i * indexControlY)] = list[(i - 1)];
                            }
                        }
                        else
                            break;
                    }
                    if (board[tempRow, tempCol] == ' ') // boş deger olunca oynanan yer
                    {
                        board[row, col] = ' ';
                        row = tempRow;
                        col = tempCol;
                        board[row, col] = 'P';
                    }
                    //Console.Clear();
                    Printing(board);
                    Array.Clear(list, 0, list.Length);
                    timeCounter++;
                    if (timeCounter % 20 == 0)
                        board = ZeroMove(board);  // zero move every 1 second
                    if (timeCounter % 200 == 0)
                        board = Decrease(board); // decrase every 15 second
                }
            }
                
        }  
        static bool IsSort(char[] list)
        {
            bool value = false;
            int[] ints = new int[list.Length];
            for (int i = 0; i < list.Length; i++)
                ints[i] = Convert.ToInt16(list[i]);
            int temp = ints[0];
            for (int i = 1; i < list.Length; i++)
            {
                if (temp >= ints[i])
                {
                    value = true;
                    temp = ints[i];
                }
                else
                {
                    value = false;
                    break;
                }
            }
            return value;
        } // siralimi kontrolu
        static char[,] ZeroMove(char[,] board)
        {
            Random random = new Random();
            int tempRow = 0;
            int tempCol = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == '0')
                    {
                        while (true)
                        {                
                            int direction = random.Next(0, 5);
                            switch (direction)
                            {
                                case 0:
                                    tempRow = i;
                                    tempCol = j - 1;
                                    break;
                                case 1:
                                    tempRow = i;
                                    tempCol = j + 1;
                                    break;
                                case 2:
                                    tempRow = i - 1;
                                    tempCol = j;
                                    break;
                                case 3:
                                    tempRow = i + 1;
                                    tempCol = j;
                                    break;
                            }
                            if (board[tempRow, tempCol] == 'P')
                            {
                                life = Life(ref life);
                            }
                            else if (board[tempRow, tempCol] == ' ')
                            {
                                board[i, j] = ' ';
                                board[tempRow, tempCol] = '0';
                                break;
                            }
                            else
                                direction = random.Next(0, 5);
                        }
                    }                
                }
            }
            return board;
        } // zero move 
        static char[,] Decrease(char[,] board)
        {
            Random random = new Random();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i,j] != ' ' && board[i,j] != '#' && board[i,j] != '0' && board[i,j] != '1')
                        board[i,j] = Convert.ToChar(Convert.ToInt32(board[i,j]) -1);
                    else if (board[i,j] == '1')
                    {
                        int probility = random.Next(1, 101);
                        if (probility< 4)
                            board[i, j] = Convert.ToChar(Convert.ToInt32(board[i, j]) - 1);
                    }
                }
            }
            return board;
        } // sayilari azaltna 
        static void RandomNumberCreate(ref char[,] board) { 
            Random random = new Random();
            int row = random.Next(1, 22);
            int col = random.Next(1, 52);
            int number = random.Next(48,58);
            while (true)
            {
                if (board[row , col] == ' ')
                {
                    board[row,col] = Convert.ToChar(number);
                    break;
                }
                else
                {
                    row = random.Next(1, 22);
                    col = random.Next(1, 52);
                }
            }
        }
        static int Life(ref int life)
        {
            life--;
            Thread.Sleep(100);
            return life;
        }

    }
}
