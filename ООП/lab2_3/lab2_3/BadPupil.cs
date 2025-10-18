using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_3
{
    public sealed class BadPupil : Pupil
    {
        public override int GetCurrentGrade
        {
            get
            {
                int chance = rand.Next(100);
                return chance < 70 ? 2 : 3;
            }
        }

        public override void Study() => Console.WriteLine("Двоечник ленится учиться.");
        public override void Read() => Console.WriteLine("Двоечник редко читает.");
        public override void Write() => Console.WriteLine("Двоечник пишет неразборчиво.");
        public override void Relax() => Console.WriteLine("Двоечник всё время отдыхает.");
    }
}
