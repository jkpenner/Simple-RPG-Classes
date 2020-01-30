using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RPG;
using RPG.Items;
using RPG.Actors;
using RPG.Actors.Stats;
using RPG.Actors.Resources;
using RPG.Actors.Leveling;
using RPG.Spells;
using Game;

public class Program {
	public static void Main() {
        Console.Clear();

        var healthStat = Stats.Get(StatKeys.Health);
        var speedStat = Stats.Get(StatKeys.Speed);

        var generalResources = new ResourceTemplate();
        generalResources.SetContext(
            new ResourceContext() {
                resource = Game.Resources.Get(ResourceKeys.Health),
                defaultValue = new ResourceValueContext(healthStat),
                minimumValue = new ResourceValueContext(0f),
                maximumValue = new ResourceValueContext(healthStat),
            }
        );
        generalResources.SetContext(
            new ResourceContext() {
                resource = Game.Resources.Get(ResourceKeys.Speed),
                defaultValue = new ResourceValueContext(speedStat),
                minimumValue = new ResourceValueContext(0f),
                maximumValue = new ResourceValueContext(speedStat),
            }
        );

        var heroClass = new ActorClass("Hero Class", 
            HeroStatTemplate(), generalResources, new SimpleExpTemplateAsset()
        );

        var bossClass = new ActorClass("Boss Class",
            BossStatTemplate(), generalResources, null
        );

        

        var hero = new Actor("Hero", 1, heroClass,
            Stats.Get(StatKeys.Health), Stats.Get(StatKeys.Speed), 
            Stats.Get(StatKeys.ExpBonus));
        var boss = new Actor("Boss", 1, bossClass,
            Stats.Get(StatKeys.Health), Stats.Get(StatKeys.Speed), 
            Stats.Get(StatKeys.ExpBonus));

        //var buffs = new BuffCollection(hero);
        //buffs.Apply(new TestBuff());

        //hero.Resources.SetToDefault(ResourceKeys.Health);


        
		
        var turnHandler = new TurnHandler();
        turnHandler.Queue(hero);
        turnHandler.Queue(boss);

        var gameOver = false;
        for(var i = 0; i < 10 && !gameOver; i++) {
            Console.WriteLine("|========================================================");
            Console.WriteLine("| Turn {0}", i);
            var actors = turnHandler.GetActors();
            for(var j = 0; j < actors.Count; j++) {
                Console.Write("| ");
                actors[j].PrintStats();
            }
            Console.WriteLine("|--------------------------------------------------------\n");

            turnHandler.UpdateTurns();
            
            while(true) {
                var actor = turnHandler.GetNextReady();
                if (actor == null)
                    break;

                // Handle Turn update here
                Console.WriteLine("{0} Turn", actor.Name);
                actor.PrintStats();


                var healthRes = Resources.Get(ResourceKeys.Health);
                actor.Modify(healthRes, -30);

                // Reset actor and requeue
                actor.SetToDefault(Game.Resources.Get(ResourceKeys.Speed));
                turnHandler.Queue(actor);

                Console.WriteLine();

                if (CheckWinCondition(turnHandler.GetActors())) {
                    Console.WriteLine("Game Over");
                    gameOver = true;
                    break;
                }
            }
        }
	}

    public static bool CheckWinCondition(ReadOnlyCollection<Actor> actors) {
        int alive = 0;
        foreach(var actor in actors) {
            if (actor.IsAlive)
                alive += 1;
        }
        return alive != 2;
    }

    public static StatTemplate HeroStatTemplate() {
        var health      = Stats.Get(StatKeys.Health);
        var mana        = Stats.Get(StatKeys.Mana);
        var stamina     = Stats.Get(StatKeys.Stamina);
        var strength    = Stats.Get(StatKeys.Strength);
        var wisdom      = Stats.Get(StatKeys.Wisdom);
        var speed       = Stats.Get(StatKeys.Speed);

        return new StatTemplateBuilder()
            .Set(health, 100, 0)
                .Link(health, stamina, 25f)
                .Link(health, strength, 1f)
            .Set(mana, 1000, 20)
                .Link(mana, wisdom, 50f)
            .Set(strength, 0, 0)
            .Set(stamina, 0, 0)
            .Set(wisdom, 12, 10)
            .Set(speed, 1f)
            .Build();
    }

    public static StatTemplate BossStatTemplate() {
        var health      = Stats.Get(StatKeys.Health);
        var mana        = Stats.Get(StatKeys.Mana);
        var stamina     = Stats.Get(StatKeys.Stamina);
        var strength    = Stats.Get(StatKeys.Strength);
        var wisdom      = Stats.Get(StatKeys.Wisdom);
        var speed       = Stats.Get(StatKeys.Speed);

        return new StatTemplateBuilder()
            .Set(health, 200, 50)
                .Link(health, stamina, 25f)
                .Link(health, strength, 1f)
            .Set(mana, 1000, 20)
                .Link(mana, wisdom, 50f)
            .Set(strength, 20, 5)
            .Set(stamina, 8, 4)
            .Set(wisdom, 12, 10)
            .Set(speed, 2f)
            .Build();
    }
}

