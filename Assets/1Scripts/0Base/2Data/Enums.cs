public class Enums 
{

    // MAIN
    public enum GameStage { launch ,loading, login, menu, inRoom, heroCreation, 
        
        game, statistics, rewards, heroUpgrade, 
        
        camp, inventory, abilities, heroStats,

        StoryChoice
    
    }
    public enum ServerGameStage { creatingHeroes, gameLevel, camp, finished }

    public enum HeroCreationPage { heroClass, race, origin, startItem, overview }

    public enum PopupType { item, ability, StoryRewards }



    //statistics
    public enum StatisticsType { damage, tank, heal, control, caster }





    // hero
    public enum Races { human, elf, orc, dwarf }
    public enum ClassHelmTypes  { none, openFaceHelm, mask, fullHelmet }




    // animator
    public enum Animations
    {
        Idle, Run, Fight, FightRange, Cast, // bool
        CastTaunt = 10, CastJumpAttack, CastWhirlAttack, CastArea, CastShieldAttack, CastProjectile, CastHandUp, CastSpell, DrinkPotion,      // trigger
        Stun = 50, Frozen, Dead, Knocked // bool control
    }
    public enum AnimatorStates { normal, casting, underControl, dead, playingSpecialAnimation }






    // Universal Button
    public enum UniversalButtonType 
    {
        available, unavailable, choosen,  // nfts
        noPlace, notEnoughMoney, canBuy, takeFromLoot, putToLoot, canSell, cannotSell, // items

        canLearn, CanUpgrade, notEnoughPointsToLearn, notEnoughPointsToUpgrade, requirementsNotFulfilled, maxAmount, maxLevel, // talents

        backButton // info
    }








    // Inventory
    public enum Rarity  { common, uncommon, rare, epic, legendary }




    // ABILITIES!!!!
    public enum AbilityTarget  { Caster, Enemies, allies, lowestHpAlly, randomTarget, environment, closestAnotherAlly }
    public enum AbilityActionType  { damage, heal, buff, taunt, summon, restoreMana, damageAllyAndBuffCaster, resurrection, projectile }
    public enum AbilityCostType  { mana, health, }  // gold???
    public enum AbilityCastTypes  { withoutTarget, pointing }
    public enum AbilityConcreteTypes 
    {
        attackSkill, defenseSkill, rangeSkill,
        healSpell,
        fireSpell, iceSpell, arcaneSpell, electroSpell, natureSpell, demonologySpell, deathSpell, lightSpell

    }
    public enum DamageTypes { physical, fire, ice, electro, arcane, light, dark, poison, bloodshed, pure }
    //public enum AbilityCasterStages : byte { nothing, pointing }
    public enum MagicSchools  {  None, fire, ice, arcane, electro, nature, demonology, death, light }



    // Passives
    public enum AbilityType  { Class, Common, Witchcraft}
    public enum PassiveTypes 
    {
        stats, buffByTrigger, special,
        debuffToEnemyByTrigger, aura, buffOnStartLevel, castByTrigger,
        areaEffectByTrigger,
        permanentStatsByTrigger,
        healByTrigger
    }

    public enum PassiveTrigger 
    {
        none,

        recievedDamage, attacked, castAnyAbility, attackAnimation, attacking,

        hpLessThen10, hpLessThen25, hpLessThen50, hpLessThen75,

        castAnySpell,
        castArcaneSpell, castFireSpell, castIceSpell, castElectroSpell, castNatureSpell, castDemonologySpell, castDeathSpell, castLightSpell,

        block, evade, parry,

        criticalHit,
        recievedCriticalHit,

        dayStartOfLevel, nightStartOfLevel,

        spellCrit, 
        fireSpellCrit, arcaneSpellCrit, iceSpellCrit, electroSpellCrit, natureSpellCrit, DemonologySpellCrit, DeathSpellCrit, LightSpellCrit
    }






    // BUFF
    public enum CharacterStates
    {
        none, invulnerable, invisible, magicInvulnerable, physicInvulnerable, controlInvulnerable
    }
    public enum ControlState { none, Stun, Knocked, Frozen, Silenced, CantMove, Hexed, Disarmed, Sleep, Fear }







    // LEVELS STORIES

    public enum Region {  ForestGlade, Dungeon }
    public enum LevelType { Battle, StoryChoice }




    // REWARDS

    public enum RewardType { Item, LevelUp, Passive, Gold, TalentPoint}

    public enum RewardReciever { PlayerWithTag, PlayerWithoutTag, All, OneItemToLoot }


    // TAGS
    public enum Tag { None, Elf, HighElf, Dwarf, Human, Undead, Orc,
                Knight, Paladin,
    
    }







    // stats 
    public enum StatType {  summ, basic, passive, item, buff }

    public enum MainAttributes { strength, agility, intelligence, 
        strengthAgility, strengthIntelligence, agilityIntelligence,
        strengthAgilityIntelligence }

    public enum Stats
    {
        hp, hpPc, mp, mpPc,

        strength, strengthPc, agility, agilityPc, intelligence, intelligencePc, stamina, staminaPc, spirit, spiritPc, charisma, charismaPc, luck, luckPc,

        coeffAllDamage = 18, coeffAllArmor, coeffAllAttackSpeed, coeffAllMovementSpeed, coeffAllSpellSpeed, coeffAllIncomingDamage, coeffAllIncomingHeal,

        meleeAttack = 25, meleeAttackPc, rangeAttack, rangeAttackPc, attackSpeedFinal, attackSpeedRate, masteryFinal, masteryRate,
        critChanceFinalPc, critChanceRate, critDamageFinalPc, critDamageRate, armorPenetrationFinal, armorPenetrationRate,

        defensePc, armor, armorPc, blockFinalPc, blockRate, evadeFinalPc, evadeRate, parryFinalPc, parryRate, hpRegeneration,

        spellPower, spellPowerPc, spellDamage, spellDamagePc, spellHeal, spellHealPc, spellCritChanceFinalPc, spellCritChanceRate, spellCritDamageFinalPc,
        spellCritDamageRate, spellSpeedRate, spellSpeedFinal, magicDefenseFinalPc, magicDefenseRate, concentrationRate,
        spellPenetrationFinalPc, spellPenetrationRate, mpRegeneration,

        firePowerPc, icePowerPc, electricPowerPc, arcanePowerPc, darkArtsPowerPc, lightPowerPc, poisonPowerPc, bloodShedPowerPc,

        fireResistPc, iceResistPc, electricResistPc, arcaneResistPc, darkArtsResistPc, lightResistPc, poisonResistPc, bloodShedResistPc,

        physicalResilience, magicResilience, mentalResilience, damageReturnMelee,

        //
        accuracy,

        meleeDamage = 90, rangeDamage = 91,

        //
        stealth, backstabDamage, damageFromFront, damageFromBack, lifestealPc, spellLifestealPc, statusResistance, movementSpeed,
        //

        none = 100
    }








    public enum Emotions
    { 
        Normal,
        Angry = 1,
        Fun = 2,
        Joy = 3,
    }












}
