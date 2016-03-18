using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

    private bool set;
    private UISprite healthBar;
    void Start()
    {
        healthBar = this.gameObject.GetComponent<UISprite>();
    }

    void Update()
    {
        if (GameScript.PLAY)
        {
            healthBar.fillAmount=GameScript.HEALTH / 100;
        }
    }

   
}
