using System;
using System.IO;
using System.Collections.Generic;

namespace aoc2023 {

    public class CalibrationDocument {

        private static Dictionary<string, int> wordsToNums = new()
        {
            {"one", 1},
            {"two", 2},
            {"three", 3},
            {"four", 4},
            {"five", 5},
            {"six", 6},
            {"seven", 7},
            {"eight", 8},
            {"nine", 9}
        };

        public static HashSet<int> FindSubstringIndices(string source, string substring)
        {
            HashSet<int> indices = new HashSet<int>();
            if (String.IsNullOrEmpty(substring))
            {
                return indices;
            }

            int index = 0;
            while ((index = source.IndexOf(substring, index, StringComparison.OrdinalIgnoreCase)) != -1)
            {
                indices.Add(index);
                index += 1;
            }

            return indices;
        }


        public static int GetCalibrationValue(string filepath) {
            int calibrationValue = 0;
            int firstNum = 0;
            int lastNum = 0;
            String? line = "";
            
            using (StreamReader reader = new(filepath)) {
                while ((line = reader.ReadLine()) != null) {

                    int substringFirstIndex = int.MaxValue;
                    int substringLastIndex = -1;
                    int min_idx = -1;
                    int max_idx = -1;

                    foreach (string key in wordsToNums.Keys) {
                        
                        HashSet<int> indices = FindSubstringIndices(line, key);
                        if (indices.Count > 0) {
                            min_idx = indices.Min();
                            max_idx = indices.Max();
                        }
                        
                        if (min_idx < substringFirstIndex && min_idx >= 0) {
                            substringFirstIndex = min_idx;
                            firstNum = wordsToNums[key];
                        }
                        
                        if (max_idx > substringLastIndex && max_idx >= 0) {
                            substringLastIndex = max_idx;
                            lastNum = wordsToNums[key];
                        }
                    }

                    for (int i = 0; i < line.Length; i++) {
                        char current = line[i];
                        
                        if (Char.IsNumber(current)) {
                            if (substringFirstIndex == -1 || (substringFirstIndex >= 0 && i < substringFirstIndex)) {
                                firstNum = current - 48;
                                break;
                            }
                        }
                    }

                    for (int i = line.Length - 1; i >= 0; i--) {
                        char current = line[i];
                        
                        if (Char.IsNumber(current) && (i > substringLastIndex || substringLastIndex == -1)) {
                            lastNum = current - 48;
                            break;
                        }
                    }
                    
                    calibrationValue += 10 * firstNum + lastNum;
                }
            }

            return calibrationValue;
        
        }
    }
}