using System;
using static Algorithm.Utility;

namespace Algorithm
{
    public static class Graph<T>
    {
        static int[] dx = { 0, 0, 1, -1, 0, 0 };
        static int[] dy = { 1, -1, 0, 0, 0, 0 };
        static int[] dz = { 0, 0, 0, 0, 1, -1 };

        public static void Dfs(T[,] map, (int x, int y) point, (int x, int y) lengths,
                               Func<(int x, int y), bool> codition)
        {
            int[,] board = new int[lengths.y, lengths.x];
            bool[,] visit = new bool[lengths.y, lengths.x];

            Dfs(point);

            void Dfs((int x, int y) npoint)
            {
                visit[npoint.y, npoint.x] = true;

                for (int i = 0; i < 4; i++)
                {
                    int nx = npoint.x + dx[i];
                    int ny = npoint.y + dy[i];

                    if (nx < 0 || nx >= lengths.x || ny < 0 || ny >= lengths.y
                        || visit[ny, nx] || !codition((nx, ny)))
                        continue;

                    board[ny, nx] = board[npoint.y, npoint.x] + 1;
                    Dfs((nx, ny));
                }
            }

            Print(board);
        }

        public static void Bfs(T[,] map, (int x, int y) point, (int x, int y) lengths,
                               Func<(int x, int y), bool> codition)
        {
            int[,] board = new int[lengths.y, lengths.x];
            bool[,] visit = new bool[lengths.y, lengths.x];
            var q = new DataStructure.Queue<(int, int, int)>();

            q.Enqueue((point.x, point.y, 1));

            while (q.Count != 0)
            {
                (int x, int y, int t) = q.Dequeue();

                visit[y, x] = true;

                for (int i = 0; i < 4; i++)
                {
                    int nx = x + dx[i];
                    int ny = y + dy[i];

                    if (nx < 0 || nx >= lengths.x || ny < 0 || ny >= lengths.y
                        || visit[ny, nx] || !codition((nx, ny)))
                        continue;

                    board[ny, nx] = t;
                    q.Enqueue((nx, ny, t + 1));
                }
            }

            Print(board);
        }

        public static void Dfs(T[,,] map, (int x, int y, int z) point, (int x, int y, int z) lengths,
                               Func<(int x, int y, int z), bool> codition)
        {
            int[,,] board = new int[lengths.z, lengths.y, lengths.x];
            bool[,,] visit = new bool[lengths.z, lengths.y, lengths.x];

            Dfs(point);

            void Dfs((int x, int y, int z) npoint)
            {
                visit[npoint.z, npoint.y, npoint.x] = true;

                for (int i = 0; i < 6; i++)
                {
                    int nx = npoint.x + dx[i];
                    int ny = npoint.y + dy[i];
                    int nz = npoint.z + dz[i];

                    if (nx < 0 || nx >= lengths.x || ny < 0 || ny >= lengths.y
                        || visit[nz, ny, nx] || !codition((nx, ny, nz)))
                        continue;

                    board[nz, ny, nx] = board[npoint.z, npoint.y, npoint.x] + 1;
                    Dfs((nx, ny, nz));
                }
            }

            Print(board);
        }

        public static void Bfs(T[,,] map, (int x, int y, int z) point, (int x, int y, int z) lengths,
                               Func<(int x, int y, int z), bool> codition)
        {
            int[,,] board = new int[lengths.z, lengths.y, lengths.x];
            bool[,,] visit = new bool[lengths.z, lengths.y, lengths.x];
            var q = new DataStructure.Queue<(int, int, int, int)>();

            q.Enqueue((point.x, point.y, point.z, 1));

            while (q.Count != 0)
            {
                (int x, int y, int z, int t) = q.Dequeue();

                visit[z, y, x] = true;

                for (int i = 0; i < 6; i++)
                {
                    int nx = x + dx[i];
                    int ny = y + dy[i];
                    int nz = z + dz[i];

                    if (nx < 0 || nx >= lengths.x || ny < 0 || ny >= lengths.y || nz < 0 || nz >= lengths.z ||
                        visit[nz, ny, nx] || !codition((nz, nx, ny)))
                        continue;

                    board[nz, ny, nx] = t;
                    q.Enqueue((nx, ny, nz, t + 1));
                }
            }

            Print(board);
        }
    }
}
