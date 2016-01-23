using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using AscheLib;
using UniRx;

public class TestSpritePopup : UIContentControllerBase, IUIContentDataRecipient<TestSpritePopup.Sender> {
	public class Sender {
		public string titleTextString = "Title";
		public Sprite showSprite = null;
		public string okButtonTextString = "OK";
	}

	public Text titleText;
	public Image showSpriteImage;
	public Text okButtonText;
	public Button okButton;

	public Sender receiveData {get; set;}

	protected override void UIInit() {
		titleText.text = receiveData.titleTextString;
		showSpriteImage.sprite = receiveData.showSprite;
		okButtonText.text = receiveData.okButtonTextString;
	}
}