using AscheLib;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;

public class TestScript : AscheBehaviour {

	public UnityEngine.UI.Image image = null;

	//開始時
	public void Start () {
		ResourceLoader.Instance.LoadSpriteObservable("Asche_CancelButton")
			.Subscribe(resource => {
				image.sprite = resource;
			});
	}

	//更新処理
	public void Update () {
	
	}
}