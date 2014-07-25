using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum DirectionEnum
{
	Right=0,
	Up=1,
	Left=2,
	Down=3
}

public class SnakeController : MonoBehaviour 
{
	public float MoveBySeconds;
	public GameObject Body;

	private DirectionEnum _direction;
	private  List<Transform> _parts;
			
	// Use this for initialization
	void Start () 
	{
		_parts = new List<Transform>();
		_parts.Add(transform);
		InvokeRepeating("MoveTheSnakeOneStep",0,MoveBySeconds);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			TurnLeft();
		}
		if(Input.GetKeyDown (KeyCode.RightArrow))
		{
			TurnRight ();
		}
		if (Input.GetKeyDown (KeyCode.Space))
		{
			AddNewBody();
		}
	}
	
	void MoveTheSnakeOneStep()
	{
		int index = 0;
		Vector3 previousPosition = Vector3.zero;
		foreach(var part in _parts)
		{
			if(index==0)
			{
				//The snake head
				previousPosition = part.transform.position;
				transform.Translate(Vector3.right);
			}
			else
			{
				//A snake Body
				Vector3 bodyTranslation = previousPosition - part.transform.position;
				previousPosition = part.transform.position;
				part.transform.Translate(bodyTranslation);
			}
			index++;
		}
	}
	
	Quaternion GetRotationToAppliedForGivenDirection(DirectionEnum direction)
	{
		switch(direction)
		{
		case DirectionEnum.Right:
			return Quaternion.Euler(0,0,0);
		case DirectionEnum.Up:
			return Quaternion.Euler(0,0,90);
		case DirectionEnum.Left:
			return Quaternion.Euler(0,0,180);
		default:
			return Quaternion.Euler(0,0,270);
		}
	}
	
	Vector3 GetVectorFromDirection(DirectionEnum direction)
	{
		switch(direction)
		{
			case DirectionEnum.Right:
				return Vector3.right;
			case DirectionEnum.Up:
				return Vector3.up;
			case DirectionEnum.Left:
				return Vector3.left;
			default:
				return Vector3.down;
		}
	}
	
	void AddNewBody()
	{
		if (Body!=null)
		{
			var newBody = Instantiate(Body) as GameObject;
			newBody.SetActive(true);
			var lastPart = GetLastPart();
			newBody.transform.position = lastPart.position;
			_parts.Add(newBody.transform);
			
		}
	}
	
	Transform GetLastPart()
	{
		return _parts.Last();
	}
	
	void TurnRight()
	{
		
		
		if(_direction==DirectionEnum.Right)
			_direction=DirectionEnum.Down;
		else
			_direction= (DirectionEnum)((int)_direction-1);
		transform.rotation = GetRotationToAppliedForGivenDirection(_direction);
			
	}
	
	void TurnLeft()
	{
		
		if(_direction==DirectionEnum.Down)
			_direction=DirectionEnum.Right;
		else
			_direction= (DirectionEnum)((int)_direction+1);
		transform.rotation = GetRotationToAppliedForGivenDirection(_direction);
	}
	
	void OnTriggerEnter2D(Collider2D other) 
	{
		if(other.tag=="Wall")
		{
			
		}
		else if (other.tag=="Fruit")
		{
			Destroy (other.gameObject);
			AddNewBody();
		
		}
		
	}
	
	
}
