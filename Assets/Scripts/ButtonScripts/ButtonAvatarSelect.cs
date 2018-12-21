using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAvatarSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Avatar()
    {
        GridLayout gridLayout = transform.parent.GetComponent<GridLayout>();
        Vector3Int cellPos = gridLayout.WorldToCell(transform.position);

    }

    private int CalcPos(Vector3Int pos)
    {
        int x = (pos.x - 35) / 70;
        int y = -(pos.y + 35) / 70;
        return x + 3 * y;
    }
}