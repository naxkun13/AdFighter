using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour {
	[SerializeField]
	private float lerpSpeed;

	[SerializeField]
	private Text statValue;

	private Image content;
	private float currentFill;
	public float MyMaxValue { get; set;}
	private float currentValue;
	public float MyCurrentValue
	{
		get
		{
			return currentValue;
		}

		set
		{
			if (value > MyMaxValue) {
				currentValue = MyMaxValue;
			} else if (value < 0) {
				currentValue = 0;
			} else 
			{
				currentValue = value;
			}

			currentFill = currentValue / MyMaxValue;

			statValue.text = currentValue + "/" + MyMaxValue;
		}
	}


	// Use this for initialization
	protected virtual void Start () 
	{
		content = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (currentFill != content.fillAmount) 
		{
			content.fillAmount = Mathf.Lerp (content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
		}
		Debug.Log (MyCurrentValue);
	}

	public void Initialize(float currentValue, float maxValue)
	{
		MyMaxValue = maxValue;
		MyCurrentValue = currentValue;
	}
}
