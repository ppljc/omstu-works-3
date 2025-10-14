using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_3
{
    class ClassRoom
    {
        public static int StudentCount { get; private set; }
        private List<Pupil> pupils = new List<Pupil>();

        public ClassRoom(params Pupil[] inputPupils)
        {
            pupils.AddRange(inputPupils);

            while (pupils.Count < 4)
            {
                pupils.Add(new Pupil());
            }

            StudentCount = pupils.Count;
        }

        public double GetRoundGrade
        {
            get
            {
                return pupils.Average(p => p.GetCurrentGrade);
            }
        }

        public void ShowInfo()
        {
            int i = 1;
            foreach (var p in pupils)
            {
                Console.WriteLine($"Ученик {i}: Оценка {p.GetCurrentGrade}");
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
