using UnityEngine;
using System.Collections;


public class menu : MonoBehaviour {

	private AdMob admob = new AdMob();
	private bool started = false;
	void Start()
	{
		this.admob.requestBanner();
		this.admob.requestBannerInterstitial();
	}


	void Update()
	{
		if(!started)
		{
			this.admob.showBanners();
			this.admob.showInterstitial();	
		}

	}

	void OnGUI () {

		GUIStyle text_style =  new GUIStyle();
		text_style.fontSize = Screen.width/20;
		text_style.alignment = TextAnchor.MiddleCenter;
		text_style.fontStyle = FontStyle.Bold;
		text_style.normal.textColor = Color.white;


		GUIStyle button_style = new GUIStyle(GUI.skin.button);
		button_style.fontSize = Screen.width/20;

		if ( Application.isLoadingLevel )
		{
			GUI.Box(new Rect(Screen.width / 4, Screen.height / 8 * 3, Screen.width/2, Screen.height/10), TextManager.Instance.getText( "loading" ) + "...",text_style );
		}
		GUI.Box(new Rect(Screen.width / 4, Screen.height / 8, Screen.width/2, Screen.height/10), TextManager.Instance.getText( "last_score" ) + " : " + PlayerPrefs.GetInt( "last_score" ),text_style );
		GUI.Box(new Rect(Screen.width / 4, Screen.height / 8 * 2, Screen.width/2, Screen.height/10), TextManager.Instance.getText( "best_score" ) + " : " + PlayerPrefs.GetInt( "best_score" ),text_style );
		GUI.BeginGroup(new Rect (Screen.width / 4, Screen.height / 2, Screen.width/2 , Screen.height/4+15 ));
		// Make a background box
		GUI.Box(new Rect (0, 0, Screen.width/2 , Screen.height/4+15 ), "" );
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if( GUI.Button(new Rect(5, 5, Screen.width/2-10, Screen.height/8), TextManager.Instance.getText( "play" ) ,button_style)) {

			this.started = true;

			this.admob.hideBanners();

			Application.LoadLevel(1);
		}
		
		// Make the second button.
		if(GUI.Button(new Rect(5, Screen.height/8 + 10, Screen.width/2-10, Screen.height/8), TextManager.Instance.getText( "exit" ),button_style)) {
			Application.Quit();
		}
		GUI.EndGroup();
	}
}
			