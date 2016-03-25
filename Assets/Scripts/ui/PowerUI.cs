using UnityEngine;
using UnityEngine.UI;

public class PowerUI : MonoBehaviour {

	[SerializeField][Tooltip("This is in the player. Need it to display (max)power.")]
	PowerContainer pc;

    GameObject fillArea;
    Text text;
	Slider slider;

	void Start() {
        fillArea = transform.FindChild("Fill Area").gameObject;
        text = transform.FindChild("PowerText").gameObject.GetComponent<Text>();
		slider = GetComponent<Slider>();
	}

	void Update() {
		slider.value = pc.Power < Mathf.Floor(pc.Power / 2) ? pc.Power - 1 : pc.Power;
        slider.maxValue = pc.MaxPower;

        text.text = Mathf.Round(pc.Power) + " / " + (pc.MaxPower > 0 ? (int)pc.MaxPower : 0); // If it's less than 0 I don't want it to display that.

        if (pc.Power <= 0) {
            fillArea.SetActive(false);
        } else {
            fillArea.SetActive(true);
        }
	}
}
