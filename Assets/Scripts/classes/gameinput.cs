using UnityEngine;


public class GameInput 
{
	private Vector2 firstPressPos;
	private Vector2 secondPressPos;
	private float movement_distance = 0f;
	
	public int getTouch()
	{
		if(Input.touches.Length > 0)	
		{
			Touch t = Input.GetTouch(0);
			
			if(t.phase == TouchPhase.Began)
			{
				firstPressPos = new Vector2(t.position.x,t.position.y);
			}
			
			if(t.phase == TouchPhase.Ended)	
			{
				secondPressPos = new Vector2(t.position.x,t.position.y);
				
				int result = 0;
				if( (secondPressPos.y - firstPressPos.y) > movement_distance  )	
				{
					result = 1;
				}
				if( (secondPressPos.y - firstPressPos.y) < movement_distance  )	
				{
					result = -1;			
				}
				
				return result;
			}
			
		}
		return 0;
	}

}
