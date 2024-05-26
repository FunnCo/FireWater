using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    // Из-за глупого юнити, приходится делать такой вот кульбит

    [SerializeField] List<Object> objectsToActivate;
    List<IActivateble> activatableTargets => objectsToActivate.Select(obj => obj.GetComponent<IActivateble>()).ToList();
    bool isButtonPressed = false;
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isButtonPressed = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isButtonPressed = false;
        activatableTargets.ForEach(obj => obj.Deactivate());
    }


    // Update is called once per frame
    void Update()
    {
        if (isButtonPressed)
        {
            activatableTargets.ForEach(obj => obj.Activate());
        }        
    }
}
