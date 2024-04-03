using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private bool KeyLock = false;
    public TMP_Text TextBox;

    // Start is called before the first frame update
    void Start()
    {
        //Welcome to Frong {input}
        //Your objective is to gather as many flies as you can {input}
            //show fly
        //you move your tongue with the W S keys or Up and Down Arrows if player two
        //but, uh oh! the flies are trapped in bubbles {input}
            //put bubble on fly
        //every bubble needs 3 hits to pop
            //show hit hit pop
        //but be careful, just cause you popped the bubble doesn't mean your 
        //opponent can't snatch it away from you
            //second player eats fly
        //only the tip of your tongue is sticky enough to effect the bubble, if the bubble hits the back of your tongue, you won't even touch it{}
            //show example
        //every fly is one point {input}
        //if you come across a rare dragonfly, you'll gain 3 points for eating one! {input}
            //show dragon fly
        // on top you have special bars
            //show
        //you gain special points by collecting floating pollen
            //show pollen
        //pollen is so light and small, it doesn't run into anything except the playing field
            //show
        //to use your special, press your corresponding side's shift key [default]{input}
        // use one bar of special to go through two layers of bubblw (if there's only 1 layer left, you'll pierce through the fly and score!)
            //show
        //if you're able to grab 5 special points, the next time you use your special, you'll activate super mode!
            //show
        //for an amount of time, you'll be able to collect any and all flies with one hit
            //show
        //the game ends after the timer hits zero
            //show
        //the highest scoring player wins
            //show
        //be aware, many hazards and obsticles may get in your way, so be careful!
            //show ducks and portals
        //Got all that? Ok! Have fun! You can always come back to review the rules again
    }
    IEnumerable TextBoxing()
    {
        TextBox.text = $"";
        TextBox.text = $"Welcome to Frong!";
        yield return waitForKeyPress();
        TextBox.text = $"Your objective is to eat as many flies as you can";
        yield return waitForKeyPress();
        TextBox.text = $"But, Uh Oh! The flies are trapped in bubbles!";
        yield return waitForKeyPress();
        TextBox.text = $"You move your tongue with the W & S Keys or Up & Down Arrows if Player two";
        yield return waitForKeyPress();
        TextBox.text = $"Use your tongue to pop the bubbles and catch the fly";
        yield return waitForKeyPress();
        TextBox.text = $"Every bubble needs 3 hits to pop";
        yield return waitForKeyPress();
        TextBox.text = $"But be careful. Just because you popped the bubble, doesn't mean your opponent can't snatch it away from you";
        yield return waitForKeyPress();
        TextBox.text = $"Only the tip of your tongue is sticky enough to effect the bubble, if the bubble hits the back of your tongue, you won't even touch it";
        yield return waitForKeyPress();
        TextBox.text = $"Every fly is one point";
        yield return waitForKeyPress();
        TextBox.text = $"If you come across a rare dragonfly, you'll gain 3 points for eating one!";
        yield return waitForKeyPress();
        TextBox.text = $"On top you have your special bar";
        yield return waitForKeyPress();
        TextBox.text = $"You gain special points by collecting floating pollen";
        yield return waitForKeyPress();
        TextBox.text = $"Pollen is so light and small, it doesn't run into anything except the playing field";
        yield return waitForKeyPress();
        TextBox.text = $"To use your special, press your corresponding side's shift key by default";
        yield return waitForKeyPress();
        TextBox.text = $"Use one bar of special to go through two layers of bubble (If there's only 1 layer left, you'll pierce through the fly and score!)";
        yield return waitForKeyPress();
        TextBox.text = $"If you're able to grab 5 special points, the next time you use your special, you'll activate super mode!";
        yield return waitForKeyPress();
        TextBox.text = $"For an period of time, you'll be able to collect any and all flies with one hit";
        yield return waitForKeyPress();
        TextBox.text = $"The game ends after the timer hits zero";
        yield return waitForKeyPress();
        TextBox.text = $"As always, the highest scoring player wins";
        yield return waitForKeyPress();
        TextBox.text = $"Be aware, many hazards and obsticles may get in your way, so be careful!";
        yield return waitForKeyPress();
        TextBox.text = $"Got all that? Ok! Have fun! You can always come back to review the rules again";
        yield return waitForKeyPress();


        yield return null;
    }

    IEnumerable TextClickThrough(float wait)
    {
        KeyLock = true;
        yield return new WaitForSeconds(wait);
        KeyLock = false;

    }
    private IEnumerator waitForKeyPress()
{
    bool done = false;
    yield return new WaitForSeconds(0.1f);
    while(!done) // essentially a "while true", but with a bool to break out naturally
    {
        if(Input.anyKey)
        {
            done = true; // breaks the loop
        }
        yield return null; // wait until next frame, then continue execution from here (loop continues)
    }
 
    // now this function returns
}
    // Update is called once per frame
    void Update()
    {
        
    }
}
