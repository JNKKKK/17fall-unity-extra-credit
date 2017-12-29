using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject dropdown;
    public Bitmap image;
    public bool goPressed = false;
    public UnityEngine.UI.Button goRemote, goLocal;
    public UnityEngine.UI.InputField textbox;
    public float GroundWidth = 40, GroundHeight = 20;
    public float WorldWidth = 78, WorldHeight = 38;
    public UnityEngine.UI.Toggle ToggleBlack, ToggleWhite, ToggleColors;

    private List<string> options;
    private System.Random rnd = new System.Random();

    public class xy
    {
        public int x, y;
        public xy(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public xy randXY()
    {
        int x = 0, y = 0;
        while ((x < (this.GroundWidth / 2 + 5)) && (x > -(this.GroundWidth / 2 + 5)) && (y > -(this.GroundHeight / 2 + 4)) && (y < (this.GroundHeight / 2 + 2)))
        {
            x = rnd.Next((int)-this.WorldWidth / 2, (int)this.WorldWidth / 2);
            y = rnd.Next((int)-this.WorldHeight / 2, (int)this.WorldHeight / 2);
        }
        return new xy(x, y);
    }

    void Start()
    {
        goRemote.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(GoRemoteOnClick);
        goLocal.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(GoLocalOnClick);

        string[] dirs = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Assets\\ResourceImages\\"));
        options = new List<string>(dirs);
        for (int i = options.Count - 1; i > -1; i--)
        {
            if (options[i].IndexOf(".meta") > -1)
            {
                options.RemoveAt(i);
            }
            else
            {
                options[i] = "ResourceImages" + options[i].Substring(options[i].LastIndexOf('\\'));
            }
        }
        dropdown.GetComponent<UnityEngine.UI.Dropdown>().ClearOptions();
        dropdown.GetComponent<UnityEngine.UI.Dropdown>().AddOptions(options);
    }

    void GoRemoteOnClick()
    {
        System.Net.WebRequest request =
            System.Net.WebRequest.Create(textbox.GetComponent<UnityEngine.UI.InputField>().text);
        System.Net.WebResponse response = request.GetResponse();
        System.IO.Stream responseStream =
            response.GetResponseStream();
        this.image = new Bitmap(responseStream);
        if (((float)this.image.Height) / this.image.Width > GroundHeight / GroundWidth)
        {
            this.image = ResizeImage(this.image, (int)Math.Truncate(this.image.Width * (GroundHeight / this.image.Height)), (int)Math.Truncate(GroundHeight));
        }
        else
        {
            this.image = ResizeImage(this.image, (int)Math.Truncate(GroundWidth), (int)Math.Truncate(this.image.Height * (GroundWidth / this.image.Width)));
        }
        Debug.Log(this.image.Width);
        Debug.Log(this.image.Height);
        this.goPressed = true;
    }

    void GoLocalOnClick()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Assets\\" + dropdown.GetComponent<UnityEngine.UI.Dropdown>().captionText.text);
        this.image = (Bitmap)Image.FromFile(path, true);
        if (((float)this.image.Height) / this.image.Width > GroundHeight / GroundWidth)
        {
            this.image = ResizeImage(this.image, (int)Math.Truncate(this.image.Width * (GroundHeight / this.image.Height)), (int)Math.Truncate(GroundHeight));
        }
        else
        {
            this.image = ResizeImage(this.image, (int)Math.Truncate(GroundWidth), (int)Math.Truncate(this.image.Height * (GroundWidth / this.image.Width)));
        }
        Debug.Log(this.image.Width);
        Debug.Log(this.image.Height);
        this.goPressed = true;
    }

    private static Bitmap ResizeImage(Image image, int width, int height)
    {
        var destRect = new Rectangle(0, 0, width, height);
        var destImage = new Bitmap(width, height);
        destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
        using (var graphics = System.Drawing.Graphics.FromImage(destImage))
        {
            graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            using (var wrapMode = new System.Drawing.Imaging.ImageAttributes())
            {
                wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }
        }
        return destImage;
    }

    void Update()
    {

    }
}
