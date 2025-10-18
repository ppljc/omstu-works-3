using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_3
{
    public class ClassRoom
    {
        public static int StudentCount { get; private set; }

        private readonly List<Pupil> pupils = [];
        private readonly Dictionary<Pupil, int> fixedGrades = new();

        public ClassRoom(params Pupil[] inputPupils)
        {
            if (inputPupils.Length > 4)
            {
                pupils.AddRange(inputPupils.Take(4));
            }
            else
            {
                pupils.AddRange(inputPupils);
                while (pupils.Count < 4)
                {
                    pupils.Add(new Pupil());
                }
            }

            foreach (var p in pupils)
            {
                fixedGrades[p] = p.GetCurrentGrade;
            }

            StudentCount = pupils.Count;
        }

        public double GetRoundGrade
        {
            get
            {
                return fixedGrades.Values.Average();
            }
        }

        public void ShowInfo()
        {
            int i = 1;
            foreach (var p in pupils)
            {
                int grade = fixedGrades[p];
                Console.WriteLine($"Ученик {i}: Оценка {grade}");
                p.Study();
                p.Read();
                p.Write();
                p.Relax();
                Console.WriteLine();
                i++;
            }
            Console.WriteLine($"Средний балл по классу: {GetRoundGrade:F2}");
        }
    }
}
