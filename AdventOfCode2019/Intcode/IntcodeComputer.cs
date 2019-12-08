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
                        currentInstructionLength = Input(memory, instructionPointer, input.Value);
                        break;
                    case 4:
                        currentInstructionLength = Output(memory, instructionPointer, output, parameterModes);
                        break;
                    case 5:
                        currentInstructionLength = JumpTrue(memory, ref instructionPointer, parameterModes);
                        break;
                    case 6:
                        currentInstructionLength = JumpFalse(memory, ref instructionPointer, parameterModes);
                        break;
                    case 7:
                        currentInstructionLength = LessThan(memory, instructionPointer, parameterModes);
                        break;
                    case 8:
                        currentInstructionLength = Equals(memory, instructionPointer, parameterModes);
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

            var instructionString = rawInstruction.ToString().ToCharArray();
            return instructionString[..^2]
                .Select(c => int.Parse(c.ToString()))
                .ToArray();
        }

        private static int Add(int[] memory, int instructionPointer, int[] parameterModes)
        {
            var parameterOne = memory[instructionPointer + 1];
            var parameterTwo = memory[instructionPointer + 2];
            var parameterThree = memory[instructionPointer + 3];
            var inputTwoMode = parameterModes.Length >= 2 ? parameterModes[^2] : 0;
            var inputOneMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            var inputOneValue = inputOneMode == 0 ? memory[parameterOne] : parameterOne;
            var inputTwoValue = inputTwoMode == 0 ? memory[parameterTwo] : parameterTwo;

            memory[parameterThree] = inputOneValue + inputTwoValue;

            return 4;
        }

        private static int Multiply(int[] memory, int instructionPointer, int[] parameterModes)
        {
            var parameterOne = memory[instructionPointer + 1];
            var parameterTwo = memory[instructionPointer + 2];
            var parameterThree = memory[instructionPointer + 3];
            var inputTwoMode = parameterModes.Length >= 2 ? parameterModes[^2] : 0;
            var inputOneMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            var inputOneValue = inputOneMode == 0 ? memory[parameterOne] : parameterOne;
            var inputTwoValue = inputTwoMode == 0 ? memory[parameterTwo] : parameterTwo;

            memory[parameterThree] = inputOneValue * inputTwoValue;

            return 4;
        }

        private static int Input(int[] memory, int instructionPointer, int input)
        {
            var outputLoc = memory[instructionPointer + 1];
            memory[outputLoc] = input;
            return 2;
        }

        private static int Output(int[] memory, int instructionPointer, List<string> output, int[] parameterModes)
        {
            var pointer = instructionPointer + 1;
            var outputLoc = memory[pointer];
            var outputMode = parameterModes.Length > 0 ? parameterModes[0] : 0;
            var outputValue = outputMode == 0 ? memory[outputLoc] : outputLoc;
            output.Add(outputValue.ToString());
            return 2;
        }

        private static int JumpTrue(int[] memory, ref int instructionPointer, int[] parameterModes)
        {
            var parameterOne = memory[instructionPointer + 1];
            var parameterTwo = memory[instructionPointer + 2];
            var outputMode = parameterModes.Length >= 2 ? parameterModes[^2] : 0;
            var inputMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            var inputValue = inputMode == 0 ? memory[parameterOne] : parameterOne;
            var outputValue = outputMode == 0 ? memory[parameterTwo] : parameterTwo;
            if (inputValue == 0)
            {
                return 3;
            }

            instructionPointer = outputValue;
            return 0;
        }

        private static int JumpFalse(int[] memory, ref int instructionPointer, int[] parameterModes)
        {
            var parameterOne = memory[instructionPointer + 1];
            var parameterTwo = memory[instructionPointer + 2];
            var writeMode = parameterModes.Length >= 2 ? parameterModes[^2] : 0;
            var inputMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            var inputValue = inputMode == 0 ? memory[parameterOne] : parameterOne;
            var writeValue = writeMode == 0 ? memory[parameterTwo] : parameterTwo;
            if (inputValue == 0)
            {
                instructionPointer = writeValue;
                return 0;
            }

            return 3;
        }

        private static int LessThan(int[] memory, int instructionPointer, int[] parameterModes)
        {
            var parameterOne = memory[instructionPointer + 1];
            var parameterTwo = memory[instructionPointer + 2];
            var parameterThree = memory[instructionPointer + 3];
            var inputTwoMode = parameterModes.Length >= 2 ? parameterModes[^2] : 0;
            var inputOneMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            var inputOneValue = inputOneMode == 0 ? memory[parameterOne] : parameterOne;
            var inputTwoValue = inputTwoMode == 0 ? memory[parameterTwo] : parameterTwo;

            memory[parameterThree] = inputOneValue < inputTwoValue ? 1 : 0;

            return 4;
        }

        private static int Equals(int[] memory, int instructionPointer, int[] parameterModes)
        {
            var parameterOne = memory[instructionPointer + 1];
            var parameterTwo = memory[instructionPointer + 2];
            var parameterThree = memory[instructionPointer + 3];
            var inputTwoMode = parameterModes.Length >= 2 ? parameterModes[^2] : 0;
            var inputOneMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            var inputOneValue = inputOneMode == 0 ? memory[parameterOne] : parameterOne;
            var inputTwoValue = inputTwoMode == 0 ? memory[parameterTwo] : parameterTwo;

            memory[parameterThree] = inputOneValue == inputTwoValue ? 1 : 0;

            return 4;
        }
    }
}