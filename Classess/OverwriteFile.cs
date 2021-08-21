using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MassTextModifier.Classess
{
    public class textModifier
    {
        public static void OverwriteFile(string filePath)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();

            List<int> ABindexList = new List<int>();
            int iterIndex = 0;
            foreach (string line in lines)
            {
                iterIndex++;
                if (line.Contains("SP ("))
                {
                    ABindexList.Add(iterIndex);
                }
            }

            int insertIndex = ABindexList[ABindexList.Count - 1] - 1;

            if (insertIndex > 0)
            {
                lines.Insert(insertIndex, "CALL BN_NestKontur ()");
                lines.Insert(insertIndex, "CALL BN_TrennerInnenAussen ()");
            }

            File.WriteAllLines(filePath, lines);
        }
    }
}
