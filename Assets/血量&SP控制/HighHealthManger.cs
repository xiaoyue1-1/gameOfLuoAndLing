public static class HighHealthManager
{
    private static int currentHighHealth = 100;


    public static int HighHealthChange(int delta)
    {
        if (delta == 0)
        {
            return currentHighHealth;
        }
        else
        {
            currentHighHealth += delta;
            return 0;
        }
    }

    public static void SetHighHealth(int health)
    {
        currentHighHealth = health;
    }
}