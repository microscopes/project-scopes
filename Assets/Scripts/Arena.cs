﻿/*!
 * @file Arena.cs
 * @brief Contains Arena class definition.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Arena : MonoBehaviour 
{
	public GameObject head;

	private Color[] mianPixMap;				// Table of texture pixel colors

	private MeshRenderer renderer;			// Arena renderer component
	private Texture2D mainTexture;			// Arena texture

	private int arenaSize;

	private float frameRate;
	private float nextFrame;

	private int initSize = 4;
	private float initSpeed = 2.0f;

	private int noOfPlayers = 2;
	private List<Player> players = new List<Player>();


	// Use this for initialization
	void Start() 
	{		
		frameRate 		= 0.01f;
		arenaSize 		= 600;

		renderer 	= GetComponent<MeshRenderer> ();
		mainTexture = new Texture2D (arenaSize,arenaSize);
		mianPixMap 	= mainTexture.GetPixels ();

		mainTexture.filterMode = FilterMode.Trilinear;

		// Setting texture to Arena renderer
		renderer.materials[0].mainTexture = mainTexture;

		// Setting Arena background color
		SetArenaBgColor (Color.black);

		for (int i = 0; i < noOfPlayers; i++) {
			players.Add (new Player (Instantiate(head)));
		}

		foreach (Player player in players) {
			player.SetupPlayer(new Vector2 (Random.Range(20.0f, arenaSize-20), 
											Random.Range(20.0f, arenaSize-20)), 
											Random.Range(0.0f, 2.0f), 
											initSize, 
											initSpeed, 
											Color.red);
		}

		players [1].ChangeColor (Color.green);
		players [1].SetControlKeys (KeyCode.Z, KeyCode.X);
	}


	// Update is called once per frame
	void Update () 
	{
		if (Time.time > nextFrame) 
		{
			nextFrame = Time.time + frameRate;

			foreach (Player player in players) 
			{
				player.Turn ();
			}
				
			RedrawArena ();
		}

		if(Input.GetKeyDown(KeyCode.G))
		{
			foreach (Player player in players) 
			{
				player.DoubleSize ();
			}
		}

		if(Input.GetKeyDown(KeyCode.H))
		{
			foreach (Player player in players) 
			{
				player.ReduceSize ();
			}
		}

		if(Input.GetKeyDown(KeyCode.J))
		{
			foreach (Player player in players) 
			{
				player.DoubleSpeed ();
			}
		}

		if(Input.GetKeyDown(KeyCode.K))
		{
			foreach (Player player in players) 
			{
				player.ReduceSpeed ();
			}
		}

		// helpful for getting KeyCode of pressed key (to set preferred keys for players in GUI)

		/*foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKeyDown(kcode))
				Debug.Log("KeyCode down: " + kcode);
		}*/
	}


	void RedrawArena ()
	{
		DrawPlayers ();

		mainTexture.SetPixels (mianPixMap);
		mainTexture.Apply (false);
	}


	void DrawPlayers ()
	{
		foreach (Player player in players)
		{
			float playerPosX = player.GetX ();
			float playerPosY = player.GetY ();

			Color playerColor = player.GetColor ();

			int playerSize = player.GetSize ();
			float playerSpeed = player.GetSpeed ();

			float sinDegree = Mathf.Sin (Mathf.PI * player.GetDegree ());
			float cosDegree = Mathf.Cos (Mathf.PI * player.GetDegree ());

			for (int j = 0; j < Mathf.CeilToInt(playerSpeed); j++) 
			{
				if (player.isActive ()) 
				{
					if (playerSpeed < 1) {
						playerPosX += sinDegree * playerSpeed;
						playerPosY += cosDegree * playerSpeed;
					} else {
						playerPosX += sinDegree;
						playerPosY += cosDegree;
					}

					float posX = playerPosX - playerSize * cosDegree;
					float posY = playerPosY + playerSize * sinDegree;

					for (int i = 1; i < 2 * playerSize; i++)
					{
						float posX2 = posX + i * playerSize * cosDegree / playerSize;
						float posY2 = posY - i * playerSize * sinDegree / playerSize;

						for (int k = 1; k <= Mathf.CeilToInt (playerSize * 0.7f) + 3; k++) {
							DrawPixel (mianPixMap, posX2 - sinDegree * k / 5.0f, posY2 - cosDegree * k / 5.0f, playerColor, player.isVisible(), true);
						}
							
						if (/*j % Mathf.CeilToInt(playerSpeed)-1 == 0 &&*/ (i % 2 == 0 || i == 1) && DrawPixel (mianPixMap, posX2 + 2.5f * sinDegree, posY2 + 2.5f * cosDegree, Color.green, player.isVisible(), false)) {
								player.Collision ();
						} 

						DrawPixel (mianPixMap, posX2, posY2, playerColor, player.isVisible(), true);
					}
				}
			}

			playerPosX %= arenaSize;
			playerPosY %= arenaSize;

			if (playerPosX < 0) 
			{
				playerPosX = (arenaSize + playerPosX) % arenaSize;
			}

			if (playerPosY < 0) 
			{
				playerPosY = (arenaSize + playerPosY) % arenaSize;
			}

			player.SetX (playerPosX);
			player.SetY (playerPosY);

			player.MoveHead (playerPosX + sinDegree, playerPosY + cosDegree);
		}
	}


	bool DrawPixel (Color[] map, float x, float y, Color col, bool visible, bool collCheck)
	{
		int pos = (Mathf.FloorToInt(y) * arenaSize + Mathf.FloorToInt(x)) % map.Length;

		if (pos < 0) 
		{
			pos = (map.Length + pos) % map.Length;
		}

		if (visible) 
		{
			if (map [pos] == Color.black) 
			{
				if (collCheck) {
					map [pos] = col;
				}
				return false;
			} 
			else 
			{
				return true;
			}
		}

		return false;
	}


	void SetArenaBgColor (Color col)
	{
		for (int i = 0; i < mianPixMap.Length; i++) 
		{
			mianPixMap [i] = col;
		}

		mainTexture.SetPixels (mianPixMap);
		mainTexture.Apply (false);
	}
}
