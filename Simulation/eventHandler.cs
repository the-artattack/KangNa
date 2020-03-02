using System.Collections.Generic;
using System.Linq;

public static class eventHandler
{
    #region rice parameters
    public static double RiceReduction(double quantity, double damage)
    {
        return quantity - (quantity * damage / 100);
    }

    public static double WaterReduction(double volume, double reductRatio)
    {
        return volume - (volume * reductRatio / 100);
    }
    #endregion

    /**
    public static void makeReservior()
    {
        useReservoir = true;
    }
    **/
}