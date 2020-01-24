using System;
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
        var heroClass = new ActorClass("Hero Class", 
            HeroStatTemplate(), new PlayerExpTemplate()
        );

        var bossClass = new ActorClass("Boss Class",
            BossStatTemplate(), null
        );

        var hero = new Actor("Hero", 1, heroClass);
        var boss = new Actor("Boss", 1, bossClass);

        hero.Buffs.Apply(new TestBuff());
		
        var turnHandler = new TurnHandler();
        turnHandler.Queue(hero);
        turnHandler.Queue(boss);

        for(var i = 0; i < 5; i++) {
            Console.WriteLine("Turn {0}", i);
            turnHandler.UpdateTurns();

            while(true) {
                var actor = turnHandler.GetNextReady();
                if (actor == null)
                    break;

                // Handle Turn update here
                Console.WriteLine("{0} Turn", actor.Name);

                // Reset actor and requeue
                actor.Resources.Set(
                    ResourceKeys.Speed,
                    actor.Stats.Speed
                );
                turnHandler.Queue(actor);
            }
        }
	}

    public static StatTemplate HeroStatTemplate() {
        return new StatTemplateBuilder()
            .Set(StatKeys.Health, 100, 0)
                .Link(StatKeys.Health, StatKeys.Stamina, 25f)
                .Link(StatKeys.Health, StatKeys.Strength, 1f)
            .Set(StatKeys.Mana, 1000, 20)
                .Link(StatKeys.Mana, StatKeys.Wisdom, 50f)
            .Set(StatKeys.Strength, 0, 0)
            .Set(StatKeys.Stamina, 0, 0)
            .Set(StatKeys.Wisdom, 12, 10)
            .Set(StatKeys.Speed, 1f)
            .Build();
    }

    public static StatTemplate BossStatTemplate() {
        return new StatTemplateBuilder()
            .Set(StatKeys.Health, 200, 50)
                .Link(StatKeys.Health, StatKeys.Stamina, 25f)
                .Link(StatKeys.Health, StatKeys.Strength, 1f)
            .Set(StatKeys.Mana, 1000, 20)
                .Link(StatKeys.Mana, StatKeys.Wisdom, 50f)
            .Set(StatKeys.Strength, 20, 5)
            .Set(StatKeys.Stamina, 8, 4)
            .Set(StatKeys.Wisdom, 12, 10)
            .Set(StatKeys.Speed, 2f)
            .Build();
    }
}