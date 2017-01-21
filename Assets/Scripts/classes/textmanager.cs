using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextManager 
{
	private static TextManager instance;
	private Dictionary<string,string> textTable;
	private const string DEFAULT_LANG = "English";

	private TextManager() 
	{
		textTable = new Dictionary<string,string>();
		loadLanguage();
	} 
	
	public static TextManager Instance 
	{
		get 	
		{
			if (instance == null) 
			{
				instance = new TextManager();			
			}
			return instance;	
		}
	}
 
	
	private bool loadLanguage ( string filename = null )
	{
		if( filename == null )
		{
			filename = Application.systemLanguage.ToString();
		}
		string fullpath = filename;

		TextAsset textAsset= Resources.Load( fullpath ) as TextAsset;
		
		if ( textAsset == null ) 
		{
			if( filename.Equals( DEFAULT_LANG ) )
			{
				loadLanguage( DEFAULT_LANG );
			}
			return false;
		}

		string[] lines = textAsset.text.Split( '\n' );
		foreach( string line in lines )
		{
			string[] line_values = line.Split('=');
			textTable.Add( line_values[0],line_values[1] );
		}
		return true;
	}
		
	public string getText ( string key)
	{
		if (key != null && textTable != null)	
		{
			if ( textTable.ContainsKey( key  ) )	
			{
				string result = (string)textTable[key];
				if (result.Length > 0)				
				{
					key = result;				
				}		
			}	
		}
		return key;	
	}
}

