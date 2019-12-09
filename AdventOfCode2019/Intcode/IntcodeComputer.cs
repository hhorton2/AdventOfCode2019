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
                            Input(state, parameterModes, state.Input?.Dequeue() ?? 0);
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
                    case 9:
                        currentInstructionLength = AdjustRelativeOffset(state, parameterModes);
                        break;
                }

                state.InstructionPointer += currentInstructionLength;
            }
        }

        private static long GetOpCode(long rawInstruction)
        {
            if (rawInstruction < 100)
            {
                return rawInstruction;
            }

            var instructionString = rawInstruction.ToString();
            return int.Parse(instructionString.Substring(instructionString.Length - 2));
        }

        private static int[] GetParameterModes(long rawInstruction)
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
            var outputLocRaw = state.Memory[state.InstructionPointer + 3];
            var outputMode = parameterModes.Length >= 3 ? parameterModes[^3] : 0;
            var inputTwoMode = parameterModes.Length >= 2 ? parameterModes[^2] : 0;
            var inputOneMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            var inputOneValue = GetParameterValue(state, inputOneMode, parameterOne);
            var inputTwoValue = GetParameterValue(state, inputTwoMode, parameterTwo);
            var outputLocVal = GetOutputParameterValue(state, outputMode, outputLocRaw);

            state.Memory[outputLocVal] = inputOneValue + inputTwoValue;

            return 4;
        }

        private static int Multiply(IntcodeState state, int[] parameterModes)
        {
            var parameterOne = state.Memory[state.InstructionPointer + 1];
            var parameterTwo = state.Memory[state.InstructionPointer + 2];
            var outputLocRaw = state.Memory[state.InstructionPointer + 3];
            var outputMode = parameterModes.Length >= 3 ? parameterModes[^3] : 0;
            var inputTwoMode = parameterModes.Length >= 2 ? parameterModes[^2] : 0;
            var inputOneMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            var inputOneValue = GetParameterValue(state, inputOneMode, parameterOne);
            var inputTwoValue = GetParameterValue(state, inputTwoMode, parameterTwo);
            var outputLocVal = GetOutputParameterValue(state, outputMode, outputLocRaw);

            state.Memory[outputLocVal] = inputOneValue * inputTwoValue;

            return 4;
        }

        private static int Input(IntcodeState state, int[] parameterModes, int input)
        {
            var outputMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            var outputLocRaw = state.Memory[state.InstructionPointer + 1];
            var outputLocVal = GetOutputParameterValue(state, outputMode, outputLocRaw);
            state.Memory[outputLocVal] = input;
            return 2;
        }

        private static int Output(IntcodeState state, int[] parameterModes)
        {
            var pointer = state.InstructionPointer + 1;
            var outputLoc = state.Memory[pointer];
            var outputMode = parameterModes.Length > 0 ? parameterModes[0] : 0;
            var outputValue = GetParameterValue(state, outputMode, outputLoc);
            state.Output.Add(outputValue.ToString());
            return 2;
        }

        private static int JumpTrue(IntcodeState state, int[] parameterModes)
        {
            var parameterOne = state.Memory[state.InstructionPointer + 1];
            var parameterTwo = state.Memory[state.InstructionPointer + 2];
            var outputMode = parameterModes.Length >= 2 ? parameterModes[^2] : 0;
            var inputMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            var inputValue = GetParameterValue(state, inputMode, parameterOne);
            var outputValue = GetParameterValue(state, outputMode, parameterTwo);
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
            var inputValue = GetParameterValue(state, inputMode, parameterOne);
            var writeValue = GetParameterValue(state, writeMode, parameterTwo);
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
            var outputLocRaw = state.Memory[state.InstructionPointer + 3];
            var outputMode = parameterModes.Length >= 3 ? parameterModes[^3] : 0;
            var inputTwoMode = parameterModes.Length >= 2 ? parameterModes[^2] : 0;
            var inputOneMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            var inputOneValue = GetParameterValue(state, inputOneMode, parameterOne);
            var inputTwoValue = GetParameterValue(state, inputTwoMode, parameterTwo);
            var outputLocVal = GetOutputParameterValue(state, outputMode, outputLocRaw);

            state.Memory[outputLocVal] = inputOneValue < inputTwoValue ? 1 : 0;

            return 4;
        }

        private static int Equals(IntcodeState state, int[] parameterModes)
        {
            var parameterOne = state.Memory[state.InstructionPointer + 1];
            var parameterTwo = state.Memory[state.InstructionPointer + 2];
            var outputLocRaw = state.Memory[state.InstructionPointer + 3];
            var outputMode = parameterModes.Length >= 3 ? parameterModes[^3] : 0;
            var inputTwoMode = parameterModes.Length >= 2 ? parameterModes[^2] : 0;
            var inputOneMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            var inputOneValue = GetParameterValue(state, inputOneMode, parameterOne);
            var inputTwoValue = GetParameterValue(state, inputTwoMode, parameterTwo);
            var outputLocVal = GetOutputParameterValue(state, outputMode, outputLocRaw);

            state.Memory[outputLocVal] = inputOneValue == inputTwoValue ? 1 : 0;

            return 4;
        }

        private static int AdjustRelativeOffset(IntcodeState state, int[] parameterModes)
        {
            var parameterOne = state.Memory[state.InstructionPointer + 1];
            var inputMode = parameterModes.Length >= 1 ? parameterModes[^1] : 0;
            var inputValue = GetParameterValue(state, inputMode, parameterOne);

            state.RelativeBase += inputValue;

            return 2;
        }


        private static long GetParameterValue(IntcodeState state, int parameterMode, long parameter)
        {
            switch (parameterMode)
            {
                case 0:
                    if (!state.Memory.ContainsKey(parameter))
                    {
                        state.Memory.Add(parameter, 0);
                    }

                    return state.Memory[parameter];
                case 1:
                    return parameter;
                case 2:
                    if (!state.Memory.ContainsKey(parameter + state.RelativeBase))
                    {
                        state.Memory.Add(parameter + state.RelativeBase, 0);
                    }

                    return state.Memory[parameter + state.RelativeBase];
                default:
                    return state.Memory[parameter];
            }
        }

        private static long GetOutputParameterValue(IntcodeState state, int outputMode, long outputLocRaw)
        {
            long outputLocVal;
            switch (outputMode)
            {
                case 0:
                    if (!state.Memory.ContainsKey(outputLocRaw))
                    {
                        state.Memory.Add(outputLocRaw, 0);
                    }

                    outputLocVal = outputLocRaw;
                    break;
                case 1:
                    outputLocVal = outputLocRaw;
                    break;
                default:
                    outputLocVal = outputLocRaw + state.RelativeBase;
                    break;
            }

            return outputLocVal;
        }
    }
}