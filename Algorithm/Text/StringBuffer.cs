using Algorithm.Datastructure;

namespace Algorithm.Text
{
    public class StringBuffer
    {
        public int Length => buffer.Count;

        List<char> buffer;
        string pattern;
        int[] pi;
        List<int> patternPos;

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

        public void Clear()
        {
            buffer = new List<char>();
            pattern = "";
            pi = null;
            patternPos = new List<int>();
        }

        public int IndexOf(string pattern)
        {
            if (this.pattern != pattern)
                SetPattern(pattern);

            return IndexByPattern();
        }        

        public bool Remove(string pattern)
        {
            if (IndexOf(pattern) == -1) return false;

            for (int i = 0; i < pattern.Length; i++)
                buffer.RemoveAt(patternPos[0]);

            patternPos.RemoveAt(0);
            return true;
        }

        public void Replace(string oldString, string newString)
        {
            if (pattern != oldString)
                SetPattern(oldString);

            int indexDiff = 0;

            foreach (int index in patternPos)
            {
                Replace(index - indexDiff, oldString.Length, newString);
                indexDiff += oldString.Length - newString.Length;
            }
        }

        private void Replace(int startIndex, int length, string newString)
        {
            int lengthDiff = length - newString.Length;

            if (length == lengthDiff)
            {
                for (int i = 0; i < length; i++)                
                    buffer.RemoveAt(startIndex);                
            }
            else if (lengthDiff < 0)
            {
                int i = 0;
                for (; i < length; i++)                
                    buffer[startIndex + i] = newString[i];

                for (int j = 0; j < -lengthDiff; j++)
                    buffer.Insert(startIndex + i, newString[i + j]);
            }
            else if (lengthDiff == 0)
            {
                for (int i = 0; i < length; i++)                
                    buffer[startIndex + i] = newString[i];                
            }
            else
            {
                int i = 0, len = newString.Length;                
                for (; i < len; i++)
                    buffer[startIndex + i] = newString[i];

                for (int j = 0; j < lengthDiff; j++)
                    buffer.RemoveAt(startIndex + i);
            }
        }
        
        public void SetPattern(string pattern)
        {
            this.pattern = pattern;
            int len = pattern.Length;
            patternPos = new List<int>();
            pi = new int[Length];
            int j = 0;

            for (int i = 1; i < len; i++)
            {
                while (j > 0 && pattern[i] != pattern[j])
                    j = pi[j - 1];
                if (pattern[i] == pattern[j])
                    pi[i] = ++j;
            }

            j = 0;

            for (int i = 0; i < Length; i++)
            {
                while (j > 0 && buffer[i] != pattern[j])
                    j = pi[j - 1];
                if (buffer[i] == pattern[j])
                    if (j == len - 1)
                        patternPos.Add(i - len + 1);
                    else j++;
            }
        }

        private int IndexByPattern()
        {
            return patternPos.Count == 0 ? -1 : patternPos[0];
        }

        public override string ToString()
        {
            return string.Join("", buffer);
        }

        public bool Equals(StringBuffer sb)
        {
            if (Length != sb.Length) return false;
            for (int i = 0; i < Length; i++)
                if (buffer[i] != sb[i])
                    return false;
            return true;            
        }

        public bool Equals(string str)
        {
            if (Length != str.Length) return false;
            for (int i = 0; i < Length; i++)
                if (buffer[i] != str[i])
                    return false;
            return true;
        }

        public char this[int index]
        {
            get => buffer[index];
            set => buffer[index] = value;
        }        
    }
}
