﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Assembly : MonoBehaviour
{
	public GameObject thisPanel;
   public void closeButt()
	{
		thisPanel.SetActive(false);
	}
}