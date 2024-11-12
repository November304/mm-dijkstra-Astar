using System.Collections.Generic;

public class PriorityQueue<T>
{
    private List<(T Item, float Priority)> elements;

    public PriorityQueue()
    {
        elements = new List<(T, float)>();
    }

    public int Count => elements.Count;

    /// <summary>
    /// Ajoute un �l�ment avec une priorit� donn�e.
    /// </summary>
    public void Enqueue(T item, float priority)
    {
        elements.Add((item, priority));
        elements.Sort((x, y) => x.Priority.CompareTo(y.Priority));
    }

    /// <summary>
    /// Retire et renvoie l'�l�ment avec la plus haute priorit� (plus basse valeur de priorit�).
    /// </summary>
    public T Dequeue()
    {
        if (elements.Count == 0)
            throw new System.InvalidOperationException("The queue is empty.");

        T item = elements[0].Item;
        elements.RemoveAt(0);
        return item;
    }

    /// <summary>
    /// V�rifie l'�l�ment avec la plus haute priorit� sans le retirer.
    /// </summary>
    public T Peek()
    {
        if (elements.Count == 0)
            throw new System.InvalidOperationException("The queue is empty.");

        return elements[0].Item;
    }
}
