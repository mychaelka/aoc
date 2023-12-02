using System;
using System.Collections.Generic;
using System.IO;


namespace aoc2023 {
    
    public class Outputs {
        public static void Main() {

            // Day 1
            int calibrationValue = CalibrationDocument.GetCalibrationValue("../input1a.txt");
            Console.WriteLine("Day 1:");
            Console.WriteLine(String.Format("Calibration value: {0}", calibrationValue));

            // Day 2
            Cubes game = new("../input2.txt");
            int possible = game.PossibleConfigurations();
            int powerSum = game.FewestPossibleCubes();
            Console.WriteLine("Day 2:");
            Console.WriteLine(String.Format("Number of possible configurations: {0}", possible));
            Console.WriteLine(String.Format("Sum of powers is: {0}", powerSum));
        }
    }
}

