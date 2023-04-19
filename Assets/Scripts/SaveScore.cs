using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveScore : MonoBehaviour
{
    public int hs;
    public Text IR;
    public Text IIR;
    public Text IIIR;
    public Text IVR;
    public Text VR;
    
    void Start()
    {
        IR.text = "" + PlayerPrefs.GetInt("I");
        IIR.text = "" + PlayerPrefs.GetInt("II");
        IIIR.text = "" + PlayerPrefs.GetInt("III");
        IVR.text = "" + PlayerPrefs.GetInt("IV");
        VR.text = "" + PlayerPrefs.GetInt("V");
    }

    public void R()
    {
        VR.text = IVR.text;
        IVR.text = IIIR.text;
        IIIR.text = IIR.text;
        IIR.text = IR.text;
        IR.text = "" + PlayerPrefs.GetInt("records");
        PlayerPrefs.SetInt("V",PlayerPrefs.GetInt("IV"));
        PlayerPrefs.SetInt("IV",PlayerPrefs.GetInt("III"));
        PlayerPrefs.SetInt("III",PlayerPrefs.GetInt("II"));
        PlayerPrefs.SetInt("II",PlayerPrefs.GetInt("I"));
        PlayerPrefs.SetInt("I",hs);
    }
    
    private void Update()
    {
        if(PlayerPrefs.GetInt("records") > PlayerPrefs.GetInt("I"))
        {
            hs = PlayerPrefs.GetInt("records");
            R();
        }

        PlayerPrefs.Save();

        //if(Input.GetKeyDown(KeyCode.U))
        //{
        //    PlayerPrefs.DeleteAll();
        //}
    }
}
