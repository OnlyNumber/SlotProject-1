using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollControl : MonoBehaviour
{
    [SerializeField]
    private RollObject _prefab;

    [SerializeField]
    private List<RectTransform> _rollingObjects;

    [SerializeField]
    private RectTransform _mask;

    [SerializeField]
    private float _rollingSpeed;

    private bool _isRoll;

    public bool IsStopping;

    public int IndexOfStoppedObject;

    public void Initialize(float xCoordinate)
    {


        RectTransform transfer;

        for (int i = 0; i < 2; i++)
        {
            transfer = Instantiate(_prefab, _mask).GetComponent<RectTransform>();
            transfer.GetComponent<RollObject>().Initialize();
            _rollingObjects.Add(transfer);
            transfer.anchoredPosition = new Vector2(xCoordinate, ((_mask.sizeDelta.y + transfer.sizeDelta.y) / 2) + transfer.sizeDelta.y * (i));
        }

    }


    private void Update()
    {
        if (!_isRoll)
        {
            return;
        }

        //Vector3 pos = _rollingObjects[0].anchoredPosition;

        if (IsStopping && (-_rollingObjects[0].GetComponent<RollObject>().GetYOfObject(IndexOfStoppedObject) >= _rollingObjects[0].anchoredPosition.y -30 && -_rollingObjects[0].GetComponent<RollObject>().GetYOfObject(IndexOfStoppedObject) <= _rollingObjects[0].anchoredPosition.y + 30) )
        {
            _isRoll = false;
        }




        foreach (var item in _rollingObjects)
        {
            item.anchoredPosition = item.anchoredPosition + new Vector2(0, -_rollingSpeed) * Time.deltaTime;
            if (item.anchoredPosition.y <= -(item.sizeDelta.y / 2 + _mask.sizeDelta.y / 2))
            {
                item.anchoredPosition = new Vector3(item.anchoredPosition.x, item.anchoredPosition.y + item.sizeDelta.y * (_rollingObjects.Count) /*item.sizeDelta.y / 2 + 160*/);
            }

        }

        //Debug.Log(_rollingObjects[0].position);
    }

    public void Roll(bool state)
    {
        _isRoll = state;
    }

}
