public static class HealthManager
{
    private static int currentHealth = 100; 


    public static int HealthChange(int delta)
    {
        if (delta == 0)
        {
            return currentHealth;
        }
        else
        {
            currentHealth += delta;
            return 0;
        }
    }

    public static void SetHealth(int health)
    {
        currentHealth = health;
    }
}