using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class trailRendererRandom : MonoBehaviour
{
	[SerializeField]private TrailRenderer trailRenderer;
	Gradient gradient;
	GradientColorKey[] colorKey;
	GradientAlphaKey[] alphaKey;

	private Start () {
		trailRenderer = gameObject.GetComponent<TrailRenderer>();
		trailRenderer.material = new Material(Shader.Find("Sprites/Default"));
		trailRenderer.colorGradient = NewGrad();
	}

	Gradient NewGrad() {
		gradient = new Gradient();
		colorKey = new GradientColorKey[2];
		colorKey[0].color = GetColor();
		colorKey[0].time = 0.0f;

		//colorKey[1].color = GetColor();
		//colorKey[1].time = 0.25f;
		//colorKey[2].color = GetColor();
		//colorKey[2].time = 0.50f;
		//colorKey[3].color = GetColor();
		//colorKey[3].time = 0.75f;

		colorKey[1].color = GetColor();
		colorKey[1].time = 1.0f;

		alphaKey = new GradientAlphaKey[2];
		alphaKey[0].alpha = 1.0f;
		alphaKey[0].time = 0.0f;

		//alphaKey[1].alpha = 0.75f;
		//alphaKey[1].time = 0.25f;
		//alphaKey[2].alpha = 0.50f;
		//alphaKey[2].time = 0.50f;
		//alphaKey[3].alpha = 0.25f;
		//alphaKey[3].time = 0.75f;

		alphaKey[1].alpha = 0.0f;
		alphaKey[1].time = 1.0f;
		gradient.SetKeys(colorKey, alphaKey);
		return gradient;
	}

	Color GetColor() {
		var R = (byte)Random.Range(0f, 255f);
		var G = (byte)Random.Range(0f, 255f);
		var B = (byte)Random.Range(0f, 255f);
		return new Color32(R, G, B, 1);
	}
}
