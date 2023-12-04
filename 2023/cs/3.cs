using System;
using System.IO;
using System.Collections.Generic;

namespace aoc2023 {
    public class Engine {
        private List<char[]> engineMatrix = [];
        private Dictionary<(int, int), List<int>> symbolsToNums = [];

        public List<char[]> GetEngineMatrix() {
            return engineMatrix;
        }

        public Dictionary<(int, int), List<int>> GetsymbolsToNums() {
            return symbolsToNums;
        }
        
        public void ReadEngineMatrix(string filepath) {
            string[] input = File.ReadAllLines(filepath);

            for (int i = 0; i < input.Length; i++) {
                char[] row = [.. input[i]];
                this.engineMatrix.Add(row);
            }
        }
        
        public bool AdjacentToSymbol(int digitRow, int digitCol, List<char[]> matrix) {
            
            int[] rowOffsets = [-1, -1, -1, 0, 0, 1, 1, 1];
            int[] colOffsets = [-1, 0, 1, -1, 1, -1, 0, 1];

            for (int i = 0; i < 8; i++) {
                int newRow = digitRow + rowOffsets[i];
                int newCol = digitCol + colOffsets[i];

                if (newRow >= 0 && newRow < matrix.Count && newCol >= 0 && newCol < matrix[newRow].Length) {
                    char neighbor = matrix[newRow][newCol];

                    if (neighbor != '.' && !Char.IsDigit(neighbor)) {
                        return true;
                    }
                }
            }

            return false;
        }

        public int GetGearSum(List<char[]> matrix) {
            int num = 0;
            HashSet<(int, int)> neighborSymbols = [];
            int[] rowOffsets = [-1, -1, -1, 0, 0, 1, 1, 1];
            int[] colOffsets = [-1, 0, 1, -1, 1, -1, 0, 1];

            for (int i = 0; i < matrix.Count; i++) {
                for (int j = 0; j < matrix[i].Length; j++) {
                    
                    if (char.IsDigit(matrix[i][j])) {
                        
                        for (int k = 0; k < 8; k++) {
                            int newRow = i + rowOffsets[k];
                            int newCol = j + colOffsets[k];

                            if (newRow >= 0 && newRow < matrix.Count && newCol >= 0 && newCol < matrix[newRow].Length) {
                                neighborSymbols.Add((newRow, newCol));
                            }
                        }

                        num = num * 10 + (matrix[i][j] - '0');
                        continue;
                    }

                    if (num != 0 && neighborSymbols.Count > 0) {
                        foreach (var (row, col) in neighborSymbols) {
                            if (!symbolsToNums.ContainsKey((row, col))) {
                                symbolsToNums[(row, col)] = new List<int>();
                            }
                            symbolsToNums[(row, col)].Add(num);
                        }
                    }

                    num = 0;
                    neighborSymbols.Clear();
                }
            }

            int gearSum = 0;
            foreach (var key in symbolsToNums.Keys) {
                if (matrix[key.Item1][key.Item2] == '*' && symbolsToNums[key].Count == 2) {
                    gearSum += symbolsToNums[key][0] * symbolsToNums[key][1];
                }
            }

            return gearSum;

        }

        public int SumOfParts(List<char[]> matrix) {
            int sum = 0;

            for (int i = 0; i < matrix.Count; i++) {
                int currentNum = 0;
                bool currentNumAdjacent = false;
                
                for (int j = 0; j < matrix[i].Length; j++) {                  
                    if (Char.IsDigit(matrix[i][j])) {
                        currentNum = 10 * currentNum + (matrix[i][j] - '0');
                        
                        if (AdjacentToSymbol(i, j, matrix)) {
                            currentNumAdjacent = true;
                        }

                    } else {
                        if (currentNumAdjacent) {
                            sum += currentNum;
                        }
                        currentNum = 0;
                        currentNumAdjacent = false;
                    }

                    // Check for a number at the end of the line
                    if (j == matrix[i].Length - 1 && currentNumAdjacent) {
                        sum += currentNum;
                    }
                }
            }

            return sum;
        }

    }
}