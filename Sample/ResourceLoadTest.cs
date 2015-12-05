using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using AscheLib;
using UniRx;

public class ResourceLoadTest : MonoBehaviour {
	public Texture2D texture;

	public void LoadTexture(string textureName) {
		ResourceLoader.Instance.LoadResourceObservable<Texture2D>(textureName)
			.Subscribe(result => {
				texture = result;
			});
	}

	public void PlaySound(string soundName) {
		SoundManager.Instance.LoadAndPlaySoundObservable(soundName, SoundType.SE, true).Subscribe();
	}

	public void OpenUIContent() {
		//フェードあり
		ResourceLoader.Instance.LoadResourceObservable<GameObject>("FadePanel", false, true)
			.Zip(ResourceLoader.Instance.LoadResourceObservable<GameObject>("OneButtonPopup", false, true), (x, y) => new{fadePanel = x, popup = y})
			.Subscribe(zipPrefab => {
				FadePanel fadePanel = UIContentManager.Instance.ContentInstantiate<FadePanel>(zipPrefab.fadePanel);
				OneButtonPopup popup = UIContentManager.Instance.ContentInstantiate<OneButtonPopup, OneButtonPopup.Sender>(zipPrefab.popup,
					new OneButtonPopup.Sender().Chain(sender => {
						sender.okButtonTextString = "OK";
						sender.titleTextString = "テストタイトル";
						sender.commentTextString = "テストコメント";
					}));
				popup.OnCloseStartAsObservable().Subscribe(_ => fadePanel.Close()).AddTo(fadePanel);
			});

		//フェード無し
		/*ResourceLoader.Instance.loadResourceObservable<GameObject>("OneButtonPopup")
			.Subscribe(prefab => {
				UIContentManager.Instance.contentInstantiate<OneButtonPopup, OneButtonPopup.Sender>(prefab,
					new OneButtonPopup.Sender().chain(sender => {
						sender.buttonTextString = "OK";
						sender.titleTextString = "テストタイトル";
						sender.commentTextString = "テストコメント";
						sender.buttonAction = () => {};
					}));
			});*/
	}

	public void TestTransition() {
		ResourceLoader.Instance.LoadResourceObservable<GameObject>("MaskFadePanel", false, true)
			.Subscribe(prefab => {
				FadePanel fadePanel = UIContentManager.Instance.ContentInstantiate<FadePanel>(prefab);
				GeneralUtility.CallLateAfterSecond(() => fadePanel.Close(), 2);
			});
	}
}