using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	private float shakeStrength = 0f;
	private float shakeReduceRate = 1f;
	private float shakeMax = 1f;
	//private float 

	public void ShakeImpulse(float amount)
	{
		this.shakeStrength = Mathf.Max(amount, this.shakeStrength);
	}

	public void Update()
	{
		this.transform.localPosition = Random.insideUnitCircle * this.shakeStrength;
		this.shakeStrength = Mathf.Min(this.shakeMax,
			Mathf.Max(0, this.shakeStrength - this.shakeReduceRate * Time.deltaTime));
	}
}
