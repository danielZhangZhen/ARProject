using UnityEngine;

public class changeTextures : UICtrler {

    public Texture2D[] SomeTextures;
    public void SetTexture(Texture2D[] textures)
    {
        SomeTextures = textures;
    }

    protected override void changeFunction(string name)
    {
        base.changeFunction(name);
        shareMat.mainTexture = SomeTextures[getIndex];
 
    }

}
