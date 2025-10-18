using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using lab2_3;

namespace lab2_3.tests
{
    [TestFixture]
    class PupilTests
    {
        [Test]
        public void Test_GetCurrentGrade()
        {
            var pupils = new Pupil[]
            {
                new ExcelentPupil(),
                new GoodPupil(),
                new BadPupil()
            };

            foreach (var pupil in pupils)
            {
                for (int i = 0; i < 100; i++)
                {
                    int grade = pupil.GetCurrentGrade;
                    Assert.That(grade, Is.InRange(2, 5));
                }
            }
        }

        [TestCase(typeof(ExcelentPupil), 4.5, 5.0)]
        [TestCase(typeof(GoodPupil), 3.5, 5.0)]
        [TestCase(typeof(BadPupil), 2.0, 3.2)]
        public void Test_CurrentGrade(Type pupilType, double minExpected, double maxExpected)
        {
            var pupil = (Pupil)Activator.CreateInstance(pupilType);
            int sampleCount = 1000;
            double sum = 0;

            for (int i = 0; i < sampleCount; i++)
            {
                sum += pupil.GetCurrentGrade;
            }

            double avg = sum / sampleCount;

            Assert.That(avg, Is.InRange(minExpected, maxExpected));
        }
    }
}
