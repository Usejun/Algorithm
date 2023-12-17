namespace Algorithm.Datastructure
{        
    //기본 노드 클래스
    public abstract class Node<T>
    {
        public T Value;

        public Node(T value)
        {
            Value = value;
        }
    }                           
}
