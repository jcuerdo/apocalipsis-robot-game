using System.Collections;
using UnityEngine;

public class RandomHelper
{
	public ArrayList generateRandoms( int start, int end, int max_values = 1 )
	{
		int random;
		ArrayList randoms = new ArrayList();
		while( randoms.Count < max_values )
		{
			random = Random.Range( start, end + 1 );
			if( !randoms.Contains( random ) )
			{
				randoms.Add(random);
			}
		}
		return randoms;
	}

	public int generateRandom( int start, int end )
	{
		return Random.Range( start, end );
	}
}


