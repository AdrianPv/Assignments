using System;
using System.Collections.Generic;

namespace BrothersInTheBar
{
    class Program
    {
        static void Main(string[] args)
        {
            var glasses = new[] { 1, 1, 2, 3, 3, 3, 2, 2, 1, 1 };

            int maxRounds = GetMaxRounds(glasses);
            Console.WriteLine($"brothersInTheBar(glasses) = {maxRounds}");
        }

        private static int GetMaxRounds(int[] glassesArray)
        {
            int length = glassesArray.Length;
            bool[] finishedGlasses = new bool[length];
            int currentSequenceLength = 1;
            int rounds = 0;

            for (int i = 1; i < length; i++)
            {
                if (finishedGlasses[i] == false && finishedGlasses[i - 1] == false)
                {
                    if (glassesArray[i] == glassesArray[i - 1])
                    {
                        currentSequenceLength++;

                        if (currentSequenceLength == 3)
                        {
                            finishedGlasses[i] = true;
                            finishedGlasses[i - 1] = true;
                            finishedGlasses[i - 2] = true;
                            rounds++;
                            currentSequenceLength = 1;

                            //scan neighbours recursively
                            if (i + 2 < glassesArray.Length)
                            {
                                rounds += ScanBackwards(i, 2, glassesArray, finishedGlasses);
                            }
                            else if (i + 1 < glassesArray.Length)
                            {
                                rounds += ScanBackwards(i, 1, glassesArray, finishedGlasses);
                            }

                        }
                    }
                    else
                    {
                        currentSequenceLength = 1;
                    }
                }
            }

            return rounds;
        }

        private static int ScanBackwards(int i, int n, int[] glassesArray, bool[] finishedGlasses)
        {
            Stack<int> backwardsIndexes = new Stack<int>();
            int rounds = 0;

            for (int j = i + n; j >= 0; j--)
            {
                if (finishedGlasses[j] == false)
                {
                    if (backwardsIndexes.Count == 0)
                    {
                        backwardsIndexes.Push(j);
                    }
                    else
                    {
                        if (glassesArray[j] == glassesArray[backwardsIndexes.Peek()])
                        {
                            backwardsIndexes.Push(j);

                            if (backwardsIndexes.Count == 3)
                            {
                                rounds++;
                                finishedGlasses[backwardsIndexes.Pop()] = true;
                                finishedGlasses[backwardsIndexes.Pop()] = true;
                                int k = backwardsIndexes.Pop(); // last finished glass index
                                finishedGlasses[k] = true;

                                if (k + 2 < glassesArray.Length)
                                {
                                    rounds += ScanBackwards(k, 2, glassesArray, finishedGlasses);
                                }
                                else if (k + 1 < glassesArray.Length)
                                {
                                    rounds += ScanBackwards(k, 1, glassesArray, finishedGlasses);
                                }
                            }
                        }
                        else
                        {
                            //stops pointless scanning
                            return rounds;
                        }
                    }
                }
            }

            return rounds;
        }
    }
}
