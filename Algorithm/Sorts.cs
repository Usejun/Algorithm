using System;
using System.Linq;

namespace Algorithm
{
    public static class Sorts
    {
        /// <summary>
        /// 선택 정렬, 시간복잡도 : N^2
        /// </summary>
        public static T[] SelectionSort<T>(T[] source)
        {
            T[] array = new T[source.Length];
            Array.Copy(source, array, source.Length);
            int index = 0;
            dynamic min; // 최솟값

            for (int i = 0; i < array.Length; i++)
            {
                min = (dynamic)array.Max() + 1;
                // 초기 최솟값은 그 배열의 최댓값 또는 입력될 수 중 가장
                // 큰 수에서 1 큰 수로 정해둔다.

                for (int j = i; j < array.Length; j++)
                    if (min > array[j])
                        (min, index) = (array[j], j);

                // 배열을 순환하면 최솟값을 찾는다.
                // 그 값의 인덱스 값을 얻어온다.

                (array[i], array[index]) = (array[index], array[i]);
                // 찾은 최솟값과 i번째 값의 위치를 바꿔준다.
            }

            return array;
        }

        /// <summary>
        /// 버블 정렬, 시간 복잡도 : N^2
        /// </summary>
        public static T[] BobbleSort<T>(T[] source)
        {
            T[] array = new T[source.Length];
            Array.Copy(source, array, source.Length);
            int i, j;

            for (i = 1; i < array.Length; i++)
            {
                for (j = 0; j < array.Length - i; j++)
                {
                    if ((dynamic)array[j] > array[j + 1])
                    {
                        // j번째 값과 j+1번째 값중에서 j번째 값이 작으면
                        // 위치를 바꿔준다.
                        // 왼쪽부터 작은 순으로 정렬된다.

                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    }
                }
            }

            return array;
        }

        /// <summary>
        /// 삽입 정렬, 시간 복잡도 : 최선의 경우 N, 최악의 경우 N^2
        /// </summary>
        public static T[] InsertionSort<T>(T[] source)
        {
            T[] array = new T[source.Length];
            Array.Copy(source, array, source.Length);
            int i, j;

            // 배열을 돌면서 키 값을 결정한다.
            for (i = 0; i < array.Length - 1; i++)
            {
                j = i;
                // 키 값은 인덱스가 0보다 작고, 키 값의 오른쪽 값과 비교해
                // 만약에 오른쪽 값보다 클 경우 두 값의 위치를 바꾼다.
                while (j >= 0 && (dynamic)array[j] > array[j + 1])
                {
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);

                    // 바꾼 후에는 한 칸 뒤로 이동해서 다시 비교한다.
                    j--;
                }
            }

            return array;
        }

        /// <summary>
        /// 퀵 정렬, 시간 복잡도 : 평균 NlogN, 최악의 경우 N^2
        /// </summary>
        public static T[] QuickSort<T>(T[] source)
        {
            T[] array = new T[source.Length];
            Array.Copy(source, array, source.Length);

            void QuickSort(int start, int end)
            {
                if (start >= end) return;

                // 리스트에 있는 한 값은 피벗(pivot)으로 정한다.
                int key = start, i = start + 1, j = end;

                while (i <= j)
                {
                    // 피벗보다 작은 값을 찾는다.
                    while (i <= end && (dynamic)array[i] <= array[key]) i++;

                    // 피벗보다 큰 값을 찾는다.
                    while ((dynamic)array[j] >= array[key] && j > start) j--;

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

            QuickSort(0, array.Length - 1);

            return array;
        }

        /// <summary>
        /// 병합 정렬, 시간 복잡도 : NlogN
        /// </summary>
        public static T[] MergeSort<T>(T[] source)
        {
            T[] array = new T[source.Length];
            Array.Copy(source, array, source.Length);

            void Sort(T[] _source, int low, int high)
            {
                // 배열의 크기가 0, 1일 경우에는 정렬된 배열로 처리된다.
                if (high - low < 2) return;

                // 배열를 균등하게 자르기 위해 중앙을 찾는다.
                int mid = Mathf.Mid(high, low);

                // 배열을 나누고 정렬한다.
                Sort(_source, 0, mid);
                Sort(_source, mid, high);
                Merge(_source, low, mid, high);
            }

            void Merge(T[] _source, int low, int mid, int high)
            {
                // 배열의 값을 담을 임시 배열을 만든다.   
                T[] _array = new T[high - low];
                int t = 0, l = low, h = mid;

                while (l < mid && h < high)
                {
                    // 두 개로 나눈 배열을 차례차례 비교해 합친다.
                    if ((dynamic)_source[l] < _source[h])
                        _array[t++] = _source[l++];
                    else
                        _array[t++] = _source[h++];
                }

                // 만약에 두 개로 나눈 배열 중에 먼저 순환이 끝난 배열이 있다면 
                // 아직 끝나지 않은 배열을 돌아준다.

                while (l < mid)
                    _array[t++] = _source[l++];

                while (h < high)
                    _array[t++] = _source[h++];

                // 마지막으로 정렬된 배열을 복사해준다.
                for (int i = low; i < high; i++)
                    _source[i] = _array[i - low];
            }

            Sort(array, 0, array.Length);

            return array;
        }

        public static T[] HeapSort<T>(T[] source)
        {
            T[] array = new T[source.Length];
            Array.Copy(source, array, source.Length);

            return array;
        }

        public static T[] TreeSort<T>(T[] source)
        {
            T[] array = new T[source.Length];
            Array.Copy(source, array, source.Length);

            return array;
        }
    }
}
