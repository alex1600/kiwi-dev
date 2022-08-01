using UnityEngine;

/// <summary>
/// Put it on a prefab
/// </summary>
public class AudioObject : MonoBehaviour
{
	
    [SerializeField]
    private AudioSource source;
	
    [SerializeField]
    private AudioClip clip;

    /// <summary>
    /// Play sound at start
    /// </summary>
    private void Start()
    {
		source.clip = clip;
    }
	
	public void PlayOneShot(){
		source.PlayOneShot(clip);
	}
}