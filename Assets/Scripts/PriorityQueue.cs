using System;
using System.Collections.Generic;

public class PriorityQueue<TElement, TPriority> where TPriority : IComparable<TPriority>
{
    private List<KeyValuePair<TElement, TPriority>> heap = new List<KeyValuePair<TElement, TPriority>>();

    public int Count
    {
        get { return heap.Count; }
    }

    public bool IsEmpty
    {
        get { return heap.Count == 0; }
    }

    public void Enqueue(TElement element, TPriority priority)
    {
        var node = new KeyValuePair<TElement, TPriority>(element, priority);
        heap.Add(node);
        int current = heap.Count - 1;

        while (current > 0)
        {
            int parent = (current - 1) / 2;
            if (heap[current].Value.CompareTo(heap[parent].Value) > 0)
            {
                Swap(current, parent);
                current = parent;
            }
            else
            {
                break;
            }
        }
    }

    public TElement Dequeue()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Priority queue is empty.");

        TElement result = heap[0].Key;
        int last = heap.Count - 1;
        heap[0] = heap[last];
        heap.RemoveAt(last);

        int current = 0;
        while (current < heap.Count / 2)
        {
            int leftChild = 2 * current + 1;
            int rightChild = 2 * current + 2;

            int largestChild = leftChild;
            if (rightChild < heap.Count && heap[rightChild].Value.CompareTo(heap[leftChild].Value) > 0)
            {
                largestChild = rightChild;
            }

            if (heap[largestChild].Value.CompareTo(heap[current].Value) > 0)
            {
                Swap(largestChild, current);
                current = largestChild;
            }
            else
            {
                break;
            }
        }

        return result;
    }

    public TElement Peek()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Priority queue is empty.");

        return heap[0].Key;
    }

    private void Swap(int index1, int index2)
    {
        var temp = heap[index1];
        heap[index1] = heap[index2];
        heap[index2] = temp;
    }
}
