using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_3
{
    class Pupil
    {
        protected static Random rand = new Random();

        public virtual int GetCurrentGrade
        {
            get
            {
                return rand.Next(2, 6);
            }
        }

        public virtual void Study()
        {
            Console.WriteLine("Ученик старается учиться.");
        }

        public virtual void Read()
        {
            Console.WriteLine("Ученик читает учебник.");
        }

        public virtual void Write()
        {
            Console.WriteLine("Ученик пишет конспект.");
        }

        public virtual void Relax()
        {
            Console.WriteLine("Ученик отдыхает.");
        }
    }
}
