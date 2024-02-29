using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollObject : MonoBehaviour
{
    public List<RectTransform> _prefabElements;

    private List<RectTransform> _currentElements = new List<RectTransform>();

    public float _distanceBetweenElements;

    [SerializeField]
    private RectTransform _rollObj;

    public void Initialize()
    {
        if(_rollObj == null)
        {
            _rollObj = gameObject.GetComponent<RectTransform>();
        }

        _rollObj.sizeDelta = new Vector2(_prefabElements[0].sizeDelta.x, (_prefabElements[0].sizeDelta.y + _distanceBetweenElements)* _prefabElements.Count);

        RectTransform transfer;

        for (int i = 0; i < _prefabElements.Count; i++)
        {
            transfer = Instantiate(_prefabElements[i], _rollObj);
            transfer.anchoredPosition = new Vector2(0, ((_prefabElements[0].sizeDelta.y + _distanceBetweenElements) * i + _prefabElements [0].sizeDelta.y/2)-(_rollObj.sizeDelta.y / 2));
            _currentElements.Add(transfer);
        }

    }


    public float GetYOfObject(int index)
    {
        return _currentElements[index].anchoredPosition.y;
    }


}
