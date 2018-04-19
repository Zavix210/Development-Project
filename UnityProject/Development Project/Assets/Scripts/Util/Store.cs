using UnityEngine;
using System.Collections.Generic;

public class Store<StorageType> 
{
    List<StorageType> store;

    public Store()
    {
        store = new List<StorageType>();
    }

    public List<StorageType> GetRawList()
    {
        return store;
    }

    public bool Add(StorageType item)
    {
        if(!store.Contains(item))
        {
            store.Add(item);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Remove(StorageType item)
    {
        return store.Remove(item);
    }

    public bool Contains(StorageType item)
    {
        return store.Contains(item);
    }

    public void Clear()
    {
        store.Clear();
    }
}
