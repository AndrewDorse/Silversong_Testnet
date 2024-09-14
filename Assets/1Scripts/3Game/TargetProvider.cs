using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Silversong.Game
{
    public static class TargetProvider
    {

        // GET ALLIES

        public static (ITarget, string) GetClosestAlly(Enemy enemy)
        {
            if (GameMaster.instance.LocalHero == null)
            {
                return (null, "");
            }
            if (GameMaster.instance.OtherHeroes == null) // what about solo game??? TODO
            {
                return (null, "");
            }
            if (GameMaster.instance.OtherHeroes.Count == 0)
            {
                return (null, "");
            }

            List<OtherHero> otherHeroes = GameMaster.instance.OtherHeroes;

            float bestDistance = 100;
            string targetId = string.Empty;
            ITarget target = null;

            for (int i = 0; i < otherHeroes.Count; i++)
            {
                // if hero can be attacked

                float distance = Vector3.Distance(otherHeroes[i].transform.position, enemy.transform.position);

                if (distance < bestDistance)
                {
                    targetId = otherHeroes[i].UserId;
                    bestDistance = distance;
                    target = otherHeroes[i];
                }
            }

            // check local hero too

            float localHeroDistance = Vector3.Distance(GameMaster.instance.LocalHero.transform.position, enemy.transform.position);

            if (localHeroDistance < bestDistance)
            {
                targetId = DataController.instance.LocalData.UserId;
                target = GameMaster.instance.LocalHero;
            }


            return (target, targetId);
        }

        public static (ITarget, string) GetAllyTargetByTargetId(string targetId)
        {
            ITarget target = null;
            string newTargetId = string.Empty;

            List<OtherHero> otherHeroes = GameMaster.instance.OtherHeroes;

            for (int i = 0; i < otherHeroes.Count; i++)
            {
                if (otherHeroes[i].UserId == targetId)
                {
                    target = otherHeroes[i];
                    newTargetId = otherHeroes[i].UserId;
                    break;
                }
            }

            if (DataController.instance.LocalData.UserId == targetId)
            {
                target = GameMaster.instance.LocalHero;
                newTargetId = DataController.instance.LocalData.UserId;
            }



            return (target, newTargetId);
        }




        // GET ENEMIES

        public static bool IsEnemiesInMeleeZone(Vector3 position)
        {
            List<Enemy> enemies = GameMaster.instance.Enemies;

            for (int i = 0; i < enemies.Count; i++)
            {
                float distance = Vector3.Distance(enemies[i].transform.position, position);

                if (distance < 4f)
                {
                    return true;
                }
            }

            return false;
        }

        public static ITarget GetEnemyById(string enemyId)
        {
            List<Enemy> enemies = GameMaster.instance.Enemies;

            ITarget result = null;

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].Id == enemyId)
                {
                    result = enemies[i];
                }
            }

            return result;
        }






        public static List<ITarget> GetEnemyTargetsInRadius(Vector3 point, float radius)
        {
            List<Enemy> enemies = GameMaster.instance.Enemies;

            List<ITarget> list = new List<ITarget>();

            for (int i = 0; i < enemies.Count; i++)
            {

                if (Vector3.Distance(point, enemies[i].GetPosition()) <= radius)
                {
                    list.Add(enemies[i]);
                }
            }

            return list;
        }





    }
}