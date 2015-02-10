using UnityEngine;


public class maincharacter : MonoBehaviour {
	
	private Game game;
	// Use this for initialization
	void Start () 
	{
		this.game = new Game();
		GameObject gamead = GameObject.Find( "gamead" );
		AdMobPlugin admp = (AdMobPlugin) gamead.GetComponent( typeof(AdMobPlugin) );
		admp.size = AdSize.SMART_BANNER;
		admp.Reconfigure();
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
			collision.gameObject.rigidbody.useGravity = true;
			game.setGameOver( true );
		}

	}

	void OnTriggerEnter(Collider collision) 
	{
		if( collision.gameObject.name.StartsWith( "point" )  )
		{
			game.incrementPoints();
			GameObject text_points = GameObject.Find("textpoints");
			text_points.guiText.text = TextManager.Instance.getText( "points" ) + " : " + game.getPoints();
			collision.gameObject.animation.Play( "animationvida" );
		}
	}
}
