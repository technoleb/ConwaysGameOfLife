using System;

namespace ConwaysGameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            int runs = 0;
            int height, width, maxRuns;
            
            // Get the INput Values 
            Console.Write("Enter Board Height : ");
            int.TryParse(Console.ReadLine(), out height);

            Console.Write("Enter Board Width : ");
            int.TryParse(Console.ReadLine(), out width);

            Console.Write("Enter Number of Generation : ");
            int.TryParse(Console.ReadLine(), out maxRuns);

            // Validate the values and if values are not int, then exit from program
            if ( height == 0  || width == 0 || maxRuns == 0)
            {
                Console.Write("Value for Height, Width and NUmbe of Generation must be greater then 0");
                Console.ReadLine();
                return;

            }
            // Display a message for  Board
            GameOfLife gm = new GameOfLife(height, width);
            Console.WriteLine("\r");
            Console.Write("Generating a Bord of {0} X {1}, will run for {2}", height.ToString(), width.ToString(), maxRuns.ToString());
            Console.WriteLine("\r");
            // Run the Program
            while (runs++ < maxRuns)
            {
                gm.StartGame();
                System.Threading.Thread.Sleep(1000);
            }

            Console.ReadKey();

        }
    }

    public class GameOfLife
    {
        private int Heigth;
        private int Width;
        private bool[,] cells;
        public GameOfLife(int Heigth, int Width)
        {
            this.Heigth = Heigth;
            this.Width = Width;
            cells = new bool[Heigth, Width];
            GenerateBoard();
        }

        //Create a Game Board with specific height and Widtha and randomly decide where to spot Dots at initially
        private void GenerateBoard()
        {
            Random generator = new Random();
            int number;
            for (int i = 0; i < Heigth; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    number = generator.Next(2);
                    cells[i, j] = ((number == 0) ? false : true);
                }
            }
        }

        public void StartGame()
        {
            DrawDoard();
            Start();
        }

        // Draw a Board based on Initial Board Generated, will display X in stead of Dots here
        private void DrawDoard()
        {
            for (int i = 0; i < Heigth; i++)
            {
                for (int j = 0; j < Width; j++)
                {

                    if (j == 0)
                    {
                        Console.Write(cells[i, j] ? "| x |" : "|   |");
                    }
                    else
                    {
                        Console.Write(cells[i, j] ? " x |" : "   |");
                    }
                    if (j == Width - 1) Console.WriteLine("\r");
                }
                for (int j = 0; j < Width; j++)
                {

                    Console.Write("----");
                }
                Console.WriteLine("\r");
            }
            Console.SetCursorPosition(0, Console.WindowTop + 5);
        }
        // Generate Board for next accurance
        private void Start()
        {
            for (int i = 0; i < Heigth; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    int availableBlocks = GetNext(i, j);

                    if (cells[i, j])
                    {
                        if (availableBlocks < 2)
                        {
                            cells[i, j] = false;
                        }

                        if (availableBlocks > 3)
                        {
                            cells[i, j] = false;
                        }
                    }
                    else
                    {
                        if (availableBlocks == 3)
                        {
                            cells[i, j] = true;
                        }
                    }
                }
            }
        }
        private int GetNext(int x, int y)
        {
            int availableBlocks = 0;

            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (!((i < 0 || j < 0)
                            || (i >= Heigth || j >= Width)))
                    {
                        if (cells[i, j] == true) availableBlocks++;
                    }
                }
            }
            return availableBlocks;
        }

    }
}
