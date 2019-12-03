using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Day03
{
    public class DayThreeSolver : ISolver
    {
        public string PartOneSolve(string input)
        {
            var lines = input.Split("\n");
            var wireOneInstructions = lines[0].Split(",");
            var wireTwoInstructions = lines[1].Split(",");
            var wireOneCoords = new HashSet<Coord>();
            var wireTwoCoords = new HashSet<Coord>();
            var currentX = 0;
            var currentY = 0;
            foreach (var instruction in wireOneInstructions)
            {
                var direction = instruction.Substring(0, 1);
                var moveAmount = int.Parse(instruction.Substring(1));
                Move(direction, moveAmount, ref currentX, ref currentY, wireOneCoords);
            }

            currentX = 0;
            currentY = 0;
            foreach (var instruction in wireTwoInstructions)
            {
                var direction = instruction.Substring(0, 1);
                var moveAmount = int.Parse(instruction.Substring(1));
                Move(direction, moveAmount, ref currentX, ref currentY, wireTwoCoords);
            }

            wireOneCoords.IntersectWith(wireTwoCoords);
            return wireOneCoords
                .Select(c => Math.Abs(c.X) + Math.Abs(c.Y))
                .Min()
                .ToString();
        }


        public string PartTwoSolve(string input)
        {
            throw new NotImplementedException();
        }

        private static void Move(string direction, int moveAmount, ref int currentX, ref int currentY,
            ISet<Coord> coords)
        {
            switch (direction)
            {
                case "R":
                    MoveRight(moveAmount, ref currentX, ref currentY, coords);
                    break;
                case "L":
                    MoveLeft(moveAmount, ref currentX, ref currentY, coords);
                    break;
                case "U":
                    MoveUp(moveAmount, ref currentX, ref currentY, coords);
                    break;
                default:
                    MoveDown(moveAmount, ref currentX, ref currentY, coords);
                    break;
            }
        }

        private static void MoveRight(int moveAmount, ref int currentX, ref int currentY, ISet<Coord> coords)
        {
            for (var i = 0; i < moveAmount; i++)
            {
                currentX++;
                coords.Add(new Coord {X = currentX, Y = currentY});
            }
        }

        private static void MoveLeft(int moveAmount, ref int currentX, ref int currentY, ISet<Coord> coords)
        {
            for (var i = 0; i < moveAmount; i++)
            {
                currentX--;
                coords.Add(new Coord {X = currentX, Y = currentY});
            }
        }

        private static void MoveUp(int moveAmount, ref int currentX, ref int currentY, ISet<Coord> coords)
        {
            for (var i = 0; i < moveAmount; i++)
            {
                currentY++;
                coords.Add(new Coord {X = currentX, Y = currentY});
            }
        }

        private static void MoveDown(int moveAmount, ref int currentX, ref int currentY, ISet<Coord> coords)
        {
            for (var i = 0; i < moveAmount; i++)
            {
                currentY--;
                coords.Add(new Coord {X = currentX, Y = currentY});
            }
        }
    }
}