using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_3
{
    public sealed class GoodPupil : Pupil
    {
        public override int GetCurrentGrade
        {
            get
            {
                int chance = rand.Next(100);
                if (chance < 60) return 4;
                else if (chance < 90) return 5;
                else return 3;
            }
        }

        public override void Study() => Console.WriteLine("Хорошист старается учиться.");
        public override void Read() => Console.WriteLine("Хорошист читает регулярно.");
        public override void Write() => Console.WriteLine("Хорошист пишет аккуратно.");
        public override void Relax() => Console.WriteLine("Хорошист отдыхает после дела.");
    }
}
