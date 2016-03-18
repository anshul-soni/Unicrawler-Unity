using UnityEngine;
using System.Collections;

public class bgScroller : MonoBehaviour {

    public float ScrollSpeed;
	
	private float _startPos;
    private float myTime;

	void Start () {
	 _startPos = this.gameObject.transform.position.x;
	}
	
	void FixedUpdate () {
        myTime += Time.deltaTime;
        Vector2 scrollVec = new Vector2(0,0); 
        scrollVec.x =  (ScrollSpeed*myTime)%1;
        scrollVec.y = (ScrollSpeed * myTime)%1; 
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", scrollVec);
	}
}
