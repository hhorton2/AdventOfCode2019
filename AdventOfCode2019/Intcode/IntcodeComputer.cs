using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Intcode
{
    public class IntcodeComputer
    {
        public IEnumerable<string> Compute(int[] memory, int? input = null)
        {
            var output = new List<string>();
            var instructionPointer = 0;
            var currentInstructionLength = 4;
            while (true)
            {
                var op = GetOpCode(memory[instructionPointer]);
                var parameterModes = GetParameterModes(memory[instructionPointer]);
                if (op == 99)
                {
                    break;
                }


                switch (op)
                {
                    case 1:
                        currentInstructionLength = Add(memory, instructionPointer, parameterModes);
                        break;
                    case 2:
                        currentInstructionLength = Multiply(memory, instructionPointer, parameterModes);
                        break;
                    case 3:
                        if (input != null)
                        {
                            currentInstructionLength = Input(memory, instructionPointer, input.Value);
                        }

                        break;
                    case 4:
                        currentInstructionLength = Output(memory, instructionPointer, output);
                        break;
                }

                instructionPointer += currentInstructionLength;
            }

            return output;
        }

        private static int GetOpCode(int rawInstruction)
        {
            if (rawInstruction < 100)
            {
                return rawInstruction;
            }

            var instructionString = rawInstruction.ToString();
            return int.Parse(instructionString.Substring(instructionString.Length - 2));
        }

        private static int[] GetParameterModes(int rawInstruction)
        {
            if (rawInstruction < 100)
            {
                return new int[0];
            }

            var instructionString = rawInstruction.ToString();
            return instructionString.Substring(0, instructionString.Length - 2)
                .Split("")
                .Select(int.Parse)
                .ToArray();
        }

        private static int Add(int[] memory, int instructionPointer, int[] parameterModes)
        {
            var parameterOne = memory[instructionPointer + 1];
            var parameterTwo = memory[instructionPointer + 2];
            var parameterThree = memory[instructionPointer + 3];
            var outputMode = parameterModes.Length == 3 ? parameterModes[^3] : 0;
            var inputTwoMode = parameterModes.Length >= 2 ? parameterModes[^2] : 0;
            var inputOneMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            if (outputMode == 1)
            {
                memory[memory[parameterThree]] =
                    (inputOneMode == 0 ? memory[parameterOne] : parameterOne)
                    +
                    (inputTwoMode == 0 ? memory[parameterTwo] : parameterTwo);
            }
            else
            {
                memory[parameterThree] =
                    (inputOneMode == 0 ? memory[parameterOne] : parameterOne)
                    +
                    (inputTwoMode == 0 ? memory[parameterTwo] : parameterTwo);
            }

            return 4;
        }

        private static int Multiply(int[] memory, int instructionPointer, int[] parameterModes)
        {
            var parameterOne = memory[instructionPointer + 1];
            var parameterTwo = memory[instructionPointer + 2];
            var parameterThree = memory[instructionPointer + 3];
            var outputMode = parameterModes.Length == 3 ? parameterModes[^3] : 0;
            var inputTwoMode = parameterModes.Length >= 2 ? parameterModes[^2] : 0;
            var inputOneMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            if (outputMode == 1)
            {
                memory[memory[parameterThree]] =
                    (inputOneMode == 0 ? memory[parameterOne] : parameterOne)
                    *
                    (inputTwoMode == 0 ? memory[parameterTwo] : parameterTwo);
            }
            else
            {
                memory[parameterThree] =
                    (inputOneMode == 0 ? memory[parameterOne] : parameterOne)
                    *
                    (inputTwoMode == 0 ? memory[parameterTwo] : parameterTwo);
            }

            return 4;
        }

        private static int Input(int[] memory, int instructionPointer, int input)
        {
            var outputLoc = memory[instructionPointer + 1];
            memory[outputLoc] = input;
            return 2;
        }

        private static int Output(int[] memory, int instructionPointer, List<string> output)
        {
            var outputLoc = memory[instructionPointer + 1];
            output.Add(memory[outputLoc].ToString());
            return 2;
        }
    }
}