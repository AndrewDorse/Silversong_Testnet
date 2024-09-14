using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerSlotUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _name, _ping;
    [SerializeField] private Image _bg;
    private Photon.Realtime.Region _region;
    private string _regionName;
    [SerializeField] private Color _normalColor, _choosedColor;

    [SerializeField] private Button _button;

    public void SetServerInfo(Photon.Realtime.Region region, bool choosed, Action<Photon.Realtime.Region> clickCallback)
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => clickCallback?.Invoke(_region));

        _regionName = region.Code;
        _name.text = region.Code;
        if (region.Ping < 4000)
            _ping.text = region.Ping.ToString();



        _region = region;

        if (choosed) _bg.color = _choosedColor;
        else _bg.color = _normalColor;
    }


}