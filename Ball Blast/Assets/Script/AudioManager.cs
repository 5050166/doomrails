using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip Click;
    public AudioClip BuyWeapon;
    public AudioClip Coin;
    public AudioClip GonNextlevel;
    public AudioClip KillEnemy;
    public AudioClip HitEnemy;
    public AudioClip SellWeapon;
    public AudioClip Bulid;
    public AudioClip Smoke;
    public AudioClip Rocket;
    public AudioClip watermelon;
    public AudioClip icecream;
    public AudioClip Water;
    public AudioClip Amethyst;
    public AudioClip TimeModBGM;

    public AudioClip BuildFail;//安装失败
    public AudioClip ClickSound; //按钮音效
    public AudioClip GameFail; //游戏失败
    public AudioClip GameStart; //游戏开始
    public AudioClip EnemyRefresh; //敌人刷新
    public AudioClip Playerdie; //玩家死亡
    public AudioClip BatGun; //蝙蝠炮
    public AudioClip IceCreamGun; //冰淇凌炮
    public AudioClip BlockGun; //方块炮
    public AudioClip ArrowGun;//弓箭炮
    public AudioClip BlackGun; //黑炮
    public AudioClip RocketGun;//火箭炮
    public AudioClip LaserGun;//激光炮
    public AudioClip ThunderGun;//闪电炮
    public AudioClip GreenGun;//绿炮
    public AudioClip FireGun;//火炮
    public AudioClip NormalGun;//普通炮
    public AudioClip ThreeWayGun;//三相炮 散弹
    public AudioClip WaveGun;//声波炮
    public AudioClip WaterGun;//水炮
    public AudioClip WatermelonGun;//西瓜炮
    public AudioClip TomatoGun;//西红柿炮
    public AudioClip StarsGun;//星星炮
    public AudioClip RoteGun;//旋转炮
    public AudioClip GearGun;//齿轮炮
    public AudioClip FanGun;//风扇炮
    public AudioClip BigLaserGun;//大激光炮
    public AudioClip BigRocketGun;//大火箭炮
    public AudioClip ElectromagneticGun;//电磁炮
    public AudioClip KnifeGun; //飞刀炮
    public AudioClip Frisbee;//飞盘炮
    public AudioClip MachineGun;//加特林炮
    public AudioClip Cheese;//奶酪炮
    public AudioClip TriangleGun;//三角炮
    public AudioClip NeedleGUn;//针炮
    public AudioClip MoonGun;//月牙炮
    public AudioClip AmethystGun;//紫水晶炮
    public AudioClip HomemadeGun;//山炮 
    
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }
    private void Awake()
    {
        instance = this;
        source = this.transform.GetComponent<AudioSource>();
    }

}
