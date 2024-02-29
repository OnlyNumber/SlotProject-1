using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePanel : MonoBehaviour
{

    public void Activate(bool activity)
    {
        gameObject.SetActive(activity);
    }

    public void Close()
    {
        gameObject.SetActive(false);

    }
}
