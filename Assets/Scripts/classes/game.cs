using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	const int PLATFORM_1 = 1;
	const int PLATFORM_2 = 2;
	const int PLATFORM_3 = 3;
	
	const int DIRECTION_UP = 1;
	const int DIRECTION_DOWN = -1;

	const float ENEMY_X_START = 8f; 
	const float ENEMY_X_END = -8f; 

	const float POINT_X_END = -3f; 

	private float[] platform_positions = new float[3]{ -4f, -1.25f, 1.5f };
	private int current_platform = 2;
	private bool gameover = false;
	
	private int points = 0;

	private float ini_velocity = 5f;
	private float current_velocity;
	private int velocity_increment_interval = 60;
	private float enemy_interval = 1.7f;
	private float point_interval = 5f;

	private float last_enemy_update = 0;
	private float last_point_update = 0;
	private GameInput game_input = new GameInput();
	private ArrayList elements = new ArrayList();
	private RandomHelper random_helper =  new RandomHelper();

	public Game()
	{
		GameObject text_points = GameObject.Find("textpoints");
		text_points.GetComponent<GUIText>().text = TextManager.Instance.getText( "points" ) + " : 0" ;

		GameObject text_time = GameObject.Find("texttime");
		text_time.GetComponent<GUIText>().text =  TextManager.Instance.getText( "time" ) + " : 0" ;
	}

	public void updateElements()
	{
		GameObject text_time = GameObject.Find("texttime");
		text_time.GetComponent<GUIText>().text = TextManager.Instance.getText( "time" ) + " : " + (int)Time.timeSinceLevelLoad ;

		this.current_velocity = (int)Time.timeSinceLevelLoad / this.velocity_increment_interval + ini_velocity;

		if ( Time.timeSinceLevelLoad - this.last_enemy_update > this.enemy_interval )
		{
			this.last_enemy_update = Time.timeSinceLevelLoad;
			ArrayList platforms = this.random_helper.generateRandoms( 0,2,2 );
			ArrayList positions_x_add = this.random_helper.generateRandoms( 0,3,3 );
			
			foreach ( object platform in platforms )
			{
				this.elements.Add( this.createEnemy( this.platform_positions[(int)platform], current_velocity, (int)positions_x_add[(int)platform] ) );
			}
		}
		if ( Time.timeSinceLevelLoad - this.last_point_update > this.point_interval )
		{
			this.last_point_update = Time.timeSinceLevelLoad;
			int platform = Random.Range( 0, 3 );
			this.elements.Add( this.createPoint( this.platform_positions[platform], current_velocity * 3/2 ) );
		}
		try
		{
			this.destroyUnusedElements();
		}
		catch( System.InvalidOperationException exception )
		{

		}
	}

	private void destroyUnusedElements()
	{
		foreach( GameObject element in this.elements )
		{
			if( element.GetComponent<Rigidbody>().position.x < ENEMY_X_END )
			{
				elements.Remove( element );
				Destroy( element );
			}
		}
	}

	public void testGameOver()
	{
		if( gameover )
		{
			gameover=false;
			PlayerPrefs.SetInt("last_score", this.points);
			int best_score = PlayerPrefs.GetInt( "best_score" );
			if( this.points >= best_score )
			{
				PlayerPrefs.SetInt("best_score", this.points);
			}
			Application.LoadLevel(0);
		}
	}

	public void incrementPoints( int points = 1 )
	{
		this.points += points;
	}

	public int getPoints()
	{
		return this.points;
	}

	public void setGameOver( bool gameover )
	{
		this.gameover = gameover;
	}
	
	private GameObject createPoint( float platform_y, float velocity_mult = 1 )
	{
		GameObject vida = GameObject.Find("point");
		
		Vector3 position = new Vector3( ENEMY_X_START, platform_y + 1f , 0 );
		GameObject vida1 = (GameObject) Instantiate( vida,position,vida.transform.rotation );
		vida1.GetComponent<Rigidbody>().velocity = Vector3.left;
		vida1.GetComponent<Rigidbody>().velocity = vida1.GetComponent<Rigidbody>().velocity * velocity_mult;
		//vida1.AddComponent("vida");
		
		return vida1;
	}
	
	private GameObject createEnemy( float platform_y, float velocity_mult = 1, float position_x_add = 0, int type = 0 )
	{
		if( type == 0 )
		{
			type = Random.Range( 1, 4 );
		}
		GameObject enemy = GameObject.Find( "enemy" + type );
		
		Vector3 position = new Vector3( ENEMY_X_START + position_x_add, platform_y + 1.3f , 0 );
		GameObject enemy1 = ( GameObject ) Instantiate(enemy,position,enemy.transform.rotation);
		enemy1.GetComponent<Rigidbody>().velocity = Vector3.left;
		enemy1.GetComponent<Rigidbody>().velocity = enemy1.GetComponent<Rigidbody>().velocity * velocity_mult;
		//enemy1.AddComponent( "enemigo" );
		
		return enemy1;
	}

	public void moveMainCharacter( Transform character )
	{
		Vector3 pos = character.position;
		int touch = this.game_input.getTouch();
		
		if ( touch > 0 || Input.GetKeyDown( "up" ) ) 
		{  
			this.changeCharacterPlatform( DIRECTION_UP, pos, character );
		}
		if ( touch < 0 || Input.GetKeyDown( "down" ) ) 
		{    
			this.changeCharacterPlatform( DIRECTION_DOWN, pos, character );
		}	
		if( touch == 0 || Input.GetKeyUp( "up" ) || Input.GetKeyUp( "down" ) )
		{
			character.GetComponent<Renderer>().material.mainTexture = Resources.Load("maincharacter") as Texture2D;
		}
	}
	
	private void changeCharacterPlatform( int direction, Vector3 pos, Transform character )
	{
		character.GetComponent<Renderer>().material.mainTexture = Resources.Load("maincharacterback") as Texture2D;
		if( direction == DIRECTION_UP )
		{
			if( this.current_platform != 3 )
			{
				this.current_platform++;
				character.position = new Vector3(pos.x , 
				                                 pos.y + 3.3f , 
				                                 pos.z);
			}
		}
		if ( direction == DIRECTION_DOWN )
		{
			if ( this.current_platform != 1 )
			{
				this.current_platform--;
				character.position = new Vector3(pos.x , 
				                                 pos.y - 2.7f , 
				                                 pos.z);
			}
		}
	}

}

