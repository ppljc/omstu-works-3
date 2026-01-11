namespace lab4;

public class ThoughtList
{
    private Think[] thoughts;
    private int count;

    public ThoughtList(int capacity = 100)
    {
        thoughts = new Think[capacity];
        count = 0;
    }

    public void Push(Think thought)
    {
        if (count < thoughts.Length)
        {
            thoughts[count] = thought;
            count++;
        }
    }

    public int Count => count;

    public Think this[int index]
    {
        get
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException();
            return thoughts[count - 1 - index];
        }
    }
}