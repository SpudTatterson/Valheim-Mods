using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Grindstone
{
    public class GrindStone : MonoBehaviour, Hoverable
    {
        public StatusEffect[] SharpnessEffects;
        public Player lastPlayer;
        public int Time = 10;
        public string Name = "Grindstone";
        private bool FindPlayerInRange()
        {
            Player player = Player.GetAllPlayers().Find(i => InRange(i.transform, 2.5f));

            if (player != null)
            {
                lastPlayer = player;
                return true;
            }

            return false;
        }
        public void FixedUpdate()
        {
            if (!GetComponent<ZNetView>())
                return;

            if (FindPlayerInRange() == true)
            {
                if (ZInput.GetButtonDown("Use"))
                {

                    StartCoroutine(GiveSharpness(SharpnessEffects, lastPlayer, Time));
                }
            }

        }

        public static IEnumerator GiveSharpness(StatusEffect[] effects, Player lastplayer, int time)
        {
            // var player = Player.m_localPlayer;
            Console.instance.Print("");
            if (lastplayer.m_rightItem == null)
            {
                Console.instance.Print("");
                yield return null;
            }

            ItemDrop.ItemData weapon = lastplayer.m_rightItem;
            int number = 1;
            if (weapon.m_shared.m_skillType.ToString() == "Axes")
            {

                number = 0;
                Console.instance.Print("Axe");
            }
            if (weapon.m_shared.m_skillType.ToString() == "Swords")
            {
                Console.instance.Print("Sword");
                number = 1;
            }
            if (weapon.m_shared.m_skillType.ToString() == "Knifes")
            {
                Console.instance.Print("Knife");
                number = 2;
            }
            if (weapon.m_shared.m_skillType.ToString() == "Polearms")
            {
                Console.instance.Print("Polearms");
                number = 3;
            }
            if (weapon.m_shared.m_skillType.ToString() == "Spears")
            {
                Console.instance.Print("Spears");
                number = 4;
            }
            weapon.m_shared.m_equipStatusEffect = effects[number];
            if (weapon.m_shared.m_skillType.ToString() != "Axes"
               && weapon.m_shared.m_skillType.ToString() != "Swords"
               && weapon.m_shared.m_skillType.ToString() != "Knifes"
               && weapon.m_shared.m_skillType.ToString() != "Polearms"
               && weapon.m_shared.m_skillType.ToString() != "Spears")
            {
                weapon.m_shared.m_equipStatusEffect = null;
            }

            // Console.instance.Print(weapon.m_shared.m_damages.m_chop.ToString());

            yield return new WaitForSeconds(time);

            weapon.m_shared.m_equipStatusEffect = null;
        }

        private bool InRange(Transform target, float range)
        {
            return Vector3.Distance(transform.position, target.position) <= range;
        }

        public string GetHoverText()
        {
            return "Press " + "E" + " to Sharpen your weapon";
        }

        public string GetHoverName()
        {
            return Name;
        }

    }
}