using UnityEngine;


public class maincharacter : MonoBehaviour {
	
	private Game game;
	// Use this for initialization
	void Start () 
	{
		this.game = new Game();
		GameObject gamead = GameObject.Find( "gamead" );
	}

	
	void Update() 
	{
		game.moveMainCharacter( transform );
	}

	void FixedUpdate()
	{
		game.testGameOver();
		game.updateElements();
	}

	void OnCollisionEnter(Collision collision) 
	{
		if( collision.gameObject.name.StartsWith( "enemy" )  )
		{
			collision.rigidbody.velocity = Vector3.left;
			transform.Rotate( 0,0,-10 );
			collision.gameObject.GetComponent<Rigidbody>().useGravity = true;
			game.setGameOver( true );
		}

	}

	void OnTriggerEnter(Collider collision) 
	{
		if( collision.gameObject.name.StartsWith( "point" )  )
		{
			game.incrementPoints();
			GameObject text_points = GameObject.Find("textpoints");
			text_points.GetComponent<GUIText>().text = TextManager.Instance.getText( "points" ) + " : " + game.getPoints();
			collision.gameObject.GetComponent<Animation>().Play( "animationvida" );
		}
	}
}
