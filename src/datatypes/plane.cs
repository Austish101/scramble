using System.Dynamic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;

public class Plane
{
    public Plane(StatBlock stats)
    {
        this.name = stats.name;
        this.description = stats.description;
        this.current_health = 100;
        this.speed = stats.speed; 
        this.agility = stats.agility;
        this.armour = stats.armour;
        this.defence = stats.defence; 
        this.reliability = stats.reliability;
        this.maintainability = stats.maintainability;
    }

    private string name
    {
        get { return name;}
        set { name = value;}
    }
    private string id
    {
        get { return id;}
        set { id = value;}
    }
    private string description
    {
        get { return description;}
        set { description = value;}
    }
    private int current_health  // between 0 and 100
    {
        get { return current_health;}
        set { current_health = value;}
    }
    // stats - between 0 and 100
    private int speed  // speed of flight
    {
        get { return speed * (currentHealth / 100);}
        set { speed = value;}
    }
    private int agility  // manouverability
    {
        get { return agility * (currentHealth / 100);}
        set { agility = value;}
    }
    private int attack  // forward facing guns, cannons, etc.
    {
        get { return attack * (currentHealth / 100);}
        set { attack = value;}
    }
    private int defence  // turrets coverage and damage
    {
        get { return defence * (currentHealth / 100);}
        set { defence = value;}
    }
    private int armour  // armour plating, self sealing fuel tanks, etc. 
    {
        get { return armour * (currentHealth / 100);}
        set { armour = value;}
    }
    private int reliability  // low means more health damage taken during flight
    {
        get { return reliability;}
        set { reliability = value;}
    }
    private int maintainability  // low means more time taken to repair at base
    {
        get { return maintainability;}
        set { maintainability = value;}
    }

    /// <summary>
    /// we are attacking the opponent
    /// get damage dealt to them based on our compared:
    ///     agility, speed,
    /// and our attack
    /// </summary>
    /// <param name="opponent">
    /// the opponent plane being attacked
    /// </param>
    /// <returns>
    /// damage to be dealt to them from our attack
    /// </returns>
    public int getDamageToDefender(Plane opponent)
    {
        return Math.Round(this.attack * ((this.agility / opponent.agility) + (this.speed / opponent.speed) / 2));
    }

    public int getDamageToAttacker(Plane opponent)
    {
        return Math.Round(this.defence * ((this.agility / opponent.agility) + (this.speed / opponent.speed) / 2));
    }

    public void takeDamage(int incomingDamage)
    {
        current_health -= Math.Round(incomingDamage * (100 / armour));
    }

    /// <summary>
    /// this aircraft is attacking the opponent
    /// </summary>
    /// <param name="opponent"></param>
    public void engage(Plane opponent)
    {
        // get damage to us
        int damageToUs = opponent.getDamageToAttacker(this);
        // get damage to them
        int damageToThem = this.getDamageToDefender(opponent);
        // resolve damage
        this.takeDamage(damageToUs);
        opponent.takeDamage(damageToThem);
    }
}