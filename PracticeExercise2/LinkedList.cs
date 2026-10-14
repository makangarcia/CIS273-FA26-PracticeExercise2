using System.Diagnostics.Metrics;

namespace PracticeExercise2;

public class LinkedListNode<T>
{
    public T Data{get; set;}
    public LinkedListNode<T> Next{get; set;}

    public LinkedListNode(T data = default, LinkedListNode<T> next=null)
    {
        Data = data;
        Next = next;
    }

    public override string ToString()
    {
        return Data is null ? "null" :Data.ToString();
    }
}


public class LinkedList<T> : IList<T>
{
   
    private LinkedListNode<T>? Head {get; set;}
    private LinkedListNode<T>? Tail {get; set;}

    public LinkedList()
    {
        Head = null;
        Tail = null;
    }


    // TODO
    /// <summary>
    /// Returns first element in list, null if empty.
    /// </summary>
    public T? First => IsEmpty ? default : Head.Data;

    // TODO
    /// <summary>
    /// Returns last element in list, null if empty.
    /// </summary>
    public T? Last => IsEmpty ? default : Tail.Data;

    // TODO
    /// <summary>
    /// Returns true if list is has no elements; false otherwise.
    /// </summary>
    public bool IsEmpty => Head is null;

    /// <summary>
    /// Number of elements in list.
    /// </summary>
    public int Length
    {
        get;
        private set;
    }

    // TODO 
    /// <summary>
    /// Adds given item to end of list.
    /// </summary>
    /// <param name="item">item to add to the list</param>
    public void Append(T item)
    {
        var newNode = new LinkedListNode<T>(item);
        //if empty
        if (IsEmpty)
        {
            Head = newNode;
            Tail = newNode;
        }
        else
        {
            //1+ node in list 
            Tail.Next= newNode;
            Tail=newNode;
        }
        Length++;
    }

    /// <summary>
    /// Remove all elements from list
    /// </summary>
    public void Clear()
    {
        Head = null;
        Tail = null;
        Length = 0;
    }

    /// <summary>
    /// Checks if the list contains the given value.
    /// </summary>
    /// <param name="item"></param>
    /// <returns>true if item is in list; false otherwise</returns>
    public bool Contains(T item)
    {
        //traverse the list looking for item
        var currentNode = Head;

        while(currentNode != null)
        {
            if (currentNode.Data.Equals(item))
            {
                return true;
            }
            currentNode = currentNode.Next;
        }



        return false;
    }

    // TODO
    /// <summary>
    /// Find index of first element with matching value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>Index of first element with value; -1 if element is not found</returns>
    public int FirstIndexOf(T item)
    {
        var counter = 0; 
        var currentNode = Head;
        
        while(currentNode != null)
        {
            if (currentNode.Data.Equals(item))
            {
                return counter;
            }
            currentNode = currentNode.Next;
            counter++;
        }
        return -1;
    }

    /// <summary>
    /// Return the element at the given index.
    /// </summary>
    /// <param name="index"></param>
    /// <returns>The element at the given index.</returns>
    public T Get(int index)
    {
        var currentNode = Head; 

        if(index < 0 || index >= Length)
        {
            throw new IndexOutOfRangeException();
        }

        for(int i = 0; i < index; i++)
        {
            currentNode = currentNode.Next;
        }

        return currentNode.Data;
    }

    // TODO
    /// <summary>
    /// Insert new value after first instance of existing value.
    /// If existingValue is not in list, then add new value to end of list.
    /// </summary>
    /// <param name="newValue"></param>
    /// <param name="existingValue"></param>
    public void InsertAfter(T newItem, T existingItem)
    {
        var existingNode = GetNode(existingItem);

        if(existingNode == null)
        {
            Append(newItem);
            return;
        }

        var newNode = new LinkedListNode<T>(newItem);

        newNode.Next = existingNode.Next;
        existingNode.Next = newNode;

        if (existingNode == Tail)
        {
            Tail = newNode;
        }


        Length++;
    }

    // TODO
    /// <summary>
    /// Insert value at given index 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="index"></param>
    public void InsertAt(T item, int index)
    {
        if(index < 0 || index > Length)
        {
            throw new IndexOutOfRangeException();
        }

        if (index == 0)
        {
            Prepend(item);
            return;
        }

        if (index == Length)
        {
            Append(item);
            return;
        }

        var previous = Head;

        for (int i = 0; i < index - 1; i++)
        {
            previous = previous.Next;
        }

        var newNode = new LinkedListNode<T>(item);

        newNode.Next = previous.Next;
        previous.Next = newNode;

        Length++;
    }


    // TODO
    /// <summary>
    /// Add value to beginning of list
    /// </summary>
    /// <param name="value"></param>
    public void Prepend(T item)
    {
        var newNode = new LinkedListNode<T>(item);

        if (IsEmpty)
        {
            Head = newNode;
            Tail = newNode;
        }
        else
        {
            newNode.Next = Head;
            Head = newNode;
        }
        Length++;
    }

    // TODO
    /// <summary>
    /// Remove first item with given value
    /// </summary>
    /// <param name="value">value of item to be removed</param>
    public void Remove(T item)
    {
       //empty list 
        if(IsEmpty)
            return;

        if (Head.Data.Equals(item))
        {
            if (Head == Tail)
            {
                Tail = null;
            }
            Head = Head.Next;
            Length--;
            return;
        }

        var currentNode = Head;
        while(currentNode != null)
        {
            var sucessorNode = currentNode.Next;

            if(sucessorNode == null)
            {
                return;
            }

            if (sucessorNode.Data.Equals(item))
            {
                if(sucessorNode == Tail)
                {
                    Tail = currentNode;
                }
                currentNode.Next = currentNode.Next.Next;
                Length--;
                return;
            }

            currentNode = currentNode.Next;
        }


    }

    // TODO
    /// <summary>
    /// Removed item at specififed index.
    /// </summary>
    /// <param name="index"></param>
    public void RemoveAt(int index)
    {
        if(index < 0 || index >= Length)
        {
            throw new IndexOutOfRangeException();
        }

        if (index == 0)
        {
            Head = Head.Next;
            
            if (Head == null)
            {
                Tail = null;
            }
            Length--;
            return;
        }

        var previous = Head;
        for (int i = 0; i < index - 1; i++)
        {
            previous = previous.Next;
        }

        if(previous.Next == Tail)
        {
            Tail = previous;
        }
        previous.Next = previous.Next.Next;
        Length--;
    }

    // TODO
    /// <summary>
    /// Return a new copy of list in reverse order
    /// </summary>
    /// <returns></returns>
    public IList<T> Reverse()
    {
        var reversed = new LinkedList<T>();
        var currentNode = Head;

        while (currentNode != null)
        {
            reversed.Prepend(currentNode.Data);

            currentNode = currentNode.Next;
        }

        return reversed;
    }

    private LinkedListNode<T> GetNode(T item)
    {
        var currentNode = Head;

        while (currentNode != null)
        {
            if (currentNode.Data.Equals(item))
            {
                return currentNode;
            }
            currentNode = currentNode.Next;
        }

        return null;
    }
    

    public override string ToString()
    {
        string str = "[ ";
        var currentNode = Head;
        while (currentNode != null)
        {
            str += currentNode.Data.ToString();

            if (currentNode != Tail)
            {
                str += ", ";
            }
            currentNode = currentNode.Next;
        }
        str += "]";
        return str;
    }
}
