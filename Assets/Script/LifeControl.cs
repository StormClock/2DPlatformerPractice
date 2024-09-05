using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeControl : MonoBehaviour
{
    public List<GameObject> LifeImage;

    public void SetLife(int Life)
    {
        foreach (GameObject obj in LifeImage) 
        {
            obj.SetActive(false);
        }

        for (int i = 0; i < Life; i++)
        {
            LifeImage[i].SetActive(true);
        }
    }
}
