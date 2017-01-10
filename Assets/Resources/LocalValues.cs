using UnityEngine;
using System.Collections;

public class LocalValues : MonoBehaviour {

	public static bool gameFlg =false;

	public static int score = 0;
	public static int totalScore = 0;
	public static int beatNum = 0;
	public static int shotNum = 0;
	public static int hitRate = 0;
	public static int rankNum = 0;
	public static string rank = "E";

	public static bool reversal = true;


	public static void Init(){
		score = 0;
		totalScore = 0;
		beatNum = 0;
		shotNum = 0;
		hitRate = 0;
		rankNum = 0;
		rank = "E";
	}


}
