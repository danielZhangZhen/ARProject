public class ShopCarShowHide : myTool.showHideObjectsNoSetActive {
    public void OnShow()
    {
        Car_ShowHide(true);
    }
    public void OnHide()
    {
        Car_ShowHide(false);
    }
    protected override void Car_ShowHide(bool showCar)
    {
        base.Car_ShowHide(showCar);
    }
}
