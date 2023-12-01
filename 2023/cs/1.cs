using System;
using System.IO;

public class CalibrationDocument {
    private string inputPath;
    
    public CalibrationDocument(string inputPath) {
        this.inputPath = inputPath;
    }

    public static int ReadInput(string filepath) {
        int calibrationValue = 0;
        char firstNum;
        char lastNum;
        string line = "";
        StreamReader reader = new StreamReader(filepath)
        while ((line = reader.ReadLine()) != null) {
            for (int i = 0; i < line.Length; i++) {
                char current = line[i];
                
                if (current >= 48 && current <= 57) {
                    firstNum = current;
                    break;
                }
            }

            for (int i = line.Length - 1; i >= 0; i--) {
                char current = line[i];
                
                if (current >= 48 && current <= 57) {
                    lastNum = current;
                    break;
                }
            }
        }

        calibrationValue = 10 * (firstNum - 48) + (lastNum - 48);

        return calibrationValue;
    
    }

    public static void Main() {
        calibrationValue = this.ReadInput(inputPath);
        Console.WriteLine(String.Format("Calibration value: {0}", calibrationValue));
    }
}