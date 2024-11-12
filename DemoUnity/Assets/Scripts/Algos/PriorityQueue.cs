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
    /// Ajoute un élément avec une priorité donnée.
    /// </summary>
    public void Enqueue(T item, float priority)
    {
        elements.Add((item, priority));
        elements.Sort((x, y) => x.Priority.CompareTo(y.Priority));
    }

    /// <summary>
    /// Retire et renvoie l'élément avec la plus haute priorité (plus basse valeur de priorité).
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
    /// Vérifie l'élément avec la plus haute priorité sans le retirer.
    /// </summary>
    public T Peek()
    {
        if (elements.Count == 0)
            throw new System.InvalidOperationException("The queue is empty.");

        return elements[0].Item;
    }
}
