using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;




    public class GrindStone : MonoBehaviour, Hoverable, Interactable
    {
        public StatusEffect[] SharpnessEffects;
        public Player lastPlayer;
        public int Time = 10;
        public string m_Name = "Grindstone";
      /*  private bool FindPlayerInRange()
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
              
               // StartCoroutine(GiveSharpness(SharpnessEffects, lastPlayer, Time));
             }
            }

        }*/
        
        public static IEnumerator GiveSharpness(StatusEffect[] effects, int time)
        {                
        var lastplayer = Player.m_localPlayer;
        Console.instance.Print("");
        if (lastplayer.m_rightItem == null)
        {
            Console.instance.Print("");
            yield return null;
        }       

        ItemDrop.ItemData weapon = lastplayer.m_rightItem;
        int number = 1;
          if(weapon.m_shared.m_skillType.ToString() == "Axes")
        {
            number = 0;            
        }
        if (weapon.m_shared.m_skillType.ToString() == "Swords")
        {          
            number = 1;
        }
        if (weapon.m_shared.m_skillType.ToString() == "Knifes")
        {           
            number = 2;
        }
        if (weapon.m_shared.m_skillType.ToString() == "Polearms")
        {         
            number = 3;
        }
        if (weapon.m_shared.m_skillType.ToString() == "Spears")
        {
            number = 4;
        }
        StatusEffect clone = effects[number].Clone();
        weapon.m_shared.m_equipStatusEffect = clone;
        if(weapon.m_shared.m_skillType.ToString() != "Axes" 
           && weapon.m_shared.m_skillType.ToString() != "Swords"
           && weapon.m_shared.m_skillType.ToString() != "Knifes"
           && weapon.m_shared.m_skillType.ToString() != "Polearms"
           && weapon.m_shared.m_skillType.ToString() != "Spears")
        {
            weapon.m_shared.m_equipStatusEffect = null;
        }
        yield return new WaitForSeconds(60);       
        weapon.m_shared.m_equipStatusEffect.m_ttl = 540;
        yield return new WaitForSeconds(60);
        weapon.m_shared.m_equipStatusEffect.m_ttl = 480;
        yield return new WaitForSeconds(60);
        weapon.m_shared.m_equipStatusEffect.m_ttl = 420;
        yield return new WaitForSeconds(60);
        weapon.m_shared.m_equipStatusEffect.m_ttl = 360;
        yield return new WaitForSeconds(60);
        weapon.m_shared.m_equipStatusEffect.m_ttl = 300;
        yield return new WaitForSeconds(60);
        weapon.m_shared.m_equipStatusEffect.m_ttl = 240;
        yield return new WaitForSeconds(60);
        weapon.m_shared.m_equipStatusEffect.m_ttl = 180;
        yield return new WaitForSeconds(60);
        weapon.m_shared.m_equipStatusEffect.m_ttl = 120;
        yield return new WaitForSeconds(60);
        weapon.m_shared.m_equipStatusEffect.m_ttl = 60;
        yield return new WaitForSeconds(60);     
        weapon.m_shared.m_equipStatusEffect.m_ttl = 600;
        weapon.m_shared.m_equipStatusEffect = null;
    }

       /* private bool InRange(Transform target, float range)
        {
            return Vector3.Distance(transform.position, target.position) <= range;
        }*/
        
    public string GetHoverText()
        {
            return "\n[<color=yellow><b>E</b></color>] Sharpen your weapon";
        }

    public string GetHoverName()
        {
            return "Grindstone";
        }

    public bool Interact(Humanoid user, bool hold)
    {
        if(user.m_rightItem == null)
        {
            return false;
        }
        StartCoroutine(GiveSharpness(SharpnessEffects, Time));
        return true;
    }

    public bool UseItem(Humanoid user, ItemDrop.ItemData item)
    {
        return false;
    }
}

