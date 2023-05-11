 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureInPicture : MonoBehaviour
{
    public enum hAlignment { left, centre, right};
    public enum vAlignment { top, middle, bottom};

    public hAlignment horAlign = hAlignment.left;
    public vAlignment vertAlign = vAlignment.top;

    public enum UnitsIn { pixels, screen_percentage};
    public UnitsIn unit = UnitsIn.screen_percentage;

    public int PiPwidth = 50;
    public int PiPheight = 50;
    public int xoffset = 0;
    public int yoffset = 0;

    public bool update = true;
    private int hsize, vsize, hloc, vloc;

    // Start is called before the first frame update
    void Start()
    {
        AdjustCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (update)
            AdjustCamera();
    }

    void AdjustCamera() 
    {

        int sw = Screen.width;
        int sh = Screen.height;
        float swPercent = sw * 0.01f;
        float shPercent = sh * 0.01f;
        float xoffPercent = xoffset * swPercent;
        float yoffPercent = yoffset * shPercent;
        int xoff;
        int yoff;

        if(unit == UnitsIn.screen_percentage) 
        {
            hsize = PiPwidth * (int)swPercent;
            vsize = PiPheight * (int)shPercent;
            xoff = (int)xoffPercent;
            yoff = (int)yoffPercent;
        }
        else 
        {
            hsize = PiPwidth;
            vsize = PiPheight;
            xoff = xoffset;
            yoff = yoffset;
        }

        switch (horAlign) 
        {
            case hAlignment.left:
                hloc = xoff;
                break;
            case hAlignment.right:
                int justifiedRight = (sw - hsize);
                hloc = (justifiedRight - xoff);
                break;
            case hAlignment.centre:
                float justifiedCenter = (sw * 0.5f) - (hsize - 0.5f);
                hloc = (int)(justifiedCenter - xoff);
                break;
        }

        switch (vertAlign) 
        {
            case vAlignment.top:
                int justifiedTop = sh - vsize;
                vloc = justifiedTop - yoff;
                break;
            case vAlignment.bottom:
                vloc = yoff;
                break;
            case vAlignment.middle:
                float justifiedMiddle = (sh * 0.5f) - (vsize * 0.5f);
                vloc = (int)(justifiedMiddle - yoff);
                break;
        }

        GetComponent<Camera>().pixelRect = new Rect(hloc, vloc, hsize, vsize);
    }
}
