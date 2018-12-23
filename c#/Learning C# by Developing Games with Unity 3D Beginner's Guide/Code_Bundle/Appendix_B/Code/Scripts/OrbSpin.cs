using UnityEngine;
using System.Collections;

public class OrbSpin : MonoBehaviour
{
	void Update ()
	{
		transform.Rotate(0, 800 * Time.deltaTime, 0);
	}
}