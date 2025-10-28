namespace lab4;

public class ExpulsionList
{
    private Student[] _students;
    private int _count;

    public ExpulsionList(int capacity = 100)
    {
        _students = new Student[capacity];
        _count = 0;
    }

    public void Add(Student student)
    {
        if (_count < _students.Length)
        {
            _students[_count] = student;
            _count++;
        }
    }

    public int Count => _count;

    public Student this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException();
            return _students[index];
        }
    }
}