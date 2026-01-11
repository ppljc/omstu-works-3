using System;

namespace lab2_3
{
    class Program
    {
        static void Main()
        {
            var classRoom = new ClassRoom(
                new ExcelentPupil(),
                new GoodPupil(),
                new BadPupil()
            );

            classRoom.ShowInfo();

            Console.WriteLine($"\nКоличество учеников: {ClassRoom.StudentCount}");
        }
    }
}
