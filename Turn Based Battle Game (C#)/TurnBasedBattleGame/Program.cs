using System;

class Detective
{
    private int health = 100;
    private int attackDamage = 25;
    private int healingCapacity = 20;
    private int maxHealth = 100;

    public int Health
    {
        get { return health; }
        private set { health = value < 0 ? 0 : (value > maxHealth ? maxHealth : value); }
    }

    public Detective()
    {
        SpawnDetective();
        DisplayStats();
    }

    private int RandomInRange(int min, int max) => new Random().Next(min, max + 1);

    public int CalculateAttack() => attackDamage + RandomInRange(5, 15);

    public int CalculateHeal() => healingCapacity + RandomInRange(10, 20);

    public void TakeDamage(int damage) => Health -= damage;

    public void Heal(int healAmount) => Health += healAmount;

    private void SpawnDetective()
    {
        Console.WriteLine("\n==================================================");
        Console.WriteLine("          DETECTIVE: RAVENPORT'S FINEST       ");
        Console.WriteLine("==================================================\n");
        Console.WriteLine("Detective: \"You're not escaping justice tonight!\"");
    }

    public void DisplayStats()
    {
        Console.WriteLine("\n---------------------------------------------------");
        Console.WriteLine("              DETECTIVE'S STATS");
        Console.WriteLine("---------------------------------------------------");
        Console.WriteLine($"Health: {Health}/{maxHealth}");
        Console.WriteLine($"Quick Strike: {attackDamage}");
        Console.WriteLine($"Adrenaline Boost: {healingCapacity}");
        Console.WriteLine("Strike Bonus: 5 to 15");
        Console.WriteLine("Adrenaline Boost Bonus: 10 to 20");
    }

    public void ShowAttack(int damage)
    {
        Console.WriteLine("             🔍 CITY CHASE 🔍");
        Console.WriteLine("============================================");
        Console.WriteLine($"Detective's strike dealt {damage} damage!");
        Console.WriteLine("--------------------------------------------");
    }

    public void ShowHeal(int healAmount)
    {
        Console.WriteLine("             🔍 CITY CHASE 🔍");
        Console.WriteLine("============================================");
        Console.WriteLine(Health >= maxHealth
            ? "Detective is fully recharged and alert!"
            : $"Detective recovered {healAmount} health.");
        Console.WriteLine("--------------------------------------------");
    }
}

class Thief
{
    private int health = 120;
    private int attackDamage = 20;
    private int maxHealth = 120;

    public int Health
    {
        get { return health; }
        set { health = value < 0 ? 0 : (value > maxHealth ? maxHealth : value); }
    }

    public Thief()
    {
        SpawnThief();
        DisplayStats();
    }

    private int RandomInRange(int min, int max) => new Random().Next(min, max + 1);

    public int CalculateAttack() => attackDamage + RandomInRange(3, 12);

    public void TakeDamage(int damage) => Health -= damage;

    private void SpawnThief()
    {
        Console.WriteLine("\n==================================================");
        Console.WriteLine("     BLACKLIGHT SYNDICATE: ELUSIVE THIEF ");
        Console.WriteLine("==================================================\n");
        Console.WriteLine("Thief: \"You'll never catch me, detective!\"");
    }

    public void DisplayStats()
    {
        Console.WriteLine("\n--------------------------------------------------");
        Console.WriteLine("                THIEF'S STATS");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine($"Health: {Health}/{maxHealth}");
        Console.WriteLine($"Sneak Strike: {attackDamage}");
        Console.WriteLine("Sneak Bonus: 3 to 12");
    }

    public void ShowAttack(int damage)
    {
        Console.WriteLine($"Thief's strike dealt {damage} damage!");
        Console.WriteLine("============================================");
    }
}

class SilentPursuitGame
{
    bool isGameExited;
    Detective detective;
    Thief thief;

    public void GameLoop()
    {
        while (!isGameExited)
        {
            ShowStory();
            StartMenu();
            if (!isGameExited)
                RestartMenu();
        }
    }

    private void ShowStory()
    {
        Console.Clear();
        Console.WriteLine("\n==================================================");
        Console.WriteLine("          SILENT PURSUIT: A CITY HUNT ");
        Console.WriteLine("==================================================\n");
        Console.WriteLine("It's midnight in Ravenport, shadows stretch across deserted streets...");
        Console.WriteLine("You, a seasoned detective, have cornered a thief from the Blacklight Syndicate,");
        Console.WriteLine("the criminal behind countless heists. It's time to end this chase once and for all!");
        Console.WriteLine("--------------------------------------------------\n");
        Console.WriteLine("                    CONFRONT!");
        Console.WriteLine("--------------------------------------------------\n");
    }

    private void StartMenu()
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("  Press S to Start the Confrontation");
        Console.WriteLine("  Press any other key to Exit");
        Console.WriteLine("==================================================");

        string input = Console.ReadLine()?.ToUpper();
        if (input == "S")
        {
            Console.Clear();
            StartBattle();
        }
        else
            ExitGame();
    }

    private void RestartMenu()
    {
        Console.WriteLine("\n==================================================");
        Console.WriteLine("  Press R to Restart");
        Console.WriteLine("  Press any other key to Exit");
        Console.WriteLine("==================================================");

        string input = Console.ReadLine()?.ToUpper();
        if (input != "R")
            ExitGame();
    }

    private void StartBattle()
    {
        detective = new();
        thief = new();

        while (detective.Health > 0 && thief.Health > 0)
            BattleTurn();
    }

    private void BattleTurn()
    {
        Console.WriteLine("\n==================================================");
        Console.WriteLine("               🔍 CHOOSE YOUR ACTION 🔍");
        Console.WriteLine("==================================================");
        Console.WriteLine("[A] Attack the Thief");
        Console.WriteLine("[H] Use Adrenaline Boost");
        Console.WriteLine("==================================================");
        Console.Write("Choice: ");

        switch (Console.ReadLine()?.ToUpper())
        {
            case "A":
                PlayerAttack();
                if (CheckGameOver()) return;
                EnemyAttack();
                break;

            case "H":
                PlayerHeal();
                EnemyAttack();
                break;

            default:
                Console.WriteLine("Invalid choice! Try again.");
                break;
        }

        ShowStats();
    }

    private void PlayerAttack()
    {
        int damage = detective.CalculateAttack();
        thief.TakeDamage(damage);
        detective.ShowAttack(damage);
    }

    private void PlayerHeal()
    {
        int heal = detective.CalculateHeal();
        detective.Heal(heal);
        detective.ShowHeal(heal);
    }

    private void EnemyAttack()
    {
        int damage = thief.CalculateAttack();
        detective.TakeDamage(damage);
        thief.ShowAttack(damage);
    }

    private void ShowStats()
    {
        detective.DisplayStats();
        thief.DisplayStats();
    }

    private bool CheckGameOver()
    {
        if (thief.Health <= 0)
        {
            Console.WriteLine("\n==================================================");
            Console.WriteLine("           🎉 CASE CLOSED: JUSTICE SERVED! 🎉");
            Console.WriteLine("==================================================");
            Console.WriteLine("The thief is apprehended and the stolen goods recovered!");
            return true;
        }

        if (detective.Health <= 0)
        {
            Console.WriteLine("\n==================================================");
            Console.WriteLine("               ❌ CASE FAILED ❌");
            Console.WriteLine("==================================================");
            Console.WriteLine("The thief slips away into the night...");
            return true;
        }

        return false;
    }

    private void ExitGame()
    {
        Console.WriteLine("\nThanks for playing Silent Pursuit: A City Hunt!");
        isGameExited = true;
    }
}

class Program
{
    public static void Main()
    {
        SilentPursuitGame game = new SilentPursuitGame();
        game.GameLoop();
    }
}
