using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour
{
	

	public RectTransform center;
	public RectTransform knob;
	public float range;
	public bool fixedJoystick;

	//[HideInInspector]
	private Vector2 direction;
	//	public float distance;
	//Vector2 pos, start;

	private int _touchNumber = -1;
	public int _touchId;


	

	public void SetTouchNumber(int value) { _touchNumber = value; }

	public int GetTouchNumber() { return _touchNumber; }


	void Update()
	{
		if (Input.touchCount == 0 && _touchNumber == -1) return;

		if (Input.touchCount == 0) return;

		Touch touch = Input.GetTouch(0);

		if (Input.touchCount == 1 ) // && SpellCaster.instance.GetTouchNumber() == -1)
		{
			if (_touchNumber == -1)
			{
				_touchNumber = 0;
				_touchId = touch.fingerId;
			}
			else if (_touchNumber == 1)
			{
				_touchNumber = 0;
				_touchId = touch.fingerId;
			}
		}
		else if (Input.touchCount == 2)
		{
			if (_touchNumber == -1)
			{
				touch = Input.GetTouch(1);
				_touchNumber = 1;
				_touchId = touch.fingerId;
			}
			else
			{
				for (int i = 0; i < 2; i++)
				{
					if (Input.GetTouch(i).fingerId == _touchId) touch = Input.GetTouch(i);
				}
			}
		}


		if (_touchNumber == -1 || Input.touchCount == 0) return;


		Vector2 pos = touch.position;

		if (pos.x < Screen.width * 0.75) // ????/ TODO
		{
			if (touch.phase == TouchPhase.Began)
			{
				ShowHide(true);
				//start = pos;

				knob.position = pos;
				center.position = pos;
			}
			else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
			{
				knob.position = pos;
				knob.position = center.position + Vector3.ClampMagnitude(knob.position - center.position, center.sizeDelta.x * range);

				if (knob.position != Input.mousePosition && !fixedJoystick)
				{
					Vector3 outsideBoundsVector = Input.mousePosition - knob.position;
					center.position += outsideBoundsVector;
				}

				//distance = (knob.position - center.position).magnitude;

				direction = (knob.position - center.position).normalized;

				knob.transform.up = direction;
				center.transform.up = direction;
			}
			else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
			{
				ShowHide(false);
				direction = Vector2.zero;
				_touchNumber = -1;
				_touchId = -666;

				//if (SpellCaster.instance.GetTouchNumber() > 0)
				//{
				//	SpellCaster.instance.SetTouchNumber(0);
				//}
			}
		}

		EventsProvider.OnJoystickMove?.Invoke(direction);
	}

	void ShowHide(bool state)
	{
		center.gameObject.SetActive(state);
		knob.gameObject.SetActive(state);
	}


}


