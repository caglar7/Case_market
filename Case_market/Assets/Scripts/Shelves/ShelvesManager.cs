


using System.Collections.Generic;
using System.Linq;
using Template;
using UnityEngine;

public class ShelvesManager : Singleton<ShelvesManager> 
{
    public List<Shelves> shelvesList = new List<Shelves>();

    private List<Shelves> _availableShelvesList = new List<Shelves>();

    public Shelves GetRandomShelves()
    {
        _availableShelvesList = shelvesList.
                                    Where(item => item.occupied == false && item.IsThereProduct()).ToList();

        if(_availableShelvesList.Count == 0) return null;

        return _availableShelvesList[Random.Range(0, _availableShelvesList.Count)];
    }
}