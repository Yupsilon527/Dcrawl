using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarController : MonoBehaviour
{
    public Transform HealthBarFill;
    public TMPro.TextMeshProUGUI HealthBarValue;

    Resource mResource;
    public void AssignResource(Resource resource)
    {
        mResource = resource;
        OnValueChange();
    }
    public void OnValueChange()
    {
        if (mResource!= null)
        {
            if (HealthBarFill != null)
            { HealthBarFill.transform.localScale = new Vector3(mResource.GetPercentage(), 1, 1); }
            if (HealthBarValue != null)
            { HealthBarValue.text = "Life: "+mResource.GetValue() ; }
        }
    }
}
