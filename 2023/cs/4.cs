using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace aoc2023 {
    
    public class ScratchCards {
        private double score = 0;
        private int numCards = 0;

        private Dictionary<int, int> cards = Enumerable
                                             .Range(1, 198)
                                             .ToDictionary(key => key, value => 1);

        public double GetScore() {
            return this.score;
        }

        public int GetNumCards() {
            return this.numCards;
        }

        public void ReadCards(string input) {
            char[] delimiters = [':', '|'];
            string[] lines = File.ReadAllLines(input);
            int i = 1;

            foreach (string line in lines) {
                string[] splits = line.Split(delimiters);

                List<int> scratchCard = splits[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                 .Select(int.Parse)
                                                 .ToList();
                List<int> myCard = splits[2].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                 .Select(int.Parse)
                                                 .ToList();
                
                IEnumerable<int> commonNums = scratchCard.Intersect(myCard);
                int common = commonNums.Count();
                
                if (common > 0) {
                    double cardScore = Math.Pow(2, (common - 1));
                    score += cardScore;

                    for (int k = 0; k < this.cards[i]; k++) {
                        for (int j = 1; j <= common; j++) {
                            if (this.cards.ContainsKey(i + j)) {
                                cards[i+j] += 1;
                            } else {
                                cards[i+j] = 1;
                            }
                        }
                    }
                }

                i += 1;
            }
        }

        public void CalculateNumCards() {
            foreach (int entry in this.cards.Keys) {
                this.numCards += cards[entry];
            }
        }
    }
}