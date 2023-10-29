using System;
using System.Collections;

namespace Algorithm.DataStructure
{
    public static class Sorts
    {
        /// <summary>
        /// 선택 정렬, 시간복잡도 : N^2
        /// </summary>
        public static void SelectionSort<T>(T[] array)
        {
            if (array.Length < 2)            
                return;

            if (!(array[0] is IComparable))
                throw new Exception("IComparable is not implemented.");

            IComparer comp = Comparer.Default;

            int index = 0;
            T min; // 최솟값

            for (int i = 0; i < array.Length; i++)
            {
                min = Mathf.Max(comp, array);
                // 초기 최솟값은 그 배열의 최댓값 또는 입력될 수 중 가장
                // 큰 수에서 1 큰 수로 정해둔다.

                for (int j = i; j < array.Length; j++)
                    if (comp.Compare(min, array[j]) >= 0)
                        (min, index) = (array[j], j);

                // 배열을 순환하면 최솟값을 찾는다.
                // 그 값의 인덱스 값을 얻어온다.

                (array[i], array[index]) = (array[index], array[i]);
                // 찾은 최솟값과 i번째 값의 위치를 바꿔준다.
            }
        }

        /// <summary>
        /// 버블 정렬, 시간 복잡도 : N^2
        /// </summary>
        public static void BobbleSort<T>(T[] array)
        {
            if (array.Length < 2)
                return;

            if (!(array[0] is IComparable))
                throw new Exception("IComparable is not implemented.");

            IComparer comp = Comparer.Default;

            int i, j;

            for (i = 1; i < array.Length; i++)
                for (j = 0; j < array.Length - i; j++)
                    if (comp.Compare(array[j], array[j + 1]) >= 0)

                        // j번째 값과 j+1번째 값중에서 j + 1번째 값이 작으면
                        // 위치를 바꿔준다.
                        // 왼쪽부터 작은 순으로 정렬된다.

                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
        }

        /// <summary>
        /// 삽입 정렬, 시간 복잡도 : 최선의 경우 N, 최악의 경우 N^2
        /// </summary>
        public static void InsertionSort<T>(T[] array)
        {
            if (array.Length < 2)
                return;

            if (!(array[0] is IComparable))
                throw new Exception("IComparable is not implemented.");

            IComparer comp = Comparer.Default;

            int i, j;

            // 배열을 돌면서 키 값을 결정한다.
            for (i = 0; i < array.Length - 1; i++)
            {
                j = i;
                // 키 값은 인덱스가 0보다 작고, 키 값의 오른쪽 값과 비교해
                // 만약에 오른쪽 값보다 클 경우 두 값의 위치를 바꾼다.
                while (j >= 0 && comp.Compare(array[j], array[j + 1]) >= 0)
                {
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);

                    // 바꾼 후에는 한 칸 뒤로 이동해서 다시 비교한다.
                    j--;
                }
            }
        }

        /// <summary>
        /// 퀵 정렬, 시간 복잡도 : 평균 NlogN, 최악의 경우 N^2
        /// </summary>
        public static void QuickSort<T>(T[] array)
        {
            if (array.Length < 2)
                return;

            if (!(array[0] is IComparable<T>))
                throw new Exception("IComparable is not implemented.");

            IComparer comp = Comparer.Default;

            QuickSort(0, array.Length - 1);

            void QuickSort(int start, int end)
            {
                if (start >= end) return;

                // 리스트에 있는 한 값은 피벗(pivot)으로 정한다.
                int key = start, i = start + 1, j = end;

                while (i <= j)
                {
                    // 피벗보다 작은 값을 찾는다.
                    while (i <= end && comp.Compare(array[i], array[key]) <= 0) i++;

                    // 피벗보다 큰 값을 찾는다.
                    while (comp.Compare(array[j], array[key]) >= 0 && j > start) j--;

                    // 만약 작은 값의 인덱스와 큰 값의 인덱스가 교차 될 때
                    // 교차된 큰 값의 인덱스와 피벗의 인덱스를 바꿔준다. 
                    if (i > j)
                        (array[j], array[key]) = (array[key], array[j]);
                    // 작은 값과 큰 값의 위치를 바꿔준다.
                    else
                        (array[j], array[i]) = (array[i], array[j]);
                }


                // 피벗 값을 제외한 나머지 부분에 다시 퀵 정렬를 한다.
                QuickSort(start, j - 1);
                QuickSort(j + 1, end);
            }
        }

        /// <summary>
        /// 병합 정렬, 시간 복잡도 : NlogN
        /// </summary>
        public static void MergeSort<T>(T[] array)
        {
            if (array.Length < 2)
                return;

            if (!(array[0] is IComparable))
                throw new Exception("IComparable is not implemented.");

            IComparer comp = Comparer.Default;

            // 임시 배열을 선언
            T[] sorted = new T[array.Length + 1];

            Sort(0, array.Length - 1);

            void Merge(int left, int mid, int right)
            {
                int i = left, j = mid + 1, k = left, l = 0;

                // 분할 정렬된 배열을 합치기
                while (i <= mid && j <= right)
                {
                    if (comp.Compare(array[i], array[j]) <= 0)
                        sorted[k++] = array[i++];
                    else
                        sorted[k++] = array[j++];
                }

                // 남아 있던 값들도 복사
                if (i > mid)
                    for (l = j; l <= right; l++)
                        sorted[k++] = array[l];
                else
                    for(l = i; l <= mid; l++)
                        sorted[k++] = array[l];

                // 임시 배열에 저장한 값을 원본 배열에 다시 복사
                for (l = left; l <= right; l++)
                    array[l] = sorted[l];                
            }

            void Sort(int left, int right)
            {
                if (array.Length > 1 && left < right)
                {
                    int mid = Mathf.Mid(left, right);
                    Sort(left, mid); // 중앙을 기준으로 나눠 정렬
                    Sort(mid + 1, right);
                    Merge(left, mid, right); // 마지막으로 정렬된 두 배열을 합치기
                }
            }           
        }

        /// <summary>
        /// 힙 정렬, 시간 복잡도 : NlogN
        /// </summary>
        public static void HeapSort<T>(T[] array)
        {
            if (array.Length < 2)
                return;

            if (!(array[0] is IComparable))
                throw new Exception("IComparable is not implemented.");

            IComparer comp = Comparer.Default;

            for (int i = 1; i < array.Length; i++)
            {
                int c = i;
                do
                {
                    int root = (c - 1) / 2;

                    if (comp.Compare(array[root], array[c]) < 0)
                        (array[root], array[c]) = (array[c], array[root]);

                    c = root;
                } while (c != 0);
            }

            for (int i = array.Length - 1; i >= 0; i--)
            {
                (array[0], array[i]) = (array[i], array[0]);
                int root = 0, c = 1;

                do
                {
                    c = 2 * root + 1;

                    if (c < i - 1 && comp.Compare(array[c], array[c + 1]) < 0)
                        c++;

                    if (c < i && comp.Compare(array[root], array[c]) < 0)
                        (array[root], array[c]) = (array[c], array[root]);

                    root = c;
                } while (c < i);
            }

        }

        public static void Measure<T>(Action<T[]> sort, T[] args)
        {
            Util.Start();

            sort(args);

            Util.Print(Util.Stop() + "ms");
        }
    }
}
