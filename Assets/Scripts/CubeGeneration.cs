using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeGeneration : MonoBehaviour {
	public GameObject Cube, AudioMan, PanelError, PanelAsk;
	AudioSource myAudio;
	public int count;
	public List<GameObject> temps;
	public InputField myField;
	public Text Error;
	//float taimer;





	void Start () {
		myAudio = AudioMan.GetComponent<AudioSource> ();
				
	}
	

	void Update () {
		if (myAudio.isPlaying)
			Equaliser ();

		//Field ();
	}

	public void Gener()
	{
		Delete ();
		for (int i = 0; i < count; i++) {
			GameObject temp = Instantiate (Cube, new Vector3 (transform.position.x + i*2, transform.position.y, transform.position.z), Quaternion.identity);
			temp.transform.localScale  = new Vector3 (transform.localScale.x, Random.Range (1f, 9f), transform.localScale.z);
			temp.GetComponent<Renderer>().material.color = new Color(Random.value,Random.value,Random.value, 1);
			temps.Add (temp);
		}
	}

	public void Delete(){
		for (int i = 0; i < temps.Count; i++) {
			Destroy (temps[i]); 
		}
		temps.Clear ();

		PanelAsk.SetActive (true);
	}


	public void Sort(){

		for (int i = 0; i < temps.Count; i++) {
			for (int j =0; j < temps.Count-1; j++) {
				if (temps [j].transform.localScale.y > temps [j+1].transform.localScale.y) 
				{

					Vector3 tempVector = temps [j].transform.position;
					temps [j].transform.position = temps [j+1].transform.position;
					temps [j+1].transform.position = tempVector;

					GameObject temp1 = temps [j];
					temps [j] = temps [j + 1];
					temps [j + 1] = temp1;
				}
			}

		}
	}


	public void Quit(){
		Application.Quit ();

	}


	public void AudioPlay(){

		if (!myAudio.isPlaying) {
			myAudio.Play ();
		}
		else
			myAudio.Pause ();
	
	}

	public void Equaliser(){
		float[] mas = new float[1024];
		AudioListener.GetSpectrumData (mas, 0, FFTWindow.Blackman);

		for (int i = 0; i < temps.Count; i++) {
			Vector3 vec = temps [i].transform.localScale;
			vec.y = Mathf.Lerp (vec.y, mas [i] * 500, Time.deltaTime);

			temps [i].transform.localScale = vec;
		}
	}

	public void Field(){
		bool isNumber;
		PanelError.SetActive (false);
		isNumber = int.TryParse (myField.text, out count);
		if (isNumber && count <= 15 && count > 0) {
			Gener ();
			PanelAsk.SetActive (false);
		} else {
			PanelError.SetActive (true);
			Error.text = "Error";
		}
	}

}
