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
            var lines = input.Split("\n");
            var wireOneInstructions = lines[0].Split(",");
            var wireTwoInstructions = lines[1].Split(",");
            var wireOneCoords = new HashSet<Coord>();
            var wireTwoCoords = new HashSet<Coord>();
            var currentX = 0;
            var currentY = 0;
            var steps = 0;
            foreach (var instruction in wireOneInstructions)
            {
                var direction = instruction.Substring(0, 1);
                var moveAmount = int.Parse(instruction.Substring(1));
                Move(direction, moveAmount, ref currentX, ref currentY, wireOneCoords, ref steps);
            }

            currentX = 0;
            currentY = 0;
            steps = 0;
            foreach (var instruction in wireTwoInstructions)
            {
                var direction = instruction.Substring(0, 1);
                var moveAmount = int.Parse(instruction.Substring(1));
                Move(direction, moveAmount, ref currentX, ref currentY, wireTwoCoords, ref steps);
            }

            var oneIntersects = wireOneCoords.Intersect(wireTwoCoords).ToList();
            var twoIntersects = wireTwoCoords.Intersect(wireOneCoords).ToList();
            var coordDict = new Dictionary<Coord, int>();
            oneIntersects.ForEach(c => 
            {
                coordDict.Add(c, c.Steps);
            });
            twoIntersects.ForEach(c => { coordDict[c] = coordDict[c] + c.Steps; });
            return coordDict.Values.Min().ToString();
        }

        private static void Move(string direction, int moveAmount, ref int currentX, ref int currentY,
            ISet<Coord> coords)
        {
            var steps = 0;
            Move(direction, moveAmount, ref currentX, ref currentY, coords, ref steps);
        }

        private static void Move(string direction, int moveAmount, ref int currentX, ref int currentY,
            ISet<Coord> coords, ref int steps)
        {
            switch (direction)
            {
                case "R":
                    MoveRight(moveAmount, ref currentX, ref currentY, coords, ref steps);
                    break;
                case "L":
                    MoveLeft(moveAmount, ref currentX, ref currentY, coords, ref steps);
                    break;
                case "U":
                    MoveUp(moveAmount, ref currentX, ref currentY, coords, ref steps);
                    break;
                default:
                    MoveDown(moveAmount, ref currentX, ref currentY, coords, ref steps);
                    break;
            }
        }

        private static void MoveRight(int moveAmount, ref int currentX, ref int currentY, ISet<Coord> coords,
            ref int steps)
        {
            for (var i = 0; i < moveAmount; i++)
            {
                currentX++;
                steps++;
                coords.Add(new Coord {X = currentX, Y = currentY, Steps = steps});
            }
        }

        private static void MoveLeft(int moveAmount, ref int currentX, ref int currentY, ISet<Coord> coords,
            ref int steps)
        {
            for (var i = 0; i < moveAmount; i++)
            {
                currentX--;
                steps++;
                coords.Add(new Coord {X = currentX, Y = currentY, Steps = steps});
            }
        }

        private static void MoveUp(int moveAmount, ref int currentX, ref int currentY, ISet<Coord> coords,
            ref int steps)
        {
            for (var i = 0; i < moveAmount; i++)
            {
                currentY++;
                steps++;
                coords.Add(new Coord {X = currentX, Y = currentY, Steps = steps});
            }
        }

        private static void MoveDown(int moveAmount, ref int currentX, ref int currentY, ISet<Coord> coords,
            ref int steps)
        {
            for (var i = 0; i < moveAmount; i++)
            {
                currentY--;
                steps++;
                coords.Add(new Coord {X = currentX, Y = currentY, Steps = steps});
            }
        }
    }
}