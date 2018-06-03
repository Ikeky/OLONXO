using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletChecker : MonoBehaviour
{
    public Vector3 p_tr;
    public int Damage;
	void OnCollisionEnter(Collision arg){
		if (arg.gameObject.layer == LayerMask.GetMask ("wall")) {
			Destroy (gameObject);
		}
	}
}
