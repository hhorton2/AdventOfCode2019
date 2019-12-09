using System.Linq;

namespace AdventOfCode2019.Intcode
{
    public class IntcodeComputer
    {
        public void Compute(IntcodeState state)
        {
            var currentInstructionLength = 4;
            while (true)
            {
                var op = GetOpCode(state.Memory[state.InstructionPointer]);
                var parameterModes = GetParameterModes(state.Memory[state.InstructionPointer]);
                if (op == 99)
                {
                    state.Halted = true;
                    break;
                }


                switch (op)
                {
                    case 1:
                        currentInstructionLength = Add(state, parameterModes);
                        break;
                    case 2:
                        currentInstructionLength = Multiply(state, parameterModes);
                        break;
                    case 3:
                        currentInstructionLength =
                            Input(state, state.Input?.Dequeue() ?? 0);
                        break;
                    case 4:
                        currentInstructionLength = Output(state, parameterModes);
                        if (state.BreakOnOutput)
                        {
                            state.InstructionPointer += currentInstructionLength;
                            return;
                        }

                        break;
                    case 5:
                        currentInstructionLength = JumpTrue(state, parameterModes);
                        break;
                    case 6:
                        currentInstructionLength = JumpFalse(state, parameterModes);
                        break;
                    case 7:
                        currentInstructionLength = LessThan(state, parameterModes);
                        break;
                    case 8:
                        currentInstructionLength = Equals(state, parameterModes);
                        break;
                }

                state.InstructionPointer += currentInstructionLength;
            }
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

        private static int Add(IntcodeState state, int[] parameterModes)
        {
            var parameterOne = state.Memory[state.InstructionPointer + 1];
            var parameterTwo = state.Memory[state.InstructionPointer + 2];
            var parameterThree = state.Memory[state.InstructionPointer + 3];
            var inputTwoMode = parameterModes.Length >= 2 ? parameterModes[^2] : 0;
            var inputOneMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            var inputOneValue = inputOneMode == 0 ? state.Memory[parameterOne] : parameterOne;
            var inputTwoValue = inputTwoMode == 0 ? state.Memory[parameterTwo] : parameterTwo;

            state.Memory[parameterThree] = inputOneValue + inputTwoValue;

            return 4;
        }

        private static int Multiply(IntcodeState state, int[] parameterModes)
        {
            var parameterOne = state.Memory[state.InstructionPointer + 1];
            var parameterTwo = state.Memory[state.InstructionPointer + 2];
            var parameterThree = state.Memory[state.InstructionPointer + 3];
            var inputTwoMode = parameterModes.Length >= 2 ? parameterModes[^2] : 0;
            var inputOneMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            var inputOneValue = inputOneMode == 0 ? state.Memory[parameterOne] : parameterOne;
            var inputTwoValue = inputTwoMode == 0 ? state.Memory[parameterTwo] : parameterTwo;

            state.Memory[parameterThree] = inputOneValue * inputTwoValue;

            return 4;
        }

        private static int Input(IntcodeState state, int input)
        {
            var outputLoc = state.Memory[state.InstructionPointer + 1];
            state.Memory[outputLoc] = input;
            return 2;
        }

        private static int Output(IntcodeState state, int[] parameterModes)
        {
            var pointer = state.InstructionPointer + 1;
            var outputLoc = state.Memory[pointer];
            var outputMode = parameterModes.Length > 0 ? parameterModes[0] : 0;
            var outputValue = outputMode == 0 ? state.Memory[outputLoc] : outputLoc;
            state.Output.Add(outputValue.ToString());
            return 2;
        }

        private static int JumpTrue(IntcodeState state, int[] parameterModes)
        {
            var parameterOne = state.Memory[state.InstructionPointer + 1];
            var parameterTwo = state.Memory[state.InstructionPointer + 2];
            var outputMode = parameterModes.Length >= 2 ? parameterModes[^2] : 0;
            var inputMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            var inputValue = inputMode == 0 ? state.Memory[parameterOne] : parameterOne;
            var outputValue = outputMode == 0 ? state.Memory[parameterTwo] : parameterTwo;
            if (inputValue == 0)
            {
                return 3;
            }

            state.InstructionPointer = outputValue;
            return 0;
        }

        private static int JumpFalse(IntcodeState state, int[] parameterModes)
        {
            var parameterOne = state.Memory[state.InstructionPointer + 1];
            var parameterTwo = state.Memory[state.InstructionPointer + 2];
            var writeMode = parameterModes.Length >= 2 ? parameterModes[^2] : 0;
            var inputMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            var inputValue = inputMode == 0 ? state.Memory[parameterOne] : parameterOne;
            var writeValue = writeMode == 0 ? state.Memory[parameterTwo] : parameterTwo;
            if (inputValue == 0)
            {
                state.InstructionPointer = writeValue;
                return 0;
            }

            return 3;
        }

        private static int LessThan(IntcodeState state, int[] parameterModes)
        {
            var parameterOne = state.Memory[state.InstructionPointer + 1];
            var parameterTwo = state.Memory[state.InstructionPointer + 2];
            var parameterThree = state.Memory[state.InstructionPointer + 3];
            var inputTwoMode = parameterModes.Length >= 2 ? parameterModes[^2] : 0;
            var inputOneMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            var inputOneValue = inputOneMode == 0 ? state.Memory[parameterOne] : parameterOne;
            var inputTwoValue = inputTwoMode == 0 ? state.Memory[parameterTwo] : parameterTwo;

            state.Memory[parameterThree] = inputOneValue < inputTwoValue ? 1 : 0;

            return 4;
        }

        private static int Equals(IntcodeState state, int[] parameterModes)
        {
            var parameterOne = state.Memory[state.InstructionPointer + 1];
            var parameterTwo = state.Memory[state.InstructionPointer + 2];
            var parameterThree = state.Memory[state.InstructionPointer + 3];
            var inputTwoMode = parameterModes.Length >= 2 ? parameterModes[^2] : 0;
            var inputOneMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            var inputOneValue = inputOneMode == 0 ? state.Memory[parameterOne] : parameterOne;
            var inputTwoValue = inputTwoMode == 0 ? state.Memory[parameterTwo] : parameterTwo;

            state.Memory[parameterThree] = inputOneValue == inputTwoValue ? 1 : 0;

            return 4;
        }
    }
}