using UnityEngine;
using System.Collections;

public class Targetting : MonoBehaviour {

    private bool isTargetting;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {

       if (Input.GetMouseButtonDown(0) && isTargetting)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 10f);
            Debug.Log("Position of click" + new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y));      
            if (hit.collider != null  && hit.transform.tag == "Target")
            {

                Destroy(hit.collider.gameObject);
                //Display the name of the parent of the object hit.
                Debug.Log(hit.rigidbody.name);
                isTargetting = false;
            }
        }

    }

    public void checkTarget()
    {
        isTargetting = true;
    }
}
