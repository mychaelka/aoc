using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace aoc2023 {
    public class Cubes {
        private static Dictionary<string, int> bag= new() 
        {
            {"blue", 14},
            {"red", 12},
            {"green", 13}
        };
        private readonly IEnumerable<String> lines; 
        private int currentGameIndex;
        
        public Cubes(string filepath) {
            this.lines = File.ReadAllLines(filepath);
        }

        public static int GetGameIndex(string line) {
            Regex regex = new(@"\d+");
            Match match = regex.Match(line);

            return int.Parse(match.Value);
        }

        public string[] ParseConfiguration(string configuration) {
            char[] delimiters = {':', ';'};
            string[] pulls = configuration.Split(delimiters);

            currentGameIndex = GetGameIndex(pulls[0]);
            pulls = pulls.Skip(1).ToArray();

            return pulls;      
        }

        public bool ConfigurationPossible(string[] pulls) {
            foreach (string pull in pulls) {
                string[] cubes = pull.Split(',');

                foreach (string cube in cubes) {
                    KeyValuePair<string, int> numColor = GetNumAndColor(cube);

                    if (Cubes.bag[numColor.Key] < numColor.Value) {
                        return false;
                    } 
                }
            }

            return true;
        }

        public int PossibleConfigurations() {
            int numPossible = 0;

            foreach (string line in this.lines) {

                string[] pulls = ParseConfiguration(line);

                if (ConfigurationPossible(pulls)) {
                    numPossible += currentGameIndex;
                }
            }

            return numPossible;
        }

        public KeyValuePair<string, int> GetNumAndColor(string cubesOfOneColor) {
            string[] numColor = cubesOfOneColor.Split(' ');
            int num = Int32.Parse(numColor[1]);
            string color = numColor[2];

            return new KeyValuePair<string, int>(color, num);

        }

        public int FewestPossibleCubes() {
            int powerSum = 0;
            Dictionary<string, int> fewestPossible = [];

            foreach (string line in this.lines) {
                fewestPossible["blue"] = 0;
                fewestPossible["green"] = 0;
                fewestPossible["red"] = 0;

                string[] pulls = ParseConfiguration(line);

                foreach (string pull in pulls) {
                    string[] cubes = pull.Split(',');

                    foreach (string cube in cubes) {
                        KeyValuePair<string, int> numColor = GetNumAndColor(cube);

                        if (numColor.Value > fewestPossible[numColor.Key]) {
                            fewestPossible[numColor.Key] = numColor.Value;
                        }
                    }
                }

                int power = 1;
                foreach (int number in fewestPossible.Values) {
                    power *= number;
                }

                powerSum += power;

            }

            return powerSum;
        }
    }
}