using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LeverController : MonoBehaviour
{

    // Из-за глупого юнити, приходится делать такой вот кульбит

    [SerializeField] List<Object> objectsToActivate;
    List<IActivateble> activatableTargets => objectsToActivate.Select(obj => obj.GetComponent<IActivateble>()).ToList();

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation.eulerAngles.z < 50)
        {
            activatableTargets.ForEach(obj => obj.Activate());
        }
        if(transform.rotation.eulerAngles.z > 310)
        {
            activatableTargets.ForEach(obj => obj.Deactivate());
        }
    }
}
