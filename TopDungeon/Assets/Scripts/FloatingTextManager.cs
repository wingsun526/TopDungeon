using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
   public GameObject textContainer;
   public GameObject textPrefab;

   private List<FloatingText> floatingTexts = new List<FloatingText>();

  

   private void Update()
   {
      foreach (FloatingText txt in floatingTexts)
      {
         txt.UpdateFloatingText();
      }
   }
   
   public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
   {
      FloatingText floatingText = GetFloatingText();

      floatingText.txt.text = msg;
      floatingText.txt.fontSize = fontSize;
      floatingText.txt.color = color;
      floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position); // Transfer world space to screen space so we can use it in the UI
      floatingText.motion = motion;
      floatingText.duration = duration;
      
      floatingText.Show();
   }
   
   private FloatingText GetFloatingText()
   {
      FloatingText txt = floatingTexts.Find(t => !t.active); // (from my understanding) reusing old FloatingText instance that are inactive, https://gameprogrammingpatterns.com/object-pool.html, 
                                                                           // a technique that save resources.

      if (txt == null)
      {
         txt = new FloatingText();
         txt.go = Instantiate(textPrefab);                        // give it a gameObject which is a floating text prefab  (This instance of the FloatingText class still doesn't have a body/gameObject.)
         txt.go.transform.SetParent(textContainer.transform);     
         txt.txt = txt.go.GetComponent<Text>();                   // the floating text gameObject has a text component, link it to this (class)FloatingText instance txt variable/property
         floatingTexts.Add(txt);
      }

      return txt;
   }
}
