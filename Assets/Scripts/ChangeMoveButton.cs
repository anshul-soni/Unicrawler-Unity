using UnityEngine;
using System.Collections;

public class ChangeMoveButton : MonoBehaviour {

    public GameObject leftButton;
    public GameObject rightButton;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeButton()
    {
        if (leftButton.gameObject.activeSelf)
        {
            rightButton.gameObject.SetActive(true);
            leftButton.gameObject.SetActive(false);
        }
        else
        {
            if (rightButton.gameObject.activeSelf)
            {
                rightButton.gameObject.SetActive(false);
                leftButton.gameObject.SetActive(true);
            }
        }

    }
}
