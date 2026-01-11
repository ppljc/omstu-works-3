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
    class ClassRoomTests
    {
        [TestCase(2, 4)]
        [TestCase(4, 4)]
        [TestCase(6, 4)]
        public void Test_Constructor(int inputCount, int expectedCount)
        {
            List<Pupil> pupilsList = [];

            for (int i = 0; i < inputCount; i++)
            {
                Pupil pupil = new();
                pupilsList.Add(pupil);
            }

            Pupil[] pupils = [.. pupilsList];

            _ = new ClassRoom(pupils);
            int actualCount = ClassRoom.StudentCount;

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [Test]
        public void Test_GetRoundGrades()
        {
            var room = new ClassRoom(
                new ExcelentPupil(),
                new GoodPupil(),
                new BadPupil(),
                new GoodPupil()
            );

            double avg = room.GetRoundGrade;

            Assert.That(avg, Is.InRange(2.0, 5.0));
        }

        [Test]
        public void Test_StaticStudentCount()
        {
            var room1 = new ClassRoom(new Pupil());
            var room2 = new ClassRoom(new Pupil(), new Pupil(), new Pupil());

            Assert.That(ClassRoom.StudentCount, Is.EqualTo(4));
        }

        [Test]
        public void Test_ShowInfo()
        {
            ClassRoom classroom = new ClassRoom(
                new ExcelentPupil(),
                new GoodPupil(),
                new BadPupil()
            );

            using var sw = new StringWriter();
            Console.SetOut(sw);

            classroom.ShowInfo();

            string output = sw.ToString();

            Assert.That(output, Does.Contain("Отличник"));
            Assert.That(output, Does.Contain("Хорошист"));
            Assert.That(output, Does.Contain("Двоечник"));
            Assert.That(output, Does.Contain("Ученик"));

            Assert.That(output, Does.Contain("учится"));
            Assert.That(output, Does.Contain("читает"));
            Assert.That(output, Does.Contain("пишет"));
            Assert.That(output, Does.Contain("отдыхает"));
        }
    }
}
