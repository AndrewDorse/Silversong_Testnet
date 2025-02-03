using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCampHeroViewsController : MonoBehaviour
{
    [SerializeField] private HeroMesh[] _heroMeshes;


    private void Start()
    {
        EventsProvider.OnPlayersDataChanged += UpdateHeroeViews;
    }
     
    private void UpdateHeroeViews(List<PlayerData> data)
    {
        for (int i = 0; i < _heroMeshes.Length; i++)
        {
            if (i < data.Count)
            {
                if (data[i].heroData.classId >= 0 && data[i].heroData.SubraceId >= 0)
                {
                    _heroMeshes[i].SetClassAndRace(DataProvider.HeroClassProviderData.GetHeroClass(data[i].heroData.classId), DataProvider.instance.GetSubrace(data[i].heroData.SubraceId));
                }
            }
            else
            {
                _heroMeshes[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnDestroy()
    {
        EventsProvider.OnPlayersDataChanged -= UpdateHeroeViews;
    }
}
