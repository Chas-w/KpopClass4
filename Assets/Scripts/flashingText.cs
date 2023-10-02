using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class flashingText : MonoBehaviour
{

    [SerializeField] TMP_Text textComponenet;
    //edit x amplitude or y amplitude 
    [SerializeField] float Xamp;
    [SerializeField] float Yamp;

    
    // Update is called once per frame
    void Update()
    {
        textComponenet.ForceMeshUpdate(); //gets text
        

        var textInfo = textComponenet.textInfo; //stores info

        for (int i = 0; i < textInfo.characterCount; ++i) //gets characters
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible) //skips invisible characters
            {
                continue;
            }
            #region create effect
            //get vertices of each text char
            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            //for loop to get each 4 verts of each char
            for (int j = 0; j < 4; ++j)
            {
                var orig = verts[charInfo.vertexIndex + j];
                //overrides og char location with new location
                //using sin + cos causes it to oscilate and *time makes chars move over time
                //create a wave effect w/ the text
                Vector3 effect = new Vector3(Mathf.Cos(Time.time * Xamp + orig.y * 0.01f), Mathf.Sin(Time.time * Yamp + orig.x * 0.01f), 0);

                //use effect
                verts[charInfo.vertexIndex + j] = orig + effect;

            }
            #endregion
            
            # region updating current text with created effect
            for (int x = 0; x < textInfo.meshInfo.Length; ++x)
            {
                var meshInfo = textInfo.meshInfo[x];
                meshInfo.mesh.vertices = meshInfo.vertices;
                textComponenet.UpdateGeometry(meshInfo.mesh, x);

            }
            #endregion
        }


    }
}