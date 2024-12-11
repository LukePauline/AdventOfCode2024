using AdventOfCode2024.Helpers;
using System.Text;

namespace AdventOfCode2024;
public class Day9 : IDay
{
    public object Exercise1(string input)
    {
        int[] data = Parse(input).ToArray();
        int leftPtr = 0;
        int rightPtr = data.Length - 1;

        while (leftPtr < rightPtr)
        {
            while (data[leftPtr] != -1)
                leftPtr++;
            while (data[rightPtr] == -1)
                rightPtr--;
            if (leftPtr > rightPtr)
                break;
            data[leftPtr] = data[rightPtr];
            data[rightPtr] = -1;
            leftPtr++;
            rightPtr--;
        }
        int end = Array.IndexOf(data, -1);
        return data[0..end].Index().Sum(x => (long)x.Index * x.Item);
    }

    public object Exercise2(string input)
    {
        List<int> movedIds = [];
        var (first, curr) = ParseToLinkedList(input);
        while (curr.Prev != null)
        {
            Block nextCurr = curr.Prev;
            if (movedIds.Contains(curr.Id))
            {
                curr = nextCurr;
                continue;
            }
            movedIds.Add(curr.Id);

            var checkSpace = first;
            while (curr.Size > checkSpace!.FreeSpace && checkSpace != curr.Prev)
                checkSpace = checkSpace.Next;
            if (checkSpace == curr.Prev)
            {
                if (curr.Prev.FreeSpace >= curr.Size)
                {
                    curr.FreeSpace += curr.Prev.FreeSpace;
                    curr.Prev.FreeSpace = 0;
                }
                curr = nextCurr;
                continue;
            }
            // remove curr
            curr.Prev!.FreeSpace += curr.FreeSpace + curr.Size;
            if (curr.Next != null)
                curr.Next.Prev = curr.Prev;
            curr.Prev.Next = curr.Next;
            // insert in new location
            int newFreeSpace = checkSpace.FreeSpace - curr.Size;
            checkSpace.FreeSpace = 0;
            curr.FreeSpace = newFreeSpace;
            curr.Prev = checkSpace;
            curr.Next = checkSpace.Next;
            checkSpace.Next!.Prev = curr;
            checkSpace.Next = curr;
            curr = nextCurr;
        }

        long sum = 0;
        int i = 0;
        Block? ptr = first;
        while (ptr != null)
        {
            for (int j = 0; j < ptr.Size; j++)
            {
                sum += i * ptr.Id;
                i++;
            }
            i += ptr.FreeSpace;
            ptr = ptr.Next;
        }
        return sum;
    }

    private static IEnumerable<int> Parse(string input) =>
        input.Trim().SelectMany((x, index) =>
        {
            bool freeSpace = index % 2 == 1;
            int id = index / 2;
            int noOfBlocks = x - 48;
            return freeSpace
                ? Enumerable.Repeat(-1, noOfBlocks)
                : Enumerable.Repeat(id, noOfBlocks);
        });

    private static (Block start, Block end) ParseToLinkedList(string input)
    {
        var items = input.Trim().Select(c => c - 48).ToArray();
        Block start = new()
        {
            Id = 0,
            Size = items[0],
            FreeSpace = items[1]
        };
        Block curr = start;
        for (int i = 2; i < items!.Length; i += 2)
        {
            curr.Next = new Block()
            {
                Id = i / 2,
                Size = items[i],
                FreeSpace = i + 1 < items.Length ? items[i + 1] : 0,
                Prev = curr
            };

            curr = curr.Next;
        }
        return (start, curr);
    }

    private class Block
    {
        public int Id { get; init; }
        public int Size { get; init; }
        public int FreeSpace { get; set; }
        public Block? Prev { get; set; }
        public Block? Next { get; set; }
    }

    private static string OutputLinkedList(Block start)
    {
        StringBuilder stringBuilder = new();
        Block? ptr = start;
        while (ptr != null)
        {
            for (int i = 0; i < ptr.Size; i++)
                stringBuilder.Append(ptr.Id);
            for (int i = 0; i < ptr!.FreeSpace; i++)
                stringBuilder.Append('.');
            ptr = ptr.Next;
        }
        return stringBuilder.ToString();
    }
}
