using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUI : MonoBehaviour {

	[SerializeField]
	PlayerMovement playerMovement;

	Slider slider;

	void Start() {
		slider = GetComponent<Slider> ();
	}

	void Update() {
		slider.value = playerMovement.Power;
		slider.maxValue = playerMovement.MaxPower;
	}
}
