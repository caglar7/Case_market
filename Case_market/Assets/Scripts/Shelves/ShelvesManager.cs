


using System.Collections.Generic;
using Template;
using UnityEngine;

public class ShelvesManager : Singleton<ShelvesManager> 
{
    public List<Shelves> shelvesList = new List<Shelves>();

    public Shelves GetRandomShelves()
    {
        return shelvesList[Random.Range(0, shelvesList.Count)];
    }
}