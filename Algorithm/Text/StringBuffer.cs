using Algorithm.Datastructure;

namespace Algorithm.Text
{
    public class StringBuffer
    {
        List<char> buffer;

        public StringBuffer()
        {
            buffer = new List<char>(0);
        }

        public StringBuffer(string text)
        {
            buffer = new List<char>(text.Length * 2 + 1);

            foreach (char c in text)
                buffer.Add(c);            
        }

        public StringBuffer(int capacity)
        {
            buffer = new List<char>(capacity: capacity);
        }

        public void Append(char c)
        {
            buffer.Add(c);
        }

        public void Append(char c, int repeat)
        {
            for (int i = 0; i < repeat; i++)
                buffer.Add(c);
        }

        public void Append(object value)
        {
            if (!(value is null))
                Append(value.ToString());
        }

        public void Append(string text)
        {
            foreach (char c in text)
                Append(c);
        }

        public void AppendLine(char c)
        {
            buffer.Add(c);
            buffer.Add('\n');
        }

        public void AppendLine(object value)
        {
            if (!(value is null))
                AppendLine(value.ToString());
        }

        public void AppendLine(string text)
        {
            foreach (char c in text)
                Append(c);
            Append("\n");
        }

        public override string ToString()
        {
            return string.Join("", buffer);
        }
    }
}
