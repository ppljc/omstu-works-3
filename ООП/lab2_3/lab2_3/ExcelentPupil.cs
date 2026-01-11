using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_3
{
    public sealed class ExcelentPupil : Pupil
    {
        public override int GetCurrentGrade
        {
            get
            {
                int chance = rand.Next(100);
                return chance < 80 ? 5 : 4;
            }
        }

        public override void Study() => Console.WriteLine("Отличник учится с энтузиазмом.");
        public override void Read() => Console.WriteLine("Отличник много читает.");
        public override void Write() => Console.WriteLine("Отличник пишет без ошибок.");
        public override void Relax() => Console.WriteLine("Отличник редко отдыхает.");
    }
}
